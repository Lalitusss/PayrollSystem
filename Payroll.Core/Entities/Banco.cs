using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Banco : IEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Codigo { get; set; }
}