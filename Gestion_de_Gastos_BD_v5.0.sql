/* ============================================================ */
/*            ADMIN_GASTOS_DB  -  SCRIPT COMPLETO               */                                   
/* ============================================================ */

/* CREAR BASE DE DATOS */
CREATE DATABASE ADMIN_GASTOS_DB;
GO
/* USAR LA BASE */
USE ADMIN_GASTOS_DB;
GO

/* ============================================================ */
/*                    CREACIÓN DE TABLAS                        */
/* ============================================================ */

/* 1. USUARIO */
CREATE TABLE USUARIO
(
    IdUsuario  INT IDENTITY(1,1) PRIMARY KEY,
    Email      VARCHAR(100) NOT NULL UNIQUE,
    Password   VARCHAR(255) NOT NULL,
    Nombre     VARCHAR(50)  NOT NULL,
    Apellido   VARCHAR(50)  NULL,
    FechaNac   DATE         NULL,
    ImagenURL  VARCHAR(255) NULL,
    Estado     BIT          NOT NULL
);

/* 2. HOGAR */
CREATE TABLE HOGAR
(
    IdHogar   INT IDENTITY(1,1) PRIMARY KEY,
    Nombre    VARCHAR(100) NOT NULL,
    Estado    BIT          NOT NULL,
    
);

/* 3. HOGAR_USUARIO */
CREATE TABLE HOGAR_USUARIO
(
    IdMiembro INT IDENTITY(1,1) PRIMARY KEY,
    IdHogar   INT         NOT NULL,
    IdUsuario INT         NOT NULL,
    Rol       VARCHAR(20) NOT NULL,   -- 'Admin' | 'Miembro' | 'Lector'
    Estado    BIT         NOT NULL,
    FOREIGN KEY (IdHogar)   REFERENCES HOGAR(IdHogar),
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario)
);

/* 4. CATEGORIA */
/* Tipo: 1=Ingreso | 2=Gasto */
CREATE TABLE CATEGORIA
(
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      VARCHAR(100) NOT NULL,
    Tipo        INT          NOT NULL,
    IdUsuario   INT          NOT NULL,
    IdHogar     INT          NULL,
    Estado      BIT          NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar)   REFERENCES HOGAR(IdHogar)
);

/* 5. MEDIOPAGO */
/* Tipo: 1=Efectivo | 2=Débito/Billetera | 3=Crédito */
CREATE TABLE MEDIOPAGO
(
    IdMedioPago    INT IDENTITY(1,1) PRIMARY KEY,
    Tipo           INT          NOT NULL,
    Descripcion    VARCHAR(100) NOT NULL,
    DiaCierre      INT          NULL,
    DiaVencimiento INT          NULL,
    IdUsuario      INT          NOT NULL,
    IdHogar        INT          NULL,
    Estado         BIT          NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar)   REFERENCES HOGAR(IdHogar)
);

/* 6. INGRESO */
CREATE TABLE INGRESO
(
    IdIngreso   INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(150)   NOT NULL,
    Fecha       DATE           NOT NULL,
    Monto       DECIMAL(18,2)  NOT NULL,
    IdCategoria INT            NOT NULL,
    IdUsuario   INT            NOT NULL,
    IdHogar     INT            NULL,
    Estado      BIT            NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES CATEGORIA(IdCategoria),
    FOREIGN KEY (IdUsuario)   REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar)     REFERENCES HOGAR(IdHogar)
);

/* 7. GASTO                                                              */
/* EsEnCuotas, CantidadCuotas y MontoCuota integrados */
/* Moneda: 1=ARS | 2=USD | 3=EUR | 4=BRL                               */
CREATE TABLE GASTO
(
    IdGasto        INT IDENTITY(1,1) PRIMARY KEY,
    Fecha          DATE           NOT NULL,
    MontoPesos     DECIMAL(18,2)  NOT NULL,
    Moneda         INT            NOT NULL,
    MontoUSD       DECIMAL(18,2)  NULL,
    Cotizacion     DECIMAL(18,2)  NULL,
    Descripcion    VARCHAR(200)   NOT NULL,
    EsEnCuotas     BIT            NOT NULL DEFAULT 0,
    CantidadCuotas INT            NULL,
    MontoCuota     DECIMAL(18,2)  NULL,
    IdCategoria    INT            NOT NULL,
    IdMedioPago    INT            NOT NULL,
    IdUsuario      INT            NOT NULL,
    IdHogar        INT            NULL,
    Estado         BIT            NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES CATEGORIA(IdCategoria),
    FOREIGN KEY (IdMedioPago) REFERENCES MEDIOPAGO(IdMedioPago),
    FOREIGN KEY (IdUsuario)   REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar)     REFERENCES HOGAR(IdHogar)
);

/* 8. CUOTA                                                          */
/* TotalCuotas agregado + Estado cambiado a INT*/
/* Estado: 1=Pendiente | 2=Pagada | 3=Vencida                       */
CREATE TABLE CUOTA
(
    IdCuota      INT IDENTITY(1,1) PRIMARY KEY,
    IdGasto      INT           NOT NULL,
    NumeroCuota  INT           NOT NULL,
    TotalCuotas  INT           NOT NULL DEFAULT 1,
    Monto        DECIMAL(18,2) NOT NULL,
    Vencimiento  DATE          NOT NULL,
    Estado       INT           NOT NULL,
    FOREIGN KEY (IdGasto) REFERENCES GASTO(IdGasto)
);

/* 9. META_AHORRO                                          */
/* Estado: 1=Activa | 2=Completada | 3=Cancelada          */
CREATE TABLE META_AHORRO
(
    IdMeta        INT IDENTITY(1,1) PRIMARY KEY,
    Nombre        VARCHAR(100)  NOT NULL,
    MontoObjetivo DECIMAL(18,2) NOT NULL,
    FechaObjetivo DATE          NULL,
    IdUsuario     INT           NOT NULL,
    IdHogar       INT           NULL,
    Estado        INT           NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
    FOREIGN KEY (IdHogar)   REFERENCES HOGAR(IdHogar)
);

/* 10. DEUDA                                               */
/* Estado: 0=Pago | 1=Pendiente | 2=Eliminado             */
CREATE TABLE DEUDA
(
    IdDeuda      INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario    INT           NOT NULL,
    NombreDeudor VARCHAR(100)  NOT NULL,
    EmailDeudor  VARCHAR(100)  NOT NULL,
    Descripcion  VARCHAR(200)  NULL,
    MontoTotal   DECIMAL(18,2) NOT NULL,
    Cuotas       INT           NULL,
    FechaInicio  DATE          NOT NULL,
    Estado       INT           NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario)
);
GO
