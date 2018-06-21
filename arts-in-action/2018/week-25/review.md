## 文章：[Why We Moved From NoSQL MongoDB to PostgreSQL](https://dzone.com/articles/why-we-moved-from-nosql-mongodb-to-postgresql)

### 阅读报告

作者首先说自己以前是全栈JS的粉丝（all-in on JS），并且数据库使用的是 MongoDB，一种 schema-less JSON database。  
接着作者说在另一篇文章中反思：全栈是否真的好？有时候是。：）  

#### 一些小问题
* 作者家的数据库采用的是主备结构。随着数据量的增大，MongoDB 开始出现一些问题：检索每个文档（document）超过一秒。  
作者花了很长时间，用了各种手段（也许）都没找到问题所在，一气之下，新建了台服务器作为主服务器，并重建了从服务器。  
整个世界清净了。。（查询时间恢复到150ms），但之前的问题仍然是未解之谜。  
* MongoDB 的数据量达到 4TB，作者为数据库重建索引时，会导致服务不可用。（读者注：可以通过`background: true`参数不锁表？db.collection.ensureIndex({key:1},{background: true})）  
* 还有一次，作者重启了下数据库服务器，然后，MongoDB 花了4小时才启动完成，原因未知。。  

#### 压垮作者的稻草
灵活的模式（schema）既是 MongoDB 的优点，也是缺点。就是说，文档（documents）在同一个集合（collection，可认为是关系型数据库的表）里不需要具有相同的字段或结构，collection 的 documents 中的公共字段的数据类型也可以不一样。总之，MongoDB 没有严格的模式规则。很多开发人员喜欢这种灵活性，但对开发人员的要求和责任也更高。
作者举了个例子：  
假如要在 MongoDB 里存储 git repository 的信息，模式很简单：
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
这样会是代码变得丑陋，而且，作者认为 90% 的 Bug 是因为代码使用 documents 中不存在的字段导致的。这种 bug 还难排除。  
作者公司的小伙伴开始想是否可以给 MongoDB 加上 schema validator 之类的黑魔法，作者认为，这不应该是数据库提供的开箱即用的功能嘛？  
当作者必须在数据库中重要的 collection 的每个 doucument 里都要加某个字段时，作者彻底崩溃了。。因为为了确保每个文档都包含该字段，作者必须逐一检索每一个文档，更新它，然后放回原处。由于集合中有数百万个文档，这个过程导致数据库性能下降到不可接受的程度，作者不得不接受4个多小时的停机时间。

放弃挣扎吧。。作者对自己说：）

#### PostgreSQL 作为救星出现
总之，作者迁移到了 PostgreSql 数据库，作者总结了一些优点：

* Postgres 有一个强类型的模式，留下很少的错误空间。您首先为表创建模式，然后将行添加到表中。您还可以使用规则定义不同表之间的关系，以便您可以将相关数据存储在多个表中并避免数据重复。所有这一切意味着团队中的某个人可以扮演数据库架构师的角色，并控制作为所有其他人遵循的标准的架构。
* 您可以在 PostgreSQL 中更改表格，而无需为每个操作锁定它。例如，您可以添加列并设置值为 NULL，而不锁定整个表。
* Postgres 还支持 JSONB，它允许您创建非结构化数据 - 但是具有数据约束和验证功能以帮助确保 JSON 文档更有意义。Sisense 的人写了一篇很好的博客，详细比较了[Postgres 与 MongoDB 的 JSON 文档](https://www.sisense.com/blog/postgres-vs-mongodb-for-storing-json-data/)。
* 作者的数据库大小减少了10倍，因为 Postgres 更有效地存储信息，并且数据不会在表中有大量冗余。
* 正如之前的研究所显示的那样，作者发现 Postgres 对 indexes 和 joins 的表现要好得多，因此他们的服务变得更快捷。

作者对迁移很满意，现在过着幸福的生活。。

读者注： PostgreSql 确实牛，对 sql 标准支持完善，可集成各种插件，用户函数等。MongoDB 没在工作中使用过，但是这种 NoSQL 类型的数据库也有适用的场景。
