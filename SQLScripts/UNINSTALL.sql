USE [master]
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'AccountingPlusDB')
BEGIN TRY

ALTER DATABASE AccountingPlusDB
SET OFFLINE WITH ROLLBACK IMMEDIATE
ALTER DATABASE AccountingPlusDB
SET ONLINE
DROP DATABASE [AccountingPlusDB]

END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS ErrorNumber;
END CATCH;
GO

USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'AccountingPlusDB')
BEGIN TRY

ALTER DATABASE [AccountingPlusDB]
SET SINGLE_USER --or RESTRICTED_USER  
WITH ROLLBACK IMMEDIATE;  

END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS ErrorNumber;
END CATCH;
GO
  
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'AccountingPlusDB')
BEGIN TRY

DROP DATABASE [AccountingPlusDB]

END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS ErrorNumber;
END CATCH;
GO

