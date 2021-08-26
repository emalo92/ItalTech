using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class RichiestaProgetto
    {
        public int Codice { get; set; }

        public int? CodiceProgetto { get; set; }

        public string Descrizione { get; set; }

        public string EsitoStudio { get; set; }

        public decimal? Budget { get; set; }


    }
}
