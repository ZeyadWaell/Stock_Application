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
        IOrderRepostory OrderRepostory { get; }
        IStockRepository StockRepository { get; }

        IStockHolderRepostory StockHolderRepostory { get; }
        Task<int> Complete();
    }
}
