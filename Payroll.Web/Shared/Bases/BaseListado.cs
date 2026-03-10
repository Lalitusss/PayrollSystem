using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace Payroll.Web.Pages.Genericos;

public abstract class BaseListado<T> : ComponentBase where T : class
{
    [Inject] protected HttpClient Http { get; set; } = default!;
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected IJSRuntime JS { get; set; } = default!;

    protected List<T>? Lista;

    // 1. Nombre de la entidad (se usa para la API y para navegar)
    protected virtual string EntityName
    {
        get
        {
            var name = typeof(T).Name.Replace("Dto", "");
            // Pluralización básica
            if (name.EndsWith("d")) return (name + "es").ToLower();
            if (name.EndsWith("a") || name.EndsWith("e") || name.EndsWith("i") || name.EndsWith("o") || name.EndsWith("u"))
                return (name + "s").ToLower();

            return (name + "s").ToLower();
        }
    }

    // 2. URL LIMPIA: Ya no necesita "/lista-optimizada"
    // Porque el GET de 'api/bancos' ahora ya devuelve el DTO por defecto
    protected virtual string ApiUrl => EntityName;

    // La ruta en Blazor (ej: /configuracion/bancos)
    protected abstract string PageUrl { get; }

    protected override async Task OnParametersSetAsync()
    {
        await Cargar();
    }

    protected async Task Cargar()
    {
        try
        {
            // Petición directa: api/bancos, api/personas, etc.
            // Sigue siendo de ~1.0 kB y < 400ms gracias al GenericController
            Lista = await Http.GetFromJsonAsync<List<T>>($"api/{ApiUrl}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en {ApiUrl}: {ex.Message}");
            Lista = new();
        }
    }

    protected async Task Eliminar(int id)
    {
        var result = await JS.InvokeAsync<SweetAlertResult>("Swal.fire", new
        {
            title = "¿Estás seguro?",
            text = "Esta acción eliminará el registro de forma permanente",
            icon = "warning",
            showCancelButton = true,
            confirmButtonColor = "#d33",
            cancelButtonColor = "#6c757d",
            confirmButtonText = "Sí, eliminar",
            cancelButtonText = "Cancelar"
        });

        if (result.IsConfirmed)
        {
            var response = await Http.DeleteAsync($"api/{ApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                await JS.InvokeVoidAsync("Swal.fire", new
                {
                    title = "Eliminado",
                    icon = "success",
                    timer = 1500,
                    showConfirmButton = false,
                    toast = true,
                    position = "top-end"
                });
                await Cargar();
            }
        }
    }

    protected void IrANuevo() => Nav.NavigateTo($"{PageUrl}/nuevo");
    protected void IrAEditar(int id) => Nav.NavigateTo($"{PageUrl}/editar/{id}");

    public class SweetAlertResult
    {
        public bool IsConfirmed { get; set; }
    }
}