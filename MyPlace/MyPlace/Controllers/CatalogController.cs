
namespace MyPlace.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Models.Catalog;
    using MyPlace.Services.Contracts;

    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogContex) =>
            _catalogService = catalogContex ?? throw new ArgumentNullException(nameof(catalogContex));


        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            ViewData["Filter"] = searchString;

            var establishments = await _catalogService.ReadAll<CatalogListingModel>();

            if (!String.IsNullOrEmpty(searchString))
                establishments = establishments
                    .Where(filter =>
                        filter.Title.ToLower().Contains(searchString.ToLower()));

            int pageSize = 1;
            return View(await PaginatedList<CatalogListingModel>.CreateAsync(establishments, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Create(EstablishmentIndexModel model)
        {
            await _catalogService.CreateReply(model.Id, model.NewPost);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Establishment(int Id) =>
            View(await _catalogService.GetById<EstablishmentIndexModel>(Id));

        public JsonResult GetAll() =>
             Json(_catalogService.Autocomplete());
    }
}

