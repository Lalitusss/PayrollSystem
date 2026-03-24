namespace Payroll.Engine.Abstractions;

public interface IPayrollContext
{
    // --- Identificación ---
    int TargetId { get; }
    DateTime FechaRef { get; }

    // --- 1. Datos Maestros (Sueldos, Topes) y Variables/Novedades (Horas, Cantidades) ---
    decimal GetParametro(string clave);
    decimal GetVariable(string clave);

    // --- 2. Resultados intermedios ---
    // Permite que un concepto posterior use [1000] o GET_RES('1000')
    void RegistrarResultado(string codigo, decimal importe, decimal @base, decimal cantidad);
    decimal GetResultado(string codigo);

    // --- 3. Acumuladores (Totales en Cascada) ---
    // Ahora usamos string para pasar directamente: PayrollConstants.Functions.GetTotalRem
    decimal GetAcumulado(string clave);

    // Método vital para el PayrollProcessor: va llenando las "bolsas" de totales
    void SumarAcumulado(string clave, decimal importe);
}