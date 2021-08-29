using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class RichiestaProgetto
    {
        public int Codice { get; set; }
        public int? CodiceProgetto { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public bool? EsitoStudio { get; set; }
        public decimal? Budget { get; set; }
        public string Cliente { get; set; }
        public string Operatore { get; set; }

        public virtual Cliente ClienteNavigation { get; set; }
        public virtual Progetto CodiceProgettoNavigation { get; set; }
        public virtual Impiegato OperatoreNavigation { get; set; }
    }
}
