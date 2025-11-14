-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Editar un registro de inventario existente
-- =============================================
CREATE PROCEDURE EditarInventario
    @Id UNIQUEIDENTIFIER,
    @NumeroIdentificacion VARCHAR(50) = NULL,
    @Descripcion VARCHAR(200),
    @Marca VARCHAR(100) = NULL,
    @Modelo VARCHAR(100) = NULL,
    @Serie VARCHAR(100) = NULL,
    @IdEstado UNIQUEIDENTIFIER,
    @Ubicacion VARCHAR(100),
    @ModoAdquisicion VARCHAR(100),
    @Observaciones VARCHAR(MAX) = NULL,
    @ObservacionesInstitucionales VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION
        UPDATE [dbo].[Inventario]
        SET [NumeroIdentificacion] = @NumeroIdentificacion,
            [Descripcion] = @Descripcion,
            [Marca] = @Marca,
            [Modelo] = @Modelo,
            [Serie] = @Serie,
            [IdEstado] = @IdEstado,
            [Ubicacion] = @Ubicacion,
            [ModoAdquisicion] = @ModoAdquisicion,
            [Observaciones] = @Observaciones,
            [ObservacionesInstitucionales] = @ObservacionesInstitucionales
        WHERE [Id] = @Id

        SELECT @Id
    COMMIT TRANSACTION
END