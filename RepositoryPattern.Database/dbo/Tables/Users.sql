CREATE TABLE [dbo].[Users] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NULL,
    [Email]          NVARCHAR (MAX) NULL,
    [CreateDateTime] DATETIME2 (7)  NULL,
    [Createby]       NVARCHAR (MAX) NULL,
    [DelFlag]        BIT            NULL,
    [UpdateDateTime] DATETIME2 (7)  NULL,
    [Updateby]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO


