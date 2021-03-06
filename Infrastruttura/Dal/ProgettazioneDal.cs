using Infrastruttura.Data.Context;
using Infrastruttura.Models;
using Infrastruttura.Models.Input;
using Infrastruttura.Mapper;
using Infrastruttura.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;
using System.Data;

namespace Infrastruttura.Dal
{
    public class ProgettazioneDal : IProgettazioneDal
    {
        private readonly ItalTechDbContext context;
        readonly string connectionString;

        public ProgettazioneDal(ItalTechDbContext context, string connectionString)
        {
            this.context = context;
            this.connectionString = connectionString;
        }

        public Task<bool> CheckProgettoExist(string codice)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckRichiestaProgettoExist(string codice)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Progetto>> GetAllProgetti(InputRicercaProgetti input)
        {
            try
            {
                var query = context.Progettos.AsNoTracking();
                if (input.Codice != 0)
                {
                    query = query.Where(x => x.Codice == input.Codice);
                }
                if (input.NomeProgetto != null)
                {
                    query = query.Where(x => x.NomeProgetto == input.NomeProgetto);
                }
                if (input.DataInizio != null)
                {
                    query = query.Where(x => x.DataInizio == input.DataInizio);
                }
                if (input.Cliente != null)
                {
                    query = query.Where(x => x.Cliente == input.Cliente);
                }
                if (input.ProjectManager != null)
                {
                    query = query.Where(x => x.ProjectManager == input.ProjectManager);
                }
                if (input.Tipo != null)
                {
                    query = query.Where(x => x.Tipo == input.Tipo);
                }

                var progetti = await query.ToListAsync();
                return progetti.ToDto();
            }
            catch (Exception)
            {
                throw new Exception("Impossibile trovare progetti");
            }

        }

