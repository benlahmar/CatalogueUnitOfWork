using Catalogue.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.EF.Repositories
{
    public class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        public readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities= _context.Set<T>();
        }

        public IQueryable<T> Query => _entities;

        public T add(T entity)
        {
            _entities.Add(entity);  
            return entity;  
        }

        public IEnumerable<T> findAll()
        {
           return  _entities.ToList();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
           return _entities.Where(expression);
        }

        public T findById(int id)
        {
            T? e = _entities.Find(id);
            return e;
        }

        public async Task<T> findByIdAsync(int id)
        {
            T? e = await _entities.FindAsync(id);
            return e;
        }

        public async Task<T> Save(T entity)
        {
              await _entities.AddAsync(entity);

            return entity;// new Task<T>(()=> entity  );
        }
    }
}
