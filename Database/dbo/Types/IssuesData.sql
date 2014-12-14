CREATE TYPE [dbo].[IssuesData] AS TABLE
(
    LineNumber    INT              NOT NULL,
    TargetFile    VARCHAR(100)     NOT NULL,
    IssueCode     VARCHAR(50)      NOT NULL,
    Message       NVARCHAR(MAX)    NOT NULL,
    ToolCode      TINYINT          NOT NULL,
    Severity      TINYINT          NOT NULL,
    ProjectName   VARCHAR(100)     NULL,
    OwnerEmail    NVARCHAR(256)    NULL
)