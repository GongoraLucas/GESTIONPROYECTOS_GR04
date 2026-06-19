/*==============================================================*/
/* DBMS name:      Microsoft SQL Server                         */
/* Description:    MÃ³dulo de GestiÃ³n de Empleados - UNIFICADO   */
/*                 (FusiÃ³n DDLAntiguo.sql + DDL_SQLSERVER.sql)  */
/*==============================================================*/

/*==============================================================*/
/* CLEANUP: EliminaciÃ³n en orden inverso de dependencias        */
/*==============================================================*/

-- MÃ³dulo Seguridad / Usuarios
IF OBJECT_ID('dbo.XEUXP_USUPE',         'U') IS NOT NULL DROP TABLE dbo.XEUXP_USUPE;
IF OBJECT_ID('dbo.XEUSU_RECUPERACION',   'U') IS NOT NULL DROP TABLE dbo.XEUSU_RECUPERACION;
IF OBJECT_ID('dbo.XEUSU_USUAR',          'U') IS NOT NULL DROP TABLE dbo.XEUSU_USUAR;
IF OBJECT_ID('dbo.XEOXP_OPCPE',          'U') IS NOT NULL DROP TABLE dbo.XEOXP_OPCPE;
IF OBJECT_ID('dbo.XEOPC_OPCIO',          'U') IS NOT NULL DROP TABLE dbo.XEOPC_OPCIO;
IF OBJECT_ID('dbo.XEPER_PERFI',          'U') IS NOT NULL DROP TABLE dbo.XEPER_PERFI;
IF OBJECT_ID('dbo.XEEST_ESTAD',          'U') IS NOT NULL DROP TABLE dbo.XEEST_ESTAD;
IF OBJECT_ID('dbo.XESIS_SISTE',          'U') IS NOT NULL DROP TABLE dbo.XESIS_SISTE;

-- MÃ³dulo Proyectos / Horas
IF OBJECT_ID('dbo.GR_GEPRY_PEEMP',       'U') IS NOT NULL DROP TABLE dbo.GR_GEPRY_PEEMP;
IF OBJECT_ID('dbo.GEPRY_PROYEC',          'U') IS NOT NULL DROP TABLE dbo.GEPRY_PROYEC;
IF OBJECT_ID('dbo.PEHRT_HORAS',           'U') IS NOT NULL DROP TABLE dbo.PEHRT_HORAS;
IF OBJECT_ID('dbo.RELATIONSHIP_19',       'U') IS NOT NULL DROP TABLE dbo.RELATIONSHIP_19;

-- MÃ³dulo Empleados
IF OBJECT_ID('dbo.PEFAE_FAMILIAR',        'U') IS NOT NULL DROP TABLE dbo.PEFAE_FAMILIAR;
IF OBJECT_ID('dbo.PEEMP_EMPLE',           'U') IS NOT NULL DROP TABLE dbo.PEEMP_EMPLE;
IF OBJECT_ID('dbo.PEINS_INSTRUCCION',     'U') IS NOT NULL DROP TABLE dbo.PEINS_INSTRUCCION;
IF OBJECT_ID('dbo.PEDIS_DISCAPACIDAD',    'U') IS NOT NULL DROP TABLE dbo.PEDIS_DISCAPACIDAD;
IF OBJECT_ID('dbo.PECAR_CARGO',           'U') IS NOT NULL DROP TABLE dbo.PECAR_CARGO;
IF OBJECT_ID('dbo.PEDEP_DEPAR',           'U') IS NOT NULL DROP TABLE dbo.PEDEP_DEPAR;
IF OBJECT_ID('dbo.PEESC_ESTCIV',          'U') IS NOT NULL DROP TABLE dbo.PEESC_ESTCIV;
IF OBJECT_ID('dbo.PESEX_SEXO',            'U') IS NOT NULL DROP TABLE dbo.PESEX_SEXO;
GO

/*==============================================================*/
/* 1. TABLAS BASE / CATÃLOGOS (Independientes)                  */
/*==============================================================*/

-- Estado Civil
CREATE TABLE dbo.PEESC_ESTCIV
(
    PEESC_CODIGO    char(1)      NOT NULL,
    PEESC_DESCRI    char(50)     NOT NULL,
    CONSTRAINT PK_PEESC_ESTCIV PRIMARY KEY CLUSTERED (PEESC_CODIGO ASC)
);
GO

