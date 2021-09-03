using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Impiegato
    {
        public Impiegato()
        {
            AnalisiDiMercatoProgettis = new HashSet<AnalisiDiMercatoProgetti>();
            Ordinis = new HashSet<Ordini>();
            Progettos = new HashSet<Progetto>();
            RichiestaProgettos = new HashSet<RichiestaProgetto>();
            Tests = new HashSet<Test>();
        }

        public string CodFiscale { get; set; }
        public string UserId { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime? DataDiNascita { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Cap { get; set; }
        public string AziendaId { get; set; }

        public virtual AspNetUser User { get; set; }
        public virtual ICollection<AnalisiDiMercatoProgetti> AnalisiDiMercatoProgettis { get; set; }
        public virtual ICollection<Ordini> Ordinis { get; set; }
        public virtual ICollection<Progetto> Progettos { get; set; }
        public virtual ICollection<RichiestaProgetto> RichiestaProgettos { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
