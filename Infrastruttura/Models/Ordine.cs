using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class Ordine
    {
        public int NumeroOrdine { get; set; }
        public int CodiceForniture { get; set; }
        public int QuantitaOrdine { get; set; }
        public DateTime DataOrdine { get; set; }
    }
}
