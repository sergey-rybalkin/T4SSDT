CREATE VIEW [dbo].[LatestBuilds] AS
    WITH BuildStats AS (
    SELECT  ROW_NUMBER() OVER ( PARTITION BY RepositoryId ORDER BY BuildDate DESC ) AS BuildOrder,
            BuildId,
            RepositoryId
    FROM    dbo.Builds)
    SELECT BuildId, RepositoryId FROM BuildStats WHERE BuildStats.BuildOrder = 1