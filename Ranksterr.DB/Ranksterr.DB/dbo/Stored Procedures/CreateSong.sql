CREATE PROCEDURE [dbo].[CreateSong]
    @SongId UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Artist NVARCHAR(255),
    @Album NVARCHAR(255),
    @ReleaseYear INT,
    @ImageUrl NVARCHAR(255)
AS
BEGIN
    INSERT INTO Songs (SongId, Title, Artist, Album, ReleaseYear, ImageUrl)
    VALUES (@SongId, @Title, @Artist, @Album, @ReleaseYear, @ImageUrl);

    EXEC CreateItem @SongId, 'Song', @Title, @ImageUrl;
END;
