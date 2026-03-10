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
        // Estándar para JS
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;

        // Evita enviar basura al Front; si un campo es null, no se incluye en el JSON
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
// REEMPLAZO DE SWAGGER POR EL NUEVO OPENAPI NATIVO
builder.Services.AddOpenApi();

// 2. CONEXIÓN A BASE DE DATOS
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PayrollDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Registrar los servicios de aplicación
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IConceptoService, ConceptoService>();
builder.Services.AddScoped<IBancoService, BancoService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IConvenioService, ConvenioService>();
builder.Services.AddScoped<ITipoConceptoService, TipoConceptoService>();
builder.Services.AddScoped<IObraSocialService, ObraSocialService>();
builder.Services.AddScoped<IProvinciaService, ProvinciaService>();

// 4. CONFIGURACIÓN DE CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// 5. CONFIGURACIÓN DEL PIPELINE
//if (app.Environment.IsDevelopment())
 
    // En .NET 10 usamos MapOpenApi en lugar de UseSwagger
    app.MapOpenApi();

    // AQUÍ ACTIVAMOS SCALAR
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Payroll API - Sistema de Liquidación")
               .WithTheme(ScalarTheme.Moon) // Podés elegir: Midnight, Moon, Solarized, etc.
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
 

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
// --- INSERTAR BLOQUE DE CALENTAMIENTO AQUÍ ---
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PayrollDbContext>();
    try
    {
        // Esto obliga a EF Core a compilar los modelos y abrir la conexión con SQL
        await context.Sistema.AnyAsync();
        Console.WriteLine("--> Base de Datos Caliente y Lista.");
    }
    catch (Exception ex)
    {
        // Si falla, te avisará en la consola (útil para debuggear la tabla nueva)
        Console.WriteLine($"--> Error al calentar la base de datos: {ex.Message}");
    }
}
app.Run();