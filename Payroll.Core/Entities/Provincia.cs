using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Provincia : IEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string CodigoAfip { get; set; } = string.Empty; // Ej: 0 (CABA), 1 (BsAs) para el aplicativo SICOSS
    public int PaisId { get; set; }
}