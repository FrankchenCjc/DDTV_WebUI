using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

namespace DDTVWebAPI
{
    public partial class DDTVServer
    {
        readonly SHA1 _sha = SHA1.Create();
        private HttpClient _client = new();
		private HttpRequestMessage _mas = new();
		public string ServerURL { get; private set; } = string.Empty;
        public string AccessKeyID { get; private set; } = string.Empty;
        public string AccessKeySecret { get; private set; } = string.Empty;
        public string Cookies { get; private set; } = string.Empty;
        public bool ApiLogin { get; init; } = false;

        private async Task<Pack<T>> _ApiPostAsync<T>(string ApiCmd, Dictionary<string, string>? Selfval)
        {
            //计算SIG
            var valuePairs = new Dictionary<string, string>{
                    { "accesskeyid", AccessKeyID },
                    { "accesskeysecret", AccessKeySecret },
                    { "cmd", ApiCmd.ToLower()},
                    { "time", (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, 0)).TotalSeconds.ToString()},
                };
            var sig = string.Join(
                string.Empty,
                valuePairs.Select((p) => $"{p.Key}={p.Value};"));
            sig = string.Join(string.Empty,
                _sha.ComputeHash(Encoding.UTF8.GetBytes(sig)).Select((b) => string.Format("{0:x2}", b)))
                .ToUpper();
            //构造from表
            valuePairs.Remove("accesskeysecret");
            valuePairs.Add("sig", sig);
            Selfval?
                .ToList()
                .ForEach((i) => valuePairs.Add(i.Key, i.Value));
            FormUrlEncodedContent from = new(valuePairs);
            //请求并逆序列化
            var jss = 
                await (await _client.PostAsync(valuePairs.GetValueOrDefault("cmd"), from))
                .Content
                .ReadAsStringAsync();
            //回传结果
            return JsonSerializer.Deserialize<Pack<T>>(jss) ?? throw new RequstException("空结果", Code.NotUsed);
		}

        private async Task<Pack<T>> _CookiesPostAsync<T>(string ApiCmd, Dictionary<string, string>? Selfval)
        {
            _mas.RequestUri = new Uri(ApiCmd);
            var jss = 
                await(await _client.SendAsync(_mas))
                .Content
                .ReadAsStringAsync();
           return JsonSerializer.Deserialize<Pack<T>>(jss)?? throw new RequstException ("空结果",Code.NotUsed);
		}

        private async Task<Pack<T>> PostAsync<T>(string ApiCmd, Dictionary<string, string>? Selfval)
        {
            if (ApiLogin)
            {
                if (AccessKeyID == string.Empty || AccessKeySecret == string.Empty) throw new NeedParamException("API连接未指定参数");
                return await _ApiPostAsync<T>(ApiCmd, Selfval);
            }
            else
            {
                if (Cookies == string.Empty) throw new NotLoginException("Cookies连接未登录");
                return await _CookiesPostAsync<T>(ApiCmd, Selfval);
            }
        }

        public async void SetCookies(string name, string paddword)
        {
            if (!ApiLogin)
            {
				Cookies = (await _CookiesPostAsync<string>("login", new Dictionary<string, string> { { "UserName", name }, { "Password", paddword } })).data;
				_mas.Headers.Add("Cookie", Cookies);
			}
			else throw new NotLoginException("非Cookies登录");
        }

		/// <summary>
		/// 使用url，AccessKeyID，和ServerBaseURL，使用API注册到一个服务器
		/// </summary>
		/// <param name="serverurl">这个api不包含/api目录，请包含api目录避免问题</param>
		/// <param name="ID">AccessKeyID或者用户名</param>
		/// <param name="Verif">AccessKeySecret或者密码</param>
		public DDTVServer(string serverurl, string ID, string Verif)
        {
            ServerURL = serverurl;
            AccessKeyID = ID;
            AccessKeySecret = Verif;
            _client.BaseAddress = new Uri(ServerURL);
            ApiLogin = true;
        }
        /// <summary>
        /// 使用WebCookies登录服务器，请在稍候使用SetCookies方法以注册。
        /// </summary>
        public DDTVServer(string serverurl)
        {
            ServerURL = serverurl;
			ApiLogin = false;
		}
    }
}