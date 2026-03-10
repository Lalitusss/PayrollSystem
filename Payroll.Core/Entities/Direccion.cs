using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Direccion : IEntity
{
    public int Id { get; set; }
    public int PersonaId { get; set; } // FK
    public string? Calle { get; set; }
    public string? Altura { get; set; }
    public string? Piso { get; set; }
    public string? Depto { get; set; }
    public string? CodigoPostal { get; set; }
    public string? Localidad { get; set; }
    public int ProvinciaId { get; set; }
    public Provincia? Provincia { get; set; }
    public int PaisId { get; set; }
}