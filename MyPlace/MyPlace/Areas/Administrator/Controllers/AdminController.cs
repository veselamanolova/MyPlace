
namespace MyPlace.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using MyPlace.DataModels;
    using MyPlace.Models.Account;
    using MyPlace.Services.Contracts;
    using MyPlace.Areas.Administrator.Models;

    [Area("Administrator")]
    //[Authorize(Roles = GlobalConstants.AdminRole)]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly SignInManager<User> _signIn;

        public AdminController(SignInManager<User> signIn, IAdminService adminService)
        {
            _signIn = signIn;
            _adminService = adminService;
        }

        public IActionResult Index() => View();


        [HttpGet]
        public IActionResult CreateEntity() => View();


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateEntity(CreateEntityBindingModel model)
        {
            if (ModelState.IsValid)
                await _adminService.CreateEntityAsync(model.Title, model.Address, model.Description, model.ImageUrl);

            return View();
        }


        [HttpGet]
        public IActionResult CreateUser() => View();


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateUser([FromForm]RegisterBindingModel model)
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


        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }


        public IActionResult Delete(int entityId, int commentId)
        {
            _adminService.Delete(entityId, commentId);
            return RedirectToAction("Establishment", "Catalog", new { area = "", id = entityId });
        }


        public async Task<JsonResult> CheckUsernameAvailability(string name)
        {
            var registeredUsers = await _adminService.RegisteredUsers();
            return Json(registeredUsers.Contains(name));
        }
    }
}
