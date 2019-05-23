
namespace MyPlace
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyPlace.Data;
    using MyPlace.Data.Models;
    
    public class Program
    {
        public static void Main(string[] args)
        {           
            var host = CreateWebHostBuilder(args).Build();
           // SeedDatabaseAsync(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static async Task SeedDatabaseAsync(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                await SeedRoleAsync(scope, "Administrator");
                await SeedRoleAsync(scope, "Manager");
                await SeedAdminAsync(scope);
                await SeedManagerAsync(scope); 
            }
        }

        private static async Task SeedRoleAsync(IServiceScope scope, string role)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Roles.Any(u => u.Name == role))
            {
                return;
            }
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole { Name = role });
        }

        private static async Task SeedAdminAsync(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Users.Any(u => u.UserName == "Admin"))
            {
                return;
            }
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser = new ApplicationUser { UserName = "Admin", Email = "admin@admin.admin" };
            var createAdminUser = await userManager.CreateAsync(adminUser, "Admin123@");
            if (createAdminUser.Succeeded)
            {
                userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
            }
        }


        private static async Task SeedManagerAsync(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Users.Any(u => u.UserName == "Manager"))
            {
                return;
            }
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser = new ApplicationUser { UserName = "Manager", Email = "Manager@Manager.com" };
            var createAdminUser = await userManager.CreateAsync(adminUser, "Admin123@");
            if (createAdminUser.Succeeded)
            {
                userManager.AddToRoleAsync(adminUser, "Manager").Wait();
            }
        }
    }
}

