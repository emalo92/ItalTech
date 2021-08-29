using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class OrdineForniture
    {
        public int CodiceOrdine { get; set; }
        public string CodiceFornitura { get; set; }
        public int Quantità { get; set; }

        public virtual Fornitura CodiceFornituraNavigation { get; set; }
        public virtual Ordini CodiceOrdineNavigation { get; set; }
    }
}
