using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsPaint.DataAccess;
using LetsPaint.Filters;
using LetsPaint.ModelAccess.Models;
using LetsPaint.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LetsPaint
{
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

            var emailConfig = Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
  services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddMvcOptions(options =>
            {
                //options.Filters.Add(new LetsPaintAuth());
            });
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddAuthentication().AddGoogle(option=> {
                option.ClientId = "657260078627-nbdgeg3r14mhc3s1m770qalb7pqo2i3m.apps.googleusercontent.com";
                option.ClientSecret = "W8mZEhJDg1_UYj3RLejObsVs";
            });
            services.AddIdentity<IdentityUser, IdentityRole>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areas",template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "default",template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
