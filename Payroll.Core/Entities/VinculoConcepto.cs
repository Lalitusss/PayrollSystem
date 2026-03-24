using Payroll.Core.Entities;
using Payroll.Core.Interfaces;

namespace Payroll.Domain.Entities
{
    public class VinculoConcepto : IEntity
    {
        public int Id { get; set; }
        public int ConceptoId { get; set; }
        public int EntidadId { get; set; }
        public int TipoEntidad { get; set; } // Usará tu Enum (1=Conv, 2=Cat, 3=Cargo)
        public decimal? ValorPersonalizado { get; set; }
        public string? FormulaOverride { get; set; }
        public bool Activo { get; set; } = true;
        public virtual Concepto Concepto { get; set; } = null!;
    }
}