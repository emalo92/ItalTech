using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Componente
    {
        public string CodiceFornitura { get; set; }
        public int CodiceProgetto { get; set; }
        public int NumPezzi { get; set; }

        public virtual Fornitura CodiceFornituraNavigation { get; set; }
        public virtual Progetto CodiceProgettoNavigation { get; set; }
    }
}
