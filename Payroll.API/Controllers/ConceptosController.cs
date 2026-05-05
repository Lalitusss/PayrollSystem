using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class ConceptosController
    : GenericController<Concepto, ConceptoDto>
{
    public ConceptosController(IConceptoService service)
       : base(service)
    {
    }
}
