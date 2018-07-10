## ln 命令
ln 在 linux 中的作用是为一个文件在另一个位置建立一个链接(link)。当我们在不同的目录，要用到相同的文件时，我们不需要每个目录下都 copy 这个文件，只要在一个目录下放上这个文件，在别的目录下用 ln 命令建立一个链接即可。
ln 创建的链接分为两种： 硬链接（hard link）和软链接（symbolic link）。
软链接：

* 软链接，以路径的形式存在。类似于Windows操作系统中的快捷方式

* 软链接可以跨文件系统 ，硬链接不可以

* 软链接可以对一个不存在的文件名进行链接

* 软链接可以对目录进行链接

硬链接:

* 硬链接，以文件副本的形式存在。但不占用实际空间。类似于指向同一处的指针。

* 不允许给目录创建硬链接

* 硬链接只有在同一个文件系统中才能创建

无论是软链接还是硬链接，ln 命令会保持每一处链接文件的同步性，也就是说，不论你改动了哪一处，其它的文件都会发生相同的变化。

默认情况下， ln 创建的链接是硬链接，若要创建软链接，请使用 -s 参数。

命令参数：

-b 删除，覆盖以前建立的链接

-d 允许超级用户制作目录的硬链接

-f 强制执行

-i 交互模式，文件存在则提示用户是否覆盖

-n 把符号链接视为一般目录

-s 软链接(符号链接)

-v 显示详细的处理过程

## MacOS 开机启动项

### Launchd Daemon
此类型的启动项都由launchd来负责启动，launchd是Mac OS下用于初始化系统环境的关键进程，它是内核装载成功之后在OS环境下启动的第一个进程。采用这种方式来配置自启动项很简单，只需要一个plist文件，该plist文件存在的目录有

```
~/Library/LaunchAgents

/Library/LaunchAgents

/System/Library/LaunchAgents
```

以上三个目录为系统推荐放置的路径，是当登录之后启动的进程

```
~/Library/LaunchDaemons

/Library/LaunchDaemons

/System/Library/LaunchDaemons
```

放置在以上三个目录，则启动为守护进程，为系统启动后立即启动的进程

不同的目录进程启动的权限和优先级是不一样的，你可以通过以下的方式进行设置：

1. 通过`launchctl load xxx.plist`或`launchctl unload xxx.plist`命令添加和删除指定启动项；

2. 直接创建、修改、删除相关目录下面的plist文件。

plist中主要的字段和它的含义

* `Label` 用来在launchd中的一个唯一标识，类似于每一个程序都有一个identifies一样。

* `UserName` 指定运行启动项的用户，只有当Launchd 作为 root 用户运行时，此项才适用。

* `GroupName` 指定运行启动项的组，只有当Launchd 作为 root 用户运行时，此项才适用。

* `KeepAlive` 这个key值是用来控制可执行文件是持续运行呢，还是满足具体条件之后再启动。默认值为false，也就是说满足具体条件之后才启动。当设置值为ture时，表明无条件的开启可执行文件，并使之保持在整个系统运行周期内。

* `RunAtLoad` 标识launchd在加载完该项服务之后立即启动路径指定的可执行文件。默认值为false。

* `Program` 这个值用来指定进程的可执行文件的路径。

* `ProgramArguments` 如果未指定Program时就必须指定该项，包括可执行文件文件和运行的参数。

## 综合实例
安装 redis

`brew install  redis`

可以直接运行 `redis-server` 启动服务, `redis-cli` 执行客户端登录

建立软连接，以便开机启动

`ln -sfv /usr/local/opt/redis/*.plist ~/Library/LaunchAgents`

立即启动

`launchctl load ~/Library/LaunchAgents/homebrew.mxcl.redis.plist`

停止

`launchctl unload ~/Library/LaunchAgents/homebrew.mxcl.redis.plist`
