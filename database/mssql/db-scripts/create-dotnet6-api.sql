USE [master];

GO

IF NOT EXISTS(SELECT name
              FROM master.dbo.sysdatabases
              WHERE name = 'dotnet6-api-sample')
CREATE DATABASE [dotnet6-api-sample]

GO

USE [dotnet6-api-sample];

GO

IF NOT EXISTS(SELECT *
           FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_SCHEMA = 'dbo'
             AND TABLE_NAME = 'users')
    BEGIN
        create table dbo.users
        (
            Id  uniqueidentifier default NEWID()
                constraint users_pk
                primary key,
            FirstName    nvarchar(255),
            LastName     nvarchar(255),
            EmailAddress nvarchar(255)
        );

        create unique index users_Id_uindex on dbo.users (Id);
    END

GO

IF (NOT EXISTS (SELECT 1
           FROM dbo.users))
    BEGIN
        INSERT dbo.users (FirstName, LastName, EmailAddress)
        SELECT 'Amelia', 'Earhart', 'aearhart@transatlantic.com' UNION
        SELECT 'Charles', 'Lindbergh', 'clindbergh@transatlantic.com' UNION
        SELECT 'Manfred', 'von Richthofen', 'redbaron@aces.com' UNION
        SELECT 'Orville', 'Wright ', 'orville@fristtofly.com' UNION
        SELECT 'Wilbur', 'Wright ', 'wilbur@fristtofly.com'
    END

GO
