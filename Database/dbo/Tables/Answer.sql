CREATE TABLE [dbo].[Answer] (
    [ID]                    UNIQUEIDENTIFIER            NOT NULL DEFAULT newsequentialid(),
    [CaseReviewWorkSheetID] UNIQUEIDENTIFIER            NOT NULL,
    [QuestionID]            UNIQUEIDENTIFIER            NOT NULL,
    [Comments]              NVARCHAR (MAX) NOT NULL,
    [Compliant] BIT NULL, 
    [Feedback] NVARCHAR(MAX) NULL , 
    [FeedbackType] NVARCHAR(50) NULL , 
    [CamConfirmation] DATETIME NULL , 
    [Advisory] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Answer_CaseReviewWorkSheet] FOREIGN KEY ([CaseReviewWorkSheetID]) REFERENCES [dbo].[CaseReviewWorkSheet] ([ID]),
    CONSTRAINT [FK_Answer_Question] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[Question] ([ID])
);

