using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        [Required (ErrorMessage = "the role name field is requered !")]
        [Display(Name = "Enter the Role Name :")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
