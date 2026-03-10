using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class PersonasController
    : GenericController<Persona, PersonaDto>
{
    public PersonasController(IPersonaService service)
        : base(service)
    {
    }

    [HttpGet("{id}")]
    public override async Task<ActionResult<Persona>> Get(int id)
    {
        // 1. Usamos el IQueryable del servicio para incluir las tablas relacionadas
        var persona = await _service.GetQueryable()
            .Include(p => p.Direccion)
            .Include(p => p.DatoBancario)
            .Include(p => p.Familiar)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (persona == null) return NotFound();

        // 2. Mapeamos a nuestro DTO de edición que tiene los IDs y la lista
        var dto = persona.Adapt<PersonaEdicionDto>();

        return Ok(dto);
    }

}
