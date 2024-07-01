CREATE PROCEDURE [dbo].[CreatePainting]
    @PaintingId UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Artist NVARCHAR(255),
    @YearCreated INT,
    @ImageUrl NVARCHAR(255)
AS
BEGIN
    INSERT INTO Paintings (PaintingId, Title, Artist, YearCreated, ImageUrl)
    VALUES (@PaintingId, @Title, @Artist, @YearCreated, @ImageUrl);

    EXEC CreateItem @PaintingId, 'Painting', @Title, @ImageUrl;
END;
