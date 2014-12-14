CREATE VIEW [dbo].[ActiveRepositories]
AS
    SELECT  r.*
    FROM    dbo.Repositories r
            INNER JOIN dbo.Projects p ON p.ProjectKey = r.ProjectKey
    WHERE   p.Status = 0
            AND ( r.Status = 0
                  OR r.Status = 3
                  OR ( r.Status = 1
                       AND DATEDIFF(HOUR, r.LastUpdated, GETDATE()) >= 1
                     )
                )
