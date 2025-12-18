-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Agregar un nuevo estado
-- =============================================
CREATE PROCEDURE AgregarEstado
    @Id UNIQUEIDENTIFIER,
    @NombreEstado VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION
        INSERT INTO [dbo].[Estado] ([Id], [NombreEstado])
        VALUES (@Id, @NombreEstado)

        SELECT @Id
    COMMIT TRANSACTION
END