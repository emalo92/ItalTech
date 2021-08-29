using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Test
    {
        public Test()
        {
            TestProdottoAssemblatos = new HashSet<TestProdottoAssemblato>();
            TestProdottoCases = new HashSet<TestProdottoCase>();
            TestPrototipos = new HashSet<TestPrototipo>();
        }

        public int Codice { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public string ValoriDiRiferimento { get; set; }
        public int? QuantitaEseguiti { get; set; }
        public int? QuantitaPassati { get; set; }
        public int? QuantitaFalliti { get; set; }
        public string Operatore { get; set; }

        public virtual Impiegato OperatoreNavigation { get; set; }
        public virtual ICollection<TestProdottoAssemblato> TestProdottoAssemblatos { get; set; }
        public virtual ICollection<TestProdottoCase> TestProdottoCases { get; set; }
        public virtual ICollection<TestPrototipo> TestPrototipos { get; set; }
    }
}
