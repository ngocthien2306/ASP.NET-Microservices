using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var result = await connection.ExecuteAsync("insert into Coupon(ProductName, Description, Amount) " +
                "values (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            return result == 0 ? false : true;

        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var result = await connection.ExecuteAsync("Delete from Coupon where ProductName = @ProductName",
                new { ProductName = productName});

            return result == 0 ? false : true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName});
            if(coupon == null)
            {
                return new Coupon { ProductName = "No discount", Amount = 0, Description = "No discount Desc" };
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var result = await connection.ExecuteAsync("update Coupon set ProductName = @ProductName, Description = @Description, Amount = @Amount " +
                "where Id = @Id", 
                new { ProductName =  coupon.ProductName, Description =  coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            return result == 0 ? false : true;
        }
    }
}
