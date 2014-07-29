
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/25/2014 13:39:23
-- Generated from EDMX file: C:\Users\Rymbln\Documents\GitHub\WPFDB\WPFDB\Model\ConferenceModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ConferenceNew];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ScienceDegreePerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_ScienceDegreePerson];
GO
IF OBJECT_ID(N'[dbo].[FK_ScienceStatusPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_ScienceStatusPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_SexPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_SexPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecialityPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_SpecialityPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPersonConference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences] DROP CONSTRAINT [FK_PersonPersonConference];
GO
IF OBJECT_ID(N'[dbo].[FK_ConferencePersonConference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences] DROP CONSTRAINT [FK_ConferencePersonConference];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyPersonConferenceDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Detail] DROP CONSTRAINT [FK_CompanyPersonConferenceDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyPersonConferenceMoney]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Payment] DROP CONSTRAINT [FK_CompanyPersonConferenceMoney];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentTypePersonConferenceMoney]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Payment] DROP CONSTRAINT [FK_PaymentTypePersonConferenceMoney];
GO
IF OBJECT_ID(N'[dbo].[FK_Detail_inherits_PersonConference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Detail] DROP CONSTRAINT [FK_Detail_inherits_PersonConference];
GO
IF OBJECT_ID(N'[dbo].[FK_Payment_inherits_PersonConference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Payment] DROP CONSTRAINT [FK_Payment_inherits_PersonConference];
GO
IF OBJECT_ID(N'[dbo].[FK_RankPersonConferenceDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Detail] DROP CONSTRAINT [FK_RankPersonConferenceDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonIacmac]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Iacmacs] DROP CONSTRAINT [FK_PersonIacmac];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactTypeEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Emails] DROP CONSTRAINT [FK_ContactTypeEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactTypePhone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Phones] DROP CONSTRAINT [FK_ContactTypePhone];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactTypeAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_ContactTypeAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_PersonAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Emails] DROP CONSTRAINT [FK_PersonEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPhone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Phones] DROP CONSTRAINT [FK_PersonPhone];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonConferenceAbstract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Abstracts] DROP CONSTRAINT [FK_PersonConferenceAbstract];
GO
IF OBJECT_ID(N'[dbo].[FK_AbstractAbstractWork]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AbstractWorks] DROP CONSTRAINT [FK_AbstractAbstractWork];
GO
IF OBJECT_ID(N'[dbo].[FK_AbstractStatusAbstractWork]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AbstractWorks] DROP CONSTRAINT [FK_AbstractStatusAbstractWork];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAbstractWork]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AbstractWorks] DROP CONSTRAINT [FK_UserAbstractWork];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderStatusPersonConferences_Payment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Payment] DROP CONSTRAINT [FK_OrderStatusPersonConferences_Payment];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[ScienceDegrees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScienceDegrees];
GO
IF OBJECT_ID(N'[dbo].[ScienceStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScienceStatuses];
GO
IF OBJECT_ID(N'[dbo].[Sexes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sexes];
GO
IF OBJECT_ID(N'[dbo].[Specialities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specialities];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Properties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Properties];
GO
IF OBJECT_ID(N'[dbo].[Conferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conferences];
GO
IF OBJECT_ID(N'[dbo].[PersonConferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonConferences];
GO
IF OBJECT_ID(N'[dbo].[Ranks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ranks];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[PaymentTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentTypes];
GO
IF OBJECT_ID(N'[dbo].[PersonConferences_Detail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonConferences_Detail];
GO
IF OBJECT_ID(N'[dbo].[PersonConferences_Payment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonConferences_Payment];
GO
IF OBJECT_ID(N'[dbo].[Iacmacs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Iacmacs];
GO
IF OBJECT_ID(N'[dbo].[OrderStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderStatuses];
GO
IF OBJECT_ID(N'[dbo].[ContactTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactTypes];
GO
IF OBJECT_ID(N'[dbo].[Emails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Emails];
GO
IF OBJECT_ID(N'[dbo].[Phones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Phones];
GO
IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[Abstracts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Abstracts];
GO
IF OBJECT_ID(N'[dbo].[AbstractWorks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AbstractWorks];
GO
IF OBJECT_ID(N'[dbo].[AbstractStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AbstractStatuses];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Id] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [SecondName] nvarchar(max)  NOT NULL,
    [ThirdName] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NULL,
    [WorkPlace] nvarchar(max)  NULL,
    [Post] nvarchar(max)  NULL,
    [SpecialityId] uniqueidentifier  NOT NULL,
    [ScienceDegreeId] uniqueidentifier  NOT NULL,
    [ScienceStatusId] uniqueidentifier  NOT NULL,
    [SexId] uniqueidentifier  NOT NULL,
    [SourceId] int  NOT NULL
);
GO

-- Creating table 'ScienceDegrees'
CREATE TABLE [dbo].[ScienceDegrees] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ScienceStatuses'
CREATE TABLE [dbo].[ScienceStatuses] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sexes'
CREATE TABLE [dbo].[Sexes] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Specialities'
CREATE TABLE [dbo].[Specialities] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Role] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Properties'
CREATE TABLE [dbo].[Properties] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ValueString] nvarchar(max)  NOT NULL,
    [ValueLogic] bit  NOT NULL,
    [ValueDate] datetime  NOT NULL,
    [ValueInt] int  NOT NULL,
    [ValueDecimal] decimal(18,0)  NOT NULL,
    [ValueGuid] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Conferences'
CREATE TABLE [dbo].[Conferences] (
    [Id] uniqueidentifier  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL
);
GO

-- Creating table 'PersonConferences'
CREATE TABLE [dbo].[PersonConferences] (
    [PersonConferenceId] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL,
    [ConferenceId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Ranks'
CREATE TABLE [dbo].[Ranks] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] uniqueidentifier  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL
);
GO

-- Creating table 'PaymentTypes'
CREATE TABLE [dbo].[PaymentTypes] (
    [Id] uniqueidentifier  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL
);
GO

-- Creating table 'PersonConferences_Detail'
CREATE TABLE [dbo].[PersonConferences_Detail] (
    [RankId] uniqueidentifier  NOT NULL,
    [CompanyId] uniqueidentifier  NOT NULL,
    [IsBadge] bit  NOT NULL,
    [IsArrive] bit  NOT NULL,
    [DateArrive] datetime  NULL,
    [IsAbstract] bit  NOT NULL,
    [IsNeedBadge] bit  NOT NULL,
    [IsAutoreg] bit  NOT NULL,
    [SourceId] int  NOT NULL,
    [PersonConferenceId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'PersonConferences_Payment'
CREATE TABLE [dbo].[PersonConferences_Payment] (
    [PaymentTypeId] uniqueidentifier  NOT NULL,
    [CompanyId] uniqueidentifier  NOT NULL,
    [PaymentDocument] nvarchar(max)  NULL,
    [PaymentDate] datetime  NULL,
    [Money] decimal(18,0)  NOT NULL,
    [IsComplect] bit  NOT NULL,
    [OrderNumber] int  NOT NULL,
    [SourceId] int  NOT NULL,
    [PersonConferenceId] uniqueidentifier  NOT NULL,
    [OrderStatusId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Iacmacs'
CREATE TABLE [dbo].[Iacmacs] (
    [IsMember] bit  NOT NULL,
    [DateRegistration] datetime  NULL,
    [Number] int  NOT NULL,
    [Code] nvarchar(max)  NULL,
    [IsCardCreate] bit  NOT NULL,
    [IsCardSent] bit  NOT NULL,
    [IsForm] bit  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'OrderStatuses'
CREATE TABLE [dbo].[OrderStatuses] (
    [Id] uniqueidentifier  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL
);
GO

-- Creating table 'ContactTypes'
CREATE TABLE [dbo].[ContactTypes] (
    [Id] uniqueidentifier  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL
);
GO

-- Creating table 'Emails'
CREATE TABLE [dbo].[Emails] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL,
    [ContactTypeId] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Phones'
CREATE TABLE [dbo].[Phones] (
    [Id] uniqueidentifier  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL,
    [ContactTypeId] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] uniqueidentifier  NOT NULL,
    [ZipCode] nvarchar(max)  NOT NULL,
    [CountryName] nvarchar(max)  NOT NULL,
    [RegionName] nvarchar(max)  NOT NULL,
    [CityName] nvarchar(max)  NOT NULL,
    [StreetHouseName] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL,
    [ContactTypeId] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Abstracts'
CREATE TABLE [dbo].[Abstracts] (
    [Id] uniqueidentifier  NOT NULL,
    [OtherAuthors] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Link] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL,
    [PersonConferenceId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'AbstractWorks'
CREATE TABLE [dbo].[AbstractWorks] (
    [Id] uniqueidentifier  NOT NULL,
    [AbstractId] uniqueidentifier  NOT NULL,
    [AbstractStatusId] uniqueidentifier  NOT NULL,
    [AbstractResponsiblePersonId] uniqueidentifier  NOT NULL,
    [IsSentByEmail] bit  NOT NULL,
    [DateWork] datetime  NOT NULL,
    [SourceId] int  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'AbstractStatuses'
CREATE TABLE [dbo].[AbstractStatuses] (
    [Id] uniqueidentifier  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SourceId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScienceDegrees'
ALTER TABLE [dbo].[ScienceDegrees]
ADD CONSTRAINT [PK_ScienceDegrees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScienceStatuses'
ALTER TABLE [dbo].[ScienceStatuses]
ADD CONSTRAINT [PK_ScienceStatuses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sexes'
ALTER TABLE [dbo].[Sexes]
ADD CONSTRAINT [PK_Sexes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Specialities'
ALTER TABLE [dbo].[Specialities]
ADD CONSTRAINT [PK_Specialities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Properties'
ALTER TABLE [dbo].[Properties]
ADD CONSTRAINT [PK_Properties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Conferences'
ALTER TABLE [dbo].[Conferences]
ADD CONSTRAINT [PK_Conferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PersonConferenceId] in table 'PersonConferences'
ALTER TABLE [dbo].[PersonConferences]
ADD CONSTRAINT [PK_PersonConferences]
    PRIMARY KEY CLUSTERED ([PersonConferenceId] ASC);
GO

-- Creating primary key on [Id] in table 'Ranks'
ALTER TABLE [dbo].[Ranks]
ADD CONSTRAINT [PK_Ranks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentTypes'
ALTER TABLE [dbo].[PaymentTypes]
ADD CONSTRAINT [PK_PaymentTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PersonConferenceId] in table 'PersonConferences_Detail'
ALTER TABLE [dbo].[PersonConferences_Detail]
ADD CONSTRAINT [PK_PersonConferences_Detail]
    PRIMARY KEY CLUSTERED ([PersonConferenceId] ASC);
GO

-- Creating primary key on [PersonConferenceId] in table 'PersonConferences_Payment'
ALTER TABLE [dbo].[PersonConferences_Payment]
ADD CONSTRAINT [PK_PersonConferences_Payment]
    PRIMARY KEY CLUSTERED ([PersonConferenceId] ASC);
GO

-- Creating primary key on [PersonId] in table 'Iacmacs'
ALTER TABLE [dbo].[Iacmacs]
ADD CONSTRAINT [PK_Iacmacs]
    PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO

-- Creating primary key on [Id] in table 'OrderStatuses'
ALTER TABLE [dbo].[OrderStatuses]
ADD CONSTRAINT [PK_OrderStatuses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContactTypes'
ALTER TABLE [dbo].[ContactTypes]
ADD CONSTRAINT [PK_ContactTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [PK_Emails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Phones'
ALTER TABLE [dbo].[Phones]
ADD CONSTRAINT [PK_Phones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Abstracts'
ALTER TABLE [dbo].[Abstracts]
ADD CONSTRAINT [PK_Abstracts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AbstractWorks'
ALTER TABLE [dbo].[AbstractWorks]
ADD CONSTRAINT [PK_AbstractWorks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AbstractStatuses'
ALTER TABLE [dbo].[AbstractStatuses]
ADD CONSTRAINT [PK_AbstractStatuses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ScienceDegreeId] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [FK_ScienceDegreePerson]
    FOREIGN KEY ([ScienceDegreeId])
    REFERENCES [dbo].[ScienceDegrees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ScienceDegreePerson'
CREATE INDEX [IX_FK_ScienceDegreePerson]
ON [dbo].[Persons]
    ([ScienceDegreeId]);
GO

-- Creating foreign key on [ScienceStatusId] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [FK_ScienceStatusPerson]
    FOREIGN KEY ([ScienceStatusId])
    REFERENCES [dbo].[ScienceStatuses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ScienceStatusPerson'
CREATE INDEX [IX_FK_ScienceStatusPerson]
ON [dbo].[Persons]
    ([ScienceStatusId]);
GO

-- Creating foreign key on [SexId] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [FK_SexPerson]
    FOREIGN KEY ([SexId])
    REFERENCES [dbo].[Sexes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SexPerson'
CREATE INDEX [IX_FK_SexPerson]
ON [dbo].[Persons]
    ([SexId]);
GO

-- Creating foreign key on [SpecialityId] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [FK_SpecialityPerson]
    FOREIGN KEY ([SpecialityId])
    REFERENCES [dbo].[Specialities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecialityPerson'
CREATE INDEX [IX_FK_SpecialityPerson]
ON [dbo].[Persons]
    ([SpecialityId]);
GO

-- Creating foreign key on [PersonId] in table 'PersonConferences'
ALTER TABLE [dbo].[PersonConferences]
ADD CONSTRAINT [FK_PersonPersonConference]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPersonConference'
CREATE INDEX [IX_FK_PersonPersonConference]
ON [dbo].[PersonConferences]
    ([PersonId]);
GO

-- Creating foreign key on [ConferenceId] in table 'PersonConferences'
ALTER TABLE [dbo].[PersonConferences]
ADD CONSTRAINT [FK_ConferencePersonConference]
    FOREIGN KEY ([ConferenceId])
    REFERENCES [dbo].[Conferences]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConferencePersonConference'
CREATE INDEX [IX_FK_ConferencePersonConference]
ON [dbo].[PersonConferences]
    ([ConferenceId]);
GO

-- Creating foreign key on [CompanyId] in table 'PersonConferences_Detail'
ALTER TABLE [dbo].[PersonConferences_Detail]
ADD CONSTRAINT [FK_CompanyPersonConferenceDetail]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyPersonConferenceDetail'
CREATE INDEX [IX_FK_CompanyPersonConferenceDetail]
ON [dbo].[PersonConferences_Detail]
    ([CompanyId]);
GO

-- Creating foreign key on [CompanyId] in table 'PersonConferences_Payment'
ALTER TABLE [dbo].[PersonConferences_Payment]
ADD CONSTRAINT [FK_CompanyPersonConferenceMoney]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyPersonConferenceMoney'
CREATE INDEX [IX_FK_CompanyPersonConferenceMoney]
ON [dbo].[PersonConferences_Payment]
    ([CompanyId]);
GO

-- Creating foreign key on [PaymentTypeId] in table 'PersonConferences_Payment'
ALTER TABLE [dbo].[PersonConferences_Payment]
ADD CONSTRAINT [FK_PaymentTypePersonConferenceMoney]
    FOREIGN KEY ([PaymentTypeId])
    REFERENCES [dbo].[PaymentTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentTypePersonConferenceMoney'
CREATE INDEX [IX_FK_PaymentTypePersonConferenceMoney]
ON [dbo].[PersonConferences_Payment]
    ([PaymentTypeId]);
GO

-- Creating foreign key on [PersonConferenceId] in table 'PersonConferences_Detail'
ALTER TABLE [dbo].[PersonConferences_Detail]
ADD CONSTRAINT [FK_Detail_inherits_PersonConference]
    FOREIGN KEY ([PersonConferenceId])
    REFERENCES [dbo].[PersonConferences]
        ([PersonConferenceId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PersonConferenceId] in table 'PersonConferences_Payment'
ALTER TABLE [dbo].[PersonConferences_Payment]
ADD CONSTRAINT [FK_Payment_inherits_PersonConference]
    FOREIGN KEY ([PersonConferenceId])
    REFERENCES [dbo].[PersonConferences]
        ([PersonConferenceId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RankId] in table 'PersonConferences_Detail'
ALTER TABLE [dbo].[PersonConferences_Detail]
ADD CONSTRAINT [FK_RankPersonConferenceDetail]
    FOREIGN KEY ([RankId])
    REFERENCES [dbo].[Ranks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RankPersonConferenceDetail'
CREATE INDEX [IX_FK_RankPersonConferenceDetail]
ON [dbo].[PersonConferences_Detail]
    ([RankId]);
GO

-- Creating foreign key on [PersonId] in table 'Iacmacs'
ALTER TABLE [dbo].[Iacmacs]
ADD CONSTRAINT [FK_PersonIacmac]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ContactTypeId] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [FK_ContactTypeEmail]
    FOREIGN KEY ([ContactTypeId])
    REFERENCES [dbo].[ContactTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactTypeEmail'
CREATE INDEX [IX_FK_ContactTypeEmail]
ON [dbo].[Emails]
    ([ContactTypeId]);
GO

-- Creating foreign key on [ContactTypeId] in table 'Phones'
ALTER TABLE [dbo].[Phones]
ADD CONSTRAINT [FK_ContactTypePhone]
    FOREIGN KEY ([ContactTypeId])
    REFERENCES [dbo].[ContactTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactTypePhone'
CREATE INDEX [IX_FK_ContactTypePhone]
ON [dbo].[Phones]
    ([ContactTypeId]);
GO

-- Creating foreign key on [ContactTypeId] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_ContactTypeAddress]
    FOREIGN KEY ([ContactTypeId])
    REFERENCES [dbo].[ContactTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactTypeAddress'
CREATE INDEX [IX_FK_ContactTypeAddress]
ON [dbo].[Addresses]
    ([ContactTypeId]);
GO

-- Creating foreign key on [PersonId] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_PersonAddress]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonAddress'
CREATE INDEX [IX_FK_PersonAddress]
ON [dbo].[Addresses]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [FK_PersonEmail]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEmail'
CREATE INDEX [IX_FK_PersonEmail]
ON [dbo].[Emails]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'Phones'
ALTER TABLE [dbo].[Phones]
ADD CONSTRAINT [FK_PersonPhone]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPhone'
CREATE INDEX [IX_FK_PersonPhone]
ON [dbo].[Phones]
    ([PersonId]);
GO

-- Creating foreign key on [PersonConferenceId] in table 'Abstracts'
ALTER TABLE [dbo].[Abstracts]
ADD CONSTRAINT [FK_PersonConferenceAbstract]
    FOREIGN KEY ([PersonConferenceId])
    REFERENCES [dbo].[PersonConferences]
        ([PersonConferenceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonConferenceAbstract'
CREATE INDEX [IX_FK_PersonConferenceAbstract]
ON [dbo].[Abstracts]
    ([PersonConferenceId]);
GO

-- Creating foreign key on [AbstractId] in table 'AbstractWorks'
ALTER TABLE [dbo].[AbstractWorks]
ADD CONSTRAINT [FK_AbstractAbstractWork]
    FOREIGN KEY ([AbstractId])
    REFERENCES [dbo].[Abstracts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AbstractAbstractWork'
CREATE INDEX [IX_FK_AbstractAbstractWork]
ON [dbo].[AbstractWorks]
    ([AbstractId]);
GO

-- Creating foreign key on [AbstractStatusId] in table 'AbstractWorks'
ALTER TABLE [dbo].[AbstractWorks]
ADD CONSTRAINT [FK_AbstractStatusAbstractWork]
    FOREIGN KEY ([AbstractStatusId])
    REFERENCES [dbo].[AbstractStatuses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AbstractStatusAbstractWork'
CREATE INDEX [IX_FK_AbstractStatusAbstractWork]
ON [dbo].[AbstractWorks]
    ([AbstractStatusId]);
GO

-- Creating foreign key on [UserId] in table 'AbstractWorks'
ALTER TABLE [dbo].[AbstractWorks]
ADD CONSTRAINT [FK_UserAbstractWork]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAbstractWork'
CREATE INDEX [IX_FK_UserAbstractWork]
ON [dbo].[AbstractWorks]
    ([UserId]);
GO

-- Creating foreign key on [OrderStatusId] in table 'PersonConferences_Payment'
ALTER TABLE [dbo].[PersonConferences_Payment]
ADD CONSTRAINT [FK_OrderStatusPersonConferences_Payment]
    FOREIGN KEY ([OrderStatusId])
    REFERENCES [dbo].[OrderStatuses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderStatusPersonConferences_Payment'
CREATE INDEX [IX_FK_OrderStatusPersonConferences_Payment]
ON [dbo].[PersonConferences_Payment]
    ([OrderStatusId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------