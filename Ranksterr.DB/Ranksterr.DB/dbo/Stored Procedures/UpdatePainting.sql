CREATE PROCEDURE [dbo].[UpdatePainting]
    @PaintingId UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Artist NVARCHAR(255),
    @YearCreated INT,
    @ImageUrl NVARCHAR(255)
AS
BEGIN
    UPDATE Paintings
    SET Title = @Title, Artist = @Artist, YearCreated = @YearCreated, ImageUrl = @ImageUrl
    WHERE PaintingId = @PaintingId;

    EXEC UpdateItem @PaintingId, 'Painting', @Title, @ImageUrl;
END;
