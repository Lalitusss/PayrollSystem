using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Payroll.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Usa una sola instancia limpia
//builder.Services.AddScoped(sp => new HttpClient
//{
//     BaseAddress = new Uri("https://payroll-api-servicios.azurewebsites.net/")
//});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7000/") });

await builder.Build().RunAsync();