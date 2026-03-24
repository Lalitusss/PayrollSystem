using Payroll.Services.DTOs;

namespace Payroll.Services.Interfaces;

public interface IVinculoConceptoService
{
    Task<List<VinculoConceptoDto>> ObtenerPorEntidad(int entidadId, int tipoEntidad);
    Task ActualizarVinculos(int entidadId, int tipoEntidad, List<int> conceptoIds);
    Task<bool> EliminarVinculo(int id);
    Task<List<ConceptoSeleccionableDto>> ObtenerMaestro();
    Task<List<VinculoConceptoDto>> ObtenerVinculosMezclados(int convenioId, int? cargoId);
}