using System;
//using Csi.WebApp.Areas.Identity.Data;
using Csi.WebApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Csi.WebApp.Areas.Identity.IdentityHostingStartup))]
namespace Csi.WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                // ZX:  For some unknown reason, adding DbContext here will break the
                //      dotnet aspnet-codegenerator tool
                //      Will receive the following error message:
                //      More than one DbContext named 'Csi.WebApp.Data.CsiDbContext' was found. 
                //      Specify which one to use by providing its fully qualified name using its exact case.
                //      So opting to move this code Startup.cs instead
                // services.AddDbContext<CsiDbContext>(options =>
                //     options.UseMySQL("name=CsiDatabase")
                // );

                services.AddDefaultIdentity<CsiUser>()
                    .AddEntityFrameworkStores<CsiDbContext>();
            });
        }
    }
}