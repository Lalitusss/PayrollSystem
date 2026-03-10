using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities;

public class CategoriaConcepto 
{
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }

    public int ConceptoId { get; set; }
    public Concepto Concepto { get; set; }
}

