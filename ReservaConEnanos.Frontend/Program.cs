using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ReservaConEnanos.Frontend;
using ReservaConEnanos.Frontend.EscapeRoomProviders.ApiClients;
using ReservaConEnanos.Frontend.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5266") });
builder.Services.AddScoped<BaseHttpService>();
builder.Services.AddScoped<IEscapeRoomProviderApiClient, EscapeRoomProviderApiClient>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
