
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
    using MyPlace.Data.Repositories;
    using MyPlace.Services.DTOs;    
    using MyPlace.Areas.Notes.Models;
    using System.Collections.Generic;

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


            // Configuring user Identity
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
            }).AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Configuring default paths
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10); // Logout user After 10 Minutes inactivity
                options.LoginPath = $"/Account/Login";
                options.LogoutPath = $"/Account/Logout";
                options.AccessDeniedPath = $"/Account/AccessDenied";
            });


            // Add services
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<IUserEntitiesService, UserEntitiesService>();
            services.AddScoped<IEntityCategoriesService, EntityCategoriesService>();

            //services.AddSingleton<IViewModelMapper<NoteDTO, NoteViewModel>, NoteViewModelMapper>();           
            //services.AddSingleton<IViewModelMapper<List<NoteDTO>, NotesViewModel>, NotesViewModelMapper>();

            // Important! -> Use Distributed cache
            // Configuring Distributed Cache
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "CacheData";
            });

            // Add and Configuring the Sessions
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true; // XSS Security [no JS]

                // Session Timeout 
                // For Testing => Set a short Timeout
                options.IdleTimeout = new TimeSpan(0, 1, 0, 0); // Days, Hours, Minutes, Seconds
            });


            // For user comments
            services.AddSignalR();
            services.AddAutoMapper(GetType().Assembly, typeof(Entity).Assembly);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            /*
                // For global AntiforgeryToken
                services.AddMvc(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });


                // For changing default paths
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

            // Important!!!
            // Add Sessions before Mvc middleware
            app.UseSession();

            // After adding the sessions
            // From Project Folder => PowerShell => execute command:
            // dotnet sql-cache create "Data Source={Connection string}" dbo CacheData

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
