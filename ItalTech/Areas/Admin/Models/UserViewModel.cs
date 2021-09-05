using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool UserDisabled { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? AccountExpired { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}