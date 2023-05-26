using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specfication
{
    public class ProductWithTypeAndBrandSpecfications : BaseSpecfication<Product>
    {
        public ProductWithTypeAndBrandSpecfications(ProductSpecParms productspec) : base(product=>
        (string.IsNullOrEmpty(productspec.Serach) || product.Name.ToLower().Contains(productspec.Serach)) &&
            (!productspec.BrandId.HasValue || product.ProductBrandId ==productspec.BrandId) && (!productspec.TypetId.HasValue || product.ProductTypeId ==productspec.TypetId))
            
            
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
            AddOrderBy(product => product.Name);
            ApplyPaging(productspec.PageSize * (productspec.PageIndex-1),productspec.PageSize);

            if (!string.IsNullOrEmpty(productspec.Sort))
            {
                switch(productspec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDecending(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
        }
        public ProductWithTypeAndBrandSpecfications(int Id)
      : base(x => x.Id==Id)
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }
    }
  

}
