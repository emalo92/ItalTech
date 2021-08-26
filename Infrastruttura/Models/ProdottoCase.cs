using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class ProdottoCase
    {
        public int Codice { get; set; }
        public int Lotto { get; set; }
        public int CodiceProgetto { get; set; } //codice del progetto di riferimento.
        public string NomeProdotto { get; set; }
        public string Descrizione { get; set; }
        public decimal Costo { get; set; }
        public double Peso { get; set; }
        public String Colore { get; set; }
        public int Quantita { get; set; }

        public virtual Progetto CodiceProgettoNavigation { get; set; }
    }
}
