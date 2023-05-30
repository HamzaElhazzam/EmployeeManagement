using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Calcule
    {
        public int Num1 { get; set; }
        public int Num2 { get; set; }
        [Required]
        public int Res { get; set; }
        public bool add { get; set; }
        public bool sous { get; set; }
        public string calcule { get; set; }

    }
}
