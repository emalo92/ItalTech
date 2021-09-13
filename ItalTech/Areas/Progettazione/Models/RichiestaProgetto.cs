using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Progettazione.Models
{
    public class RichiestaProgetto
    {
        [Display(Name = "Codice")]
        public int Codice { get; set; }

        [Display(Name = "CodiceProgetto")]
        public int? CodiceProgetto { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [Display(Name = "EsitoStudio")]
        public bool? EsitoStudio { get; set; }

        [Display(Name = "Budget")]
        public decimal? Budget { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Operatore")]
        public string Operatore { get; set; }

    }
}
