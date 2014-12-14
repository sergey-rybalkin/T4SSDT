CREATE VIEW [dbo].[HallOfShameReport]
AS
    WITH    TopUsersByIssues
              AS ( SELECT   u.UserId,
                            COUNT(*) AS IssuesCount
                   FROM     dbo.Users u
                            INNER JOIN dbo.IssueOwners o ON o.UserId = u.UserId
                            INNER JOIN dbo.Issues i ON i.IssueId = o.IssueId
                            INNER JOIN dbo.LatestBuilds b ON b.BuildId = i.BuildId
                   GROUP BY u.UserId
                 )
        SELECT  u.UserName,
                u.Created,
                u.GlobalRole,
                i.IssuesCount
        FROM    dbo.Users u
                INNER JOIN TopUsersByIssues i ON i.UserId = u.UserId