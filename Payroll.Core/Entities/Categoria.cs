using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Categoria : IEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal SueldoBasico { get; set; }
    public int CargoId { get; set; }
    public Cargo? Cargo { get; set; }
    public virtual ICollection<CategoriaConcepto> CategoriaConceptos { get; set; }
}