using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class Fornitura
    {
        public string Codice { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public decimal? CostoPerPezzo { get; set; }
        public decimal? CostoAlKg { get; set; }
        public string CodiceFornitore { get; set; }
        public int Quantita { get; set; }
        public string SettoreDeposito { get; set; }
        public string Tipo { get; set; }

    }
}
