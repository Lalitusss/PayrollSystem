using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Interfaces;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class GenericController<T, TDto> : ControllerBase
    where T : class, IEntity
{
    protected readonly IGenericService<T> _service;

    protected GenericController(IGenericService<T> service)
    {
        _service = service;
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        var list = await _service.GetQueryable()
                                 .ProjectToType<TDto>()
                                 .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TDto>> Get(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        return Ok(entity.Adapt<TDto>());
    }

    [HttpPost]
    public virtual async Task<ActionResult<TDto>> Post(TDto dto)
    {
        var entity = dto.Adapt<T>();
        var created = await _service.CreateAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.Adapt<TDto>());
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put(int id, T entity)
    {
        if (id != entity.Id)
            return BadRequest();

        await _service.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}