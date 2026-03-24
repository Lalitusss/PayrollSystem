using NCalc;
using Payroll.Engine.Abstractions;
using Payroll.Engine.Constants;

namespace Payroll.Engine.Core;

public class PayrollEvaluator
{
    public decimal Resolver(string formula, IPayrollContext context)
    {
        if (string.IsNullOrWhiteSpace(formula)) return 0;

        var expression = new Expression(formula);

        // --- 1. Manejo de Parámetros ([1000], [2000], etc.) ---
        expression.EvaluateParameter += (name, args) =>
        {
            var key = name.Replace("[", "").Replace("]", "").ToUpper();

            var res = context.GetResultado(key);
            if (res != 0) { args.Result = res; return; }

            args.Result = context.GetParametro(key);
        };

        // --- 2. Manejo de Funciones (Normalizando nombres de constantes) ---
        expression.EvaluateFunction += (name, args) =>
        {
            var functionName = name.ToUpper();

            // Usamos 'switch case var' para limpiar las constantes al comparar
            switch (functionName)
            {
                case var s when s == Limpiar(PayrollConstants.Functions.GetBasico):
                    args.Result = context.GetParametro("BASICO");
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetAntiguedad):
                    args.Result = context.GetParametro("ANTIGUEDAD");
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetTotalRem):
                    args.Result = context.GetAcumulado(PayrollConstants.Functions.GetTotalRem);
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetTotalNoRem):
                    args.Result = context.GetAcumulado(PayrollConstants.Functions.GetTotalNoRem);
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetTotalDescuentos):
                    args.Result = context.GetAcumulado(PayrollConstants.Functions.GetTotalDescuentos);
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetTopeSipa):
                    args.Result = context.GetParametro("SIPA_TOPE");
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetHijos):
                    args.Result = context.GetVariable("CANT_HIJOS");
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetValorHora):
                    args.Result = context.GetParametro("VALOR_HORA");
                    break;

                // --- Funciones con Parámetros ---
                case var s when s == Limpiar(PayrollConstants.Functions.GetHoras):
                    var tipoHora = args.Parameters[0].Evaluate().ToString();
                    args.Result = context.GetVariable($"HORAS_{tipoHora}");
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetDias):
                    var tipoDia = args.Parameters[0].Evaluate().ToString();
                    args.Result = context.GetVariable($"DIAS_{tipoDia}");
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetNovedad):
                    var novNombre = args.Parameters[0].Evaluate().ToString();
                    args.Result = context.GetVariable(novNombre);
                    break;

                case var s when s == Limpiar(PayrollConstants.Functions.GetResultado):
                    var codigo = args.Parameters[0].Evaluate().ToString();
                    args.Result = context.GetResultado(codigo);
                    break;
            }
        };

        try
        {
            return Convert.ToDecimal(expression.Evaluate());
        }
        catch (Exception ex)
        {
            throw new Exception($"Error en fórmula '{formula}': {ex.Message}");
        }
    }

    // Método auxiliar para que el Switch no sea un infierno de .Replace()
    private string Limpiar(string constante) =>
        constante.Replace("()", "").Replace("([])", "").ToUpper();
}