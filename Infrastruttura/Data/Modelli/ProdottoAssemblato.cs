using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class ProdottoAssemblato
    {
        public ProdottoAssemblato()
        {
            TestProdottoAssemblatos = new HashSet<TestProdottoAssemblato>();
        }

        public int Codice { get; set; }
        public int? Lotto { get; set; }
        public int CodiceProgetto { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public decimal? Costo { get; set; }
        public string FasciaDiMercato { get; set; }
        public double? Peso { get; set; }
        public int Quantita { get; set; }

        public virtual Progetto CodiceProgettoNavigation { get; set; }
        public virtual ICollection<TestProdottoAssemblato> TestProdottoAssemblatos { get; set; }
    }
}
