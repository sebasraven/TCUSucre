-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Eliminar un registro de inventario
-- =============================================
CREATE PROCEDURE EliminarInventario
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION
        DELETE FROM [dbo].[Inventario]
        WHERE [Id] = @Id

        SELECT @Id
    COMMIT TRANSACTION
END