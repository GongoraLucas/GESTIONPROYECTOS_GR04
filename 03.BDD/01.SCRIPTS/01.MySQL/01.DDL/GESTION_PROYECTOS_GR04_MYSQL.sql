/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2026-05-15 5:20:13 AM                        */
/*==============================================================*/


drop table if exists GEPRY_PROYEC;

drop table if exists GR_GEPRY_PEEMP;

drop table if exists PECAR_CARGO;

drop table if exists PEDEP_DEPAR;

drop table if exists PEEMP_EMPLE;

drop table if exists PEESC_ESTCIV;

drop table if exists PEFAE_FAMILIAR;

drop table if exists PEHRT_HORAS;

drop table if exists PESEX_SEXO;

drop table if exists RELATIONSHIP_19;

drop table if exists XEEST_ESTAD;

drop table if exists XEOPC_OPCIO;

drop table if exists XEOXP_OPCPE;

drop table if exists XEPER_PERFI;

drop table if exists XESIS_SISTE;

drop table if exists XEUSU_USUAR;

drop table if exists XEUXP_USUPE;

/*==============================================================*/
/* Table: GEPRY_PROYEC                                          */
/*==============================================================*/
create table GEPRY_PROYEC
(
   GEPRY_CODIGO         char(4) not null,
   PEDEP_CODIGO         char(3) not null,
   GEPRY_NOMBRE         varchar(50) not null,
   GEPRY_DESCRI         varchar(50) not null,
   GEPRY_NUMERO         int not null,
   primary key (GEPRY_CODIGO)
);

/*==============================================================*/
/* Table: GR_GEPRY_PEEMP                                        */
/*==============================================================*/
create table GR_GEPRY_PEEMP
(
   PEEMP_CODIGO         char(6) not null,
   GEPRY_CODIGO         char(4) not null,
   primary key (PEEMP_CODIGO, GEPRY_CODIGO)
);

alter table GR_GEPRY_PEEMP comment 'Relación entre las tablas GEPRY_PROYEC y  PEEMP_EMPLE';

/*==============================================================*/
/* Table: PECAR_CARGO                                           */
/*==============================================================*/
create table PECAR_CARGO
(
   PEDEP_CODIGO         char(3) not null,
   PECAR_CODIGO         char(3) not null,
   PECAR_DESCRI         varchar(50) not null,
   primary key (PEDEP_CODIGO, PECAR_CODIGO)
);

alter table PECAR_CARGO comment 'Entidad utilizada para la gestión de los diferentes CARGOSqu';

/*==============================================================*/
/* Table: PEDEP_DEPAR                                           */
/*==============================================================*/
create table PEDEP_DEPAR
(
   PEDEP_CODIGO         char(3) not null,
   PEDEP_DESCRIP        varchar(50) not null,
   primary key (PEDEP_CODIGO)
);

alter table PEDEP_DEPAR comment 'Entidad utilizada para realizar la gestión de los diferentes';

/*==============================================================*/
/* Table: PEEMP_EMPLE                                           */
/*==============================================================*/
create table PEEMP_EMPLE
(
   PEEMP_CODIGO         char(6) not null,
   PESEX_CODIGO         char(1) not null,
   PEESC_CODIGO         char(1),
   PEDEP_CODIGO         char(3) not null,
   PECAR_CODIGO         char(3) not null,
   PEE_PEEMP_CODIGO     char(6),
   PEEMP_APELLI         varchar(50) not null,
   PEEMP_NOMBRE         varchar(50) not null,
   PEEMP_DIREC          varchar(200) not null,
   PEEMP_FECNAC         date not null,
   PEEMP_FECSAL         date not null,
   PEEMP_TELEF          varchar(15) not null,
   PEEMP_EMAIL          varchar(100) not null,
   PEEMP_CEDULA         varchar(10) not null,
   PEEMP_SALAR          numeric(8,0) not null,
   primary key (PEEMP_CODIGO)
);

alter table PEEMP_EMPLE comment 'Entidad para realizar la gestion de empleados

';

/*==============================================================*/
/* Table: PEESC_ESTCIV                                          */
/*==============================================================*/
create table PEESC_ESTCIV
(
   PEESC_CODIGO         char(1) not null,
   PEESC_DESCRI         char(50) not null,
   primary key (PEESC_CODIGO)
);

alter table PEESC_ESTCIV comment 'Entidad utilizada para realizar la gestión del ESTADO CIVIL';

