CREATE TABLE [dbo].[Staff] (
    [ID]             UNIQUEIDENTIFIER           NOT NULL,
    [StaffFirstname] NVARCHAR (50) NOT NULL,
    [StaffSurname]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED ([ID] ASC)
);

