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

    // Nueva ruta: api/vinculosconceptos/convenio/1/cargo/5
    [HttpGet("convenio/{convenioId}/cargo/{cargoId}")]
    public async Task<ActionResult<List<ConceptoDto>>> GetVinculos(int convenioId, int cargoId)
    {
        var conceptos = await _vinculoService.ObtenerConvenioCargoConceptos(convenioId, cargoId);

        // ERROR COMÚN: return conceptos == null ? NotFound() : Ok(conceptos);
        // SOLUCIÓN:
        return Ok(conceptos ?? new List<ConceptoDto>());
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

        await _vinculoService.ActualizarVinculos(request.ConvenioId, request.CargoId, request.ConceptoIds);
        return Ok();
    }

    // Redefinimos el Delete para usar la lógica de tu servicio de vínculos
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _vinculoService.EliminarVinculo(id);
        return eliminado ? Ok() : NotFound();
    }
}

// DTO para el POST (puedes dejarlo aquí o llevarlo a Payroll.Services.DTOs)
public class VinculacionRequest
{
    public int ConvenioId { get; set; }
    public int CargoId { get; set; }
    public List<int> ConceptoIds { get; set; } = new();
}