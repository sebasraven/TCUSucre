/* ======================================
   CREAR BASE DE DATOS InventarioSucre
====================================== */

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'InventarioSucre')
BEGIN
    CREATE DATABASE InventarioSucre;
END
GO