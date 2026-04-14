using Payroll.Services.DTOs;

namespace Payroll.Services.Interfaces;

public interface IVinculoConceptoService
{
    Task<List<ConceptoDto>> ObtenerConvenioCargoConceptos(int convenioId, int cargoId);
    Task ActualizarVinculos(int convenioId, int cargoId, List<int> conceptoIds);
    Task<bool> EliminarVinculo(int id);
    Task<List<ConceptoSeleccionableDto>> ObtenerMaestro();
 }