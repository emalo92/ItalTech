using Infrastruttura;
using Infrastruttura.Dal;
using Infrastruttura.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace InfrastrutturaTest
{
    public class ProgettazioneDalTest
    {
        private IProgettazioneDal _sut;
        private static string connectionString = "data source=.; initial catalog = ItalTech; persist security info=True; Integrated Security = SSPI;";
        private IHost _host = null;
        private ItalTechContext context;

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder(null)
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<ItalTechContext>(options =>
                        options.UseSqlServer(connectionString));

                    services.AddScoped<IMemoryCache, MemoryCache>();

                    services.AddScoped<IProgettazioneDal>(s =>
                        new ProgettazioneDal(s.GetRequiredService<ItalTechContext>()));
                                          
                });

        [OneTimeSetUp]
        public void Init()
        {
        
        }

        [OneTimeTearDown]
        public void End()
        {
            if (_host != null)
            {
                context.Dispose();
                _host.Dispose();
            }
        }

        [SetUp]
        public void Setup()
        {
            context = _host.Services.CreateScope().ServiceProvider.GetRequiredService<ItalTechContext>();
            _sut = _host.Services.GetService<IProgettazioneDal>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}