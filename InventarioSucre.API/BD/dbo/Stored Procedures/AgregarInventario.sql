-- =============================================
-- Author:      <Author>
-- Create date: <Create Date>
-- Description: Agregar un nuevo registro de inventario
-- =============================================
CREATE PROCEDURE AgregarInventario
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
        INSERT INTO [dbo].[Inventario]
               ([Id],
                [NumeroIdentificacion],
                [Descripcion],
                [Marca],
                [Modelo],
                [Serie],
                [IdEstado],
                [Ubicacion],
                [ModoAdquisicion],
                [Observaciones],
                [ObservacionesInstitucionales])
        VALUES
               (@Id,
                @NumeroIdentificacion,
                @Descripcion,
                @Marca,
                @Modelo,
                @Serie,
                @IdEstado,
                @Ubicacion,
                @ModoAdquisicion,
                @Observaciones,
                @ObservacionesInstitucionales)
        SELECT @Id
    COMMIT TRANSACTION
END