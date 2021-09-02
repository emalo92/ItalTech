using Infrastruttura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Mapper
{
   public static class TestingMapper
    {
        public static Test ToDto(this Data.Modelli.Test test)
        {
            return new Test
            {
                Codice = test.Codice,
                Tipo = test.Tipo,
                Descrizione = test.Descrizione,
                ValoriDiRiferimento = test.ValoriDiRiferimento,
                QuantitaEseguiti = test.QuantitaEseguiti,
                QuantitaFalliti = test.QuantitaFalliti,
                QuantitaPassati = test.QuantitaPassati,
                Operatore = test.Operatore, 
            };
        }

        public static Data.Modelli.Test ToEntity(this Test test)
        {
            return new Data.Modelli.Test
            {
                Codice = test.Codice,
                Tipo = test.Tipo,
                Descrizione = test.Descrizione,
                ValoriDiRiferimento = test.ValoriDiRiferimento,
                QuantitaEseguiti = test.QuantitaEseguiti,
                QuantitaFalliti = test.QuantitaFalliti,
                QuantitaPassati = test.QuantitaPassati,
                Operatore = test.Operatore,
            };
        }

        public static List<Test> ToDto(this List<Data.Modelli.Test> tests)
        {
            List<Test> tst = new();
            foreach (var test in tests)
            {
                tst.Add(test.ToDto());
            }
            return tst;
        }
    }
}
