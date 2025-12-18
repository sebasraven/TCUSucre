
-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Obtener un estado por Id
-- =============================================
CREATE PROCEDURE ObtenerEstado
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [NombreEstado]
    FROM [dbo].[Estado]
    WHERE [Id] = @Id
END