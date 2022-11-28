using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.UI.Web.Intranet.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("NW");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<NWContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddDbContextPool<NWContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

#region Autorizaci�n
// Requerir autenticaci�n para todo el sitio, se except�a
// el uso espec�fico de Authorize o AllowAnonymous. RECOMENDADO    
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});
#endregion

builder.Services.AddTransient<BaseRepository<Employee, int>>();
builder.Services.AddTransient<BaseRepository<Region, int>>();
builder.Services.AddTransient<BaseRepository<Shipper, int>>();
builder.Services.AddTransient<BaseRepository<Supplier, int>>();
builder.Services.AddTransient<TerritoryRepository>();
builder.Services.AddTransient<CustomerRepository>();
builder.Services.AddTransient<ProductRepository>();
builder.Services.AddTransient<OrderRepository>();
builder.Services.AddTransient<CategoryRepository>(); // Instancia por controllador
//builder.Services.AddScoped<CategoryRepository>(); // Instancia por request
//builder.Services.AddSingleton<CategoryRepository>(); // Unica instancia para todos

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseStatusCodePages();
app.UseStatusCodePagesWithRedirects("/Home/ErrorWithCode?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute("admin", "admin", "admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
