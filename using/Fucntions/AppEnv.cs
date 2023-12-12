using System;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Timers;
using System.Web;

using DDTVWebAPI;
namespace DDTV_WebUI;

public static class AppEnv
{
    public static TConfig Config { get; set; } = new();
    public static DDTVServer Dserver { get; set; } = new("http://172.0.0.1:11419");
    public static System.Timers.Timer Clock { get; set; } = new(1000);
    public static List<int> ClockJobs = new() { 0, 0, 0 };
    public static List<DDTVServer.Downloads> Downloads = new();
    public static List<DDTVServer.Downloads> Downloadsc = new();
    public static List<DDTVServer.RoomDetail> RoomDetails = new();
    public static DDTVServer.ResourceData ResourceData = new();
    public static DDTVServer.SysData SysData = new();
    public static Action ChangeEvent = () => { };
    public static Action job = async () =>
    {
        if (!Dserver.ApiLogin)
            if (Dserver.Cookies == string.Empty)
                return;
        ClockJobs = ClockJobs.Select(i => i - 1).ToList();
        if (ClockJobs[0] <= 0)
        {
            ClockJobs[0] = Config.AppConfig.FlashTimer[0];
            var t = (await Dserver.GetSystemResourcesInfo()).data;
            lock (ResourceData) ResourceData = t;
        }
        if (ClockJobs[1] <= 0)
        {
            ClockJobs[1] = Config.AppConfig.FlashTimer[1];
            var ti = (await Dserver.GetAllRecordingInfo()).data;
            var tj = (await Dserver.GetAllRecordCompleteInfo()).data;
            lock (Downloads) Downloads = ti;
            lock (Downloadsc) Downloadsc = tj;
        }
        if (ClockJobs[2] <= 0)
        {
            ClockJobs[2] = Config.AppConfig.FlashTimer[2];
            var i = (await Dserver.GetAllRoomDetail()).data;
            lock (RoomDetails) RoomDetails = i;
        }
        lock (ChangeEvent) ChangeEvent();
    };
}


public class TConfig
{
    public TAppConfig AppConfig { get; set; } = new();
    public TServerConfig ServerConfig { get; set; } = new();

    public static async Task<TConfig> GetConfigFromWeb(string uri)
    {
        return await new HttpClient().GetFromJsonAsync<TConfig>(uri) ?? throw new Exception("未能读取配置");
    }

    public class TAppConfig
    {
        public double FlashTimeSpan { get; set; } = 1000;
        public List<List<string>> Greetings { get; set; } = new() { new() { "你好DD" } };
        public List<int> FlashTimer { get; set; } = new() { 1, 2, 24 };
    }
    public class TServerConfig
    {
        public bool IsApiLogin { get; init; } = false;
        public string ServerURL { get; set; } = "http://localhost:11419";
        public KeyValuePair<string, string> Ver { get; set; } = new();
        public DDTVServer ToDServer()
        {
            if (IsApiLogin)
            {
                return new DDTVServer(ServerURL, Ver.Key, Ver.Value);
            }
            else
            {
                return new DDTVServer(ServerURL);
            }
        }
    }
}
