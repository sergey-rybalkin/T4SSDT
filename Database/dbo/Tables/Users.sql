CREATE TABLE [dbo].Users
(
    UserId            smallint         IDENTITY(1,1),
    UserName          nvarchar(100)    NOT NULL,
    Email             nvarchar(256)    NOT NULL,
    HashedPassword    binary(50)       NOT NULL,
    Created           datetime2(0)     CONSTRAINT [DF_Users_Created] DEFAULT GETDATE() NOT NULL,
    GlobalRole        tinyint          CONSTRAINT [DF_Users_GlobalRole] DEFAULT 0 NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED (UserId)
)