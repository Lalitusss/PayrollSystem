namespace Payroll.Core.DTOs;
public class EmpleadoDto
{
    public int Id { get; set; }
    public string Apellido { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string NombreCompleto => $"{Apellido}, {Nombre}";
    public string CUIL { get; set; }
    public string Email { get; set; }
    public string ObraSocialNombre { get; set; } // Propiedad plana
    public string ObraSocialSigla { get; set; }  // Propiedad plana
    public DateTime FechaIngreso { get; set; }
    public bool Activo { get; set; }
}
