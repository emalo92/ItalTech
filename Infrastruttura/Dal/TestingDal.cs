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
    public class TestingDal : ITestingDal
    {
        private ItalTechDbContext context;
        string connectionString;

        public TestingDal(ItalTechDbContext context, string connectionString)
        {
            this.context = context;
            this.connectionString = connectionString;
        }

        public  Task<List<Test>> GetAllTest()
        {
            InputRicercaTest input = new();
            return GetAllTest(input);
        }

        public async Task<List<Test>> GetAllTest(InputRicercaTest input)
        {
            try
            {
                var query = context.Tests.AsNoTracking();
                if (input.Codice != 0)
                {
                    query = query.Where(x => x.Codice == input.Codice);
                }
                if (input.Tipo != null)
                {
                    query = query.Where(x => x.Tipo == input.Tipo);
                }
                if (input.Descrizione != null)
                {
                    query = query.Where(x => x.Descrizione == input.Descrizione);
                }
                if (input.ValoriDiRiferimento != null)
                {
                    query = query.Where(x => x.ValoriDiRiferimento == input.ValoriDiRiferimento);
                }
                if (input.QuantitaEseguiti != null)
                {
                    query = query.Where(x => x.QuantitaEseguiti == input.QuantitaEseguiti);
                }
                if (input.QuantitaFalliti != null)
                {
                    query = query.Where(x => x.QuantitaFalliti == input.QuantitaFalliti);
                }
                if (input.QuantitaPassati != null)
                {
                    query = query.Where(x => x.QuantitaPassati == input.QuantitaPassati);
                }
                if (input.QuantitaPassati != null)
                {
                    query = query.Where(x => x.Operatore == input.Operatore);
                }

                var tests = await query.ToListAsync();
                return tests.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare progetti");
            }

        }

        public  Task<List<TestProdottoAssemblato>> GetAllTestProdottiAssemblati() 
        {
            InputRicercaTestProdottiAssemblati input = new();
            return GetAllTestProdottiAssemblati(input);
        }

        public async Task<List<TestProdottoAssemblato>> GetAllTestProdottiAssemblati(InputRicercaTestProdottiAssemblati input)
        {
            try
            {
                var query = context.TestProdottoAssemblatos.AsNoTracking();
                if (input.CodiceTest != 0)
                {
                    query = query.Where(x => x.CodiceTest == input.CodiceTest);
                }
                if (input.CodiceProdottoAssemblato != 0)
                {
                    query = query.Where(x => x.CodiceProdottoAssemblato== input.CodiceProdottoAssemblato);
                }

                var test= await query.ToListAsync();
                return test.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare progetti Assemblati");
            }

            
            
        }
        public Task<List<TestProdottoCase>> GetAllTestProdottiCase()
        {
            InputRicercaTestProdottiCase input = new();
            return GetAllTestProdottiCase(input);
        }
        public async Task<List<TestProdottoCase>> GetAllTestProdottiCase(InputRicercaTestProdottiCase input)
        {
            try
            {
                var query = context.TestProdottoCases.AsNoTracking();
                if (input.CodiceProdottoCase != 0)
                {
                    query = query.Where(x => x.CodiceProdottoCase == input.CodiceProdottoCase);
                }
                if (input.CodiceTest!= 0)
                {
                    query = query.Where(x => x.CodiceTest== input.CodiceTest);
                }

                var test = await query.ToListAsync();
                return test.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare i Case");
            }

        }
        public async Task<bool> SaveTest(Test test, TipoCrud tipoCrud)
        {
            try
            {
                if (tipoCrud == TipoCrud.insert)
                {
                    var codiceTest = context.Tests.Max(x => x.Codice);
                    test.Codice = codiceTest + 1;
                }
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.Tests.AddAsync(test.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.Tests.Update(test.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.Tests.Remove(test.ToEntity()); return await context.SaveChangesAsync() == 1;
                }
                throw new Exception(); 
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }

        

        
        public async Task<bool> SaveTestProdottoAssemblato(int codiceTest, int codiceProdottoAssemblato, TipoCrud tipoCrud)
        {
            try
            {
                var testProdAss = new TestProdottoAssemblato
                {
                    CodiceTest = codiceTest,
                    CodiceProdottoAssemblato = codiceProdottoAssemblato
                };
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.TestProdottoAssemblatos.AddAsync(testProdAss.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.TestProdottoAssemblatos.Update(testProdAss.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.TestProdottoAssemblatos.Remove(testProdAss.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }

        }

        public async Task<bool> SaveTestProdottoCase(int codiceTest, int codiceProdottoCase, TipoCrud tipoCrud)
        {
            try
            {
                var testProdCase = new TestProdottoCase
                {
                    CodiceTest = codiceTest,
                    CodiceProdottoCase = codiceProdottoCase
                     
                };
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.TestProdottoCases.AddAsync(testProdCase.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.TestProdottoCases.Update(testProdCase.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.TestProdottoCases.Remove(testProdCase.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }
        }

        public async Task<bool> SaveTestPrototipo(int codiceTest, int codiceProgetto, int numPrototipo, TipoCrud tipoCrud)
        {
            try
            {
                var testProtot = new TestPrototipo
                {
                    CodiceTest = codiceTest,
                    CodiceProgetto = codiceProgetto,
                    NumPrototipo = numPrototipo
                };
                switch (tipoCrud)
                {
                    case TipoCrud.insert: await context.TestPrototipos.AddAsync(testProtot.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.update: context.TestPrototipos.Update(testProtot.ToEntity()); return await context.SaveChangesAsync() == 1;
                    case TipoCrud.delete: context.TestPrototipos.Remove(testProtot.ToEntity()); return await context.SaveChangesAsync() == 1;

                }
                throw new Exception(); 

            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il salvataggio in DB");
            }
        }

        public async Task<Test> GetTest(string codice)
        {
            try
            {
                using var sqlConn = new SqlConnection(connectionString);
                using var command = new SqlCommand();
                command.Connection = sqlConn;
                string query = "";

                query = @"SELECT Codice, Tipo, Descrizione, ValoriDiRiferimento, QuantitaEseguiti, QuantitaPassati, QuantitaFalliti, Operatore 
                            FROM test 
                            WHERE Codice = @Cod";

                command.Parameters.Add(new SqlParameter("@Cod", int.Parse(codice)));

                command.CommandText = query;

                sqlConn.Open();
                using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

                var tests = new List<Test>();

                var a = reader.Read();

                var test = new Test
                {
                    Codice = reader.GetSafeInt("Codice").Value,
                    Tipo = reader.GetSafeString("Tipo"),
                    Descrizione = reader.GetSafeString("Descrizione"),
                    ValoriDiRiferimento = reader.GetSafeString("ValoriDiRiferimento"),
                    QuantitaPassati= reader.GetSafeInt("QuantitaPassati"),
                    QuantitaEseguiti = reader.GetSafeInt("QuantitaEseguiti"),
                    QuantitaFalliti = reader.GetSafeInt("QuantitaFalliti"),
                    Operatore = reader.GetSafeString("Operatore"),
                };
                return test;
            }

            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare Test con il codice selezionato");
            }
        }

    }

}
