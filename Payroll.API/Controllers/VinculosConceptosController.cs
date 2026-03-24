using Microsoft.AspNetCore.Mvc;
using Payroll.API.Controllers;
using Payroll.Core.Entities; // Asegúrate que la entidad esté en el namespace correcto
using Payroll.Domain.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.Web.Api.Controllers;
 
public class VinculosConceptosController : GenericController<VinculoConcepto, VinculoConceptoDto>
{
    private readonly IVinculoConceptoService _vinculoService;

    // El constructor debe pasar el servicio a la base y guardarlo localmente
    public VinculosConceptosController(IVinculoConceptoService service)
        : base(service)
    {
        _vinculoService = service;
    }

    // GET: api/vinculos/convenio/1
    [HttpGet("convenio/{id}")]
    public async Task<ActionResult<List<VinculoConceptoDto>>> GetPorConvenio(int id)
    {
        var result = await _vinculoService.ObtenerPorEntidad(id, 1); // 1 = Convenio
        return Ok(result);
    }

    // GET: api/vinculos/cargo/1
    [HttpGet("cargo/{id}")]
    public async Task<ActionResult<List<VinculoConceptoDto>>> GetPorCargo(int id)
    {
        var result = await _vinculoService.ObtenerPorEntidad(id, 3); // 3 = Cargo
        return Ok(result);
    }

    // GET: api/vinculos/maestro
    [HttpGet("maestro")]
    public async Task<ActionResult<List<ConceptoSeleccionableDto>>> GetMaestro()
    {
        var result = await _vinculoService.ObtenerMaestro();
        return Ok(result);
    }

    // POST: api/vinculos/vincular
    [HttpPost("vincular")]
    public async Task<IActionResult> PostVinculos([FromBody] VinculacionRequest request)
    {
        if (request == null || request.ConceptoIds == null)
            return BadRequest("Datos de vinculación inválidos.");

        await _vinculoService.ActualizarVinculos(request.EntidadId, request.TipoEntidad, request.ConceptoIds);
        return Ok();
    }

    // Redefinimos el Delete para usar la lógica de tu servicio de vínculos
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _vinculoService.EliminarVinculo(id);
        return eliminado ? Ok() : NotFound();
    }

    [HttpGet("mezclados/{convenioId}")]
    public async Task<ActionResult<List<VinculoConceptoDto>>> GetMezclados(int convenioId, [FromQuery] int? cargoId = null)
    {
        try
        {
            // Llamamos al service que acabamos de crear/modificar
            var resultados = await _vinculoService.ObtenerVinculosMezclados(convenioId, cargoId);
            return Ok(resultados);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener conceptos: {ex.Message}");
        }
    }
}

// DTO para el POST (puedes dejarlo aquí o llevarlo a Payroll.Services.DTOs)
public class VinculacionRequest
{
    public int EntidadId { get; set; }
    public int TipoEntidad { get; set; }
    public List<int> ConceptoIds { get; set; } = new();
}