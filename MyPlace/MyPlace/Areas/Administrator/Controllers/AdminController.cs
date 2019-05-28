
namespace MyPlace.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Administrator")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}