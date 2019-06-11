
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

    [Area("Identity")]
    [TypeFilter(typeof(AddHeaderActionFilter))]
    public class AccountController : Controller
    {
        private readonly IDatabaseLogger _logger;
        private readonly SignInManager<User> _signIn;

        public AccountController(SignInManager<User> signIn, IDatabaseLogger logger)
        {
            _signIn = signIn;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginBindingModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _signIn.UserManager.Users
                .FirstOrDefault(usr => usr.UserName.Equals(model.UserName));

                var result = await _signIn.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Catalog");
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
