using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class AsignacionesCargosController
    : GenericController<AsignacionCargo, AsignacionCargoDto>
{
    public AsignacionesCargosController(IAsignacionCargoService service)
       : base(service)
    {
    }
}
