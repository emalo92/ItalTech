
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class Test
    {
        public int CodiceTest { get; set; }
        public string IdAziendale { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public int NumPrototipo { get; set; }
        public int NumProgetto { get; set; }//ho inserito entrambi perchè tutte e 2 sono chiavi di prototipo
        public bool Risultato { get; set; }
    }
}
