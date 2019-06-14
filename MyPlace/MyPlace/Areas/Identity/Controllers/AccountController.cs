
namespace MyPlace.Areas.Identity.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using MyPlace.Filters;
    using MyPlace.DataModels;
    using MyPlace.Models.Account;
    using MyPlace.Infrastructure.Logger;
    using MyPlace.Common;
    using MyPlace.Common;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    [Area("Identity")]
    [TypeFilter(typeof(AddHeaderActionFilter))]
    public class AccountController : Controller
    {
        private readonly IDatabaseLogger _logger;
        private readonly SignInManager<User> _signIn;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signIn, IDatabaseLogger logger, UserManager<User> userManager)
        {
            _signIn = signIn;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _signIn.UserManager.Users
                .FirstOrDefault(usr => usr.UserName.Equals(model.UserName));
                
                var result = await _signIn.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);

                var userRoles = await _userManager.GetRolesAsync(user);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains(GlobalConstants.ManagerRole))
                        return RedirectToAction("Notes", "Notes", new { area = "Notes" });

                    return RedirectToAction("Index", "Catalog");
                }
            }
            await _logger.WARN().Log($"Fail attempt to login for user: {model.UserName.ToUpper()}");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            await _logger.DEBUG().Log($"User: {this.User.Identity.Name.ToUpper()}, logout");

            return RedirectToAction("Index", "Catalog");

            // With Tag helper
            // asp-route-returnUrl="@Url.Action("Index", "Catalog", new { area = "" })"
        }
    }
}
