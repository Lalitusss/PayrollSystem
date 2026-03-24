namespace Payroll.Engine.Models;

public class ResultadoItem
{
    // El ID de la base de datos (ConceptoId) para facilitar el INSERT posterior
    public int ConceptoId { get; set; }

    // El código humano (ej: "1000", "4000")
    public string Codigo { get; set; } = string.Empty;

    // El nombre del concepto (ej: "Sueldo Básico")
    public string Descripcion { get; set; } = string.Empty;

    // El valor final calculado por NCalc
    public decimal Importe { get; set; }

    // La base de cálculo (ej: el valor del básico sobre el que se aplica un %)
    public decimal Base { get; set; }

    // La cantidad (ej: 15 días, 10 horas, o 1 unidad)
    public decimal Cantidad { get; set; }

    // El tipo (2=Rem, 3=NoRem, 4=Desc) para agrupar en el recibo
    public int Tipo { get; set; }
}