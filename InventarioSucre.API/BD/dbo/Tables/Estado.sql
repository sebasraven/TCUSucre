CREATE TABLE [dbo].[Estado] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [NombreEstado] VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED ([Id] ASC)
);

