using System;
using Csi.WebApp.Areas.Identity.Data;
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
                services.AddDbContext<IdentityDataContext>(options =>
                    // options.UseSqlServer(
                    //     context.Configuration.GetConnectionString("IdentityDataContextConnection"))
                    options.UseMySQL("name=CsiDatabase")
                    );

                
                // AddDefaultIdentity implies csi.AspNetUsers table
                // services.AddDefaultIdentity<CsiUser>()
                //     .AddEntityFrameworkStores<IdentityDataContext>();

                services.AddIdentity<CsiUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDataContext>()
                    .AddDefaultTokenProviders();
            });
        }
    }
}