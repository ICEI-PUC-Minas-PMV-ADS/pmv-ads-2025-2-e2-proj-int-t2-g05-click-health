using ClickHealth.Dashboard.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Banco de dados: SQLite usando o arquivo clickhealth.db
builder.Services.AddDbContext<ClickHealthContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Sessão
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Criar banco caso não exista
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClickHealthContext>();
    try
    {
        db.Database.EnsureCreated();
        Console.WriteLine("✅ Banco SQLite criado/validado com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("⚠️ Erro ao criar o banco SQLite:");
        Console.WriteLine(ex.Message);
    }
}

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

// Rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
