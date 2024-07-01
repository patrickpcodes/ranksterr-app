CREATE PROCEDURE [dbo].[CreateDuel]
    @GauntletId UNIQUEIDENTIFIER,
    @ItemId1 UNIQUEIDENTIFIER,
    @ItemId2 UNIQUEIDENTIFIER,
    @DuelId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET @DuelId = NEWID();
    INSERT INTO Duels (DuelId, GauntletId, ItemId1, ItemId2)
    VALUES (@DuelId, @GauntletId, @ItemId1, @ItemId2);
END;