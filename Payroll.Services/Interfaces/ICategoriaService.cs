using Payroll.Core.Entities;
using Payroll.Services.DTOs;

namespace Payroll.Services.Interfaces
{
    public interface ICategoriaService : IGenericService<Categoria>
    {
        Task<IEnumerable<Categoria>> GetByConvenioAsync(int convenioId);
    }
}
