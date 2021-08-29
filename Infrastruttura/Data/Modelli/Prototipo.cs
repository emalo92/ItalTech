using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Prototipo
    {
        public Prototipo()
        {
            TestPrototipos = new HashSet<TestPrototipo>();
        }

        public int Numero { get; set; }
        public int CodiceProgetto { get; set; }
        public DateTime? Data { get; set; }
        public string Descrizione { get; set; }
        public string Modifiche { get; set; }
        public bool? RisultatoTest { get; set; }
        public string MotivoFallimento { get; set; }

        public virtual Progetto CodiceProgettoNavigation { get; set; }
        public virtual ICollection<TestPrototipo> TestPrototipos { get; set; }
    }
}
