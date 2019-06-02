
namespace MyPlace.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Manager")]
    public class ManagerController : Controller
    {
        public IActionResult Index() => View();
    }
}