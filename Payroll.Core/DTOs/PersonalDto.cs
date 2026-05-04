namespace Payroll.Core.DTOs;
public class PersonalDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Cuil { get; set; } = string.Empty;

    // Opcional: Si quieres mostrar el legajo en la lista
    public string? Legajo { get; set; }

    // Propiedad calculada para mostrar nombre completo fácilmente
    public string NombreCompleto => $"{Apellido}, {Nombre}";
}