using System;
using ItalTech.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ItalTech.Areas.Identity.IdentityHostingStartup))]
namespace ItalTech.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ItalTechContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ItalTechContext")));

                services.AddIdentity<ItalTechUser, ItalTechRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ItalTechContext>();
            });
        }
    }
}