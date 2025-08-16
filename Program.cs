using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ProvaPub.Filters;
using ProvaPub.Models.Enum;
using ProvaPub.Repository;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;
using ProvaPub.Services.Strategy;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(c =>
{
    c.Filters.Add(typeof(ApiGlobalExceptionFilter));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentStrategyFactory, PaymentStrategyFactory>();
builder.Services.AddScoped<IOrderPaymentService, OrderPaymentService>();
builder.Services.AddScoped<IRandomNumberService, RandomNumberService>();
builder.Services.AddScoped<PixPayment>();

builder.Services.AddScoped<CreditCardPayment>();
builder.Services.AddScoped<PayPalPayment>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRandomNumberRepository, RandomNumberRepository>();

builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
