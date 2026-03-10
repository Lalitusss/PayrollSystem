using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Concepto : IEntity
{
    public int Id { get; set; }
    public int Codigo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal ValorDefecto { get; set; }
    public bool EsPorcentaje { get; set; }
    public int TipoConceptoId { get; set; }
    public TipoConcepto? Tipo { get; set; }
}