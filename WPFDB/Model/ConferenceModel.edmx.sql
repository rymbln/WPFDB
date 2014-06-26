
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/26/2014 18:11:42
-- Generated from EDMX file: C:\Users\Inspiron\documents\visual studio 2012\Projects\WPFDB\WPFDB\Model\ConferenceModel.edmx
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
IF OBJECT_ID(N'[dbo].[FK_RankPersonConferenceDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Detail] DROP CONSTRAINT [FK_RankPersonConferenceDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyPersonConferenceDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Detail] DROP CONSTRAINT [FK_CompanyPersonConferenceDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentTypePersonConferenceMoney]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Payment] DROP CONSTRAINT [FK_PaymentTypePersonConferenceMoney];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyPersonConferenceMoney]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Payment] DROP CONSTRAINT [FK_CompanyPersonConferenceMoney];
GO
IF OBJECT_ID(N'[dbo].[FK_Detail_inherits_PersonConference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Detail] DROP CONSTRAINT [FK_Detail_inherits_PersonConference];
GO
IF OBJECT_ID(N'[dbo].[FK_Payment_inherits_PersonConference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonConferences_Payment] DROP CONSTRAINT [FK_Payment_inherits_PersonConference];
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
    [Role] nvarchar(max)  NOT NULL
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
    [DateArrive] datetime  NOT NULL,
    [IsComplect] bit  NOT NULL,
    [IsAdditionalMaterial] bit  NOT NULL,
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
    [PaymentDocument] nvarchar(max)  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [Money] decimal(18,0)  NOT NULL,
    [IsComplect] bit  NOT NULL,
    [OrderNumber] int  NOT NULL,
    [OrderStatus] int  NOT NULL,
    [SourceId] int  NOT NULL,
    [PersonConferenceId] uniqueidentifier  NOT NULL
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------