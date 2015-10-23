CREATE TABLE [dbo].[User]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20),
	[Sex] INT NULL,
	[Age] INT, 
    [UserID] NVARCHAR(20) NULL, 
    [Password] VARCHAR(30) NULL, 
    [UserType] INT NULL
)
