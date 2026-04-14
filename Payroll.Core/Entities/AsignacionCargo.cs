using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;
public class AsignacionCargo : IEntity
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public int ConvenioId { get; set; }
    public int CargoId { get; set; }
    public DateTime FechaAsignacion { get; set; }
    public bool Activo { get; set; }

    // Propiedades de navegación
    public virtual Persona Persona { get; set; } = null!;
    public virtual Convenio Convenio { get; set; } = null!;
    public virtual Cargo Cargo { get; set; } = null!;
}
