using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class EmpleadosController
    : GenericController<Empleado, EmpleadoDto>
{
    public EmpleadosController(IEmpleadoService service)
        : base(service)
    {
    }
}
