using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Repository
{
    public class EmployeeRepository : ICompanyRepository<Employee>
    {
        public List<Employee> Employees { get; set; }
        public List<Produits> Produits;
        public EmployeeRepository(List<Employee> Employees)
        { 
            this.Employees = new List<Employee>();
            Employees.Add(new Employee() { Id = 1, Name = "yassine", Email = "yassine@gmail.com", PhotoPath = "/images/emp.png", Departement = Departement.IT });
            Employees.Add(new Employee() { Id = 2, Name = "ennajem", Email = "Ennajem@gmail.com", PhotoPath = "/images/emp.png", Departement = Departement.Networking });
        }
        public Employee Add(Employee entity)
        {
            entity.Id = Employees.Max(emp => emp.Id) + 1;
            entity.PhotoPath = "/Images/emp.png";
            Employees.Add(entity);
            return entity;
        }

       

        public Employee Delete(int id)
        {
            var employee = Employees.Find(emp => emp.Id == id);
            if(employee != null)
            {
                Employees.Remove(employee);
            }
            return employee;
        }

        public Employee Get(int id)
        {
            return Employees.SingleOrDefault(emp => emp.Id == id);
        }

        public IEnumerable<Employee> GetEntities()
        {
            //if(Produits.SingleOrDefault(p => p.id == ))
            return Employees;
        }

        public IEnumerable<Employee> GetEntities(string userId)
        {
            throw new NotImplementedException();
        }

        public Employee Update(Employee entityChanges)
        {
            var employee = Employees.Find(emp => emp.Id == entityChanges.Id);
            if (employee != null)
            {
                employee.Name = entityChanges.Name;
                employee.Departement = entityChanges.Departement;
                employee.Email = entityChanges.Email;
                employee.PhotoPath = entityChanges.PhotoPath;
                Employees.Remove(employee);
            }
            return employee;
        }

        
    }
}
