﻿CREATE TABLE [dbo].[CaseReviewWorkSheet] (
    [ID]           UNIQUEIDENTIFIER           NOT NULL DEFAULT newsequentialid(),
    [StaffID]      UNIQUEIDENTIFIER           NOT NULL,
    [CaseReviewTypeID] UNIQUEIDENTIFIER NULL , 
	[ClientRef]    NVARCHAR(50)           NOT NULL,
    [Reviewer]     NVARCHAR (50) NOT NULL,
    [ReviewedDate] DATETIME      NOT NULL,
    [Type] INT NOT NULL DEFAULT 1, 
    [IsCompleted] BIT NOT NULL DEFAULT 0, 
    
    CONSTRAINT [PK_CaseReviewWorkSheet] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CaseReviewWorkSheet_Staff] FOREIGN KEY ([StaffID]) REFERENCES [dbo].[Staff] ([ID]),
	CONSTRAINT [FK_CaseReviewWorkSheet_CaseReviewType] FOREIGN KEY ([CaseReviewTypeID]) REFERENCES [dbo].[CaseReviewType] ([ID])

);

