/* ========================================================= */
/* DML MYSQL - INSERCIÓN DE DATOS                            */
/* Tablas: PEDEP_DEPAR, GEPRY_PROYEC, PEEMP_EMPLE           */
/* ========================================================= */


/* ========================================================= */
/* TABLA: PEDEP_DEPAR                                        */
/* ========================================================= */

INSERT INTO PEDEP_DEPAR (PEDEP_CODIGO, PEDEP_DESCRIP)
VALUES
('D01', 'Departamento de Sistemas'),
('D02', 'Departamento de Recursos Humanos'),
('D03', 'Departamento Financiero'),
('D04', 'Departamento Marketing'),
('D05', 'Departamento Comercial'),
('D06', 'Departamento Soporte'),
('D07', 'Departamento Produccion'),
('D08', 'Departamento Legal'),
('D09', 'Departamento Innovacion'),
('D10', 'Departamento Calidad');


/* ========================================================= */
/* TABLA: GEPRY_PROYEC                                       */
/* ========================================================= */

INSERT INTO GEPRY_PROYEC (
    GEPRY_CODIGO,
    PEDEP_CODIGO,
    GEPRY_NOMBRE,
    GEPRY_DESCRI,
    GEPRY_NUMERO
)
VALUES
('P001', 'D01', 'Sistema ERP', 'Proyecto ERP Empresarial', 1001),
('P002', 'D02', 'Portal RRHH', 'Gestion Recursos Humanos', 1002),
('P003', 'D03', 'Sistema Contable', 'Control Financiero', 1003),
('P004', 'D04', 'Campaña Digital', 'Marketing Redes Sociales', 1004),
('P005', 'D05', 'CRM Ventas', 'Gestion Comercial', 1005),
('P006', 'D06', 'Mesa Ayuda', 'Sistema Soporte Tecnico', 1006),
('P007', 'D07', 'Control Produccion', 'Gestion Produccion', 1007),
('P008', 'D08', 'Sistema Legal', 'Gestion Documental Legal', 1008),
('P009', 'D09', 'Laboratorio IA', 'Innovacion Inteligencia Artificial', 1009),
('P010', 'D10', 'Auditoria Calidad', 'Control Calidad ISO', 1010);


/* ========================================================= */
/* DATOS NECESARIOS PARA EMPLEADOS                           */
/* ========================================================= */

INSERT INTO PESEX_SEXO VALUES
('M', 'Masculino'),
('F', 'Femenino');

INSERT INTO PEESC_ESTCIV VALUES
('S', 'Soltero'),
('C', 'Casado');


/* ========================================================= */
/* TABLA: PECAR_CARGO                                        */
/* ========================================================= */

INSERT INTO PECAR_CARGO (
    PEDEP_CODIGO,
    PECAR_CODIGO,
    PECAR_DESCRI
)
VALUES
('D01', 'C01', 'Desarrollador Backend'),
('D02', 'C01', 'Analista RRHH'),
('D03', 'C01', 'Contador'),
('D04', 'C01', 'Diseñador Marketing'),
('D05', 'C01', 'Ejecutivo Comercial'),
('D06', 'C01', 'Tecnico Soporte'),
('D07', 'C01', 'Supervisor Produccion'),
('D08', 'C01', 'Asesor Legal'),
('D09', 'C01', 'Investigador IA'),
('D10', 'C01', 'Auditor Calidad');


/* ========================================================= */
/* TABLA: PEEMP_EMPLE                                        */
/* ========================================================= */

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
    'Av Amazonas 100',
    '1990-05-10',
    '2022-01-15',
    '0991111111',
    'carlos@monster.com',
    '0101010101',
    1800
),

(
    'EMP002',
    'F',
    'C',
    'D02',
    'C01',
    'EMP001',
    'Perez',
    'Andrea',
    'Av Colon 200',
    '1992-08-12',
    '2021-03-01',
    '0992222222',
    'andrea@monster.com',
    '0101010102',
    1600
),

(
    'EMP003',
    'M',
    'S',
    'D03',
    'C01',
    NULL,
    'Lopez',
    'Miguel',
    'Cdla Kennedy',
    '1988-11-20',
    '2020-07-18',
    '0993333333',
    'miguel@monster.com',
    '0101010103',
    2100
),

(
    'EMP004',
    'F',
    'C',
    'D04',
    'C01',
    NULL,
    'Torres',
    'Daniela',
    'Av Quito 300',
    '1994-04-02',
    '2023-02-11',
    '0994444444',
    'daniela@monster.com',
    '0101010104',
    1500
),

(
    'EMP005',
    'M',
    'S',
    'D05',
    'C01',
    NULL,
    'Mendoza',
    'Luis',
    'Barrio Central',
    '1991-09-15',
    '2019-10-10',
    '0995555555',
    'luis@monster.com',
    '0101010105',
    1700
),

(
    'EMP006',
    'F',
    'C',
    'D06',
    'C01',
    'EMP001',
    'Ramirez',
    'Patricia',
    'Av Occidental',
    '1995-06-25',
    '2022-06-01',
    '0996666666',
    'patricia@monster.com',
    '0101010106',
    1450
),

(
    'EMP007',
    'M',
    'S',
    'D07',
    'C01',
    NULL,
    'Vera',
    'Jose',
    'Cdla Alborada',
    '1987-01-30',
    '2018-08-20',
    '0997777777',
    'jose@monster.com',
    '0101010107',
    2300
),

