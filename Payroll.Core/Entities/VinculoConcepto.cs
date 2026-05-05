using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class VinculoConcepto : IEntity
{
    public int Id { get; set; }
    public int ConceptoId { get; set; }
    public int ConvenioId { get; set; }
    public int? CargoId { get; set; } // Usará tu Enum (1=Conv, 2=Cat, 3=Cargo)
    public decimal? ValorPersonalizado { get; set; }
    public string? FormulaOverride { get; set; }
    public bool Activo { get; set; } = true;
    public virtual Concepto Concepto { get; set; } = null!;
}
