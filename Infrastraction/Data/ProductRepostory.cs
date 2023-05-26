using Core.Entites;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class ProductRepostory : IProductRepostory
    {
        private readonly StoreDbContext _db;

        public ProductRepostory(StoreDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<Product>> GetProductAsync()
        => await _db.Products.Include(x=>x.ProductType).Include(x => x.ProductBrand).ToListAsync();

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
       => await _db.Brands.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int? Id)
       => await _db.Products.Include(x=>x.ProductBrand).Include(x => x.ProductType).FirstOrDefaultAsync(x => x.Id == Id);

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
       => await _db.ProductTypes.ToListAsync();
    }
}
