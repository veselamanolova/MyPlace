
namespace MyPlace.Infrastructure.CustomMiddlewares
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using MyPlace.Common;
    using MyPlace.DataModels;

    public class SeederMiddleware
    {
        private readonly RequestDelegate _next;

        public SeederMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdminRole));
                await roleManager.CreateAsync(new IdentityRole(GlobalConstants.ManagerRole));
                await roleManager.CreateAsync(new IdentityRole(GlobalConstants.ModeratorRole));
            }

            if (!userManager.Users.Any(user => user.UserName == "admin"))
            {
                var admin = new User { UserName = "admin" };
                await userManager.CreateAsync(admin, "admin123");
                await userManager.AddToRoleAsync(admin, "Administrator");
            }
            await _next(httpContext);
        }
    }
}


