/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     2026-05-15 5:22:35 AM                        */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GEPRY_PROYEC') and o.name = 'FK_GEPRY_PR_DEPAR_PRO_PEDEP_DE')
alter table GEPRY_PROYEC
   drop constraint FK_GEPRY_PR_DEPAR_PRO_PEDEP_DE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GR_GEPRY_PEEMP') and o.name = 'FK_GR_GEPRY_GR_GEPRY__PEEMP_EM')
alter table GR_GEPRY_PEEMP
   drop constraint FK_GR_GEPRY_GR_GEPRY__PEEMP_EM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GR_GEPRY_PEEMP') and o.name = 'FK_GR_GEPRY_GR_GEPRY__GEPRY_PR')
alter table GR_GEPRY_PEEMP
   drop constraint FK_GR_GEPRY_GR_GEPRY__GEPRY_PR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PECAR_CARGO') and o.name = 'FK_PECAR_CA_PR_PEDEP__PEDEP_DE')
alter table PECAR_CARGO
   drop constraint FK_PECAR_CA_PR_PEDEP__PEDEP_DE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PEEMP_EMPLE') and o.name = 'FK_PEEMP_EM_PR_PECAR__PECAR_CA')
alter table PEEMP_EMPLE
   drop constraint FK_PEEMP_EM_PR_PECAR__PECAR_CA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PEEMP_EMPLE') and o.name = 'FK_PEEMP_EM_PR_PEEMP__PEEMP_EM')
alter table PEEMP_EMPLE
   drop constraint FK_PEEMP_EM_PR_PEEMP__PEEMP_EM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PEEMP_EMPLE') and o.name = 'FK_PEEMP_EM_PR_PEESC__PEESC_ES')
alter table PEEMP_EMPLE
   drop constraint FK_PEEMP_EM_PR_PEESC__PEESC_ES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PEEMP_EMPLE') and o.name = 'FK_PEEMP_EM_PR_PESEX__PESEX_SE')
alter table PEEMP_EMPLE
   drop constraint FK_PEEMP_EM_PR_PESEX__PESEX_SE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PEFAE_FAMILIAR') and o.name = 'FK_PEFAE_FA_PR_PEEMP__PEEMP_EM')
alter table PEFAE_FAMILIAR
   drop constraint FK_PEFAE_FA_PR_PEEMP__PEEMP_EM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PEHRT_HORAS') and o.name = 'FK_PEHRT_HO_RELATIONS_PEEMP_EM')
alter table PEHRT_HORAS
   drop constraint FK_PEHRT_HO_RELATIONS_PEEMP_EM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RELATIONSHIP_19') and o.name = 'FK_RELATION_RELATIONS_PEDEP_DE')
alter table RELATIONSHIP_19
   drop constraint FK_RELATION_RELATIONS_PEDEP_DE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RELATIONSHIP_19') and o.name = 'FK_RELATION_RELATIONS_PEEMP_EM')
alter table RELATIONSHIP_19
   drop constraint FK_RELATION_RELATIONS_PEEMP_EM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('XEOPC_OPCIO') and o.name = 'FK_XEOPC_OP_XR_XESIS__XESIS_SI')
alter table XEOPC_OPCIO
   drop constraint FK_XEOPC_OP_XR_XESIS__XESIS_SI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('XEOXP_OPCPE') and o.name = 'FK_XEOXP_OP_XR_XEOPC__XEOPC_OP')
alter table XEOXP_OPCPE
   drop constraint FK_XEOXP_OP_XR_XEOPC__XEOPC_OP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('XEOXP_OPCPE') and o.name = 'FK_XEOXP_OP_XR_XEPER__XEPER_PE')
alter table XEOXP_OPCPE
   drop constraint FK_XEOXP_OP_XR_XEPER__XEPER_PE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('XEUSU_USUAR') and o.name = 'FK_XEUSU_US_XR_PEEM_X_PEEMP_EM')
alter table XEUSU_USUAR
   drop constraint FK_XEUSU_US_XR_PEEM_X_PEEMP_EM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('XEUSU_USUAR') and o.name = 'FK_XEUSU_US_XR_XEEST__XEEST_ES')
