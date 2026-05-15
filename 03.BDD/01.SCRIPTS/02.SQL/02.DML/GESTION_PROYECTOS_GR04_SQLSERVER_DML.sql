/* ========================================================= */
/* DML - INSERCIÓN DE DATOS                                  */
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
('D04', 'C01', 'Diseñador Marketing'),
('D05', 'C01', 'Ejecutivo Comercial'),
('D06', 'C01', 'Tecnico Soporte'),
('D07', 'C01', 'Supervisor Produccion'),
('D08', 'C01', 'Asesor Legal'),
('D09', 'C01', 'Investigador Innovacion');
GO


/* ========================================================= */
/* TABLA: PEEMP_EMPLE                                        */
/* ========================================================= */
/*
NOTA:
- PESEX_CODIGO debe existir en PESEX_SEXO
- PEESC_CODIGO debe existir en PEESC_ESTCIV
- PEE_PEEMP_CODIGO representa supervisor directo
*/

INSERT INTO PEEMP_EMPLE (
    PEEMP_CODIGO,
    PESEX_CODIGO,
    PEESC_CODIGO,
    PEDEP_CODIGO,
    PECAR_CODIGO,
    PEE_PEEMP_CODIGO,
    PEEMP_APELLI,
    PEEMP_NOMBRE,
    PEEMP_DIREC,
    PEEMP_FECNAC,
    PEEMP_FECSAL,
    PEEMP_TELEF,
    PEEMP_EMAIL,
    PEEMP_CEDULA,
    PEEMP_SALAR
)
VALUES
(
    'EMP001',
    'M',
    'S',
    'D01',
    'C01',
    NULL,
    'Gonzalez',
    'Carlos',
    'Av. Amazonas 100',
    '1990-05-12',
    '2022-01-15',
    '0991111111',
    'carlos.gonzalez@monster.com',
    '0102030401',
    1800
),

(
    'EMP002',
    'F',
    'C',
    'D01',
    'C02',
    'EMP001',
    'Perez',
    'Andrea',
    'Av. Colon 220',
    '1995-03-18',
    '2023-02-01',
    '0992222222',
    'andrea.perez@monster.com',
    '0102030402',
    1500
),

(
    'EMP003',
    'M',
    'S',
    'D02',
    'C01',
    NULL,
    'Lopez',
    'Miguel',
    'Cdla. Kennedy',
    '1988-11-02',
    '2021-06-10',
    '0993333333',
    'miguel.lopez@monster.com',
    '0102030403',
    1700
),

(
    'EMP004',
    'F',
    'C',
    'D03',
    'C01',
    NULL,
    'Torres',
    'Daniela',
    'Av. Quito 450',
    '1992-07-25',
    '2020-08-20',
    '0994444444',
    'daniela.torres@monster.com',
    '0102030404',
    2000
),

(
    'EMP005',
    'M',
    'S',
    'D04',
    'C01',
    NULL,
    'Mendoza',
    'Luis',
    'Av. Central 800',
    '1991-04-10',
    '2021-10-11',
    '0995555555',
    'luis.mendoza@monster.com',
    '0102030405',
    1600
),

(
    'EMP006',
    'F',
    'C',
    'D05',
    'C01',
    NULL,
    'Ramirez',
    'Patricia',
    'Av. Sur 900',
    '1994-01-15',
    '2022-04-03',
    '0996666666',
    'patricia.ramirez@monster.com',
    '0102030406',
    1550
),

(
    'EMP007',
    'M',
    'S',
    'D06',
    'C01',
    NULL,
    'Vera',
    'Jose',
    'Cdla. Alborada',
    '1987-09-19',
    '2019-09-15',
    '0997777777',
    'jose.vera@monster.com',
    '0102030407',
    1450
),

(
    'EMP008',
    'F',
    'C',
    'D07',
    'C01',
    NULL,
    'Castro',
    'Maria',
    'Av. Norte 120',
    '1993-06-08',
    '2021-05-01',
    '0998888888',
    'maria.castro@monster.com',
    '0102030408',
    1750
),

(
    'EMP009',
    'M',
    'S',
    'D08',
    'C01',
    NULL,
    'Reyes',
    'Fernando',
    'Barrio Centro',
    '1985-12-14',
    '2018-03-12',
    '0999999999',
    'fernando.reyes@monster.com',
    '0102030409',
    2500
),

(
    'EMP010',
    'F',
    'C',
    'D09',
    'C01',
    'EMP001',
    'Jimenez',
    'Sofia',
    'Av. Occidental',
    '1996-08-30',
    '2024-01-05',
    '0981111111',
    'sofia.jimenez@monster.com',
    '0102030410',
    1400
);
GO