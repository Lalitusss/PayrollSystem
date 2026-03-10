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
    [HttpGet("{id}")]
    public override async Task<ActionResult<Convenio>> Get(int id)
    {
        // 1. Buscamos el convenio incluyendo sus cargos relacionados
        var convenio = await _service.GetQueryable()
            .Include(c => c.Cargos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (convenio == null)
        {
            return NotFound();
        }

        // 2. Mapeamos a DTO. 
        // Como usamos la convención de nombres, Mapster se encarga del resto.
        return Ok(convenio.Adapt<ConvenioDto>());
    }
}
