namespace DDTVWebAPI
{
    public partial class DDTVServer
    {
        /// <summary>
        /// 获取已录制的文件列表  
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<string>>> GetAllFileList()
        {
            return await PostAsync<List<string>>("File_GetAllFileList", null);
        }

        /// <summary>
        /// 根据文件树结构返回已录制的文件总列表
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<FileNames>>> GetFilePathList()
        {
            return await PostAsync<List<FileNames>>("File_GetFilePathList", null);
        }

        /// <summary>
        /// 描述文件的类
        /// </summary>
        public class FileNames
        {
            /// <summary>
            /// 文件名
            /// </summary>
            public string Name { get; set; } = string.Empty;
            /// <summary>
            /// 文件类型
            /// </summary>
            public string FileType { get; set; } = string.Empty;
            /// <summary>
            /// 文件大小(如果类型是文件夹则为0)
            /// </summary>
            public long Size { get; set; }
            /// <summary>
            /// 文件创建时间
            /// </summary>
            public DateTime DateTime { get; set; }
            /// <summary>
            /// 子文件夹
            /// </summary>
            public List<FileNames> children { get; set; } = new();
        }

        /// <summary>
        /// 分类获取已录制的文件总列表
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<FileList>>> GetTypeFileList()
        {
            return await PostAsync<List<FileList>>("File_GetTypeFileList", null);
        }
        /// <summary>
        /// 分类获取已录制的文件总列表
        /// </summary>
        public class FileList
        {
            public string Type { set; get; } = string.Empty;
            public List<string> files = new List<string>();
        }

    }
}
