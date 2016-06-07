CREATE TABLE [dbo].[CaseReviewType]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [TypeName] NVARCHAR(250) NOT NULL,
	[DisplayOrder] INT            NOT NULL DEFAULT 1,
    [IsActive]     BIT            NOT NULL DEFAULT 1
)
