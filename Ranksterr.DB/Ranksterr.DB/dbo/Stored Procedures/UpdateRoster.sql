CREATE PROCEDURE [dbo].[UpdateRoster]
    @RosterId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000)
AS
BEGIN
    UPDATE Rosters
    SET UserId = @UserId, Name = @Name, Description = @Description
    WHERE RosterId = @RosterId;
END;
