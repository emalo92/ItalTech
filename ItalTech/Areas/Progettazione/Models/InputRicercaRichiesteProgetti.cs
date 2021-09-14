using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Progettazione.Models
{
    public class InputRicercaRichiesteProgetti
    {
        
        public int Codice { get; set; }

       
        public int? CodiceProgetto { get; set; }

       
        public string Tipo { get; set; }

        [Display(Name = "Budget")]
        public decimal? Budget { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Operatore")]
        public string Operatore { get; set; }
    }
}
