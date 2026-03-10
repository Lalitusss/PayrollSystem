using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class DatoBancario : IEntity
{
    public int Id { get; set; }
    public int PersonaId { get; set; } // Foreign Key

    // Relación con el Banco
    public int BancoId { get; set; }
    public Banco? Banco { get; set; } // Navegación para el .ThenInclude(db => db.Banco)

    public string? CBU { get; set; }
    public string? Alias { get; set; }
    public string? TipoCuenta { get; set; } // Ej: "Caja de Ahorros", "Cuenta Corriente"
    public bool Activo { get; set; } = true;
}