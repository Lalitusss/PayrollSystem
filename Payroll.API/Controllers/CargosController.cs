using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Core.DTOs;
using Payroll.Services.Implementations;

namespace Payroll.API.Controllers;

public class CargosController : GenericController<Cargo,CargoDto>
{
    public CargosController(ICargoService service)
        : base(service)
    {
    }
   

}

