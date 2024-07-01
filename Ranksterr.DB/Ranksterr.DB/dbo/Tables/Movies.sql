CREATE TABLE [dbo].[Movies]
(
	MovieId UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(255),
    Director NVARCHAR(255),
    ReleaseYear INT,
    ImageUrl NVARCHAR(255)
)
