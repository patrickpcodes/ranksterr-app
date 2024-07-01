CREATE PROCEDURE [dbo].[UpdateSong]
    @SongId UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Artist NVARCHAR(255),
    @Album NVARCHAR(255),
    @ReleaseYear INT,
    @ImageUrl NVARCHAR(255)
AS
BEGIN
    UPDATE Songs
    SET Title = @Title, Artist = @Artist, Album = @Album, ReleaseYear = @ReleaseYear, ImageUrl = @ImageUrl
    WHERE SongId = @SongId;

    EXEC UpdateItem @SongId, 'Song', @Title, @ImageUrl;
END;