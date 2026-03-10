using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;
public class ObraSocial : IEntity
{
    public int Id { get; set; }
    public string CodigoARCA { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Sigla { get; set; }
}