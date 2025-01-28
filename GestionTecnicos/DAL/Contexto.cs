using GestionTecnicos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTecnicos.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Ciudades> Ciudades { get; set; }
    
    public DbSet<Tickets> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Tecnicos>().HasKey(t => t.TecnicoId);
    }

}