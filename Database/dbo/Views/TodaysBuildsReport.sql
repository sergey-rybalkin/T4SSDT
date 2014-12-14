CREATE VIEW [dbo].[TodaysBuildsReport]
AS
    SELECT  r.ProjectKey,
            r.BranchName,
            CONVERT(VARCHAR(8), b.BuildDate, 108) AS AnalyzedAt,
            b.ResultCode
    FROM    dbo.Builds b
            INNER JOIN dbo.Repositories r ON r.RepositoryId = b.RepositoryId
    WHERE   DATEDIFF(HOUR, b.BuildDate, GETDATE()) < 24