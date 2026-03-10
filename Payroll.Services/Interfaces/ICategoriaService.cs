using Payroll.Core.Entities;
using Payroll.Services.DTOs;

namespace Payroll.Services.Interfaces
{
    public interface ICategoriaService : IGenericService<Categoria>
    {
        Task ActualizarConceptosAsync(int categoriaId, List<int> conceptosIds);
        Task<CategoriaDto> GetByIdConConceptosAsync(int id);
    }
}
