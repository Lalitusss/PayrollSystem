namespace Payroll.Services.DTOs
{
    public class ConceptoSeleccionableDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string TipoConcepto { get; set; } = string.Empty; // Ej: Remunerativo, No Remunerativo

        // Esta es la propiedad clave para el checkbox en Blazor
        public bool Seleccionado { get; set; }
    }
}