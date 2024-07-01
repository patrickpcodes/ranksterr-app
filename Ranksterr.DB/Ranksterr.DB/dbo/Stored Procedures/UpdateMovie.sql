CREATE PROCEDURE [dbo].[UpdateMovie]
    @MovieId UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Director NVARCHAR(255),
    @ReleaseYear INT,
    @ImageUrl NVARCHAR(255)
AS
BEGIN
    UPDATE Movies
    SET Title = @Title, Director = @Director, ReleaseYear = @ReleaseYear, ImageUrl = @ImageUrl
    WHERE MovieId = @MovieId;

    EXEC UpdateItem @MovieId, 'Movie', @Title, @ImageUrl;
END;