        public Task<List<Progetto>> GetAllProgetti()
        {
            InputRicercaProgetti input = new();
            return GetAllProgetti(input);
        }
        public async Task<Progetto> GetProgetto(string codice)
        {
            try
            {
                using var sqlConn = new SqlConnection(connectionString);
                using var command = new SqlCommand();
                command.Connection = sqlConn;
                string query = "";

                query = @"SELECT Codice, Descrizione, DataInizio, DataFine, NomeProgetto, CostoPrevisto, CostoFinale, Tipo, CodiceAnalisiMercato, ProjectManager, Cliente 
                            FROM Progetto 
                            WHERE Codice = @Cod";

                command.Parameters.Add(new SqlParameter("@Cod", int.Parse(codice)));

                command.CommandText = query;

                sqlConn.Open();
                using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

                var progetti = new List<Progetto>();

                var a = reader.Read();

                var progetto = new Progetto
                {
                    Codice = reader.GetSafeInt("Codice").Value,
                    Descrizione = reader.GetSafeString("Descrizione"),
                    DataInizio = reader.GetSafeDateTime("DataInizio").Value,
                    DataFine = reader.GetSafeDateTime("DataFine"),
                    NomeProgetto = reader.GetSafeString("NomeProgetto"),
                    CostoPrevisto = reader.GetSafeDecimal("CostoPrevisto"),
                    CostoFinale = reader.GetSafeDecimal("CostoFinale"),
                    Tipo = reader.GetSafeString("Tipo"),
                    CodiceAnalisiMercato = reader.GetSafeInt("CodiceAnalisiMercato"),
                    Cliente = reader.GetSafeString("Cliente"),
                    ProjectManager = reader.GetSafeString("ProjectManager")
                };
                return progetto;
            }

            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare progetti con il codice selezionato");
            }
        }
        public async Task<RichiestaProgetto> GetRichiestaProgetto(string codice)
        {
            try
            {
                using var sqlConn = new SqlConnection(connectionString);
                using var command = new SqlCommand();
                command.Connection = sqlConn;
                string query = "";

                query = @"SELECT Codice, Descrizione,CodiceProgetto,Tipo,Cliente,EsitoStudio,Budget,Operatore 
                            FROM RichiestaProgetto
                            WHERE Codice = @Cod";

                command.Parameters.Add(new SqlParameter("@Cod", int.Parse(codice)));

                command.CommandText = query;

                sqlConn.Open();
                using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

                var progetti = new List<Progetto>();

                var a = reader.Read();

                var richiestaProgetto = new RichiestaProgetto
                {
                    Codice = reader.GetSafeInt("Codice").Value,
                    Descrizione = reader.GetSafeString("Descrizione"),
                    Tipo = reader.GetSafeString("Tipo"),
                    Cliente = reader.GetSafeString("Cliente"),
                    Operatore = reader.GetSafeString("Operatore"),
                    Budget = reader.GetSafeDecimal("Budget"),
                    CodiceProgetto = reader.GetSafeInt("CodiceProgetto"),
                    EsitoStudio = reader.GetBoolean("EsitoStudio")
                };
                return richiestaProgetto;
            }

            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare Richiesta  progetto con il codice selezionato");
            }
        }
        public Task<List<RichiestaProgetto>> GetAllRichiesteProgetti()
        {
            InputRicercaRichiesteProgetti input = new();
            return GetAllRichiesteProgetti(input);
        }
        public async Task<List<RichiestaProgetto>> GetAllRichiesteProgetti(InputRicercaRichiesteProgetti input)
        {
            try
            {
                var query = context.RichiestaProgettos.AsNoTracking();
                if (input.Codice != 0)
                {
                    query = query.Where(x => x.Codice == input.Codice);
                }
                if (input.CodiceProgetto != null)
                {
                    query = query.Where(x => x.CodiceProgetto == input.CodiceProgetto);
                }
                if (input.Tipo != null)
                {
                    query = query.Where(x => x.Tipo == input.Tipo);
                }
                if (input.Descrizione != null)
                {
                    query = query.Where(x => x.Descrizione == input.Descrizione);
                }
                if (input.Budget != null)
                {
                    query = query.Where(x => x.Budget == input.Budget);
                }
                if (input.EsitoStudio != null)
                {
                    query = query.Where(x => x.EsitoStudio == input.EsitoStudio);
                }
                if (input.Cliente != null)
                {
                    query = query.Where(x => x.Cliente == input.Cliente);
                }
                if (input.Operatore != null)
                {
                    query = query.Where(x => x.Operatore == input.Operatore);
                }

                var Richiesteprogetti = await query.ToListAsync();
                return Richiesteprogetti.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare richieste progetti");
            }
        }
        public Task<List<Impiegato>> GetAllImpiegati()
        {
            InputRicercaImpiegati input = new();
            return GetAllImpiegati(input);
        }
        public async Task<List<Impiegato>> GetAllImpiegati(InputRicercaImpiegati input)
        {
            try
            {
                var query = context.Impiegatos.AsNoTracking();
                if (input.CodFiscale != null)
                {
                    query = query.Where(x => x.CodFiscale == input.CodFiscale);
                }
                if (input.UserId != null)
                {
                    query = query.Where(x => x.UserId == input.UserId);
                }
                if (input.Nome != null)
                {
                    query = query.Where(x => x.Nome == input.Nome);
                }
                if (input.Cognome != null)
                {
                    query = query.Where(x => x.Cognome == input.Cognome);
                }
                if (input.DataDiNascita != null)
                {
                    query = query.Where(x => x.DataDiNascita == input.DataDiNascita);
                }
                if (input.Citta != null)
                {
                    query = query.Where(x => x.Citta == input.Citta);
                }
                if (input.Indirizzo != null)
                {
                    query = query.Where(x => x.Indirizzo == input.Indirizzo);
                }
                if (input.Cap != null)
                {
                    query = query.Where(x => x.Cap == input.Cap);
                }
                if (input.AziendaId != null)
                {
                    query = query.Where(x => x.AziendaId == input.AziendaId);
                }

                var impiegati = await query.ToListAsync();
                return impiegati.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare impiegati");
            }
        }
        public Task<List<Fornitura>> GetAllForniture()
        {
            InputRicercaForniture input = new();
            return GetAllForniture(input);
        }
        public async Task<List<Fornitura>> GetAllForniture(InputRicercaForniture input)
        {
            try
            {
                var query = context.Fornituras.AsNoTracking();
                if (input.Codice != null)
                {
                    query = query.Where(x => x.Codice == input.Codice);
                }
                if (input.Nome != null)
                {
                    query = query.Where(x => x.Nome == input.Nome);
                }
                if (input.Descrizione != null)
                {
                    query = query.Where(x => x.Descrizione == input.Descrizione);
                }
                if (input.CostoPerPezzo != null)
                {
                    query = query.Where(x => x.CostoPerPezzo == input.CostoPerPezzo);
                }
                if (input.CostoAlKg != null)
                {
                    query = query.Where(x => x.CostoAlKg == input.CostoAlKg);
                }
                if (input.CodiceFornitore!= null)
                {
                    query = query.Where(x => x.CodiceFornitore == input.CodiceFornitore);
                }
                if (input.Quantita != 0)
                {
                    query = query.Where(x => x.Quantita == input.Quantita);
                }
                if (input.SettoreDeposito != null)
                {
                    query = query.Where(x => x.SettoreDeposito == input.SettoreDeposito);
                }
                if (input.Tipo != null)
                {
                    query = query.Where(x => x.Tipo == input.Tipo);
                }

                var forniture = await query.ToListAsync();
                return forniture.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare forniture");
            }
        }
        public Task<List<Fornitore>> GetAllFornitori()
        {
            InputRicercaFornitori input = new();
            return GetAllFornitori(input);
        }
        public async Task<List<Fornitore>> GetAllFornitori(InputRicercaFornitori input)
        {
            try
            {
                var query = context.Fornitores.AsNoTracking();

                if (input.PartitaIva != null)
                {
                    query = query.Where(x => x.PartitaIva == input.PartitaIva);
                }
                if (input.Nome != null)
                {
                    query = query.Where(x => x.Nome == input.Nome);
                }
                if (input.NumTelefono != null)
                {
                    query = query.Where(x => x.NumTelefono == input.NumTelefono);
                }
                if (input.Indirizzo != null)
                {
                    query = query.Where(x => x.Indirizzo == input.Indirizzo);
                }
                if (input.RagioneSociale != null)
                {
                    query = query.Where(x => x.RagioneSociale == input.RagioneSociale);
                }
                if (input.Email != null)
                {
                    query = query.Where(x => x.Email == input.Email);
                }
                if (input.Città != null)
                {
                    query = query.Where(x => x.Città == input.Città);
                }
                if (input.Cap != null)
                {
                    query = query.Where(x => x.Cap == input.Cap);
                }

                var fornitori = await query.ToListAsync();
                return fornitori.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare fornitori");
            }
        }
        public Task<List<ProdottoCase>> GetAllProdottiCase()
        {
            InputRicercaProdottiCase input = new();
            return GetAllProdottiCase(input);
        }

        public async Task<List<ProdottoCase>> GetAllProdottiCase(InputRicercaProdottiCase input)
        {
            try
            {
                var query = context.ProdottoCases.AsNoTracking();

                if (input.Codice != 0)
                {
                    query = query.Where(x => x.Codice == input.Codice);
                }
                if (input.Nome != null)
                {
                    query = query.Where(x => x.Nome == input.Nome);
                }
                if (input.CodiceProgetto != 0)
                {
                    query = query.Where(x => x.CodiceProgetto == input.CodiceProgetto);
                }
                if (input.Costo != null)
                {
                    query = query.Where(x => x.Costo == input.Costo
                    );
                }
                if (input.Colore != null)
                {
                    query = query.Where(x => x.Colore == input.Colore);
                }
                if (input.Lotto != null)
                {
                    query = query.Where(x => x.Lotto == input.Lotto);
                }
                if (input.Descrizione != null)
                {
                    query = query.Where(x => x.Descrizione == input.Descrizione);
                }
                if (input.PesoKg != 0)
                {
                    query = query.Where(x => x.PesoKg == input.PesoKg);
                }
                if (input.Quantita != 0)
                {
                    query = query.Where(x => x.Quantita == input.Quantita);
                }

                var prodottocase = await query.ToListAsync();
                return prodottocase.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare i case");
            }
        }

        public Task<List<ProdottoAssemblato>> GetAllProdottiAssemblati()
        {
            InputRicercaProdottiAssemblati input = new();
            return GetAllProdottiAssemblati(input);
        }
        public async Task<List<ProdottoAssemblato>> GetAllProdottiAssemblati(InputRicercaProdottiAssemblati input)
        {
            try
            {
                var query = context.ProdottoAssemblatos.AsNoTracking();

                if (input.Codice != 0)
                {
                    query = query.Where(x => x.Codice == input.Codice);
                }
                if (input.Nome != null)
                {
                    query = query.Where(x => x.Nome == input.Nome);
                }
                if (input.CodiceProgetto != 0)
                {
                    query = query.Where(x => x.CodiceProgetto == input.CodiceProgetto);
                }
                if (input.Costo != null)
                {
                    query = query.Where(x => x.Costo == input.Costo
                    );
                }
                if (input.FasciaDiMercato != null)
                {
                    query = query.Where(x => x.FasciaDiMercato == input.FasciaDiMercato);
                }
                if (input.Lotto != null)
                {
                    query = query.Where(x => x.Lotto == input.Lotto);
                }
                if (input.Descrizione != null)
                {
                    query = query.Where(x => x.Descrizione == input.Descrizione);
                }
                if (input.Peso != 0)
                {
                    query = query.Where(x => x.Peso == input.Peso);
                }
                if (input.Quantita != 0)
                {
                    query = query.Where(x => x.Quantita == input.Quantita);
                }

                var prodottoassemblato = await query.ToListAsync();
                return prodottoassemblato.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare gli assemblati");
            }
        }

        public Task<List<Componente>> GetAllComponenti(int codice = 0)
        {
            InputRicercaComponenti input = new();
            if (codice != 0)
            {
                input.CodiceProgetto = codice;
            }
            return GetAllComponenti(input);
        }
        public async Task<List<Componente>> GetAllComponenti(InputRicercaComponenti input)
        {
            try
            {
                var query = (from comp in context.Componentes
                             join forn in context.Fornituras
                             on comp.CodiceFornitura equals forn.Codice into temp
                             from result in temp
                             select new Componente()
                             {
                                 CodiceFornitura = comp.CodiceFornitura,
                                 CodiceProgetto = comp.CodiceProgetto,
                                 NumPezzi = comp.NumPezzi,
                                 Nome = result.Nome,
                                 Descrizione = result.Descrizione
                             }).AsQueryable();
                
                if (input.CodiceProgetto != 0)
                {
                    query = query.Where(x => x.CodiceProgetto == input.CodiceProgetto);
                }
                if (input.CodiceFornitura != null)
                {
                    query = query.Where(x => x.CodiceFornitura == input.CodiceFornitura);
                }
                if (input.NumPezzi != 0)
                {
                    query = query.Where(x => x.NumPezzi == input.NumPezzi);
                }

                var componente = await query.ToListAsync();
                return componente;
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare componenti");
            }
        }
        public Task<List<Ordini>> GetAllOrdini()
        {
            InputRicercaOrdini input = new();
            return GetAllOrdini(input);
        }
        public async Task<List<Ordini>> GetAllOrdini(InputRicercaOrdini input)
        {
            try
            {
                var query = context.Ordinis.AsNoTracking();

                if (input.Codice != 0)
                {
                    query = query.Where(x => x.Codice == input.Codice);
                }
                if (input.DataCreazione != null)
                {
                    query = query.Where(x => x.DataCreazione == input.DataCreazione);
                }
                if (input.DataInvio != null)
                {
                    query = query.Where(x => x.DataInvio == input.DataInvio);
                }
                if (input.Operatore != null)
                {
                    query = query.Where(x => x.Operatore == input.Operatore);
                }

                var ordine = await query.ToListAsync();
                return ordine.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare ordini");
            }
        }
        public Task<List<OrdineForniture>> GetAllOrdiniForniture()
        {
            InputRicercaOrdiniForniture input = new();
            return GetAllOrdiniForniture(input);
        }

        public async Task<List<OrdineForniture>> GetAllOrdiniForniture(InputRicercaOrdiniForniture input)
        {
            try
            {
                var query = context.OrdineFornitures.AsNoTracking();

                if (input.CodiceOrdine != 0)
                {
                    query = query.Where(x => x.CodiceOrdine == input.CodiceOrdine);
                }
                if (input.CodiceFornitura != null)
                {
                    query = query.Where(x => x.CodiceFornitura == input.CodiceFornitura);
                }
                if (input.Quantità != 0)
                {
                    query = query.Where(x => x.Quantità == input.Quantità);
                }

                var ordineforniture = await query.ToListAsync();
                return ordineforniture.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare gli ordini di forniture");
            }
        }
        public Task<List<Prototipo>> GetAllPrototipi()
        {
            InputRicercaPrototipi input = new();
            return GetAllPrototipi(input);
        }
        public async Task<List<Prototipo>> GetAllPrototipi(InputRicercaPrototipi input)
        {
            try
            {
                var query = context.Prototipos.AsNoTracking();

                if (input.Numero != 0)
                {
                    query = query.Where(x => x.Numero == input.Numero);
                }
                if (input.CodiceProgetto != 0)
                {
                    query = query.Where(x => x.CodiceProgetto == input.CodiceProgetto);
                }
                if (input.Data != null)
                {
                    query = query.Where(x => x.Data == input.Data);
                }
                if (input.Descrizione != null)
                {
                    query = query.Where(x => x.Descrizione == input.Descrizione);
                }
                if (input.Modifiche != null)
                {
                    query = query.Where(x => x.Modifiche == input.Modifiche);
                }
                if (input.MotivoFallimento != null)
                {
                    query = query.Where(x => x.MotivoFallimento == input.MotivoFallimento);
                }
                if (input.RisultatoTest != null)
                {
                    query = query.Where(x => x.RisultatoTest == input.RisultatoTest);
                }

                var prototipo = await query.ToListAsync();
                return prototipo.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare prototipi");
            }
        }
        public Task<List<Cliente>> GetAllClienti()
        {
            InputRicercaClienti input = new();
            return GetAllClienti(input);
        }
        public async Task<List<Cliente>> GetAllClienti(InputRicercaClienti input)
        {
            try
            {
                var query = context.Clientes.AsNoTracking();

                if (input.CodFiscale != null)
                {
                    query = query.Where(x => x.CodFiscale == input.CodFiscale);
                }
                if (input.Nome != null)
                {
                    query = query.Where(x => x.Nome == input.Nome);
                }
                if (input.Cognome != null)
                {
                    query = query.Where(x => x.Cognome == input.Cognome);
                }
                if (input.Indirizzo != null)
                {
                    query = query.Where(x => x.Indirizzo == input.Indirizzo);
                }
                if (input.DataDiNascita != null)
                {
                    query = query.Where(x => x.DataDiNascita == input.DataDiNascita);
                }
                if (input.Citta != null)
                {
                    query = query.Where(x => x.Citta == input.Citta);
                }
                if (input.Cap != null)
                {
                    query = query.Where(x => x.Cap == input.Cap);
                }

                var cliente = await query.ToListAsync();
                return cliente.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare clienti");
            }
        }

        public async Task<bool> SaveProgetto(Progetto progetto, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Progettos.AddAsync(progetto.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Progettos.Update(progetto.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.Progettos.Remove(progetto.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveFornitore(Fornitore fornitore, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Fornitores.AddAsync(fornitore.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Fornitores.Update(fornitore.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.Fornitores.Remove(fornitore.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveFornitura(Fornitura fornitura, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Fornituras.AddAsync(fornitura.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Fornituras.Update(fornitura.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.Fornituras.Remove(fornitura.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveCliente(Cliente cliente, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Clientes.AddAsync(cliente.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Clientes.Update(cliente.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.Clientes.Remove(cliente.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveComponente(Componente componente, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Componentes.AddAsync(componente.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Componentes.Update(componente.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.Componentes.Remove(componente.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveImpiegato(Impiegato impiegato, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Impiegatos.AddAsync(impiegato.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Impiegatos.Update(impiegato.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");

            }
        }
        public async Task<bool> SaveOrdini(Ordini ordini, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Ordinis.AddAsync(ordini.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Ordinis.Update(ordini.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveOrdineForniture(int codiceOrdine, string codiceForniture, int quantita, TipoCrud tipoCrud)
        {
            try
            {
                var ord = new OrdineForniture
                {
                    CodiceOrdine = codiceOrdine,
                    CodiceFornitura = codiceForniture,
                    Quantità = quantita
                };
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.OrdineFornitures.AddAsync(ord.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.OrdineFornitures.Update(ord.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.OrdineFornitures.Remove(ord.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveProdottoAssemblato(ProdottoAssemblato prodottoAssemblato, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.ProdottoAssemblatos.AddAsync(prodottoAssemblato.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.ProdottoAssemblatos.Update(prodottoAssemblato.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.ProdottoAssemblatos.Remove(prodottoAssemblato.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveProdottoCase(ProdottoCase prodottocase, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.ProdottoCases.AddAsync(prodottocase.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.ProdottoCases.Update(prodottocase.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.ProdottoCases.Remove(prodottocase.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SavePrototipo(Prototipo prototipo, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Prototipos.AddAsync(prototipo.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Prototipos.Update(prototipo.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }
        public async Task<bool> SaveRichiestaProgetto(RichiestaProgetto richiestaProgetto, TipoCrud tipoCrud)
        {
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.RichiestaProgettos.AddAsync(richiestaProgetto.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.RichiestaProgettos.Update(richiestaProgetto.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.RichiestaProgettos.Remove(richiestaProgetto.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }

        public async Task<bool> SaveComponenti(List<Componente> dettaglio, TipoCrud tipoCrud)
        {
            try
            {
                var count = dettaglio.Count;
                foreach (var componente in dettaglio)
                {
                    
                    if (componente.CodiceFornitura == null)
                    {
                        count = count - 1;
                        break;
                    }
                    if (tipoCrud == TipoCrud.update)
                    {
                        var presente = await context.Componentes.AsNoTracking().Where(x => x.CodiceProgetto == componente.CodiceProgetto && x.CodiceFornitura == componente.CodiceFornitura).FirstOrDefaultAsync();
                        if (presente == null)
                        {
                            tipoCrud = TipoCrud.insert;
                        }
                    }
                    switch (tipoCrud)
                    {
                        case TipoCrud.insert: await context.Componentes.AddAsync(componente.ToEntity()); break;
                        case TipoCrud.update: context.Componentes.Update(componente.ToEntity()); break;
                        case TipoCrud.delete: context.Componentes.Remove(componente.ToEntity()); break;

                    }
                }
                return await context.SaveChangesAsync() == count;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }
        }
    }
}
