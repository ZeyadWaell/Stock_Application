using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specfication
{
    public class ProductwithFilterForCountSpecfication : BaseSpecfication<Product>
    {
        public ProductwithFilterForCountSpecfication(ProductSpecParms productspec) : base(product =>
          (string.IsNullOrEmpty(productspec.Serach) || product.Name.ToLower().Contains(productspec.Serach)) &&
          (!productspec.BrandId.HasValue || product.ProductBrandId == productspec.BrandId) && (!productspec.TypetId.HasValue || product.ProductTypeId == productspec.TypetId))
        {
        }


        }
}