-- Sexo / GÃ©nero
CREATE TABLE dbo.PESEX_SEXO
(
    PESEX_CODIGO    char(1)      NOT NULL,
    PESEX_DESCRI    varchar(50)  NOT NULL,
    CONSTRAINT PK_PESEX_SEXO PRIMARY KEY CLUSTERED (PESEX_CODIGO ASC)
);
GO

-- Departamentos
CREATE TABLE dbo.PEDEP_DEPAR
(
    PEDEP_CODIGO    char(3)      NOT NULL,
    PEDEP_DESCRIP   varchar(50)  NOT NULL,
    CONSTRAINT PK_PEDEP_DEPAR PRIMARY KEY CLUSTERED (PEDEP_CODIGO ASC)
);
GO

-- CatÃ¡logo de Discapacidades (CONADIS / MSP Ecuador)
CREATE TABLE dbo.PEDIS_DISCAPACIDAD
(
    PEDIS_CODIGO    char(2)      NOT NULL,
    PEDIS_DESCRI    varchar(50)  NOT NULL,
    CONSTRAINT PK_PEDIS_DISCAPACIDAD PRIMARY KEY CLUSTERED (PEDIS_CODIGO ASC)
);
GO

-- Nivel de InstrucciÃ³n (SENESCYT / Ministerio del Trabajo)
CREATE TABLE dbo.PEINS_INSTRUCCION
(
    PEINS_CODIGO    char(2)      NOT NULL,
    PEINS_DESCRI    varchar(50)  NOT NULL,
    CONSTRAINT PK_PEINS_INSTRUCCION PRIMARY KEY CLUSTERED (PEINS_CODIGO ASC)
);
GO

-- Sistemas (MÃ³dulo de Seguridad)
CREATE TABLE dbo.XESIS_SISTE
(
    XESIS_CODIGO    char(1)      NOT NULL,
    XESIS_DESCRI    varchar(50)  NOT NULL,
    CONSTRAINT PK_XESIS_SISTE PRIMARY KEY CLUSTERED (XESIS_CODIGO ASC)
);
GO

-- Estados (MÃ³dulo de Seguridad / Usuarios)
CREATE TABLE dbo.XEEST_ESTAD
(
    XEEST_CODIGO    char(1)      NOT NULL,
    XEEST_DESCRI    varchar(50)  NOT NULL,
    CONSTRAINT PK_XEEST_ESTAD PRIMARY KEY CLUSTERED (XEEST_CODIGO ASC)
);
GO

/*==============================================================*/
/* 2. TABLAS SUB-DEPENDIENTES                                   */
/*==============================================================*/

-- Cargos (depende de Departamentos)
CREATE TABLE dbo.PECAR_CARGO
(
    PEDEP_CODIGO    char(3)      NOT NULL,
    PECAR_CODIGO    char(3)      NOT NULL,
    PECAR_DESCRI    varchar(50)  NOT NULL,
    CONSTRAINT PK_PECAR_CARGO PRIMARY KEY CLUSTERED (PEDEP_CODIGO ASC, PECAR_CODIGO ASC)
);
GO

-- Opciones de MenÃº (depende de Sistemas)
CREATE TABLE dbo.XEOPC_OPCIO
(
    XEOPC_CODIGO    char(3)      NOT NULL,
    XESIS_CODIGO    char(1)      NOT NULL,
    XEOPC_DESCRI    varchar(100) NOT NULL,
    CONSTRAINT PK_XEOPC_OPCIO PRIMARY KEY CLUSTERED (XEOPC_CODIGO ASC)
);
GO

-- Perfiles de Seguridad
CREATE TABLE dbo.XEPER_PERFI
(
    XEPER_CODIGO    char(8)      NOT NULL,
    XEPER_DESCRI    varchar(100) NOT NULL,
    XEPER_OBSER     varchar(max) NULL,
    CONSTRAINT PK_XEPER_PERFI PRIMARY KEY CLUSTERED (XEPER_CODIGO ASC)
) TEXTIMAGE_ON [PRIMARY];
GO

/*==============================================================*/
/* 3. TABLA PRINCIPAL: EMPLEADOS                                */
/*==============================================================*/

