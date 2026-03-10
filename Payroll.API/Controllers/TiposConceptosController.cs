using Payroll.Core.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class TiposConceptosController
    : GenericController<TipoConcepto, TipoConceptoDto>
{
    public TiposConceptosController(ITipoConceptoService service)
        : base(service)
    {
    }
}
