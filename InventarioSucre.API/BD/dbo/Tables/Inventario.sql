CREATE TABLE [dbo].[Inventario] (
    [Id]                           UNIQUEIDENTIFIER NOT NULL,
    [NumeroIdentificacion]         VARCHAR (50)     NULL,
    [Descripcion]                  VARCHAR (200)    NOT NULL,
    [Marca]                        VARCHAR (100)    NULL,
    [Modelo]                       VARCHAR (100)    NULL,
    [Serie]                        VARCHAR (100)    NULL,
    [IdEstado]                     UNIQUEIDENTIFIER NOT NULL,
    [Ubicacion]                    VARCHAR (100)    NOT NULL,
    [ModoAdquisicion]              VARCHAR (100)    NOT NULL,
    [Observaciones]                VARCHAR (MAX)    NULL,
    [ObservacionesInstitucionales] VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_Inventario] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Inventario_Estado] FOREIGN KEY ([IdEstado]) REFERENCES [dbo].[Estado] ([Id])
);

