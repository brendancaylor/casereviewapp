﻿CREATE TABLE [dbo].[Section] (
    [ID]           UNIQUEIDENTIFIER            NOT NULL DEFAULT newsequentialid(),
    [SectionName]  NVARCHAR (250) NOT NULL,
    [DisplayOrder] INT            NOT NULL,
    [IsActive]     BIT            NOT NULL,
    CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED ([ID] ASC)
);

