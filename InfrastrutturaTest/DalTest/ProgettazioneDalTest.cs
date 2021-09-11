using Infrastruttura;
using Infrastruttura.Dal;
using Infrastruttura.Data.Context;
using Infrastruttura.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace InfrastrutturaTest
{
    public class ProgettazioneDalTest
    {
        private IProgettazioneDal _sut;
        private static string connectionString = "data source=.; initial catalog = ItalTech; persist security info=True; Integrated Security = SSPI;";
        private IHost _host = null;
        private ItalTechDbContext context;

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder(null)
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<ItalTechDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    services.AddScoped<IMemoryCache, MemoryCache>();

                    services.AddScoped<IProgettazioneDal>(s =>
                        new ProgettazioneDal(s.GetRequiredService<ItalTechDbContext>(), connectionString));

                });

        [OneTimeSetUp]
        public void Init()
        {
            _host = CreateHostBuilder().Build();
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
            context = _host.Services.CreateScope().ServiceProvider.GetRequiredService<ItalTechDbContext>();
            _sut = _host.Services.GetService<IProgettazioneDal>();
        }
        //data mese giorno anno
        [TestCase("Lngmnl92s12d086n", "Emanuele", "Longo", "11/12/1992", "Cosenza", "viale parco", "87100")]
        //[TestCase("Lngmnl92s12d086n", "Emanuele", "Longo", "11/12/1992", "Cosenza", "viale parco", "87100")]//mettete i vostri dati
        //[TestCase("Lngmnl92s12d086n", "Emanuele", "Longo", "11/12/1992", "Cosenza", "viale parco", "87100")]
        public async Task ClientePopulationAsync(string codiceFiscale, string nome, string cognome, DateTime dataDiNascita, string citta, string indirizzo, string cap)
        {
            var cliente = new Cliente
            {
                CodFiscale = codiceFiscale.ToUpper(),
                Nome = nome,
                Cognome = cognome,
                DataDiNascita = dataDiNascita,
                Citta = citta,
                Indirizzo = indirizzo,
                Cap = cap
            };
            var a = cliente;
            var result = await _sut.SaveCliente(cliente, TipoCrud.insert);
            Assert.IsTrue(result);
        }

        [TestCase("Lngmnl92s12d086n", "Emanuele", "Longo", "11/12/1992", "Cosenza", "viale parco", "87100", "0000000001")]
        //[TestCase("Lngmnl92s12d086n", "Emanuele", "Longo", "11/12/1992", "Cosenza", "viale parco", "87100", "0000000002")]//mettete i vostri dati
        //[TestCase("Lngmnl92s12d086n", "Emanuele", "Longo", "11/12/1992", "Cosenza", "viale parco", "87100", "0000000003")]
        public async Task ImpiegatoPopulationAsync(string codiceFiscale, string nome, string cognome, DateTime dataDiNascita, string citta, string indirizzo, string cap, string aziendaId)
        {
            var impiegato = new Impiegato
            {
                CodFiscale = codiceFiscale.ToUpper(),
                Nome = nome,
                Cognome = cognome,
                DataDiNascita = dataDiNascita,
                Citta = citta,
                Indirizzo = indirizzo,
                Cap = cap,
                AziendaId = aziendaId
            };
            var result = await _sut.SaveImpiegato(impiegato, TipoCrud.insert);
            Assert.IsTrue(result);
        }

        [TestCase("02749384123", "Intel", "Intel Corporation", "Cosenza", "viale parco", "87100", "0984123456", "intelcosenza@intel.com")]
        [TestCase("02749384124", "Nvidia", "NvidiaCorporation", "Cosenza", "viale cosmai", "87100", "0984123457", "nvidiacosenza@nvidia.com")]
        [TestCase("02749384125", "Pvc", "Pvc&co", "Cosenza", "viale crati", "87100", "0984123458", "pvccosenza@gmail.com")]
        public async Task FornitorePopulationAsync(string partitaIva, string nome, string ragioneSociale, string citta, string indirizzo, string cap, string numTelefono, string email)
        {
            var fornitore = new Fornitore
            {
                PartitaIva = partitaIva,
                Nome = nome,
                RagioneSociale = ragioneSociale,
                Città = citta,
                Indirizzo = indirizzo,
                Cap = cap,
                NumTelefono = numTelefono,
                Email = email
            };
            var result = await _sut.SaveFornitore(fornitore, TipoCrud.insert);
            Assert.IsTrue(result);
        }
    }
}