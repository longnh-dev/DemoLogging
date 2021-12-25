using DemoLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoLogging.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(int productId);

        Task<Product> CreateNewProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int productId);
    }
}