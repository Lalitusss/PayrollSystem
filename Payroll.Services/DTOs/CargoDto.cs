namespace Payroll.Services.DTOs;

public class CargoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Útil para mostrar en las tablas de la lista
    public string? ConvenioNombre { get; set; }

    // Útil si alguna vez haces un formulario para editar Cargos 
    // y quieres elegir el Convenio de un dropdown
    public int? ConvenioId { get; set; }

    public List<CategoriaDto> Categorias { get; set; } = new();
}