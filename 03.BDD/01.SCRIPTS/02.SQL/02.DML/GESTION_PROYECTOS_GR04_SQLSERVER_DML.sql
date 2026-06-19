/* ========================================================= */
/* DML - INSERCIÃ“N DE DATOS                                  */
/* Tablas: PEDEP_DEPAR, PECAR_CARGO, PEEMP_EMPLE            */
/* ========================================================= */


/* ========================================================= */
/* TABLA: PEDEP_DEPAR                                        */
/* ========================================================= */

INSERT INTO PEDEP_DEPAR (PEDEP_CODIGO, PEDEP_DESCRIP)
VALUES 
('D01', 'Departamento de Sistemas'),
('D02', 'Departamento de Recursos Humanos'),
('D03', 'Departamento Financiero'),
('D04', 'Departamento de Marketing'),
('D05', 'Departamento Comercial'),
('D06', 'Departamento de Soporte'),
('D07', 'Departamento de Produccion'),
('D08', 'Departamento Legal'),
('D09', 'Departamento de Innovacion'),
('D10', 'Departamento de Calidad');
GO


/* ========================================================= */
/* TABLA: PECAR_CARGO                                        */
/* ========================================================= */

INSERT INTO PECAR_CARGO (PEDEP_CODIGO, PECAR_CODIGO, PECAR_DESCRI)
VALUES
('D01', 'C01', 'Desarrollador Backend'),
('D01', 'C02', 'Desarrollador Frontend'),
('D02', 'C01', 'Analista RRHH'),
('D03', 'C01', 'Contador'),
('D04', 'C01', 'DiseÃ±ador Marketing'),
('D05', 'C01', 'Ejecutivo Comercial'),
('D06', 'C01', 'Tecnico Soporte'),
('D07', 'C01', 'Supervisor Produccion'),
('D08', 'C01', 'Asesor Legal'),
('D09', 'C01', 'Investigador Innovacion');
GO


/* ========================================================= */
/* TABLAS DE CATÃLOGOS NUEVOS                                */
/* ========================================================= */

-- 1. CatÃ¡logo de Discapacidades
INSERT INTO PEDIS_DISCAPACIDAD (PEDIS_CODIGO, PEDIS_DESCRI) VALUES
('01', 'FÃ­sica'),
('02', 'Intelectual'),
('03', 'Sensorial'),
('04', 'Auditiva'),
('05', 'Visual'),
('06', 'Mental');
GO

-- 2. CatÃ¡logo de Nivel de InstrucciÃ³n
INSERT INTO PEINS_INSTRUCCION (PEINS_CODIGO, PEINS_DESCRI) VALUES
('01', 'Primaria'),
('02', 'Secundaria'),
('03', 'Tercer Nivel (Pregrado)'),
('04', 'Cuarto Nivel (Postgrado)'),
('05', 'Doctorado (PhD)');
GO


/* ========================================================= */
/* TABLA: PEEMP_EMPLE                                        */
/* ========================================================= */
/*
NOTA:
- PESEX_CODIGO debe existir en PESEX_SEXO
- PEESC_CODIGO debe existir en PEESC_ESTCIV
- PEDIS_CODIGO representa discapacidad (CONADIS)
- PEINS_CODIGO representa nivel de instrucciÃ³n (SENESCYT)
- PEE_PEEMP_CODIGO representa supervisor directo
*/

INSERT INTO PEEMP_EMPLE (
    PEEMP_CODIGO,
    PESEX_CODIGO,
    PEESC_CODIGO,
    PEDEP_CODIGO,
    PECAR_CODIGO,
    PEDIS_CODIGO,
    PEINS_CODIGO,
    PEE_PEEMP_CODIGO,
    PEEMP_APELLI,
    PEEMP_NOMBRE,
    PEEMP_DIREC,
    PEEMP_FECNAC,
    PEEMP_FECSAL,
    PEEMP_TELEF,
    PEEMP_EMAIL,
    PEEMP_CEDULA,
    PEEMP_SALAR,
    PEEMP_ESTADO,
    PEEMP_PORCEN_DISC
)
VALUES
(
    'EMP001',
    'M',
    'S',
    'D01',
    'C01',
    NULL,
    '03',
    NULL,
    'Gonzalez',
    'Carlos',
    'Av. Amazonas 100',
    '1990-05-12',
    '2022-01-15',
    '0991111111',
    'carlos.gonzalez@monster.com',
    '0102030401',
    1800,
    'A',
    0
),

