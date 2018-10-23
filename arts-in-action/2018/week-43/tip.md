## macOS/Linux 环境变量设置
* 查看系统存在的 shell：
`$ cat /etc/shells`

```
# List of acceptable shells for chpass(1).
# Ftpd will not allow users to connect who are not using
# one of these shells.

/bin/bash
/bin/csh
/bin/ksh
/bin/sh
/bin/tcsh
/bin/zsh
```
* 查看当前使用的shell：
`$ echo $SHELL`
```
/bin/zsh
```

* macOS 默认的是 `Bourne Shell`(/bin/sh)，其环境变量配置文件及加载顺序如下：
```
/etc/profile
/etc/bashrc
/etc/paths 
~/.bash_profile # macOS
~/.bash_login 
~/.profile 
~/.bashrc # linux
```

其中 `/etc/profile`, `/etc/bashrc` 和 `/etc/paths` 是系统级环境变量，对所有用户都有效。但它们的加载时机有所区别：

* `/etc/profile` 任何用户登录时都会读取该文件
* `/etc/bashrc` bash shell执行时，不管是何种方式，读取此文件
* `/etc/paths` 任何用户登录时都会读取该文件

后面几个是当前用户级的环境变量。macOS 默认用户环境变量配置文件为 `~/.bash_profile`，Linux 为 `~/.bashrc`。

如果不存在 `~/.bash_profile`，则可以自己创建一个 `~/.bash_profile`。

* 如果 `~/.bash_profile` 文件存在，则后面的几个文件就会被忽略
* 如果 `~/.bash_profile` 文件不存在，才会以此类推读取后面的文件
>> 如果使用的是 SHELL 类型是 zsh，则还可能存在对应的 `/etc/zshrc` 和 `~/.zshrc`。任何用户登录 zsh 的时候，都会读取该文件。某个用户登录的时候，会读取其对应的 `~/.zshrc`。

## 添加环境变量
**系统环境变量 `/etc/paths`**
添加系统环境变量，一般不建议直接修改 `/etc/paths` 文件，而是将路径写在 `/etc/paths.d/` 目录下的一个文件里，系统会逐一读取 `/etc/paths.d/` 下的每个文件。

dotnet 路径就是这样实现的。我们先看看 dotnet 的例子：

首先 `/etc/paths` 的文件内容大致如下：

```
$ cat /etc/paths
/usr/local/bin
/usr/bin
/bin
/usr/sbin
/sbin
```
然后查看 `/etc/paths.d/` 目录：

```
$ ls -l /etc/paths.d
-rw-r--r--  1 root  wheel  13 10 26  2016 40-XQuartz
-rw-r--r--  1 root  wheel  24  8  8 15:41 dotnet
-rw-r--r--  1 root  wheel  15  8  8 15:42 dotnet-cli-tools
```

查看 /etc/paths.d/dotnet 文件的内容：

```
$ cat /etc/paths.d/dotnet
/usr/local/share/dotnet
```
`/usr/local/share/dotnet` 就是 dotnet 的可执行文件路径。

所以我们如果要添加一个系统环境变量，也可以参考这种方式。

提供一个添加环境变量的的 shell语句，其中 /usr/local/sbin/mypath 就是我们自己的可执行文件的路径：

```
$ sudo -s 'echo "/usr/local/sbin/mypath" > /etc/paths.d/mypath'
```
但添加完成之后，命令不会立即生效，有两种方法使配置文件生效：

重新登录终端（如果是图形界面，即重新打开 Terminal）
通过 source 命令加载：source /etc/paths
配置生效之后，就可以使用 mypath 命令了。

系统环境变量 /etc/profile 和 /etc/bashrc
注：一般不建议修改这两个文件

添加环境变量的语法为：


`export PATH="$PATH:<PATH 1>:<PATH 2>:<PATH 3>:...:<PATH N>"`

所以在 /etc/profile 和 /etc/bashrc 中添加环境变量，只需要在文件中加入如下代码：


`export PATH="/Users/jh/anaconda/bin:$PATH"`

### 用户环境变量

添加用户环境变量，只需要修改 `~/.bash_profile`（Bourne Shell）或 `~/.zshrc`（zsh）或其他用户级配置文件即可。添加环境变量的语法也是：


export PATH="$PATH:<PATH 1>:<PATH 2>:<PATH 3>:...:<PATH N>"


可以通过 echo $PATH 命令查看当前环境变量


echo $PATH

修改了配置文件之后，依旧需要重新登录 SHELL 或者使用 `source ~/.zshrc` 来是配置立即生效。

### export
还有一种添加环境变量的方法： `export` 命令。

`export` 命令用于设置或显示环境变量。通过 `export` 添加的环境变量仅在此次登录周期内有效。

比如很多时候我们的开发环境和生产环境，就可以通过设置一个临时环境变量来，然后在程序中根据不同的环境变量来设置不同的参数。