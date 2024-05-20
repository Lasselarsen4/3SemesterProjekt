using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebshopAPI.Database;
using WebshopAPI.BusinessLogicLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IProductLogic, ProductLogic>();
builder.Services.AddScoped<IOrderLogic, OrderLogic>();
builder.Services.AddScoped<ICustomerLogic, CustomerLogic>();
builder.Services.AddScoped<ICartLogic, CartLogic>();
builder.Services.AddScoped<IOrderLineLogic, OrderLineLogic>();

builder.Services.AddScoped<DatabaseConnection>();


builder.Services.AddScoped<IProductDB, ProductDB>();
builder.Services.AddScoped<IOrderDB, OrderDB>();
builder.Services.AddScoped<ICustomerDB, CustomerDB>();
builder.Services.AddScoped<IOrderLineDB, OrderLineDB>();
builder.Services.AddScoped<ICartDB, CartDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();