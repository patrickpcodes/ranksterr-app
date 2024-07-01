CREATE TABLE [dbo].[Songs]
(
	SongId UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(255),
    Artist NVARCHAR(255),
    Album NVARCHAR(255),
    ReleaseYear INT,
    ImageUrl NVARCHAR(255)
)
