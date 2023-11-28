USE [master]
GO

CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    CorreoElectronico VARCHAR(50),
    Telefono VARCHAR(15) UNIQUE
);
GO


CREATE TABLE Equipos (
    EquipoID INT IDENTITY(1,1) PRIMARY KEY,
    TipoEquipo VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50),
    UsuarioID INT,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE Reparaciones (
    ReparacionID INT IDENTITY(1,1) PRIMARY KEY,
    EquipoID INT,
    FechaSolicitud DATETIME,
    Estado CHAR(1)
);
GO


CREATE TABLE Asignaciones (
    AsignacionID INT IDENTITY(1,1) PRIMARY KEY,
    ReparacionID INT,
    TecnicoID INT,
    FechaAsignacion DATETIME,
    FOREIGN KEY (ReparacionID) REFERENCES Reparaciones(ReparacionID),
    FOREIGN KEY (TecnicoID) REFERENCES Tecnicos(TecnicoID)
);
GO


CREATE TABLE Tecnicos (
    TecnicoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50),
    Especialidad VARCHAR(50)
);
GO


CREATE TABLE DetallesReparacion (
    DetalleID INT IDENTITY(1,1) PRIMARY KEY,
    ReparacionID INT,
    Descripcion VARCHAR(50),
    FechaInicio DATETIME
);
GO