CREATE TABLE [dbo].[LibraryItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [CategoryId] INT NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Author] NVARCHAR(50) NOT NULL, 
    [Pages] INT NULL, 
    [RunTimeMinutes] INT NULL, 
    [IsBorrowable] BIT NOT NULL, 
    [Borrower] NVARCHAR(50) NULL, 
    [BorrowDate] DATE NULL, 
    [Type] NVARCHAR(50) NOT NULL, 
    FOREIGN KEY (CategoryId) REFERENCES Category(Id)
)
