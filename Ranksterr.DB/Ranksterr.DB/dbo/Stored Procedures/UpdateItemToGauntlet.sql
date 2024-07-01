CREATE PROCEDURE [dbo].[UpdateItemToGauntlet]
    @GauntletItemId UNIQUEIDENTIFIER,
    @GauntletId UNIQUEIDENTIFIER,
    @ItemId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE GauntletItems
    SET GauntletId = @GauntletId, ItemId = @ItemId
    WHERE GauntletItemId = @GauntletItemId;
END;