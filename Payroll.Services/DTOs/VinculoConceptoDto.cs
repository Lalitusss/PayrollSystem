namespace Payroll.Services.DTOs
{
    public class VinculoConceptoDto
    {
        public int Id { get; set; } // ID de la tabla VinculosConceptos
        public int ConceptoId { get; set; }
        public string NombreConcepto { get; set; } = string.Empty;
        public string Formula { get; set; } = string.Empty;
        public string TipoConcepto { get; set; } = string.Empty;
        public int ConvenioId { get; set; }
        public int CargoId { get; set; } 

        public decimal? ValorPersonalizado { get; set; }
        public string? FormulaOverride { get; set; }
    }
}