using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class VinculoConceptoService
    : GenericService<VinculoConcepto>, IVinculoConceptoService
{
    public VinculoConceptoService(PayrollDbContext context)
        : base(context)
    {
    }
}