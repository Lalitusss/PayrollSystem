namespace Payroll.Services.DTOs
{
    public class AsignacionMasivaDto
    {
        public int ConvenioId { get; set; }
        // Esta es la clave: una lista de objetos, no de enteros
        public List<ItemAsignacionDto> Items { get; set; } = new();
    }
}