CREATE TYPE [dbo].[TestResultsData] AS TABLE
(
    TestName             VARCHAR(512)   NOT NULL,
    Succeeded            BIT            NOT NULL,
    DurationMilliseconds INT            NOT NULL
)