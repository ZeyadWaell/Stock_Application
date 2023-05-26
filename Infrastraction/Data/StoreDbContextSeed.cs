using Core.Entites;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public  class StoreDbContextSeed
    {

        public static async Task SeedAsync(StoreDbContext context,ILoggerFactory loggerfactory)

        {
            try
            {
                if(context.Brands != null && !context.Brands.Any())
                {
                    var brandData = File.ReadAllText("../Infrastraction/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach(var brand in brands)
                        context.Brands.Add(brand);

                    await context.SaveChangesAsync();
                }
                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var ProductTypesData = File.ReadAllText("../Infrastraction/Data/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);

                    foreach (var Type in Types)
                        context.ProductTypes.Add(Type);

                    await context.SaveChangesAsync();
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Infrastraction/Data/SeedData/products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    foreach (var products in product)
                        context.Products.Add(products);

                    await context.SaveChangesAsync();
                }
                if (context.DeliverMethods != null && !context.DeliverMethods.Any())
                {
                    var DeliverMethodsData = File.ReadAllText("../Infrastraction/Data/SeedData/delivery.json");
                    var DeliverMethods = JsonSerializer.Deserialize<List<DeliverMethod>>(DeliverMethodsData);

                    foreach (var method in DeliverMethods)
                        context.DeliverMethods.Add(method);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<StoreDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
