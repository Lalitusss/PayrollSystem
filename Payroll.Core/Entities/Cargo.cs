using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class Cargo : IEntity
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public int ConvenioId { get; set; }

    public Convenio? Convenio { get; set; }

    public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}