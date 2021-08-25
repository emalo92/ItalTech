using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Data.Modelli
{
    public class ProdottoAssembalto
    {
        public int Codice { get; set; }
        public int Lotto { get; set; }
        public int CodiceProgetto { get; set; } 
        public string NomeProdotto { get; set; }
        public string Descrizione { get; set; }// tecnicamente in descizione dovrebbero esserci i pezzi assemblati, ma a questo punto converrebbe una solo classe prodotto
        public decimal Costo { get; set; }
        public double Peso { get; set; }
        public int Quantita { get; set; }
        

    }
}
