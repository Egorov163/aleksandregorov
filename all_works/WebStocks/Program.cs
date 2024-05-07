using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff;
using WebStocks.DbStuff.Repositories;
using WebStocks.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("WebStocksDb");
builder.Services.AddDbContext<WebDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<Portfolio>();

//Repositories
builder.Services.AddScoped<StockRepository>();
builder.Services.AddScoped<DividendRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=StocksPortfolio}/{action=Home}/{id?}");

app.Run();
