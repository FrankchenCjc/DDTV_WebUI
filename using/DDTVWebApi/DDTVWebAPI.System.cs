using System.Runtime.CompilerServices;

namespace DDTVWebAPI
{
    public partial class DDTVServer
    {
        /// <summary>
        /// 获取系统硬件资源使用情况
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<ResourceData>> GetSystemResourcesInfo()
        {
            return await PostAsync<ResourceData>("System_Resources", null);
        }
        /// <summary>
        /// 系统硬件资源使用情况
        /// </summary>
        [Serializable]
        public class ResourceData
        {
            /// <summary>
            /// 平台
            /// </summary>
            public string? Platform { set; get; }
            /// <summary>
            /// CPU使用率
            /// </summary>
            public double CPU_usage { set; get; }
            /// <summary>
            /// 内存
            /// </summary>
            public MemInfo? Memory { set; get; }
            /// <summary>
            /// 硬盘信息
            /// </summary>
            public List<HDD_Info>? HDDInfo { set; get; }

            public class MemInfo
            {
                /// <summary>
                /// 总计内存大小
                /// </summary>
                public long Total { get; set; }
                /// <summary>
                /// 可用内存大小
                /// </summary>
                public long Available { get; set; }
            }
            public class HDD_Info
            {
                /// <summary>
                /// 注册路径
                /// </summary>
                public string FileSystem { set; get; } = string.Empty;
                /// <summary>
                /// 硬盘大小
                /// </summary>
                public string Size { get; set; } = string.Empty;

                /// <summary>
                /// 已使用大小
                /// </summary>
                public string Used { get; set; } = string.Empty;

                /// <summary>
                /// 可用大小
                /// </summary>
                public string Avail { get; set; } = string.Empty;

                /// <summary>
                /// 使用率
                /// </summary>
                public string Usage { get; set; } = string.Empty;
                /// <summary>
                /// 挂载路径
                /// </summary>
                public string MountPath { set; get; } = string.Empty;
            }
        }

        /// <summary>
        /// 获取系统运行情况
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<SysData>> GetSystemInfo()
        {
            return await PostAsync<SysData>("System_info", null);
        }

        /// <summary>
        /// 系统运行情况
        /// </summary>
        public class SysData
        {
            /// <summary>
            /// 当前DDTV版本号
            /// </summary>
            public string? DDTVCore_Ver { get; set; }
            /// <summary>
            /// 监控房间数量
            /// </summary>
            public int Room_Quantity { get; set; }
            /// <summary>
            /// 设置的服务器名称
            /// </summary>
            public string? ServerName { get; set; }
            /// <summary>
            /// 服务器的唯一资源编号
            /// </summary>
            public string ServerAID { get; set; } = string.Empty;
            /// <summary>
            /// 操作系统相关信息
            /// </summary>
            public OS_Info os_Info { get; set; } = new();
            /// <summary>
            /// 下载任务基础信息
            /// </summary>
            public Download_Info download_Info { get; set; } = new();
            public class OS_Info
            {
                /// <summary>
                /// 系统版本
                /// </summary>
                public string OS_Ver { get; set; } = string.Empty;
                /// <summary>
                /// 系统类型
                /// </summary>
                public string OS_Tpye { get; set; } = string.Empty;
                /// <summary>
                /// 使用内存量，单位bit
                /// </summary>
                public long Memory_Usage { get; set; }
                /// <summary>
                /// 运行时版本
                /// </summary>
                public string Runtime_Ver { get; set; } = string.Empty;
                /// <summary>
                /// 是否在交互模式下
                /// </summary>
                public bool UserInteractive { get; set; }
                /// <summary>
                /// 关联的用户
                /// </summary>
                public string Associated_Users { get; set; } = string.Empty;
                /// <summary>
                /// 工作目录
                /// </summary>
                public string Current_Directory { get; set; } = string.Empty;
                /// <summary>
                /// Core程序核心框架版本
                /// </summary>
                public string AppCore_Ver { set; get; } = string.Empty;
                /// <summary>
                /// Web程序核心框架版本
                /// </summary>
                public string WebCore_Ver { set; get; } = string.Empty;
            }
            public class Download_Info
            {
                /// <summary>
                /// 下载中的任务数
                /// </summary>
                public int Downloading { get; set; }
                /// <summary>
                /// 下载结束的任务数
                /// </summary>
                public int Completed_Downloads { get; set; }
            }

        }

