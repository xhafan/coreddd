
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK4239B252C3BA1A19]') AND parent_object_id = OBJECT_ID('[Email]'))
alter table [Email]  drop constraint FK4239B252C3BA1A19


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB3CACEE7BB7A8FCD]') AND parent_object_id = OBJECT_ID('[EmailPart]'))
alter table [EmailPart]  drop constraint FKB3CACEE7BB7A8FCD


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC187ECA4EBD3F1E8]') AND parent_object_id = OBJECT_ID('[HtmlEmailPart]'))
alter table [HtmlEmailPart]  drop constraint FKC187ECA4EBD3F1E8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5655592DEBD3F1E8]') AND parent_object_id = OBJECT_ID('[VariableEmailPart]'))
alter table [VariableEmailPart]  drop constraint FK5655592DEBD3F1E8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5655592DCBEDA9AC]') AND parent_object_id = OBJECT_ID('[VariableEmailPart]'))
alter table [VariableEmailPart]  drop constraint FK5655592DCBEDA9AC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC644F053C3BA1A19]') AND parent_object_id = OBJECT_ID('[EmailTemplatePart]'))
alter table [EmailTemplatePart]  drop constraint FKC644F053C3BA1A19


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE07F3D8E84A755C8]') AND parent_object_id = OBJECT_ID('[HtmlEmailTemplatePart]'))
alter table [HtmlEmailTemplatePart]  drop constraint FKE07F3D8E84A755C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2A9FDCE184A755C8]') AND parent_object_id = OBJECT_ID('[VariableEmailTemplatePart]'))
alter table [VariableEmailTemplatePart]  drop constraint FK2A9FDCE184A755C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2A9FDCE1CBEDA9AC]') AND parent_object_id = OBJECT_ID('[VariableEmailTemplatePart]'))
alter table [VariableEmailTemplatePart]  drop constraint FK2A9FDCE1CBEDA9AC


    if exists (select * from dbo.sysobjects where id = object_id(N'[Email]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Email]

    if exists (select * from dbo.sysobjects where id = object_id(N'[EmailPart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [EmailPart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[HtmlEmailPart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [HtmlEmailPart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[VariableEmailPart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [VariableEmailPart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[EmailTemplate]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [EmailTemplate]

    if exists (select * from dbo.sysobjects where id = object_id(N'[EmailTemplatePart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [EmailTemplatePart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[HtmlEmailTemplatePart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [HtmlEmailTemplatePart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[VariableEmailTemplatePart]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [VariableEmailTemplatePart]

    if exists (select * from dbo.sysobjects where id = object_id(N'[VariableType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [VariableType]

    if exists (select * from dbo.sysobjects where id = object_id(N'hibernate_unique_key') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table hibernate_unique_key

    create table [Email] (
        Id INT not null,
       EmailTemplateId INT null,
       primary key (Id)
    )

    create table [EmailPart] (
        Id INT not null,
       Position INT null,
       EmailId INT null,
       primary key (Id)
    )

    create table [HtmlEmailPart] (
        Id INT not null,
       Html NVARCHAR(MAX) null,
       primary key (Id)
    )
    
     create table [EmailTemplateForCulture](
		Id int IDENTITY(1,1) NOT NULL,
		Culture nvarchar(5) NULL,
		Name nvarchar(255) NULL,
		EmailTemplateId int NULL,
		primary key (Id)
	)

    create table [VariableEmailPart] (
        Id INT not null,
       Value NVARCHAR(MAX) null,
       VariableTypeId INT null,
       primary key (Id)
    )

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

    alter table [Email] 
        add constraint FK4239B252C3BA1A19 
        foreign key (EmailTemplateId) 
        references [EmailTemplate]

    alter table [EmailPart] 
        add constraint FKB3CACEE7BB7A8FCD 
        foreign key (EmailId) 
        references [Email]

    alter table [HtmlEmailPart] 
        add constraint FKC187ECA4EBD3F1E8 
        foreign key (Id) 
        references [EmailPart]

    alter table [VariableEmailPart] 
        add constraint FK5655592DEBD3F1E8 
        foreign key (Id) 
        references [EmailPart]

    alter table [VariableEmailPart] 
        add constraint FK5655592DCBEDA9AC 
        foreign key (VariableTypeId) 
        references [VariableType]

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
        add constraint FK2A9FDCE1CBEDA9AC 
        foreign key (VariableTypeId) 
        references [VariableType]

    alter table [EmailTemplateForCulture]  
		add constraint [FK_EmailTemplateForCulture_EmailTemplate1] 
		foreign key([EmailTemplateId])
		references [EmailTemplate]
		
    create table hibernate_unique_key (
         next_hi INT 
    )

    insert into hibernate_unique_key values ( 1 )
