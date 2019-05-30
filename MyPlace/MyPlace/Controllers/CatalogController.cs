
namespace MyPlace.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyPlace.Models;
    using MyPlace.Models.Catalog;
    using MyPlace.Services.Contracts;

    public class CatalogController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogContex, IMemoryCache cache)
        {
            _cache = cache;
            _catalogService = catalogContex;
        }
            

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            ViewData["Filter"] = searchString;

            var establishments = await _catalogService.ReadAllAsync<CatalogListingModel>();

            if (!String.IsNullOrEmpty(searchString))
                establishments = establishments
                    .Where(filter =>
                        filter.Title.ToLower().Contains(searchString.ToLower()));

            int pageSize = 1;
            return View(await PaginatedList<CatalogListingModel>.CreateAsync(establishments, pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> Establishment(int Id) =>
            View(await _catalogService.GetByIdAsync<EstablishmentIndexModel>(Id));



        public JsonResult GetAll()
        {
            IEnumerable<string> cacheEntry;

            if (!_cache.TryGetValue("AutocompleteValues", out cacheEntry))
            {
                cacheEntry = _catalogService.AutocompleteGetAll();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                _cache.Set("AutocompleteValues", cacheEntry, cacheEntryOptions);
            }
            return Json(cacheEntry);
        }

        // For caching HTML -> cache Tag helper
        // <Cache expires-after="@TimeSpan.FromMinutes(10)">
        //     ...........
        // </cache>

        // expires-sliding="@TimeSpan.FromSeconds(60)" -> Ако страницата не е отваряна 60 секунди се изчиства от кеша
    }
}

