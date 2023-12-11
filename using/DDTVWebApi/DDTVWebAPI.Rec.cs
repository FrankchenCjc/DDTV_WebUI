namespace DDTVWebAPI
{
    public partial class DDTVServer
    {

        public class Downloads
        {
            public string Token { get; set; } = string.Empty;
            /// <summary>
            /// 房间号
            /// </summary>
            public string RoomId { get; set; } = string.Empty;
            /// <summary>
            /// 用户UID
            /// </summary>
            public long Uid { set; get; }
            /// <summary>
            /// 昵称
            /// </summary>
            public string Name { get; set; } = string.Empty;
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get; set; } = string.Empty;
            /// <summary>
            /// FLV大小限制使能
            /// </summary>
            public bool FlvSplit { get; set; } = false;
            /// <summary>
            /// FLV切割大小单位为byte
            /// </summary>
            public long FlvSplitSize { set; get; }
            /// <summary>
            /// 是否下载中
            /// </summary>
            public bool IsDownloading { get; set; }
            /// <summary>
            /// 下载地址
            /// </summary>
            public string Url { get; set; } = string.Empty;
            /// <summary>
            /// 下载的完整文件路径
            /// </summary>
            public string FileName { set; get; } = string.Empty;
            /// <summary>
            /// 文件夹路径
            /// </summary>
            public string FilePath { set; get; } = string.Empty;
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime StartTime { set; get; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime EndTime { set; get; }
            public dynamic flvTimes { set; get; } = 0;
            /// <summary>
            /// FLV文件头
            /// </summary>
            public dynamic FlvHeader { set; get; } = string.Empty;
            /// <summary>
            /// FLV头脚本数据
            /// </summary>
            public dynamic FlvScriptTag { set; get; } = string.Empty;
            /// <summary>
            /// WebRequest类的HTTP的实现
            /// </summary>
            public dynamic HttpWebRequest { get; set; } = string.Empty;
            /// <summary>
            /// 当前已下载字节数
            /// </summary>
            public long DownloadCount { get; set; }
            /// <summary>
            /// 该任务下所有任务的总下载字节数
            /// </summary>
            public long TotalDownloadCount { get; set; }
            /// <summary>
            /// 下载状态
            /// </summary>
            public DownloadStatus Status { get; set; } = DownloadStatus.NewTask;
            public enum DownloadStatus
            {
                /// <summary>
                /// 新任务
                /// </summary>
                NewTask,
                /// <summary>
                /// 已准备
                /// </summary>
                Standby,
                /// <summary>
                /// 下载中
                /// </summary>
                Downloading,
                /// <summary>
                /// 下载结束
                /// </summary>
                DownloadComplete,
                /// <summary>
                /// 取消下载
                /// </summary>
                Cancel,
            }
        }

        public class BriefDownloads
        {
            public string Token { get; set; } = string.Empty;
            /// <summary>
            /// 房间号
            /// </summary>
            public string RoomId { get; set; } = string.Empty;
            /// <summary>
            /// 用户UID
            /// </summary>
            public long Uid { set; get; }
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get; set; } = string.Empty;
            /// <summary>
            /// 文件夹路径
            /// </summary>
            public string FilePath { set; get; } = string.Empty;
            /// <summary>
            /// 开始时间(秒，Unix时间戳)
            /// </summary>
            public long StartTime { set; get; }
            /// <summary>
            /// 结束时间(秒，Unix时间戳)
            /// </summary>
            public long EndTime { set; get; }
            /// <summary>
            /// 该任务下所有子任务的总下载字节数
            /// </summary>
            public long TotalDownloadCount { get; set; }
        }

        /// <summary>
        /// 获取下载中的任务情况详细情况
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<Downloads>>> GetAllRecordingInfo()
        {
            return await PostAsync<List<Downloads>>("Rec_RecordingInfo", null);
        }

        /// <summary>
        /// 获取下载中的任务情况简略情况
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<BriefDownloads>>> GetBriefRecordingInfo()
        {
            return await PostAsync<List<BriefDownloads>>("Rec_RecordingInfo_Lite", null);

        }

        /// <summary>
        /// 获取已经完成的任务详细情况
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<Downloads>>> GetAllRecordCompleteInfo()
        {
            return await PostAsync<List<Downloads>>("Rec_RecordCompleteInfon", null);
        }

        /// <summary>
        /// 获取已经完成的任务简略情况
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<BriefDownloads>>> GetBriefRecordCompleteInfo()
        {
            return await PostAsync<List<BriefDownloads>>("Rec_RecordCompleteInfon_Lite", null);
        }

        /// <summary>
        /// 取消某个下载任务
        /// </summary>
        /// <param name="UID">要取消的房间UID</param>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<string>> CancelDownload(long UID)
        {
            return await PostAsync<string>("Rec_CancelDownload", new Dictionary<string, string> { { "UID", UID.ToString() } });
        }
    }
}