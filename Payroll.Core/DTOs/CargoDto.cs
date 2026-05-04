using Payroll.Core.Entities;

namespace Payroll.Core.DTOs;
public class CargoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal SueldoBasico { get; set; } // Ahora vive aquí
    public int CategoriaId { get; set; } // La nueva FK que renombramos

}