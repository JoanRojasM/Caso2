using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Caso_2.Models;
using System.Reflection.Emit;

public class CasoContext : IdentityDbContext<Usuario>
{
    public CasoContext(DbContextOptions<CasoContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Relación uno a muchos entre Categoria y Evento
        builder.Entity<Categoria>()
            .HasMany(c => c.Eventos)
            .WithOne(e => e.Categoria)
            .HasForeignKey(e => e.CategoriaId)
            .OnDelete(DeleteBehavior.Cascade); // Elimina los eventos asociados si se elimina una categoría

        // Configuración de valores predeterminados
        builder.Entity<Categoria>()
            .Property(c => c.FechaRegistro)
            .HasDefaultValueSql("GETDATE()");

        builder.Entity<Evento>()
            .Property(e => e.FechaRegistro)
            .HasDefaultValueSql("GETDATE()");
        // Crear roles iniciales
        var adminRole = new IdentityRole { Id = "1", Name = "Administrador", NormalizedName = "ADMINISTRADOR" };
        var organizadorRole = new IdentityRole { Id = "2", Name = "Organizador", NormalizedName = "ORGANIZADOR" };
        var usuarioRole = new IdentityRole { Id = "3", Name = "Usuario", NormalizedName = "USUARIO" };

        builder.Entity<IdentityRole>().HasData(adminRole, organizadorRole, usuarioRole);

        // Crear usuarios iniciales
        var hasher = new PasswordHasher<Usuario>();

        var adminUser = new Usuario
        {
            Id = "1",
            UserName = "admin@example.com",
            NormalizedUserName = "ADMIN@EXAMPLE.COM",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            EmailConfirmed = true,
            NombreCompleto = "Administrador General",
            Telefono = "123456789",
            Rol = "Administrador"
        };
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123");

        var organizadorUser = new Usuario
        {
            Id = "2",
            UserName = "organizador@example.com",
            NormalizedUserName = "ORGANIZADOR@EXAMPLE.COM",
            Email = "organizador@example.com",
            NormalizedEmail = "ORGANIZADOR@EXAMPLE.COM",
            EmailConfirmed = true,
            NombreCompleto = "Organizador Evento",
            Telefono = "987654321",
            Rol = "Organizador"
        };
        organizadorUser.PasswordHash = hasher.HashPassword(organizadorUser, "Organizador123");

        var usuarioUser = new Usuario
        {
            Id = "3",
            UserName = "usuario@example.com",
            NormalizedUserName = "USUARIO@EXAMPLE.COM",
            Email = "usuario@example.com",
            NormalizedEmail = "USUARIO@EXAMPLE.COM",
            EmailConfirmed = true,
            NombreCompleto = "Usuario Regular",
            Telefono = "1122334455",
            Rol = "Usuario"
        };
        usuarioUser.PasswordHash = hasher.HashPassword(usuarioUser, "Usuario123");

        builder.Entity<Usuario>().HasData(adminUser, organizadorUser, usuarioUser);

        // Asignar roles a los usuarios
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "1", RoleId = "1" }, // Admin
            new IdentityUserRole<string> { UserId = "2", RoleId = "2" }, // Organizador
            new IdentityUserRole<string> { UserId = "3", RoleId = "3" }  // Usuario
        );
    }
}