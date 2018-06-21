## 文章：[Why We Moved From NoSQL MongoDB to PostgreSQL](https://dzone.com/articles/why-we-moved-from-nosql-mongodb-to-postgresql)

### 阅读报告

作者首先说自己以前是全栈JS的粉丝（all-in on JS），并且数据库使用的是 MongoDB，一种 schema-less JSON database。  
接着作者说在另一篇文章中反思：全栈是否真的好？有时候是。：）  

#### 一些小问题
* 作者家的数据库采用的是主备结构。随着数据量的增大，MongoDB开始出现一些问题：检索每个文档（document）超过一秒。  
作者花了很长时间，用了各种手段（也许）都没找到问题所在，一气之下，新建了台服务器作为主服务器，并重建了从服务器。  
真个世界清净了。。（查询恢复了到150ms），但之前的问题仍然是未解之谜。  
* MongoDB的数据量达到4TB，作者为数据库重建索引时，会导致服务不可用。（读者注：可以通过`background: true`参数不锁表？db.collection.ensureIndex({key:1},{background: true})）  
* 还有一次，作者重启了下数据库服务器，然后，MongoDB花了4小时才启动完成，原因未知。。  

#### 压垮作者的稻草
灵活的模式（schema）既是MongoDB的优点，也是缺点。就是说，文档（documents）在同一个集合（collection，可认为是关系型数据库的表）里不需要具有相同的字段或结构，collection的documents中的公共字段的数据类型也可以不一样。总之，MongoDB没有严格的模式规则。很多开发人员喜欢这种灵活性，但开发人员的要求和责任也更高。
作者举了个例子：  
假如要在MongoDB里存储Git repository的信息，模式很简单：
|     Field name     | added on    | 
| ------------------ | -------- | 
| provider	| 12/1/2012 |
| repoOrg	| 12/1/2012 |
| repoName	| 12/1/2012 |
| isPrivate	| 7/17/2014 |
| hasTeams	| 2/23/2016 |

正如你所料，随着产品的演化，数据的 schema 可能会不同，有的 documents 中会有 `isPrivate` 和 `hasTeams` 字段，有的却没有。那么，所有需要使用 `repo.hasTeams` 字段的的地方都会充斥下面这样的代码：
```
if exists(repo.hasTeams) and repo.hasTeams === true
{
# do something
} else {
# do something
}
```
