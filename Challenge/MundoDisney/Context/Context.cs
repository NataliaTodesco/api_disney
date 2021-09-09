using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

public class Context : DbContext
{
    private string cadenaConexion;
    public DbSet<Personaje> Personajes {get;set;}
    public DbSet<Pelicula_Serie> Peliculas_Series {get;set;}
    public DbSet<Genero> Generos {get;set;}
    public DbSet<Usuario> Usuarios {get;set;}
    public Context(DbContextOptions options) : base(options)
    {
    }
    public Context()
    {
        var configuration = GetConfiguration();
        cadenaConexion =
        configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
    }
    public IConfigurationRoot GetConfiguration() //Va a appsetting.json y obtiene todas las keys
    {
        var builder = new
        ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional:
        true, reloadOnChange: true);
        return builder.Build();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(cadenaConexion);
        }
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        base.OnModelCreating(builder);
    }
}