namespace DDTVWebAPI
{
    public partial class DDTVServer
    {
        /// <summary>
        /// 在提示登陆的情况下获取用于的登陆二维码
        /// GETAPI
        /// </summary>
        // public class loginqr { }

        /// <summary>
        /// 重新登陆哔哩哔哩账号
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<string>> LoginReset()
        {
            return await PostAsync<string>("Login_Reset", null);
        }

        /// <summary>
        /// 取得登录状态
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<LoginStates>> LoginState()
        {
            return await PostAsync<LoginStates>("Login_State", null);
        }

        public enum LoginStates
        {
            /// <summary>
            /// 未登录
            /// </summary>
            NotLoggedIn = 0,
            /// <summary>
            /// 已登陆
            /// </summary>
            LoggedIn = 1,
            /// <summary>
            /// 登陆失效
            /// </summary>
            LoginFailure = 2,
            /// <summary>
            /// 登陆中
            /// </summary>
            LoggingIn = 3
        }
    }
}
