
namespace MyPlace.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ManagerController : Controller
    {
        public IActionResult Index() => View();
    }
}