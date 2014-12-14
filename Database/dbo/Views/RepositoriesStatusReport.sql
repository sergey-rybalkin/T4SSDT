CREATE VIEW [dbo].[RepositoriesStatusReport]
AS
    SELECT  r.ProjectKey,
            r.BranchName,
            b.BuildDate AS LastAnalyzed,
            b.ResultCode
    FROM    dbo.Builds b
            INNER JOIN dbo.LatestBuilds lb ON lb.BuildId = b.BuildId
            INNER JOIN dbo.Repositories r ON r.RepositoryId = b.RepositoryId
