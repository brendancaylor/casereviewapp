﻿/*
Deployment script for InContCaseReviews

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "InContCaseReviews"
:setvar DefaultFilePrefix "InContCaseReviews"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014EXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014EXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key f0421778-fb7b-4c2b-ac67-db426b77b202 is skipped, element [dbo].[CaseReviewType].[Id] (SqlSimpleColumn) will not be renamed to ID';


GO
PRINT N'Rename refactoring operation with key e8facbc9-dd3c-4ac0-9bb1-ea46450a1545 is skipped, element [dbo].[SectionCaseReviewType].[Id] (SqlSimpleColumn) will not be renamed to ID';


GO
PRINT N'Dropping unnamed constraint on [dbo].[CaseReviewWorkSheet]...';


GO
ALTER TABLE [dbo].[CaseReviewWorkSheet] DROP CONSTRAINT [DF__CaseReview__Type__300424B4];


GO
PRINT N'Dropping unnamed constraint on [dbo].[CaseReviewWorkSheet]...';


GO
ALTER TABLE [dbo].[CaseReviewWorkSheet] DROP CONSTRAINT [DF__CaseRevie__IsCom__30F848ED];


GO
PRINT N'Dropping [dbo].[FK_Answer_CaseReviewWorkSheet]...';


GO
ALTER TABLE [dbo].[Answer] DROP CONSTRAINT [FK_Answer_CaseReviewWorkSheet];


GO
PRINT N'Dropping [dbo].[FK_CaseReviewWorkSheet_Staff]...';


GO
ALTER TABLE [dbo].[CaseReviewWorkSheet] DROP CONSTRAINT [FK_CaseReviewWorkSheet_Staff];


GO
PRINT N'Starting rebuilding table [dbo].[CaseReviewWorkSheet]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_CaseReviewWorkSheet] (
    [ID]               UNIQUEIDENTIFIER DEFAULT newsequentialid() NOT NULL,
    [StaffID]          UNIQUEIDENTIFIER NOT NULL,
    [CaseReviewTypeID] UNIQUEIDENTIFIER NULL,
    [ClientRef]        NVARCHAR (50)    NOT NULL,
    [Reviewer]         NVARCHAR (50)    NOT NULL,
    [ReviewedDate]     DATETIME         NOT NULL,
    [Type]             INT              DEFAULT 1 NOT NULL,
    [IsCompleted]      BIT              DEFAULT 0 NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_CaseReviewWorkSheet1] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[CaseReviewWorkSheet])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_CaseReviewWorkSheet] ([ID], [StaffID], [ClientRef], [Reviewer], [ReviewedDate], [Type], [IsCompleted])
        SELECT   [ID],
                 [StaffID],
                 [ClientRef],
                 [Reviewer],
                 [ReviewedDate],
                 [Type],
                 [IsCompleted]
        FROM     [dbo].[CaseReviewWorkSheet]
        ORDER BY [ID] ASC;
    END

DROP TABLE [dbo].[CaseReviewWorkSheet];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CaseReviewWorkSheet]', N'CaseReviewWorkSheet';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_CaseReviewWorkSheet1]', N'PK_CaseReviewWorkSheet', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[CaseReviewType]...';


GO
CREATE TABLE [dbo].[CaseReviewType] (
    [ID]           UNIQUEIDENTIFIER NOT NULL,
    [TypeName]     NVARCHAR (250)   NOT NULL,
    [DisplayOrder] INT              NOT NULL,
    [IsActive]     BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[SectionCaseReviewType]...';


GO
CREATE TABLE [dbo].[SectionCaseReviewType] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [SectionID]        UNIQUEIDENTIFIER NOT NULL,
    [CaseReviewTypeID] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[CaseReviewType]...';


GO
ALTER TABLE [dbo].[CaseReviewType]
    ADD DEFAULT newsequentialid() FOR [ID];


GO
PRINT N'Creating unnamed constraint on [dbo].[SectionCaseReviewType]...';


GO
ALTER TABLE [dbo].[SectionCaseReviewType]
    ADD DEFAULT newsequentialid() FOR [ID];


GO
PRINT N'Creating unnamed constraint on [dbo].[Answer]...';


GO
ALTER TABLE [dbo].[Answer]
    ADD DEFAULT newsequentialid() FOR [ID];


GO
PRINT N'Creating unnamed constraint on [dbo].[Question]...';


GO
ALTER TABLE [dbo].[Question]
    ADD DEFAULT newsequentialid() FOR [ID];


GO
PRINT N'Creating unnamed constraint on [dbo].[Section]...';


GO
ALTER TABLE [dbo].[Section]
    ADD DEFAULT newsequentialid() FOR [ID];


GO
PRINT N'Creating unnamed constraint on [dbo].[Staff]...';


GO
ALTER TABLE [dbo].[Staff]
    ADD DEFAULT newsequentialid() FOR [ID];


GO
PRINT N'Creating unnamed constraint on [dbo].[StandardLine]...';


GO
ALTER TABLE [dbo].[StandardLine]
    ADD DEFAULT newsequentialid() FOR [Id];


GO
PRINT N'Creating [dbo].[FK_Answer_CaseReviewWorkSheet]...';


GO
ALTER TABLE [dbo].[Answer] WITH NOCHECK
    ADD CONSTRAINT [FK_Answer_CaseReviewWorkSheet] FOREIGN KEY ([CaseReviewWorkSheetID]) REFERENCES [dbo].[CaseReviewWorkSheet] ([ID]);


GO
PRINT N'Creating [dbo].[FK_CaseReviewWorkSheet_Staff]...';


GO
ALTER TABLE [dbo].[CaseReviewWorkSheet] WITH NOCHECK
    ADD CONSTRAINT [FK_CaseReviewWorkSheet_Staff] FOREIGN KEY ([StaffID]) REFERENCES [dbo].[Staff] ([ID]);


GO
PRINT N'Creating [dbo].[FK_CaseReviewWorkSheet_CaseReviewType]...';


GO
ALTER TABLE [dbo].[CaseReviewWorkSheet] WITH NOCHECK
    ADD CONSTRAINT [FK_CaseReviewWorkSheet_CaseReviewType] FOREIGN KEY ([CaseReviewTypeID]) REFERENCES [dbo].[CaseReviewType] ([ID]);


GO
PRINT N'Creating [dbo].[FK_SectionCaseReviewType_Section]...';


GO
ALTER TABLE [dbo].[SectionCaseReviewType] WITH NOCHECK
    ADD CONSTRAINT [FK_SectionCaseReviewType_Section] FOREIGN KEY ([SectionID]) REFERENCES [dbo].[Section] ([ID]);


GO
PRINT N'Creating [dbo].[FK_SectionCaseReviewType_CaseReviewType]...';


GO
ALTER TABLE [dbo].[SectionCaseReviewType] WITH NOCHECK
    ADD CONSTRAINT [FK_SectionCaseReviewType_CaseReviewType] FOREIGN KEY ([CaseReviewTypeID]) REFERENCES [dbo].[CaseReviewType] ([ID]);


GO
PRINT N'Refreshing [dbo].[vwNonCompliant]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[vwNonCompliant]';


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'f0421778-fb7b-4c2b-ac67-db426b77b202')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('f0421778-fb7b-4c2b-ac67-db426b77b202')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'e8facbc9-dd3c-4ac0-9bb1-ea46450a1545')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('e8facbc9-dd3c-4ac0-9bb1-ea46450a1545')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Answer] WITH CHECK CHECK CONSTRAINT [FK_Answer_CaseReviewWorkSheet];

ALTER TABLE [dbo].[CaseReviewWorkSheet] WITH CHECK CHECK CONSTRAINT [FK_CaseReviewWorkSheet_Staff];

ALTER TABLE [dbo].[CaseReviewWorkSheet] WITH CHECK CHECK CONSTRAINT [FK_CaseReviewWorkSheet_CaseReviewType];

ALTER TABLE [dbo].[SectionCaseReviewType] WITH CHECK CHECK CONSTRAINT [FK_SectionCaseReviewType_Section];

ALTER TABLE [dbo].[SectionCaseReviewType] WITH CHECK CHECK CONSTRAINT [FK_SectionCaseReviewType_CaseReviewType];


GO
PRINT N'Update complete.';


GO
