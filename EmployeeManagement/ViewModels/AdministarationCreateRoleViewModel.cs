using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class AdministarationCreateRoleViewModel
    {
        [Required]
        [Display(Name = "Enter The Name :")]
        public string RoleName { get; set; }
    }
}
