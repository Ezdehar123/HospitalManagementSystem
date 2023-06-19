using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{


    public interface IGenericRepository<T> : IDisposable
    {


        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");


        T GetByID(object id);
        Task<T> GetByIDAsync(object id);

        void Add(T entity);
        Task<T> AddAsync(T entity);
        Task<T> Update(T entity);
        Task<T> UpdateAsync(T entity);

        void Delete(T entity);

        Task<T> DeleteAsync(T entity);
    }
}



