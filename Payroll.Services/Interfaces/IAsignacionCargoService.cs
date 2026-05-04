using Payroll.Core.Entities;
using Payroll.Core.DTOs;
using Payroll.Services.Interfaces;

public interface IAsignacionCargoService : IGenericService<AsignacionCargo>
{
    Task<bool> EjecutarAsignacionMasivaAsync(AsignacionMasivaDto dto);
}