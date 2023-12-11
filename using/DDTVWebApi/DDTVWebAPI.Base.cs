namespace DDTVWebAPI
{
    public partial class DDTVServer
    {
        /// <summary>
        /// DDTV的传入状态码，参考 https://ddtv.pro/API/
        /// 参考日期2023/4/12
        /// </summary>
        public enum Code
        {
            /// <summary>
            /// 该状态码表示尚未被使用或者赋值，本程序自定
            /// </summary>
            NotUsed = 1,

            Success = 0,
            UIDNotExist = -1,

            WebLoginFail = 6000,
            WebSigFail = 6001,

            ApiSigFail = 6002,

            OpFail = 7000
        }

        /// <summary>
        /// DDTV传入的标准格式
        /// </summary>
        /// 
        [Serializable]
        public class Pack<TData>
        {
            public Code code { get; set; } = 0;
            public string cmd { get; set; } = string.Empty;
            public string message { get; set; } = string.Empty;
            public TData data
            {
                get
                {
                    return code == Code.Success
                        ? _data ?? throw new RequstException("空的值，未完成解析", code)
                        : throw new RequstException(message, code);
				}
                set { _data = value; }
            }
            private TData? _data;
        }
    }
}
