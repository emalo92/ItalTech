using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastruttura.Models.Configuration.RolesGroups
{
    public class GroupsDetail
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public GroupsMaster Master { get; set; }
    }
}
