using Basket.API.GrpcServices;
using Basket.API.IBasketRepositories;
using Discount.Grpc.Protos;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddGrpcClient<DiscountProtoServices.DiscountProtoServicesClient>
    (o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<DiscountGrpcServices>();
builder.Services.AddStackExchangeRedisCache(op =>
{
    op.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
