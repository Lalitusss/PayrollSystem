using Mapster;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Domain.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class VinculoConceptoService : GenericService<VinculoConcepto>, IVinculoConceptoService
{
    private readonly PayrollDbContext _context;

    public PayrollDbContext Context => _context;

    public VinculoConceptoService(PayrollDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<VinculoConceptoDto>> ObtenerPorEntidad(int entidadId, int tipoEntidad)
    {
        return await _context.VinculosConceptos
            .Include(v => v.Concepto)
            .Where(v => v.EntidadId == entidadId && v.TipoEntidad == tipoEntidad)
            // ProjectToType es de Mapster y es más eficiente que el Select manual
            .ProjectToType<VinculoConceptoDto>()
            .ToListAsync();
    }

    public async Task ActualizarVinculos(int entidadId, int tipoEntidad, List<int> conceptoIds)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var actuales = await _context.VinculosConceptos
                .Where(v => v.EntidadId == entidadId && v.TipoEntidad == tipoEntidad)
                .ToListAsync();

            _context.VinculosConceptos.RemoveRange(actuales);

            foreach (var cId in conceptoIds)
            {
                await _context.VinculosConceptos.AddAsync(new VinculoConcepto
                {
                    ConceptoId = cId,
                    EntidadId = entidadId,
                    TipoEntidad = tipoEntidad,
                    Activo = true
                });
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> EliminarVinculo(int id)
    {
        var entity = await _context.VinculosConceptos.FindAsync(id);
        if (entity == null) return false;

        _context.VinculosConceptos.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<ConceptoSeleccionableDto>> ObtenerMaestro()
    {
        // Traemos los conceptos de la tabla 'Conceptos'
        // y los proyectamos directamente al DTO Seleccionable
        return await _context.Conceptos
            .Where(c => c.Activo) // Opcional: solo los que no estén de baja
            .Select(c => new ConceptoSeleccionableDto
            {
                Id = c.Id,
                Nombre = c.Descripcion,
                TipoConcepto = c.Tipo.ToString(), // O el nombre del Enum/Relación
                Seleccionado = false // Siempre inicia en false para el modal
            })
            .ToListAsync();
    }
    public async Task<List<VinculoConceptoDto>> ObtenerVinculosMezclados(int convenioId, int? cargoId)
    {
        // Buscamos los vínculos e incluimos la entidad 'Concepto'
        var query = _context.VinculosConceptos
            .Include(v => v.Concepto) // <--- ESTO ES CLAVE para el nombre
            .Where(v => (v.TipoEntidad == 1 && v.EntidadId == convenioId));

        if (cargoId.HasValue)
        {
            var queryCargo = _context.VinculosConceptos
                .Include(v => v.Concepto)
                .Where(v => v.TipoEntidad == 3 && v.EntidadId == cargoId.Value);

            // Unimos ambos resultados
            var combined = await query.ToListAsync();
            combined.AddRange(await queryCargo.ToListAsync());

            return combined.Select(v => new VinculoConceptoDto
            {
                Id = v.Id,
                NombreConcepto = v.Concepto?.Descripcion ?? "Sin Nombre",
                Formula = "", // <--- Ya no la enviamos a esta vista
                TipoConcepto = v.Concepto?.Tipo.ToString() ?? "",
                TipoEntidad = v.TipoEntidad,
                EntidadId = v.EntidadId
            }).ToList();
        }

        var listaConvenio = await query.ToListAsync();
        return listaConvenio.Select(v => new VinculoConceptoDto
        {
            Id = v.Id,
            NombreConcepto = v.Concepto?.Descripcion ?? "Sin Nombre",
            Formula = v.Concepto?.Formula ?? "",
            TipoConcepto = v.Concepto?.Tipo.ToString() ?? "",
            TipoEntidad = v.TipoEntidad,
            EntidadId = v.EntidadId
        }).ToList();
    }
}