using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities
{
    public class TipoConcepto : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ColorHex { get; set; } = "#000000";
    }
}
