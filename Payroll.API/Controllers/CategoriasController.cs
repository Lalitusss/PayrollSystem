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
    public CategoriasController(ICategoriaService service)
        : base(service)
    {
    }

    [HttpGet("{id}/conceptos")]
    public async Task<IActionResult> GetConceptos(int id)
    {
        var service = (ICategoriaService)_service;
        var dto = await service.GetByIdConConceptosAsync(id);

        if (dto == null) return NotFound();

        // Devolvemos la lista 'Conceptos' que llenamos manualmente arriba
        return Ok(dto.Conceptos);
    }

    [HttpPost("{id:int}/conceptos")] // Agregamos :int para mayor claridad
    public async Task<IActionResult> AsignarConceptos([FromRoute] int id, [FromBody] List<int> conceptosIds)
    {
        // Verificamos que la lista no venga nula antes de procesar
        if (conceptosIds == null)
            return BadRequest("La lista de IDs de conceptos es requerida en el cuerpo de la petición.");

        try
        {
            if (_service is ICategoriaService categoriaService)
            {
                await categoriaService.ActualizarConceptosAsync(id, conceptosIds);
                return Ok(new { message = "Conceptos actualizados correctamente" });
            }

            return StatusCode(500, "El servicio no implementa ICategoriaService");
        }
        catch (Exception ex)
        {
            // Esto te va a decir el error real si falla la DB
            return BadRequest($"Error interno: {ex.Message}");
        }
    }
}
