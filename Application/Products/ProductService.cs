using Application.Common;
using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products
{
    public class ProductService :CrudService<Product>
    {
        public ProductService(AppDbContext context) : base(context) { }
        public async Task<Product> CreateAsync(ProductDto prd)
        {
            var product = new Product
            {
                Title = prd.Title,
                Price = prd.Price
            };

            return await base.CreateAsync(product);
        }
    }
}