CREATE TABLE dbo.PEEMP_EMPLE
(
    PEEMP_CODIGO        char(6)         NOT NULL,
    PESEX_CODIGO        char(1)         NOT NULL,
    PEESC_CODIGO        char(1)         NULL,
    PEDEP_CODIGO        char(3)         NOT NULL,
    PECAR_CODIGO        char(3)         NOT NULL,
    PEDIS_CODIGO        char(2)         NULL,               -- CatÃ¡logo CONADIS
    PEINS_CODIGO        char(2)         NOT NULL,           -- CatÃ¡logo Nivel InstrucciÃ³n
    PEE_PEEMP_CODIGO    char(6)         NULL,               -- Autorreferencia (Jefe directo)
    PEEMP_APELLI        varchar(50)     NOT NULL,
    PEEMP_NOMBRE        varchar(50)     NOT NULL,
    PEEMP_DIREC         varchar(200)    NOT NULL,
    PEEMP_FECNAC        date            NOT NULL,
    PEEMP_FECSAL        date            NOT NULL,           -- Fecha de ingreso/salida
    PEEMP_TELEF         varchar(15)     NOT NULL,
    PEEMP_EMAIL         varchar(100)    NOT NULL,
    PEEMP_CEDULA        varchar(10)     NOT NULL,
    PEEMP_SALAR         numeric(8, 0)   NOT NULL,
    PEEMP_FOTO          varchar(250)    NULL,               -- Ruta de imagen (del esquema antiguo)
    PEEMP_ESTADO        char(1)         NOT NULL DEFAULT 'A',       -- Borrado lÃ³gico
    PEEMP_PORCEN_DISC   numeric(3, 0)   NOT NULL DEFAULT 0,         -- % carnet discapacidad
    CONSTRAINT PK_PEEMP_EMPLE  PRIMARY KEY CLUSTERED (PEEMP_CODIGO ASC),
    CONSTRAINT UQ_PEEMP_CEDULA UNIQUE NONCLUSTERED (PEEMP_CEDULA ASC),
    CONSTRAINT UQ_PEEMP_EMAIL  UNIQUE NONCLUSTERED (PEEMP_EMAIL ASC)
);
GO

/*==============================================================*/
/* 4. TABLA DETALLE: FAMILIARES                                 */
/*==============================================================*/

CREATE TABLE dbo.PEFAE_FAMILIAR
(
    PEFAE_ID        bigint          NOT NULL,               -- AsignaciÃ³n manual
    PEEMP_CODIGO    char(6)         NULL,
    PEFAE_NOMBRES   varchar(50)     NOT NULL,
    PEFAE_APELL     varchar(50)     NOT NULL,
    PEFAE_FECHAN    date            NOT NULL,
    PEFAE_EDAD      int             NOT NULL,
    PEFAE_PARENT    varchar(50)     NOT NULL,
    CONSTRAINT PK_PEFAE_FAMILIAR PRIMARY KEY CLUSTERED (PEFAE_ID ASC)
);
GO

/*==============================================================*/
/* 5. MÃ“DULO DE PROYECTOS                                       */
/*==============================================================*/

-- Proyectos (depende de Departamentos)
CREATE TABLE dbo.GEPRY_PROYEC
(
    GEPRY_CODIGO    char(4)      NOT NULL,
    PEDEP_CODIGO    char(3)      NOT NULL,
    GEPRY_NOMBRE    varchar(50)  NOT NULL,
    GEPRY_DESCRI    varchar(50)  NOT NULL,
    GEPRY_NUMERO    int          NOT NULL,
    CONSTRAINT PK_GEPRY_PROYEC PRIMARY KEY CLUSTERED (GEPRY_CODIGO ASC)
);
GO

-- RelaciÃ³n Empleado â†” Proyecto (N:M)
CREATE TABLE dbo.GR_GEPRY_PEEMP
(
    PEEMP_CODIGO    char(6)      NOT NULL,
    GEPRY_CODIGO    char(4)      NOT NULL,
    CONSTRAINT PK_GR_GEPRY_PEEMP PRIMARY KEY CLUSTERED (PEEMP_CODIGO ASC, GEPRY_CODIGO ASC)
);
GO

-- Registro de Horas Trabajadas
CREATE TABLE dbo.PEHRT_HORAS
(
    PEHRT_ID        bigint       NOT NULL,
    PEEMP_CODIGO    char(6)      NOT NULL,
    PEHRT_HRINIC    datetime     NOT NULL,
    PEHRT_HRFIN     datetime     NOT NULL,
    CONSTRAINT PK_PEHRT_HORAS PRIMARY KEY CLUSTERED (PEHRT_ID ASC)
);
GO

