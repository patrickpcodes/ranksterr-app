CREATE TABLE [dbo].[Duels]
(
	DuelId UNIQUEIDENTIFIER PRIMARY KEY,
    GauntletId UNIQUEIDENTIFIER,
    ItemId1 UNIQUEIDENTIFIER,
    ItemId2 UNIQUEIDENTIFIER,
    WinnerId UNIQUEIDENTIFIER NULL, -- Initially NULL, updated when the user picks a winner
    FOREIGN KEY (GauntletId) REFERENCES Gauntlets(GauntletId),
    FOREIGN KEY (ItemId1) REFERENCES Items(ItemId),
    FOREIGN KEY (ItemId2) REFERENCES Items(ItemId),
    FOREIGN KEY (WinnerId) REFERENCES Items(ItemId)
)
