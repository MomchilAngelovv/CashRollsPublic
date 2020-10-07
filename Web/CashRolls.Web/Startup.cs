namespace CashRolls.Web
{
    using Stripe;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using CashRolls.Data;
    using CashRolls.Services;
    using CashRolls.Web.Filters;
    using CashRolls.Data.Models;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(
            IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Database
            services.AddDbContext<CashRollDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Identity
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<CashRollDbContext>()
            .AddDefaultTokenProviders();

            //Login and Access options
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Users/Login";
                options.AccessDeniedPath = "/Home/AccessDenied";
            });

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //MVC
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                options.Filters.Add<RegisterErrorExceptionFilter>();
            });

            //Stripe
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

            //Services
            services.AddTransient<IErrorsService, ErrorsService>();
            services.AddTransient<IRollsService, RollsService>();
            services.AddTransient<ICurrenciesService, CurrenciesService>();
            services.AddTransient<IUsersService, UsersService>();
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

                endpoints.MapControllerRoute(
                   name: "participants",
                   pattern: "Rolls/{id}/Participants",
                   defaults: new { Controller = "Rolls", Action = "Participants" });
            });
        }
    }
}
