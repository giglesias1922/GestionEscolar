using Escuela_Front;
using Escuela_Front.Services;
using Escuela_Front.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// 1) Crear HttpClient temporal para leer appsettings
var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

// 2) Leer appsettings.json
var configStream = await http.GetStreamAsync("appsettings.json");

// 3) Agregarlo al IConfiguration de Blazor WASM
builder.Configuration.AddJsonStream(configStream);

// 4) Registrar HttpClient apuntando al BACKEND
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiUrl"]!)
});


builder.Services.AddMudServices();

builder.Services.AddScoped<AsignaturasService>();
builder.Services.AddScoped<EstudiantesService>();
builder.Services.AddScoped<CursosService>();
builder.Services.AddSingleton<AppState>();

await builder.Build().RunAsync();
