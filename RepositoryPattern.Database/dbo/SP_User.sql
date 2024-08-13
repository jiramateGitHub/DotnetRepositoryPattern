CREATE PROCEDURE [dbo].[SP_User]
    @Module NVARCHAR(500) = NULL,
    @SessionEmpID INT = NULL,
	@Id INT = NULL,
	@Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Createby NVARCHAR(255) = NULL,
    @CreateDateTime NVARCHAR(255) = NULL,
    @Updateby NVARCHAR(255) = NULL,
    @UpdateDateTime NVARCHAR(255) = NULL,
    @DelFlag NVARCHAR(255) = NULL,
    @StatusId INT OUTPUT,
    @StatusText NVARCHAR(255) OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;

        BEGIN TRAN;

        IF @Module = 'getlist'
        BEGIN
            SELECT *
            FROM Users
            WHERE (ISNULL(@Id, '') = '' OR (Id) = @Id)
                  AND (ISNULL(@Name, '') = '' OR LOWER(@Name) LIKE '%%' + LOWER(@Name) + '%%')
                  AND (ISNULL(@Email, '') = '' OR LOWER(@Email) LIKE '%%' + LOWER(@Email) + '%%')
                  AND (ISNULL(@Createby, '') = '' OR LOWER(Createby) LIKE '%%' + LOWER(@Createby) + '%%')
                  AND (ISNULL(@CreateDateTime, '') = '' OR CreateDateTime = @CreateDateTime)
                  AND (ISNULL(@Updateby, '') = '' OR LOWER(Updateby) LIKE '%%' + LOWER(@Updateby) + '%%')
                  AND (ISNULL(@UpdateDateTime, '') = '' OR UpdateDateTime = @UpdateDateTime)
                  AND (DelFlag IS NULL OR DelFlag <> 1);

            SET @StatusId = 0;
        END;

        IF @Module = 'upsert'
        BEGIN
            IF @Id IS NULL OR @Id = 0 
            BEGIN
                INSERT INTO [dbo].[Users]
                (
                    [Name],
                    [Email],
                    [Createby],
                    [CreateDateTime],
                    [Updateby],
                    [UpdateDateTime],
                    [DelFlag]
                )
                VALUES
                (@Name, @Email, @Createby, GETDATE(),
                 @Updateby,GETDATE(), 0);
            END;
            ELSE
            BEGIN
                UPDATE [dbo].[Users]
                SET [Name] = @Name,
                    [Email] = @Email,
                    [Createby] = @Createby,
                    [Updateby] = @Updateby,
                    [UpdateDateTime] = GETDATE()
                WHERE Id = @Id;
            END;

            SET @StatusId = 0;
        END;

        IF @Module = 'delete'
        BEGIN
            UPDATE [dbo].[Users]
            SET [DelFlag] = 1
            WHERE Id = @Id;

            SET @StatusId = 0;
        END;

        COMMIT;
    END TRY
    BEGIN CATCH
        SET @StatusId = ERROR_NUMBER();
        SET @StatusText = ERROR_MESSAGE();
        ROLLBACK;

        INSERT INTO [dbo].[Log] ([LogKey], [LogValue], [CreateTime])
        VALUES
        ('[SP_User', @Module + '//' + @StatusText, GETDATE());

    END CATCH;
END;