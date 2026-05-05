using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;
namespace Payroll.API.Controllers;

public class VinculosConceptosController
    : GenericController<VinculoConcepto, VinculoConceptoDto>
{
    public VinculosConceptosController(IVinculoConceptoService service)
        : base(service)
    {
    }
}
