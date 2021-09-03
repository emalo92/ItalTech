using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models.Input
{
    public class InputRicercaRichiesteProgetti
    {
        public int Codice { get; set; }
        public int? CodiceProgetto { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public bool? EsitoStudio { get; set; }
        public decimal? Budget { get; set; }
        public string Cliente { get; set; }
        public string Operatore { get; set; }
    }
}
