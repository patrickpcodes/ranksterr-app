CREATE PROCEDURE [dbo].[UpdateItemToRoster]
    @RosterItemId UNIQUEIDENTIFIER,
    @RosterId UNIQUEIDENTIFIER,
    @ItemId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE RosterItems
    SET RosterId = @RosterId, ItemId = @ItemId
    WHERE RosterItemId = @RosterItemId;
END;