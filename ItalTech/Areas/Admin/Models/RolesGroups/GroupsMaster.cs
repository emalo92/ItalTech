using Infrastruttura.Models.Configuration.RolesGroups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Admin.Models.RolesGroups
{
    public class GroupsMaster
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome gruppo")]
        public string Name { get; set; }

        [Display(Name = "Descrizione gruppo")]
        public string Description { get; set; }

        [Display(Name = "Disabilitato")]
        public bool Disabled { get; set; }
        public ICollection<GroupsDetail> GroupsDetails { get; set; }
        public List<string> AssociatedUsers { get; set; }
    }
}