/*==============================================================*/
/* Table: PEFAE_FAMILIAR                                        */
/*==============================================================*/
create table PEFAE_FAMILIAR
(
   PEFAE_NOMBRES        varchar(50) not null,
   PEFAE_FECHAN         date not null,
   PEFAE_EDAD           int not null,
   PEFAE_PARENT         varchar(50) not null,
   PEFAE_APELL          varchar(50) not null,
   PEFAE_ID             bigint not null,
   PEEMP_CODIGO         char(6),
   primary key (PEFAE_ID)
);

/*==============================================================*/
/* Table: PEHRT_HORAS                                           */
/*==============================================================*/
create table PEHRT_HORAS
(
   PEHRT_ID             bigint not null,
   PEEMP_CODIGO         char(6) not null,
   PEHRT_HRINIC         time not null,
   PEHRT_HRFIN          time not null,
   primary key (PEHRT_ID)
);

/*==============================================================*/
/* Table: PESEX_SEXO                                            */
/*==============================================================*/
create table PESEX_SEXO
(
   PESEX_CODIGO         char(1) not null,
   PESEX_DESCRI         varchar(50) not null,
   primary key (PESEX_CODIGO)
);

alter table PESEX_SEXO comment 'Entidad utilizada para realizar la gestión del SEXO o GËNERO';

/*==============================================================*/
/* Table: RELATIONSHIP_19                                       */
/*==============================================================*/
create table RELATIONSHIP_19
(
   PEDEP_CODIGO         char(3) not null,
   PEEMP_CODIGO         char(6) not null,
   primary key (PEDEP_CODIGO, PEEMP_CODIGO)
);

/*==============================================================*/
/* Table: XEEST_ESTAD                                           */
/*==============================================================*/
create table XEEST_ESTAD
(
   XEEST_CODIGO         char(1) not null,
   XEEST_DESCRI         varchar(50) not null,
   primary key (XEEST_CODIGO)
);

alter table XEEST_ESTAD comment 'Entidad utilizada para gestionar el estado de las difetrente';

/*==============================================================*/
/* Table: XEOPC_OPCIO                                           */
/*==============================================================*/
create table XEOPC_OPCIO
(
   XEOPC_CODIGO         char(3) not null,
   XESIS_CODIGO         char(1) not null,
   XEOPC_DESCRI         varchar(100) not null,
   primary key (XEOPC_CODIGO)
);

alter table XEOPC_OPCIO comment 'Entidad utilizada para realizar el registro de las diferente';

/*==============================================================*/
/* Table: XEOXP_OPCPE                                           */
/*==============================================================*/
create table XEOXP_OPCPE
(
   XEOPC_CODIGO         char(3) not null,
   XEPER_CODIGO         char(8) not null,
   XEOXP_FECASI         date not null,
   XEOXP_FECRET         date,
   primary key (XEOPC_CODIGO, XEPER_CODIGO, XEOXP_FECASI)
);

alter table XEOXP_OPCPE comment 'Entidad utilizada para llevar el registro de las opciones qu';

/*==============================================================*/
/* Table: XEPER_PERFI                                           */
/*==============================================================*/
create table XEPER_PERFI
(
   XEPER_CODIGO         char(8) not null,
   XEPER_DESCRI         varchar(100) not null,
   XEPER_OBSER          text,
   primary key (XEPER_CODIGO)
);

alter table XEPER_PERFI comment 'Entidad utilizada para realizar la gestión de los diferentes';

/*==============================================================*/
/* Table: XESIS_SISTE                                           */
/*==============================================================*/
create table XESIS_SISTE
(
   XESIS_CODIGO         char(1) not null,
   XESIS_DESCRI         varchar(50) not null,
   primary key (XESIS_CODIGO)
);

alter table XESIS_SISTE comment 'Entidad utilizada para realziar la gestión de los diferentes';

/*==============================================================*/
/* Table: XEUSU_USUAR                                           */
/*==============================================================*/
create table XEUSU_USUAR
(
   XEUSU_PASWD          varchar(16) not null,
   XEEST_CODIGO         char(1) not null,
   PEEMP_CODIGO         char(6) not null,
   XEUSU_FECCRE         datetime not null,
   XEUSU_FECMOD         datetime not null,
   XEUSU_PIEFIR         varchar(100) not null,
   primary key (XEUSU_PASWD)
);

alter table XEUSU_USUAR comment 'Entidad relacionada para gentionar los usuario que ingrsan a';

