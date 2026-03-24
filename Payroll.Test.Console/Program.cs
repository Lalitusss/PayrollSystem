using System;
using System.Collections.Generic;
using Payroll.Engine.Abstractions;
using Payroll.Engine.Constants;
using Payroll.Engine.Core;
using Payroll.Engine.Models;

// --- CONFIGURACIÓN DE CONSOLA ---
Console.Title = "Payroll Engine - Test de Consola";
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("==================================================");
Console.WriteLine("   SISTEMA DE LIQUIDACIÓN - PRUEBA DE MOTOR");
Console.WriteLine("==================================================");
Console.WriteLine($"Fecha: {DateTime.Now:dd/MM/yyyy} | Estado: Testing\n");

// 1. MOCK DE DATOS MAESTROS (Ficha del Empleado)
var maestros = new Dictionary<string, decimal>
{
    { "BASICO", 850000m },
    { "ANTIGUEDAD", 12m },
    { "SIPA_TOPE", 1580000m },
    { "VALOR_HORA", 4250m }
};

// 2. MOCK DE NOVEDADES (Movimientos del Mes)
var novedades = new Dictionary<string, decimal>
{
    { "HORAS_50", 10m },
    { "DIAS_FALTA", 0m },
    { "ADELANTO", 50000m }
};

// 3. DEFINICIÓN DE CONCEPTOS (Lo que vendría de SQL)
var conceptos = new List<ConceptoDto>
{
    new() { Codigo = "1000", Orden = 10, Descripcion = "Sueldo Básico", Formula = "GET_BASICO()", Tipo = 2 },
    new() { Codigo = "1010", Orden = 20, Descripcion = "Antigüedad", Formula = "GET_BASICO() * 0.01 * GET_ANTIGUEDAD()", Tipo = 2 },
    new() { Codigo = "1050", Orden = 30, Descripcion = "Horas Extras 50%", Formula = "GET_HORAS('50') * GET_VALOR_HORA() * 1.5", Tipo = 2 },
    new() { Codigo = "4000", Orden = 100, Descripcion = "Jubilación (11%)",
            Formula = "if(GET_TOTAL_REM() > GET_SIPA_TOPE(), GET_SIPA_TOPE() * 0.11, GET_TOTAL_REM() * 0.11)", Tipo = 4 },
    new() { Codigo = "5000", Orden = 200, Descripcion = "Adelanto", Formula = "GET_NOV('ADELANTO')", Tipo = 4 }
};

// 4. INICIALIZACIÓN DEL CONTEXTO Y PROCESADOR
// Asegurate que tu PayrollContext tenga este constructor
var contexto = new PayrollContext(maestros, novedades)
{
    TargetId = 1,
    FechaRef = DateTime.Now
};

var processor = new PayrollProcessor();

// 5. EJECUCIÓN
try
{
    var resultados = processor.Liquidar(conceptos, contexto);

    // 6. RENDERIZADO DE RESULTADOS
    Console.WriteLine($"{"COD",-6} | {"DESCRIPCION",-22} | {"HABERES",12} | {"DESC",10}");
    Console.WriteLine(new string('-', 60));

    foreach (var item in resultados)
    {
        string haber = (item.Tipo == 2) ? item.Importe.ToString("N2") : "";
        string desc = (item.Tipo == 4) ? item.Importe.ToString("N2") : "";
        Console.WriteLine($"{item.Codigo,-6} | {item.Descripcion,-22} | {haber,12} | {desc,10}");
    }

    // 7. TOTALES FINALES
    decimal totalHaberes = contexto.GetAcumulado(PayrollConstants.Functions.GetTotalRem);
    decimal totalDesc = contexto.GetAcumulado(PayrollConstants.Functions.GetTotalDescuentos);

    Console.WriteLine(new string('=', 60));
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"TOTAL NETO A COBRAR:".PadRight(47) + $"$ {(totalHaberes - totalDesc):N2}");
    Console.ResetColor();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n[ERROR FATAL]: {ex.Message}");
    Console.ResetColor();
}

Console.WriteLine("\n\nPresione cualquier tecla para cerrar...");
Console.ReadKey();