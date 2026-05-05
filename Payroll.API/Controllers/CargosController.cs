using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class CargosController 
    : GenericController<Cargo,CargoDto>
{
    public CargosController(ICargoService service)
        : base(service)
    {
    }
}

