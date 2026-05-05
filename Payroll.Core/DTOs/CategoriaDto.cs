using System.Text.Json.Serialization;

namespace Payroll.Core.DTOs;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int ConvenioId { get; set; }

    // âœ… Solo incluir si viene en la respuesta (evita JSON innecesario)
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<CargoDto>? Cargos { get; set; }
}
