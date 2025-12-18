
-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Obtener todos los estados
-- =============================================
CREATE PROCEDURE ObtenerEstados
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [NombreEstado]
    FROM [dbo].[Estado]
END