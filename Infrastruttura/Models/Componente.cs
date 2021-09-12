using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class Componente
    {
        public string CodiceFornitura { get; set; }
        public int CodiceProgetto { get; set; }
        public int NumPezzi { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
    }
}
