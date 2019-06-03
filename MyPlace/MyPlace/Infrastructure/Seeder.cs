
namespace MyPlace.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyPlace.Data;
    using MyPlace.DataModels;
    using MyPlace.Infrastructure.Contracts;

    public class Seeder : ISeeder
    {
        private readonly SignInManager<User> _signIn;

        public Seeder(SignInManager<User> signIn) =>
            _signIn = signIn;

        public void SeedRoleAndAdmin(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            string[] roles = new string[] { "Administrator", "Moderator", "Manager" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                    context.SaveChangesAsync();
                }               
            }

            if (!context.Users.Any(user => user.UserName == "admin"))
            {
                var admin = new User { UserName = "admin" };
                _signIn.UserManager.CreateAsync(admin, "admin123");
                _signIn.UserManager.AddToRoleAsync(admin, "Administrator");
                context.SaveChangesAsync();
            }
        }
    }
}


