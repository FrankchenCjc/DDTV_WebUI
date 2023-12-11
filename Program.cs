using System.Net.Http.Json;
using System.Timers;

using DDTV_WebUI;

using DDTVWebAPI;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMasaBlazor();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

GlobalVal.SetGCongfigAsync((await new HttpClient().GetFromJsonAsync<Config>($"{builder.HostEnvironment.BaseAddress}/static/config.json")) ?? new Config());

GlobalVal.SetDDTV_serverAsync();
GlobalVal.Clock.Interval = GlobalVal.GConfig.FlashTimeSpan;
GlobalVal.Clock.Start();

await builder.Build().RunAsync();


public static class GlobalVal
{
    public static bool OnWriting { get; private set; } = false;
    public static Config GConfig { get; private set; } = new();
    public static DDTVServer DDTV_server { get; private set; } = new("http://localhost:11419", "", "");
    public static System.Timers.Timer Clock { get; private set; } = new(1000);
    public static ElapsedEventHandler? TimeEvent { get; set; }

    public static void SetGCongfigAsync(Config Config)
    {
        if (OnWriting) Thread.Sleep(100);
        OnWriting = true;
        GConfig = Config;
        OnWriting = false;
    }
    public static void SetClock()
    {
        if (TimeEvent != null) Clock.Elapsed += TimeEvent;
    }
    public static void SetDDTV_serverAsync(DDTVServer server)
    {
        if (OnWriting) Thread.Sleep(100);
        OnWriting = true;
        DDTV_server = server;
        OnWriting = false;
    }
    public static void SetDDTV_serverAsync()
    {
        SetDDTV_serverAsync(new(GConfig.ServerURL, GConfig.AccessKeyID, GConfig.AccessKeySecret));
    }
}
public class Config
{
    public string ServerURL { get; set; } = "http://localhost:11419";
    public string AccessKeyID { get; set; } = string.Empty;
    public string AccessKeySecret { get; set; } = string.Empty;
    public double FlashTimeSpan { get; set; } = 1000;
    public List<List<string>> Greetings { get; set; } = new();
}


