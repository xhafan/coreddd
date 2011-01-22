
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC644F053C3BA1A19]') AND parent_object_id = OBJECT_ID('[EmailTemplatePart]'))
alter table [EmailTemplatePart]  drop constraint FKC644F053C3BA1A19


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE07F3D8E84A755C8]') AND parent_object_id = OBJECT_ID('[HtmlEmailTemplatePart]'))
alter table [HtmlEmailTemplatePart]  drop constraint FKE07F3D8E84A755C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2A9FDCE184A755C8]') AND parent_object_id = OBJECT_ID('[VariableEmailTemplatePart]'))
alter table [VariableEmailTemplatePart]  drop constraint FK2A9FDCE184A755C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2A9FDCE1326ED16A]') AND parent_object_id = OBJECT_ID('[VariableEmailTemplatePart]'))
alter table [VariableEmailTemplatePart]  drop constraint FK2A9FDCE1326ED16A


    if exists (select * from dbo.sysobjects where id = object_id(N'[EmailTemplate]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [EmailTemplate]

    if exists (select * from dbo.sysobjects where id = object_id(N'[EmailTemplatePart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [EmailTemplatePart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[HtmlEmailTemplatePart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [HtmlEmailTemplatePart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[VariableEmailTemplatePart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [VariableEmailTemplatePart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[VariableType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [VariableType]

    if exists (select * from dbo.sysobjects where id = object_id(N'hibernate_unique_key') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table hibernate_unique_key

    create table [EmailTemplate] (
        Id INT not null,
       primary key (Id)
    )

    create table [EmailTemplatePart] (
        Id INT not null,
       Position INT null,
       EmailTemplateId INT null,
       primary key (Id)
    )

    create table [HtmlEmailTemplatePart] (
        Id INT not null,
       Html NVARCHAR(MAX) null,
       primary key (Id)
    )

    create table [VariableEmailTemplatePart] (
        Id INT not null,
       Value NVARCHAR(MAX) null,
       VariableTypeId INT null,
       primary key (Id)
    )

    create table [VariableType] (
        Id INT not null,
       Name NVARCHAR(255) null,
       primary key (Id)
    )

    alter table [EmailTemplatePart] 
        add constraint FKC644F053C3BA1A19 
        foreign key (EmailTemplateId) 
        references [EmailTemplate]

    alter table [HtmlEmailTemplatePart] 
        add constraint FKE07F3D8E84A755C8 
        foreign key (Id) 
        references [EmailTemplatePart]

    alter table [VariableEmailTemplatePart] 
        add constraint FK2A9FDCE184A755C8 
        foreign key (Id) 
        references [EmailTemplatePart]

    alter table [VariableEmailTemplatePart] 
        add constraint FK2A9FDCE1326ED16A 
        foreign key (VariableTypeId) 
        references [VariableType]

    create table hibernate_unique_key (
         next_hi INT 
    )

    insert into hibernate_unique_key values ( 1 )
