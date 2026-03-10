using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Pais : IEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string CodigoISO { get; set; } = string.Empty; // Ej: ARG, BRA, USA
}