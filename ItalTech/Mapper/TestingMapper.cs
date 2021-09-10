using ItalTech.Areas.Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Mapper
{
    public static class TestingMapper
    {
        public static Infrastruttura.Models.Test ToDto(this Test test)
        {
            return new Infrastruttura.Models.Test
            {
                Codice = test.Codice,
                Tipo = test.Tipo,
                ValoriDiRiferimento = test.ValoriDiRiferimento,
                Descrizione = test.Descrizione,
                QuantitaEseguiti = test.QuantitaEseguiti,
                QuantitaPassati = test.QuantitaPassati,
                QuantitaFalliti = test.QuantitaFalliti,
                Operatore = test.Operatore,
            };
        }
    }
}
