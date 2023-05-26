using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IUniterofWork
    {
        IGenericRepostory<TEntity> Repostory<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
