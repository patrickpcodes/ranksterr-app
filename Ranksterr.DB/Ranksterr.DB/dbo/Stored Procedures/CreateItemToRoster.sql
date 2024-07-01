CREATE PROCEDURE [dbo].[CreateItemToRoster]
    @RosterId UNIQUEIDENTIFIER,
    @ItemId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO RosterItems (RosterItemId, RosterId, ItemId)
    VALUES (NEWID(), @RosterId, @ItemId);
END;