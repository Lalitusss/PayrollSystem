using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Implementations;

namespace Payroll.API.Controllers;

public class CargosController : GenericController<Cargo,CargoDto>
{
    public CargosController(ICargoService service)
        : base(service)
    {
    }
    [HttpGet("{id}")]
    public override async Task<ActionResult<Cargo>> Get(int id)
    {
        // El secreto está en el .Include
        var entidad = await _service.GetQueryable()
            .Include(x => x.Categorias) // <--- ESTO ES LO QUE TE FALTA
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entidad == null) return NotFound();

        // Mapster convertirá la entidad Cargo y su lista de Categorias a DTO
        var dto = entidad.Adapt<CargoDto>();

        return Ok(dto);
    }

}

