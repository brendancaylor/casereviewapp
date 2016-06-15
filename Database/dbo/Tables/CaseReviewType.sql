﻿CREATE TABLE [dbo].[CaseReviewType]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newsequentialid(), 
    [TypeName] NVARCHAR(250) NOT NULL,
	[DisplayOrder] INT            NOT NULL ,
    [IsActive]     BIT            NOT NULL
)
