
-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Editar un estado existente
-- =============================================
CREATE PROCEDURE EditarEstado
    @Id UNIQUEIDENTIFIER,
    @NombreEstado VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION
        UPDATE [dbo].[Estado]
        SET [NombreEstado] = @NombreEstado
        WHERE [Id] = @Id

        SELECT @Id
    COMMIT TRANSACTION
END