using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Fornitore
    {
        public Fornitore()
        {
            Fornituras = new HashSet<Fornitura>();
        }

        public string PartitaIva { get; set; }
        public string Nome { get; set; }
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public string Cap { get; set; }
        public string NumTelefono { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Fornitura> Fornituras { get; set; }
    }
}
