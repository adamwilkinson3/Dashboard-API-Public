CREATE TABLE [dbo].[customer]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
	[first_name] NVARCHAR(45),
	[last_name] NVARCHAR(45),
	[email] NVARCHAR(50),
	[active] BIT,
	[create_date] DATETIME NOT NULL,
	[last_update] TIMESTAMP
)
