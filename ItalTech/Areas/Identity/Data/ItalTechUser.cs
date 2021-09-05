using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ItalTech.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ItalTechUser class
    public class ItalTechUser : IdentityUser
    {
        [Display(Name = "Data scadenza password")]
        public DateTime? NextPasswordUpdate { get; set; }

        [Display(Name = "Data ultimo accesso")]
        public DateTime? LastLogin { get; set; }
        public bool ForceChangePassword { get; set; }
        public bool UserDisabled { get; set; }

        [Display(Name = "Data scadenza utente")]
        public DateTime? AccountExpiration { get; set; }

        [Display(Name = "Data creazione utente")]
        public DateTime? CreationDate { get; set; }
    }
}
