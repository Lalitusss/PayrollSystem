using Payroll.Core.Entities;
using Payroll.Core.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class ProvinciasController
    : GenericController<Provincia,ProvinciaDto>
{
    public ProvinciasController(IProvinciaService service)
        : base(service)
    {
    }
}
