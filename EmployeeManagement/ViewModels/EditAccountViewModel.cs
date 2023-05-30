using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EditAccountViewModel : AppUser
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password makaytchabeh mea lakhr hh")]
        [Display(Name = "Confirme PassWord")]
        public string confirmPassword { get; set; }
    }
}
