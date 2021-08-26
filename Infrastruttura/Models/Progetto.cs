using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class Progetto
    {
        public int Codice { get; set; }
        public DateTime Data { get; set; }
        public string Descrizione { get; set; }
        public string IdAziendale { get; set; }//chi redige il progetto
        public decimal CostoLavoro { get; set; }
        public decimal Costo { get; set; }// tipo un costo del progetto stesso (possibile preventivo)
        public string Analisi { get; set; }
    }
}
