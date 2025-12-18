
-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Eliminar un estado
-- =============================================
CREATE PROCEDURE EliminarEstado
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION
        DELETE FROM [dbo].[Estado]
        WHERE [Id] = @Id

        SELECT @Id
    COMMIT TRANSACTION
END