using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class AnalisiMercatoProgetti
    {
        public int Codice { get; set; }
        public string CodiceOperatore { get; set; }
        public string NomeOperatore { get; set; }
        public int CodiceProgetto { get; set; }
        public DateTime Datainizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string Descrizione { get; set; }
        public string Risultato { get; set; }
    }
}
