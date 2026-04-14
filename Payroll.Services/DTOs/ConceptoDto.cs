using Payroll.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Services.DTOs;

public class ConceptoDto
{
    public int Id { get; set; }
    [MaxLength(4)]
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal ValorDefecto { get; set; }
    public bool EsPorcentaje { get; set; }
    public TipoConcepto Tipo { get; set; }
    public bool EsTotalizador { get; set; }

    // NUEVAS PROPIEDADES PARA LA REESTRUCTURACIÓN:
    public int Orden { get; set; }
    public string? Formula { get; set; }
    public DateTime FechaVigenciaDesde { get; set; } = DateTime.Now;
    public DateTime? FechaVigenciaHasta { get; set; }
    public bool AfectaAguinaldo { get; set; } = true;

    // ESTA ES LA QUE TE FALTA Y CAUSA EL ERROR:
    public bool Activo { get; set; } = true;
    public int? CargoId { get; set; }
    public int? VinculoConceptoId { get; set; }

}