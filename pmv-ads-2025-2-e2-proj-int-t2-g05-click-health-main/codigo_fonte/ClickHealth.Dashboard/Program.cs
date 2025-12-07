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
    db.Database.EnsureCreated();
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

// Sessão antes de tudo
app.UseSession();

// 🔒 Middleware para bloquear páginas protegidas
app.Use(async (context, next) =>
{
    // Páginas que exigem login
    var requiresLogin = new[]
    {
        "/Feed",
        "/Medicamentos",
        "/Agenda"
    };

    var path = context.Request.Path.Value ?? "";

    bool isProtected = requiresLogin.Any(p =>
        path.StartsWith(p, StringComparison.OrdinalIgnoreCase)
    );

    // ⚠️ Corrigido: a chave correta é UserId
    bool isLogged = context.Session.GetInt32("UserId") != null;

    if (isProtected && !isLogged)
    {
        context.Response.Redirect("/Account/Login");
        return;
    }

    await next();
});

app.UseAuthorization();

// Rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
