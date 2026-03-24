using Mapster;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class CategoriaService : GenericService<Categoria>, ICategoriaService
{
    private readonly PayrollDbContext _context;

    public CategoriaService(PayrollDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> GetByConvenioAsync(int convenioId)
    {
        return await _context.Categorias
            .Where(x => x.ConvenioId == convenioId)
            .Include(x => x.Cargos) // <--- Esto sigue siendo vital para la Columna 2
            .ToListAsync();
    }
}