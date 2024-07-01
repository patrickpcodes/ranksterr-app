﻿CREATE TABLE [dbo].[RosterItems]
(
	RosterItemId UNIQUEIDENTIFIER PRIMARY KEY,
    RosterId UNIQUEIDENTIFIER,
    ItemId UNIQUEIDENTIFIER,
    FOREIGN KEY (RosterId) REFERENCES Rosters(RosterId),
    FOREIGN KEY (ItemId) REFERENCES Items(ItemId)
)