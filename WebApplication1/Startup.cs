using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Startup
    {
        private IConfigurationRoot configurationRoot;
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            configurationRoot = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbCOntext>(options => options.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbCOntext>(); 
            //(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env ,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateRoles(serviceProvider).Wait();
        }


        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "HR", "Staff", "Accountant" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var _admin = await UserManager.FindByEmailAsync("admin@admin.com");
            if (_admin == null)
            {
                var admin = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"

                };

                var createAdmin = await UserManager.CreateAsync(admin, "Admin2019!");
                if (createAdmin.Succeeded)
                    await UserManager.AddToRoleAsync(admin, "Admin");
            }

            //var _hr = await UserManager.FindByEmailAsync("admin@hr.com");
            //if (_hr == null)
            //{
            //    var hr = new ApplicationUser
            //    {
            //        UserName = "admin@hr.com",
            //        Email = "admin@hr.com"
            //    };

            //    var createTeacher = await UserManager.CreateAsync(hr, "HRManager");
            //    if (createTeacher.Succeeded)
            //        await UserManager.AddToRoleAsync(hr, "HR");
            //}

            //var _staff = await UserManager.FindByEmailAsync("admin@staff.com");
            //if (_staff == null)
            //{
            //    var staff = new ApplicationUser
            //    {
            //        UserName = "admin@staff.com",
            //        Email = "admin@staff.com"
            //    };

            //    var createStudent = await UserManager.CreateAsync(staff, "Staff!");
            //    if (createStudent.Succeeded)
            //        await UserManager.AddToRoleAsync(staff, "Staff");
            //}

            //var _accountant = await UserManager.FindByEmailAsync("admin@accountant.com");
            //if (_accountant == null)
            //{
            //    var accountant = new ApplicationUser
            //    {
            //        UserName = "admin@accountant.com",
            //        Email = "admin@accountant.com"
            //    };

            //    var createStudent = await UserManager.CreateAsync(accountant, "Accountant!");
            //    if (createStudent.Succeeded)
            //        await UserManager.AddToRoleAsync(accountant, "Accountant");
            //}

            //var _visitor = await UserManager.FindByEmailAsync("accountant@visitor.com");
            //if (_visitor == null)
            //{
            //    var visitor = new ApplicationUser
            //    {
            //        UserName = "visitor@visitor.com",
            //        Email = "visitor@visitor.com"
            //    };

            //    var createVisitor = await UserManager.CreateAsync(visitor, "Visitor2019!");
            //    if (createVisitor.Succeeded)
            //        await UserManager.AddClaimAsync(visitor, new Claim("BadgeNumber", "visitor-badge"));
            //}

            //var _gamer = await UserManager.FindByEmailAsync("gamer@gamer.com");
            //if (_gamer == null)
            //{
            //    var gamer = new ApplicationUser
            //    {
            //        UserName = "gamer@gamer.com",
            //        Email = "gamer@gamer.com"
            //    };

            //    var createVisitor = await UserManager.CreateAsync(gamer, "Gamer2019!");
            //    if (createVisitor.Succeeded)
            //        await UserManager.AddClaimAsync(gamer, new Claim(ClaimTypes.DateOfBirth, DateTime.Now.AddYears(-20).ToString()));
            //}
        }
    }
}
