using Payroll.Core.DTOs;
using Payroll.Core.Entities;
using Payroll.Services.Interfaces;

namespace Payroll.API.Controllers;

public class BancosController 
    : GenericController<Banco, BancoDto>
{
    public BancosController(IBancoService service)
        : base(service)
    {
    }
}
