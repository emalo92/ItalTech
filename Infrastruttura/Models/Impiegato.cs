using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class Impiegato
    {
        public string IdAziendale { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public int NumeroTelefono { get; set; }
        public string Email { get; set; }
        public string Competenze { get; set; }

    }
}
