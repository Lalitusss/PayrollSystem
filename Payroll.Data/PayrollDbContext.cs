using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;

namespace Payroll.Data;

public class PayrollDbContext : DbContext
{
    public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options) { }

    public DbSet<Banco> Bancos => Set<Banco>();
    public DbSet<Cargo> Cargos => Set<Cargo>();
    public DbSet<CategoriaConcepto> CategoriasConceptos => Set<CategoriaConcepto>();
    public DbSet<DatoBancario> DatosBancarios => Set<DatoBancario>();
    public DbSet<Direccion> Direcciones => Set<Direccion>();
    public DbSet<Familiar> Familiares => Set<Familiar>();
    public DbSet<Persona> Personas => Set<Persona>();
    public DbSet<Concepto> Conceptos => Set<Concepto>();
    public DbSet<Convenio> Convenios => Set<Convenio>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Provincia> Provincias => Set<Provincia>();
    public DbSet<Pais> Paises => Set<Pais>();
    public DbSet<TipoConcepto> TiposConceptos => Set<TipoConcepto>();
    public DbSet<ObraSocial> ObrasSociales => Set<ObraSocial>();
    public DbSet<LiquidacionCabecera> LiquidacionesCabecera => Set<LiquidacionCabecera>();
    public DbSet<LiquidacionDetalle> LiquidacionesDetalle => Set<LiquidacionDetalle>();
    public DbSet<Sistema> Sistema => Set<Sistema>(); //Fix load

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CategoriaConcepto>()
        .HasKey(cc => new { cc.CategoriaId, cc.ConceptoId });

        // Configuración de precisión decimal para .NET 10
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(18);
            property.SetScale(2);
        }

        // Relación 1 a 1: Persona -> Direccion
        modelBuilder.Entity<Persona>()
            .HasOne(p => p.Direccion)
            .WithOne()
            .HasForeignKey<Direccion>(d => d.PersonaId);

        // Relación 1 a 1: Persona -> DatosBancarios
        modelBuilder.Entity<Persona>()
            .HasOne(p => p.DatoBancario)
            .WithOne()
            .HasForeignKey<DatoBancario>(db => db.PersonaId);

        // Relación 1 a N: Persona -> Familiares
        modelBuilder.Entity<Persona>()
            .HasMany(p => p.Familiar)
            .WithOne()
            .HasForeignKey(f => f.PersonaId);
    }
}