alter table XEUSU_USUAR
   drop constraint FK_XEUSU_US_XR_XEEST__XEEST_ES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('XEUXP_USUPE') and o.name = 'FK_XEUXP_US_XR_XEPER__XEPER_PE')
alter table XEUXP_USUPE
   drop constraint FK_XEUXP_US_XR_XEPER__XEPER_PE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('XEUXP_USUPE') and o.name = 'FK_XEUXP_US_XR_XEUSU__XEUSU_US')
alter table XEUXP_USUPE
   drop constraint FK_XEUXP_US_XR_XEUSU__XEUSU_US
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GEPRY_PROYEC')
            and   name  = 'DEPAR_PROYECT_FK'
            and   indid > 0
            and   indid < 255)
   drop index GEPRY_PROYEC.DEPAR_PROYECT_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GEPRY_PROYEC')
            and   type = 'U')
   drop table GEPRY_PROYEC
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GR_GEPRY_PEEMP')
            and   name  = 'GR_GEPRY_PEEMP_FK'
            and   indid > 0
            and   indid < 255)
   drop index GR_GEPRY_PEEMP.GR_GEPRY_PEEMP_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GR_GEPRY_PEEMP')
            and   name  = 'GR_GEPRY_PEEMP2_FK'
            and   indid > 0
            and   indid < 255)
   drop index GR_GEPRY_PEEMP.GR_GEPRY_PEEMP2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GR_GEPRY_PEEMP')
            and   type = 'U')
   drop table GR_GEPRY_PEEMP
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PECAR_CARGO')
            and   name  = 'PR_PEDEP_PECAR_FK'
            and   indid > 0
            and   indid < 255)
   drop index PECAR_CARGO.PR_PEDEP_PECAR_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PECAR_CARGO')
            and   type = 'U')
   drop table PECAR_CARGO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PEDEP_DEPAR')
            and   type = 'U')
   drop table PEDEP_DEPAR
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PEEMP_EMPLE')
            and   name  = 'PR_PEEMP_PEEMP_FK'
            and   indid > 0
            and   indid < 255)
   drop index PEEMP_EMPLE.PR_PEEMP_PEEMP_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PEEMP_EMPLE')
            and   name  = 'PR_PECAR_PEEMP__FK'
            and   indid > 0
            and   indid < 255)
   drop index PEEMP_EMPLE.PR_PECAR_PEEMP__FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PEEMP_EMPLE')
            and   name  = 'PR_PEESC_PEEMP_FK'
            and   indid > 0
            and   indid < 255)
   drop index PEEMP_EMPLE.PR_PEESC_PEEMP_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PEEMP_EMPLE')
            and   name  = 'PR_PESEX_PEEMP_FK'
            and   indid > 0
            and   indid < 255)
   drop index PEEMP_EMPLE.PR_PESEX_PEEMP_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PEEMP_EMPLE')
            and   type = 'U')
   drop table PEEMP_EMPLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PEESC_ESTCIV')
            and   type = 'U')
   drop table PEESC_ESTCIV
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PEFAE_FAMILIAR')
            and   name  = 'PR_PEEMP_PEFAE_FK'
            and   indid > 0
            and   indid < 255)
   drop index PEFAE_FAMILIAR.PR_PEEMP_PEFAE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PEFAE_FAMILIAR')
            and   type = 'U')
   drop table PEFAE_FAMILIAR
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PEHRT_HORAS')
            and   name  = 'RELATIONSHIP_21_FK'
            and   indid > 0
            and   indid < 255)
   drop index PEHRT_HORAS.RELATIONSHIP_21_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PEHRT_HORAS')
            and   type = 'U')
   drop table PEHRT_HORAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PESEX_SEXO')
            and   type = 'U')
   drop table PESEX_SEXO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RELATIONSHIP_19')
            and   name  = 'RELATIONSHIP_19_FK'
            and   indid > 0
            and   indid < 255)
   drop index RELATIONSHIP_19.RELATIONSHIP_19_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RELATIONSHIP_19')
            and   name  = 'RELATIONSHIP_20_FK'
            and   indid > 0
            and   indid < 255)
   drop index RELATIONSHIP_19.RELATIONSHIP_20_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RELATIONSHIP_19')
            and   type = 'U')
   drop table RELATIONSHIP_19
go

