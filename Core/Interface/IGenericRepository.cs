using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IGenericRepository<T> where T : Base
    {
        DbSet<T> EntitySet { get; }
        void Add(T entity);

        void Update(T entity);


        void Remove(T entity);


        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        T Get(Expression<Func<T, bool>> expression);
    }
}
