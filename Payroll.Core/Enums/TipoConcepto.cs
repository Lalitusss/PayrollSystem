namespace Payroll.Core.Enums;

public enum TipoConcepto
{
    Informativo = 1,       // Bases de cálculo (Ej: Base SAC, Topes)
    Remunerativo = 2,      // Conceptos con aportes
    NoRemunerativo = 3,    // Conceptos sin aportes
    Deduccion = 4,         // Retenciones de ley (Jubilación, Obra Social)
    RetencionTerceros = 5, // Cuota sindical, embargos, seguros
    AsignacionFamiliar = 6, // Salario familiar
    Redondeo = 7,          // Ajuste final del neto
    AportePatronal = 8     // Lo que paga la empresa (Cargas sociales)
}

