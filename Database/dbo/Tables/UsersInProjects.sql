CREATE TABLE [dbo].UsersInProjects
(
    UserId       SMALLINT    NOT NULL,
    ProjectId    CHAR(8)     NOT NULL,
    Role         TINYINT     NOT NULL,
    CONSTRAINT PK_UsersInProjects PRIMARY KEY CLUSTERED (UserId, ProjectId), 
    CONSTRAINT FK_Projects_UsersInProjects FOREIGN KEY (ProjectId)
    REFERENCES [dbo].Projects(ProjectKey) ON UPDATE CASCADE,
    CONSTRAINT FK_Users_UsersInProjects FOREIGN KEY (UserId)
    REFERENCES [dbo].Users(UserId)
)