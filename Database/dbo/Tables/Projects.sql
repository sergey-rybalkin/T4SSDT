CREATE TABLE [dbo].Projects
(
    ProjectKey     char(8)          NOT NULL,
    Name           nvarchar(100)    NOT NULL,
    Status         tinyint          NOT NULL,
    Description    nvarchar(max)    CONSTRAINT [DF_Projects_Description] DEFAULT '' NOT NULL,
    CONSTRAINT PK_Projects PRIMARY KEY CLUSTERED (ProjectKey)
)