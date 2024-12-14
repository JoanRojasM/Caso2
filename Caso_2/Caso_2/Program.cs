using Caso_2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddDbContext<CasoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<CasoContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Usuarios/AccessDenied";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// GET /api/events - Lista de eventos
app.MapGet("/api/events", async (CasoContext db) =>
{
    var eventos = await db.Eventos
        .Where(e => e.Estado) // Solo eventos activos
        .Select(e => new
        {
            e.Id,
            e.Titulo,
            e.Descripcion,
            Categoria = e.Categoria != null ? e.Categoria.Nombre : "Sin Categoría",
            FechaEvento = e.Fecha.ToString("yyyy-MM-dd"),
            HoraEvento = e.Hora.ToString(@"hh\:mm"),
            e.Ubicacion,
            e.CupoMaximo
        })
        .ToListAsync();

    return Results.Ok(eventos);
});

// GET /api/events/{id}
app.MapGet("/api/events/{id:int}", async (int id, CasoContext db) =>
{
    var evento = await db.Eventos
        .Where(e => e.Estado && e.Id == id) 
        .Select(e => new
        {
            e.Id,
            e.Titulo,
            e.Descripcion,
            Categoria = e.Categoria != null ? e.Categoria.Nombre : "Sin Categoría",
            FechaEvento = e.Fecha.ToString("yyyy-MM-dd"),
            HoraEvento = e.Hora.ToString(@"hh\:mm"),
            e.Ubicacion,
            e.CupoMaximo,
            e.Duracion
        })
        .FirstOrDefaultAsync();

    if (evento == null)
    {
        return Results.NotFound(new { Message = $"El evento con ID {id} no fue encontrado." });
    }

    return Results.Ok(evento);
});

app.Run();
