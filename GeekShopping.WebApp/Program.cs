using GeekShopping.WebApp.Models.Services;
using GeekShopping.WebApp.Models.Services.Iservices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<IProductService, ProductService>(c =>
    c.BaseAddress = new Uri($"{builder.Configuration["ServicesUrls:ProducAPI"]}")
);
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
