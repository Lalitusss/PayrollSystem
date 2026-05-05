using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class CategoriasController
    : GenericController<Categoria, CategoriaDto>
{
    public CategoriasController(ICategoriaService service)
        : base(service)
    {
    }
}
