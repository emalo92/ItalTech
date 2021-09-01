using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models.Input
{
    public class InputRicercaProgetti
    {
        public int Codice { get; set; }
        public DateTime? DataInizio { get; set; }
        public string NomeProgetto { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string ProjectManager { get; set; }
    }
}
