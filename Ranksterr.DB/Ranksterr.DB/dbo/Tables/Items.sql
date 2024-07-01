CREATE TABLE [dbo].[Items]
(
	ItemId UNIQUEIDENTIFIER PRIMARY KEY,
    ItemType NVARCHAR(50), -- e.g., 'Movie', 'Song', 'Painting'
    ItemName NVARCHAR(255),
    ImageUrl NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE()
)
