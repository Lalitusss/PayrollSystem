using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json; // Importante para la comparación

public abstract class BaseFormulario<T> : ComponentBase where T : class, new()
{
    [Parameter] public int Id { get; set; }

    [Inject] protected HttpClient Http { get; set; } = default!;
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected IJSRuntime JS { get; set; } = default!;

    protected T Modelo { get; set; } = new();

    // Aquí guardamos la "foto" inicial del objeto
    private string _estadoInicialJson = "";

    protected abstract string ApiUrl { get; }
    protected abstract string PageUrl { get; }

    protected override async Task OnParametersSetAsync()
    {
        if (Id != 0)
        {
            Modelo = await Http.GetFromJsonAsync<T>($"api/{ApiUrl}/{Id}") ?? new();
        }
        else
        {
            Modelo = new();
        }

        // Serializamos el modelo apenas llega para tener la referencia
        ActualizarEstadoReferencia();
    }

    // Compara el estado actual con el inicial
    protected bool HuboCambios()
    {
        var estadoActual = JsonSerializer.Serialize(Modelo);
        return _estadoInicialJson != estadoActual;
    }

    private void ActualizarEstadoReferencia()
        => _estadoInicialJson = JsonSerializer.Serialize(Modelo);

    protected async Task Cancelar()
    {
        if (HuboCambios())
        {
            var result = await JS.InvokeAsync<SweetAlertResult>("Swal.fire", new
            {
                title = "¿Descartar cambios?",
                text = "Tenés cambios sin guardar. Si salís, se perderán.",
                icon = "warning",
                showCancelButton = true,
                confirmButtonText = "Sí, salir",
                cancelButtonText = "Seguir editando",
                confirmButtonColor = "#d33",
                cancelButtonColor = "#6c757d"
            });

            if (!result.IsConfirmed) return;
        }

        Nav.NavigateTo(PageUrl);
    }

    protected virtual async Task Guardar()
    {
        // Si no tocó nada, avisamos y volvemos sin pegarle a la API
        if (!HuboCambios())
        {
            await JS.InvokeVoidAsync("Swal.fire", new
            {
                title = "Sin cambios",
                text = "No se detectaron modificaciones.",
                icon = "info",
                timer = 1500,
                showConfirmButton = false
            });
            Nav.NavigateTo(PageUrl);
            return;
        }

        HttpResponseMessage response;

        if (Id == 0)
            response = await Http.PostAsJsonAsync($"api/{ApiUrl}", Modelo);
        else
            response = await Http.PutAsJsonAsync($"api/{ApiUrl}/{Id}", Modelo);

        if (response.IsSuccessStatusCode)
        {
            await JS.InvokeVoidAsync("Swal.fire", new
            {
                title = Id == 0 ? "¡Registro Creado!" : "¡Registro Actualizado!",
                text = "Los cambios se guardaron correctamente",
                icon = "success",
                timer = 1800,
                showConfirmButton = false
            });

            await Task.Delay(1000);
            Nav.NavigateTo(PageUrl);
        }
        else
        {
            await JS.InvokeVoidAsync("Swal.fire", "Error", "No se pudo procesar la solicitud", "error");
        }
    }
}

// Clase auxiliar para capturar la respuesta de SweetAlert
public class SweetAlertResult { public bool IsConfirmed { get; set; } }