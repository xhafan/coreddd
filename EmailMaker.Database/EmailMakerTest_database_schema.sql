create database EmailMakerTest

go

use EmailMakerTest;
    
go    

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
       Html NVARCHAR(255) null,
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

    create table hibernate_unique_key (
         next_hi INT 
    )

    insert into hibernate_unique_key values ( 1 )
