CREATE PROCEDURE [dbo].[UpdateDuel]
    @DuelId UNIQUEIDENTIFIER,
    @GauntletId UNIQUEIDENTIFIER,
    @ItemId1 UNIQUEIDENTIFIER,
    @ItemId2 UNIQUEIDENTIFIER,
    @WinnerId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Duels
    SET GauntletId = @GauntletId, ItemId1 = @ItemId1, ItemId2 = @ItemId2, WinnerId = @WinnerId
    WHERE DuelId = @DuelId;
END;