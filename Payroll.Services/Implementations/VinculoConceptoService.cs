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

    public async Task<List<ConceptoDto>> ObtenerConvenioCargoConceptos(int convenioId, int cargoId)
    {
        // Usamos .AsNoTracking() para mejorar performance en consultas de solo lectura
        var query = _context.VinculosConceptos
            .Include(v => v.Concepto) // Esto es vital para traer la tabla Conceptos
            .Where(v => v.ConvenioId == convenioId && v.Activo);

        // Lógica mixta según tus capturas de SQL
        if (cargoId > 0)
        {
            // Traemos lo específico del cargo seleccionado (ej: 1 o 3) 
            // MAS lo que sea general (CargoId 0 o NULL)
            query = query.Where(v => v.CargoId == cargoId || v.CargoId == 0 || v.CargoId == null);
        }
        else
        {
            query = query.Where(v => v.CargoId == 0 || v.CargoId == null);
        }

        return await query
            .Select(v => new ConceptoDto
            {
                Id = v.ConceptoId,
                Codigo = v.Concepto.Codigo,
                // Accedemos directamente a la propiedad de la tabla vinculada
                Descripcion = v.Concepto.Descripcion,

                // Si el vínculo tiene una fórmula específica (Override), la usamos.
                // Si no, usamos la fórmula que configuraste en la pantalla de "Editar Concepto" (GET_BASICO())
                Formula = !string.IsNullOrEmpty(v.FormulaOverride) ? v.FormulaOverride : v.Concepto.Formula,

                Tipo = v.Concepto.Tipo,

                // IMPORTANTE: En el motor de liquidación, usá el Orden del MAESTRO 
                // para mantener la jerarquía legal (Haberes -> Deducciones)
                Orden = v.Concepto.Orden,

                // Datos adicionales de la tabla Conceptos que veo en tu SQL
                EsPorcentaje = v.Concepto.EsPorcentaje,
                ValorDefecto = v.Concepto.ValorDefecto,
                CargoId = v.CargoId,
                VinculoConceptoId = v.Id
            })
            .OrderBy(c => c.Codigo)
            .ThenBy(c => c.Orden)
            .ToListAsync();
    }

    public async Task ActualizarVinculos(int convenioId, int cargoId, List<int> conceptoIds)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // 1. Buscamos qué conceptos ya están vinculados al CONVENIO (CargoId 0 o null)
            // Solo si estamos intentando actualizar un CARGO específico.
            var idsEnConvenio = new List<int>();
            if (cargoId > 0)
            {
                idsEnConvenio = await _context.VinculosConceptos
                    .Where(v => v.ConvenioId == convenioId && (v.CargoId == 0 || v.CargoId == null))
                    .Select(v => v.ConceptoId)
                    .ToListAsync();
            }

            // 2. Obtenemos los vínculos actuales del nivel donde estamos parados para limpiar
            var actuales = await _context.VinculosConceptos
                .Where(v => v.ConvenioId == convenioId && v.CargoId == cargoId)
                .ToListAsync();

            _context.VinculosConceptos.RemoveRange(actuales);

            // 3. Insertamos los nuevos, pero con el filtro de "No duplicar"
            foreach (var cId in conceptoIds)
            {
                // REGLA: Si estoy en un cargo y el concepto ya existe en el convenio, lo salteo.
                if (cargoId > 0 && idsEnConvenio.Contains(cId))
                {
                    continue;
                }

                await _context.VinculosConceptos.AddAsync(new VinculoConcepto
                {
                    ConceptoId = cId,
                    ConvenioId = convenioId,
                    CargoId = cargoId,
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

}