## 如何同步 github 上 fork 的 repository？
1. 首先看下你本地的 repository 有没有上游的 repository：
```sh
git remote
```
一般情况下都会显示：
```
origin
upstream
```
2. 如没有 upstream，则添加之：
```
git remote add upstream https://github.com/ORIGINAL_OWNER/ORIGINAL_REPOSITORY.git
```

3. 拉取上游的 repository：
```
git fetch upstream
```

4. 签出本地的 master 分支：
```
git checkout master
```
5. 把从上游 `upstream/master` 分支的修改合并到你本地的 `master` 分支：
```
git merge upstream/master
```

6. 把最新的代码更新到 github：
```
git push
```
