# DDDD

一个DDTV的web界面

## 特性

- dotnet生态，wasm应用
- 管理和控制直播录制，增删改(目前还不能查)房间
- 不需要dotnet环境，直接部署，简单配置即可使用
- 黑白二色主题
- 可高度自定义的问候语，
- ~~可选鉴权API和WEBAPI两种方式登录~~（目前版本只可使用鉴权API）

## 配置和部署

1. 安装并配置web服务器（例如iis，nginx，apache/tomcat等）
1. 下载本项目的release包
1. 将wwwroot文件夹释放到web服务器的根目录，下或者根目录下某个目录下
1. 将wwwroot目录下index.html中base元素改为实际的web路径
1. 更改服务器配置，将该目录下子目录定向到index.html上
1. 参照[config.json配置详细](config.json.md)更改config.json
1. 启动服务器
