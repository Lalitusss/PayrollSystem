using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class ObraSocialService
    : GenericService<ObraSocial>, IObraSocialService
{
    public ObraSocialService(PayrollDbContext context)
        : base(context)
    {
    }
}