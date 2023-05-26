using Core.Entites;
using Core.Specfication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IGenericRepostory<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetEntityWithSpecfictions(ISpecfication<T> specfictions);
        Task<IReadOnlyList<T>> ListAsync(ISpecfication<T> specfictions);
        Task<int> CountAsync(ISpecfication<T> specfication);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
