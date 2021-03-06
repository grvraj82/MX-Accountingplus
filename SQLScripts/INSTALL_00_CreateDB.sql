/* Create Database*/
USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'AccountingPlusDB')
--ALTER DATABASE AccountingPlusDB
--SET OFFLINE WITH ROLLBACK IMMEDIATE
--ALTER DATABASE AccountingPlusDB
--SET ONLINE
DROP DATABASE [AccountingPlusDB]
GO

CREATE DATABASE [AccountingPlusDB] 
GO
USE [AccountingPlusDB]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'AccountingPlusAdmin')

CREATE LOGIN [AccountingPlusAdmin] WITH PASSWORD='AccountingPlusAdmin', DEFAULT_DATABASE=[AccountingPlusDB], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'AccountingPlusAdmin')

CREATE USER [AccountingPlusAdmin] FOR LOGIN [AccountingPlusAdmin] WITH DEFAULT_SCHEMA=[dbo]

GO

USE [AccountingPlusDB]
GO

exec sp_addrolemember db_owner, [AccountingPlusAdmin]
GO  

USE [AccountingPlusDB]
GO
/* End of Create Database*/