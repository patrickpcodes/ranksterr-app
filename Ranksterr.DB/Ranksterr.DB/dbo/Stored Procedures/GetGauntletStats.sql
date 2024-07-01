CREATE PROCEDURE [dbo].[GetGauntletStats]
    @GauntletId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        d.DuelId,
        i1.ItemName AS Item1Name,
        i2.ItemName AS Item2Name,
        w.ItemName AS WinnerName
    FROM Duels d
    JOIN Items i1 ON d.ItemId1 = i1.ItemId
    JOIN Items i2 ON d.ItemId2 = i2.ItemId
    LEFT JOIN Items w ON d.WinnerId = w.ItemId
    WHERE d.GauntletId = @GauntletId;
END;