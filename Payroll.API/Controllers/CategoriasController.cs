using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Implementations;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class CategoriasController
    : GenericController<Categoria,CategoriaDto>
{
    private readonly ICategoriaService _categoriaService;
    public CategoriasController(ICategoriaService service)
        : base(service)
    {
        _categoriaService = service;
    }

    [HttpGet("convenio/{convenioId}")]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetByConvenio(int convenioId)
    {
        // Usamos _categoriaService que SÍ tiene definido GetByConvenioAsync
        var resultado = await _categoriaService.GetByConvenioAsync(convenioId);
        return Ok(resultado);
    }

}
