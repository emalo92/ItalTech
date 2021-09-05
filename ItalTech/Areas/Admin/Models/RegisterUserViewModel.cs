using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Admin.Models
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Inserire lo UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Inserire la password")]
        [StringLength(100, ErrorMessage = "La {0} deve essere minimo {2} e massimo {1} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Inserire la password di conferma")]
        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password", ErrorMessage = "La password e la password di conferma non coincidono.")]
        public string ConfirmPassword { get; set; }
        public bool IsSendedEmail { get; set; }
    }
}
