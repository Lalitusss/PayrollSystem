namespace Payroll.Core.DTOs;
public class EmpleadoEdicionDto
{
    // Datos General
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public string CUIL { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; }
    public int? ObraSocialId { get; set; }

    // --- DIRECCIÓN (Relación: Direccion) ---
    // Nombre del Objeto + Nombre de la Propiedad
    public int? DireccionId { get; set; }
    public string? DireccionCalle { get; set; }
    public string? DireccionAltura { get; set; }
    public string? DireccionPiso { get; set; }
    public string? DireccionDepto { get; set; }
    public string? DireccionLocalidad { get; set; }
    public int DireccionProvinciaId { get; set; }

    // --- BANCO (Relación: DatoBancario) ---
    // Nombre del Objeto + Nombre de la Propiedad
    public int? DatoBancarioBancoId { get; set; }
    public string? DatoBancarioCBU { get; set; }
    public string? DatoBancarioAlias { get; set; }

    // --- FAMILIARES (Relación: Familiar) ---
    // Aquí el nombre debe ser igual al de la ICollection en la Entidad
    public List<FamiliarDto> Familiar { get; set; } = new();
}