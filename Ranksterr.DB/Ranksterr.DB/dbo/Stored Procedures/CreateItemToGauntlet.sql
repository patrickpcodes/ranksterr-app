CREATE PROCEDURE [dbo].[CreateItemToGauntlet]
    @GauntletId UNIQUEIDENTIFIER,
    @ItemId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO GauntletItems (GauntletItemId, GauntletId, ItemId)
    VALUES (NEWID(), @GauntletId, @ItemId);
END;