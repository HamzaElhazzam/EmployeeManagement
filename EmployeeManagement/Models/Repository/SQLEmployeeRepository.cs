using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Repository
{
    public class SQLEmployeeRepository : ICompanyRepository<Employee>
    {
        public AppDbContext Context;
        public SQLEmployeeRepository(AppDbContext context)
        {
            Context = context;
        }

        

        public Employee Add(Employee entity)
        {
            
            this.Context.Employees.Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public Employee Delete(int id)
        {
            var employee = Get(id);
            if(employee != null)
            {
                this.Context.Employees.Remove(employee);
                this.Context.SaveChanges();
            }
            return employee;
        }

        public Employee Get(int id)
        {
            var employee = this.Context.Employees.SingleOrDefault(emp => emp.Id == id);
            return employee;
        }

        public IEnumerable<Employee> GetEntities()
        {
           
            return this.Context.Employees;
        }

        public Employee Update(Employee entityChanges)
        {
            var employee = this.Context.Employees.Attach(entityChanges);
            employee.State = EntityState.Modified;
            this.Context.SaveChanges();
            return entityChanges;
        }
    }
}
