using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class ConvenioService
    : GenericService<Convenio>, IConvenioService
{
    public ConvenioService(PayrollDbContext context)
        : base(context)
    {
    }
}