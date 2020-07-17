using CRMBusiness.Middleware.Authorization;
using CRMBusiness.Middleware.Authorization.Interface;
using CRMData.Configurations.GlobalConfigurationVariables;
using CRMData.Configurations.Middlewares.Authorizations;
using CRMData.Configurations.Attributes.Filters;
using CRMData.Dao;
using CRMData.Dao.Account.ProfileClaimConfiguration;
using CRMData.Dao.Account.ProfileClaimConfiguration.Implementation;
using CRMData.Data;
using CRMData.Models.Identity;
using CRMData.Services;
using CRMData.Services.Account.ProfileClaimConfiguration;
using CRMData.Services.Account.ProfileClaimConfiguration.Implementation;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
using CRMData.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
//using CRMData.ViewModels.BaseViewModel;
//using CRMData.Configurations.Extensions;
using CRMData.Configurations.Extensions;
using Microsoft.EntityFrameworkCore.Internal;
using CompData.Dao.Regulation;
using CompData.Models.Library;
using CompData.Dao.Regulation.Impl;
using CompData.Services.Regulation;
using CompData.Services.Regulation.Impl;
using CompWeb.Configurations.Email;
using CompWeb.Configurations.Email.Impl;
using CRMData.Services.SystemAudit;
using CRMData.Services.SystemAudit.Impl;
using CRMData.Dao.SystemAudit;
using CRMData.Dao.SystemAudit.Impl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CRMWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(true);
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole() ));
            });

            services.Configure<IdentityDefaultOptions>(Configuration.GetSection("IdentityDefaultOptions"));
            var identityDefaultOptions = Configuration.Get<IdentityDefaultOptions>();

            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    //Password Characteristics
                    options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
                    options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
                    options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
                    options.Password.RequireUppercase = identityDefaultOptions.MyPropertPasswordRequireUppercase;
                    options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
                    options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

                    //LockOut Characteristics
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
                    options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
                    options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;

                    options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;
                    options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;
                }
            ).AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // cookie settings
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                //options.Cookie.HttpOnly = identityDefaultOptions.CookieHttpOnly;
                options.LoginPath = identityDefaultOptions.LoginPath; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = identityDefaultOptions.LogoutPath; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = identityDefaultOptions.AccessDeniedPath; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = identityDefaultOptions.SlidingExpiration;
            });

            services.AddSession(configure =>
            {
                configure.Cookie.IsEssential = identityDefaultOptions.SessionCookieIsEssential;
            });


            services.AddHttpContextAccessor();

            services.AddScoped<IMiddlewareAuthorization, MiddlewareAuthorization>();

            #region Configuration Services
            services.AddTransient<IProfileClaimConfiguration, ProfileClaimConfiguration>();
            services.AddTransient<IProfileClaimConfigurationDao, ProfileClaimConfigurationDao>();
            #endregion

            #region Library
            services.AddTransient<IRegulationService, RegulationService>();
            services.AddTransient<IRegulationDao, RegulationDao>();
            #endregion

            #region Utility
            services.AddTransient<Utility>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISystemAuditService, SystemAuditService>();
            services.AddTransient<ISystemAuditDao, SystemAuditDao>();
            #endregion

            services.AddControllersWithViews(config =>
            {
                //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                //config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(typeof(ActionFilter));
            });
            //  .AddXmlSerializerFormatters()
            //  .AddRazorRuntimeCompilation();

            services.AddMvc()
                .AddXmlSerializerFormatters()
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseCors();
            //app.UseUserRights();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "areas",
            //      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //
            //    routes.MapRoute(
            //      name: "default",
            //      template: "{controller=Home}/{action=Index}/{id?}"
            //    );
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
