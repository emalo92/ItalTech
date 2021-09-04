using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models.Input
{
    public class InputRicercaOrdiniForniture
    {
        public int CodiceOrdine { get; set; }
        public string CodiceFornitura { get; set; }
        public int Quantità { get; set; }
    }
}
