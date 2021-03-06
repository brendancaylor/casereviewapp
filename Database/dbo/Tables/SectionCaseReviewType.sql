﻿CREATE TABLE [dbo].[SectionCaseReviewType]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newsequentialid(), 
    [SectionID] UNIQUEIDENTIFIER NOT NULL, 
    [CaseReviewTypeID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_SectionCaseReviewType_Section] FOREIGN KEY ([SectionID]) REFERENCES [Section]([ID]),
	CONSTRAINT [FK_SectionCaseReviewType_CaseReviewType] FOREIGN KEY ([CaseReviewTypeID]) REFERENCES [CaseReviewType]([ID])
)
