using EmployeeManagement.Tools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "CheckingExistingEmail",controller:"Account")]
        [ValidEmailDomainAttribute(Domain:"elhazzam.com;gmail.com;hotmail.com;hotmail.fr;ofppt-edu.ma",ErrorMessage = "Email domain must be in elhazzam.com or gmail.com or hotmail.com or hotmail.fr or ofppt-edu.ma ")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
  
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password makaytchabeh mea lakhr hh")]
        [Display(Name ="Confirme PassWord")]
        public string confirmPassword { get; set; }
        [Required]
        [Range(minimum:10,maximum:60)]
        public int Age { get; set; }

    }
}
