namespace Payroll.Services.DTOs;

public class ConvenioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public bool Activo { get; set; }

    // Al llamarse [Coleccion] + [Count], Mapster ejecuta un 
    // SELECT COUNT(...) en SQL automáticamente.
    public int CargosCount { get; set; }
    public List<CategoriaDto> Categorias { get; set; } = new();
}