using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.Engine.Abstractions;
using Payroll.Engine.Constants;
using Payroll.Engine.Models;

namespace Payroll.Engine.Core;

public class PayrollProcessor
{
    // Instanciamos el evaluador que contiene la lógica de NCalc
    private readonly PayrollEvaluator _evaluator = new();

    public List<ResultadoItem> Liquidar(List<ConceptoDto> conceptos, IPayrollContext contexto)
    {
        var resultadosFinales = new List<ResultadoItem>();

        // 1. El ORDEN es sagrado: permite que los acumulados se llenen en cascada
        var conceptosOrdenados = conceptos.OrderBy(c => c.Orden).ToList();

        foreach (var concepto in conceptosOrdenados)
        {
            try
            {
                // 2. Ejecutamos el cálculo llamando al Evaluador
                decimal importeCalculado = _evaluator.Resolver(concepto.Formula, contexto);

                // 3. Registramos el resultado individual (Para el [1000] o GET_RES)
                contexto.RegistrarResultado(
                    concepto.Codigo,
                    importeCalculado,
                    0, // Base (podrías extraerla si la fórmula fuera más compleja)
                    1  // Cantidad (por defecto 1)
                );

                // 4. ACTUALIZAMOS LOS ACUMULADORES (Las "bolsas" de totales)
                ActualizarTotales(contexto, concepto.Tipo, importeCalculado);

                // 5. Mapeo para el retorno a la UI o SQL
                resultadosFinales.Add(new ResultadoItem
                {
                    ConceptoId = concepto.Id,
                    Codigo = concepto.Codigo,
                    Descripcion = concepto.Descripcion,
                    Importe = importeCalculado,
                    Tipo = concepto.Tipo
                });
            }
            catch (Exception ex)
            {
                // Agregamos contexto al error para saber qué concepto falló
                throw new Exception($"Error en Concepto {concepto.Codigo} ({concepto.Descripcion}): {ex.Message}");
            }
        }

        return resultadosFinales;
    }

    private void ActualizarTotales(IPayrollContext contexto, int tipo, decimal importe)
    {
        // Mapeamos según tu lógica: 2=Rem, 3=NoRem, 4=Desc
        // Usamos las constantes exactas para que el Contexto las encuentre
        switch (tipo)
        {
            case 2:
                contexto.SumarAcumulado(PayrollConstants.Functions.GetTotalRem, importe);
                break;
            case 3:
                contexto.SumarAcumulado(PayrollConstants.Functions.GetTotalNoRem, importe);
                break;
            case 4:
                contexto.SumarAcumulado(PayrollConstants.Functions.GetTotalDescuentos, importe);
                break;
        }
    }
}