using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class StoreDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public StoreDbContext(DbContextOptions<StoreDbContext> option) : base(option)
        {

        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockHolder> StocksHolder { get; set;}
        public DbSet<Orders> Orders { get; set; }

    }
}
