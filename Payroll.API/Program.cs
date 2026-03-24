using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Services.Implementations;
using Payroll.Services.Interfaces;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN DE SERVICIOS
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// OpenAPI Nativo de .NET
builder.Services.AddOpenApi();

// 2. CONEXIÓN A BASE DE DATOS
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PayrollDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. REGISTRAR SERVICIOS DE APLICACIÓN
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IConceptoService, ConceptoService>();
builder.Services.AddScoped<IBancoService, BancoService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IConvenioService, ConvenioService>();
builder.Services.AddScoped<IObraSocialService, ObraSocialService>();
builder.Services.AddScoped<IProvinciaService, ProvinciaService>();
builder.Services.AddScoped<IVinculoConceptoService, VinculoConceptoService>();

// 4. CONFIGURACIÓN DE CORS (Antes del build para asegurar disponibilidad)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// 5. CONFIGURACIÓN DEL PIPELINE
// IMPORTANTE: CORS debe ir antes que cualquier mapeo de rutas o auth
app.UseCors("AllowAll");

// Habilitamos OpenAPI y Scalar para todos los entornos (incluido Azure)
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.WithTitle("Payroll API - Sistema de Liquidación")
           .WithTheme(ScalarTheme.Moon)
           .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Redirección de la raíz (/) a Scalar para evitar la pantalla por defecto de Azure
app.MapGet("/", () => Results.Redirect("/scalar/v1"));

// --- BLOQUE DE CALENTAMIENTO ---
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PayrollDbContext>();
    try
    {
        // Forzamos la primera conexión para evitar latencia inicial
        await context.Database.CanConnectAsync();
        Console.WriteLine("--> Base de Datos Caliente y Lista.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> Error al calentar la base de datos: {ex.Message}");
    }
}

app.Run();