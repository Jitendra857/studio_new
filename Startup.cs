using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PIOAccount.Classes;
using Rotativa.AspNetCore;
using Soc_Management_Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUNMark
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
            services.AddControllersWithViews();

            services.AddMvcCore()
        .AddDataAnnotations()
        .AddCors();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //MailHelper.FromEmail = Configuration["SMTP:FromEmail"];
            //MailHelper.Password = Configuration["SMTP:Password"];
            //MailHelper.Port = Convert.ToInt32(Configuration["SMTP:Port"]);
            //MailHelper.Host = Configuration["SMTP:Host"];

            WhatAppHelper.skrumessage = Configuration["WHATAPP:skrumessage"];
            WhatAppHelper.SURL = Configuration["WHATAPP:SURL"];
            services.AddHostedService<MyBackgroundService>();
            services.AddHostedService<MyBackgroundMailservice>();
            //WhatAppHelper.SInstanceID =Configuration["WHATAPP:SInstanceID"];
            //WhatAppHelper.SToken = Configuration["WHATAPP:SToken"];

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
            RotativaConfiguration.Setup(env.EnvironmentName, "..\\Rotativa\\");
        }
    }
}
