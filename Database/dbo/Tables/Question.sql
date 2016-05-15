﻿CREATE TABLE [dbo].[Question] (
    [ID]           UNIQUEIDENTIFIER            NOT NULL,
    [SectionID]    UNIQUEIDENTIFIER            NOT NULL,
    [QuestionName] NVARCHAR (MAX) NOT NULL,
    [DisplayOrder] INT            NOT NULL,
    [IsActive]     BIT            NOT NULL,
    CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Question_Section] FOREIGN KEY ([SectionID]) REFERENCES [dbo].[Section] ([ID])
);
