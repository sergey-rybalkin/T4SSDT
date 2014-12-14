CREATE TABLE [dbo].Repositories
(
    RepositoryId         SMALLINT         IDENTITY(1,1),
    ProjectKey           CHAR(8)          NOT NULL,
    VersionControlSystem TINYINT          NOT NULL,
    BranchName           NVARCHAR(100)    NOT NULL,
    Status               TINYINT          NOT NULL,
    LastUpdated          DATETIME2(0)     NOT NULL,
    Deploy	             BIT              NOT NULL,
    RunTests             BIT              NOT NULL,
	Analyze              BIT              NOT NULL,
    CONSTRAINT PK_Repositories PRIMARY KEY CLUSTERED (RepositoryId), 
    CONSTRAINT FK_Projects_Repositories FOREIGN KEY (ProjectKey)
    REFERENCES [dbo].Projects(ProjectKey) ON UPDATE CASCADE
)