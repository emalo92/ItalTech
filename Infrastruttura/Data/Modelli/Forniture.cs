using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Data.Modelli
{
    public class Forniture
    {
        public int CodiceFornitura { get; set; }
        public string Nome { get; set; }
        public int Quantita { get; set; }
        public int Posizione { get; set; }//posizione magazzino
        public bool Disponibile { get; set; }// disponibilità pezzo
        public string Descrizione { get; set; }
        public decimal Costo { get; set; }
        public string PartitaIva { get; set; }//fornitore che l'ha fornito
        
    }
}
