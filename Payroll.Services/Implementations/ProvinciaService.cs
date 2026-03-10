using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class ProvinciaService
    : GenericService<Provincia>, IProvinciaService
{
    public ProvinciaService(PayrollDbContext context)
        : base(context)
    {
    }
}
