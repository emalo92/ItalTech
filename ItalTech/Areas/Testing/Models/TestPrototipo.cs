using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Testing.Models
{
        public class TestPrototipo
        {

            [Display(Name = "CodiceTest")]
            public int CodiceTest { get; set; }

            [Display(Name = "CodicePrototipo")]
            public int NumPrototipo { get; set; }

            [Display(Name = "Codice Progetto")]
            public int CodiceProgetto { get; set; }
        }
    }
