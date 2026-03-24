using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class ConveniosController
    : GenericController<Convenio,ConvenioDto>
{
    public ConveniosController(IConvenioService service)
        : base(service)
    {
    }
}
