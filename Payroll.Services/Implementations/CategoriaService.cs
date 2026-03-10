using Mapster;
using Microsoft.EntityFrameworkCore; // <--- ESTA ES LA CLAVE
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class CategoriaService
    : GenericService<Categoria>, ICategoriaService
{
    public CategoriaService(PayrollDbContext context)
        : base(context)
    {
    }

    public async Task<CategoriaDto> GetByIdConConceptosAsync(int id)
    {
        var entidad = await _context.Categorias
            .Include(c => c.CategoriaConceptos)
                .ThenInclude(cc => cc.Concepto)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entidad == null) return null;

        // MAPEADO MANUAL (Única forma segura)
        var dto = new CategoriaDto
        {
            Id = entidad.Id,
            Nombre = entidad.Nombre,
            SueldoBasico = entidad.SueldoBasico,
            CargoId = entidad.CargoId,
            // Aquí convertimos la tabla intermedia en la lista del DTO
            Conceptos = entidad.CategoriaConceptos
                .Select(cc => new ConceptoDto
                {
                    Id = cc.Concepto.Id,
                    Nombre = cc.Concepto.Descripcion,
                    // Agrega aquí TipoNombre o lo que necesites
                }).ToList()
        };

        return dto;
    }
    public async Task ActualizarConceptosAsync(int categoriaId, List<int> conceptosIds)
    {
        // 1. Buscamos los vínculos actuales en la tabla intermedia
        var actuales = await _context.Set<CategoriaConcepto>()
            .Where(cc => cc.CategoriaId == categoriaId)
            .ToListAsync();

        // 2. Limpiamos los vínculos viejos
        _context.Set<CategoriaConcepto>().RemoveRange(actuales);

        // 3. Creamos los nuevos vínculos
        if (conceptosIds != null && conceptosIds.Any())
        {
            var nuevosVínculos = conceptosIds.Select(cId => new CategoriaConcepto
            {
                CategoriaId = categoriaId,
                ConceptoId = cId
            });

            await _context.Set<CategoriaConcepto>().AddRangeAsync(nuevosVínculos);
        }

        // 4. Guardamos todo en una sola transacción
        await _context.SaveChangesAsync();
    }
}