        /// <summary>
        /// 获取系统配置文件信息
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<List<Config>>> GetSysConfig()
        {
            return await PostAsync<List<Config>>("System_Config", null);
        }

        /// <summary>
        /// 系统配置文件信息
        /// </summary>
        public class Config
        {
            /// <summary>
            /// 配置键
            /// </summary>
            public ConfigKey Key { set; get; }
            /// <summary>
            /// 配置键名称
            /// </summary>
            public string KeyName { set; get; } = string.Empty;
            /// <summary>
            /// 配置分组
            /// </summary>
            public ConfigGroup Group { set; get; } = new();
            /// <summary>
            /// 配置值
            /// </summary>
            public string Value { set; get; } = string.Empty;
            /// <summary>
            /// 是否有效
            /// </summary>
            public bool Enabled { set; get; } = false;

            /// <summary>
            /// 配置分组(每个值对应的组是固定的，请勿随意填写)
            /// </summary>
            public enum ConfigGroup
            {
                /// <summary>
                /// 缺省配置组(按道理应该给每个配置都设置组，不应该在缺省组里)
                /// </summary>
                Default,
                /// <summary>
                /// DDTV_Core运行相关的配置
                /// </summary>
                Core,
                /// <summary>
                /// 下载系统运行相关的配置
                /// </summary>
                Download,
                /// <summary>
                /// WEBAPI相关的配置
                /// </summary>
                WEB_API,
                /// <summary>
                /// 播放器相关设置
                /// </summary>
                Play,
                /// <summary>
                /// GUI相关设置
                /// </summary>
                GUI,
            }

