using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Core.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class EmpleadosController
    : GenericController<Empleado, EmpleadoDto>
{
    public EmpleadosController(IEmpleadoService service)
        : base(service)
    {
    }

    [HttpGet("{id}")]
    public override async Task<ActionResult<Empleado>> Get(int id)
    {
        // 1. Usamos el IQueryable del servicio para incluir las tablas relacionadas
        var Empleado = await _service.GetQueryable()
            .Include(p => p.Direccion)
            .Include(p => p.DatoBancario)
            .Include(p => p.Familiar)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (Empleado == null) return NotFound();

        // 2. Mapeamos a nuestro DTO de edición que tiene los IDs y la lista
        var dto = Empleado.Adapt<EmpleadoEdicionDto>();

        return Ok(dto);
    }

    [HttpGet("buscar/{termino}")]
    public async Task<ActionResult<List<PersonalDto>>> BuscarParaAsignacion(string termino)
    {
        // ProjectToType genera el SQL optimizado (SELECT Id, Nombre, Apellido, Cuil...)
        var dtos = await _service.GetQueryable()
            .Where(p => p.Apellido.Contains(termino) ||
                        p.Nombre.Contains(termino) ||
                        p.CUIL.Contains(termino))
            .Take(10)
            .ProjectToType<PersonalDto>() // <--- Magia de Mapster
            .ToListAsync();

        return Ok(dtos);
    }
}
