CREATE VIEW [dbo].[NullableColumnsView]
AS
    SELECT u.UserName,
           r.RoleId
    FROM Users u LEFT JOIN UsersInRoles r ON u.UserId = r.UserId
