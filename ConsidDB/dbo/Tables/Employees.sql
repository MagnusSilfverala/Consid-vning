CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Salary] DECIMAL NOT NULL, 
    [IsCEO] BIT NOT NULL, 
    [IsManager] BIT NOT NULL, 
    [ManagerId] INT NULL
)
