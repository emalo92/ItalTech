using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Data.Modelli
{
   public class Ordine
    {
        public int NumeroOrdine { get; set; }
        public int CodiceForniture { get; set; }//componente da ordinare
        //public int PartitaIva { get; set; } fornitore da dove ordinarli? lo metto?
        public int QuantitaOrdine { get; set; }
        public DateTime DataOrdine { get; set; }
    }
}
