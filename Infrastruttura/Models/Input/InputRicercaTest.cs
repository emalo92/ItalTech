using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models.Input
{
    public class InputRicercaTest
    {
        public int Codice { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public string ValoriDiRiferimento { get; set; }
        public int? QuantitaEseguiti { get; set; }
        public int? QuantitaPassati { get; set; }
        public int? QuantitaFalliti { get; set; }
        public string Operatore { get; set; }
    }
}
