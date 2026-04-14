using Payroll.Core.Entities;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

public interface IAsignacionCargoService : IGenericService<AsignacionCargo>
{
    Task<bool> EjecutarAsignacionMasivaAsync(AsignacionMasivaDto dto);
}