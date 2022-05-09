using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
/*
 * auteur BENLAHMAR
 */
namespace Catalogue.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public T add(T entity);
        
        public IEnumerable<T> findAll();
        public T findById(int id);
        public Task<T> findByIdAsync(int id);   
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> Query { get; }

    }
}
