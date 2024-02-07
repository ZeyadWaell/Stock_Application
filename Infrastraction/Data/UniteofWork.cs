using Core.Entites;
using Core.Interface;
using Infrastraction.Identity;
using Infrastraction.Services;
using Microsoft.Extensions.Configuration;
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
        public UniteofWork(StoreDbContext db)
        {
           _db = db;
        }
        public StoreDbContext StoreDbContext { get; }
        #region private regon
        private IOrderRepostory orderRepostory;

        private IStockRepository stockRepostory;

        private IStockHolderRepostory stockHolderRepostory;

        #endregion

        #region public region
        public IOrderRepostory OrderRepostory => orderRepostory ??= new OrderRepostory(StoreDbContext);

        public IStockRepository StockRepository => stockRepostory ??= new StockRepository(StoreDbContext);

        public IStockHolderRepostory StockHolderRepostory => stockHolderRepostory ??= new StockHolderRepostory(StoreDbContext);

        #endregion

        public async Task<int> Complete()
      =>await _db.SaveChangesAsync();

    }
}
