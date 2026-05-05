using Microsoft.EntityFrameworkCore;

using Payroll.Core.Entities;
using Payroll.Data.Configurations;
namespace Payroll.Data;

public class PayrollDbContext : DbContext
{
    public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options) { }
    public DbSet<AsignacionCargo> AsignacionesCargos => Set<AsignacionCargo>();
    public DbSet<Banco> Bancos => Set<Banco>();
    public DbSet<Cargo> Cargos => Set<Cargo>();
    public DbSet<DatoBancario> DatosBancarios => Set<DatoBancario>();
    public DbSet<Direccion> Direcciones => Set<Direccion>();
    public DbSet<Familiar> Familiares => Set<Familiar>();
    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<Concepto> Conceptos => Set<Concepto>();
    public DbSet<Convenio> Convenios => Set<Convenio>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Provincia> Provincias => Set<Provincia>();
    public DbSet<Pais> Paises => Set<Pais>();
    public DbSet<ObraSocial> ObrasSociales => Set<ObraSocial>();
    public DbSet<LiquidacionCabecera> LiquidacionesCabecera => Set<LiquidacionCabecera>();
    public DbSet<LiquidacionDetalle> LiquidacionesDetalle => Set<LiquidacionDetalle>();
    public DbSet<VinculoConcepto> VinculosConceptos => Set<VinculoConcepto>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
             
       // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConvenioConfiguration).Assembly);
    }

    // Agrega este método a tu DbContext (fuera de OnModelCreating)
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Esto quita todos los warnings de decimales de un solo golpe
        // Aplicando precisión 18 y escala 2 a todo el modelo.
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        configurationBuilder.Properties<decimal?>().HavePrecision(18, 2);
    }
}