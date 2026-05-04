using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Empleado : IEntity
{
    public int Id { get; set; }
    public string Apellido { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public string CUIL { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Sexo { get; set; }
    public DateTime FechaIngreso { get; set; }
    public bool Activo { get; set; } = true;

    public int? ObraSocialId { get; set; }
    public ObraSocial? ObraSocial { get; set; }

    // Propiedades de navegación puras (sin virtual)
    public Direccion? Direccion { get; set; }
    public DatoBancario? DatoBancario { get; set; }
    public List<Familiar> Familiar { get; set; } = new();
}