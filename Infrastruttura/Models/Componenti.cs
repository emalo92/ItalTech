using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class Componenti
    {
        public int CodiceFornitura { get; set; }
        public string Nome { get; set; }
        public int Quantita { get; set; }
        public int Posizione { get; set; }
        public bool Disponibile { get; set; }
        public string Descrizione { get; set; }
        public decimal Costo { get; set; }
        public string PartitaIva { get; set; }
        public int QuantitaSelezionata { get; set; }
    }
}
