// Program.cs
using mf_dev_backend_2025.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===== Servi�os =====

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
    app.UseHsts(); // HTTPS estrito em produ��o
}
else
{
    // Em dev, deixa as p�ginas de erro detalhadas
    app.UseDeveloperExceptionPage();
}

// HTTPS + arquivos est�ticos (wwwroot)
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// (Se tiver autentica��o/autoriza��o, ficariam aqui)
// app.UseAuthentication();
app.UseAuthorization();

// ===== Rotas =====

// 1) Rota para �REAS (Dashboard, Admin, etc.)
//    Padr�o: /{Area}/{Controller}/{Action}/{id?}
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

// 2) Rota default do site (controllers fora de �reas)
//    Padr�o: /{controller}/{action}/{id?}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// (Opcional) Se voc� usar Razor Pages em alguma �rea:
// app.MapRazorPages();

app.Run();
