using Core.Entites;
using Core.Interface;
using Infrastraction.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        protected readonly StoreDbContext _dbContext;
        public DbSet<T> EntitySet { get; }

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            EntitySet = _dbContext.Set<T>();
        }



        public void Add(T entity)
            => _dbContext.Add(entity);


        public T Get(Expression<Func<T, bool>> expression)
            => EntitySet.FirstOrDefault(expression);

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
            => await EntitySet.FirstOrDefaultAsync(expression);



        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await EntitySet.OrderByDescending(x => x.CreatedOn).ToListAsync(cancellationToken);
        }


        public void Remove(T entity)
            => _dbContext.Remove(entity);

        public void Update(T entity)
            => _dbContext.Update(entity);


    }
}
