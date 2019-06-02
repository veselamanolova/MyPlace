
namespace MyPlace.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using MyPlace.DataModels;
    using MyPlace.Models.Account;
    using MyPlace.Services.Contracts;

    [Area("Administrator")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly SignInManager<User> _signIn;

        public AdminController(SignInManager<User> signIn, IAdminService adminService)
        {
            _signIn = signIn;
            _adminService = adminService;
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

                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }

                return View("Register");
            }
            return View(model);
        }

        public IActionResult Delete(int entityId, int commentId)
        {
            _adminService.Delete(entityId, commentId);
            return RedirectToAction("Establishment", "Catalog", new { area = "", id = entityId });
        }
    }
}
