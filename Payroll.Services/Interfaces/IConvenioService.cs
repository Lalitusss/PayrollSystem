using Payroll.Core.DTOs;
using Payroll.Core.Entities;

namespace Payroll.Services.Interfaces;

public interface IConvenioService : IGenericService<Convenio>
{
    Task<IEnumerable<ConvenioDto>> GetConvenios();
}