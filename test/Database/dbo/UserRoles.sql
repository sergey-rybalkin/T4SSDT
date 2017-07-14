CREATE VIEW dbo.UserRoles
AS
    SELECT UserName,
           Created,
           RoleId
    FROM Users u
         INNER JOIN UsersInRoles r ON u.UserId = r.UserId
