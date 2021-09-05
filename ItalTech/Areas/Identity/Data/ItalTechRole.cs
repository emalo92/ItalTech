using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Identity.Data
{
    public class ItalTechRole : IdentityRole
    {
        public string Description { get; set; }
        public string Menu { get; set; }
        public string SubMenu1 { get; set; }
        public string SubMenu2 { get; set; }
    }
}