if exists (select 1
            from  sysobjects
           where  id = object_id('XEEST_ESTAD')
            and   type = 'U')
   drop table XEEST_ESTAD
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('XEOPC_OPCIO')
            and   name  = 'XR_XESIS_XEOPC_FK'
            and   indid > 0
            and   indid < 255)
   drop index XEOPC_OPCIO.XR_XESIS_XEOPC_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('XEOPC_OPCIO')
            and   type = 'U')
   drop table XEOPC_OPCIO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('XEOXP_OPCPE')
            and   name  = 'XR_XEOPC_XEOXP_FK'
            and   indid > 0
            and   indid < 255)
   drop index XEOXP_OPCPE.XR_XEOPC_XEOXP_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('XEOXP_OPCPE')
            and   name  = 'XR_XEPER_XEOXP_FK'
            and   indid > 0
            and   indid < 255)
   drop index XEOXP_OPCPE.XR_XEPER_XEOXP_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('XEOXP_OPCPE')
            and   type = 'U')
   drop table XEOXP_OPCPE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('XEPER_PERFI')
            and   type = 'U')
   drop table XEPER_PERFI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('XESIS_SISTE')
            and   type = 'U')
   drop table XESIS_SISTE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('XEUSU_USUAR')
            and   name  = 'XR_PEEM_XEUSU_FK'
            and   indid > 0
            and   indid < 255)
   drop index XEUSU_USUAR.XR_PEEM_XEUSU_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('XEUSU_USUAR')
            and   name  = 'XR_XEEST_XEUSU_FK'
            and   indid > 0
            and   indid < 255)
   drop index XEUSU_USUAR.XR_XEEST_XEUSU_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('XEUSU_USUAR')
            and   type = 'U')
   drop table XEUSU_USUAR
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('XEUXP_USUPE')
            and   name  = 'XR_XEUSU_XEUXP_FK'
            and   indid > 0
            and   indid < 255)
   drop index XEUXP_USUPE.XR_XEUSU_XEUXP_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('XEUXP_USUPE')
            and   name  = 'XR_XEPER_XEUXP_FK'
            and   indid > 0
            and   indid < 255)
   drop index XEUXP_USUPE.XR_XEPER_XEUXP_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('XEUXP_USUPE')
            and   type = 'U')
   drop table XEUXP_USUPE
go

/*==============================================================*/
/* Table: GEPRY_PROYEC                                          */
/*==============================================================*/
create table GEPRY_PROYEC (
   GEPRY_CODIGO         char(4)              not null,
   PEDEP_CODIGO         char(3)              not null,
   GEPRY_NOMBRE         varchar(50)          not null,
   GEPRY_DESCRI         varchar(50)          not null,
   GEPRY_NUMERO         int                  not null,
   constraint PK_GEPRY_PROYEC primary key (GEPRY_CODIGO)
)
go

/*==============================================================*/
/* Index: DEPAR_PROYECT_FK                                      */
/*==============================================================*/




create nonclustered index DEPAR_PROYECT_FK on GEPRY_PROYEC (PEDEP_CODIGO ASC)
go

