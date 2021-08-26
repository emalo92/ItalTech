using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class AnalisiMercato
    {
        public int NumAnalisiMercato { get; set; }
        public string IdAziendale { get; set; }
        public int NumProgetto { get; set; }
        public DateTime Data { get; set; }
        public string Descrizioni { get; set; }
        public string Risultato { get; set; }
    }
}
