using Infrastruttura;
using Infrastruttura.Dal;
using Infrastruttura.Data.Context;
using ItalTech.Areas.Identity.Data;
using ItalTech.Areas.Identity.Data.DAL;
using ItalTech.ExtensionMethods;
using ItalTech.Manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech
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
            //services.AddDbContext<ItalTechContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("ItalTechContext")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ItalTechContext>();
            services.AddControllersWithViews();

            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif



            string connectionString = Configuration.GetConnectionString("ItalTechContext");
            var dalStrartup = new Infrastruttura.Startup(Configuration);
            dalStrartup.ConfigureServices(services, connectionString);

            services.AddDbContext<ItalTechAppContext>(options => options.UseSqlServer(connectionString));

            Config config = services.ConfigureStartupConfig<Config>(Configuration.GetSection("AppSettings"));

            services.AddScoped<IProgettazioneDal>(s =>
                new ProgettazioneDal(s.GetRequiredService<ItalTechDbContext>(), connectionString));
            services.AddScoped<ITestingDal>(s =>
                new TestingDal(s.GetRequiredService<ItalTechDbContext>(), connectionString));
            services.AddScoped<IIdentityDal>(s =>
                new IdentityDal(s.GetRequiredService<ItalTechAppContext>(), s.GetRequiredService<ItalTechContext>()));
            services.AddScoped<IAdminManager>(s =>
            new AdminManager(config, s.GetRequiredService<UserManager<ItalTechUser>>(),
                             s.GetRequiredService<RoleManager<ItalTechRole>>(), s.GetRequiredService<IIdentityDal>()));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }

        
        
    }
}
