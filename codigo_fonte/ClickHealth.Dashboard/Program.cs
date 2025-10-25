// USING statements DESCOMENTADOS
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// LINHA DESCOMENTADA: Registra o DbContext novamente
builder.Services.AddDbContext<ClickHealthContext>(options =>
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura a sessão em memória
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Ativa a sessão

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();