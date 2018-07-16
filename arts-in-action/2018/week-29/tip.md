## 工作中常用的命令

1. 查看本机IP
```
ifconfig | grep "inet " | grep -v 127.0.0.1
```
2. 查看Linux 版本
```
cat /proc/version

uname -a

lsb_release -a
```
3. 查找程序安装的位置
```
whereis mysql # 查看文件的位置
which mysql # 查看可执行文件的位置
find / -name nginx # 在根目录下查找名为 nginx 的文件
find . -name nginx # 在当前目录查找名为 nginx 的文件
find /home -name nginx # 在 home 目录下查找 名为 nginx 的文件
```
4. 查看日志的几种基本操作

* tail [必要参数] [选择参数] [文件]
```
   -f 循环读取
   -q 不显示处理信息
   -v 显示详细的处理信息
   -c<数目> 显示的字节数
   -n<行数> 显示行数
   -q, --quiet, --silent 从不输出给出文件名的首部 
   -s, --sleep-interval=S 与-f合用,表示在每次反复的间隔休眠S秒

   tail  -n  10     test.log   查询日志尾部最后10行的日志;
   tail  -n +10     test.log   查询10行之后的所有日志;
   tail  -fn 10     test.log   循环实时查看最后10行记录(最常用的)

   //一般还会配合着grep用, 例如 :  tail -fn 1000 test.log | grep '关键字'
   如果一次性查询的数据量太大,可以进行翻页查看,
   例如:tail -n 4700  aa.log |more -1000 可以进行多屏显示(ctrl + f 或者 空格键可以快捷键)
```
* head
```
head -n  10  test.log   //查询日志文件中的头10行日志;
head -n -10  test.log   //查询日志文件除了最后10行的其他所有日志;
```
* cat
```
一次显示整个文件 : $ cat filename
从键盘创建一个文件 : $ cat > filename  
将几个文件合并为一个文件： $cat file1 file2 > file //只能创建新文件,不能编辑已有文件.
将一个日志文件的内容追加到另外一个 : $cat -n textfile1 > textfile2
清空一个日志文件 $cat : >textfile2 
ps: > 意思是创建，>> 是追加。
```
* tac
```
tac 则是由最后一行到第一行反向在终端上显示出来
```
5. 查找进程
```
ps -ef | grep nginx  #nginx是进程关键字
```