(
    'EMP008',
    'F',
    'C',
    'D08',
    'C01',
    NULL,
    'Castro',
    'Maria',
    'Av Norte 500',
    '1993-03-18',
    '2021-11-12',
    '0998888888',
    'maria@monster.com',
    '0101010108',
    2500
),

(
    'EMP009',
    'M',
    'S',
    'D09',
    'C01',
    NULL,
    'Reyes',
    'Fernando',
    'Sector Sur',
    '1996-12-10',
    '2024-01-05',
    '0999999999',
    'fernando@monster.com',
    '0101010109',
    1900
),

(
    'EMP010',
    'F',
    'C',
    'D10',
    'C01',
    'EMP003',
    'Jimenez',
    'Sofia',
    'Av Central 100',
    '1997-07-07',
    '2023-09-01',
    '0981111111',
    'sofia@monster.com',
    '0101010110',
    1750
);


/* ========================================================= */
/* TABLAS DE SEGURIDAD Y CONFIGURACIÓN                       */
/* ========================================================= */

-- 1. Insertar géneros si no existen
INSERT IGNORE INTO PESEX_SEXO (PESEX_CODIGO, PESEX_DESCRIP) VALUES 
('M', 'Masculino'),
('F', 'Femenino');

-- 2. Insertar estados civiles si no existen
INSERT IGNORE INTO PEESC_ESTCIV (PEESC_CODIGO, PEESC_DESCRIP) VALUES 
('S', 'Soltero'),
('C', 'Casado');

-- 3. Insertar estado activo si no existe
INSERT IGNORE INTO XEEST_ESTAD (XEEST_CODIGO, XEEST_DESCRI) VALUES 
('A', 'Activo');

-- 4. Insertar sistema si no existe
INSERT IGNORE INTO XESIS_SISTE (XESIS_CODIGO, XESIS_DESCRI) VALUES 
('S', 'Sistema de Gestión');

-- 5. Insertar opciones del sistema si no existen (Requeridas por políticas de autorización)
INSERT IGNORE INTO XEOPC_OPCIO (XEOPC_CODIGO, XESIS_CODIGO, XEOPC_DESCRI) VALUES 
('USR', 'S', 'Gestión de Usuarios'),
('EMP', 'S', 'Gestión de Empleados'),
('PRO', 'S', 'Gestión de Proyectos'),
('REP', 'S', 'Gestión de Reportes'),
('PER', 'S', 'Gestión de Perfiles y Permisos');

-- 6. Insertar perfil administrador si no existe
INSERT IGNORE INTO XEPER_PERFI (XEPER_CODIGO, XEPER_DESCRI, XEPER_OBSER) VALUES 
('ADMIN', 'Administrador', 'Perfil con acceso completo al sistema');

-- 7. Asociar opciones al perfil administrador si no existen
INSERT IGNORE INTO XEOXP_OPCPE (XEOPC_CODIGO, XEPER_CODIGO, XEOXP_FECASI, XEOXP_FECRET) VALUES 
('USR', 'ADMIN', NOW(), NULL),
('EMP', 'ADMIN', NOW(), NULL),
('PRO', 'ADMIN', NOW(), NULL),
('REP', 'ADMIN', NOW(), NULL),
('PER', 'ADMIN', NOW(), NULL);

-- 8. Insertar empleado administrador si no existe (asociado a Sistemas D01 y Desarrollador C01)
INSERT IGNORE INTO PEEMP_EMPLE (
    PEEMP_CODIGO, PESEX_CODIGO, PEESC_CODIGO, PEDEP_CODIGO, PECAR_CODIGO,
    PEE_PEEMP_CODIGO, PEEMP_APELLI, PEEMP_NOMBRE, PEEMP_DIREC, PEEMP_FECNAC,
    PEEMP_FECSAL, PEEMP_TELEF, PEEMP_EMAIL, PEEMP_CEDULA, PEEMP_SALAR
) VALUES (
    'EMP000', 'M', 'S', 'D01', 'C01',
    NULL, 'Administrador', 'Usuario', 'Dirección General', '1990-01-01',
    NULL, '0999999999', 'admin@monster.com', '9999999999', 3000.00
);

-- 9. Insertar usuario administrador si no existe (Login: admin, Password: Admin123*)
-- Hash BCrypt de "Admin123*": $2a$11$mKpPwycUyKdywzKkeuC6V..FGKTVrW05Gz/7WR8tHmZoIlhzO/AIy
INSERT IGNORE INTO XEUSU_USUAR (
    XEUSU_PASWD, XEEST_CODIGO, PEEMP_CODIGO, XEUSU_FECCRE, XEUSU_FECMOD, 
    XEUSU_PIEFIR, XEUSU_LOGIN, XEUSU_EMAIL, XEUSU_PRIMER_INGRESO
) VALUES (
    '$2a$11$mKpPwycUyKdywzKkeuC6V..FGKTVrW05Gz/7WR8tHmZoIlhzO/AIy', 
    'A', 'EMP000', NOW(), NOW(), 'Firma Admin', 'admin', 'admin@monster.com', 0
);

-- 10. Asignar perfil de Administrador al usuario si no tiene perfil asignado
INSERT IGNORE INTO XEUXP_USUPE (XEPER_CODIGO, XEUXP_FECASI, XEUXP_FECRET, XEUSU_ID)
SELECT 'ADMIN', NOW(), NULL, XEUSU_ID 
FROM XEUSU_USUAR 
WHERE XEUSU_LOGIN = 'admin'
ON DUPLICATE KEY UPDATE XEPER_CODIGO = 'ADMIN';