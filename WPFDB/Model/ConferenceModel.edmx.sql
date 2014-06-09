
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/09/2014 14:34:44
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

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Id] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [SecondName] nvarchar(max)  NOT NULL,
    [ThirdName] nvarchar(max)  NOT NULL,
    [BirthDate] nvarchar(max)  NOT NULL,
    [WorkPlace] nvarchar(max)  NOT NULL,
    [Post] nvarchar(max)  NOT NULL,
    [SpecialityId] uniqueidentifier  NOT NULL,
    [ScienceDegreeId] uniqueidentifier  NOT NULL,
    [ScienceStatusId] uniqueidentifier  NOT NULL,
    [SexId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ScienceDegrees'
CREATE TABLE [dbo].[ScienceDegrees] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ScienceStatuses'
CREATE TABLE [dbo].[ScienceStatuses] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sexes'
CREATE TABLE [dbo].[Sexes] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Specialities'
CREATE TABLE [dbo].[Specialities] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------