/*==============================================================*/
/* Table: XEUXP_USUPE                                           */
/*==============================================================*/
create table XEUXP_USUPE
(
   XEUSU_PASWD          varchar(16) not null,
   XEPER_CODIGO         char(8) not null,
   XEUXP_FECASI         date not null,
   XEUXP_FECRET         date,
   primary key (XEUSU_PASWD, XEPER_CODIGO, XEUXP_FECASI)
);

alter table XEUXP_USUPE comment 'Entidad utilizada para realizar el registro de los diferente';

alter table GEPRY_PROYEC add constraint FK_DEPAR_PROYECT foreign key (PEDEP_CODIGO)
      references PEDEP_DEPAR (PEDEP_CODIGO) on delete restrict on update restrict;

alter table GR_GEPRY_PEEMP add constraint FK_GR_GEPRY_PEEMP foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO) on delete restrict on update restrict;

alter table GR_GEPRY_PEEMP add constraint FK_GR_GEPRY_PEEMP2 foreign key (GEPRY_CODIGO)
      references GEPRY_PROYEC (GEPRY_CODIGO) on delete restrict on update restrict;

alter table PECAR_CARGO add constraint FK_PR_PEDEP_PECAR foreign key (PEDEP_CODIGO)
      references PEDEP_DEPAR (PEDEP_CODIGO) on delete restrict on update restrict;

alter table PEEMP_EMPLE add constraint FK_PR_PECAR_PEEMP_ foreign key (PEDEP_CODIGO, PECAR_CODIGO)
      references PECAR_CARGO (PEDEP_CODIGO, PECAR_CODIGO) on delete restrict on update restrict;

alter table PEEMP_EMPLE add constraint FK_PR_PEEMP_PEEMP foreign key (PEE_PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO) on delete restrict on update restrict;

alter table PEEMP_EMPLE add constraint FK_PR_PEESC_PEEMP foreign key (PEESC_CODIGO)
      references PEESC_ESTCIV (PEESC_CODIGO) on delete restrict on update restrict;

alter table PEEMP_EMPLE add constraint FK_PR_PESEX_PEEMP foreign key (PESEX_CODIGO)
      references PESEX_SEXO (PESEX_CODIGO) on delete restrict on update restrict;

alter table PEFAE_FAMILIAR add constraint FK_PR_PEEMP_PEFAE foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO) on delete restrict on update restrict;

alter table PEHRT_HORAS add constraint FK_RELATIONSHIP_21 foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO) on delete restrict on update restrict;

alter table RELATIONSHIP_19 add constraint FK_RELATIONSHIP_19 foreign key (PEDEP_CODIGO)
      references PEDEP_DEPAR (PEDEP_CODIGO) on delete restrict on update restrict;

alter table RELATIONSHIP_19 add constraint FK_RELATIONSHIP_20 foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO) on delete restrict on update restrict;

alter table XEOPC_OPCIO add constraint FK_XR_XESIS_XEOPC foreign key (XESIS_CODIGO)
      references XESIS_SISTE (XESIS_CODIGO) on delete restrict on update restrict;

alter table XEOXP_OPCPE add constraint FK_XR_XEOPC_XEOXP foreign key (XEOPC_CODIGO)
      references XEOPC_OPCIO (XEOPC_CODIGO) on delete restrict on update restrict;

alter table XEOXP_OPCPE add constraint FK_XR_XEPER_XEOXP foreign key (XEPER_CODIGO)
      references XEPER_PERFI (XEPER_CODIGO) on delete restrict on update restrict;

alter table XEUSU_USUAR add constraint FK_XR_PEEM_XEUSU foreign key (PEEMP_CODIGO)
      references PEEMP_EMPLE (PEEMP_CODIGO) on delete restrict on update restrict;

alter table XEUSU_USUAR add constraint FK_XR_XEEST_XEUSU foreign key (XEEST_CODIGO)
      references XEEST_ESTAD (XEEST_CODIGO) on delete restrict on update restrict;

alter table XEUXP_USUPE add constraint FK_XR_XEPER_XEUXP foreign key (XEPER_CODIGO)
      references XEPER_PERFI (XEPER_CODIGO) on delete restrict on update restrict;

alter table XEUXP_USUPE add constraint FK_XR_XEUSU_XEUXP foreign key (XEUSU_PASWD)
      references XEUSU_USUAR (XEUSU_PASWD) on delete restrict on update restrict;

