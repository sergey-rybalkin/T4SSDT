/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS(SELECT * FROM Users)
BEGIN
	INSERT INTO Users ([UserName], [Email], [HashedPassword], [Created], [GlobalRole])
	VALUES ( N's.rybalkin',
	         N's.rybalkin@itransition.com',
			 0x00B4655671AECC30F091A5EA875694E4157A5D10CD8551554508E6C1576F16EFE8CDC89FD82B7900A541B4EC26C03B15C3C4,
			 GETDATE(),
			 2)

    -- Non-administrative account of the CodeGuard developer
    INSERT INTO Users ([UserName], [Email], [HashedPassword], [Created], [GlobalRole])
	VALUES ( N'sergey.rybalkin',
	         N'rybalkinsp@gmail.com',
			 0x00B4655671AECC30F091A5EA875694E4157A5D10CD8551554508E6C1576F16EFE8CDC89FD82B7900A541B4EC26C03B15C3C4,
			 GETDATE(),
			 0)
END


IF NOT EXISTS(SELECT * FROM Projects)
BEGIN
	DECLARE @ProjectKey CHAR(8)
    DECLARE @CodeGuardDeveloperId SMALLINT
	SET @ProjectKey = 'CODEGRD'
    SELECT @CodeGuardDeveloperId = UserId FROM dbo.Users WHERE Email = 'rybalkinsp@gmail.com'

	INSERT INTO Projects VALUES (@ProjectKey, N'CodeGuard', 0, N'Self-tracking repository')

    INSERT INTO dbo.UsersInProjects
            ( UserId, ProjectId, Role )
    VALUES  ( @CodeGuardDeveloperId, -- UserId - smallint
              @ProjectKey, -- ProjectId - char(8)
              2  -- Role - tinyint
              )

	INSERT INTO dbo.Repositories
	        ( ProjectKey,
	          VersionControlSystem,
			  BranchName,
	          Status,
	          LastUpdated,
              Deploy,
              RunTests,
              Analyze
	        )
	VALUES  ( @ProjectKey, -- ProjectKey - char(8)
	          0, -- VersionControlSystem - tinyint
			  'master', -- BranchName - nvarchar(100)
	          0, -- Status - tinyint
	          GETDATE(), -- LastUpdateDate - datetime2
              1, -- Deploy - bit
              1, -- RunTests - bit
              1  -- Analyze - bit
	        )

	INSERT INTO dbo.Repositories
	        ( ProjectKey,
	          VersionControlSystem,
			  BranchName,
	          Status,
	          LastUpdated,
              Deploy,
              RunTests,
              Analyze
	        )
	VALUES  ( @ProjectKey, -- ProjectKey - char(8)
	          0, -- VersionControlSystem - tinyint
			  'dev', -- BranchName - nvarchar(100)
	          0, -- Status - tinyint
	          GETDATE(), -- LastUpdateDate - datetime2
              1, -- Deploy - bit
              1, -- RunTests - bit
              1  -- Analyze - bit
	        )
END