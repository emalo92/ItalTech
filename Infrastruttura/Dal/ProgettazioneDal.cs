using Infrastruttura.Data.Context;
using Infrastruttura.Models;
using Infrastruttura.Models.Input;
using Infrastruttura.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Dal
{
    public class ProgettazioneDal : IProgettazioneDal
    {
        private ItalTechDbContext context;
        string connectionString;
        
        public ProgettazioneDal (ItalTechDbContext context, string connectionString)
        {
            this.context = context;
            this.connectionString = connectionString;
        }

        public Task<bool> CheckProgettoExist(string codice)
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
                    query = query.Where( x => x.Codice == input.Codice);
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
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare progetti");
            }
            
        }

        public Task<List<Progetto>> GetAllProgetti()
        {
            throw new NotImplementedException();
        }

        public Task<List<Progetto>> GetAllProgetti(InputRichiesteProgetti input)
        {
            throw new NotImplementedException();
        }

        public async Task<Progetto> GetProgetto(string codice)
        {
            try
            {
                using var sqlConn = new SqlConnection(connectionString);
                using var command = new SqlCommand();
                command.Connection = sqlConn;
                string query = "";

                query = @" SELECT Codice, Descrizione 
                            FROM Progetto
                            WHERE Codice == @Cod
                            GROUP BY Codice";

                command.Parameters.Add(new SqlParameter("@Cod", codice));

                command.CommandText = query;

                sqlConn.Open();
                using var reader = await command.ExecuteReaderAsync();

                var progetti = new List<Progetto>();

                while (reader.Read())
                {
                    var progetto = new Progetto
                    {
                        Codice = reader.GetInt32("Codice"),
                        Descrizione = reader.GetString("Descrizione")
                    };

                    progetti.Add(progetto);

                }
                return progetti[0];
            }
            catch (Exception ex)
            {
                ex.Source = "impossibile trovare progetti con il codice selezionato";
                throw;
            }
        }

        public async Task<bool> SaveProgetto(Progetto progetto, TipoCrud tipoCrud)
        {
            switch (tipoCrud)
            {
                case TipoCrud.insert: await context.Progettos.AddAsync(progetto.ToEntity()); return await context.SaveChangesAsync()==1;
                case TipoCrud.update: context.Progettos.Update(progetto.ToEntity()); return await context.SaveChangesAsync() == 1;

            }

            throw new NotImplementedException();
        }
    }
}
