
namespace MyPlace
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Hubs;
    using MyPlace.Data;
    using MyPlace.Services;
    using MyPlace.DataModels;
    using MyPlace.Infrastructure;
    using MyPlace.Services.Contracts;
    using AutoMapper;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
            }).AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Account/Login";
                options.LogoutPath = $"/Account/Logout";
                options.AccessDeniedPath = $"/Account/AccessDenied";
            });


            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserEntitiesService, UserEntitiesService>();
            services.AddScoped<IEntityCategoriesService, EntityCategoriesService>();

            services.AddSignalR();
            services.AddAutoMapper(GetType().Assembly, typeof(Entity).Assembly);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            /*
                services.Configure<RazorViewEngineOptions>(options =>
                {
                    // List of default paths for views
                    var viewsList = options.AreaViewLocationFormats;

                    // Clear the default paths
                    options.AreaViewLocationFormats.Clear();

                    // Add my own path
                    options.AreaViewLocationFormats.Add("/Administrator/Views/Admin/Index");
                });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Catalog/Error");
                app.UseHsts();
            }

            RoleSeeder.Seed(serviceProvider);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<CommentHub>("/commentHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
            );

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Moderator}/{action=Index}/{id?}"
            );

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Manager}/{action=Index}/{id?}"
            );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Catalog}/{action=Index}/{id?}");
            });
        }
    }
}
