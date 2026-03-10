using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Convenio : IEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public string Numero { get; set; } = string.Empty;
    public ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();
}