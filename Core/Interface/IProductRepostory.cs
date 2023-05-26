using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IProductRepostory
    {
        Task<Product> GetProductByIdAsync(int? Id);

        Task<IReadOnlyList<Product>> GetProductAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

    }
}
