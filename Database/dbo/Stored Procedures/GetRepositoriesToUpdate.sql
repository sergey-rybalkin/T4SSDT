CREATE PROCEDURE [dbo].[GetRepositoriesToUpdate]
	@ProjectKey CHAR(8) = NULL,
	@BranchName NVARCHAR(100) = NULL
AS 
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON;

    SELECT  RepositoryId,
            r.ProjectKey,
            VersionControlSystem,
			BranchName,
			r.[Status],
            LastUpdated,
			Deploy,
			RunTests,
            Analyze
    FROM    dbo.Repositories r
            INNER JOIN dbo.Projects p ON r.ProjectKey = p.ProjectKey
    WHERE   ( r.Status = 0
	          OR r.Status = 3
              OR ( r.Status = 1
                   AND DATEDIFF(HOUR,r.LastUpdated,GETDATE()) >= 1
                 )
            )
            AND p.Status = 0
			AND ((@ProjectKey IS NULL) OR (r.ProjectKey = @ProjectKey))
			AND ((@BranchName IS NULL) OR (r.BranchName = @BranchName))
END