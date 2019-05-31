
namespace MyPlace.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Moderator")]
    public class ModeratorController : Controller
    {
        public IActionResult Index() => View();
    }
}