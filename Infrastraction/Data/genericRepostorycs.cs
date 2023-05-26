using Core.Entites;
using Core.Interface;
using Core.Specfication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class genericRepostorycs<T> : IGenericRepostory<T> where T : BaseEntity
    {
        private readonly StoreDbContext _db;
        public genericRepostorycs(StoreDbContext context)
        {
            _db = context;
        }
        public async void Add(T entity)
         => _db.Set<T>().Add(entity);


        public void Delete(T entity)
      => _db.Set<T>().Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _db.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
       => await _db.Set<T>().FindAsync(id);

        public async Task<T> GetEntityWithSpecfictions(ISpecfication<T> specfictions)
       => await ApplySpecfication(specfictions).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecfication<T> specfictions)
          => await ApplySpecfication(specfictions).ToListAsync();

        private IQueryable<T> ApplySpecfication(ISpecfication<T> specfication)
              =>  SpecficationEvalutor<T>.GetQuery(_db.Set<T>().AsQueryable(), specfication);
        public void Update(T entity)
         => _db.Set<T>().Update(entity);

        public async Task<int> CountAsync(ISpecfication<T> specfication)
        => await ApplySpecfication(specfication).CountAsync();
    }
}
