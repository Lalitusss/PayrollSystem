namespace Payroll.Services.DTOs;

public class ConceptoDto
{
    public int Id { get; set; }
    public int Codigo { get; set; }

    // 1. Resolvemos el error de 'Nombre': 
    // Usamos 'Nombre' como propiedad principal porque CargoForm.razor lo pide.
    public string Nombre { get; set; } = string.Empty;

    // 2. Resolvemos el error de 'Descripcion': 
    // Creamos un alias. Así, cuando ConceptoForm.razor pida 'Descripcion', 
    // obtendrá lo mismo que hay en 'Nombre'.
    public string Descripcion
    {
        get => Nombre;
        set => Nombre = value;
    }

    public decimal ValorDefecto { get; set; }
    public bool EsPorcentaje { get; set; }
    public int TipoConceptoId { get; set; }

    // 3. Resolvemos la columna 'Tipo' vacía:
    // Al llamarse 'TipoNombre', Mapster buscará automáticamente en Concepto.Tipo.Nombre
    public string TipoNombre { get; set; } = string.Empty;

    // Extra: Para que los badges tengan color en la lista
    public string TipoColorHex { get; set; } = string.Empty;
}