-- Jefes de Departamento (relaciÃ³n Departamento â†” Empleado con fecha inicio)
CREATE TABLE dbo.RELATIONSHIP_19
(
    PEDEP_CODIGO            char(3)     NOT NULL,
    PEEMP_CODIGO            char(6)     NOT NULL,
    FECHA_INICIO_DIRECCION  datetime    NOT NULL,
    CONSTRAINT PK_RELATIONSHIP_19 PRIMARY KEY CLUSTERED (PEDEP_CODIGO ASC, PEEMP_CODIGO ASC)
);
GO

/*==============================================================*/
/* 6. MÃ“DULO DE SEGURIDAD / USUARIOS                            */
/*==============================================================*/

-- Usuarios del Sistema
CREATE TABLE dbo.XEUSU_USUAR
(
    XEUSU_ID            int             IDENTITY(1,1) NOT NULL,
    XEEST_CODIGO        char(1)         NOT NULL,
    PEEMP_CODIGO        char(6)         NOT NULL,
    XEUSU_LOGIN         varchar(50)     NOT NULL,
    XEUSU_PASWD         varchar(255)    NOT NULL,
    XEUSU_EMAIL         varchar(100)    NOT NULL,
    XEUSU_PIEFIR        varchar(100)    NOT NULL,
    XEUSU_FECCRE        datetime        NOT NULL,
    XEUSU_FECMOD        datetime        NOT NULL,
    XEUSU_PRIMER_INGRESO bit            NOT NULL,
    CONSTRAINT PK_XEUSU_USUAR_ID  PRIMARY KEY CLUSTERED (XEUSU_ID ASC),
    CONSTRAINT UQ_XEUSU_LOGIN     UNIQUE NONCLUSTERED (XEUSU_LOGIN ASC),
    CONSTRAINT UQ_XEUSU_EMAIL     UNIQUE NONCLUSTERED (XEUSU_EMAIL ASC)
);
GO

-- Tokens de RecuperaciÃ³n de ContraseÃ±a
CREATE TABLE dbo.XEUSU_RECUPERACION
(
    ID                  bigint          IDENTITY(1,1) NOT NULL,
    XEUSU_ID            int             NOT NULL,
    TOKEN               varchar(200)    NOT NULL,
    FECHA_CREACION      datetime        NOT NULL,
    FECHA_EXPIRACION    datetime        NOT NULL,
    UTILIZADO           bit             NOT NULL,
    CONSTRAINT PK_XEUSU_RECUPERACION PRIMARY KEY CLUSTERED (ID ASC)
);
GO

-- AsignaciÃ³n de Opciones a Perfiles
CREATE TABLE dbo.XEOXP_OPCPE
(
    XEOPC_CODIGO    char(3)      NOT NULL,
    XEPER_CODIGO    char(8)      NOT NULL,
    XEOXP_FECASI   datetime     NOT NULL,
    XEOXP_FECRET   datetime     NULL,
    CONSTRAINT PK_XEOXP_OPCPE PRIMARY KEY CLUSTERED (XEOPC_CODIGO ASC, XEPER_CODIGO ASC, XEOXP_FECASI ASC)
);
GO

-- AsignaciÃ³n de Perfiles a Usuarios
CREATE TABLE dbo.XEUXP_USUPE
(
    XEUXP_ID        bigint       IDENTITY(1,1) NOT NULL,
    XEPER_CODIGO    char(8)      NOT NULL,
    XEUSU_ID        int          NOT NULL,
    XEUXP_FECASI   datetime     NOT NULL,
    XEUXP_FECRET   datetime     NULL,
    CONSTRAINT PK_XEUXP_USUPE PRIMARY KEY CLUSTERED (XEUXP_ID ASC)
);
GO

/*==============================================================*/
/* 7. RESTRICCIONES DE CLAVE FORÃNEA                            */
/*==============================================================*/

-- â”€â”€ MÃ³dulo Empleados â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

