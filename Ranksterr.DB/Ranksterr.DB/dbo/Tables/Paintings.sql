CREATE TABLE [dbo].[Paintings]
(
	PaintingId UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(255),
    Artist NVARCHAR(255),
    YearCreated INT,
    ImageUrl NVARCHAR(255)
)
