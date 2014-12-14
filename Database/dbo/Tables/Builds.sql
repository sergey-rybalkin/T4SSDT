CREATE TABLE [dbo].Builds
(
    BuildId              INT             IDENTITY(1,1),
    RepositoryId         SMALLINT        NOT NULL,
    BuildDate            DATETIME2(0)    CONSTRAINT [DF_Builds_BuildDate] DEFAULT GETDATE() NOT NULL,
    DurationMilliseconds INT             NOT NULL,
    DuplicatesFound      SMALLINT        CONSTRAINT [DF_Builds_DuplicatesFound] DEFAULT 0 NOT NULL,
    MaintainabilityIndex TINYINT         NOT NULL,
    CyclomaticComplexity SMALLINT        NOT NULL,
    DepthOfInheritance   TINYINT         NOT NULL,
    ClassCoupling        SMALLINT        NOT NULL,
    LinesOfCode          INT             NOT NULL,
    ResultCode           TINYINT         NOT NULL,
    CONSTRAINT PK_Builds PRIMARY KEY CLUSTERED (BuildId), 
    CONSTRAINT FK_Repositories_Builds FOREIGN KEY (RepositoryId)
    REFERENCES [dbo].Repositories(RepositoryId)
)