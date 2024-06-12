using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebStocks.BusinessService;
using WebStocks.Controllers;
using WebStocks.CustomMiddleware;
using WebStocks.DbStuff;
using WebStocks.DbStuff.Repositories;
using WebStocks.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(AuthController.AUTH_KEY)
    .AddCookie(AuthController.AUTH_KEY, option =>
    {
        option.LoginPath = "/Auth/Login";
        option.AccessDeniedPath = "/Auth/denie";
    });

var connectionString = builder.Configuration.GetConnectionString("WebStocksDb");
builder.Services.AddDbContext<WebDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();
// Add services to the container.

//Helpers
builder.Services.AddScoped<PortfolioHelper>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ReflectionService>();

//Permissions
builder.Services.AddScoped<StockPermissions>();
builder.Services.AddScoped<DividendPermissions>();

//BusinnesService
builder.Services.AddScoped<StockBusinnesService>();
builder.Services.AddScoped<DividendBusinnesService>();

//Repositories
builder.Services.AddScoped<StockRepository>();
builder.Services.AddScoped<DividendRepository>();
builder.Services.AddScoped<UserRepository>();

// Вариант автоматической регистрации репозиториев
//var typeOfBaseRepository = typeof(BaseRepository<>);
//Assembly
//    .GetAssembly(typeOfBaseRepository)
//    .GetTypes()
//    .Where(x => x.BaseType?.IsGenericType ?? false
//        && x.BaseType.GetGenericTypeDefinition() == typeOfBaseRepository)
//    .ToList()
//    .ForEach(repositoryType => builder.Services.AddScoped(repositoryType));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

SeedExtension.Seed(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.UseMiddleware<CustomLocalizationMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=StocksPortfolio}/{action=Home}/{id?}");

app.Run();
