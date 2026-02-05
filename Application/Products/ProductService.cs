using Application.Common;
using Domain.ProductModels;
using Domain.ProductsVMS;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products
{
    public class ProductService : CrudService<Product>
    {
        public ProductService(AppDbContext context) : base(context) { }

        //public async Task<Product> CreateAsync(ProductDto prd)
        //{
        //    var product = new Product
        //    {
        //        Title = prd.Title,
        //        Price = prd.Price
        //    };

        //    return await base.CreateAsync(product);
        //}

        //public async Task<bool> UpdateAsync(ProductDto prd)
        //{
        //    var product = new Product
        //    {
        //        Id = prd.Id,
        //        Title = prd.Title,
        //        Price = prd.Price
        //    };
        //    return await base.UpdateAsync(product);
        //}

        //public async Task<bool> DeleteAsync(int productId)
        //{

        //    await base.DeleteAsync(productId);
        //    return true;
        //}

        //public async Task<Product> CreateAsync(ProductDto prd)
        //{
        //    var product = new Product
        //    {
        //        Title = prd.Title,
        //        Price = prd.Price
        //    };

        //    return await base.CreateAsync(product);
        //}
    }
}
