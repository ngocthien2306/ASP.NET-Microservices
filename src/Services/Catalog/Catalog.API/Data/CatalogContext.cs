using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var clients = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var databases = clients.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = databases.GetCollection<Products>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
          
        }

        
        public IMongoCollection<Products> Products { get ; }
    }
}
