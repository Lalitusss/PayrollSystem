using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class ConceptoService
    : GenericService<Concepto>, IConceptoService
{
    public ConceptoService(PayrollDbContext context)
        : base(context)
    {
    }
}
