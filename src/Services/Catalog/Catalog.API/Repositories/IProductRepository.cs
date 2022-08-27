using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<List<Products>> GetProducts();
        Task<Products> GetProductsById(string productId);

        Task<List<Products>> GetProductByName(string productName);
        Task<List<Products>> GetProdyctByCatogory(string catogoryName);
        Task CreateProduct(Products products);

        Task<bool> DeleteProduct(string productId);
        Task<bool> UpdateProduct(Products products);
    }
}
