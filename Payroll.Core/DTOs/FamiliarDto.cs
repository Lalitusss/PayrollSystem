namespace Payroll.Core.DTOs;

public class FamiliarDto
{
    public int Id { get; set; }
    public int EmpleadoId { get; set; }

    public string Apellido { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;

    // Propiedad calculada en el DTO para mostrar en la grilla de familiares
    public string NombreCompleto => $"{Apellido}, {Nombre}";

    public string DNI { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }

    public int Parentesco { get; set; }

    // Estos booleanos son claves para los checkboxes en la pestaña "Familiares"
    public bool Discapacidad { get; set; }
    public bool Escolaridad { get; set; }
    public bool ACargo { get; set; }
}