using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class CargoService
    : GenericService<Cargo>, ICargoService
{
    public CargoService(PayrollDbContext context)
        : base(context)
    {
    }

    public override async Task UpdateAsync(Cargo cargo)
    {
        var existing = await _context.Cargos.FindAsync(cargo.Id);

        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(cargo);
            await _context.SaveChangesAsync();
        }
    }
}
