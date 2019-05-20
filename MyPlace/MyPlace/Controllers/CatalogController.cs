
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

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["Filter"] = searchString;

            var establishments = await _catalogService.ReadAll<CatalogListingModel>();

            if (!String.IsNullOrEmpty(searchString))
                establishments = establishments
                    .Where(filter =>
                        filter.Title.ToLower().Contains(searchString.ToLower()));

            return View(new CatalogIndexModel
            {
                EntitiesList = establishments
            });
        }

        public async Task<IActionResult> Create(int id, string text)
        {
            await _catalogService.CreateReply(id, text);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Establishment(int Id) =>
            View(await _catalogService.GetById<EstablishmentIndexModel>(Id));
    }
}

