using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class LiquidacionCabecera : IEntity
{
    public int Id { get; set; }
    public int EmpleadoId { get; set; }
    public int Mes { get; set; }
    public int Anio { get; set; }
    public DateTime FechaProceso { get; set; } = DateTime.Now;
    public decimal TotalBruto { get; set; }
    public decimal TotalNoRemunerativo { get; set; }
    public decimal TotalDescuentos { get; set; }
    public decimal NetoResultante { get; set; }
}