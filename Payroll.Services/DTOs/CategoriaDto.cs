using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Services.DTOs;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal SueldoBasico { get; set; }

    // Propiedad para el ID del cargo (útil para combos/filtros)
    public int CargoId { get; set; }

    // Esta propiedad es la clave: Mapster busca "Cargo" + "Nombre" 
    // en la entidad original y lo mapea aquí automáticamente.
    public string CargoNombre { get; set; } = string.Empty;

    public List<ConceptoDto> Conceptos { get; set; } = new();
}
