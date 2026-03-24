using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Categoria : IEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int ConvenioId { get; set; } // FK al convenio

    // Relación: Una Categoría tiene muchos Cargos
    public virtual ICollection<Cargo> Cargos { get; set; }
}