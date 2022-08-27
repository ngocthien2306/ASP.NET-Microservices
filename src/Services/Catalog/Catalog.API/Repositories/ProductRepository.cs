using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateProduct(Products products)
        {
            await _context.Products.InsertOneAsync(products);
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.Id, productId);
            DeleteResult deleteResult = await _context.Products
                                                        .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged 
                && deleteResult.DeletedCount > 0;
        }

        public async Task<List<Products>> GetProductByName(string productName)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.ElemMatch(p => p.Category, productName);
            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<List<Products>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Products> GetProductsById(string productId)
        {
            return await _context
                            .Products
                            .Find(p => p.Id == productId)
                            .FirstOrDefaultAsync();

        }

        public async Task<List<Products>> GetProdyctByCatogory(string catogoryName)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.ElemMatch(p => p.Category, catogoryName);
            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Products products)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == products.Id, replacement: products);
            return updateResult.IsAcknowledged && updateResult.MatchedCount > 0;
        }
    }
}
