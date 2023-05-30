using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        [Required]                 
        [MinLength(5)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        [Required]
        public Departement Departement { get; set; }
        
        public AppUser EmailUser { get; set; }
    }
}
