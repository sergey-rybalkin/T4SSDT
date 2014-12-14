CREATE TABLE [dbo].TestResults
(
    BuildId              INT          NOT NULL,
    TestName             VARCHAR(512) NOT NULL,
    Succeeded            BIT          NOT NULL,
    DurationMilliseconds INT          NOT NULL,
    CONSTRAINT PK_TestResults PRIMARY KEY CLUSTERED ( BuildId, TestName ),
    CONSTRAINT FK_Builds_TestResults FOREIGN KEY ( BuildId ) REFERENCES [dbo].Builds ( BuildId )
)