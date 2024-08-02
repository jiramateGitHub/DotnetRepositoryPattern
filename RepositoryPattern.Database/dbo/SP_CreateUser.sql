CREATE PROCEDURE [dbo].[sp_CreateUser]
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Users (Name, Email)
    VALUES (@Name, @Email);
    
    SET @Id = SCOPE_IDENTITY();
END