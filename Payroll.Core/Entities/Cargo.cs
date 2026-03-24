using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Cargo : IEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal SueldoBasico { get; set; } // Ahora vive aquí
    public int CategoriaId { get; set; } // La nueva FK que renombramos

 }