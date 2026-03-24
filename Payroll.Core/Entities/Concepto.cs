using Payroll.Core.Enums;
using Payroll.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Core.Entities;

public class Concepto : IEntity
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal ValorDefecto { get; set; }
    public bool EsPorcentaje { get; set; }
    public TipoConcepto Tipo { get; set; }

    public bool EsTotalizador { get; set; }

    public int Orden { get; set; }

    public string? Formula { get; set; }

    public DateTime FechaVigenciaDesde { get; set; } = DateTime.Now;

    public DateTime? FechaVigenciaHasta { get; set; }

    public bool AfectaAguinaldo { get; set; } = true;

    public bool Activo { get; set; } = true;
}
 