CREATE PROCEDURE [dbo].[CreateMovie]
    @MovieId UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Director NVARCHAR(255),
    @ReleaseYear INT,
    @ImageUrl NVARCHAR(255)
AS
BEGIN
    INSERT INTO Movies (MovieId, Title, Director, ReleaseYear, ImageUrl)
    VALUES (@MovieId, @Title, @Director, @ReleaseYear, @ImageUrl);

    EXEC CreateItem @MovieId, 'Movie', @Title, @ImageUrl;
END;
