CREATE TABLE [dbo].[Course]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50),--课程名
	[Describe] NVARCHAR(500),--课程介绍
	[Price] FLOAT,--课程价格
	[Author] NVARCHAR(20),--作者
	[AddUser] INT,--添加者 对应User表ID
	[AddDateTime] DATETIME,--添加时间
	[UpdateDateTime] DATETIME--修改时间
)
