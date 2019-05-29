
namespace MyPlace.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Areas.Administrator.Models;

    [Area("Administrator")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(CreateEstablishmentBindingModel model)
        {
            return View();
        }
    }
}