(
    'EMP002',
    'F',
    'C',
    'D01',
    'C02',
    NULL,
    '03',
    'EMP001',
    'Perez',
    'Andrea',
    'Av. Colon 220',
    '1995-03-18',
    '2023-02-01',
    '0992222222',
    'andrea.perez@monster.com',
    '0102030402',
    1500,
    'A',
    0
),

(
    'EMP003',
    'M',
    'S',
    'D02',
    'C01',
    NULL,
    '03',
    NULL,
    'Lopez',
    'Miguel',
    'Cdla. Kennedy',
    '1988-11-02',
    '2021-06-10',
    '0993333333',
    'miguel.lopez@monster.com',
    '0102030403',
    1700,
    'A',
    0
),

(
    'EMP004',
    'F',
    'C',
    'D03',
    'C01',
    NULL,
    '03',
    NULL,
    'Torres',
    'Daniela',
    'Av. Quito 450',
    '1992-07-25',
    '2020-08-20',
    '0994444444',
    'daniela.torres@monster.com',
    '0102030404',
    2000,
    'A',
    0
),

(
    'EMP005',
    'M',
    'S',
    'D04',
    'C01',
    NULL,
    '03',
    NULL,
    'Mendoza',
    'Luis',
    'Av. Central 800',
    '1991-04-10',
    '2021-10-11',
    '0995555555',
    'luis.mendoza@monster.com',
    '0102030405',
    1600,
    'A',
    0
),

(
    'EMP006',
    'F',
    'C',
    'D05',
    'C01',
    NULL,
    '03',
    NULL,
    'Ramirez',
    'Patricia',
    'Av. Sur 900',
    '1994-01-15',
    '2022-04-03',
    '0996666666',
    'patricia.ramirez@monster.com',
    '0102030406',
    1550,
    'A',
    0
),

(
    'EMP007',
    'M',
    'S',
    'D06',
    'C01',
    NULL,
    '03',
    NULL,
    'Vera',
    'Jose',
    'Cdla. Alborada',
    '1987-09-19',
    '2019-09-15',
    '0997777777',
    'jose.vera@monster.com',
    '0102030407',
    1450,
    'A',
    0
),

(
    'EMP008',
    'F',
    'C',
    'D07',
    'C01',
    NULL,
    '03',
    NULL,
    'Castro',
    'Maria',
    'Av. Norte 120',
    '1993-06-08',
    '2021-05-01',
    '0998888888',
    'maria.castro@monster.com',
    '0102030408',
    1750,
    'A',
    0
),

(
    'EMP009',
    'M',
    'S',
    'D08',
    'C01',
    NULL,
    '03',
    NULL,
    'Reyes',
    'Fernando',
    'Barrio Centro',
    '1985-12-14',
    '2018-03-12',
    '0999999999',
    'fernando.reyes@monster.com',
    '0102030409',
    2500,
    'A',
    0
),

(
    'EMP010',
    'F',
    'C',
    'D09',
    'C01',
    NULL,
    '03',
    'EMP001',
    'Jimenez',
    'Sofia',
    'Av. Occidental',
    '1996-08-30',
    '2024-01-05',
    '0981111111',
    'sofia.jimenez@monster.com',
    '0102030410',
    1400,
    'A',
    0
);
GO


/* ========================================================= */
/* TABLAS DE SEGURIDAD Y CONFIGURACIÃ“N                       */
/* ========================================================= */

-- 1. Insertar gÃ©neros si no existen
IF NOT EXISTS (SELECT 1 FROM PESEX_SEXO WHERE PESEX_CODIGO = 'M')
    INSERT INTO PESEX_SEXO (PESEX_CODIGO, PESEX_DESCRIP) VALUES ('M', 'Masculino');
IF NOT EXISTS (SELECT 1 FROM PESEX_SEXO WHERE PESEX_CODIGO = 'F')
    INSERT INTO PESEX_SEXO (PESEX_CODIGO, PESEX_DESCRIP) VALUES ('F', 'Femenino');
GO

