CREATE TABLE [dbo].[IssueOwners]
(
    UserId  SMALLINT NOT NULL,
    IssueId INT      NOT NULL,
    CONSTRAINT PK_IssueOwners PRIMARY KEY CLUSTERED (UserId, IssueId),
    CONSTRAINT FK_Users_IssueOwners FOREIGN KEY (UserId)
    REFERENCES dbo.Users(UserId) ON DELETE CASCADE,
    CONSTRAINT FK_Issues_IssueOwners FOREIGN KEY (IssueId)
    REFERENCES dbo.Issues(IssueId) ON DELETE CASCADE
)