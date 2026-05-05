using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class BancoService
    : GenericService<Banco>, IBancoService
{
    public BancoService(PayrollDbContext context)
        : base(context)
    {
    }
}
