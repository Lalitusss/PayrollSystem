using Microsoft.EntityFrameworkCore;
using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class ConvenioService
    : GenericService<Convenio>, IConvenioService
{
    public ConvenioService(PayrollDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<ConvenioDto>> GetConvenios()
    {
        return await _context.Convenios
            .AsNoTracking()
            .OrderBy(c => c.Nombre)
            .Select(c => new ConvenioDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Numero = c.Numero,
                Activo = c.Activo
            })
            .ToListAsync();
    }
}