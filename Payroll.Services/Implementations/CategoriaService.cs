using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class CategoriaService
    : GenericService<Categoria>, ICategoriaService
{
    public CategoriaService(PayrollDbContext context)
        : base(context)
    {
    }
}
