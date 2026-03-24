using Payroll.Engine.Abstractions;

namespace Payroll.Engine.Formulas;

/// <summary>
/// Define el contrato para la ejecución de expresiones de cálculo 
/// dentro del motor de nómina.
/// </summary>
public interface IFormula
{
    /// <summary>
    /// Evalúa una cadena de texto como una fórmula matemática o lógica
    /// utilizando el contexto de liquidación actual.
    /// </summary>
    /// <param name="formula">La expresión a evaluar (ej: "GET_BASICO() * 0.15").</param>
    /// <param name="context">El repositorio de datos maestros, variables y acumuladores.</param>
    /// <returns>El resultado numérico del cálculo.</returns>
    decimal Calcular(string formula, IPayrollContext context);
}