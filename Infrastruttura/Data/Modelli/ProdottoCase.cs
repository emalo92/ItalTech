using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class ProdottoCase
    {
        public ProdottoCase()
        {
            TestProdottoCases = new HashSet<TestProdottoCase>();
        }

        public int Codice { get; set; }
        public int? Lotto { get; set; }
        public int CodiceProgetto { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public decimal? Costo { get; set; }
        public double? PesoKg { get; set; }
        public string Colore { get; set; }
        public int Quantita { get; set; }

        public virtual Progetto CodiceProgettoNavigation { get; set; }
        public virtual ICollection<TestProdottoCase> TestProdottoCases { get; set; }
    }
}
