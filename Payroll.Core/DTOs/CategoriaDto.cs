using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Core.DTOs;
public class CategoriaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int ConvenioId { get; set; } // FK al convenio

    // Relación: Una Categoría tiene muchos Cargos
    public ICollection<CargoDto> Cargos { get; set; } = new List<CargoDto>();
}
