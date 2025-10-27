// Program.cs
using mf_dev_backend_2025.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===== Serviços =====

// MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// Opcional enquanto desenvolve (hot-reload das views .cshtml):
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// EF Core + SQLite (usa a connection string "DefaultConnection" do appsettings.json)
builder.Services.AddDbContext<AppBbContex>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ===== Pipeline HTTP =====

// Tratamento de erros
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTPS estrito em produção
}
else
{
    // Em dev, deixa as páginas de erro detalhadas
    app.UseDeveloperExceptionPage();
}

// HTTPS + arquivos estáticos (wwwroot)
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// (Se tiver autenticação/autorização, ficariam aqui)
// app.UseAuthentication();
app.UseAuthorization();

// ===== Rotas =====

// 1) Rota para ÁREAS (Dashboard, Admin, etc.)
//    Padrão: /{Area}/{Controller}/{Action}/{id?}
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

// 2) Rota default do site (controllers fora de áreas)
//    Padrão: /{controller}/{action}/{id?}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// (Opcional) Se você usar Razor Pages em alguma área:
// app.MapRazorPages();

app.Run();
