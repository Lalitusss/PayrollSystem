using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class CargoService
    : GenericService<Cargo>, ICargoService
{
    public CargoService(PayrollDbContext context)
        : base(context)
    {
    }
}