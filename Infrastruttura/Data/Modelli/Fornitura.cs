using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Fornitura
    {
        public Fornitura()
        {
            Componentes = new HashSet<Componente>();
            OrdineFornitures = new HashSet<OrdineForniture>();
        }

        public string Codice { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public decimal? CostoPerPezzo { get; set; }
        public decimal? CostoAlKg { get; set; }
        public string PartitaIva { get; set; }
        public int Quantita { get; set; }
        public string SettoreDeposito { get; set; }
        public string Tipo { get; set; }

        public virtual Fornitore CodiceFornitoreNavigation { get; set; }
        public virtual ICollection<Componente> Componentes { get; set; }
        public virtual ICollection<OrdineForniture> OrdineFornitures { get; set; }
    }
}
