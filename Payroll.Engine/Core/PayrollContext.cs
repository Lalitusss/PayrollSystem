using Payroll.Engine.Abstractions;
using Payroll.Engine.Constants;

namespace Payroll.Engine.Core;

public class PayrollContext : IPayrollContext
{
    private readonly Dictionary<string, decimal> _maestros;
    private readonly Dictionary<string, decimal> _variables;

    // Resultados individuales por código de concepto (ej: "1000")
    private readonly Dictionary<string, decimal> _resultados = new(StringComparer.OrdinalIgnoreCase);

    // Bolsas para los acumuladores (ej: "GET_TOTAL_REM()")
    private readonly Dictionary<string, decimal> _bolsasDeTotales = new(StringComparer.OrdinalIgnoreCase);

    public PayrollContext(Dictionary<string, decimal>? maestros, Dictionary<string, decimal>? variables)
    {
        _maestros = maestros ?? new(StringComparer.OrdinalIgnoreCase);
        _variables = variables ?? new(StringComparer.OrdinalIgnoreCase);

        // Inicializamos las bolsas con las constantes exactas que vienen de la UI/Fórmulas
        _bolsasDeTotales[PayrollConstants.Functions.GetTotalRem] = 0;
        _bolsasDeTotales[PayrollConstants.Functions.GetTotalNoRem] = 0;
        _bolsasDeTotales[PayrollConstants.Functions.GetTotalDescuentos] = 0;
    }

    // Estos datos suelen venir de la entidad que estás liquidando (Empleado/Legajo)
    public int TargetId { get; set; }
    public DateTime FechaRef { get; set; }

    // --- Métodos de Lectura ---

    public decimal GetParametro(string clave) => _maestros.GetValueOrDefault(clave, 0);

    public decimal GetVariable(string clave) => _variables.GetValueOrDefault(clave, 0);

    public decimal GetResultado(string codigo) => _resultados.GetValueOrDefault(codigo, 0);

    public decimal GetAcumulado(string funcionKey) => _bolsasDeTotales.GetValueOrDefault(funcionKey, 0);

    // --- Métodos de Escritura ---

    public void RegistrarResultado(string codigo, decimal importe, decimal baseCalc, decimal cant)
    {
        // Guardamos el importe final para que otros conceptos lo usen con [CODIGO]
        _resultados[codigo] = importe;

        // TIP: Aquí podrías guardar también baseCalc y cant en otro diccionario 
        // si quisieras recuperarlos después para el recibo.
    }

    public void SumarAcumulado(string funcionKey, decimal importe)
    {
        // Si la key no existe (por si agregás funciones nuevas), la crea.
        if (!_bolsasDeTotales.ContainsKey(funcionKey))
            _bolsasDeTotales[funcionKey] = 0;

        _bolsasDeTotales[funcionKey] += importe;
    }
}