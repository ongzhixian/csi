using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Csi.WebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // ZX:  Disabled because of the way MY Ubuntu server is setup.
            //      Cloud -> HAProxy -> Nginx -> Kestrel
            //      I want the site to handle non-HTTPS traffic as well for testing purposes.
            //      (for cases where 3rd party API don't understand HTTPS)
            // app.UseHttpsRedirection();

            app.UseStaticFiles();

            // ZX:  Initially was thinking of using one of the following options to handle
            //      Let's Encrypt acme-challenge folders, but it didn't work out.
            //      The problem is that the acme-challenge file will be treated as a
            //      unknown file type by Kestrel and that's maybe a security concern.
            //      See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-2.1
            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     FileProvider = new PhysicalFileProvider(
            //         Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ".well-known", "acme-challenge")),
            //     RequestPath = "/.well-known/acme-challenge"
            // });
            // app.UseFileServer(new FileServerOptions
            // {
            //     FileProvider = new PhysicalFileProvider(
            //         Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ".well-known", "acme-challenge")),
            //     RequestPath = "/.well-known/acme-challenge",
            //     EnableDirectoryBrowsing = true
            // });

            app.UseCookiePolicy();

            app.UseAuthentication();

            // Setup ad-hoc routing that by-passes MVC processing 
            // app.UseRouter should be defined before app.UseMvc()
            app.UseRouter(r =>
            {
                // Handle Lets Encrypt acme-challenge
                r.MapGet(".well-known/acme-challenge/{id}", async (request, response, routeData) =>
                {
                    var id = routeData.Values["id"] as string;
                    var file = Path.Combine(env.WebRootPath, ".well-known","acme-challenge", id);
                    await response.SendFileAsync(file);
                });
            });
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
