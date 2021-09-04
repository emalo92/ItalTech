using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models.Input
{
    public class InputRicercaOrdini
    {
        public int Codice { get; set; }
        public DateTime? DataCreazione { get; set; }
        public DateTime? DataInvio { get; set; }
        public string Operatore { get; set; }
    }
}
