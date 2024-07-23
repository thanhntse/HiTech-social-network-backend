create database dbUser
use dbUser

CREATE TABLE [dbo].[user] (
    [user_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [email] NVARCHAR(256) NOT NULL,
    [password] NVARCHAR(256) NOT NULL,
    [first_name] NVARCHAR(256),
    [last_name] NVARCHAR(256),
    [phone] NVARCHAR(20),
    [address] NVARCHAR(512),
    [created_date] DATETIME NOT NULL,
    [role] NVARCHAR(50) NOT NULL,
    [is_deleted] BIT NOT NULL DEFAULT 0
);
