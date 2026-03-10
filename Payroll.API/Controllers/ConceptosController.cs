using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class ConceptosController
    : GenericController<Concepto,ConceptoDto>
{
    public ConceptosController(IConceptoService service)
        : base(service)
    {
    }
    [HttpGet]
    public override async Task<ActionResult<IEnumerable<ConceptoDto>>> GetAll()
    {
        return await _service.GetQueryable()
            .Include(x => x.Tipo) // <--- ESTO es lo que llena la columna vacía de tu foto
            .ProjectToType<ConceptoDto>()
            .ToListAsync();
    }
}
