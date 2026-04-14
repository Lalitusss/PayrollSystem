using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations
{
    public class AsignacionCargoService : GenericService<AsignacionCargo>, IAsignacionCargoService
    {
        private readonly PayrollDbContext _context;

        public AsignacionCargoService(PayrollDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> EjecutarAsignacionMasivaAsync(AsignacionMasivaDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var item in dto.Items)
                {
                    // CAMBIO CLAVE: Agregamos el CargoId a la búsqueda para permitir múltiples cargos
                    var existente = await _context.AsignacionesCargos
                        .FirstOrDefaultAsync(a => a.PersonaId == item.PersonaId
                                               && a.ConvenioId == dto.ConvenioId
                                               && a.CargoId == item.CargoId); // <-- Ahora busca la combinación exacta

                    if (existente != null)
                    {
                        // Si ya tiene ESTE cargo, solo actualizamos la fecha o el estado
                        existente.FechaAsignacion = DateTime.Now;
                        existente.Activo = true;
                        _context.AsignacionesCargos.Update(existente);
                    }
                    else
                    {
                        // Si no tiene ESTE cargo específico, creamos uno nuevo (aunque ya tenga otros)
                        var nueva = new AsignacionCargo
                        {
                            PersonaId = item.PersonaId,
                            ConvenioId = dto.ConvenioId,
                            CargoId = item.CargoId,
                            FechaAsignacion = DateTime.Now,
                            Activo = true
                        };
                        await _context.AsignacionesCargos.AddAsync(nueva);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}