CREATE TABLE [dbo].ModuleCoverage
(
    BuildId                 INT              NOT NULL,
    ModuleName              VARCHAR(100)     NOT NULL,
    BlocksCoveredPercent    DECIMAL(9, 3)    NOT NULL,
    LinesCoveredPercent     DECIMAL(9, 3)    NOT NULL,
    CONSTRAINT PK_ModuleCoverage PRIMARY KEY CLUSTERED (BuildId, ModuleName), 
    CONSTRAINT FK_Builds_ModuleCoverage FOREIGN KEY (BuildId)
    REFERENCES [dbo].Builds(BuildId)
)