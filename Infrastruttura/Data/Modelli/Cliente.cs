using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Cliente
    {
        public Cliente()
        {
            Progettos = new HashSet<Progetto>();
            RichiestaProgettos = new HashSet<RichiestaProgetto>();
        }

        public string CodFiscale { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime? DataDiNascita { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Cap { get; set; }

        public virtual ICollection<Progetto> Progettos { get; set; }
        public virtual ICollection<RichiestaProgetto> RichiestaProgettos { get; set; }
    }
}
