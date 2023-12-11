namespace DDTVWebAPI
{
    public partial class DDTVServer
    {
        /// <summary>
        /// 设定全局转码
        /// </summary>
        /// <param name="value">设定值</param>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<string>> ConfigTranscod(bool value)
        {
            return await PostAsync<string>("config_transcod", new Dictionary<string, string> { { "state", value.ToString() } });
        }


        /// <summary>
        /// 设定文件切分
        /// </summary>
        /// <param name="value">设定值</param>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<string>> ConfigFilesplit(long value)
        {
            return await PostAsync<string>("config_filesplit", new Dictionary<string, string> { { "size", value.ToString() } });

        }

        /// <summary>
        /// 全局单幕录制
        /// </summary>
        /// <param name="value">设定值</param>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<string>> ConfigDanmuRec(bool value)
        {
            return await PostAsync<string>("config_danmurec", new Dictionary<string, string> { { "state", value.ToString() } });
        }


        /// <summary>
        /// 导入关注列表
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<Follow>>> GetFollow()
        {
            return await PostAsync<List<Follow>>("config_getfollow", null);
        }

        /// <summary>
        /// 关注列表
        /// </summary>
        public class Follow
        {
            public long mid;
            public int roomid;
            public string name = string.Empty;
        }
    }
}