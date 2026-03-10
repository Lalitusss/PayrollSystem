using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class TipoConceptoService
    : GenericService<TipoConcepto>, ITipoConceptoService
{
    public TipoConceptoService(PayrollDbContext context)
        : base(context)
    {
    }
}