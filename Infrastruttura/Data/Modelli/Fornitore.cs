using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Data.Modelli
{
    public class Fornitore
    {
        public string PartitaIva { get; set; }
        public string Nome { get; set; }
        public string NomeDitta { get; set; }
        public string Indirizzo { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
    }
}
