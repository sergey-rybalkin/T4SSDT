CREATE TABLE [dbo].Issues
(
    IssueId       INT              IDENTITY(1,1),
    BuildId       INT              NOT NULL,
    LineNumber    INT              NOT NULL,
    TargetFile    VARCHAR(100)     NOT NULL,
    IssueCode     VARCHAR(50)      NOT NULL,
    Message       NVARCHAR(MAX)    NOT NULL,
    ToolCode      TINYINT          NOT NULL,
    Severity      TINYINT          NOT NULL,
    ProjectName   VARCHAR(100)     NULL,
    CONSTRAINT PK_Issues PRIMARY KEY CLUSTERED (IssueId), 
    CONSTRAINT FK_Builds_Issues FOREIGN KEY (BuildId)
    REFERENCES [dbo].Builds(BuildId)
)