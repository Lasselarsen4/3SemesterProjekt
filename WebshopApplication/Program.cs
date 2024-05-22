using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebshopApplication.ServiceLayer;
using WebshopApplication.Middleware; // Ensure you include the correct namespace for your middleware

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Register your services here
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IOrderLineService, OrderLineService>();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

// Use the custom cart item count middleware
app.UseMiddleware<CartItemCountMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "product",
    pattern: "Product/{action=Index}/{id?}",
    defaults: new { controller = "Product" });

app.MapRazorPages();

app.Run();