using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Data.Modelli
{
    public class Prototipo
    {
        public int NumPrototipo { get; set; }
        public int NumProgetto { get; set; }//progetto di riferimento del prototipo
        public int CodiceTest { get; set; }
        public DateTime Data { get; set; }
        public bool Risultato { get; set; }

    }
}
