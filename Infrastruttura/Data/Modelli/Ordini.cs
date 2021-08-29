using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Ordini
    {
        public Ordini()
        {
            OrdineFornitures = new HashSet<OrdineForniture>();
        }

        public int Codice { get; set; }
        public DateTime? DataCreazione { get; set; }
        public DateTime? DataInvio { get; set; }
        public string Operatore { get; set; }

        public virtual Impiegato OperatoreNavigation { get; set; }
        public virtual ICollection<OrdineForniture> OrdineFornitures { get; set; }
    }
}
