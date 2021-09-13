using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ItalTech.Areas.Testing.Models
{
    public class Test
    {
        [Display(Name = "Codice")]
        public int Codice { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Valori di Riferimento")]
        public string ValoriDiRiferimento { get; set; }

        [Display(Name = "Operatore")]
        public string Operatore { get; set; }

        [Display(Name = "Test Eseguiti")]
        public int? QuantitaEseguiti { get; set; }

        [Display(Name = "Test Superati")]
        public int? QuantitaPassati { get; set; }

        [Display(Name = "Test Falliti")]
        public int? QuantitaFalliti { get; set; }

        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

    }
}