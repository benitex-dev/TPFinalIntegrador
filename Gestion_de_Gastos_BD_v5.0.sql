/* CREAR BASE DE DATOS*/ 
CREATE DATABASE ADMIN_GASTOS_DB;
GO

/* USAR LA BASE */
USE ADMIN_GASTOS_DB;
GO

/* 1. CREAR TABLA USUARIO */
CREATE TABLE USUARIO
(
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NULL,
    FechaNac DATE NULL,
    ImagenURL VARCHAR(255) NULL,
    Estado BIT NOT NULL
);

/* 2. CREAR TABLA HOGAR */
CREATE TABLE HOGAR
(
    IdHogar INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    IdUsuario INT NOT NULL,
    Estado BIT NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario)
);

/* 3. CREAR TABLA HOGAR_USUARIO */
CREATE TABLE HOGAR_USUARIO
(
    IdMiembro INT IDENTITY(1,1) PRIMARY KEY,
    IdHogar INT NOT NULL,
    IdUsuario INT NOT NULL,
    Rol VARCHAR(20) NOT NULL,
    Estado BIT NOT NULL,
    FOREIGN KEY (IdHogar) REFERENCES HOGAR(IdHogar),
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario)
);

/* 4. CREAR TABLA CATEGORIA */
CREATE TABLE CATEGORIA
(
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Tipo INT NOT NULL,
    IdUsuario INT NOT NULL,
    IdHogar INT NULL,
    Estado BIT NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar) REFERENCES HOGAR(IdHogar)
);

/* 5. CREAR TABLA MEDIOPAGO */
CREATE TABLE MEDIOPAGO
(
    IdMedioPago INT IDENTITY(1,1) PRIMARY KEY,
    Tipo INT NOT NULL,
    Descripcion VARCHAR(100) NOT NULL,
    DiaCierre INT NULL,
    DiaVencimiento INT NULL,
    IdUsuario INT NOT NULL,
    IdHogar INT NULL,
    Estado BIT NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar) REFERENCES HOGAR(IdHogar)
);

/* 6. CREAR TABLA INGRESO */
CREATE TABLE INGRESO
(
    IdIngreso INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(150) NOT NULL,
    Fecha DATE NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    IdCategoria INT NOT NULL,
    IdUsuario INT NOT NULL,
    IdHogar INT NULL,
    Estado BIT NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES CATEGORIA(IdCategoria),
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar) REFERENCES HOGAR(IdHogar)
);

/* 7. CREAR TABLA GASTO */
CREATE TABLE GASTO
(
    IdGasto INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL,
    MontoPesos DECIMAL(18,2) NOT NULL,
    Moneda INT NOT NULL,
    MontoUSD DECIMAL(18,2) NULL,
    Cotizacion DECIMAL(18,2) NULL,
    Descripcion VARCHAR(200) NOT NULL,
    IdCategoria INT NOT NULL,
    IdMedioPago INT NOT NULL,
    IdUsuario INT NOT NULL,
    IdHogar INT NULL,
    Estado BIT NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES CATEGORIA(IdCategoria),
    FOREIGN KEY (IdMedioPago) REFERENCES MEDIOPAGO(IdMedioPago),
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar) REFERENCES HOGAR(IdHogar)
);

/* 8. CREAR TABLA CUOTA */
CREATE TABLE CUOTA
(
    IdCuota INT IDENTITY(1,1) PRIMARY KEY,
    IdGasto INT NOT NULL,
    NumeroCuota INT NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    Vencimiento DATE NOT NULL,
    Estado VARCHAR(20) NOT NULL,
    FOREIGN KEY (IdGasto) REFERENCES GASTO(IdGasto)
);

/* 9. CREAR TABLA META_AHORRO */
CREATE TABLE META_AHORRO
(
    IdMeta INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    MontoObjetivo DECIMAL(18,2) NOT NULL,
    FechaObjetivo DATE NULL,
    IdUsuario INT NOT NULL,
    IdHogar INT NULL,
    Estado VARCHAR(20) NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar) REFERENCES HOGAR(IdHogar)
);

/* 10. CREAR TABLA DEUDA */
CREATE TABLE DEUDA
(
    IdDeuda INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    NombreDeudor VARCHAR(100) NOT NULL,
    EmailDeudor VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(200) NULL,
    MontoTotal DECIMAL(18,2) NOT NULL,
    Cuotas INT NULL,
    FechaInicio DATE NOT NULL,
    Estado VARCHAR(20) NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario)
);
GO


/* ---------------- INSERTS ----------------- */

/* USUARIOS  */
INSERT INTO USUARIO (Email, Password, Nombre, Apellido, FechaNac, ImagenURL, Estado)
VALUES 
('sa', '123', 'Pepin', 'O,', '2004-05-15', NULL, 1),
('ma', '123', 'Mirin', 'Da', '1995-05-10', NULL, 1);
GO

/* HOGARES (Un hogar principal) */
INSERT INTO HOGAR (Nombre, IdUsuario, Estado)
VALUES ('Casa Sur', 1, 1);
GO

/*  HOGAR_USUARIO*/
/* Roles: 'Admin', 'Editor', 'Viewer' */
INSERT INTO HOGAR_USUARIO (IdHogar, IdUsuario, Rol, Estado)
VALUES 
(1, 1, 'Admin', 1),
(1, 2, 'Editor', 1);
GO

/* CATEGORÍAS (Tipos: 1 = Ingreso, 2 = Gasto) */
INSERT INTO CATEGORIA (Nombre, Tipo, IdUsuario, IdHogar, Estado)
VALUES 
('Sueldo', 1, 1, NULL, 1),
('Negocios/Ventas', 1, 1, NULL, 1),
('Supermercado', 2, 1, NULL, 1),
('Vehículo', 2, 1, NULL, 1),
('Entretenimiento', 2, 1, NULL, 1),
('Música e Instrumentos', 2, 1, NULL, 1),
('Inversiones', 2, 1, NULL, 1),
('Tecnología', 2, 1, NULL, 1);
GO

/* MEDIOS DE PAGO (Tipos: 1=Efectivo, 2=Débito, 3=Crédito) */
INSERT INTO MEDIOPAGO (Tipo, Descripcion, DiaCierre, DiaVencimiento, IdUsuario, IdHogar, Estado)
VALUES 
(1, 'Efectivo', NULL, NULL, 1, NULL, 1),
(2, 'Mercado Pago', NULL, NULL, 1, NULL, 1),
(3, 'Tarjeta Visa Banco Patagonia', 25, 5, 1, NULL, 1);
GO

ALTER TABLE CUOTA
ADD TotalCuotas INT NOT NULL DEFAULT 1;

ALTER TABLE CUOTA
ALTER COLUMN Estado INT NOT NULL;

ALTER TABLE GASTO
ADD EsEnCuotas BIT NOT NULL DEFAULT 0,
    CantidadCuotas INT NULL,
    MontoCuota DECIMAL(18,2) NULL;
GO