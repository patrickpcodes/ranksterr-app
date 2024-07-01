CREATE PROCEDURE [dbo].[UpdateItem]
    @ItemId UNIQUEIDENTIFIER,
    @ItemType NVARCHAR(50),
    @ItemName NVARCHAR(255),
    @ImageUrl NVARCHAR(255)
AS
BEGIN
    UPDATE Items
    SET ItemType = @ItemType, ItemName = @ItemName, ImageUrl = @ImageUrl
    WHERE ItemId = @ItemId;
END;