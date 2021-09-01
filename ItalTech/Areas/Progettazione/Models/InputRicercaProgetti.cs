using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Progettazione.Models
{
    public class InputRicercaProgetti
    {
        public int Codice { get; set; }
        [Display(Name = "Data Inizio")]
        public DateTime? DataInizio { get; set; }
        [Display(Name = "Nome Progetto")]
        public string NomeProgetto { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        [Display(Name = "Project Manager")]
        public string ProjectManager { get; set; }
    }
}
