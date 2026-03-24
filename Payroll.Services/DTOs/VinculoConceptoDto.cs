namespace Payroll.Services.DTOs
{
    public class VinculoConceptoDto
    {
        public int Id { get; set; } // ID de la tabla VinculosConceptos
        public int ConceptoId { get; set; }
        public string NombreConcepto { get; set; } = string.Empty;
        public string Formula { get; set; } = string.Empty;
        public string TipoConcepto { get; set; } = string.Empty;

        public int EntidadId { get; set; }
        public int TipoEntidad { get; set; } // 1=Convenio, 3=Cargo (usando tu Enum)

        // Campos extra que definimos en la tabla SQL por si los necesitás mostrar
        public decimal? ValorPersonalizado { get; set; }
        public string? FormulaOverride { get; set; }
    }
}