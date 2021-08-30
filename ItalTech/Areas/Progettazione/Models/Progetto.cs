using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Progettazione.Models
{
    public class Progetto
    {
        public int Codice { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string NomeProgetto { get; set; }
        public string Descrizione { get; set; }
        public decimal? CostoPrevisto { get; set; }
        public decimal? CostoFinale { get; set; }
        public int? CodiceAnalisiMercato { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string ProjectManager { get; set; }
    }
}
