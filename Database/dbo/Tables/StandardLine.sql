CREATE TABLE [dbo].[StandardLine]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newsequentialid(), 
    [Line] NVARCHAR(MAX) NOT NULL, 
    [LineType] NVARCHAR(200) NOT NULL DEFAULT 'Comment'
)
