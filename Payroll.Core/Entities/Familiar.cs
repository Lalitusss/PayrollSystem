using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Familiar : IEntity
{
    public int Id { get; set; }
    public int EmpleadoId { get; set; } // Foreign Key

    public string Apellido { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }

    // Parentesco: Podrías usar un int o un Enum (1=Hijo, 2=Cónyuge, etc.)
    public int Parentesco { get; set; }

    // Campos para liquidación de asignaciones
    public bool Discapacidad { get; set; }
    public bool Escolaridad { get; set; }
    public bool ACargo { get; set; }
}