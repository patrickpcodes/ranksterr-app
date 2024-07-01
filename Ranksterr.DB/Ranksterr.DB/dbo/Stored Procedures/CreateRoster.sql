CREATE PROCEDURE [dbo].[CreateRoster]
    @RosterId UNIQUEIDENTIFIER OUTPUT,
    @UserId UNIQUEIDENTIFIER,
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000)
AS
BEGIN
    SET @RosterId = NEWID();
    INSERT INTO Rosters (RosterId, UserId, Name, Description)
    VALUES (@RosterId, @UserId, @Name, @Description);
END;

