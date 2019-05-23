
namespace MyPlace.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Models;
    using MyPlace.Models.Catalog;
    using MyPlace.Services.Contracts;

    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogContex) =>
            _catalogService = catalogContex ?? throw new ArgumentNullException(nameof(catalogContex));

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


        public JsonResult GetAll() =>
             Json(_catalogService.AutocompleteGetAll());
    }
}

