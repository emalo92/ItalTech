using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class Progetto
    {
        public Progetto()
        {
            AnalisiDiMercatoProgettis = new HashSet<AnalisiDiMercatoProgetti>();
            Componentes = new HashSet<Componente>();
            ProdottoAssemblatos = new HashSet<ProdottoAssemblato>();
            ProdottoCases = new HashSet<ProdottoCase>();
            Prototipos = new HashSet<Prototipo>();
            RichiestaProgettos = new HashSet<RichiestaProgetto>();
        }

        public int Codice { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string NomeProgetto { get; set; }
        public string Descrizione { get; set; }
        public decimal? CostoPrevisto { get; set; }
        public decimal? CostoFinale { get; set; }
        public int? CodiceAnalisiMercato { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string ProjectManager { get; set; }

        public virtual Cliente ClienteNavigation { get; set; }
        public virtual Impiegato ProjectManagerNavigation { get; set; }
        public virtual ICollection<AnalisiDiMercatoProgetti> AnalisiDiMercatoProgettis { get; set; }
        public virtual ICollection<Componente> Componentes { get; set; }
        public virtual ICollection<ProdottoAssemblato> ProdottoAssemblatos { get; set; }
        public virtual ICollection<ProdottoCase> ProdottoCases { get; set; }
        public virtual ICollection<Prototipo> Prototipos { get; set; }
        public virtual ICollection<RichiestaProgetto> RichiestaProgettos { get; set; }
    }
}
