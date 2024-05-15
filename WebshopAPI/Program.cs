using WebshopAPI.Database;
using WebshopAPI.BusinessLogicLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IProductLogic, ProductLogic>();
builder.Services.AddSingleton<IOrderLogic, OrderLogic>();
builder.Services.AddSingleton<ICustomerLogic, CustomerLogic>();
builder.Services.AddSingleton<ICartLogic, CartLogic>();
builder.Services.AddSingleton<IOrderLineLogic, OrderLineLogic>();
builder.Services.AddSingleton<IProductDB, ProductDB>();
builder.Services.AddSingleton<IOrderDB, OrderDB>();
builder.Services.AddSingleton<ICustomerDB, CustomerDB>();
builder.Services.AddSingleton<IOrderLineDB, OrderLineDB>();
builder.Services.AddSingleton<DatabaseConnection>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();