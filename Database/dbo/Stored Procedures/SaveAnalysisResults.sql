CREATE PROCEDURE [dbo].[SaveAnalysisResults]
    @repositoryId          SMALLINT,
    @durationMilliseconds  INT,
    @duplicatesFound       SMALLINT,
    @maintainabilityIndex  TINYINT,
    @cyclomaticComplexity  SMALLINT,
    @depthOfInheritance    TINYINT,
    @classCoupling         SMALLINT,
    @linesOfCode           INT,
    @resultCode            TINYINT,
    @tests AS [dbo].[TestResultsData] READONLY,
    @coverage AS [dbo].[ModuleCoverageData] READONLY,
    @issues AS [dbo].[IssuesData] READONLY
AS
BEGIN
DECLARE @buildId INT

    INSERT  INTO dbo.Builds
            ( RepositoryId,
              BuildDate,
              DurationMilliseconds,
              DuplicatesFound,
              MaintainabilityIndex,
              CyclomaticComplexity,
              DepthOfInheritance,
              ClassCoupling,
              LinesOfCode,
              ResultCode
            )
    VALUES  ( @repositoryId,
              GETDATE(),
              @durationMilliseconds,
              @duplicatesFound,
              @maintainabilityIndex,
              @cyclomaticComplexity,
              @depthOfInheritance,
              @classCoupling,
              @linesOfCode,
              @resultCode
            )

    SET @buildId = SCOPE_IDENTITY()

    INSERT  INTO dbo.TestResults
            ( BuildId,
              TestName,
              Succeeded,
              DurationMilliseconds
            )
            SELECT  @buildId,
                    TestName,
                    Succeeded,
                    DurationMilliseconds
            FROM    @tests

    INSERT  INTO dbo.ModuleCoverage
            ( BuildId,
              ModuleName,
              BlocksCoveredPercent,
              LinesCoveredPercent
            )
            SELECT  @buildId,
                    ModuleName,
                    BlocksCoveredPercent,
                    LinesCoveredPercent
            FROM    @coverage

    DECLARE @Owners TABLE
    (
        IssueId INT,
        OwnerEmail NVARCHAR(256)
    );

    MERGE INTO dbo.Issues
    USING @issues AS tmp
    ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT ( BuildId,
                 LineNumber,
                 TargetFile,
                 IssueCode,
                 Message,
                 ToolCode,
                 Severity,
                 ProjectName
                )
        VALUES ( @buildId,
                 tmp.LineNumber,
                 tmp.TargetFile,
                 tmp.IssueCode,
                 tmp.Message,
                 tmp.ToolCode,
                 tmp.Severity,
                 tmp.ProjectName
                )
        OUTPUT Inserted.IssueId, tmp.OwnerEmail INTO @Owners;

    INSERT INTO dbo.IssueOwners ( UserId, IssueId )
    SELECT u.UserId, o.IssueId
    FROM @Owners AS o INNER JOIN dbo.Users u ON o.OwnerEmail = u.Email

    RETURN @buildId
END