using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class LiquidacionDetalle : IEntity
{
    public int Id { get; set; }
    public int LiquidacionId { get; set; }
    public int ConceptoId { get; set; }
    public string DescripcionOriginal { get; set; } = string.Empty;
    public decimal Cantidad { get; set; }
    public decimal SubtotalRemunerativo { get; set; }
    public decimal SubtotalNoRemunerativo { get; set; }
    public decimal SubtotalDescuento { get; set; }
}