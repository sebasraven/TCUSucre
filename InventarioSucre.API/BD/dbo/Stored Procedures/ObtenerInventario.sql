-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Obtener un registro de inventario por Id
-- =============================================
CREATE PROCEDURE ObtenerInventario
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT I.[Id],
           I.[NumeroIdentificacion],
           I.[Descripcion],
           I.[Marca],
           I.[Modelo],
           I.[Serie],
           I.[Ubicacion],
           I.[ModoAdquisicion],
           I.[Observaciones],
           I.[ObservacionesInstitucionales],
           E.[NombreEstado] AS Estado
    FROM [dbo].[Inventario] I
    INNER JOIN [dbo].[Estado] E ON I.[IdEstado] = E.[Id]
    WHERE I.[Id] = @Id
END