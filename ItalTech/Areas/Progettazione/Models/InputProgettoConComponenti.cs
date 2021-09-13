using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Progettazione.Models
{
    public class InputProgettoConComponenti
    {
        public Infrastruttura.Models.Progetto Testata { get; set; }
        public List<Infrastruttura.Models.Componente> Dettaglio { get; set; }
    }
}