ALTER TABLE dbo.PECAR_CARGO
    ADD CONSTRAINT FK_PEDEP_PECAR
    FOREIGN KEY (PEDEP_CODIGO) REFERENCES dbo.PEDEP_DEPAR (PEDEP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.PEEMP_EMPLE
    ADD CONSTRAINT FK_PECAR_PEEMP
    FOREIGN KEY (PEDEP_CODIGO, PECAR_CODIGO) REFERENCES dbo.PECAR_CARGO (PEDEP_CODIGO, PECAR_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.PEEMP_EMPLE
    ADD CONSTRAINT FK_PEEMP_PEEMP
    FOREIGN KEY (PEE_PEEMP_CODIGO) REFERENCES dbo.PEEMP_EMPLE (PEEMP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.PEEMP_EMPLE
    ADD CONSTRAINT FK_PEESC_PEEMP
    FOREIGN KEY (PEESC_CODIGO) REFERENCES dbo.PEESC_ESTCIV (PEESC_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.PEEMP_EMPLE
    ADD CONSTRAINT FK_PESEX_PEEMP
    FOREIGN KEY (PESEX_CODIGO) REFERENCES dbo.PESEX_SEXO (PESEX_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.PEEMP_EMPLE
    ADD CONSTRAINT FK_PEDIS_PEEMP
    FOREIGN KEY (PEDIS_CODIGO) REFERENCES dbo.PEDIS_DISCAPACIDAD (PEDIS_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.PEEMP_EMPLE
    ADD CONSTRAINT FK_PEINS_PEEMP
    FOREIGN KEY (PEINS_CODIGO) REFERENCES dbo.PEINS_INSTRUCCION (PEINS_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.PEFAE_FAMILIAR
    ADD CONSTRAINT FK_PEEMP_PEFAE
    FOREIGN KEY (PEEMP_CODIGO) REFERENCES dbo.PEEMP_EMPLE (PEEMP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- â”€â”€ MÃ³dulo Proyectos â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

ALTER TABLE dbo.GEPRY_PROYEC
    ADD CONSTRAINT FK_PEDEP_GEPRY
    FOREIGN KEY (PEDEP_CODIGO) REFERENCES dbo.PEDEP_DEPAR (PEDEP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.GR_GEPRY_PEEMP
    ADD CONSTRAINT FK_PEEMP_GRPEEMP
    FOREIGN KEY (PEEMP_CODIGO) REFERENCES dbo.PEEMP_EMPLE (PEEMP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.GR_GEPRY_PEEMP
    ADD CONSTRAINT FK_GEPRY_GRPEEMP
    FOREIGN KEY (GEPRY_CODIGO) REFERENCES dbo.GEPRY_PROYEC (GEPRY_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.PEHRT_HORAS
    ADD CONSTRAINT FK_PEEMP_PEHRT
    FOREIGN KEY (PEEMP_CODIGO) REFERENCES dbo.PEEMP_EMPLE (PEEMP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.RELATIONSHIP_19
    ADD CONSTRAINT FK_PEDEP_REL19
    FOREIGN KEY (PEDEP_CODIGO) REFERENCES dbo.PEDEP_DEPAR (PEDEP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.RELATIONSHIP_19
    ADD CONSTRAINT FK_PEEMP_REL19
    FOREIGN KEY (PEEMP_CODIGO) REFERENCES dbo.PEEMP_EMPLE (PEEMP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- â”€â”€ MÃ³dulo Seguridad / Usuarios â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

ALTER TABLE dbo.XEOPC_OPCIO
    ADD CONSTRAINT FK_XESIS_XEOPC
    FOREIGN KEY (XESIS_CODIGO) REFERENCES dbo.XESIS_SISTE (XESIS_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.XEUSU_USUAR
    ADD CONSTRAINT FK_XEEST_XEUSU
    FOREIGN KEY (XEEST_CODIGO) REFERENCES dbo.XEEST_ESTAD (XEEST_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.XEUSU_USUAR
    ADD CONSTRAINT FK_PEEMP_XEUSU
    FOREIGN KEY (PEEMP_CODIGO) REFERENCES dbo.PEEMP_EMPLE (PEEMP_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.XEUSU_RECUPERACION
    ADD CONSTRAINT FK_XEUSU_RECUP
    FOREIGN KEY (XEUSU_ID) REFERENCES dbo.XEUSU_USUAR (XEUSU_ID)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.XEOXP_OPCPE
    ADD CONSTRAINT FK_XEOPC_XEOXP
    FOREIGN KEY (XEOPC_CODIGO) REFERENCES dbo.XEOPC_OPCIO (XEOPC_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.XEOXP_OPCPE
    ADD CONSTRAINT FK_XEPER_XEOXP
    FOREIGN KEY (XEPER_CODIGO) REFERENCES dbo.XEPER_PERFI (XEPER_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.XEUXP_USUPE
    ADD CONSTRAINT FK_XEPER_XEUXP
    FOREIGN KEY (XEPER_CODIGO) REFERENCES dbo.XEPER_PERFI (XEPER_CODIGO)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE dbo.XEUXP_USUPE
    ADD CONSTRAINT FK_XEUSU_XEUXP
    FOREIGN KEY (XEUSU_ID) REFERENCES dbo.XEUSU_USUAR (XEUSU_ID)
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
