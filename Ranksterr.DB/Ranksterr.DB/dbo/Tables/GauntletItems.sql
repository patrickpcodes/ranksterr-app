CREATE TABLE [dbo].[GauntletItems]
(
	GauntletItemId UNIQUEIDENTIFIER PRIMARY KEY,
    GauntletId UNIQUEIDENTIFIER,
    ItemId UNIQUEIDENTIFIER,
    FOREIGN KEY (GauntletId) REFERENCES Gauntlets(GauntletId),
    FOREIGN KEY (ItemId) REFERENCES Items(ItemId)
)
