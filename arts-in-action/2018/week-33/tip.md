## [.NET Core Global Tools overview](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools)

需要 .NET Core SDK 的版本 >= 2.1.300
查看当前系统中安装的 .NET Core 运行时：

`dotnet --list-runtimes`

一个 .NET Core 全局工具是一个包含控制台程序的 NuGet 包。

使用以下命令可以安装在默认路径下：

`dotnet tool install -g <package-name> --version <version-number>`

各系统默认目录如下：

| OS          | Path                          |
|-------------|-------------------------------|
| Linux/macOS | `$HOME/.dotnet/tools`         |
| Windows     | `%USERPROFILE%\.dotnet\tools` |

查看本机已经安装的工具：

`dotnet tool list -g`

更新包：
`dotnet tool update -g <packagename>`

卸载包：
`dotnet tool uninstall -g <packagename>`