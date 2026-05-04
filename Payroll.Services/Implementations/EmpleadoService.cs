using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class EmpleadoService
    : GenericService<Empleado>, IEmpleadoService
{
    public EmpleadoService(PayrollDbContext context) 
        : base(context)
    {
    }
}