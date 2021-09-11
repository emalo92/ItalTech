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
        private ITestingDal _sutTest;
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

                    services.AddScoped<ITestingDal>(s =>
                       new TestingDal(s.GetRequiredService<ItalTechDbContext>(), connectionString));

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
            _sutTest = _host.Services.GetService<ITestingDal>();
        }
        
        [TestCase("Lngmnl92s12d086n", "Emanuele", "Longo", "11/12/1992", "Cosenza", "viale parco", "87100")]
        [TestCase("Cnsncl91r18g317k", "Nicola", "Cunsolo", "10/18/1991", "Cosenza", "Via Giulio Cesare", "87036")]
        [TestCase("mlnlgu91p05c002l", "Luigi", "Maiolino", "09/05/1991", "Cassano all'Ionio", "Via Pietro Mancini", "87040")]
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
        [TestCase("Cnsncl91r18g317k", "Nicola", "Cunsolo", "10/18/1991", "Cosenza", "Via Giulio Cesare", "87036", "0000000002")]
        [TestCase("mlnlgu91p05c002l", "Luigi", "Maiolino", "09/05/1991", "Cassano all'Ionio", "Via Pietro Mancini", "87040", "0000000003")]
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

        //riempire campi
        public async Task ForniturePopulationAsync(string codice, string nome,string descrizione, decimal costoAlKg, decimal costoPerPezzo, string PartitaIva, int quantita, string settoredeposito, string tipo)
        {
            var fornitura = new Fornitura
            {
                   Codice = codice,
                   Nome = nome,
                   Descrizione = descrizione,
                   CostoAlKg = costoAlKg,
                   CostoPerPezzo = costoPerPezzo,
                   PartitaIva = PartitaIva,
                   Quantita = quantita,
                   SettoreDeposito = settoredeposito,
                   Tipo = tipo
            };
            var result = await _sut.SaveFornitura(fornitura, TipoCrud.insert);
            Assert.IsTrue(result);
        }
        //riempire campi
        public async Task TestPopulationAsync(int codice, string descrizione, string valorediRiferimento, int quantitaEseguiti, int quantitaPassati, int quantitaFalliti, string operatore, string tipo)
        {
            var test = new Test
            {
                Codice = codice,
                Descrizione = descrizione,
                ValoriDiRiferimento = valorediRiferimento,
                QuantitaEseguiti = quantitaEseguiti,
                QuantitaFalliti = quantitaFalliti,
                QuantitaPassati = quantitaPassati,
                Operatore = operatore,
                Tipo = tipo
            };
            var result = await _sutTest.SaveTest(test, TipoCrud.insert);
            Assert.IsTrue(result);
        }
    }
}