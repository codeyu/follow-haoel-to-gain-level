## 文章：[PostgreSQL 9.5: Insert IF not Exists, Update IF Exists (Insert ON CONFLICT option)](https://www.dbrnd.com/2016/08/postgresql-9-5-insert-if-not-exists-update-if-exists-insert-on-conflict-do-update-do-nothing/)

### 阅读笔记

这篇文章主要讲了 PostgreSQL 9.5 引入的新功能：`INSERT ON CONFLICT [DO UPDATE] [DO NOTHING]`。

在此之前，必须使用 upsert 或 merge 语句来执行此类操作。可以看看之前发表的[文章](https://www.dbrnd.com/2015/10/postgresql-insert-update-or-upsert-merge-using-writable-cte/)。

ON CONFLICT 有两种用法：
* INSERT ON CONFLICT DO UPDATE：如果记录匹配，则使用新数据值更新。
* INSERT ON CONFLICT DO NOTHING：如果记录匹配，则跳过。

下面是演示例子：

创建包含示例数据的表：
```sql
CREATE TABLE tbl_Employee
(	
	EmpID INT PRIMARY KEY
	,EmpName CHARACTER VARYING 
);
 
INSERT INTO tbl_Employee 
VALUES (1,'Anvesh'),(2,'Roy'),(3,'Lee')
,(4,'Nivu'),(5,'Rajesh'),(6,'Nupur');
```
使用选项 INSERT ON CONFLICT DO UPDATE 插入两行：
注意：这里用 “Excluded” 表，这个特殊的表包含刚插入行的数据。
```sql
INSERT INTO tbl_Employee 
VALUES (7,'Ramu')
ON CONFLICT (EmpID)
DO UPDATE SET EmpName = Excluded.EmpName;

INSERT INTO tbl_Employet 
VALUES (7,'Mahi')
ON CONFLICT (EmpID)
DO UPDATE SET EmpName = Excluded.EmpName;
```
使用选项 INSERT ON DO NOTHING，尝试插入重复的 EmpID 记录。
使用此选项，如果发生冲突，则不会采取任何操作直接跳过。
```sql
INSERT INTO tbl_Employee 
VALUES (8,'Noor')
ON CONFLICT (EmpID)
DO NOTHING;
 
INSERT INTO tbl_Employee 
VALUES (8,'Noor')
ON CONFLICT (EmpID)
DO NOTHING;
```
检查结果：
```sql
SELECT *FROM tbl_Employee;
 
/*
EmpID   EmpName
-----------------
1       Anvesh
2       Roy
3       Lee
4       Nivu
5       Rajesh
6       Nupur
7       Mahi
8       Noor
*/
```

## 附录：名词解释
* DML（data manipulation language）：
       它们是SELECT、UPDATE、INSERT、DELETE，就象它的名字一样，这4条命令是用来对数据库里的数据进行操作的语言
* DDL（data definition language）：
       DDL比DML要多，主要的命令有CREATE、ALTER、DROP等，DDL主要是用在定义或改变表（TABLE）的结构，数据类型，表之间的链接和约束等初始化工作上，他们大多在建立表时使用
* DCL（Data Control Language）：
       是数据库控制功能。是用来设置或更改数据库用户或角色权限的语句，包括（grant,deny,revoke等）语句。在默认状态下，只有sysadmin,dbcreator,db_owner或db_securityadmin等人员才有权力执行DCL