            /// <summary>
            /// 配置键
            /// </summary>
            public enum ConfigKey
            {
                /// <summary>
                /// 房间配置文件路径 (应该是一个绝对\相对路径文件地址)
                /// 组：Core      默认值：./RoomListConfig.json
                /// </summary>
                RoomListConfig,
                /// <summary>
                /// 默认下载总文件夹路径 (应该是一个绝对\相对路径目录)
                /// 组：Download  默认值：./Rec/
                /// </summary>
                DownloadPath,
                /// <summary>
                /// 临时文件存放文件夹路径 (应该是一个绝对\相对路径文件地址)
                /// 组：Download  默认值：./tmp/
                /// </summary>
                TmpPath,
                /// <summary>
                /// 默认下载文件夹名字格式 (应该为关键字组合，如:{KEY}_{KEY})
                /// 组：Download  默认值：{ROOMID}_{NAME}        可选值：ROOMID|NAME|DATE|TIME|TITLE|R
                /// </summary>
                DownloadDirectoryName,
                /// <summary>
                /// 默认下载文件名格式 (应该为关键字组合，如:{KEY}_{KEY})
                /// 组：Download  默认值：{DATE}_{TIME}_{TITLE}  可选值：ROOMID|NAME|DATE|TIME|TITLE|R
                /// </summary>
                DownloadFileName,
                /// <summary>
                /// 转码默认参数 (应该是带{After}{Before}的ffmpeg参数字符串，如:-i {Before} -vcodec copy -acodec copy {After})
                /// 组：Core      默认值：-i {Before} -vcodec copy -acodec copy {After}
                /// </summary>
                TranscodParmetrs,
                /// <summary>
                /// 自动转码 (自动转码的使能配置，为布尔值false或ture)
                /// 组：Core      默认值：false
                /// </summary>
                IsAutoTranscod,
                /// <summary>
                /// 是否启用WEB_API加密证书 (应该为布尔值)
                /// 组：WEB_API   默认值：false
                /// </summary>
                WEB_API_SSL,
                /// <summary>
                /// WEB_API启用HTTPS后调用的pfx证书文件路径 (应该是一个绝对\相对路径文件地址)
                /// 组：WEB_API   默认值：
                /// </summary>
                pfxFileName,
                /// <summary>
                /// WEB_API启用后HTTPS调用的pfx证书秘钥文件路径 (应该是一个绝对\相对路径文件地址)
                /// 组：WEB_API   默认值：
                /// </summary>
                pfxPasswordFileName,
                /// <summary>
                /// 播放器默认音量 (应该是一个uint值)
                /// 组：Play      默认值：50      可选值：0-100
                /// </summary>
                DefaultVolume,
                /// <summary>
                /// GUI首次启动标志位 (应该是一个布尔值第一次启动为真)
                /// 组：Core      默认值：true
                /// </summary>
                GUI_FirstStart,
                /// <summary>
                /// WEB首次启动标志位 (应该是一个布尔值第一次启动为真)
                /// 组：Core      默认值：true
                /// </summary>
                WEB_FirstStart,
                /// <summary>
                /// 录制分辨率 (应该为有限的int值)
                /// 组：Download  默认值：10000  可选值：流畅:80  高清:150  超清:250  蓝光:400  原画:10000
                /// </summary>
                RecQuality,
                /// <summary>
                /// 默认在线观看的分辨率 (应该为有限的int值)
                /// 组：Play      默认值：250    可选值：流畅:80  高清:150  超清:250  蓝光:400  原画:10000
                /// </summary>
                PlayQuality,
                /// <summary>
                /// 全局弹幕录制开关 (布尔值，每个房间自己在房间配置列表单独设置，这个是是否启用弹幕录制功能的总共开关)
                /// 组：Download  默认值：true
                /// </summary>
                IsRecDanmu,
                /// <summary>
                /// 全局礼物录制开关 (布尔值)
                /// 组：Download  默认值：true
                /// </summary>
                IsRecGift,
                /// <summary>
                /// 全局上舰录制开关 (布尔值)
                /// 组：Download  默认值：true
                /// </summary>
                IsRecGuard,
                /// <summary>
                /// 全局SC录制开关 (布尔值)
                /// 组：Download  默认值：true
                /// </summary>
                IsRecSC,
                /// <summary>
                /// 全局FLV文件按大小切分开关 (布尔值)
                /// 组：Download  默认值：false
                /// </summary>
                IsFlvSplit,
                /// <summary>
                /// 当IsFlvSplit为真时使能，FLV文件切分的大小 (应该为long值，切割值应该以byte计算)
                /// 组：Download  默认值：1073741824
                /// </summary>
                FlvSplitSize,
                /// <summary>
                /// WEB登陆使用的用户名 (string)
                /// 组：WEB_API   默认值：ami
                /// </summary>
                WebUserName,
                /// <summary>
                /// WEB登陆使用的密码 (string)
                /// 组：WEB_API   默认值：ddtv
                /// </summary>
                WebPassword,
                /// <summary>
                /// WEBAPI使用的KeyId (string)
                /// 组：WEB_API   默认值：(随机字符串)
                /// </summary>
                AccessKeyId,
                /// <summary>
                /// WEBAPI使用的KeySecret (string)
                /// 组：WEB_API   默认值：(随机字符串)
                /// </summary>
                AccessKeySecret,
                /// <summary>
                /// 用于标记服务器资源ID编号 (string)
                /// 组：WEB_API   默认值：(随机字符串)
                /// </summary>
                ServerAID,
                /// <summary>
                /// 用于标记服务器名称 (string)
                /// 组：WEB_API   默认值：DDTV_Server
                /// </summary>
                ServerName,
                /// <summary>
                /// 客户端唯一标识 (string)
                /// 组：Core      默认值：(随机字符串)
                /// </summary>
                ClientAID,
                /// <summary>
                /// 是否需要初始化
                /// 组：  默认值：
                /// </summary>
                InitializationStatus,
                /// <summary>
                /// DDTVGUI缩放是否隐藏到托盘
                /// 组：GUI       默认值：false
                /// </summary>
                HideIconState,
                /// <summary>
                /// DDTV_WEB跨域设置路径（应为前端网址，必须带协议和端口号，如：http://127.0.0.1:5500）
                /// 组：WEB_API   默认值：*
                /// </summary>
                AccessControlAllowOrigin,
                /// <summary>
                /// DDTV_WEB的Credentials设置 (布尔值)
                /// 组：WEB_API   默认值：true
                /// </summary>
                AccessControlAllowCredentials,

            }
        }


        public async Task<Pack<bool>> IsWebFirstStart()
        {
            return await PostAsync<bool>("System_QueryWebFirstStart", null);
        }

        /// <summary>
        /// 设置初始化状态值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<string>> SetWebFirstStart(bool value)
        {
            return await PostAsync<string>("System_SetWebFirstStart", new Dictionary<string, string> { { "state", value.ToString() } });
        }

        /// <summary>
        /// 用于判断用户登陆状态是否有效
        /// </summary>
        /// <returns>请使用Pack.GetData()取得数据结果，并处理异常</returns>
        public async Task<Pack<bool>> QueryUserState()
        {
            return await PostAsync<bool>("System_QueryUserState", null);
        }

    }
}