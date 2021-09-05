using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Admin.Models.RolesGroups
{
    public class GroupsDetail
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public GroupsMaster Master { get; set; }
    }
}
