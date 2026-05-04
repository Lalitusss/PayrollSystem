using Payroll.Core.Entities;

namespace Payroll.Services.Interfaces
{
    public interface ICategoriaService : IGenericService<Categoria>
    {
        Task<IEnumerable<Categoria>> GetByConvenioAsync(int convenioId);
    }
}
