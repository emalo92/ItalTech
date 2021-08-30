using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models
{
    public class Prototipo
    {
        public int Numero { get; set; }
        public int CodiceProgetto { get; set; }
        public DateTime? Data { get; set; }
        public string Descrizione { get; set; }
        public string Modifiche { get; set; }
        public bool? RisultatoTest { get; set; }
        public string MotivoFallimento { get; set; }

    }
}
