CREATE PROCEDURE [dbo].[CreateItem]
    @ItemId UNIQUEIDENTIFIER,
    @ItemType NVARCHAR(50),
    @ItemName NVARCHAR(255),
    @ImageUrl NVARCHAR(255)
AS
BEGIN
    INSERT INTO Items (ItemId, ItemType, ItemName, ImageUrl)
    VALUES (@ItemId, @ItemType, @ItemName, @ImageUrl);
END;