using Payroll.Engine.Attributes;
using System.ComponentModel;

namespace Payroll.Engine.Constants;

public static class PayrollConstants
{
    public static class Functions
    {
        // --- Variables Principales ---
        [Description("Sueldo Básico"), PayrollGroup("Variables Principales")]
        public const string GetBasico = "GET_BASICO()";

        [Description("Años de Antigüedad"), PayrollGroup("Variables Principales")]
        public const string GetAntiguedad = "GET_ANTIGUEDAD()";

        [Description("Cantidad de Hijos"), PayrollGroup("Variables Principales")]
        public const string GetHijos = "GET_HIJOS()";

        [Description("Tope SIPA (Aportes)"), PayrollGroup("Variables Principales")]
        public const string GetTopeSipa = "GET_SIPA_TOPE()";

        // --- Nuevas de Tiempo y Cantidades ---
        [Description("Horas Trabajadas/Extras"), PayrollGroup("Tiempos y Cantidades")]
        public const string GetHoras = "GET_HORAS()";

        [Description("Días del Mes/Trabajados"), PayrollGroup("Tiempos y Cantidades")]
        public const string GetDias = "GET_DIAS()";

        [Description("Valor Hora Calculado"), PayrollGroup("Tiempos y Cantidades")]
        public const string GetValorHora = "GET_VALOR_HORA()";

        // --- Nuevas de Totales (Acumuladores) ---
        [Description("Total Remunerativo"), PayrollGroup("Totales (Acumuladores)")]
        public const string GetTotalRem = "GET_TOTAL_REM()";

        [Description("Total No Remunerativo"), PayrollGroup("Totales (Acumuladores)")]
        public const string GetTotalNoRem = "GET_TOTAL_NO_REM()";

        [Description("Total Descuentos"), PayrollGroup("Totales (Acumuladores)")]
        public const string GetTotalDescuentos = "GET_TOTAL_DESC()";

        // --- Referencias (Llevan parámetros entre corchetes) ---
        [Description("Valor de Novedad"), PayrollGroup("Referencias")]
        public const string GetNovedad = "GET_NOV([])";

        [Description("Resultado de Concepto"), PayrollGroup("Referencias")]
        public const string GetResultado = "GET_RES([])";
    }
}