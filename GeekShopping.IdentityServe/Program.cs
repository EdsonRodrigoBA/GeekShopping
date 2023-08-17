using Duende.IdentityServer.Services;
using GeekShopping.IdentityServe.Configuration;
using GeekShopping.IdentityServe.Domain;
using GeekShopping.IdentityServe.Domain.Context;
using GeekShopping.IdentityServe.Initializer;
using GeekShopping.IdentityServe.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
var conection = builder.Configuration.GetConnectionString("mysqlConnectionString");

builder.Services.AddDbContext<IdentityMySqlContext>(options =>
    options.UseMySql(conection, new MySqlServerVersion(new Version(8, 0, 21)))
);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentityMySqlContext>().AddDefaultTokenProviders();

var buiderIdentityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;


}).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)

    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<ApplicationUser>();

buiderIdentityServer.AddDeveloperSigningCredential();
builder.Services.AddScoped<IDBInitializer, DBInitializer>();
builder.Services.AddScoped<IProfileService, ProfileService>();





var app = builder.Build();

var scope = app.Services.CreateScope();
var serviceInitializer = scope.ServiceProvider.GetService<IDBInitializer>();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
serviceInitializer?.Initialize();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