-- 2. Insertar estados civiles si no existen
IF NOT EXISTS (SELECT 1 FROM PEESC_ESTCIV WHERE PEESC_CODIGO = 'S')
    INSERT INTO PEESC_ESTCIV (PEESC_CODIGO, PEESC_DESCRIP) VALUES ('S', 'Soltero');
IF NOT EXISTS (SELECT 1 FROM PEESC_ESTCIV WHERE PEESC_CODIGO = 'C')
    INSERT INTO PEESC_ESTCIV (PEESC_CODIGO, PEESC_DESCRIP) VALUES ('C', 'Casado');
GO

-- 3. Insertar estado activo si no existe
IF NOT EXISTS (SELECT 1 FROM XEEST_ESTAD WHERE XEEST_CODIGO = 'A')
    INSERT INTO XEEST_ESTAD (XEEST_CODIGO, XEEST_DESCRI) VALUES ('A', 'Activo');
GO

-- 4. Insertar sistema si no existe
IF NOT EXISTS (SELECT 1 FROM XESIS_SISTE WHERE XESIS_CODIGO = 'S')
    INSERT INTO XESIS_SISTE (XESIS_CODIGO, XESIS_DESCRI) VALUES ('S', 'Sistema de GestiÃ³n');
GO

-- 5. Insertar opciones del sistema si no existen (Requeridas por polÃ­ticas de autorizaciÃ³n)
IF NOT EXISTS (SELECT 1 FROM XEOPC_OPCIO WHERE XEOPC_CODIGO = 'USR')
    INSERT INTO XEOPC_OPCIO (XEOPC_CODIGO, XESIS_CODIGO, XEOPC_DESCRI) VALUES ('USR', 'S', 'GestiÃ³n de Usuarios');
IF NOT EXISTS (SELECT 1 FROM XEOPC_OPCIO WHERE XEOPC_CODIGO = 'EMP')
    INSERT INTO XEOPC_OPCIO (XEOPC_CODIGO, XESIS_CODIGO, XEOPC_DESCRI) VALUES ('EMP', 'S', 'GestiÃ³n de Empleados');
IF NOT EXISTS (SELECT 1 FROM XEOPC_OPCIO WHERE XEOPC_CODIGO = 'PRO')
    INSERT INTO XEOPC_OPCIO (XEOPC_CODIGO, XESIS_CODIGO, XEOPC_DESCRI) VALUES ('PRO', 'S', 'GestiÃ³n de Proyectos');
IF NOT EXISTS (SELECT 1 FROM XEOPC_OPCIO WHERE XEOPC_CODIGO = 'REP')
    INSERT INTO XEOPC_OPCIO (XEOPC_CODIGO, XESIS_CODIGO, XEOPC_DESCRI) VALUES ('REP', 'S', 'GestiÃ³n de Reportes');
IF NOT EXISTS (SELECT 1 FROM XEOPC_OPCIO WHERE XEOPC_CODIGO = 'PER')
    INSERT INTO XEOPC_OPCIO (XEOPC_CODIGO, XESIS_CODIGO, XEOPC_DESCRI) VALUES ('PER', 'S', 'GestiÃ³n de Perfiles y Permisos');
GO

-- 6. Insertar perfil administrador si no existe
IF NOT EXISTS (SELECT 1 FROM XEPER_PERFI WHERE XEPER_CODIGO = 'ADMIN')
    INSERT INTO XEPER_PERFI (XEPER_CODIGO, XEPER_DESCRI, XEPER_OBSER) 
    VALUES ('ADMIN', 'Administrador', 'Perfil con acceso completo al sistema');
GO

-- 7. Asociar opciones al perfil administrador si no existen
IF NOT EXISTS (SELECT 1 FROM XEOXP_OPCPE WHERE XEOPC_CODIGO = 'USR' AND XEPER_CODIGO = 'ADMIN')
    INSERT INTO XEOXP_OPCPE (XEOPC_CODIGO, XEPER_CODIGO, XEOXP_FECASI, XEOXP_FECRET) VALUES ('USR', 'ADMIN', GETDATE(), NULL);
IF NOT EXISTS (SELECT 1 FROM XEOXP_OPCPE WHERE XEOPC_CODIGO = 'EMP' AND XEPER_CODIGO = 'ADMIN')
    INSERT INTO XEOXP_OPCPE (XEOPC_CODIGO, XEPER_CODIGO, XEOXP_FECASI, XEOXP_FECRET) VALUES ('EMP', 'ADMIN', GETDATE(), NULL);
