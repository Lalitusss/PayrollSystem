namespace Payroll.Engine.Models;

public class ConceptoDto
{
    public int Id { get; set; }

    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Formula { get; set; } = string.Empty;
    public int Orden { get; set; }
    public int Tipo { get; set; } // 1: Rem, 2: NoRem, 3: Desc
}