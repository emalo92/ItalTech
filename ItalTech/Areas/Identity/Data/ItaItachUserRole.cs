using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Identity.Data
{
    public class ItaItachUserRole : IdentityUserRole<string>
    {
        public virtual ItalTechUser User { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}
