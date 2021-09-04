using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models.Input
{
    public class InputRicercaClienti
    {
        public string CodFiscale { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime? DataDiNascita { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Cap { get; set; }
    }
}
