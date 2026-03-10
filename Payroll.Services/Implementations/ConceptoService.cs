using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class ConceptoService
    : GenericService<Concepto>, IConceptoService
{
    public ConceptoService(PayrollDbContext context)
        : base(context)
    {
    }

    //public override async Task UpdateAsync(Concepto concepto)
    //{
    //    var existing = await _context.Conceptos.FindAsync(concepto.Id);

    //    if (existing != null)
    //    {
    //        _context.Entry(existing).CurrentValues.SetValues(concepto);
    //        await _context.SaveChangesAsync();
    //    }
    //}
}
