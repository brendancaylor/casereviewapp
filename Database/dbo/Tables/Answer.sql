﻿CREATE TABLE [dbo].[Answer] (
    [ID]                    UNIQUEIDENTIFIER            NOT NULL,
    [CaseReviewWorkSheetID] UNIQUEIDENTIFIER            NOT NULL,
    [QuestionID]            UNIQUEIDENTIFIER            NOT NULL,
    [Comments]              NVARCHAR (MAX) NOT NULL,
    [Compliant] BIT NULL, 
    CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Answer_CaseReviewWorkSheet] FOREIGN KEY ([CaseReviewWorkSheetID]) REFERENCES [dbo].[CaseReviewWorkSheet] ([ID]),
    CONSTRAINT [FK_Answer_Question] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[Question] ([ID])
);

