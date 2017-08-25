CREATE PROCEDURE [dbo].[TestProcedure]
    @IntNotNullable int NOT NULL,
    @IntNullable int NULL,
    @IntOut INT OUT
AS
    SELECT @IntNotNullable + @IntNullable
RETURN 0