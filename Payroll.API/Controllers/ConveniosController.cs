using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class ConveniosController
    : GenericController<Convenio, ConvenioDto>
{
    public ConveniosController(IConvenioService service)
        : base(service)
    {
    }
}