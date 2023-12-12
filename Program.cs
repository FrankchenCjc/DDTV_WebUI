using System.Net.Http.Json;
using System.Timers;

using BlazorComponent;
using Masa.Blazor;

using DDTV_WebUI;

using DDTVWebAPI;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var js = JsonSerializer.Serialize(new KeyValuePair<string, string>("key-a", "value-b"));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMasaBlazor();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

AppEnv.Config = await TConfig.GetConfigFromWeb(builder.HostEnvironment.BaseAddress + "static/config.json");

AppEnv.Dserver = AppEnv.Config.ServerConfig.ToDServer();
if (!AppEnv.Config.ServerConfig.IsApiLogin) AppEnv.Dserver.SetCookies(AppEnv.Config.ServerConfig.Ver.Key, AppEnv.Config.ServerConfig.Ver.Value);

AppEnv.job();
AppEnv.Clock.Elapsed += (_, _) => AppEnv.job();
AppEnv.Clock.Interval = AppEnv.Config.AppConfig.FlashTimeSpan;
AppEnv.Clock.Start();

await builder.Build().RunAsync();

