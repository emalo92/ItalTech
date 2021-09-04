using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastruttura.Models.Configuration.RolesGroups
{
    public class GroupsMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Disabled { get; set; }
        public ICollection<GroupsDetail> GroupsDetails { get; set; }
    }
}
