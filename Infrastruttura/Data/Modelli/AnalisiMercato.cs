using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Data.Modelli
{
    public class AnalisiMercato
    {
        public int NumAnalisiMercato { get; set; }
        public string IdAziendale { get; set; }
        public int NumProgetto { get; set; }// progetto a cui sono riferite le analisi
        public DateTime Data{ get; set; }
        public string Descrizioni { get; set; }
        public string Risultato { get; set; }
    }
}
