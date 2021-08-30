using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class ProdottoAssemblato
    {
        public int Codice { get; set; }
        public int? Lotto { get; set; }
        public int CodiceProgetto { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public decimal? Costo { get; set; }
        public string FasciaDiMercato { get; set; }
        public double? Peso { get; set; }
        public int Quantita { get; set; }
    }
}
