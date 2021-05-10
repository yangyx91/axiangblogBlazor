# axiangblogBlazor
Net 5.0 + Blazor搭建


#--查看SQL版本
select SERVERPROPERTY('Edition') as [edition]

go

#--查看数据库占用
sp_spaceused

#--查看服务器编码 COLLATE
SELECT SERVERPROPERTY('Collation')  

#--查看数据库编码 COLLATE
SELECT DATABASEPROPERTYEX('AXiangDB', 'Collation')

--select * from sys.databases where Name='AXiangDB'

#--创建一个免费层SQL数据库，编码：Chinese_PRC_CI_AS
--CREATE DATABASE AXiangDB COLLATE Chinese_PRC_CI_AS
--(
--	  EDITION = 'Free',
--	  SERVICE_OBJECTIVE='',
--	)

#--连接数据库字符串
--Server=tcp:{your_databaseName}.database.windows.net,1433;Initial Catalog=AXiangDB;Persist Security Info=False;User ID={your_userID};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;