
namespace MyPlace.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using MyPlace.Filters;
    using MyPlace.DataModels;
    using MyPlace.Models.Account;
    using System.Security.Cryptography;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using MyPlace.Hubs;

    [TypeFilter(typeof(AddHeaderActionFilter))]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signIn;

        public AccountController(SignInManager<User> signIn) =>
            _signIn = signIn;

        public AccountController(IHubContext<CommentHub> hubContext)
        {
            hubContext.Clients.All.SendAsync("BadComment");
        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [AllowAnonymous]
        public IActionResult Login([FromForm]LoginBindingModel model)
        {

            if(ModelState.IsValid)
            {
                var user = _signIn.UserManager.Users
                .FirstOrDefault(usr => usr.UserName.Equals(model.UserName));

                if (user != null)
                {
                    _signIn.SignInAsync(user, false).Wait();

                    return RedirectToAction("Index", "Catalog");
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register([FromForm]RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { UserName = model.UserName };

                var password = SHA256.Create("admin123");

                IdentityResult irUser = await _signIn.UserManager.CreateAsync(user, model.Password);

                if (irUser.Succeeded)
                {
                    await _signIn.UserManager.AddToRoleAsync(user, model.Role);

                    return RedirectToAction("Login", "Account");
                }

                return View("Register");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("Index", "Catalog");

            // With Tag helper
            // asp-route-returnUrl="@Url.Action("Index", "Catalog", new { area = "" })"
        }
    }
}
