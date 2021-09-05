using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Admin.Models.RolesGroups
{
    public class UserRoles
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string IdRole { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string RoleMenu { get; set; }
        public string RoleSubMenu1 { get; set; }
        public string RoleSubMenu2 { get; set; }
    }
}
