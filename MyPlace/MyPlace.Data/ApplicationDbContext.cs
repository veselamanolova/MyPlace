
namespace MyPlace.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using MyPlace.DataModels;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityCategory> EntityCategories { get; set; }
        public DbSet<UserEntity> UsersEntities { get; set; }
        public DbSet<Note> Notes { get; set; }

        // For Profanity filter
        public DbSet<ForbiddenWord> ForbiddenWords { get; set; }

        // Logs
        public DbSet<EventLog> EventLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);

                builder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(builder);
        }
    }
}