IF NOT EXISTS (SELECT 1 FROM XEOXP_OPCPE WHERE XEOPC_CODIGO = 'PRO' AND XEPER_CODIGO = 'ADMIN')
    INSERT INTO XEOXP_OPCPE (XEOPC_CODIGO, XEPER_CODIGO, XEOXP_FECASI, XEOXP_FECRET) VALUES ('PRO', 'ADMIN', GETDATE(), NULL);
IF NOT EXISTS (SELECT 1 FROM XEOXP_OPCPE WHERE XEOPC_CODIGO = 'REP' AND XEPER_CODIGO = 'ADMIN')
    INSERT INTO XEOXP_OPCPE (XEOPC_CODIGO, XEPER_CODIGO, XEOXP_FECASI, XEOXP_FECRET) VALUES ('REP', 'ADMIN', GETDATE(), NULL);
IF NOT EXISTS (SELECT 1 FROM XEOXP_OPCPE WHERE XEOPC_CODIGO = 'PER' AND XEPER_CODIGO = 'ADMIN')
    INSERT INTO XEOXP_OPCPE (XEOPC_CODIGO, XEPER_CODIGO, XEOXP_FECASI, XEOXP_FECRET) VALUES ('PER', 'ADMIN', GETDATE(), NULL);
GO

-- 8. Insertar empleado administrador si no existe (asociado a Sistemas D01 y Desarrollador C01)
IF NOT EXISTS (SELECT 1 FROM PEEMP_EMPLE WHERE PEEMP_CODIGO = 'EMP000')
    INSERT INTO PEEMP_EMPLE (
        PEEMP_CODIGO, PESEX_CODIGO, PEESC_CODIGO, PEDEP_CODIGO, PECAR_CODIGO,
        PEDIS_CODIGO, PEINS_CODIGO, PEE_PEEMP_CODIGO, PEEMP_APELLI, PEEMP_NOMBRE,
        PEEMP_DIREC, PEEMP_FECNAC, PEEMP_FECSAL, PEEMP_TELEF, PEEMP_EMAIL,
        PEEMP_CEDULA, PEEMP_SALAR, PEEMP_ESTADO, PEEMP_PORCEN_DISC
    ) VALUES (
        'EMP000', 'M', 'S', 'D01', 'C01',
        NULL, '03', NULL, 'Administrador', 'Usuario',
        'DirecciÃ³n General', '1990-01-01', '2026-01-01', '0999999999', 'admin@monster.com',
        '9999999999', 3000, 'A', 0
    );
GO

-- 9. Insertar usuario administrador si no existe (Login: admin, Password: Admin123*)
-- Hash BCrypt de "Admin123*": ..FGKTVrW05Gz/7WR8tHmZoIlhzO/AIy
IF NOT EXISTS (SELECT 1 FROM XEUSU_USUAR WHERE XEUSU_LOGIN = 'admin')
    INSERT INTO XEUSU_USUAR (
        XEUSU_PASWD, XEEST_CODIGO, PEEMP_CODIGO, XEUSU_FECCRE, XEUSU_FECMOD, 
        XEUSU_PIEFIR, XEUSU_LOGIN, XEUSU_EMAIL, XEUSU_PRIMER_INGRESO
    ) VALUES (
        '..FGKTVrW05Gz/7WR8tHmZoIlhzO/AIy', 
        'A', 'EMP000', GETDATE(), GETDATE(), 'Firma Admin', 'admin', 'admin@monster.com', 0
    );
GO

-- 10. Asignar perfil de Administrador al usuario si no tiene perfil asignado
DECLARE @UserId INT;
SELECT @UserId = XEUSU_ID FROM XEUSU_USUAR WHERE XEUSU_LOGIN = 'admin';

IF @UserId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM XEUXP_USUPE WHERE XEUSU_ID = @UserId AND XEPER_CODIGO = 'ADMIN')
    INSERT INTO XEUXP_USUPE (XEPER_CODIGO, XEUXP_FECASI, XEUXP_FECRET, XEUSU_ID)
    VALUES ('ADMIN', GETDATE(), NULL, @UserId);
GO
