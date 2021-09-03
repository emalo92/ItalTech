using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models.Input
{
    public class InputRicercaFornitori
    {
        public string PartitaIva { get; set; }
        public string Nome { get; set; }
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public string Cap { get; set; }
        public string NumTelefono { get; set; }
        public string Email { get; set; }
    }
}
