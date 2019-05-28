
namespace MyPlace.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using MyPlace.Filters;
    using MyPlace.DataModels;
    using MyPlace.Models.Account;

    [TypeFilter(typeof(AddHeaderActionFilter))]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signIn;

        public AccountController(SignInManager<User> signIn) =>
            _signIn = signIn;


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [AutoValidateAntiforgeryToken]
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

