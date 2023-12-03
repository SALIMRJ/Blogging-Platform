using Blogging_Platform.DB.DB;
using Blogging_Platform.Domain.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Platform.DB.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;

        public Repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
          await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public void DeleteAsync(T entity)
        {
             _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<T> Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null) 
                foreach(var include in includes)
                    query=query.Include(include);
            return await query.SingleOrDefaultAsync(match);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> match=null)
        {
            IQueryable<T> query = _context.Set<T>();

            return await query.Where(match).ToListAsync();
        }

        public async Task<T> GetByIdAsync(long Id, Expression<Func<T, bool>> match = null)
        {
            IQueryable<T> query = _context.Set<T>();
            return await query.SingleOrDefaultAsync(match);
        }

        public T UpdateAsync(T entity)
        {
             _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
