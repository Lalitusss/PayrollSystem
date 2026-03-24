using Payroll.Engine.Abstractions;
using Payroll.Engine.Core;

namespace Payroll.Engine.Formulas;

public class FormulaDinamica : IFormula
{
    private readonly PayrollEvaluator _evaluator = new();

    public decimal Calcular(string formula, IPayrollContext context)
    {
        // 1. Validación preventiva: si no hay fórmula, el resultado es 0
        if (string.IsNullOrWhiteSpace(formula))
            return 0m;

        try
        {
            // 2. Invocamos al motor de NCalc a través del Evaluator
            // IMPORTANTE: Asegurate que en PayrollEvaluator el método sea:
            // public decimal Resolver(string formula, IPayrollContext context)
            return _evaluator.Resolver(formula, context);
        }
        catch (Exception ex)
        {
            // 3. Capturamos el error para saber exactamente QUÉ fórmula falló
            // Esto es vital para el debug en producción
            throw new Exception($"[Error en Motor de Cálculo] Fórmula: {formula}. Detalle: {ex.Message}", ex);
        }
    }
}