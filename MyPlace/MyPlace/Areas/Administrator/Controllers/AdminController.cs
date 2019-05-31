
namespace MyPlace.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using MyPlace.DataModels;
    using MyPlace.Models.Account;

    [AllowAnonymous]
    [Area("Administrator")]
    public class AdminController : Controller
    {
        private readonly SignInManager<User> _signIn;

        public AdminController(SignInManager<User> signIn) =>
            _signIn = signIn;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register([FromForm]RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { UserName = model.UserName };

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
    }
}
