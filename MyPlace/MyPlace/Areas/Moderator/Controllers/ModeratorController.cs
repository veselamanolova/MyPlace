
namespace MyPlace.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using MyPlace.Common;

    [Area("Moderator")]
    [Authorize(Roles = GlobalConstants.ModeratorRole)]
    public class ModeratorController : Controller
    {
        public IActionResult Index() => View();
    }
}
