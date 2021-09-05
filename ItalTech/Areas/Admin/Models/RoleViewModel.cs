using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string AssociatedMenu { get; set; }
        public string AssociatedSubMenu1 { get; set; }
        public string AssociatedSubMenu2 { get; set; }
    }
}
