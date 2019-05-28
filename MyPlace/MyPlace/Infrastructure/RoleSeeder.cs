
namespace MyPlace.Infrastructure
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyPlace.Data;

    public static class RoleSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            string[] roles = new string[] { "Administrator", "Moderator", "Manager" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role)) 
                    roleStore.CreateAsync(new IdentityRole(role));
            }
            context.SaveChangesAsync();
        }
    }
}

