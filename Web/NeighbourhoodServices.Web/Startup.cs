﻿namespace NeighbourhoodServices.Web
{
    using CloudinaryDotNet;

    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NeighbourhoodServices.Data;
    using NeighbourhoodServices.Data.Common;
    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Data.Repositories;
    using NeighbourhoodServices.Data.Seeding;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Services.Data.Service;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Services.Messaging;
    using NeighbourhoodServices.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication()
                .AddGoogle(option =>
                {
                    option.ClientId = this.configuration["Google:ClientId"];
                    option.ClientSecret = this.configuration["Google:ClientSecret"];
                })
                .AddFacebook(option =>
            {
                option.AppId = this.configuration["Facebook:AppId"];
                option.AppSecret = this.configuration["Facebook:AppSecret"];
            });

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IAnnouncementService, AnnouncementService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ICommentService, CommentsService>();
            services.AddTransient<IRatingService, RatingService>();

            var account = new Account(
                this.configuration["Cloudinary:ApiName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            var cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);

            // Runtime Refresh
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                        endpoints.MapBlazorHub();
                    });
        }
    }
}
