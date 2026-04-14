using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Services.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Payroll.API.Controllers;

public class AsignacionesCargosController
    : GenericController<AsignacionCargo, AsignacionCargoDto>
{
    private readonly IAsignacionCargoService _asignacionService;
    public AsignacionesCargosController(IAsignacionCargoService service)
        : base(service)
    {
        _asignacionService = service;
    }

    [HttpPost("masiva")]
    public async Task<IActionResult> PostMasiva([FromBody] AsignacionMasivaDto dto)
    {
        // Si el DTO llega vacío o mal formado, avisamos
        if (dto == null || !dto.Items.Any())
            return BadRequest("No hay datos para procesar.");

        var resultado = await _asignacionService.EjecutarAsignacionMasivaAsync(dto);

        if (!resultado)
            return StatusCode(500, "Error en el servidor al procesar la asignación.");

        return Ok();
    }

    [HttpGet("convenio/{id}")]
    public async Task<ActionResult<List<AsignacionCargoDto>>> GetByConvenio(int id)
    {
        // 1. Obtenemos la data incluyendo las relaciones para evitar NullReference en Blazor
        var asignaciones = await _asignacionService.GetQueryable()
            .Include(a => a.Persona)
            .Include(a => a.Cargo)
            .Where(a => a.ConvenioId == id && a.Activo)
            .ToListAsync();

        // 2. Mapeamos a DTO (esto resuelve el error CS0029)
        // Si usas Mapster o AutoMapper podés usar .Adapt o .ProjectToType
        var dtos = asignaciones.Select(a => new AsignacionCargoDto
        {
            Id = a.Id,
            PersonaId = a.PersonaId,
            CargoId = a.CargoId,
            ConvenioId = a.ConvenioId,
            // Es vital que el DTO tenga estas propiedades para el diseño limpio
            Persona = a.Persona,
            Cargo = a.Cargo
        }).ToList();

        return Ok(dtos);
    }
}
