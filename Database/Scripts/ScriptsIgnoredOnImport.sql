
IF OBJECT_ID('BuildConfigurations') IS NOT NULL
    PRINT '<<< CREATED TABLE BuildConfigurations >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE BuildConfigurations >>>'
GO

IF OBJECT_ID('Projects') IS NOT NULL
    PRINT '<<< CREATED TABLE Projects >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Projects >>>'
GO

IF OBJECT_ID('Repositories') IS NOT NULL
    PRINT '<<< CREATED TABLE Repositories >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Repositories >>>'
GO

IF OBJECT_ID('Users') IS NOT NULL
    PRINT '<<< CREATED TABLE Users >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Users >>>'
GO

IF OBJECT_ID('UsersInProjects') IS NOT NULL
    PRINT '<<< CREATED TABLE UsersInProjects >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE UsersInProjects >>>'
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id=OBJECT_ID('Users') AND name='I_Users_Login')
    PRINT '<<< CREATED INDEX Users.I_Users_Login >>>'
ELSE
    PRINT '<<< FAILED CREATING INDEX Users.I_Users_Login >>>'
GO

IF OBJECT_ID('AnalysisResults') IS NOT NULL
    PRINT '<<< CREATED TABLE AnalysisResults >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE AnalysisResults >>>'
GO

IF OBJECT_ID('Builds') IS NOT NULL
    PRINT '<<< CREATED TABLE Builds >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Builds >>>'
GO

IF OBJECT_ID('ModuleCoverage') IS NOT NULL
    PRINT '<<< CREATED TABLE ModuleCoverage >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE ModuleCoverage >>>'
GO

IF OBJECT_ID('Projects') IS NOT NULL
    PRINT '<<< CREATED TABLE Projects >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Projects >>>'
GO

IF OBJECT_ID('Repositories') IS NOT NULL
    PRINT '<<< CREATED TABLE Repositories >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Repositories >>>'
GO

IF OBJECT_ID('TestResults') IS NOT NULL
    PRINT '<<< CREATED TABLE TestResults >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE TestResults >>>'
GO

IF OBJECT_ID('Users') IS NOT NULL
    PRINT '<<< CREATED TABLE Users >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Users >>>'
GO

IF OBJECT_ID('UsersInProjects') IS NOT NULL
    PRINT '<<< CREATED TABLE UsersInProjects >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE UsersInProjects >>>'
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id=OBJECT_ID('Users') AND name='I_Users_UserName')
    PRINT '<<< CREATED INDEX Users.I_Users_UserName >>>'
ELSE
    PRINT '<<< FAILED CREATING INDEX Users.I_Users_UserName >>>'
GO
