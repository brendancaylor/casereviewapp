CREATE TABLE [dbo].[Staff] (
    [ID]             UNIQUEIDENTIFIER           NOT NULL DEFAULT newsequentialid(),
    [StaffFirstname] NVARCHAR (50) NOT NULL,
    [StaffSurname]   NVARCHAR (50) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [Email] NVARCHAR(256) NOT NULL DEFAULT '', 
    CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED ([ID] ASC)
);

