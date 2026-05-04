using Microsoft.AspNetCore.Mvc;
using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class ConveniosController : GenericController<Convenio, ConvenioDto>
{
    private new readonly IConvenioService _service;

    public ConveniosController(IConvenioService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("optimizado")]
    public async Task<ActionResult<List<ConvenioDto>>> GetOptimizado()
    {
        // Llamamos al método del servicio que explicamos antes
        // Este debe devolver solo Id, Nombre y Numero sin incluir colecciones pesadas
        var convenios = await _service.GetConvenios();
        return Ok(convenios);
    }
}