/*==============================================================*/
/* Table: GR_GEPRY_PEEMP                                        */
/*==============================================================*/
create table GR_GEPRY_PEEMP (
   PEEMP_CODIGO         char(6)              not null,
   GEPRY_CODIGO         char(4)              not null,
   constraint PK_GR_GEPRY_PEEMP primary key (PEEMP_CODIGO, GEPRY_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('GR_GEPRY_PEEMP') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'GR_GEPRY_PEEMP' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Relación entre las tablas GEPRY_PROYEC y  PEEMP_EMPLE', 
   'user', @CurrentUser, 'table', 'GR_GEPRY_PEEMP'
go

/*==============================================================*/
/* Index: GR_GEPRY_PEEMP2_FK                                    */
/*==============================================================*/




create nonclustered index GR_GEPRY_PEEMP2_FK on GR_GEPRY_PEEMP (GEPRY_CODIGO ASC)
go

/*==============================================================*/
/* Index: GR_GEPRY_PEEMP_FK                                     */
/*==============================================================*/




create nonclustered index GR_GEPRY_PEEMP_FK on GR_GEPRY_PEEMP (PEEMP_CODIGO ASC)
go

/*==============================================================*/
/* Table: PECAR_CARGO                                           */
/*==============================================================*/
create table PECAR_CARGO (
   PEDEP_CODIGO         char(3)              not null,
   PECAR_CODIGO         char(3)              not null,
   PECAR_DESCRI         varchar(50)          not null,
   constraint PK_PECAR_CARGO primary key (PEDEP_CODIGO, PECAR_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PECAR_CARGO') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PECAR_CARGO' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para la gestión de los diferentes CARGOSque pertenece a un DEPARTAMENTO de una EMPRESA', 
   'user', @CurrentUser, 'table', 'PECAR_CARGO'
go

/*==============================================================*/
/* Index: PR_PEDEP_PECAR_FK                                     */
/*==============================================================*/




create nonclustered index PR_PEDEP_PECAR_FK on PECAR_CARGO (PEDEP_CODIGO ASC)
go

/*==============================================================*/
/* Table: PEDEP_DEPAR                                           */
/*==============================================================*/
create table PEDEP_DEPAR (
   PEDEP_CODIGO         char(3)              not null,
   PEDEP_DESCRIP        varchar(50)          not null,
   constraint PK_PEDEP_DEPAR primary key (PEDEP_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PEDEP_DEPAR') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PEDEP_DEPAR' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para realizar la gestión de los diferentes DEPARTAMENTOS de una EMPRESA
   ', 
   'user', @CurrentUser, 'table', 'PEDEP_DEPAR'
go

/*==============================================================*/
/* Table: PEEMP_EMPLE                                           */
/*==============================================================*/
create table PEEMP_EMPLE (
   PEEMP_CODIGO         char(6)              not null,
   PESEX_CODIGO         char(1)              not null,
   PEESC_CODIGO         char(1)              null,
   PEDEP_CODIGO         char(3)              not null,
   PECAR_CODIGO         char(3)              not null,
   PEE_PEEMP_CODIGO     char(6)              null,
   PEEMP_APELLI         varchar(50)          not null,
   PEEMP_NOMBRE         varchar(50)          not null,
   PEEMP_DIREC          varchar(200)         not null,
   PEEMP_FECNAC         datetime             not null,
   PEEMP_FECSAL         datetime             not null,
   PEEMP_TELEF          varchar(15)          not null,
   PEEMP_EMAIL          varchar(100)         not null,
   PEEMP_CEDULA         varchar(10)          not null,
   PEEMP_SALAR          numeric              not null,
   constraint PK_PEEMP_EMPLE primary key (PEEMP_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PEEMP_EMPLE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PEEMP_EMPLE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad para realizar la gestion de empleados
   
   ', 
   'user', @CurrentUser, 'table', 'PEEMP_EMPLE'
go

/*==============================================================*/
/* Index: PR_PESEX_PEEMP_FK                                     */
/*==============================================================*/




create nonclustered index PR_PESEX_PEEMP_FK on PEEMP_EMPLE (PESEX_CODIGO ASC)
go

/*==============================================================*/
/* Index: PR_PEESC_PEEMP_FK                                     */
/*==============================================================*/




create nonclustered index PR_PEESC_PEEMP_FK on PEEMP_EMPLE (PEESC_CODIGO ASC)
go

/*==============================================================*/
/* Index: PR_PECAR_PEEMP__FK                                    */
/*==============================================================*/




create nonclustered index PR_PECAR_PEEMP__FK on PEEMP_EMPLE (PEDEP_CODIGO ASC,
  PECAR_CODIGO ASC)
go

/*==============================================================*/
/* Index: PR_PEEMP_PEEMP_FK                                     */
/*==============================================================*/




create nonclustered index PR_PEEMP_PEEMP_FK on PEEMP_EMPLE (PEE_PEEMP_CODIGO ASC)
go

/*==============================================================*/
/* Table: PEESC_ESTCIV                                          */
/*==============================================================*/
create table PEESC_ESTCIV (
   PEESC_CODIGO         char(1)              not null,
   PEESC_DESCRI         char(50)             not null,
   constraint PK_PEESC_ESTCIV primary key (PEESC_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PEESC_ESTCIV') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PEESC_ESTCIV' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para realizar la gestión del ESTADO CIVIL
   ', 
   'user', @CurrentUser, 'table', 'PEESC_ESTCIV'
go

/*==============================================================*/
/* Table: PEFAE_FAMILIAR                                        */
/*==============================================================*/
create table PEFAE_FAMILIAR (
   PEFAE_NOMBRES        varchar(50)          not null,
   PEFAE_FECHAN         datetime             not null,
   PEFAE_EDAD           int                  not null,
   PEFAE_PARENT         varchar(50)          not null,
   PEFAE_APELL          varchar(50)          not null,
   PEFAE_ID             bigint               not null,
   PEEMP_CODIGO         char(6)              null,
   constraint PK_PEFAE_FAMILIAR primary key (PEFAE_ID)
)
go

/*==============================================================*/
/* Index: PR_PEEMP_PEFAE_FK                                     */
/*==============================================================*/




create nonclustered index PR_PEEMP_PEFAE_FK on PEFAE_FAMILIAR (PEEMP_CODIGO ASC)
go

/*==============================================================*/
/* Table: PEHRT_HORAS                                           */
/*==============================================================*/
create table PEHRT_HORAS (
   PEHRT_ID             bigint               not null,
   PEEMP_CODIGO         char(6)              not null,
   PEHRT_HRINIC         datetime             not null,
   PEHRT_HRFIN          datetime             not null,
   constraint PK_PEHRT_HORAS primary key (PEHRT_ID)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_21_FK                                    */
/*==============================================================*/




create nonclustered index RELATIONSHIP_21_FK on PEHRT_HORAS (PEEMP_CODIGO ASC)
go

/*==============================================================*/
/* Table: PESEX_SEXO                                            */
/*==============================================================*/
create table PESEX_SEXO (
   PESEX_CODIGO         char(1)              not null,
   PESEX_DESCRI         varchar(50)          not null,
   constraint PK_PESEX_SEXO primary key (PESEX_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PESEX_SEXO') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PESEX_SEXO' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para realizar la gestión del SEXO o GËNERO de una PERSONA
   ', 
   'user', @CurrentUser, 'table', 'PESEX_SEXO'
go

/*==============================================================*/
/* Table: RELATIONSHIP_19                                       */
/*==============================================================*/
create table RELATIONSHIP_19 (
   PEDEP_CODIGO         char(3)              not null,
   PEEMP_CODIGO         char(6)              not null,
   constraint PK_RELATIONSHIP_19 primary key (PEDEP_CODIGO, PEEMP_CODIGO)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_20_FK                                    */
/*==============================================================*/




create nonclustered index RELATIONSHIP_20_FK on RELATIONSHIP_19 (PEEMP_CODIGO ASC)
go

/*==============================================================*/
/* Index: RELATIONSHIP_19_FK                                    */
/*==============================================================*/




create nonclustered index RELATIONSHIP_19_FK on RELATIONSHIP_19 (PEDEP_CODIGO ASC)
go

/*==============================================================*/
/* Table: XEEST_ESTAD                                           */
/*==============================================================*/
create table XEEST_ESTAD (
   XEEST_CODIGO         char(1)              not null,
   XEEST_DESCRI         varchar(50)          not null,
   constraint PK_XEEST_ESTAD primary key (XEEST_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('XEEST_ESTAD') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'XEEST_ESTAD' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para gestionar el estado de las difetrentes tablas', 
   'user', @CurrentUser, 'table', 'XEEST_ESTAD'
go

/*==============================================================*/
/* Table: XEOPC_OPCIO                                           */
/*==============================================================*/
create table XEOPC_OPCIO (
   XEOPC_CODIGO         char(3)              not null,
   XESIS_CODIGO         char(1)              not null,
   XEOPC_DESCRI         varchar(100)         not null,
   constraint PK_XEOPC_OPCIO primary key (XEOPC_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('XEOPC_OPCIO') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'XEOPC_OPCIO' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para realizar el registro de las diferentes opciones de un sistema', 
   'user', @CurrentUser, 'table', 'XEOPC_OPCIO'
go

/*==============================================================*/
/* Index: XR_XESIS_XEOPC_FK                                     */
/*==============================================================*/




create nonclustered index XR_XESIS_XEOPC_FK on XEOPC_OPCIO (XESIS_CODIGO ASC)
go

/*==============================================================*/
/* Table: XEOXP_OPCPE                                           */
/*==============================================================*/
create table XEOXP_OPCPE (
   XEOPC_CODIGO         char(3)              not null,
   XEPER_CODIGO         char(8)              not null,
   XEOXP_FECASI         datetime             not null,
   XEOXP_FECRET         datetime             null,
   constraint PK_XEOXP_OPCPE primary key (XEOPC_CODIGO, XEPER_CODIGO, XEOXP_FECASI)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('XEOXP_OPCPE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'XEOXP_OPCPE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para llevar el registro de las opciones que pertenecen a un perfil', 
   'user', @CurrentUser, 'table', 'XEOXP_OPCPE'
go

/*==============================================================*/
/* Index: XR_XEPER_XEOXP_FK                                     */
/*==============================================================*/




create nonclustered index XR_XEPER_XEOXP_FK on XEOXP_OPCPE (XEPER_CODIGO ASC)
go

/*==============================================================*/
/* Index: XR_XEOPC_XEOXP_FK                                     */
/*==============================================================*/




create nonclustered index XR_XEOPC_XEOXP_FK on XEOXP_OPCPE (XEOPC_CODIGO ASC)
go

/*==============================================================*/
/* Table: XEPER_PERFI                                           */
/*==============================================================*/
create table XEPER_PERFI (
   XEPER_CODIGO         char(8)              not null,
   XEPER_DESCRI         varchar(100)         not null,
   XEPER_OBSER          text                 null,
   constraint PK_XEPER_PERFI primary key (XEPER_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('XEPER_PERFI') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'XEPER_PERFI' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para realizar la gestión de los diferentes perfiles', 
   'user', @CurrentUser, 'table', 'XEPER_PERFI'
go

/*==============================================================*/
/* Table: XESIS_SISTE                                           */
/*==============================================================*/
create table XESIS_SISTE (
   XESIS_CODIGO         char(1)              not null,
   XESIS_DESCRI         varchar(50)          not null,
   constraint PK_XESIS_SISTE primary key (XESIS_CODIGO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('XESIS_SISTE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'XESIS_SISTE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para realziar la gestión de los diferentes subsistemas', 
   'user', @CurrentUser, 'table', 'XESIS_SISTE'
go

/*==============================================================*/
/* Table: XEUSU_USUAR                                           */
/*==============================================================*/
create table XEUSU_USUAR (
   XEUSU_PASWD          varchar(16)          not null,
   XEEST_CODIGO         char(1)              not null,
   PEEMP_CODIGO         char(6)              not null,
   XEUSU_FECCRE         datetime             not null,
   XEUSU_FECMOD         datetime             not null,
   XEUSU_PIEFIR         varchar(100)         not null,
   constraint PK_XEUSU_USUAR primary key (XEUSU_PASWD)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('XEUSU_USUAR') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'XEUSU_USUAR' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad relacionada para gentionar los usuario que ingrsan al sistema', 
   'user', @CurrentUser, 'table', 'XEUSU_USUAR'
go

/*==============================================================*/
/* Index: XR_XEEST_XEUSU_FK                                     */
/*==============================================================*/




create nonclustered index XR_XEEST_XEUSU_FK on XEUSU_USUAR (XEEST_CODIGO ASC)
go

/*==============================================================*/
/* Index: XR_PEEM_XEUSU_FK                                      */
/*==============================================================*/




create nonclustered index XR_PEEM_XEUSU_FK on XEUSU_USUAR (PEEMP_CODIGO ASC)
go

/*==============================================================*/
/* Table: XEUXP_USUPE                                           */
/*==============================================================*/
create table XEUXP_USUPE (
   XEUSU_PASWD          varchar(16)          not null,
   XEPER_CODIGO         char(8)              not null,
   XEUXP_FECASI         datetime             not null,
   XEUXP_FECRET         datetime             null,
   constraint PK_XEUXP_USUPE primary key (XEUSU_PASWD, XEPER_CODIGO, XEUXP_FECASI)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('XEUXP_USUPE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'XEUXP_USUPE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad utilizada para realizar el registro de los diferentes usuarios que pertenecen a un perfil', 
   'user', @CurrentUser, 'table', 'XEUXP_USUPE'
go

/*==============================================================*/
/* Index: XR_XEPER_XEUXP_FK                                     */
/*==============================================================*/




create nonclustered index XR_XEPER_XEUXP_FK on XEUXP_USUPE (XEPER_CODIGO ASC)
go

/*==============================================================*/
/* Index: XR_XEUSU_XEUXP_FK                                     */
/*==============================================================*/




create nonclustered index XR_XEUSU_XEUXP_FK on XEUXP_USUPE (XEUSU_PASWD ASC)
go

alter table GEPRY_PROYEC
   add constraint FK_GEPRY_PR_DEPAR_PRO_PEDEP_DE foreign key (PEDEP_CODIGO)
      references PEDEP_DEPAR (PEDEP_CODIGO)
go

alter table GR_GEPRY_PEEMP
   add constraint FK_GR_GEPRY_GR_GEPRY__PEEMP_EM foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO)
go

alter table GR_GEPRY_PEEMP
   add constraint FK_GR_GEPRY_GR_GEPRY__GEPRY_PR foreign key (GEPRY_CODIGO)
      references GEPRY_PROYEC (GEPRY_CODIGO)
go

alter table PECAR_CARGO
   add constraint FK_PECAR_CA_PR_PEDEP__PEDEP_DE foreign key (PEDEP_CODIGO)
      references PEDEP_DEPAR (PEDEP_CODIGO)
go

alter table PEEMP_EMPLE
   add constraint FK_PEEMP_EM_PR_PECAR__PECAR_CA foreign key (PEDEP_CODIGO, PECAR_CODIGO)
      references PECAR_CARGO (PEDEP_CODIGO, PECAR_CODIGO)
go

alter table PEEMP_EMPLE
   add constraint FK_PEEMP_EM_PR_PEEMP__PEEMP_EM foreign key (PEE_PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO)
go

alter table PEEMP_EMPLE
   add constraint FK_PEEMP_EM_PR_PEESC__PEESC_ES foreign key (PEESC_CODIGO)
      references PEESC_ESTCIV (PEESC_CODIGO)
go

alter table PEEMP_EMPLE
   add constraint FK_PEEMP_EM_PR_PESEX__PESEX_SE foreign key (PESEX_CODIGO)
      references PESEX_SEXO (PESEX_CODIGO)
go

alter table PEFAE_FAMILIAR
   add constraint FK_PEFAE_FA_PR_PEEMP__PEEMP_EM foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO)
go

alter table PEHRT_HORAS
   add constraint FK_PEHRT_HO_RELATIONS_PEEMP_EM foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO)
go

alter table RELATIONSHIP_19
   add constraint FK_RELATION_RELATIONS_PEDEP_DE foreign key (PEDEP_CODIGO)
      references PEDEP_DEPAR (PEDEP_CODIGO)
go

alter table RELATIONSHIP_19
   add constraint FK_RELATION_RELATIONS_PEEMP_EM foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO)
go

alter table XEOPC_OPCIO
   add constraint FK_XEOPC_OP_XR_XESIS__XESIS_SI foreign key (XESIS_CODIGO)
      references XESIS_SISTE (XESIS_CODIGO)
go

alter table XEOXP_OPCPE
   add constraint FK_XEOXP_OP_XR_XEOPC__XEOPC_OP foreign key (XEOPC_CODIGO)
      references XEOPC_OPCIO (XEOPC_CODIGO)
go

alter table XEOXP_OPCPE
   add constraint FK_XEOXP_OP_XR_XEPER__XEPER_PE foreign key (XEPER_CODIGO)
      references XEPER_PERFI (XEPER_CODIGO)
go

alter table XEUSU_USUAR
   add constraint FK_XEUSU_US_XR_PEEM_X_PEEMP_EM foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO)
go

alter table XEUSU_USUAR
   add constraint FK_XEUSU_US_XR_XEEST__XEEST_ES foreign key (XEEST_CODIGO)
      references XEEST_ESTAD (XEEST_CODIGO)
go

alter table XEUXP_USUPE
   add constraint FK_XEUXP_US_XR_XEPER__XEPER_PE foreign key (XEPER_CODIGO)
      references XEPER_PERFI (XEPER_CODIGO)
go

alter table XEUXP_USUPE
   add constraint FK_XEUXP_US_XR_XEUSU__XEUSU_US foreign key (XEUSU_PASWD)
      references XEUSU_USUAR (XEUSU_PASWD)
go

