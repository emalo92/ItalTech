using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class AnalisiDiMercatoProgetti
    {
        public int Codice { get; set; }
        public string CodiceOperatore { get; set; }
        public string NomeOperatore { get; set; }
        public int CodiceProgetto { get; set; }
        public DateTime Datainizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string Descrizione { get; set; }
        public string Risultato { get; set; }

        public virtual Impiegato CodiceOperatoreNavigation { get; set; }
        public virtual Progetto CodiceProgettoNavigation { get; set; }
    }
}
