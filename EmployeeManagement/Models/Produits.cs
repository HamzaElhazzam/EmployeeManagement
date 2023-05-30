using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Produits
    {
        public int id { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public double Prix { get; set; }
        public string Description { get; set; }
    }
}
