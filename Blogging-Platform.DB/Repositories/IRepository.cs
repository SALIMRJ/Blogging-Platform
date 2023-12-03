using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogging_Platform.Domain.Repo
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long Id ,Expression<Func<T, bool>> match=null);
        Task< List<T>> GetAllAsync(Expression<Func<T, bool>> match=null);
        Task<T> AddAsync(T entity);
        T UpdateAsync(T entity);
        void  DeleteAsync(T entity);
        Task<T> Find(Expression<Func<T, bool>> match, string[] includes =null);

    }
}
