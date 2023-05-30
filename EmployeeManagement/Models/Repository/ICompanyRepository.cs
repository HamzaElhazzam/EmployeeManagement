using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Repository
{
    public interface ICompanyRepository<TEntity>
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetEntities();
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entityChanges);
        TEntity Delete(int id);
    }
}
