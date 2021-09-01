using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Progettazione.Models
{
    public class Progetto
    {
        [Display(Name = "Codice")]
        public int Codice { get; set; }
        [Display(Name = "Data Inizio")]
        public DateTime DataInizio { get; set; }
        [Display(Name = "Data Fine")]
        public DateTime? DataFine { get; set; }
        [Display(Name = "Nome")]
        public string NomeProgetto { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [Display(Name = "Costo previsto")]
        public decimal? CostoPrevisto { get; set; }
        [Display(Name = "Costo finale")]
        public decimal? CostoFinale { get; set; }
        [Display(Name = "Analisi mercato")]
        public int? CodiceAnalisiMercato { get; set; }
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
        [Display(Name = "Cliente")]
        public string Cliente { get; set; }
        [Display(Name = "Project Manager")]
        public string ProjectManager { get; set; }
    }
}
