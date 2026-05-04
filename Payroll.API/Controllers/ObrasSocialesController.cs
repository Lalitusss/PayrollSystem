using Payroll.Core.Entities;
using Payroll.Core.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class ObrasSocialesController
    : GenericController<ObraSocial,ObraSocialDto>
{
    public ObrasSocialesController(IObraSocialService service)
        : base(service)
    {
    }
}
