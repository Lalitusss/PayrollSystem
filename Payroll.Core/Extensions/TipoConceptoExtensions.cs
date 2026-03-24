namespace Payroll.Core.Extensions;

using Payroll.Core.Enums;

public static class TipoConceptoExtensions
{
    public static string GetColor(this TipoConcepto tipo) => tipo switch
    {
        TipoConcepto.Informativo => "#6c757d", // Gris: Bases de cálculo/estadística
        TipoConcepto.Remunerativo => "#28a745", // Verde: Ingreso sujeto a aportes
        TipoConcepto.NoRemunerativo => "#17a2b8", // Cian: Ingreso sin aportes
        TipoConcepto.Deduccion => "#dc3545", // Rojo: Descuentos de ley/gremio
        TipoConcepto.RetencionTerceros => "#6f42c1", // Púrpura: Embargos, préstamos
        TipoConcepto.AsignacionFamiliar => "#fd7e14", // Naranja: Beneficio social
        TipoConcepto.Redondeo => "#adb5bd", // Gris claro: Ajuste final
        TipoConcepto.AportePatronal => "#e83e8c", // Rosa: Costo exclusivo empresa
        _ => "#343a40"
    };

    public static int GetOrdenGeneracion(this TipoConcepto tipo)
    {
        return (int)tipo * 10;
    }

    public static string GetFriendlyName(this TipoConcepto tipo) => tipo switch
    {
        TipoConcepto.Informativo => "Dato Informativo",
        TipoConcepto.Remunerativo => "Remunerativo",
        TipoConcepto.NoRemunerativo => "No Remunerativo",
        TipoConcepto.Deduccion => "Deducción de Ley",
        TipoConcepto.RetencionTerceros => "Retención Terceros/Varios",
        TipoConcepto.AsignacionFamiliar => "Asignación Familiar",
        TipoConcepto.Redondeo => "Ajuste de Redondeo",
        TipoConcepto.AportePatronal => "Contribución Patronal",
        _ => tipo.ToString()
    };
}