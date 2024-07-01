CREATE PROCEDURE [dbo].[CreateGauntlet]
    @GauntletId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET @GauntletId = NEWID();
    INSERT INTO Gauntlets (GauntletId)
    VALUES (@GauntletId);
END;