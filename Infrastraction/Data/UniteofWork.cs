using Core.Entites;
using Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class UniteofWork : IUniterofWork
    {
        private readonly StoreDbContext _db;
        private Hashtable _repostory;

        public UniteofWork(StoreDbContext db)
        {
            _db = db;
        }

        public async Task<int> Complete()
      =>await _db.SaveChangesAsync();

        public IGenericRepostory<TEntity> Repostory<TEntity>() where TEntity : BaseEntity
        {
            if(_repostory == null)
                _repostory = new Hashtable();

            var type = typeof(TEntity).Name;
            if (!_repostory.ContainsKey(type))
            {
                var repostoryType = typeof(genericRepostorycs<>);

                var repostoryInstance = Activator.CreateInstance(repostoryType.MakeGenericType(typeof(TEntity)),_db);

                _repostory.Add(type, repostoryInstance);
            }

            return (IGenericRepostory<TEntity>)_repostory[type];
        }
    }
}
