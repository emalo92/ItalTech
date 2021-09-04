using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Models.Input
{
    public class InputRicercaComponenti
    {
        public string CodiceFornitura { get; set; }
        public int CodiceProgetto { get; set; }
        public int NumPezzi { get; set; }
    }
}
