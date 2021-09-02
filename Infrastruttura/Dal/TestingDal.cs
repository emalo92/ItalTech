using Infrastruttura.Data.Context;
using Infrastruttura.Models;
using Infrastruttura.Mapper;
using Infrastruttura.Models.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        
    }
    
}
