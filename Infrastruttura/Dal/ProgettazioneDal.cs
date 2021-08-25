using Infrastruttura.Data.Context;
using Infrastruttura.Models;
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

        public Task<List<Progetto>> GetAllProgetti()
        {
            //try
            //{
            //    var progetti = await context.Progetto.Select(s => new Progetto
            //    {
            //        Codice = s.Codice,
            //        Descrizione = s.Descrizione
            //    }).ToListAsync();
            //    return progetti;
            //}
            //catch (Exception ex)
            //{
            //    ex.Source = "CheckProgettoExist";
            //    throw;
            //}
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

        public Task<bool> SaveProgetto(Progetto progetto, TipoCrud tipoCrud)
        {
            switch (tipoCrud)
            {
                case TipoCrud.insert: break;

            }

            throw new NotImplementedException();
        }
    }
}
