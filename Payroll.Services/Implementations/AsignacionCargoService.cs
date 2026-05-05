using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class AsignacionCargoService
    : GenericService<AsignacionCargo>, IAsignacionCargoService
{
    public AsignacionCargoService(PayrollDbContext context)
        : base(context)
    {
    }
}