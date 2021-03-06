/* Create Database*/
USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'AccountingPlusDB')
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
USE [AccountingPlusDB]
GO
/* End of Create Database*/

/****** Object:  StoredProcedure [dbo].[GetJobReport]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobReport]
GO
/****** Object:  StoredProcedure [dbo].[GetTopReports]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTopReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTopReports]
GO

/****** Object:  StoredProcedure [dbo].[GetLocalizedServerMessage]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedServerMessage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLocalizedServerMessage]
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedLabel]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedLabel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLocalizedLabel]
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedStrings]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedStrings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLocalizedStrings]
GO
/****** Object:  StoredProcedure [dbo].[AddLanguage]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddLanguage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddLanguage]
GO
/****** Object:  StoredProcedure [dbo].[GetJobPrice]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobPrice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobPrice]
GO
/****** Object:  StoredProcedure [dbo].[GetREPORTNEW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetREPORTNEW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetREPORTNEW]
GO


/****** Object:  StoredProcedure [dbo].[ManageFirstLogOn]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ManageFirstLogOn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ManageFirstLogOn]
GO
/****** Object:  StoredProcedure [dbo].[GetPricing]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPricing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPricing]
GO
/****** Object:  StoredProcedure [dbo].[RecordJobEvent]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordJobEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecordJobEvent]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUsageLimits]    Script Date: 03/05/2012 15:08:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateUsageLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateUsageLimits]
GO
/****** Object:  StoredProcedure [dbo].[AutoRefill]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AutoRefill]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AutoRefill]
GO
/****** Object:  StoredProcedure [dbo].[GetAccessRights]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAccessRights]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAccessRights]
GO
/****** Object:  UserDefinedFunction [dbo].[GetDatesforAday]    Script Date: 03/02/2012 19:49:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDatesforAday]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetDatesforAday]
GO
/****** Object:  StoredProcedure [dbo].[GetGraphicalReports]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGraphicalReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetGraphicalReports]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_NumXWeekDaysinMonth]    Script Date: 03/02/2012 19:49:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_NumXWeekDaysinMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_NumXWeekDaysinMonth]
GO
/****** Object:  Table [dbo].[T_AD_USERS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]') AND type in (N'U'))
DROP TABLE [dbo].[T_AD_USERS]
GO
/****** Object:  StoredProcedure [dbo].[ImportADUsers]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportADUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportADUsers]
GO
/****** Object:  StoredProcedure [dbo].[GetSlicedData]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSlicedData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSlicedData]
GO
/****** Object:  StoredProcedure [dbo].[GetJobUsageDetails]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobUsageDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobUsageDetails]
GO
/****** Object:  StoredProcedure [dbo].[GetAuditLog]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAuditLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAuditLog]
GO
/****** Object:  StoredProcedure [dbo].[GetJobUsage]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobUsage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobUsage]
GO
/****** Object:  Table [dbo].[T_AUTOREFILL_LIMITS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]') AND type in (N'U'))
DROP TABLE [dbo].[T_AUTOREFILL_LIMITS]
GO
/****** Object:  Table [dbo].[SQL_EXECUTION]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SQL_EXECUTION]') AND type in (N'U'))
DROP TABLE [dbo].[SQL_EXECUTION]
GO
/****** Object:  Table [dbo].[T_JOB_DISPATCHER]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_DISPATCHER]
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS]
GO
/****** Object:  StoredProcedure [dbo].[InsertMultipleJobLogs]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertMultipleJobLogs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertMultipleJobLogs]
GO
/****** Object:  StoredProcedure [dbo].[testreport]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[testreport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[testreport]
GO
/****** Object:  StoredProcedure [dbo].[Backup_Restore]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Backup_Restore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Backup_Restore]
GO
/****** Object:  Table [dbo].[T_DEVICE_FLEETS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]') AND type in (N'U'))
DROP TABLE [dbo].[T_DEVICE_FLEETS]
GO
/****** Object:  Table [dbo].[T_COSTCENTER_USERS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_COSTCENTER_USERS]') AND type in (N'U'))
DROP TABLE [dbo].[T_COSTCENTER_USERS]
GO
/****** Object:  Table [dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]') AND type in (N'U'))
DROP TABLE [dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]
GO
/****** Object:  StoredProcedure [dbo].[GetFleetReports]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFleetReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetFleetReports]
GO
/****** Object:  Table [dbo].[T_JOB_TRANSMITTER]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_TRANSMITTER]
GO
/****** Object:  Table [dbo].[T_ACCESS_RIGHTS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ACCESS_RIGHTS]') AND type in (N'U'))
DROP TABLE [dbo].[T_ACCESS_RIGHTS]
GO
/****** Object:  Table [dbo].[M_COUNTRIES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]') AND type in (N'U'))
DROP TABLE [dbo].[M_COUNTRIES]
GO
/****** Object:  StoredProcedure [dbo].[GetExecutiveSummary]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetExecutiveSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetExecutiveSummary]
GO
/****** Object:  Table [dbo].[M_PRICE_PROFILES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PRICE_PROFILES]') AND type in (N'U'))
DROP TABLE [dbo].[M_PRICE_PROFILES]
GO
/****** Object:  Table [dbo].[T_JOB_TRACKER]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_TRACKER]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_TRACKER]
GO
/****** Object:  Table [dbo].[M_USER_GROUPS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_USER_GROUPS]') AND type in (N'U'))
DROP TABLE [dbo].[M_USER_GROUPS]
GO
/****** Object:  Table [dbo].[T_SERVICE_MONITOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_SERVICE_MONITOR]') AND type in (N'U'))
DROP TABLE [dbo].[T_SERVICE_MONITOR]
GO
/****** Object:  StoredProcedure [dbo].[InsertMultipleRecords]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertMultipleRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertMultipleRecords]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceUnits]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceUnits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetInvoiceUnits]
GO
/****** Object:  Table [dbo].[RESX_CLIENT_MESSAGES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_CLIENT_MESSAGES]') AND type in (N'U'))
DROP TABLE [dbo].[RESX_CLIENT_MESSAGES]
GO
/****** Object:  Table [dbo].[APP_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[APP_SETTINGS]
GO
/****** Object:  UserDefinedFunction [dbo].[ConvertStringListToTable]    Script Date: 03/02/2012 19:49:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConvertStringListToTable]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ConvertStringListToTable]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoice]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetInvoice]
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedValues]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLocalizedValues]
GO
/****** Object:  Table [dbo].[M_MFPS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MFPS]') AND type in (N'U'))
DROP TABLE [dbo].[M_MFPS]
GO
/****** Object:  Table [dbo].[M_JOB_TYPES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_JOB_TYPES]') AND type in (N'U'))
DROP TABLE [dbo].[M_JOB_TYPES]
GO
/****** Object:  Table [dbo].[RESX_SERVER_MESSAGES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_SERVER_MESSAGES]') AND type in (N'U'))
DROP TABLE [dbo].[RESX_SERVER_MESSAGES]
GO
/****** Object:  Table [dbo].[T_CURRENT_JOBS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENT_JOBS]') AND type in (N'U'))
DROP TABLE [dbo].[T_CURRENT_JOBS]
GO
/****** Object:  Table [dbo].[AccountingPlus_Users]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountingPlus_Users]') AND type in (N'U'))
DROP TABLE [dbo].[AccountingPlus_Users]
GO
/****** Object:  Table [dbo].[T_JOBS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOBS]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOBS]
GO
/****** Object:  Table [dbo].[JOB_CONFIGURATION]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JOB_CONFIGURATION]') AND type in (N'U'))
DROP TABLE [dbo].[JOB_CONFIGURATION]
GO
/****** Object:  Table [dbo].[RESX_LABELS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_LABELS]') AND type in (N'U'))
DROP TABLE [dbo].[RESX_LABELS]
GO
/****** Object:  Table [dbo].[M_OSA_JOB_PROPERTIES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_OSA_JOB_PROPERTIES]') AND type in (N'U'))
DROP TABLE [dbo].[M_OSA_JOB_PROPERTIES]
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]
GO
/****** Object:  StoredProcedure [dbo].[GetPagedData]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPagedData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPagedData]
GO
/****** Object:  Table [dbo].[CostCenterDefaultPL]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CostCenterDefaultPL]') AND type in (N'U'))
DROP TABLE [dbo].[CostCenterDefaultPL]
GO
/****** Object:  Table [dbo].[M_MFP_GROUPS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MFP_GROUPS]') AND type in (N'U'))
DROP TABLE [dbo].[M_MFP_GROUPS]
GO
/****** Object:  Table [dbo].[M_ROLES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_ROLES]') AND type in (N'U'))
DROP TABLE [dbo].[M_ROLES]
GO
/****** Object:  Table [dbo].[M_DEPARTMENTS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]') AND type in (N'U'))
DROP TABLE [dbo].[M_DEPARTMENTS]
GO
/****** Object:  Table [dbo].[T_MFP_ACCESS_RIGHTS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_MFP_ACCESS_RIGHTS]') AND type in (N'U'))
DROP TABLE [dbo].[T_MFP_ACCESS_RIGHTS]
GO
/****** Object:  Table [dbo].[T_PRINT_JOBS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]') AND type in (N'U'))
DROP TABLE [dbo].[T_PRINT_JOBS]
GO
/****** Object:  Table [dbo].[M_COST_CENTERS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]') AND type in (N'U'))
DROP TABLE [dbo].[M_COST_CENTERS]
GO
/****** Object:  Table [dbo].[T_JOB_USAGE_PAPERSIZE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_USAGE_PAPERSIZE]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_USAGE_PAPERSIZE]
GO
/****** Object:  Table [dbo].[AD_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AD_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[AD_SETTINGS]
GO
/****** Object:  Table [dbo].[T_AUTO_REFILL]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]') AND type in (N'U'))
DROP TABLE [dbo].[T_AUTO_REFILL]
GO
/****** Object:  Table [dbo].[T_AUDIT_LOG]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUDIT_LOG]') AND type in (N'U'))
DROP TABLE [dbo].[T_AUDIT_LOG]
GO
/****** Object:  Table [dbo].[T_PRINT_JOB_WEB_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOB_WEB_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[T_PRINT_JOB_WEB_SETTINGS]
GO
/****** Object:  Table [dbo].[CARD_CONFIGURATION]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]') AND type in (N'U'))
DROP TABLE [dbo].[CARD_CONFIGURATION]
GO
/****** Object:  Table [dbo].[T_CURRENT_SESSIONS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENT_SESSIONS]') AND type in (N'U'))
DROP TABLE [dbo].[T_CURRENT_SESSIONS]
GO
/****** Object:  Table [dbo].[T_PRICES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRICES]') AND type in (N'U'))
DROP TABLE [dbo].[T_PRICES]
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]
GO
/****** Object:  Table [dbo].[M_USERS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_USERS]') AND type in (N'U'))
DROP TABLE [dbo].[M_USERS]
GO
/****** Object:  StoredProcedure [dbo].[UpdateRetryCount]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateRetryCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateRetryCount]
GO
/****** Object:  Table [dbo].[M_JOB_CATEGORIES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_JOB_CATEGORIES]') AND type in (N'U'))
DROP TABLE [dbo].[M_JOB_CATEGORIES]
GO
/****** Object:  Table [dbo].[APP_LANGUAGES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]') AND type in (N'U'))
DROP TABLE [dbo].[APP_LANGUAGES]
GO
/****** Object:  Table [dbo].[M_PAPER_SIZES]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PAPER_SIZES]') AND type in (N'U'))
DROP TABLE [dbo].[M_PAPER_SIZES]
GO
/****** Object:  StoredProcedure [dbo].[ManageSessions]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ManageSessions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ManageSessions]
GO
/****** Object:  Table [dbo].[T_ASSIGN_MFP_USER_GROUPS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ASSIGN_MFP_USER_GROUPS]') AND type in (N'U'))
DROP TABLE [dbo].[T_ASSIGN_MFP_USER_GROUPS]
GO
/****** Object:  Table [dbo].[INVALID_CARD_CONFIGURATION]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[INVALID_CARD_CONFIGURATION]') AND type in (N'U'))
DROP TABLE [dbo].[INVALID_CARD_CONFIGURATION]
GO
/****** Object:  Table [dbo].[T_GROUP_MFPS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_GROUP_MFPS]') AND type in (N'U'))
DROP TABLE [dbo].[T_GROUP_MFPS]
GO
/****** Object:  Table [dbo].[T_JOB_LOG]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_LOG]
GO
/****** Object:  Default [DF_APP_LANGUAGES_APP_CULTURE_DIR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_LANGUAGES_APP_CULTURE_DIR]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_LANGUAGES_APP_CULTURE_DIR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_LANGUAGES] DROP CONSTRAINT [DF_APP_LANGUAGES_APP_CULTURE_DIR]
END


End
GO
/****** Object:  Default [DF_APP_LANGUAGES_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_LANGUAGES_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_LANGUAGES_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_LANGUAGES] DROP CONSTRAINT [DF_APP_LANGUAGES_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_APP_SETTINGS_APPSETNG_KEY_ORDER]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_SETTINGS_APPSETNG_KEY_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_SETTINGS_APPSETNG_KEY_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_SETTINGS] DROP CONSTRAINT [DF_APP_SETTINGS_APPSETNG_KEY_ORDER]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_DATA_ON]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_DATA_ON]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_DATA_ON]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_CARD_DATA_ON]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_POSITION_START]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_POSITION_START]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_POSITION_START]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_CARD_POSITION_START]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]') AND parent_object_id = OBJECT_ID(N'[dbo].[INVALID_CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[INVALID_CARD_CONFIGURATION] DROP CONSTRAINT [DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]
END


End
GO
/****** Object:  Default [DF_M_COST_CENTERS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COST_CENTERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COST_CENTERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COST_CENTERS] DROP CONSTRAINT [DF_M_COST_CENTERS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COST_CENTERS] DROP CONSTRAINT [DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_M_COUNTRIES_REC_ACVITE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COUNTRIES_REC_ACVITE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COUNTRIES_REC_ACVITE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COUNTRIES] DROP CONSTRAINT [DF_M_COUNTRIES_REC_ACVITE]
END


End
GO
/****** Object:  Default [DF_M_COUNTRIES_REC_USER]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COUNTRIES_REC_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COUNTRIES_REC_USER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COUNTRIES] DROP CONSTRAINT [DF_M_COUNTRIES_REC_USER]
END


End
GO
/****** Object:  Default [DF_M_DEPARTMENTS_DEPT_SOURCE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_DEPARTMENTS_DEPT_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_DEPARTMENTS_DEPT_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_DEPARTMENTS] DROP CONSTRAINT [DF_M_DEPARTMENTS_DEPT_SOURCE]
END


End
GO
/****** Object:  Default [DF_M_DEPARTMENTS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_DEPARTMENTS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_DEPARTMENTS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_DEPARTMENTS] DROP CONSTRAINT [DF_M_DEPARTMENTS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_JOB_TYPES_ITEM_ORDER]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TYPES_ITEM_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_JOB_CATEGORIES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TYPES_ITEM_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_JOB_CATEGORIES] DROP CONSTRAINT [DF_T_JOB_TYPES_ITEM_ORDER]
END


End
GO
/****** Object:  Default [DF_M_JOB_TYPES_ITEM_ORDER]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_JOB_TYPES_ITEM_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_JOB_TYPES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_JOB_TYPES_ITEM_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_JOB_TYPES] DROP CONSTRAINT [DF_M_JOB_TYPES_ITEM_ORDER]
END


End
GO
/****** Object:  Default [DF_M_MFP_GROUPS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFP_GROUPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFP_GROUPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFP_GROUPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFP_GROUPS] DROP CONSTRAINT [DF_M_MFP_GROUPS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_ALLOW_NETWORK_PASSWORD]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_ALLOW_NETWORK_PASSWORD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_SSO]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_SSO]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_SSO]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_SSO]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]
END


End
GO
/****** Object:  Default [DF_M_MFPS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_REC_DATE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_REC_DATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_REC_DATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_REC_DATE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_PRINT_API]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_PRINT_API]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_PRINT_API]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_PRINT_API]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_EAM_ENABLED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_EAM_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_EAM_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_EAM_ENABLED]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_ACM_ENABLED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_ACM_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_ACM_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_ACM_ENABLED]
END


End
GO
/****** Object:  Default [DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_OSA_JOB_PROPERTIES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_OSA_JOB_PROPERTIES] DROP CONSTRAINT [DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]
END


End
GO
/****** Object:  Default [DF_M_PAPER_SIZES_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_PAPER_SIZES_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_PAPER_SIZES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_PAPER_SIZES_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_PAPER_SIZES] DROP CONSTRAINT [DF_M_PAPER_SIZES_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_SOURCE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_USR_SOURCE]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_DEPARTMENT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_DEPARTMENT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_DEPARTMENT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_USR_DEPARTMENT]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_COSTCENTER]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_COSTCENTER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_COSTCENTER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_USR_COSTCENTER]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_ROLE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_ROLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_ROLE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_USR_ROLE]
END


End
GO
/****** Object:  Default [DF_M_USERS_RETRY_COUNT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_RETRY_COUNT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_RETRY_COUNT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_RETRY_COUNT]
END


End
GO
/****** Object:  Default [DF_M_USERS_REC_CDATE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_REC_CDATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_REC_CDATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_REC_CDATE]
END


End
GO
/****** Object:  Default [DF_M_USERS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_USERS_ALLOW_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_ALLOW_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_ALLOW_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_ALLOW_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_CLIENT_MESSAGES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_CLIENT_MESSAGES] DROP CONSTRAINT [DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_RESX_LABELS_RESX_IS_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_LABELS_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_LABELS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_LABELS_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_LABELS] DROP CONSTRAINT [DF_RESX_LABELS_RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_RESX_SERVER_MESSAGES_RESX_IS_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_SERVER_MESSAGES_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_SERVER_MESSAGES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_SERVER_MESSAGES_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_SERVER_MESSAGES] DROP CONSTRAINT [DF_RESX_SERVER_MESSAGES_RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_T_AD_USERS_DEPARTMENT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AD_USERS_DEPARTMENT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AD_USERS_DEPARTMENT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AD_USERS] DROP CONSTRAINT [DF_T_AD_USERS_DEPARTMENT]
END


End
GO
/****** Object:  Default [DF_T_AD_USERS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AD_USERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AD_USERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AD_USERS] DROP CONSTRAINT [DF_T_AD_USERS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_AUTO_REFILL_AUTO_REFILL_FOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTO_REFILL_AUTO_REFILL_FOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTO_REFILL_AUTO_REFILL_FOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTO_REFILL] DROP CONSTRAINT [DF_T_AUTO_REFILL_AUTO_REFILL_FOR]
END


End
GO
/****** Object:  Default [DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTO_REFILL] DROP CONSTRAINT [DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_JOB_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_GROUP_MFPS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_GROUP_MFPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_GROUP_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_GROUP_MFPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_GROUP_MFPS] DROP CONSTRAINT [DF_T_GROUP_MFPS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_SIZE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] DROP CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] DROP CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] DROP CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_MFP_ID]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_MFP_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_MFP_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_MFP_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_GRUP_ID]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_GRUP_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_GRUP_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_GRUP_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_USR_SOURCE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_USR_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_USR_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_USR_SOURCE]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_PRICE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_PRICE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_PRICE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_JOB_PRICE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_PRICE_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_PRICE_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_PRICE_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_JOB_PRICE_BW]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_REC_DATE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_REC_DATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_REC_DATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_REC_DATE]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_JOB_SIZE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] DROP CONSTRAINT [DF_T_JOB_TRANSMITTER_JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] DROP CONSTRAINT [DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_LISTNER_PORT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_LISTNER_PORT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_LISTNER_PORT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] DROP CONSTRAINT [DF_T_JOB_TRANSMITTER_LISTNER_PORT]
END


End
GO
/****** Object:  Default [DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_USAGE_PAPERSIZE]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_USAGE_PAPERSIZE] DROP CONSTRAINT [DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_SIZE]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]
END


End
GO
/****** Object:  Default [DF_T_SERVICE_MONITOR_SRVC_TIME]    Script Date: 03/02/2012 19:49:49 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_SERVICE_MONITOR_SRVC_TIME]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_SERVICE_MONITOR]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_SERVICE_MONITOR_SRVC_TIME]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_SERVICE_MONITOR] DROP CONSTRAINT [DF_T_SERVICE_MONITOR_SRVC_TIME]
END


End
GO
/****** Object:  Table [dbo].[T_JOB_LOG]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_LOG](
	[REC_SLNO] [bigint] IDENTITY(1,1) NOT NULL,
	[MFP_ID] [int] NULL,
	[GRUP_ID] [int] NULL,
	[COST_CENTER_NAME] [nvarchar](100) NULL,
	[MFP_IP] [varchar](40) NULL,
	[MFP_MACADDRESS] [varchar](50) NULL,
	[USR_ID] [varchar](30) NULL,
	[USR_DEPT] [nvarchar](100) NULL,
	[USR_SOURCE] [varchar](2) NULL,
	[JOB_ID] [varchar](50) NULL,
	[JOB_MODE] [varchar](30) NULL,
	[JOB_TYPE] [varchar](20) NULL,
	[JOB_COMPUTER] [nvarchar](50) NULL,
	[JOB_USRNAME] [nvarchar](50) NULL,
	[JOB_START_DATE] [datetime] NULL,
	[JOB_END_DATE] [datetime] NULL,
	[JOB_COLOR_MODE] [varchar](20) NULL,
	[JOB_SHEET_COUNT_COLOR] [int] NULL,
	[JOB_SHEET_COUNT_BW] [int] NULL,
	[JOB_SHEET_COUNT] [int] NOT NULL,
	[JOB_PRICE_COLOR] [float] NULL,
	[JOB_PRICE_BW] [float] NULL,
	[JOB_PRICE_TOTAL] [float] NULL,
	[JOB_STATUS] [varchar](30) NULL,
	[JOB_PAPER_SIZE] [varchar](10) NULL,
	[JOB_FILE_NAME] [nvarchar](max) NULL,
	[DUPLEX_MODE] [varchar](20) NULL,
	[REC_DATE] [datetime] NULL,
 CONSTRAINT [PK_T_JOB_LOG_1] PRIMARY KEY CLUSTERED 
(
	[REC_SLNO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_GROUP_MFPS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_GROUP_MFPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_GROUP_MFPS](
	[GRUP_ID] [int] NOT NULL,
	[MFP_IP] [nvarchar](50) NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20) NULL
)
END
GO
/****** Object:  Table [dbo].[INVALID_CARD_CONFIGURATION]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[INVALID_CARD_CONFIGURATION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[INVALID_CARD_CONFIGURATION](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[CARD_TYPE] [nvarchar](50) NOT NULL,
	[CARD_RULE] [varchar](3) NOT NULL,
	[CARD_SUB_RULE] [varchar](10) NOT NULL,
	[CARD_DATA_ENABLED] [bit] NULL,
	[CARD_DATA_ON] [char](1) NULL,
	[CARD_POSITION_START] [int] NULL,
	[CARD_POSITION_LENGTH] [int] NULL,
	[CARD_DELIMETER_START] [nvarchar](50) NULL,
	[CARD_DELIMETER_END] [nvarchar](50) NULL,
	[CARD_CODE_VALUE] [nvarchar](50) NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
 CONSTRAINT [PK_INVALID_CARD_CONFIGURATION_1] PRIMARY KEY CLUSTERED 
(
	[CARD_TYPE] ASC,
	[CARD_RULE] ASC,
	[CARD_SUB_RULE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_ASSIGN_MFP_USER_GROUPS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ASSIGN_MFP_USER_GROUPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_ASSIGN_MFP_USER_GROUPS](
	[MFP_GROUP_ID] [int] NOT NULL,
	[COST_CENTER_ID] [int] NOT NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](50) NULL
)
END
GO
/****** Object:  StoredProcedure [dbo].[ManageSessions]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ManageSessions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ManageSessions] @maxUsers int, @sessionTimeOut int,  @clientSessionID nvarchar(50),  @clientMachineID nvarchar(50)
as 
delete from T_CURRENT_SESSIONS where 
( 
DATEDIFF(MINUTE, LAST_ACCESSTIME, GetDate()) > @sessionTimeOut 
)

declare @recordCount int
declare @isAccessAllowed bit
set @isAccessAllowed = 0
-- or CLIENT_SESSION_ID not in (select top @maxUsers CLIENT_SESSION_ID from T_CURRENT_SESSIONS order by LAST_ACCESSTIME desc)

if exists (select CLIENT_SESSION_ID from T_CURRENT_SESSIONS where CLIENT_SESSION_ID = @clientSessionID and CLIENT_MACHINE_ID = @clientMachineID)
	begin
		update T_CURRENT_SESSIONS set LAST_ACCESSTIME = GetDate() where CLIENT_SESSION_ID = @clientSessionID and CLIENT_MACHINE_ID = @clientMachineID
		set @isAccessAllowed = 1
	end
else
	begin
		select @recordCount = count(CLIENT_SESSION_ID) from T_CURRENT_SESSIONS
		if @recordCount < @maxUsers
			begin
				set @isAccessAllowed = 1
				insert into T_CURRENT_SESSIONS(CLIENT_SESSION_ID, CLIENT_MACHINE_ID, LAST_ACCESSTIME) values(@clientSessionID, @clientMachineID, GetDate())
			end
		else
			begin
				set @isAccessAllowed = 0
			end
	end
select @isAccessAllowed as ''IsAccessAllowed'', GetDate() as ''CurrentTime''

select * from T_CURRENT_SESSIONS
' 
END
GO
/****** Object:  Table [dbo].[M_PAPER_SIZES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PAPER_SIZES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_PAPER_SIZES](
	[SYS_ID] [int] IDENTITY(1,1) NOT NULL,
	[PAPER_SIZE_NAME] [nvarchar](50) NULL,
	[PAPER_SIZE_CATEGORY] [nvarchar](50) NULL,
	[REC_ACTIVE] [bit] NULL
)
END
GO
/****** Object:  Table [dbo].[APP_LANGUAGES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APP_LANGUAGES](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[APP_CULTURE] [varchar](10) NOT NULL,
	[APP_NEUTRAL_CULTURE] [varchar](10) NULL,
	[APP_LANGUAGE] [nvarchar](50) NULL,
	[APP_CULTURE_DIR] [varchar](3) NULL,
	[REC_ACTIVE] [bit] NULL,
 CONSTRAINT [PK_APP_LANGUAGES] PRIMARY KEY CLUSTERED 
(
	[APP_CULTURE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'APP_LANGUAGES', N'COLUMN',N'APP_CULTURE'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Culture ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'APP_LANGUAGES', @level2type=N'COLUMN',@level2name=N'APP_CULTURE'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'APP_LANGUAGES', N'COLUMN',N'APP_LANGUAGE'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Laungage' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'APP_LANGUAGES', @level2type=N'COLUMN',@level2name=N'APP_LANGUAGE'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'APP_LANGUAGES', N'COLUMN',N'APP_CULTURE_DIR'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Direction of UI/Text' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'APP_LANGUAGES', @level2type=N'COLUMN',@level2name=N'APP_CULTURE_DIR'
GO
/****** Object:  Table [dbo].[M_JOB_CATEGORIES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_JOB_CATEGORIES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_JOB_CATEGORIES](
	[JOB_ID] [varchar](20) NOT NULL,
	[ITEM_ORDER] [smallint] NULL,
 CONSTRAINT [PK_T_JOB_TYPES] PRIMARY KEY CLUSTERED 
(
	[JOB_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRetryCount]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateRetryCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE procedure [dbo].[UpdateRetryCount]

@UserID varchar(100),
@AllowedCount int,
@AuthType varchar(50)

as
Begin
	---Delete old date records
	update M_USERS set  RETRY_DATE = null , RETRY_COUNT = 0 where RETRY_DATE < getdate()
	update M_USERS set  RETRY_COUNT = 0 where RETRY_COUNT is null
	update M_USERS set RETRY_COUNT = RETRY_COUNT + 1 where USR_ID = @UserID and USR_SOURCE = @AuthType
	
if @AllowedCount != -1
	begin
		declare @currentRetryCount int 

		select @currentRetryCount = RETRY_COUNT from M_USERS where USR_ID = @UserID and USR_SOURCE = @AuthType

		if @currentRetryCount >= @AllowedCount
		Begin
			update M_USERS set REC_ACTIVE = 0 where USR_ID=@userID and USR_SOURCE=@AuthType
			select REC_ACTIVE from M_USERS where USR_ID=@userID and USR_SOURCE=@AuthType
		End
		else
		begin
			select REC_ACTIVE from M_USERS where USR_ID=@userID and USR_SOURCE=@AuthType
		end
	end
else
		select REC_ACTIVE from M_USERS where USR_ID=@userID and USR_SOURCE=@AuthType
End



' 
END
GO
/****** Object:  Table [dbo].[M_USERS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_USERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_USERS](
	[USR_ACCOUNT_ID] [int] IDENTITY(1000,1) NOT NULL,
	[USR_SOURCE] [varchar](2) NOT NULL,
	[USR_DOMAIN] [nvarchar](50) NOT NULL,
	[USR_ID] [nvarchar](100) NOT NULL,
	[USR_CARD_ID] [nvarchar](1000) NULL,
	[USR_NAME] [nvarchar](100) NULL,
	[USR_PIN] [nvarchar](200) NULL,
	[USR_PASSWORD] [nvarchar](200) NULL,
	[USR_ATHENTICATE_ON] [nvarchar](50) NULL,
	[USR_EMAIL] [varchar](100) NULL,
	[USR_DEPARTMENT] [int] NOT NULL,
	[USR_COSTCENTER] [int] NULL,
	[USR_AD_PIN_FIELD] [varchar](50) NULL,
	[USR_ROLE] [varchar](10) NOT NULL,
	[RETRY_COUNT] [tinyint] NULL,
	[RETRY_DATE] [datetime] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_ACTIVE] [bit] NULL,
	[ALLOW_OVER_DRAFT] [bit] NULL,
 CONSTRAINT [PK_M_USERS] PRIMARY KEY CLUSTERED 
(
	[USR_SOURCE] ASC,
	[USR_DOMAIN] ASC,
	[USR_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'M_USERS', N'COLUMN',N'USR_SOURCE'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Source of the user expected values, DB for database, AD for Active Directory' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'M_USERS', @level2type=N'COLUMN',@level2name=N'USR_SOURCE'
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL](
	[GRUP_ID] [int] NOT NULL,
	[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
	[JOB_TYPE] [varchar](20) NOT NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_USED] [bigint] NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
 CONSTRAINT [PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] PRIMARY KEY CLUSTERED 
(
	[GRUP_ID] ASC,
	[PERMISSIONS_LIMITS_ON] ASC,
	[JOB_TYPE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_PRICES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRICES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_PRICES](
	[PRICE_PROFILE_ID] [int] NOT NULL,
	[JOB_TYPE] [varchar](20) NOT NULL,
	[PAPER_SIZE] [nvarchar](50) NULL,
	[PRICE_PERUNIT_COLOR] [float] NULL,
	[PRICE_PERUNIT_BLACK] [float] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20) NULL
)
END
GO
/****** Object:  Table [dbo].[T_CURRENT_SESSIONS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENT_SESSIONS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_CURRENT_SESSIONS](
	[CLIENT_SESSION_ID] [nvarchar](50) NOT NULL,
	[CLIENT_MACHINE_ID] [nvarchar](50) NOT NULL,
	[LAST_ACCESSTIME] [datetime] NULL,
 CONSTRAINT [PK_T_CURRENT_SESSIONS] PRIMARY KEY CLUSTERED 
(
	[CLIENT_SESSION_ID] ASC,
	[CLIENT_MACHINE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[CARD_CONFIGURATION]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CARD_CONFIGURATION](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[CARD_TYPE] [nvarchar](50) NOT NULL,
	[CARD_RULE] [varchar](3) NOT NULL,
	[CARD_SUB_RULE] [varchar](10) NOT NULL,
	[CARD_DATA_ENABLED] [bit] NULL,
	[CARD_DATA_ON] [char](1) NULL,
	[CARD_POSITION_START] [int] NULL,
	[CARD_POSITION_LENGTH] [int] NULL,
	[CARD_DELIMETER_START] [nvarchar](50) NULL,
	[CARD_DELIMETER_END] [nvarchar](50) NULL,
	[CARD_CODE_VALUE] [nvarchar](50) NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
 CONSTRAINT [PK_CARD_CONFIGURATION_1] PRIMARY KEY CLUSTERED 
(
	[CARD_TYPE] ASC,
	[CARD_RULE] ASC,
	[CARD_SUB_RULE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_PRINT_JOB_WEB_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOB_WEB_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_PRINT_JOB_WEB_SETTINGS](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[USR_ID] [varchar](30) NOT NULL,
	[JOB_NAME] [nvarchar](150) NOT NULL,
	[DRIVER_PRINT_SETTING] [varchar](50) NOT NULL,
	[DRIVER_PRINT_SETTING_VALUE] [nvarchar](150) NULL,
	[OSA_SETTING] [varchar](50) NULL,
	[OSA_SETTING_VALUE] [nvarchar](150) NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](50) NULL,
	[REC_EDITOR] [nvarchar](50) NULL,
 CONSTRAINT [PK_T_PRINT_JOB_WEB_SETTINGS] PRIMARY KEY CLUSTERED 
(
	[USR_ID] ASC,
	[JOB_NAME] ASC,
	[DRIVER_PRINT_SETTING] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_AUDIT_LOG]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUDIT_LOG]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AUDIT_LOG](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[MSG_SOURCE] [varchar](30) NULL,
	[MSG_TYPE] [nvarchar](20) NULL,
	[MSG_TEXT] [nvarchar](max) NULL,
	[MSG_SUGGESTION] [nvarchar](max) NULL,
	[MSG_EXCEPTION] [nvarchar](max) NULL,
	[MSG_STACKSTRACE] [nvarchar](max) NULL,
	[REC_USER] [nvarchar](30) NULL,
	[REC_DATE] [datetime] NOT NULL,
 CONSTRAINT [PK_T_AUDIT_LOG] PRIMARY KEY CLUSTERED 
(
	[REC_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_AUTO_REFILL]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AUTO_REFILL](
	[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
	[AUTO_FILLING_TYPE] [nvarchar](50) NOT NULL,
	[AUTO_REFILL_FOR] [varchar](5) NULL,
	[ADD_TO_EXIST_LIMITS] [nvarchar](50) NOT NULL,
	[AUTO_REFILL_ON] [nvarchar](50) NOT NULL,
	[AUTO_REFILL_VALUE] [nvarchar](50) NOT NULL,
	[LAST_REFILLED_ON] [datetime] NULL,
	[IS_REFILL_REQUIRED] [bit] NULL
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'T_AUTO_REFILL', N'COLUMN',N'IS_REFILL_REQUIRED'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is refill details are modified' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_AUTO_REFILL', @level2type=N'COLUMN',@level2name=N'IS_REFILL_REQUIRED'
GO
/****** Object:  Table [dbo].[AD_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AD_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AD_SETTINGS](
	[SLNO] [int] IDENTITY(1,1) NOT NULL,
	[AD_SETTING_KEY] [nvarchar](100) NULL,
	[AD_SETTING_VALUE] [nvarchar](50) NULL,
	[AD_SETTING_DESCRIPTION] [nvarchar](50) NULL
)
END
GO
/****** Object:  Table [dbo].[T_JOB_USAGE_PAPERSIZE]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_USAGE_PAPERSIZE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_USAGE_PAPERSIZE](
	[REC_SLNO] [int] NOT NULL,
	[DEVICE_ID] [varchar](30) NULL,
	[JOB_TYPE] [nvarchar](30) NOT NULL,
	[JOB_PAPER_SIZE] [varchar](20) NULL,
	[JOB_SHEET_COUNT] [int] NULL
)
END
GO
/****** Object:  Table [dbo].[M_COST_CENTERS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_COST_CENTERS](
	[COSTCENTER_ID] [int] IDENTITY(1,1) NOT NULL,
	[COSTCENTER_NAME] [nvarchar](50) NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20) NULL,
	[ALLOW_OVER_DRAFT] [bit] NULL,
 CONSTRAINT [PK_M_COST_CENTERS] PRIMARY KEY CLUSTERED 
(
	[COSTCENTER_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_PRINT_JOBS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_PRINT_JOBS](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[JOB_RELEASER_ASSIGNED] [nvarchar](50) NULL,
	[USER_SOURCE] [varchar](50) NULL,
	[USER_ID] [varchar](50) NULL,
	[JOB_ID] [nvarchar](300) NULL,
	[JOB_FILE] [nvarchar](300) NULL,
	[JOB_SIZE] [bigint] NULL,
	[JOB_SETTINGS_ORIGINAL] [ntext] NULL,
	[JOB_SETTINGS_REQUEST] [ntext] NULL,
	[JOB_CHANGED_SETTINGS] [bit] NULL,
	[JOB_RELEASE_WITH_SETTINGS] [bit] NULL,
	[JOB_FTP_PATH] [varchar](500) NULL,
	[JOB_FTP_ID] [nvarchar](50) NULL,
	[JOB_FTP_PASSWORD] [nvarchar](50) NULL,
	[JOB_PRINT_RELEASED] [bit] NULL,
	[DELETE_AFTER_PRINT] [bit] NULL,
	[JOB_RELEASE_NOTIFY] [bit] NULL,
	[JOB_RELEASE_NOTIFY_EMAIL] [varchar](max) NULL,
	[JOB_RELEASE_NOTIFY_SMS] [varchar](500) NULL,
	[JOB_RELEASE_REQUEST_FROM] [nvarchar](50) NULL,
	[REC_DATE] [datetime] NULL,
 CONSTRAINT [PK_T_PRINT_JOBS] PRIMARY KEY CLUSTERED 
(
	[REC_SYSID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_MFP_ACCESS_RIGHTS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_MFP_ACCESS_RIGHTS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_MFP_ACCESS_RIGHTS](
	[MFP_GROUP_ID] [int] NOT NULL,
	[USR_GROUP_ID] [int] NOT NULL,
	[IS_ACCESS_ALLOWED] [bit] NOT NULL
)
END
GO
/****** Object:  Table [dbo].[M_DEPARTMENTS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_DEPARTMENTS](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[DEPT_NAME] [nvarchar](100) NOT NULL,
	[DEPT_SOURCE] [char](2) NULL,
	[DEPT_DESC] [nvarchar](250) NULL,
	[DEPT_RESX_ID] [varchar](50) NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](20) NULL,
	[REC_USER] [nvarchar](20) NULL,
 CONSTRAINT [PK_M_DEPARTMENTS] PRIMARY KEY CLUSTERED 
(
	[REC_SLNO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'M_DEPARTMENTS', N'COLUMN',N'DEPT_SOURCE'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AD/DB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'M_DEPARTMENTS', @level2type=N'COLUMN',@level2name=N'DEPT_SOURCE'
GO
/****** Object:  Table [dbo].[M_ROLES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_ROLES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_ROLES](
	[ROLE_ID] [varchar](10) NOT NULL,
	[ROLE_NAME] [nvarchar](50) NULL,
 CONSTRAINT [PK_M_ROLES] PRIMARY KEY CLUSTERED 
(
	[ROLE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[M_MFP_GROUPS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MFP_GROUPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_MFP_GROUPS](
	[GRUP_ID] [int] IDENTITY(1,1) NOT NULL,
	[GRUP_NAME] [nvarchar](50) NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](50) NULL,
 CONSTRAINT [PK_M_MFP_GROUPS] PRIMARY KEY CLUSTERED 
(
	[GRUP_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[CostCenterDefaultPL]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CostCenterDefaultPL]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CostCenterDefaultPL](
	[COSTCENTER_ID] [int] NOT NULL,
	[GRUP_ID] [int] NOT NULL,
	[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
	[JOB_TYPE] [varchar](20) NOT NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL
)
END
GO
/****** Object:  StoredProcedure [dbo].[GetPagedData]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPagedData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetPagedData]  
(  
@Tables varchar(1000),  
@PK varchar(100),  
@Sort varchar(200) = NULL,  
@PageNumber int = 1,  
@PageSize int = 10,  
@Fields varchar(1000) = ''*'',  
@Filter nvarchar(1000) = NULL,  
@Group varchar(1000) = NULL)  
AS  
  
/*Default Sorting*/  
IF @Sort IS NULL OR @Sort = ''''  
 SET @Sort = @PK  
  
/*Find the @PK type*/  
DECLARE @SortTable varchar(100)  
DECLARE @SortName varchar(100)  
DECLARE @strSortColumn varchar(200)  
DECLARE @operator char(2)  
DECLARE @type varchar(100)  
DECLARE @prec int  
  
/*Set sorting variables.*/   
IF CHARINDEX(''DESC'',@Sort)>0  
 BEGIN  
  SET @strSortColumn = REPLACE(@Sort, ''DESC'', '''')  
  SET @operator = ''<=''  
 END  
ELSE  
 BEGIN  
  IF CHARINDEX(''ASC'', @Sort) = 0  
   SET @strSortColumn = REPLACE(@Sort, ''ASC'', '''')  
  SET @operator = ''>=''  
 END  
  
  
IF CHARINDEX(''.'', @strSortColumn) > 0  
 BEGIN  
  SET @SortTable = SUBSTRING(@strSortColumn, 0, CHARINDEX(''.'',@strSortColumn))  
  SET @SortName = SUBSTRING(@strSortColumn, CHARINDEX(''.'',@strSortColumn) + 1, LEN(@strSortColumn))  
 END  
ELSE  
 BEGIN  
  SET @SortTable = @Tables  
  SET @SortName = @strSortColumn  
 END  
  
SELECT @type=t.name, @prec=c.prec  
FROM sysobjects o   
JOIN syscolumns c on o.id=c.id  
JOIN systypes t on c.xusertype=t.xusertype  
WHERE o.name = @SortTable AND c.name = @SortName  
  
IF CHARINDEX(''char'', @type) > 0  
   SET @type = @type + ''('' + CAST(@prec AS varchar) + '')''  
  
DECLARE @strPageSize varchar(50)  
DECLARE @strStartRow varchar(50)  
DECLARE @strFilter varchar(1000)  
DECLARE @strSimpleFilter varchar(1000)  
DECLARE @strGroup varchar(1000)  
  
/*Default Page Number*/  
IF @PageNumber < 1  
 SET @PageNumber = 1  
  
/*Set paging variables.*/  
SET @strPageSize = CAST(@PageSize AS varchar(50))  
SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS varchar(50))  
  
/*Set filter & group variables.*/  
IF @Filter IS NOT NULL AND @Filter != ''''  
 BEGIN  
  SET @strFilter = '' WHERE '' + @Filter + '' ''  
  SET @strSimpleFilter = '' AND '' + @Filter + '' ''  
 END  
ELSE  
 BEGIN  
  SET @strSimpleFilter = ''''  
  SET @strFilter = ''''  
 END  
IF @Group IS NOT NULL AND @Group != ''''  
 SET @strGroup = '' GROUP BY '' + @Group + '' ''  
ELSE  
 SET @strGroup = ''''  
   
/*Execute dynamic query*/   
EXEC(  
''  
DECLARE @SortColumn '' + @type + ''  
SET ROWCOUNT '' + @strStartRow + ''  
SELECT @SortColumn='' + @strSortColumn + '' FROM '' + @Tables + @strFilter + '' '' + @strGroup + '' ORDER BY '' + @Sort + ''
SET ROWCOUNT '' + @strPageSize + ''  
SELECT '' + @Fields + '' FROM '' + @Tables + '' WHERE '' + @strSortColumn + @operator + '' @SortColumn '' + @strSimpleFilter + '' '' + @strGroup + '' ORDER BY '' + @Sort + ''  
''  
)  
' 
END
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP](
	[GRUP_ID] [int] NOT NULL,
	[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
	[JOB_TYPE] [varchar](20) NOT NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_USED] [bigint] NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
 CONSTRAINT [PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] PRIMARY KEY CLUSTERED 
(
	[GRUP_ID] ASC,
	[PERMISSIONS_LIMITS_ON] ASC,
	[JOB_TYPE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[M_OSA_JOB_PROPERTIES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_OSA_JOB_PROPERTIES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_OSA_JOB_PROPERTIES](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[JOB_PROPERTY_CATEGORY] [varchar](50) NULL,
	[JOB_TYPE] [char](1) NOT NULL,
	[JOB_PROPERTY] [varchar](25) NOT NULL,
	[JOB_ORDER] [tinyint] NULL,
	[JOB_PROPERTY_RESX] [varchar](25) NULL,
	[JOB_PROPERTY_TYPE] [varchar](50) NULL,
	[JOB_PROPERTY_ALLOWED] [varchar](500) NULL,
	[JOB_PROPERTY_DEFAULT] [varchar](30) NULL,
	[JOB_PROPERTY_VALIDATAION] [varchar](100) NULL,
	[JOB_PROPERTY_SETTABLE] [bit] NULL,
	[JOB_PROPERTY_DRIVER_SETTING] [varchar](25) NULL,
	[JOB_PROPERTY_DRIVER_VALUES] [varchar](500) NULL,
 CONSTRAINT [PK_M_OSA_JOB_PROPERTIES_1] PRIMARY KEY CLUSTERED 
(
	[JOB_TYPE] ASC,
	[JOB_PROPERTY] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[RESX_LABELS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_LABELS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RESX_LABELS](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[RESX_MODULE] [nvarchar](30) NULL,
	[RESX_CULTURE_ID] [varchar](50) NOT NULL,
	[RESX_ID] [varchar](50) NOT NULL,
	[RESX_VALUE] [nvarchar](250) NULL,
	[RESX_IS_USED] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](50) NULL,
	[REC_EDITOR] [nvarchar](50) NULL,
 CONSTRAINT [PK_RESX_LABELS] PRIMARY KEY CLUSTERED 
(
	[RESX_CULTURE_ID] ASC,
	[RESX_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[JOB_CONFIGURATION]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JOB_CONFIGURATION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[JOB_CONFIGURATION](
	[SLNO] [int] IDENTITY(1,1) NOT NULL,
	[JOBSETTING_KEY] [varchar](100) NOT NULL,
	[JOBSETTING_VALUE] [varchar](50) NOT NULL,
	[JOBSETTING_DISCRIPTION] [varchar](200) NULL
)
END
GO
/****** Object:  Table [dbo].[T_JOBS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOBS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOBS](
	[SLNO] [int] IDENTITY(1,1) NOT NULL,
	[USER_ID] [varchar](100) NOT NULL,
	[JOB_ID] [varchar](200) NOT NULL,
	[FILE_TYPE] [varchar](50) NOT NULL,
	[FILE_DATA] [image] NULL,
	[CDATE] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[AccountingPlus_Users]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountingPlus_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountingPlus_Users](
	[USR_ACCOUNT_ID] [nvarchar](50) NULL,
	[USR_SOURCE] [nvarchar](50) NULL,
	[USR_DOMAIN] [nvarchar](50) NULL,
	[USR_ID] [nvarchar](50) NULL,
	[USR_CARD_ID] [nvarchar](50) NULL,
	[USR_NAME] [nvarchar](50) NULL,
	[USR_PIN] [nvarchar](50) NULL,
	[USR_PASSWORD] [nvarchar](50) NULL,
	[USR_ATHENTICATE_ON] [nvarchar](50) NULL,
	[USR_EMAIL] [nvarchar](50) NULL,
	[USR_DEPARTMENT] [nvarchar](50) NULL,
	[USR_AD_PIN_FIELD] [nvarchar](50) NULL,
	[USR_ROLE] [nvarchar](50) NULL,
	[RETRY_COUNT] [nvarchar](50) NULL,
	[RETRY_DATE] [nvarchar](50) NULL,
	[REC_CDATE] [nvarchar](50) NULL,
	[REC_ACTIVE] [nvarchar](50) NULL,
	[ALLOW_OVER_DRAFT] [nvarchar](50) NULL
)
END
GO
/****** Object:  Table [dbo].[T_CURRENT_JOBS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENT_JOBS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_CURRENT_JOBS](
	[JOB_ID] [int] IDENTITY(1,1) NOT NULL,
	[JOB_NAME] [nvarchar](500) NULL,
	[JOB_DATE] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[RESX_SERVER_MESSAGES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_SERVER_MESSAGES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RESX_SERVER_MESSAGES](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[RESX_MODULE] [nvarchar](30) NULL,
	[RESX_CULTURE_ID] [varchar](50) NOT NULL,
	[RESX_ID] [varchar](50) NOT NULL,
	[RESX_VALUE] [nvarchar](2000) NULL,
	[RESX_IS_USED] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](50) NULL,
	[REC_EDITOR] [nvarchar](50) NULL,
 CONSTRAINT [PK_RESX_SERVER_MESSAGES] PRIMARY KEY CLUSTERED 
(
	[RESX_CULTURE_ID] ASC,
	[RESX_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[M_JOB_TYPES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_JOB_TYPES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_JOB_TYPES](
	[JOB_ID] [varchar](20) NOT NULL,
	[ITEM_ORDER] [smallint] NULL,
 CONSTRAINT [PK_M_JOB_TYPES] PRIMARY KEY CLUSTERED 
(
	[JOB_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[M_MFPS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MFPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_MFPS](
[MFP_ID] [int] IDENTITY(1,1) NOT NULL,
	[MFP_IP] [nvarchar](30) NOT NULL,
	[MFP_NAME] [nvarchar](100) NULL,
	[MFP_SERIALNUMBER] [nvarchar](50) NULL,
	[MFP_MODEL] [nvarchar](50) NULL,
	[MFP_MAC_ADDRESS] [nvarchar](200) NULL,
	[MFP_LOGON_MODE] [varchar](20) NULL,
	[MFP_CARD_TYPE] [varchar](50) NULL,
	[MFP_CARDREADER_TYPE] [varchar](2) NULL,
	[MFP_MANUAL_AUTH_TYPE] [nvarchar](50) NULL,
	[MFP_LOGON_AUTH_SOURCE] [nvarchar](50) NULL,
	[ALLOW_NETWORK_PASSWORD] [bit] NULL CONSTRAINT [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]  DEFAULT ((0)),
	[MFP_SSO] [bit] NULL CONSTRAINT [DF_M_MFPS_MFP_SSO]  DEFAULT ((0)),
	[MFP_LOCK_DOMAIN_FIELD] [bit] NULL CONSTRAINT [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]  DEFAULT ((0)),
	[MFP_URL] [nvarchar](100) NULL,
	[MFP_LOCATION] [nvarchar](100) NULL,
	[REC_ACTIVE] [bit] NULL CONSTRAINT [DF_M_MFPS_REC_ACTIVE]  DEFAULT ((1)),
	[REC_DATE] [datetime] NULL CONSTRAINT [DF_M_MFPS_REC_DATE]  DEFAULT (getdate()),
	[REC_USER] [nvarchar](20) NULL,
	[MFP_DEVICE_ID] [nvarchar](50) NULL,
	[MFP_UI_LANGUAGE] [nvarchar](20) NULL,
	[MFP_PRINT_JOB_ACCESS] [nvarchar](10) NULL,
	[MFP_PRINT_API] [nvarchar](20) NULL CONSTRAINT [DF_M_MFPS_MFP_PRINT_API]  DEFAULT (N'FTP'),
	[FTP_PROTOCOL] [nvarchar](5) NULL,
	[FTP_ADDRESS] [nvarchar](100) NULL,
	[FTP_PORT] [nvarchar](5) NULL,
	[FTP_USER_ID] [nvarchar](30) NULL,
	[FTP_USER_PASSWORD] [nvarchar](50) NULL,
	[MFP_EAM_ENABLED] [bit] NULL CONSTRAINT [DF_M_MFPS_MFP_EAM_ENABLED]  DEFAULT ((0)),
	[MFP_ACM_ENABLED] [bit] NULL CONSTRAINT [DF_M_MFPS_MFP_ACM_ENABLED]  DEFAULT ((0)),
	[APP_THEME] [nvarchar](50) NULL,
 CONSTRAINT [PK_M_MFPS] PRIMARY KEY CLUSTERED 
(
	[MFP_IP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'M_MFPS', N'COLUMN',N'MFP_LOGON_MODE'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Card/Manual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'M_MFPS', @level2type=N'COLUMN',@level2name=N'MFP_LOGON_MODE'
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedValues]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedValues]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[GetLocalizedValues] @source varchar(20), @cultureID varchar(20), @stringIDs ntext
	
AS
BEGIN
	If @source = ''ServerMessages''
		Begin
			select * from RESX_SERVER_MESSAGES where RESX_CULTURE_ID = @cultureID and  RESX_ID in ( select * from ConvertStringListToTable(@stringIDs, '','') )
		End
	If @source = ''ClientMessages''
		Begin
			select * from RESX_CLIENT_MESSAGES where RESX_CULTURE_ID = @cultureID and RESX_ID in ( select * from ConvertStringListToTable(@stringIDs, '','') )
		End
	If @source = ''Labels''
		Begin
			select * from RESX_LABELS where RESX_CULTURE_ID = @cultureID and  RESX_ID in ( select * from ConvertStringListToTable(@stringIDs, '','') )
		End
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetInvoice]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoice]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Rajshekhar D
-- Create date: Dec 22, 08
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetInvoice] 
	-- Add the parameters for the stored procedure here
	@startDate DateTime , 
	@endDate DateTime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    create table #Invoice(SlNo int identity, JobItem varchar(20) default '''', JobType varchar(30) default '''', JobCount int default 0, JobPrice real default 0, TotalPrice real default 0)

	insert into #Invoice(JobType, JobCount, JobPrice, TotalPrice) select JOB_TYPE, 0, JOB_PRICE, 0 from M_JOB_PRICE order by ITEM_ORDER

	--select JOB_MODE as JobMode, JOB_COLOR_MODE as JobColorMode, sum(JOB_SHEET_COUNT) as JobCount into #JobUsage from T_JOB_LOG where (JOB_START_DATE >= @startDate and JOB_START_DATE <= @endDate) and JOB_STATUS = ''FINISHED''  group by JOB_MODE, JOB_COLOR_MODE order by JOB_MODE 

	select JOB_MODE as JobMode, JOB_COLOR_MODE as JobColorMode, sum(JOB_SHEET_COUNT) as JobCount into #JobUsage from T_JOB_LOG where JOB_STATUS = ''FINISHED''  group by JOB_MODE, JOB_COLOR_MODE order by JOB_MODE 
	
    update #Invoice set #Invoice.JobCount = #JobUsage.JobCount from #JobUsage, #Invoice  where JobMode = ''COPY'' and JobType = ''Copy Color'' and JobColorMode in(''FULL-COLOR'', ''DUAL-COLOR'', ''SINGLE-COLOR'')
	update #Invoice set #Invoice.JobCount = #JobUsage.JobCount from #JobUsage, #Invoice  where JobMode = ''COPY'' and JobType = ''Copy BW'' and JobColorMode = ''MONOCHROME''
	
	update #Invoice set #Invoice.JobCount = #JobUsage.JobCount from #JobUsage, #Invoice  where JobMode = ''PRINT'' and JobType = ''Print Color'' and JobColorMode = ''FULL-COLOR''
	update #Invoice set #Invoice.JobCount = #JobUsage.JobCount from #JobUsage, #Invoice  where JobMode = ''PRINT'' and JobType = ''Print BW'' and JobColorMode = ''MONOCHROME''

	update #Invoice set #Invoice.JobCount = #JobUsage.JobCount from #JobUsage, #Invoice  where JobMode = ''SCANNER'' and JobType = ''Scan Color'' and JobColorMode = ''FULL-COLOR''
	update #Invoice set #Invoice.JobCount = #JobUsage.JobCount from #JobUsage, #Invoice  where JobMode = ''SCANNER'' and JobType = ''Scan BW'' and JobColorMode = ''MONOCHROME''
	
	update #Invoice set #Invoice.JobCount = #JobUsage.JobCount from #JobUsage, #Invoice  where JobMode = ''FAX-SEND'' and JobType = ''Fax''
	update #Invoice set #Invoice.JobCount = #Invoice.JobCount + #JobUsage.JobCount from #JobUsage, #Invoice  where JobMode = ''FAX-PRINT'' and JobType = ''Fax''

    update #Invoice set TotalPrice = JobCount * JobPrice
    
	declare @SubTotal real ;
	declare @Tax real ;
	declare @Discount real ;	
    declare @Total real ;	

	set @Tax = 12.50 * 0.01;
	set @Discount = 5 * 0.01	

	select @SubTotal = Sum(TotalPrice) from #Invoice
	
	set @Tax = @SubTotal * @Tax
    
	set @Discount = @SubTotal * @Discount
	
	set @Total = @SubTotal + @Tax - @Discount

	insert into #Invoice(JobItem, TotalPrice) values(''Sub Total'', @SubTotal)
	insert into #Invoice(JobItem, TotalPrice) values(''Tax (12.5%)'', @Tax)
	insert into #Invoice(JobItem, TotalPrice) values(''Discount (5%)'', @Discount)
	insert into #Invoice(JobItem, TotalPrice) values(''Total'', @Total)

	select * from #Invoice
    select * from #JobUsage
	
	drop table #INVOICE
END





' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[ConvertStringListToTable]    Script Date: 03/02/2012 19:49:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConvertStringListToTable]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[ConvertStringListToTable](@ntext ntext, @splitCharacter char(1))

RETURNS @TokenVals table (TokenVal nvarchar(1024))

AS

BEGIN

  declare @maxStrLen int

  declare @s nvarchar(4000)

  declare @textLen int

  declare @textPos int

  declare @nextTextDelim int

  declare @strLen int

  declare @strPos int

  declare @nextStrDelim int

  declare @val nvarchar(1024)

  if @splitCharacter = ''''
  begin
    set @splitCharacter = '',''
  end

  set @maxStrLen = 4000

 

  set @textLen = datalength(@ntext) / 2 --2 bytes per char

  set @textPos = 1

 

  if (@textLen = 0) return

 

  -- Parse the text into strings of 8000 chars

  While (@textPos <= @textLen)

  Begin

 

     If ((@textLen - @textPos + 1) > @maxStrLen)

       begin

        set @s = substring(@ntext, @textPos, @maxStrLen)

        -- Search for the last delimiter within the string

        set @nextTextDelim = @maxStrLen - charindex(@splitCharacter, reverse(@s)) + 1

 

        -- cut the text to the last delimiter before the maxStrLen mark

        if (@nextTextDelim > 0)

           set @s = substring(@s, 1, @nextTextDelim - 1)

        else

           -- Rare case, happens only when the delimiter is exactly after the last char

           set @nextTextDelim = @maxStrLen + 1

 

        set @textPos = @textPos + @nextTextDelim

       end

     Else

       begin

        -- Last text block - convert it into string

        set @s = substring(@ntext, @textPos, @textLen - @textPos + 1)

        set @textPos = @textLen + 1

       end

 

 

     -- Parse the string

     set @strLen = len(@s)

     set @strPos = 0

     

     -- For each token, load it into the table

     While @strPos <= @strLen

     Begin

        -- get the next token

        set @nextStrDelim = charindex(@splitCharacter, @s, @strPos)

        if @nextStrDelim = 0

           set @nextStrDelim = @strLen + 1

     

        set @val = substring(@s, @strPos, @nextStrDelim - @strPos)

     

        -- load the table variable

        insert @TokenVals values (@val)

     

        -- increment to the next position, and handle the last token

        set @strPos = @nextStrDelim + 1

     End --string handling

 

  End --text handling

 

RETURN

END' 
END
GO
/****** Object:  Table [dbo].[APP_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APP_SETTINGS](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[APPSETNG_CATEGORY] [nvarchar](30) NOT NULL,
	[APPSETNG_KEY] [nvarchar](50) NOT NULL,
	[APPSETNG_RESX_ID] [varchar](100) NULL,
	[APPSETNG_KEY_DESC] [nvarchar](50) NULL,
	[APPSETNG_KEY_ORDER] [tinyint] NULL,
	[APPSETNG_VALUE] [nvarchar](50) NULL,
	[APPSETNG_VALUE_TYPE] [varchar](20) NULL,
	[ADS_LIST] [nvarchar](150) NULL,
	[ADS_LIST_VALUES] [nvarchar](150) NULL,
	[ADS_DEF_VALUE] [nvarchar](50) NULL,
	[CONTROL_TYPE] [nvarchar](50) NULL,
 CONSTRAINT [PK_APP_SETTINGS] PRIMARY KEY CLUSTERED 
(
	[APPSETNG_CATEGORY] ASC,
	[APPSETNG_KEY] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[RESX_CLIENT_MESSAGES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_CLIENT_MESSAGES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RESX_CLIENT_MESSAGES](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[RESX_MODULE] [nvarchar](30) NULL,
	[RESX_CULTURE_ID] [varchar](50) NOT NULL,
	[RESX_ID] [varchar](50) NOT NULL,
	[RESX_VALUE] [nvarchar](2000) NULL,
	[RESX_IS_USED] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](50) NULL,
	[REC_EDITOR] [nvarchar](50) NULL,
 CONSTRAINT [PK_RESX_CLIENT_MESSAGES] PRIMARY KEY CLUSTERED 
(
	[RESX_ID] ASC,
	[RESX_CULTURE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceUnits]    Script Date: 03/21/2012 21:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetInvoiceUnits] 
	@startDate DateTime , 
	@endDate DateTime,
	@CostCenter nvarchar(50),
	@userID nvarchar(30)
AS
BEGIN
	declare @ReportOn nvarchar(1000)
	declare @SQL varchar(max)

	create table #FileredLog(SlNo int identity, GRUP_ID int, MFP_IP nvarchar(20),USR_ID varchar(30),JOB_MODE varchar(30), JOB_START_DATE datetime, JOB_END_DATE datetime, JOB_COLOR_MODE varchar(20), JOB_SHEET_COUNT_COLOR int default 0, JOB_SHEET_COUNT_BW int default 0, JOB_PRICE_COLOR float default 0, JOB_PRICE_BW float default 0)
	
	if @startDate != '' and @endDate != ''
		begin
			insert into #FileredLog(GRUP_ID,MFP_IP,USR_ID,JOB_MODE,JOB_START_DATE,JOB_END_DATE,JOB_COLOR_MODE,JOB_SHEET_COUNT_COLOR,JOB_SHEET_COUNT_BW,JOB_PRICE_COLOR,JOB_PRICE_BW) select GRUP_ID,MFP_IP,USR_ID,JOB_MODE,JOB_START_DATE,JOB_END_DATE,JOB_COLOR_MODE,JOB_SHEET_COUNT_COLOR,JOB_SHEET_COUNT_BW,JOB_PRICE_COLOR,JOB_PRICE_BW from T_JOB_LOG where (JOB_START_DATE >= @startDate + ' 00:00' and JOB_END_DATE <= @endDate+ ' 23:59') and (JOB_STATUS='FINISHED' or JOB_STATUS='SUSPENDED' or JOB_STATUS='ERROR')
		end
	else
		begin
			insert into #FileredLog(GRUP_ID,MFP_IP,USR_ID,JOB_MODE,JOB_START_DATE,JOB_END_DATE,JOB_COLOR_MODE,JOB_SHEET_COUNT_COLOR, JOB_SHEET_COUNT_BW, JOB_PRICE_COLOR,JOB_PRICE_BW) select GRUP_ID, MFP_IP, USR_ID,JOB_MODE, JOB_START_DATE, JOB_END_DATE, JOB_COLOR_MODE, JOB_SHEET_COUNT_COLOR, JOB_SHEET_COUNT_BW, JOB_PRICE_COLOR,JOB_PRICE_BW from T_JOB_LOG where (JOB_STATUS='FINISHED' or JOB_STATUS='SUSPENDED' or JOB_STATUS='ERROR')
		end
	
	-- If Cost Center and Users are set to ALL (-1)
	if @CostCenter = '-1' and @userID = '-1' 
		begin
			set @ReportOn = '1=1'			
		end

	-- If Cost center is Selected and User 	
	if @CostCenter != '-1' and @userID = '-1' 
		begin
			set @ReportOn = 'GRUP_ID='''+@CostCenter+''''
		end
	-- If User only selected
	if @CostCenter = '-1' and @userID != '-1' 
		begin
			set @ReportOn = 'USR_ID='''+@userID+''''
		end

	-- IF Both CostCenter and User is Slected
	if @CostCenter != '-1' and @userID != '-1' 
		begin
			set @ReportOn = 'GRUP_ID='''+@CostCenter+''' and USR_ID='''+@userID+''''
		end
	--select * from #FileredLog
	set @SQL = 'select JOB_MODE, sum(JOB_PRICE_COLOR) as COLOR_PRICE, sum(JOB_PRICE_BW) as MONOCHROME_PRICE,sum(JOB_SHEET_COUNT_COLOR) as COLOR_COUNT,sum(JOB_SHEET_COUNT_BW) as MONOCHROME_COUNT from #FileredLog where 1=1 and ' + @ReportOn + ' group by JOB_MODE order by JOB_MODE'
	print @SQL
	exec(@SQL)
	drop table #FileredLog
END
GO
/****** Object:  StoredProcedure [dbo].[InsertMultipleRecords]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertMultipleRecords]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertMultipleRecords]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

-- Create the variables for the random number generation
DECLARE @Random int;
DECLARE @Upper int;
DECLARE @Lower int

-- This will create a random number between 1 and 999
SET @Lower = 1 -- The lowest random number
SET @Upper = 10000 -- The highest random number



DECLARE @intFlag INT
SET @intFlag = 1
WHILE (@intFlag <=5000000)
BEGIN
INSERT INTO [PrintRover].[dbo].[T_AUDIT_LOG]
           ([MSG_SOURCE]
           ,[MSG_TYPE]
           ,[MSG_TEXT]
           ,[MSG_SUGGESTION]
           ,[MSG_EXCEPTION]
           ,[MSG_STACKSTRACE]
           ,[REC_USER]
           ,[REC_DATE])
     SELECT
           [MSG_SOURCE]
           ,[MSG_TYPE]
           ,[MSG_TEXT]
           ,[MSG_SUGGESTION]
           ,[MSG_EXCEPTION]
           ,[MSG_STACKSTRACE]
           ,''PrintDataProvider1''
           ,GETDATE()
	FROM [PrintRover].[dbo].[T_AUDIT_LOG]
	WHERE REC_ID = @Random
SELECT @Random = Round(((@Upper - @Lower -1) * Rand() + @Lower), 0)
SET @intFlag = @intFlag + 1
END

    

END



' 
END
GO
/****** Object:  Table [dbo].[T_SERVICE_MONITOR]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_SERVICE_MONITOR]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_SERVICE_MONITOR](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[SRVC_NAME] [nvarchar](50) NULL,
	[SRVC_PORT] [smallint] NULL,
	[SRVC_STAUS] [nvarchar](10) NULL,
	[SRVC_TIME] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[M_USER_GROUPS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_USER_GROUPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_USER_GROUPS](
	[GRUP_ID] [int] IDENTITY(1,1) NOT NULL,
	[GRUP_NAME] [nvarchar](50) NOT NULL,
	[GRUP_SOURCE] [varchar](2) NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20) NULL,
	[AUTH_TYPE] [nvarchar](50) NULL,
	[SYNC_STATUS] [nvarchar](50) NULL,
 CONSTRAINT [PK_M_GROUPS_1] PRIMARY KEY CLUSTERED 
(
	[GRUP_NAME] ASC,
	[GRUP_SOURCE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'M_USER_GROUPS', N'COLUMN',N'GRUP_SOURCE'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Source of the user expected values DB or database, AD for Active Directory' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'M_USER_GROUPS', @level2type=N'COLUMN',@level2name=N'GRUP_SOURCE'
GO
/****** Object:  Table [dbo].[T_JOB_TRACKER]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_TRACKER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_TRACKER](
	[REC_SYS_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SRVC_NAME] [nvarchar](50) NULL,
	[SRVC_PORT] [smallint] NULL,
	[JOB_TEMP_FILE] [nvarchar](255) NULL,
	[JOB_PRN_FILE] [nvarchar](255) NULL,
	[JOB_SIZE] [decimal](18, 0) NULL,
	[JOB_NAME] [nvarchar](255) NULL,
	[JOB_MACHINE_NAME] [nvarchar](100) NULL,
	[JOB_DRIVER_NAME] [nvarchar](150) NULL,
	[JOB_DRIVER_TYPE] [nvarchar](100) NULL,
	[JOB_SPOOL_START_TIME] [nvarchar](150) NULL,
	[JOB_USER_SOURCE] [varchar](2) NULL,
	[JOB_USER_NAME] [nvarchar](100) NULL,
	[JOB_RX_START] [datetime] NULL,
	[JOB_RX_END] [datetime] NULL,
	[JOB_RX_TIME] [decimal](18, 0) NULL,
	[JOB_SPLIT_START] [datetime] NULL,
	[JOB_SPLIT_END] [datetime] NULL,
	[JOB_SPLIT_TIME] [decimal](18, 0) NULL,
	[JOB_RELEASE_START] [datetime] NULL,
	[JOB_RELEASE_END] [datetime] NULL,
	[JOB_RELEASE_TIME] [decimal](18, 0) NULL
)
END
GO
/****** Object:  Table [dbo].[M_PRICE_PROFILES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PRICE_PROFILES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_PRICE_PROFILES](
	[PRICE_PROFILE_ID] [int] IDENTITY(1,1) NOT NULL,
	[PRICE_PROFILE_NAME] [nvarchar](50) NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20) NULL,
 CONSTRAINT [PK_M_PRICE_PROFILES] PRIMARY KEY CLUSTERED 
(
	[PRICE_PROFILE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[GetExecutiveSummary]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetExecutiveSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[GetExecutiveSummary]
@fromDate varchar(10), 
@toDate  varchar(10)
as
begin
declare @dateCriteria varchar(1000)
declare @SQL varchar(max)
create table #tempDuplex(TotalPages int default 0, Duplex_mode varchar(50))
set @dateCriteria = '' ( JOB_END_DATE BETWEEN '''''' + @fromDate + '' 00:00'''' and '''''' +@toDate  + '' 23:59'''')''

set @SQL=''SELECT DATEDIFF(day,'''''' + @fromDate + '' 00:00'''','''''' +@toDate  + '' 23:59'''') as TotalNumberofDays''
exec(@SQL)


set @SQL=''SELECT COUNT(*) as TotalNumberofJobs FROM T_JOB_LOG where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT count(DISTINCT USR_ID) as TotalNumberofUsers FROM T_JOB_LOG  where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT case 
when SUM(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)  is null then 0
ELSE SUM(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)
end TotalPages
FROM T_JOB_LOG where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT case 
when SUM(JOB_SHEET_COUNT_BW)  is null then 0
ELSE SUM(JOB_SHEET_COUNT_BW)
end BWPages
FROM T_JOB_LOG where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT case 
when SUM(JOB_SHEET_COUNT_COLOR)  is null then 0
ELSE SUM(JOB_SHEET_COUNT_COLOR)
end ColorPages
FROM T_JOB_LOG where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT count(DISTINCT MFP_IP) as TotalNumberofDevices FROM T_JOB_LOG  where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT count(DISTINCT JOB_Computer) as TotalNumberofWorkStations FROM T_JOB_LOG where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and JOB_Computer<>''''''''  and '' + @dateCriteria + ''''
exec(@SQL)
end

set @SQL=''insert into #tempDuplex select sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as TotalPages,''''1SIDED'''' as DuplexMode  from T_JOB_LOG where duplex_Mode like ''''1%''''and '' + @dateCriteria + '' and (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')''

exec(@SQL)

set @SQL=''insert into #tempDuplex select sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as TotalPages,''''2SIDED'''' as DuplexMode  from T_JOB_LOG where duplex_Mode like ''''2%''''and '' + @dateCriteria + ''and (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')''

exec(@SQL)
update #tempDuplex set TotalPages=''0'' where TotalPages is null and duplex_mode = ''2SIDED''
update #tempDuplex set TotalPages=''0'' where TotalPages is null and duplex_mode = ''1SIDED''
set @SQL=''select * from #tempDuplex order by Duplex_mode desc''
exec(@SQL)

set @SQL=''SELECT   sum(JOB_SHEET_COUNT_COLOR) as ColorPages,sum(JOB_SHEET_COUNT_BW) as BWPages,JOB_PAPER_SIZE   FROM T_JOB_LOG
where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + '' and JOB_PAPER_SIZE like ''''A3%'''' group by JOB_PAPER_SIZE  order by JOB_PAPER_SIZE desc ''
exec(@SQL)
set @SQL=''SELECT   sum(JOB_SHEET_COUNT_COLOR) as ColorPages,sum(JOB_SHEET_COUNT_BW) as BWPages,JOB_PAPER_SIZE   FROM T_JOB_LOG
where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + '' and JOB_PAPER_SIZE like ''''A4%'''' group by JOB_PAPER_SIZE  order by JOB_PAPER_SIZE desc ''
exec(@SQL)
set @SQL=''SELECT   sum(JOB_SHEET_COUNT_COLOR) as ColorPages,sum(JOB_SHEET_COUNT_BW) as BWPages,JOB_PAPER_SIZE   FROM T_JOB_LOG
where (JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED'''')  and '' + @dateCriteria + '' and JOB_PAPER_SIZE Not like ''''A3%'''' and JOB_PAPER_SIZE Not like ''''A4%'''' group by JOB_PAPER_SIZE  order by JOB_PAPER_SIZE desc ''
exec(@SQL)

' 
END
GO
/****** Object:  Table [dbo].[M_COUNTRIES]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_COUNTRIES](
	[COUNTRY_ID] [int] IDENTITY(1,1) NOT NULL,
	[COUNTRY_NAME] [nvarchar](50) NOT NULL,
	[COUNTRY_DEFAULT] [bit] NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_USER] [int] NULL,
	[REC_DATE] [datetime] NULL,
 CONSTRAINT [PK_M_COUNTRIES] PRIMARY KEY CLUSTERED 
(
	[COUNTRY_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_ACCESS_RIGHTS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ACCESS_RIGHTS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_ACCESS_RIGHTS](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[ASSIGN_ON] [nvarchar](30) NOT NULL,
	[ASSIGN_TO] [nvarchar](30) NULL,
	[MFP_OR_GROUP_ID] [nvarchar](50) NULL,
	[USER_OR_COSTCENTER_ID] [nvarchar](50) NULL,
	[USR_SOURCE] [varchar](2) NULL
)
END
GO
/****** Object:  Table [dbo].[T_JOB_TRANSMITTER]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_TRANSMITTER](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[USER_SOURCE] [varchar](2) NULL,
	[USER_ID] [nvarchar](50) NULL,
	[JOB_DRIVER] [nvarchar](50) NULL,
	[JOB_FILE] [nvarchar](255) NULL,
	[JOB_SIZE] [int] NULL,
	[JOB_TRMSN_START] [datetime] NULL,
	[JOB_TRRX_END] [datetime] NULL,
	[JOB_TRMSN_DURATION] [int] NULL,
	[JOB_TRMSN_STATUS] [nvarchar](20) NULL,
	[LISTNER_PORT] [int] NULL,
	[TRNSMTR_OS] [nvarchar](100) NULL,
	[TRNSMTR_NAME] [nvarchar](50) NULL,
	[TRNSMTR_IP] [varchar](50) NULL,
	[TRNSMTR_PROCESSOR] [nvarchar](100) NULL,
	[REC_DATE] [datetime] NULL,
 CONSTRAINT [PK_T_JOB_TRANSMITTER] PRIMARY KEY CLUSTERED 
(
	[REC_SYSID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[GetFleetReports]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFleetReports]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetFleetReports] @deviceIP nvarchar(50)
as
BEGIN
	select top 1 * from T_DEVICE_FLEETS where device_id=@deviceIP order by DEVICE_LAST_UPDATE desc;
	select MFP_IP,MFP_NAME,MFP_SERIALNUMBER,MFP_MODEL,MFP_MAC_ADDRESS,MFP_LOCATION from M_MFPS where MFP_IP=@deviceIP
END
' 
END
GO
/****** Object:  Table [dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS](
	[COST_PROFILE_ID] [int] NOT NULL,
	[ASSIGNED_TO] [nvarchar](30) NULL,
	[MFP_GROUP_ID] [nvarchar](50) NOT NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](50) NULL
)
END
GO
/****** Object:  Table [dbo].[T_COSTCENTER_USERS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_COSTCENTER_USERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_COSTCENTER_USERS](
	[USR_ID] [nvarchar](50) NOT NULL,
	[COST_CENTER_ID] [nvarchar](50) NOT NULL,
	[USR_SOURCE] [nvarchar](2) NULL
)
END
GO
/****** Object:  Table [dbo].[T_DEVICE_FLEETS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_DEVICE_FLEETS](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[DEVICE_ID] [nvarchar](30) NOT NULL,
	[DEVICE_STATUS] [nvarchar](30) NULL,
	[DEVICE_LAST_UPDATE] [datetime] NOT NULL,
	[DEVICE_UPDATE_START] [datetime] NOT NULL,
	[DEVICE_UPDATE_END] [datetime] NOT NULL,
	[DEVICE_OUTPUT_PRINT_BW] [int] NULL,
	[DEVICE_OUTPUT_PRINT_SINGLE_COLOR] [int] NULL,
	[DEVICE_OUTPUT_PRINT_TWO_COLOR] [int] NULL,
	[DEVICE_OUTPUT_PRINT_FULL_COLOR] [int] NULL,
	[DEVICE_OUTPUT_DOC_FILING_BW] [int] NULL,
	[DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR] [int] NULL,
	[DEVICE_OUTPUT_DOC_FILING_TWO_COLOR] [int] NULL,
	[DEVICE_OUTPUT_DOC_FILING_FULL_COLOR] [int] NULL,
	[DEVICE_OUTPUT_COPY_BW] [int] NULL,
	[DEVICE_OUTPUT_COPY_SINGLE_COLOR] [int] NULL,
	[DEVICE_OUTPUT_COPY_TWO_COLOR] [int] NULL,
	[DEVICE_OUTPUT_COPY_FULL_COLOR] [int] NULL,
	[DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW] [int] NULL,
	[DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR] [int] NULL,
	[DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR] [int] NULL,
	[DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR] [int] NULL,
	[DEVICE_OUTPUT_OTHERS_BW] [int] NULL,
	[DEVICE_OUTPUT_OTHERS_SINGLE_COLOR] [int] NULL,
	[DEVICE_OUTPUT_OTHERS_TWO_COLOR] [int] NULL,
	[DEVICE_OUTPUT_OTHERS_FULL_COLOR] [int] NULL,
	[DEVICE_SEND_SCAN_BW] [int] NULL,
	[DEVICE_SEND_SCAN_SINGLE_COLOR] [int] NULL,
	[DEVICE_SEND_SCAN_TWO_COLOR] [int] NULL,
	[DEVICE_SEND_SCAN_FULL_COLOR] [int] NULL,
	[DEVICE_SEND_SCAN_TO_HD_BW] [int] NULL,
	[DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR] [int] NULL,
	[DEVICE_SEND_SCAN_TO_HD_TWO_COLOR] [int] NULL,
	[DEVICE_SEND_SCAN_TO_HD_FULL_COLOR] [int] NULL,
	[DEVICE_SEND_INTERNET_FAX_BW] [int] NULL,
	[DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR] [int] NULL,
	[DEVICE_SEND_INTERNET_FAX_TWO_COLOR] [int] NULL,
	[DEVICE_SEND_INTERNET_FAX_FULL_COLOR] [int] NULL,
 CONSTRAINT [PK_T_DEVICE_FLEETS] PRIMARY KEY CLUSTERED 
(
	[DEVICE_ID] ASC,
	[DEVICE_LAST_UPDATE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[Backup_Restore]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Backup_Restore]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[Backup_Restore] 
@BackuporRestore varchar(20),
@MyFileNameRestore varchar(500),
@BackupPath varchar(2000)

	
AS
BEGIN
if @BackuporRestore = ''Backup''
begin

DECLARE
@MyFileName varchar(100)
SELECT
@MyFileName = (SELECT @BackupPath  +''AccountingPlusDB_BACKUP_'' + convert(varchar(50),GetDate(),112) +''_''+REPLACE(CONVERT(VARCHAR(20),GETDATE(),108),'':'','''') +''.bak'')
BACKUP
DATABASE AccountingPlusDB TO DISK = @MyFileName WITH FORMAT, MEDIANAME = ''D_SQLServerBackups'', NAME = ''Full Backup of the AccountingPlusDB Database'';
end

else if @BackuporRestore = ''Restore''
begin

RESTORE
DATABASE AccountingPlusDB FROM DISK = @MyFileNameRestore WITH FILE = 1, NOUNLOAD, REPLACE,STATS = 10
end


else if @BackuporRestore = ''Delete''
begin
EXECUTE master.dbo.xp_delete_file 0,@MyFileNameRestore
end

else if @BackuporRestore = ''Summary''
 begin
		DECLARE @db_name VARCHAR(100)
		SELECT @db_name = ''AccountingPlusDB''
		 
		-- Get Backup History for required database
		 
		SELECT TOP ( 30 )
		 s.database_name,
		 m.physical_device_name,
		 cast(CAST(s.backup_size / 1000000 AS INT) as varchar(14))
		 + '''' + ''MB'' as bkSize,
		 CAST(DATEDIFF(second, s.backup_start_date,
		 s.backup_finish_date) AS VARCHAR(4)) + ''''
		+ ''Seconds'' TimeTaken,
		 s.backup_start_date,
		 CAST(s.first_lsn AS varchar(50)) AS first_lsn,
		 CAST(s.last_lsn AS varchar(50)) AS last_lsn,
		 CASE s.[type]
		 WHEN ''D'' THEN ''Full''
		WHEN ''I'' THEN ''Differential''
		WHEN ''L'' THEN ''Transaction Log''
		END as BackupType,
		s.server_name,
		s.recovery_model
		FROM msdb.dbo.backupset s
		inner join msdb.dbo.backupmediafamily m ON s.media_set_id = m.media_set_id
		WHERE s.database_name = @db_name
		 ORDER BY backup_start_date desc,
		 backup_finish_date
 end
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[testreport]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[testreport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

create procedure [dbo].[testreport]
@ReportOn varchar(50),
@UserID varchar(50),
@AuthenticationSource varchar(2),
@fromDate varchar(10),
@toDate  varchar(10)
as 
begin

declare @sql varchar(max)

--Inserting JOBLog table data to Temp1 Table

select R.USR_ID,
case 
when R.JOB_COLOR_MODE =''MONOCHROME'' then ''Black and White''
ELSE ''''
end ''BlackandWhite'',
case 
when R.JOB_COLOR_MODE =''FULL-COLOR'' then ''Colour'' end ''Colour'',
case  
when R.JOB_COLOR_MODE =''MONOCHROME'' then R.JOB_SHEET_COUNT_BW 
ELSE ''''
end BWJObs,
case  
when R.JOB_COLOR_MODE =''FULL-COLOR'' then R.JOB_SHEET_COUNT_COLOR end ColourJObs,
case 
when R.JOB_PAPER_SIZE like ''A4%'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR) end A4,
case 
when R.JOB_PAPER_SIZE like ''Ledger%'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR) end Ledger,
case 
when R.JOB_PAPER_SIZE like ''A3%'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR) end A3,
case 
when R.JOB_PAPER_SIZE like ''Legal%'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR) end Legal,
case 
when R.JOB_PAPER_SIZE like ''Letter%'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR) end Letter,
case 
when R.JOB_PAPER_SIZE not in(select distinct JOB_PAPER_SIZE from T_JOB_LOG where JOB_PAPER_SIZE like ''A4%'' or JOB_PAPER_SIZE like ''A3%'' or JOB_PAPER_SIZE like ''Ledger%'' or JOB_PAPER_SIZE like ''Letter%'') then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR) end Other,
case R.DUPLEX_MODE
when ''1SIDED'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR)
end ''ONESIDED'',
case 
when  R.DUPLEX_MODE <>''1SIDED'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR)
end ''TWOSIDED'',
R.USR_DEPT,R.JOB_COMPUTER,R.MFP_IP,R.JOB_SHEET_COUNT,R.JOB_USRNAME,R.JOB_SHEET_COUNT_COLOR,R.JOB_SHEET_COUNT_BW,R.USR_SOURCE,M.MFP_SERIALNUMBER,M.MFP_MODEL into #TEMP1  from T_JOB_LOG R left join M_MFPS M on R.MFP_IP=M.MFP_IP where R.JOB_STATUS=''FINISHED'' and R.JOB_COLOR_MODE<>'''' and  R.JOB_END_DATE BETWEEN '''' + @fromDate + '' 00:00'' and '''' +@toDate  + '' 23:59''

--update #TEMP1 table null values
update #TEMP1 set A4=0 where A4 is null
update #TEMP1 set Ledger=0 where Ledger is null
update #TEMP1 set Legal=0 where Legal is null
update #TEMP1 set Letter=0 where Letter is null
update #TEMP1 set A3=0 where A3 is null
update #TEMP1 set Other=0 where Other is null
update #TEMP1 set ONESIDED=0 where ONESIDED is null
update #TEMP1 set TWOSIDED=0 where TWOSIDED is null

--Create #JobReport Table fro select satement
--select * from #TEMP1
create table #JobReport(slno int identity, ReportOf nvarchar(100) default '''', UserID nvarchar(100) default '''',
ReportOn nvarchar(100) default '''', Total int default 0, TotalBW int default 0, TotalColor int default 0
,Ledger int default 0
,A3 int default 0
,Legal int default 0
,Letter int default 0
,A4 int default 0
,Other int default 0
,Duplex_One_Sided float default 0
,Duplex_Two_Sided float default 0
,UserName nvarchar(100) default ''''
,Department nvarchar(100) default ''''
,ComputerName nvarchar(100) default ''''
,LoginName nvarchar(100) default ''''
,ModelName nvarchar(100) default ''''
,SerialNumber nvarchar(100) default ''''
,AuthenticationSource char(2) default ''''

)
--Total Calculation 
declare @TotalCalculation varchar(max)
declare @TotalColumnName varchar(50)
--Total text display column name based on reporton condiiotn
if(@ReportOn=''USR_ID'')
	  begin
		set @TotalColumnName=''UserName''
      end
if(@ReportOn=''MFP_IP'')
	  begin
		set @TotalColumnName=''SerialNumber''
      end
if(@ReportOn=''USR_DEPT'')
	  begin
		set @TotalColumnName=''Department''
      end
if(@ReportOn=''JOB_COMPUTER'')
	  begin
		set @TotalColumnName=''ComputerName''
      end

				set @TotalCalculation=''insert into #JobReport(''+@TotalColumnName+'',Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided)
				select ''''Total'''',(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as TOTAL,
				sum(JOB_SHEET_COUNT_BW) as TOTALBW,sum(JOB_SHEET_COUNT_COLOR) as TOTALCOLOUR,
				sum(Ledger) as TOTALLEDGER,sum(A3) as TOTALA3,
				sum(Legal) as TOTALLEGAL,sum(Letter) as Letter,sum(A4) as TOTALA4,
				sum(Other) as TOTALOTHER,sum(ONESIDED) as TotalONESIDED,
				sum(TWOSIDED) / 2 TWOSIDED from #TEMP1''

--User ID 
 if(@ReportOn=''USR_ID'')
	  begin
				set @sql=''insert into #JobReport(LoginName,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided,UserID,Department,ComputerName)
				select Job_UsrName as ''''User Name'''',(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR) as TotalColor,
				sum(Ledger) as Ledger,sum(A3) as A3,sum(Legal) as Legal,sum(Letter) as Letter,sum(A4) as A4,sum(Other) as Other,sum(ONESIDED) as ''''Duplex_One_Side'''',
				sum(TWOSIDED)/2 as ''''Duplex_Two_Side'''','' + @ReportOn + '',USR_DEPT as Department,JOB_COMPUTER as [Computer Name] from #TEMP1
				group by  '' + @ReportOn + '',Job_UsrName,USR_DEPT,JOB_COMPUTER,USR_SOURCE''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select UserName,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided,Department,ComputerName, UserID from  #JobReport 
	  end

--MFP IP
if(@ReportOn=''MFP_IP'')
	  begin
				set @sql=''insert into #JobReport(SerialNumber,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided,ModelName)
				select MFP_SERIALNUMBER as SerialNumber,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR) as TotalColor,sum(Ledger) as Ledger,sum(A3) as A3,sum(Legal) as Legal,sum(Letter) as Letter,sum(A4) as A4,sum(Other) as Other,sum(ONESIDED) as ''''Duplex_One_Side'''',
         		sum(TWOSIDED)/2 as ''''Duplex_Two_Side'''',MFP_MODEL from #TEMP1
				group by  '' + @ReportOn + '',MFP_SERIALNUMBER,MFP_MODEL,USR_SOURCE''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select SerialNumber,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided,ModelName from  #JobReport 
	 end

--Department
if(@ReportOn=''USR_DEPT'')
	  begin
				set @sql=''insert into #JobReport(Department,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided)
				select USR_DEPT,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR) as TotalColor,
				sum(Ledger) as Ledger,sum(A3) as A3,sum(Legal) as Legal,sum(Letter) as Letter,sum(A4) as A4,sum(Other) as Other,sum(ONESIDED) as ''''Duplex_One_Side'''',
				sum(TWOSIDED)/2 as ''''Duplex_Two_Side'''' from #TEMP1
				group by  '' + @ReportOn + '',USR_SOURCE''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select Department,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided from  #JobReport 
	end

--Computer
if(@ReportOn=''JOB_COMPUTER'')
	  begin
				set @sql=''insert into #JobReport(ComputerName,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided)
				select JOB_COMPUTER,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR) as TotalColor,
				sum(Ledger) as Ledger,sum(A3) as A3,sum(Legal) as Legal,sum(Letter) as Letter,sum(A4) as A4,sum(Other) as Other,sum(ONESIDED) as ''''Duplex_One_Side'''',
				sum(TWOSIDED)/2 as ''''Duplex_Two_Side'''' from #TEMP1
				group by  '' + @ReportOn + '',USR_SOURCE''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select ComputerName,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided from  #JobReport 
	end

end



' 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertMultipleJobLogs]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertMultipleJobLogs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:   <Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertMultipleJobLogs]
AS
BEGIN
      -- SET NOCOUNT ON added to prevent extra result sets from
      -- interfering with SELECT statements.
      SET NOCOUNT ON;

-- Create the variables for the random number generation
DECLARE @Random int;
DECLARE @Upper int;
DECLARE @Lower int

-- This will create a random number between 1 and 999
SET @Lower = 1 -- The lowest random number
SET @Upper = 3000 -- The highest random number

DECLARE @intUserFlag INT
SET @intUserFlag = 1
DECLARE @intJobLogFlag INT
SET @intJobLogFlag = 1
DECLARE @userId nvarchar(max)

WHILE (@intUserFlag <= 1000) -- users
BEGIN

SET @userId = ''Admin''
SET @userId = @userId + ( SELECT CAST(@intUserFlag as varchar(255)) )
SET @intJobLogFlag = 1
	WHILE (@intJobLogFlag <=1000)
	BEGIN
	
		SELECT @Random = Round(((@Upper - @Lower -1) * Rand() + @Lower), 0)

		INSERT INTO [MXAccountingPlus].[dbo].[T_JOB_LOG]
				   ([MFP_ID]
				   ,[GRUP_ID]
				   ,[MFP_IP]
				   ,[MFP_MACADDRESS]
				   ,[USR_ID]
				   ,[USR_DEPT]
				   ,[USR_SOURCE]
				   ,[JOB_ID]
				   ,[JOB_MODE]
				   ,[JOB_TYPE]
				   ,[JOB_COMPUTER]
				   ,[JOB_USRNAME]
				   ,[JOB_START_DATE]
				   ,[JOB_END_DATE]
				   ,[JOB_COLOR_MODE]
				   ,[JOB_SHEET_COUNT_COLOR]
				   ,[JOB_SHEET_COUNT_BW]
				   ,[JOB_SHEET_COUNT]
				   ,[JOB_PRICE_COLOR]
				   ,[JOB_PRICE_BW]
				   ,[JOB_STATUS]
				   ,[JOB_PAPER_SIZE]
				   ,[JOB_FILE_NAME]
				   ,[DUPLEX_MODE])
			 SELECT
				   [MFP_ID]
				   ,[GRUP_ID]
				   ,[MFP_IP]
				   ,[MFP_MACADDRESS]
				   ,@userId
				   ,[USR_DEPT]
				   ,[USR_SOURCE]
				   ,[JOB_ID]
				   ,[JOB_MODE]
				   ,[JOB_TYPE]
				   ,[JOB_COMPUTER]
				   ,[JOB_USRNAME]
				   ,GetDate()
				   ,GetDate()
				   ,[JOB_COLOR_MODE]
				   ,[JOB_SHEET_COUNT_COLOR]
				   ,[JOB_SHEET_COUNT_BW]
				   ,[JOB_SHEET_COUNT]
				   ,[JOB_PRICE_COLOR]
				   ,[JOB_PRICE_BW]
				   ,[JOB_STATUS]
				   ,[JOB_PAPER_SIZE]
				   ,[JOB_FILE_NAME]
				   ,[DUPLEX_MODE]
			  FROM [MXAccountingPlus].[dbo].[T_JOB_LOG]
			  WHERE REC_SLNO = @Random
		SET @intJobLogFlag = @intJobLogFlag + 1
	END
	SET @intUserFlag = @intUserFlag + 1
END
END



' 
END
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS](
	[GRUP_ID] [int] NOT NULL,
	[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
	[JOB_TYPE] [varchar](20) NOT NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_USED] [bigint] NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
 CONSTRAINT [PK_T_JOB_PERMISSIONS_LIMITS] PRIMARY KEY CLUSTERED 
(
	[GRUP_ID] ASC,
	[PERMISSIONS_LIMITS_ON] ASC,
	[JOB_TYPE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_JOB_DISPATCHER]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_DISPATCHER](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[USER_SOURCE] [varchar](2) NULL,
	[USER_ID] [nvarchar](50) NULL,
	[JOB_DRIVER] [nvarchar](50) NULL,
	[JOB_FILE] [nvarchar](255) NULL,
	[JOB_SIZE] [int] NULL,
	[JOB_SETTINGS_FILE_NAME] [nvarchar](100) NULL,
	[JOB_SETTINGS] [ntext] NULL,
	[JOB_DESTINATION_ADDRESS] [nvarchar](500) NULL,
	[JOB_DESTINATION_USER_ID] [varchar](50) NULL,
	[JOB_DESTINATION_USER_PASSWORD] [varchar](50) NULL,
	[JOB_DISPATCH_WITH_SETTINGS] [bit] NULL,
	[JOB_DISPATCH_START] [datetime] NULL,
	[JOB_DISPATCH_END] [datetime] NULL,
	[JOB_DISPACTCH_DURATION] [int] NULL,
	[JOB_DISPATCH_STATUS] [nvarchar](20) NULL,
	[DISPATCHER_SRVR_NAME] [nvarchar](50) NULL,
	[DISPATCHER_SRVR_IP] [varchar](50) NULL,
	[DISPATCHER_SRVR_OS] [nvarchar](100) NULL,
	[DISPATCHER_SRVR_PROCESSOR] [nvarchar](100) NULL,
	[REC_DATE] [datetime] NULL,
 CONSTRAINT [PK_T_JOB_DISPATCHER] PRIMARY KEY CLUSTERED 
(
	[REC_SYSID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[SQL_EXECUTION]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SQL_EXECUTION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SQL_EXECUTION](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[IP_ADDRESS] [nvarchar](50) NULL,
	[SQL_QUERY] [ntext] NULL,
	[REC_DATE] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[T_AUTOREFILL_LIMITS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AUTOREFILL_LIMITS](
	[GRUP_ID] [int] NOT NULL,
	[JOB_TYPE] [nvarchar](50) NOT NULL,
	[LIMITS_ON] [tinyint] NOT NULL,
	[IS_JOB_ALLOWED] [bit] NOT NULL,
	[JOB_LIMIT] [int] NULL,
	[JOB_USED] [bigint] NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL,
 CONSTRAINT [PK_T_AUTOREFILL_LIMITS] PRIMARY KEY CLUSTERED 
(
	[GRUP_ID] ASC,
	[JOB_TYPE] ASC,
	[LIMITS_ON] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[GetJobUsage]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobUsage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetJobUsage] @DeviceID varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	If(@DeviceID != '''')
		Begin
			select * into #JobUsage from T_JOB_USAGE where DEVICE_ID = @DeviceID

			alter table #JobUsage add Total int, SlNo int identity
			
			insert into #JobUsage(DEVICE_ID,PAPER_SIZE,COPY_COLOR,COPY_BW,PRINT_COLOR,PRINT_BW,SCAN_COLOR,SCAN_BW,FAX) select '''',''Total'',Sum(COPY_COLOR),Sum(COPY_BW),Sum(PRINT_COLOR),Sum(PRINT_BW),Sum(SCAN_COLOR),Sum(SCAN_BW),Sum(FAX) from #JobUsage
			UPDATE #JobUsage set Total = COPY_COLOR + COPY_BW + PRINT_COLOR + PRINT_BW + SCAN_COLOR + SCAN_BW + FAX
			select * from #JobUsage order by SlNo
			drop table #JobUsage
		End
	Else
		Begin
			
			select * into #JobUsageTotal from T_JOB_USAGE where 1 = 2
			alter table #JobUsageTotal add Total int, SlNo int identity
			-- Get Sum based on paper Size

			select Paper_SIZE as PaperSize, sum(COPY_COLOR) as CopyColor, 
			Sum(COPY_BW) as CopyBW, 
			Sum(PRINT_COLOR) as PrintColor,
			Sum(PRINT_BW) as PrintBw,
			Sum(SCAN_COLOR) as ScanColor,
			Sum(SCAN_BW) as ScanBw,
			Sum(FAX) as Fax into #PaperCount
			from T_JOB_USAGE group By Paper_SIZE,COPY_COLOR,COPY_BW,PRINT_COLOR , PRINT_BW , SCAN_COLOR , SCAN_BW , FAX

			--select * from #PaperCount

			insert into #JobUsageTotal(DEVICE_ID,PAPER_SIZE,COPY_COLOR,COPY_BW,PRINT_COLOR,PRINT_BW,SCAN_COLOR,SCAN_BW,FAX)
			select '''', PaperSize, CopyColor, CopyBW, PrintColor, PrintBw, ScanColor, ScanBw, Fax from #PaperCount

			insert into #JobUsageTotal(DEVICE_ID,PAPER_SIZE,COPY_COLOR,COPY_BW,PRINT_COLOR,PRINT_BW,SCAN_COLOR,SCAN_BW,FAX) select '''',''Total'',Sum(COPY_COLOR),Sum(COPY_BW),Sum(PRINT_COLOR),Sum(PRINT_BW),Sum(SCAN_COLOR),Sum(SCAN_BW),Sum(FAX) from #JobUsageTotal
			UPDATE #JobUsageTotal set Total = COPY_COLOR + COPY_BW + PRINT_COLOR + PRINT_BW + SCAN_COLOR + SCAN_BW + FAX
			
			select * from #JobUsageTotal order by SlNo

			drop table #PaperCount
			drop table #JobUsageTotal
			
		End
	
		--UPDATE #JobUsage set Total = COPY_COLOR + COPY_BW + PRINT_COLOR + PRINT_BW + SCAN_COLOR + SCAN_BW + FAX

END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAuditLog]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAuditLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[GetAuditLog] @pageSize int, @pageNumber int
as

declare @sqlQuery varchar(max)
declare @rowIndex int
declare @totalRows float
declare @totalPages float
set @rowIndex = @pageSize * ( @pageNumber -1)
set @sqlQuery = ''SELECT TOP '' + cast (@pageSize as varchar(20)) + '' * FROM (SELECT
   ROW_NUMBER() OVER (ORDER BY REC_ID desc) AS RowNumber,
   *
FROM
   dbo.T_AUDIT_LOG) _myResults
WHERE
   RowNumber >'' + cast (@rowIndex as varchar(20))
print @sqlQuery
exec(@sqlQuery)

SELECT @totalRows = rows FROM sysindexes WHERE id = OBJECT_ID(''T_AUDIT_LOG'') AND indid < 2

select @totalRows as TotalRows

select CEILING(@totalRows/@pageSize) as TotalPages' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetJobUsageDetails]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobUsageDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetJobUsageDetails] @DeviceID varchar(50)
AS
SET NOCOUNT ON;

if(@DeviceID != '''')
	BEGIN
		
		create table #JobUsage(slno int identity, DeviceId varchar(30) default '''',PaperSize varchar(30) default '''', CopyColor int default 0, CopyBW int default 0, ScanColor int default 0, ScanBW int default 0, PrintColor int default 0, PrintBW int default 0, Fax int default 0, Total int default 0)
	
		insert into #JobUsage(PaperSize) select distinct JOB_PAPER_SIZE from T_JOB_USAGE_PAPERSIZE where DEVICE_ID = @DeviceID

    
		create table #temp(DeviceId varchar(30), PaperSize varchar(30), SheetCount int default 0)
	
		-- Copy Color
		insert into #temp(DeviceId, PaperSize, SheetCount) select @DeviceID, JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Copy Color'' group by DEVICE_ID, JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage set CopyColor = SheetCount from #temp where #JobUsage.PaperSize = #temp.PaperSize
	
		-- Copy BW
		truncate table #temp
		insert into #temp(DeviceId, PaperSize, SheetCount) select @DeviceID, JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Copy Bw'' group by DEVICE_ID, JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage set CopyBW = SheetCount from #temp where #JobUsage.PaperSize = #temp.PaperSize

		-- Print Color
		truncate table #temp
		insert into #temp(DeviceId, PaperSize, SheetCount) select @DeviceID, JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Print Color'' group by DEVICE_ID, JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage set PrintColor = SheetCount from #temp where #JobUsage.PaperSize = #temp.PaperSize 

		-- Print BW
		truncate table #temp
		insert into #temp(DeviceId, PaperSize, SheetCount) select @DeviceID, JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Print BW'' group by DEVICE_ID, JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage set PrintBW = SheetCount from #temp where #JobUsage.PaperSize = #temp.PaperSize 

		-- Scan Color
		truncate table #temp
		insert into #temp(DeviceId, PaperSize, SheetCount) select @DeviceID, JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Scan Color'' group by DEVICE_ID, JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage set ScanColor = SheetCount from #temp where #JobUsage.PaperSize = #temp.PaperSize 

		-- Scan BW
		truncate table #temp
		insert into #temp(DeviceId, PaperSize, SheetCount) select @DeviceID, JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Scan BW'' group by DEVICE_ID, JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage set ScanBW = SheetCount from #temp where #JobUsage.PaperSize = #temp.PaperSize 

		-- Fax
		truncate table #temp
		insert into #temp(DeviceId, PaperSize, SheetCount) select @DeviceID, JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Fax'' group by DEVICE_ID, JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage set Fax = SheetCount from #temp where #JobUsage.PaperSize = #temp.PaperSize 

		insert into #JobUsage(DeviceId, PaperSize, CopyColor, CopyBW, PrintColor, PrintBW, ScanColor, ScanBW, Fax )
		select ''DeviceId'', ''Total'', sum(CopyColor), sum(CopyBW), sum(PrintColor), sum(PrintBW), sum(ScanColor), sum(ScanBW), sum(Fax) from #JobUsage 
		update #JobUsage Set Total = CopyColor + CopyBW + PrintColor + PrintBW + ScanColor + ScanBW + Fax 
			
		select * from #JobUsage
		drop table #JobUsage
		drop table #Temp
	END
Else
	Begin

		select * into #JobUsageTotal from T_JOB_USAGE_PAPERSIZE where 1 = 2
		create table #JobUsage1(slno int identity, PaperSize varchar(30) default '''', CopyColor int default 0, CopyBW int default 0, ScanColor int default 0, ScanBW int default 0, PrintColor int default 0, PrintBW int default 0, Fax int default 0, Total int default 0)
	
		insert into #JobUsage1(PaperSize) select distinct JOB_PAPER_SIZE from T_JOB_USAGE_PAPERSIZE 
    
		create table #temp1( PaperSize varchar(30), SheetCount int default 0)
		
		-- Copy Color
		insert into #temp1(PaperSize, SheetCount) select  JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Copy Color'' group by JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage1 set CopyColor = SheetCount from #temp1 where #JobUsage1.PaperSize = #temp1.PaperSize 
		
		-- Copy BW
		truncate table #temp1
		insert into #temp1( PaperSize, SheetCount) select JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Copy Bw'' group by  JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage1 set CopyBW = SheetCount from #temp1 where #JobUsage1.PaperSize = #temp1.PaperSize 

		-- Print Color
		truncate table #temp1
		insert into #temp1( PaperSize, SheetCount) select JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Print Color'' group by  JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage1 set PrintColor = SheetCount from #temp1 where #JobUsage1.PaperSize = #temp1.PaperSize 

		-- Print BW
		truncate table #temp1
		insert into #temp1( PaperSize, SheetCount) select JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Print BW'' group by  JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage1 set PrintBW = SheetCount from #temp1 where #JobUsage1.PaperSize = #temp1.PaperSize 

		-- Scan Color
		truncate table #temp1
		insert into #temp1( PaperSize, SheetCount) select JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Scan Color'' group by  JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage1 set ScanColor = SheetCount from #temp1 where #JobUsage1.PaperSize = #temp1.PaperSize 

		-- Scan BW
		truncate table #temp1
		insert into #temp1( PaperSize, SheetCount) select JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Scan BW'' group by  JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage1 set ScanBW = SheetCount from #temp1 where #JobUsage1.PaperSize = #temp1.PaperSize 

		-- Fax
		truncate table #temp1
		insert into #temp1( PaperSize, SheetCount) select JOB_PAPER_SIZE, sum(JOB_SHEET_COUNT) from T_JOB_USAGE_PAPERSIZE  where JOB_TYPE = ''Fax'' group by  JOB_PAPER_SIZE, JOB_SHEET_COUNT
		update 	#JobUsage1 set Fax = SheetCount from #temp1 where #JobUsage1.PaperSize = #temp1.PaperSize 

		insert into #JobUsage1( PaperSize, CopyColor, CopyBW, PrintColor, PrintBW, ScanColor, ScanBW, Fax )
		select ''Total'', sum(CopyColor), sum(CopyBW), sum(PrintColor), sum(PrintBW), sum(ScanColor), sum(ScanBW), sum(Fax) from #JobUsage1
		update #JobUsage1 Set Total = CopyColor + CopyBW + PrintColor + PrintBW + ScanColor + ScanBW + Fax
			
		select * from #JobUsage1
		drop table #JobUsage1
		drop table #Temp1
END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSlicedData]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSlicedData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[GetSlicedData]
(
@tables varchar(1000),  
@primaryKey varchar(100),  
@sortOn varchar(200) = NULL,  
@pageNumber int = 1,  
@pageSize int = 10,  
@selectFields varchar(1000) = ''*'',  
@filterCriteria nvarchar(1000) = NULL,  
@groupOn varchar(1000) = NULL 
)
as

declare @sqlQuery varchar(max)

declare @whereCondition nvarchar(1050)

declare @rowIndex int
declare @totalRows float
declare @totalPages float
set @rowIndex = @pageSize * ( @pageNumber -1)

if @sortOn is null or @sortOn = ''''
Begin
	set @sortOn = @primaryKey
End

set @whereCondition = '' where 1 = 1 ''
if @filterCriteria is not null and @filterCriteria <> '''' 
Begin
	set @whereCondition = @whereCondition + '' and '' + @filterCriteria
End

set @sqlQuery = ''SELECT TOP '' + cast (@pageSize as varchar(20)) + '' '' + @selectFields +  '' FROM (SELECT
   ROW_NUMBER() OVER (ORDER BY '' + @sortOn + '') as RowNumber,
   '' + @selectFields + ''  
FROM
   dbo.'' + @tables + '' '' + @whereCondition + '' ) slicedData
WHERE
   RowNumber >'' + cast (@rowIndex as varchar(20))
print @sqlQuery
exec(@sqlQuery)

--SELECT @totalRows = rows FROM sysindexes WHERE id = OBJECT_ID(@tables) AND indid < 2
--select @totalRows = count (@primaryKey) from @tables @whereCondition
create table #TotalRows(TotalRowCount int)

--set @sqlQuery = ''select count ('' + @primaryKey +'') as TotalRows into #TotalRows from '' + @tables + '' '' + @whereCondition
set @sqlQuery = ''insert into #TotalRows select count ('' + @primaryKey +'') as TotalRows from '' + @tables + '' '' + @whereCondition
exec(@sqlQuery)
select @totalRows = TotalRowCount from #TotalRows
select @totalRows as TotalRows
select CEILING(@totalRows/@pageSize) as TotalPages' 
END
GO
/****** Object:  StoredProcedure [dbo].[ImportADUsers]    Script Date: 03/18/2012 00:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportADUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ImportADUsers] @sessionID varchar(50), @selectedUsers ntext, @importAllUsers varchar(10), @userSource varchar(5), @userRole varchar(10)
as

create table #SelectedUsers(UserID nvarchar(100))

if @importAllUsers = ''YES''
	begin
		insert into #SelectedUsers(UserID) select USER_ID from T_AD_USERS where SESSION_ID=@sessionID
	end
else
	begin
		insert into #SelectedUsers(UserID) select TokenVal from ConvertStringListToTable(@selectedUsers, '''')
	end

select * from  #SelectedUsers
insert into M_USERS(USR_SOURCE,USR_DOMAIN,USR_ID,USR_NAME,USR_EMAIL,USR_ROLE,REC_CDATE,REC_ACTIVE,USR_DEPARTMENT) 
select USR_SOURCE,DOMAIN,USER_ID,USER_NAME,EMAIL,@userRole,C_DATE,REC_ACTIVE,DEPARTMENT  from T_AD_USERS 
where USER_ID in (select UserID from #SelectedUsers)
and USER_ID not in (select USR_ID from M_USERS where USR_SOURCE = @userSource) and SESSION_ID=@sessionID

update M_USERS set USR_NAME = T_AD_USERS.USER_NAME, USR_EMAIL = T_AD_USERS.EMAIL, USR_DEPARTMENT = T_AD_USERS.DEPARTMENT, USR_ROLE= @userRole
from T_AD_USERS
where M_USERS.USR_ID = T_AD_USERS.USER_ID and M_USERS.USR_SOURCE = @userSource and T_AD_USERS.SESSION_ID=''@sessionID'' and M_USERS.USR_ID in (select UserID from #SelectedUsers)

-- Assign users to Default Cost Center
select UserID from  #SelectedUsers
delete from T_COSTCENTER_USERS where USR_ID in (select UserID from  #SelectedUsers) and COST_CENTER_ID=''1'' and USR_SOURCE=''AD''
insert into T_COSTCENTER_USERS(USR_ID,COST_CENTER_ID,USR_SOURCE) select UserID, ''1'', ''AD'' from #SelectedUsers' 
END
GO
/****** Object:  Table [dbo].[T_AD_USERS]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AD_USERS](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[USER_ID] [nvarchar](200) NULL,
	[SESSION_ID] [nvarchar](30) NULL,
	[USR_SOURCE] [varchar](2) NULL,
	[USR_ROLE] [varchar](10) NULL,
	[DOMAIN] [nvarchar](50) NULL,
	[FIRST_NAME] [nvarchar](200) NULL,
	[LAST_NAME] [nvarchar](200) NULL,
	[EMAIL] [varchar](100) NULL,
	[RESIDENCE_ADDRESS] [nvarchar](max) NULL,
	[COMPANY] [varchar](50) NULL,
	[STATE] [nvarchar](50) NULL,
	[COUNTRY] [nvarchar](50) NULL,
	[PHONE] [nvarchar](20) NULL,
	[EXTENSION] [nvarchar](50) NULL,
	[FAX] [nvarchar](50) NULL,
	[DEPARTMENT] [int] NULL,
	[USER_NAME] [nvarchar](200) NULL,
	[CN] [nvarchar](200) NULL,
	[DISPLAY_NAME] [nvarchar](200) NULL,
	[FULL_NAME] [nvarchar](200) NULL,
	[C_DATE] [datetime] NULL,
	[REC_ACTIVE] [bit] NULL
)
END
GO
/****** Object:  UserDefinedFunction [dbo].[udf_NumXWeekDaysinMonth]    Script Date: 03/02/2012 19:49:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_NumXWeekDaysinMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE Function [dbo].[udf_NumXWeekDaysinMonth](@Date datetime)
RETURNS smallint
AS
BEGIN
Declare @dte varchar(10)
Declare @TestDate varchar(10)



Declare @i smallint
Declare @iNumDays smallint

Set @dte = Convert(varchar(10),@Date,101)
Set @i = 1
Set @iNumDays = 0


While @i < 32
Begin
Set @TestDate = cast(month(@dte) as varchar(2)) + ''/'' + cast(@i as varchar(2)) + ''/'' + Cast(Year(@dte) as varchar(4))
--print @TestDate
IF isdate(@TestDate) = 1
BEGIN
IF (DATENAME(dw, @TestDate) = DATENAME(dw, @dte))
BEGIN
Set @iNumDays = @iNumDays + 1
END
END

Set @i = @i+1
End
Return @iNumDays
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetGraphicalReports]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGraphicalReports]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetGraphicalReports]
@ReportOn varchar(50), 
@AuthenticationSource varchar(2),  
@fromDate varchar(10), 
@toDate  varchar(10)

as
begin
declare @dateCriteria varchar(1000)
declare @SQL varchar(max)
set @dateCriteria = '' ( JOB_END_DATE BETWEEN '''''' + @fromDate + '' 00:00'''' and '''''' +@toDate  + '' 23:59'''')''

set @SQL=''select sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR) as TotalColor,(sum(JOB_SHEET_COUNT_BW)+sum(JOB_SHEET_COUNT_COLOR)) as TotalJobs from T_JOB_LOG
where JOB_STATUS=''''FINISHED'''' and '' + @dateCriteria + ''''
exec(@SQL)
--drop table #TEMP1
set @SQL=''select case R.DUPLEX_MODE when ''''1SIDED'''' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR) end ''''ONESIDED'''',
case when  R.DUPLEX_MODE <>''''1SIDED'''' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR) end ''''TWOSIDED'''' into #TEMP1 from T_JOB_LOG R where JOB_STATUS=''''FINISHED'''' and '' + @dateCriteria + ''
UPDATE #TEMP1 SET ONESIDED = 0 where  ONESIDED is null 
UPDATE #TEMP1 SET TWOSIDED = 0 where  TWOSIDED is null 
select sum(ONESIDED) as OneSided,sum(TWOSIDED) as TwoSided,(sum(ONESIDED)+sum(TWOSIDED)) as TotalJobs from #TEMP1''
--print @SQL
exec(@SQL)
set @SQL = ''select top 10 GRUP_ID,sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR) as TotalColor,(sum(JOB_SHEET_COUNT_BW)+sum(JOB_SHEET_COUNT_COLOR)) as Total from T_JOB_LOG
where JOB_STATUS=''''FINISHED'''' and '' + @dateCriteria + ''
group by GRUP_ID order by TotalColor desc''
exec(@SQL)
set @SQL=''select top 10 USR_ID,sum(JOB_SHEET_COUNT_BW) TotalBW,sum(JOB_SHEET_COUNT_COLOR) TotalColor,(sum(JOB_SHEET_COUNT_BW)+sum(JOB_SHEET_COUNT_COLOR)) TotalJobs from T_JOB_LOG
where JOB_STATUS=''''FINISHED'''' and '' + @dateCriteria + ''
GROUP BY USR_ID order by TotalJobs desc''
exec(@SQL)
end
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetDatesforAday]    Script Date: 03/02/2012 19:49:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDatesforAday]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'Create FUNCTION [dbo].[GetDatesforAday]
(
      -- Add the parameters for the function here
      @DtFrom DATETIME,
      @DtTo DATETIME,
      @DayName VARCHAR(12)
)

RETURNS @DateList TABLE (Dt datetime)

AS
BEGIN
      IF NOT (@DayName = ''Monday'' OR @DayName = ''Sunday'' OR @DayName = ''Tuesday'' OR @DayName = ''Wednesday'' OR @DayName = ''Thursday'' OR @DayName = ''Friday'' OR @DayName = ''Saturday'')
      BEGIN
            --Error Insert the error message and return

            INSERT INTO @DateList
            SELECT NULL AS DAT
            RETURN
      END 

      DECLARE @TotDays INT
      DECLARE @CNT INT

      SET @TotDays =  DATEDIFF(DD,@DTFROM,@DTTO)-- [NO OF DAYS between two dates]

      SET @CNT = 0
      WHILE @TotDays >= @CNT        -- repeat for all days 
      BEGIN
        -- Pick each single day and check for the day needed
            IF DATENAME(DW, (@DTTO - @CNT)) = @DAYNAME
            BEGIN
                  INSERT INTO @DateList
                 SELECT (@DTTO - @CNT) AS DAT
            END
            SET @CNT = @CNT + 1
      END
      RETURN
END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAccessRights]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAccessRights]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetAccessRights]
@userID nvarchar(50), 
@selectedCostCenter nvarchar(50),  
@deviceIpAddress nvarchar(50),
@limitsOn nvarchar(50)

as
begin
declare @count INT 
declare @SQL varchar(max)
	if(@limitsOn = ''Cost Center'')
	begin
		declare @CCenterName nvarchar(100)
		select @CCenterName = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID =''''+@selectedCostCenter+''''
		select @SQL = count(*) from  T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID in (select COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID =''''+@selectedCostCenter+'''') and MFP_OR_GROUP_ID='''' + @deviceIpAddress + ''''
		--print (@SQL)
		if(@SQL = ''0'')
		begin
			declare @MFPGName nvarchar(100)
			select @MFPGName = GRUP_NAME from M_MFP_GROUPS where GRUP_ID in( select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
			select @SQL = count(*) from  T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=@CCenterName and MFP_OR_GROUP_ID in (select GRUP_NAME from M_MFP_GROUPS where GRUP_ID in( select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+''''))
			
			--print (@SQL)
		end
	end
	else
	begin
		select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID='''' + @userID + '''' and MFP_OR_GROUP_ID='''' + @deviceIpAddress + ''''
		--print (@SQL)
		if(@SQL = ''0'')
			begin
				select @SQL = count(*) from T_GROUP_MFPS where MFP_IP='''' + @deviceIpAddress + '''' and GRUP_ID in (select GRUP_ID from M_MFP_GROUPS where GRUP_NAME in(select MFP_OR_GROUP_ID from T_ACCESS_RIGHTS where ASSIGN_ON=''MFP Groups'' and USER_OR_COSTCENTER_ID='''' + @userID + ''''))
				--print (@SQL)
				if(@SQL = ''0'')
					begin
						declare @CostCenterName nvarchar(100)
						declare @MFPGroupName nvarchar(100)
						select @CostCenterName = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID in (select COST_CENTER_ID from T_COSTCENTER_USERS where USR_ID=''''+@userID+'''')
						select @MFPGroupName = GRUP_NAME from M_MFP_GROUPS where GRUP_ID in( select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
						select @SQL = count(*) from T_ACCESS_RIGHTS	where USER_OR_COSTCENTER_ID in (select COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID in (select COST_CENTER_ID from T_COSTCENTER_USERS where USR_ID=''''+@userID+'''')) and MFP_OR_GROUP_ID='''' + @deviceIpAddress + ''''
						--print (@SQL)
						if(@SQL = ''0'')
							begin
								select @SQL = count(*) from  T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=@CostCenterName and MFP_OR_GROUP_ID in (select GRUP_NAME from M_MFP_GROUPS where GRUP_ID in( select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+''''))
								--print (@SQL)
							end
						else
							begin
								set @SQL = ''1''
							end
					end
				else
					set @SQL = ''1''
			end
		else
		begin
			set @SQL = ''1''
		end
	end
	select @SQL as count
end' 
END
GO
/****** Object:  StoredProcedure [dbo].[AutoRefill]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AutoRefill]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[AutoRefill] @refillFor varchar(5) 

as 

declare @addwithOldLimits varchar(10)
declare @refillMode varchar(20)

select @refillMode = AUTO_FILLING_TYPE, @addwithOldLimits = ADD_TO_EXIST_LIMITS  from T_AUTO_REFILL where AUTO_REFILL_FOR = @refillFor
Print @refillMode
Print @refillFor

if @refillMode = ''Automatic''
	begin
		if @refillFor = ''C''  -- C = Cost Center
			begin
				-- Build Data for Permissions and Limits for Cost Centers from configured data (T_JOB_PERMISSIONS_LIMITS_AUTOREFILL)
				select COSTCENTER_ID, GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED,ALERT_LIMIT,ALLOWED_OVER_DRAFT into #CostCenterDefaultPL from M_COST_CENTERS, T_JOB_PERMISSIONS_LIMITS_AUTOREFILL  where PERMISSIONS_LIMITS_ON = 0 group by COSTCENTER_ID, GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT,JOB_ISALLOWED, ALERT_LIMIT,ALLOWED_OVER_DRAFT order by COSTCENTER_ID, JOB_TYPE
				-- Update GRUP_ID = COSTCENTER_ID as both are same
				update #CostCenterDefaultPL set  GRUP_ID = COSTCENTER_ID 
				--select * from #CostCenterDefaultPL
				
				-- update with remaining limits
				if @addwithOldLimits = ''Yes''
					begin
						truncate table T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP
						-- Copy PL in to Temp Table
						insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP select * from T_JOB_PERMISSIONS_LIMITS	where GRUP_ID <> -1
						--select * from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP

						update  #CostCenterDefaultPL set #CostCenterDefaultPL.JOB_LIMIT = #CostCenterDefaultPL.JOB_LIMIT + (T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_LIMIT - T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_USED) from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP where #CostCenterDefaultPL.JOB_LIMIT < 214748360 and #CostCenterDefaultPL.GRUP_ID = T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.GRUP_ID and #CostCenterDefaultPL.JOB_TYPE = T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_TYPE
						update #CostCenterDefaultPL set #CostCenterDefaultPL.JOB_LIMIT = 2147483647 where #CostCenterDefaultPL.JOB_LIMIT > 2147483647
						truncate table T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP
						--select * from #CostCenterDefaultPL
					end
				
				-- Delete old Permissions and Limits
				Print ''Delete old Permissions and Limits''
				delete from T_JOB_PERMISSIONS_LIMITS where PERMISSIONS_LIMITS_ON = 0 and  GRUP_ID <> -1 -- for Cost Center
				
				-- insert auto refilled data
				Print ''insert auto refilled data''
				insert into T_JOB_PERMISSIONS_LIMITS(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED,ALERT_LIMIT,ALLOWED_OVER_DRAFT) select GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED,ALERT_LIMIT,ALLOWED_OVER_DRAFT from #CostCenterDefaultPL
				update T_AUTO_REFILL set LAST_REFILLED_ON=getdate(), IS_REFILL_REQUIRED=''0'' where AUTO_REFILL_FOR=@refillFor
			end
		else -- U = For Users
			begin
				-- Build Data for Permissions and Limits for Users from configured data (T_JOB_PERMISSIONS_LIMITS_AUTOREFILL)
				select USR_ACCOUNT_ID, USR_SOURCE, GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED,ALERT_LIMIT,ALLOWED_OVER_DRAFT into #UserDefaultPL from M_USERS, T_JOB_PERMISSIONS_LIMITS_AUTOREFILL  where PERMISSIONS_LIMITS_ON = 1 group by GRUP_ID,USR_ACCOUNT_ID, USR_SOURCE, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT,JOB_ISALLOWED, ALERT_LIMIT,ALLOWED_OVER_DRAFT order by USR_ACCOUNT_ID, USR_SOURCE, JOB_TYPE
				-- Update GRUP_ID = COSTCENTER_ID as both are same
				update #UserDefaultPL set  GRUP_ID = USR_ACCOUNT_ID 
				--select * from #CostCenterDefaultPL
				
				-- update with remaining limits
				if @addwithOldLimits = ''Yes''
					begin
						truncate table T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP
						-- Copy PL in to Temp Table
						insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP select * from T_JOB_PERMISSIONS_LIMITS	where GRUP_ID <> -1
						--select * from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP
						update  #UserDefaultPL set #UserDefaultPL.JOB_LIMIT = #UserDefaultPL.JOB_LIMIT + (T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_LIMIT - T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_USED) from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP where #UserDefaultPL.JOB_LIMIT < 214748360 and #UserDefaultPL.GRUP_ID = T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.GRUP_ID and #UserDefaultPL.JOB_TYPE = T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_TYPE
						update #UserDefaultPL set #UserDefaultPL.JOB_LIMIT = 2147483647 where #UserDefaultPL.JOB_LIMIT > 2147483647
						truncate table T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP
						--select * from #CostCenterDefaultPL
					end
				
				-- Delete old Permissions and Limits
				Print ''Delete old Permissions and Limits''
				delete from T_JOB_PERMISSIONS_LIMITS where PERMISSIONS_LIMITS_ON = 1 and  GRUP_ID <> -1 
				
				-- insert auto refilled data
				Print ''insert auto refilled data''
				insert into T_JOB_PERMISSIONS_LIMITS(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED,ALERT_LIMIT,ALLOWED_OVER_DRAFT) select GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED,ALERT_LIMIT,ALLOWED_OVER_DRAFT from #UserDefaultPL
				update T_AUTO_REFILL set LAST_REFILLED_ON=getdate(), IS_REFILL_REQUIRED=''0'' where AUTO_REFILL_FOR=@refillFor
			end
	end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_11_UpdatePermissionsAndLimts]    Script Date: 03/19/2012 11:31:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_11_UpdatePermissionsAndLimts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPGRADE_11_UpdatePermissionsAndLimts]
/****** Object:  StoredProcedure [dbo].[UPGRADE_11_UpdatePermissionsAndLimts]    Script Date: 03/18/2012 19:36:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[UPGRADE_11_UpdatePermissionsAndLimts]
as

ALTER TABLE M_JOB_CATEGORIES ALTER column JOB_ID varchar (50) NOT NULL 
ALTER TABLE M_JOB_TYPES ALTER column JOB_ID varchar (50) NOT NULL 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS ALTER column JOB_TYPE varchar (50) NOT NULL 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL ALTER column JOB_TYPE varchar (50) NOT NULL 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP ALTER column JOB_TYPE varchar (50) NOT NULL 

UPDATE T_JOB_PERMISSIONS_LIMITS set JOB_TYPE='Doc Filing Scan BW' where JOB_TYPE='Doc Filing BW'
UPDATE T_JOB_PERMISSIONS_LIMITS set JOB_TYPE='Doc Filing Scan Color' where JOB_TYPE='Doc Filing Color'

UPDATE M_JOB_CATEGORIES set JOB_ID='Doc Filing Scan' where JOB_ID='Doc-Filing'
UPDATE M_JOB_CATEGORIES set ITEM_ORDER='6' where JOB_ID='Fax'

UPDATE M_JOB_TYPES set JOB_ID='Doc Filing Scan BW',ITEM_ORDER='10' where JOB_ID='Doc Filing BW'
UPDATE M_JOB_TYPES set JOB_ID='Doc Filing Scan Color',ITEM_ORDER='9' where JOB_ID='Doc Filing Color'

INSERT INTO M_JOB_TYPES(JOB_ID, ITEM_ORDER) values('Doc Filing Print BW','8')
INSERT INTO M_JOB_TYPES(JOB_ID, ITEM_ORDER) values('Doc Filing Print Color','7')
UPDATE M_JOB_TYPES set ITEM_ORDER='11' where JOB_ID='Fax'
UPDATE M_JOB_TYPES set ITEM_ORDER='12' where JOB_ID='Settings'

INSERT INTO M_JOB_CATEGORIES(JOB_ID, ITEM_ORDER) values('Doc Filing Print','4')


declare @rowCount int
select @rowCount = count(*) from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL

if(@rowCount > 0 )
begin
	
	UPDATE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL set JOB_TYPE='Doc Filing Scan BW' where JOB_TYPE='Doc Filing BW'
	UPDATE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL set JOB_TYPE='Doc Filing Scan Color' where JOB_TYPE='Doc Filing Color'


	insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL 
	(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
	values 
	(-1, 0, 'Doc Filing Print Color', 0, 0, 0, 0, 0)

	insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL 
	(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
	values 
	(-1, 0, 'Doc Filing Print BW', 0, 0, 0, 0, 0)

	insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL 
	(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
	values 
	(-1, 1, 'Doc Filing Print Color', 0, 0, 0, 0, 0)

	insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL 
	(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
	values 
	(-1, 1, 'Doc Filing Print BW', 0, 0, 0, 0, 0)

end

-- insert Permissions and Limits for Users and Cost centers
insert into T_JOB_PERMISSIONS_LIMITS 
(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
values 
(1, 0, 'Doc Filing Print Color', 50, 0, 0, 0, 1)

insert into T_JOB_PERMISSIONS_LIMITS 
(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
values 
(1, 0, 'Doc Filing Print BW', 100, 0, 0, 0, 1)

select distinct GRUP_ID,PERMISSIONS_LIMITS_ON into #CostCenters from T_JOB_PERMISSIONS_LIMITS where GRUP_ID > 1 

insert into T_JOB_PERMISSIONS_LIMITS 
(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
select GRUP_ID, PERMISSIONS_LIMITS_ON , 'Doc Filing Print BW', 0, 0, 0, 0, 0 from #CostCenters

insert into T_JOB_PERMISSIONS_LIMITS 
(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
select GRUP_ID, PERMISSIONS_LIMITS_ON , 'Doc Filing Print Color', 0, 0, 0, 0, 0 from #CostCenters
GO

exec UPGRADE_11_UpdatePermissionsAndLimts
GO

/****** Object:  StoredProcedure [dbo].[UPGRADE_11_Data]    Script Date: 03/19/2012 11:33:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_11_Data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPGRADE_11_Data]
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_11_Data]    Script Date: 03/21/2012 21:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UPGRADE_11_Data]
as

INSERT [dbo].[APP_SETTINGS] ([APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (N'GeneralSettings', N'Anonymous Direct Print to MFP', N'ANONYMOUS_DIRECT_MFPPRINT', N'Anonymous Direct Print to MFP', 13, N'Enable', N'string', N'Enable,Disable', N'Enable,Disable', N'Enable', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (N'GeneralSettings', N'Authenticate User For Direct Print', N'AUTHENTICATE_DIRECTPRINT', N'Authenticate User For Direct Print', 14, N'No', N'string', N'Yes,No', N'Yes,No', N'No', N'DROPDOWN')

ALTER TABLE T_JOB_LOG ALTER column JOB_PAPER_SIZE varchar (100) NULL 

ALTER TABLE T_JOB_LOG ADD JOB_PAPER_SIZE_ORIGINAL varchar (100) NULL 
GO
exec UPGRADE_11_Data
GO
/****** Object:  StoredProcedure [dbo].[RecordJobEvent]    Script Date: 03/21/2012 21:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[RecordJobEvent]
	@deviceIP varchar(40), 
	@deviceMacAddress varchar(50), 
	@userAccountId int, 
	@jobNo varchar(50), 
	@jobMode varchar(30), 
	@jobComputer nvarchar(50), 
	@startDate DateTime, 
	@endDate DateTime, 
	@colorMode varchar(20), 
	@monochromeSheetCount int, 
	@colorSheetCount int, 
	@jobStatus varchar(30), 
	@originalPaperSize varchar(10), 
	@paperSize varchar(10), 
	@fileName nvarchar(255), 
	@duplexMode varchar(20), 
	@costCenter int 
as

declare @jobType nvarchar(30)
declare @costCenterAssigned nvarchar(100)
declare @userName nvarchar(50)
declare @userID varchar(30)
declare @isUserExists bit  
declare @isMFPExists bit  
declare @itemCount int
declare @colorPricePerUnit float
declare @monochromePricePerUnit float
declare @authenticationsource varchar(10)
declare @userDepartment nvarchar(30)
set @userDepartment = ''

declare @colorPriceTotal float
declare @monochromeTotal float
declare @jobPriceTotal float

declare @totalSheetCount int

set @costCenterAssigned = ''
set @jobType = ''

if exists( select MFP_IP from M_MFPS where MFP_IP = @deviceIP)
begin
	print 'MFP found IP : ' + @deviceIP 
	
	select @userName = USR_NAME,  @userID = USR_ID, @authenticationsource= USR_SOURCE from M_USERS where USR_ACCOUNT_ID = @userAccountId
	print 'User found User ID: ' + @userID
	if(@userAccountId = '100')
	begin
		set @userID = 'Unknown'
		set @costCenter = '1'
		set @userName = 'Unknown'
	end
	if 	@userID <> ''
		begin
			print 'User found User ID: ' + @userID


			-- Authentication Source
			--select @authenticationsource = MFP_LOGON_AUTH_SOURCE from M_MFPS where MFP_IP = @deviceIP
			-- TBD : Check whether permissions are defined for the user on MFP

			-- Get cost Center Assigned
			
			select @costCenterAssigned = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID = @costCenter
			
			if @costCenterAssigned <> null  print 'Cost Center : ' + @costCenterAssigned
						
			-- Get Price for Paper Size, Cost Center, MFP
			create table #jobPrice(colorPrice float, monochromePrice float)
			insert into #jobPrice exec GetJobPrice  @deviceIP, @costCenter , @jobMode, @paperSize
			select @colorPricePerUnit = colorPrice, @monochromePricePerUnit = monochromePrice from #jobPrice 
			
			set @colorPriceTotal = @colorPricePerUnit * @colorSheetCount
			set @monochromeTotal = @monochromePricePerUnit * @monochromeSheetCount
			set @jobPriceTotal = @colorPriceTotal + @monochromeTotal
			set @totalSheetCount = @monochromeSheetCount + @colorSheetCount

			declare @recordsCount int
			
			select @recordsCount = count(REC_SLNO) from T_JOB_LOG where JOB_ID = @jobNo and JOB_MODE = @jobMode

			if(@recordsCount = 0)
				begin
					insert into T_JOB_LOG
					(
						MFP_IP, MFP_MACADDRESS, GRUP_ID, USR_ID, USR_SOURCE,
						USR_DEPT, JOB_ID, JOB_MODE, JOB_TYPE, JOB_USRNAME, 
						JOB_COMPUTER, JOB_START_DATE, JOB_END_DATE, JOB_COLOR_MODE, JOB_SHEET_COUNT_COLOR,
						JOB_SHEET_COUNT_BW, JOB_SHEET_COUNT, JOB_PRICE_COLOR, JOB_PRICE_BW, JOB_PRICE_TOTAL,
						JOB_STATUS,	JOB_PAPER_SIZE_ORIGINAL, JOB_PAPER_SIZE, JOB_FILE_NAME, DUPLEX_MODE, COST_CENTER_NAME
					)
					values
					(
						@deviceIP, @deviceMacAddress, @costCenter, @userID, @authenticationsource,
						@userDepartment, @jobNo, @jobMode, @jobType, @userName,
						@jobComputer, @startDate, @endDate, @colorMode, @colorSheetCount,
						@monochromeSheetCount, @totalSheetCount, @colorPriceTotal, @monochromeTotal, @jobPriceTotal,
						@jobStatus, @originalPaperSize, @paperSize, @fileName, @duplexMode, @costCenterAssigned
					)
				end
			else
			begin
				if(@jobMode = 'Doc Filing Print' )
					begin
						update T_JOB_LOG set JOB_STATUS = @jobStatus where JOB_ID = @jobNo
					end
			end
		end
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateUsageLimits]    Script Date: 03/18/2012 00:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateUsageLimits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateUsageLimits] 
	@GroupID nvarchar(10), 
	@LimitsOn int, 
	@JobType nvarchar(50), 
	@SheetCount int, 
	@jobID varchar(50),
	@jobMode varchar(30)
 
AS
BEGIN
	declare @UserGroupID int
	declare @RecordsCount int

	set @UserGroupID = @GroupID
	select @RecordsCount = count(GRUP_ID) from T_JOB_PERMISSIONS_LIMITS where GRUP_ID = @GroupID and PERMISSIONS_LIMITS_ON=@LimitsOn
	print @RecordsCount
	if(@RecordsCount >= 1)
	begin
		set @UserGroupID = ''-1''
	end
	
	declare @jobLogRecordsCount int
	select @jobLogRecordsCount = count(REC_SLNO) from T_JOB_LOG where JOB_ID = @jobID and JOB_MODE = @jobMode
	print (@jobLogRecordsCount)
	if(@jobLogRecordsCount = 0)
		begin
			print @SheetCount
			print @GroupID
			print @JobType
			print @LimitsOn
			update T_JOB_PERMISSIONS_LIMITS set JOB_USED = JOB_USED + @SheetCount where GRUP_ID=@GroupID and JOB_TYPE = @JobType and PERMISSIONS_LIMITS_ON=@LimitsOn
		end
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPricing]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPricing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetPricing] 
@DeviceIPAddress nvarchar(100), 
@JobType nvarchar(20), 
@PaperSize nvarchar(20)

as
declare @costProfile nvarchar(max)
if(@JobType = ''SCANNER'')
begin
	set @JobType = ''Scan''
end
 set @costProfile = (select COST_PROFILE_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where ASSIGNED_TO=''MFP'' and MFP_GROUP_ID=''''+@DeviceIPAddress+'''')
 print (@costProfile)
 if @costProfile != ''''
	select * from T_PRICES where PRICE_PROFILE_ID=@costProfile and JOB_TYPE=@JobType and PAPER_SIZE = @PaperSize
 else
	begin
		print (''asdasd'')
		declare @GroupId int
		set @GroupId = (select GRUP_ID from M_MFP_GROUPS where GRUP_ID in(select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@DeviceIPAddress+''''))
		set @costProfile = (select COST_PROFILE_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where ASSIGNED_TO=''MFP Group'' and MFP_GROUP_ID=''''+@GroupId+'''')
		select * from T_PRICES where PRICE_PROFILE_ID=@costProfile and JOB_TYPE=@JobType and PAPER_SIZE = @PaperSize
	end



' 
END
GO
/****** Object:  StoredProcedure [dbo].[ManageFirstLogOn]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ManageFirstLogOn]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ManageFirstLogOn]        
 @userID nvarchar(20),        
 @password nvarchar(100),        
 @UserName nvarchar(50),        
 @Email nvarchar(50),        
 @AuthenticationType nvarchar(50),      
 @DomainName nVarchar(50),  
 @Department nvarchar(50),  
 @SettingsPassword nvarchar(50),
 @AuthenticationServer nvarchar(50)
 as         
        
BEGIN        
--1. Findout the use exists in M_USERS_AD        
declare        
@Count varchar(20)        
SET @Count = (SELECT count(*) FROM M_USERS WHERE USR_ID=@userID and USR_SOURCE=@AuthenticationType)        
UPDATE APP_SETTINGS set APPSETNG_VALUE=@AuthenticationType where  APPSETNG_KEY=''Authentication Settings''        
if(@AuthenticationType=''DB'')        
 BEGIN        
  --2. If exists, update role as USR_ROLE = "admin" and other inormation like Email, Last Name, .....        
   IF (@Count<>0)        
      BEGIN        
      update M_USERS set USR_ROLE=''admin'',USR_PASSWORD=@password,USR_NAME=@UserName,USR_EMAIL=@Email,USR_DOMAIN=@AuthenticationServer where USR_ID=@userID and USR_SOURCE=@AuthenticationType         
      --UPDATE APP_SETTINGS set APPSETNG_VALUE=@DomainName where  APPSETNG_KEY=''Domain Name'' and APPSETNG_CATEGORY=''GeneralSettings''       
     END        
  --3. If not exists, create new user in M_USERS_AD        
   ELSE        
      BEGIN        
      INSERT INTO  M_USERS(USR_CARD_ID,USR_ID,USR_PIN,USR_NAME,USR_PASSWORD,USR_EMAIL,USR_ROLE,REC_ACTIVE,USR_SOURCE,USR_DEPARTMENT,USR_DOMAIN) values('''',@userID,'''',@UserName,@password,@Email,''admin'',''1'',@AuthenticationType,@Department,@AuthenticationServer)        
      --UPDATE APP_SETTINGS set APPSETNG_VALUE=@DomainName where  APPSETNG_KEY=''Domain Name'' and APPSETNG_CATEGORY=''GeneralSettings''        
     END        
 END        
ELSE        
 BEGIN        
              
   --2. If exists, update role as USR_ROLE = "admin" and other inormation like Email, Last Name, .....        
    IF (@Count<>0)        
       BEGIN        
       update M_USERS set USR_ROLE=''admin''  where USR_ID=@userID and USR_SOURCE=@AuthenticationType     
       UPDATE AD_SETTINGS set AD_SETTING_VALUE=@DomainName where  AD_SETTING_KEY=''DOMAIN_CONTROLLER'' --and APPSETNG_CATEGORY=''GeneralSettings''  
       UPDATE AD_SETTINGS set AD_SETTING_VALUE=@DomainName where  AD_SETTING_KEY=''DOMAIN_NAME'' --and APPSETNG_CATEGORY=''GeneralSettings''  
       UPDATE AD_SETTINGS set AD_SETTING_VALUE=@userID where  AD_SETTING_KEY=''AD_USERNAME'' --and APPSETNG_CATEGORY=''GeneralSettings''        
       UPDATE AD_SETTINGS set AD_SETTING_VALUE=@SettingsPassword where  AD_SETTING_KEY=''AD_PASSWORD'' --and APPSETNG_CATEGORY=''GeneralSettings''              
       END        
   --3. If not exists, create new user in M_USERS_AD        
    ELSE        
       BEGIN        
        INSERT INTO  M_USERS(USR_CARD_ID,USR_ID,USR_PIN,USR_NAME,USR_PASSWORD,USR_EMAIL,USR_ROLE,REC_ACTIVE,USR_SOURCE,USR_DEPARTMENT,USR_DOMAIN) values('''',@userID,'''','''','''','''',''admin'',''1'',@AuthenticationType,@Department,@AuthenticationServer)        
        UPDATE AD_SETTINGS set AD_SETTING_VALUE=@DomainName where  AD_SETTING_KEY=''DOMAIN_CONTROLLER'' --and APPSETNG_CATEGORY=''GeneralSettings''  
        UPDATE AD_SETTINGS set AD_SETTING_VALUE=@DomainName where  AD_SETTING_KEY=''DOMAIN_NAME'' --and APPSETNG_CATEGORY=''GeneralSettings''       
        UPDATE AD_SETTINGS set AD_SETTING_VALUE=@userID where  AD_SETTING_KEY=''AD_USERNAME'' --and APPSETNG_CATEGORY=''GeneralSettings''        
        UPDATE AD_SETTINGS set AD_SETTING_VALUE=@SettingsPassword where  AD_SETTING_KEY=''AD_PASSWORD'' --and APPSETNG_CATEGORY=''GeneralSettings''              
       END         
        
 END        
        
        
END      ' 
END
GO

/****** Object:  StoredProcedure [dbo].[GetREPORTNEW]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetREPORTNEW]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetREPORTNEW]
@ReportOn varchar(50),
@UserID varchar(50),
@AuthenticationSource varchar(2),
@fromDate varchar(10),
@toDate  varchar(10)

as 
begin

declare @sql varchar(max)

--Inserting JOBLog table data to Temp1 Table

select R.USR_ID,
case 
when R.JOB_COLOR_MODE =''MONOCHROME'' then ''Black and White''
ELSE ''''
end ''BlackandWhite'',
case 
when R.JOB_COLOR_MODE =''FULL-COLOR'' then ''Colour'' end ''Colour'',
case  
when R.JOB_COLOR_MODE =''MONOCHROME'' then R.JOB_SHEET_COUNT_BW 
ELSE ''''
end BWJObs,
case  
when R.JOB_COLOR_MODE =''FULL-COLOR'' then R.JOB_SHEET_COUNT_COLOR end ColourJObs,
case 
when R.JOB_PAPER_SIZE like ''A4%'' then (R.JOB_SHEET_COUNT_BW) end A4BW,
case 
when R.JOB_PAPER_SIZE like ''A4%'' then (R.JOB_SHEET_COUNT_COLOR) end A4C,
case 
when R.JOB_PAPER_SIZE like ''A3%'' then (R.JOB_SHEET_COUNT_BW) end A3BW,
case 
when R.JOB_PAPER_SIZE like ''A3%'' then (R.JOB_SHEET_COUNT_COLOR) end A3C,
case 
when R.JOB_PAPER_SIZE not in(select  JOB_PAPER_SIZE from T_JOB_LOG where JOB_PAPER_SIZE like ''A4%'' or JOB_PAPER_SIZE like ''A3%'' ) then (R.JOB_SHEET_COUNT_BW) end OtherBW,
case 
when R.JOB_PAPER_SIZE not in(select  JOB_PAPER_SIZE from T_JOB_LOG where JOB_PAPER_SIZE like ''A4%'' or JOB_PAPER_SIZE like ''A3%'' ) then (R.JOB_SHEET_COUNT_COLOR) end OtherC,
case R.DUPLEX_MODE
when ''1SIDED'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR)
end ''ONESIDED'',
case 
when  R.DUPLEX_MODE <>''1SIDED'' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR)
end ''TWOSIDED'',
R.USR_DEPT,R.JOB_COMPUTER,R.MFP_IP,R.GRUP_ID,R.COST_CENTER_NAME,R.JOB_SHEET_COUNT,R.JOB_USRNAME,R.JOB_SHEET_COUNT_COLOR,R.JOB_SHEET_COUNT_BW,M.MFP_SERIALNUMBER,M.MFP_MODEL into #TEMP1  from T_JOB_LOG R left join M_MFPS M on R.MFP_IP=M.MFP_IP where R.JOB_STATUS=''FINISHED'' or R.JOB_STATUS =''SUSPENDED'' and R.JOB_COLOR_MODE<>'''' and  R.JOB_END_DATE BETWEEN '''' + @fromDate + '' 00:00'' and '''' +@toDate  + '' 23:59''

--update #TEMP1 table null values
update #TEMP1 set A4BW=0 where A4BW is null
update #TEMP1 set A4C=0 where A4C is null
update #TEMP1 set A3BW=0 where A3BW is null
update #TEMP1 set A3C=0 where A3C is null
update #TEMP1 set OtherBW=0 where OtherBW is null
update #TEMP1 set OtherC=0 where OtherC is null
update #TEMP1 set ONESIDED=0 where ONESIDED is null
update #TEMP1 set TWOSIDED=0 where TWOSIDED is null

--Create #JobReport Table fro select satement
--select * from #TEMP1
create table #JobReport(slno int identity, ReportOf nvarchar(100) default '''', UserID nvarchar(100) default '''',
ReportOn nvarchar(100) default '''', Total int default 0, TotalBW int default 0, TotalColor int default 0
,A3BW int default 0
,A3C int default 0
,A4BW int default 0
,A4C int default 0
,OtherBW int default 0
,OtherC int default 0
,Duplex_One_Sided float default 0
,Duplex_Two_Sided float default 0
,UserName nvarchar(100) default ''''
,Department nvarchar(100) default ''''
,ComputerName nvarchar(100) default ''''
,LoginName nvarchar(100) default ''''
,ModelName nvarchar(100) default ''''
,SerialNumber nvarchar(100) default ''''
,AuthenticationSource char(2) default ''''
,CostCenter nvarchar(100) default ''''
,GroupID nvarchar(100) default ''''

)
--Total Calculation 
declare @TotalCalculation varchar(max)
declare @TotalColumnName varchar(50)
--Total text display column name based on reporton condiiotn
if(@ReportOn=''USR_ID'')
	  begin
		set @TotalColumnName=''UserID''
      end
if(@ReportOn=''MFP_IP'')
	  begin
		set @TotalColumnName=''SerialNumber''
      end
if(@ReportOn=''GRUP_ID'')
	  begin
		set @TotalColumnName=''COSTCENTER''
      end
if(@ReportOn=''JOB_COMPUTER'')
	  begin
		set @TotalColumnName=''ComputerName''
      end

				set @TotalCalculation=''insert into #JobReport(''+@TotalColumnName+'',Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
				select ''''Total'''',(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as TOTAL,
				
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,
				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as TotalONESIDED,
				sum(TWOSIDED) / 2 TWOSIDED from #TEMP1''

--User ID 
 if(@ReportOn=''USR_ID'')
	  begin
				set @sql=''insert into #JobReport(UserName,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,UserID,ComputerName)
				select JOB_USRNAME as ''''UserName'''',(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,
				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as ''''Duplex_One_Side'''',
				sum(TWOSIDED)/2 as ''''Duplex_Two_Side'''','' + @ReportOn + '',JOB_COMPUTER as [Computer Name] from #TEMP1
				group by  '' + @ReportOn + '',JOB_USRNAME,JOB_COMPUTER''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
			select UserID,Total,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,ComputerName,UserName  from  #JobReport 
	  end

--MFP IP
if(@ReportOn=''MFP_IP'')
	  begin
				set @sql=''insert into #JobReport(SerialNumber,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,ModelName)
				select MFP_SERIALNUMBER as SerialNumber,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,
				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as ''''Duplex_One_Side'''',
         		sum(TWOSIDED)/2 as ''''Duplex_Two_Side'''',MFP_MODEL from #TEMP1
				group by  '' + @ReportOn + '',MFP_SERIALNUMBER,MFP_MODEL''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select SerialNumber,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,ModelName from  #JobReport 
	 end

--Department
if(@ReportOn=''GRUP_ID'')
	  begin
				set @sql=''insert into #JobReport(CostCenter,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
				select COST_CENTER_NAME as CostCenter,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,
				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as ''''Duplex_One_Side'''',
				sum(TWOSIDED)/2 as ''''Duplex_Two_Side'''' from #TEMP1
				group by  ''+@ReportOn+'',COST_CENTER_NAME''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select CostCenter,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided from  #JobReport 
	end

--Computer
if(@ReportOn=''JOB_COMPUTER'')
	  begin
				set @sql=''insert into #JobReport(ComputerName,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
				select JOB_COMPUTER,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,

				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as ''''Duplex_One_Side'''',
				sum(TWOSIDED)/2 as ''''Duplex_Two_Side'''' from #TEMP1
				group by  '' + @ReportOn + ''''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select ComputerName,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided from  #JobReport 
	end

end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetJobPrice]    Script Date: 03/21/2012 22:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetJobPrice]
@mfpIPAddress varchar(40), 
@costCenterID int,
@jobType varchar(20), 
@paperSize varchar(20)

as 

declare @costCenterName nvarchar(50)
declare @costProfileID int
declare @rowsCount int
declare @colorPricePerUnit float
declare @monochromePricePerUnit float

if(@jobType = 'SCANNER') set @jobType = 'SCAN'

select GRUP_ID, cast('' as nvarchar(50)) as GRUP_NAME into #mfpGroups from T_GROUP_MFPS where MFP_IP = @mfpIPAddress

--update #mfpGroups set #mfpGroups.GRUP_NAME = M_MFP_GROUPS.GRUP_NAME from M_MFP_GROUPS where M_MFP_GROUPS.GRUP_ID = #mfpGroups.GRUP_ID

select COST_PROFILE_ID, cast('' as nvarchar(50)) as COST_PROFILE_NAME, ASSIGNED_TO, MFP_GROUP_ID, cast('' as nvarchar(50)) as MFP_GRUP_NAME into #costProfiles 
from T_ASSGIN_COST_PROFILE_MFPGROUPS
where MFP_GROUP_ID = @mfpIPAddress or MFP_GROUP_ID in (select cast(GRUP_ID as varchar(40)) from #mfpGroups)

--update #costProfiles set  MFP_GRUP_NAME = GRUP_NAME from M_MFP_GROUPS where ASSIGNED_TO = 'MFP Group' and M_MFP_GROUPS.GRUP_ID = #costProfiles.MFP_GROUP_ID
--update #costProfiles set  COST_PROFILE_NAME = PRICE_PROFILE_NAME from M_PRICE_PROFILES where PRICE_PROFILE_ID = COST_PROFILE_ID

-- if user selects cost center [user group], then the preference will be Cost profile assigned to MFP Group
-- if user select cost center as My Account then the preference will be Cost profile assigned with MFP

select @rowsCount = count(COST_PROFILE_ID) from #costProfiles where ASSIGNED_TO = 'MFP Group'
--select * from #costProfiles
--print (@costCenterID)
--print(@rowsCount)
if @rowsCount >= 1 and @costCenterID > 1 -- Default Cost Center ID = 1
	begin
		select @costProfileID = COST_PROFILE_ID from #costProfiles where ASSIGNED_TO = 'MFP Group'
	end
else if @costCenterID = 0
	begin
		select @costProfileID = COST_PROFILE_ID from #costProfiles where ASSIGNED_TO = 'MFP'
	end

select @colorPricePerUnit = PRICE_PERUNIT_COLOR,  @monochromePricePerUnit = PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = @costProfileID and JOB_TYPE = @jobType and PAPER_SIZE = @paperSize
if @colorPricePerUnit is null set @colorPricePerUnit = 0
if @monochromePricePerUnit is null set @monochromePricePerUnit = 0
select @colorPricePerUnit as 'ColorPrice', @monochromePricePerUnit as 'MonochromePrice'
--select @costProfileID

--select * from #mfpGroups
--select * from #costProfiles

GO
/****** Object:  StoredProcedure [dbo].[AddLanguage]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddLanguage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[AddLanguage]
@cultureID varchar(10),
@cultureName nvarchar(50),
@uiDirection varchar(3),
@userID varchar(100),
@Status bit
 
as 
declare @defaultLanguage varchar(10)
set @defaultLanguage = ''en-US''

delete from APP_LANGUAGES where APP_CULTURE = @cultureID
delete from RESX_LABELS where RESX_CULTURE_ID = @cultureID
delete from RESX_CLIENT_MESSAGES where RESX_CULTURE_ID = @cultureID
delete from RESX_SERVER_MESSAGES where RESX_CULTURE_ID = @cultureID

-- Copy string from default Language
insert into APP_LANGUAGES(APP_CULTURE, APP_NEUTRAL_CULTURE, APP_LANGUAGE, APP_CULTURE_DIR,REC_ACTIVE)
values(@cultureID,'''',@cultureName,@uiDirection,@Status)

insert into RESX_LABELS(RESX_CULTURE_ID,RESX_MODULE, RESX_ID,RESX_VALUE,REC_CDATE,REC_MDATE,REC_AUTHOR,REC_EDITOR) 
select @cultureID,RESX_MODULE, RESX_ID, @cultureID + '' : '' + RESX_VALUE,getdate(),getdate(),@userID,@userID from RESX_LABELS where RESX_CULTURE_ID = @defaultLanguage

insert into RESX_CLIENT_MESSAGES(RESX_CULTURE_ID,RESX_MODULE, RESX_ID,RESX_VALUE,REC_CDATE,REC_MDATE,REC_AUTHOR,REC_EDITOR) 
select @cultureID,RESX_MODULE, RESX_ID, @cultureID + '' : '' + RESX_VALUE,getdate(),getdate(),@userID,@userID from RESX_CLIENT_MESSAGES where RESX_CULTURE_ID = @defaultLanguage

insert into RESX_SERVER_MESSAGES(RESX_CULTURE_ID,RESX_MODULE, RESX_ID,RESX_VALUE,REC_CDATE,REC_MDATE,REC_AUTHOR,REC_EDITOR) 
select @cultureID,RESX_MODULE, RESX_ID, @cultureID + '' : '' + RESX_VALUE,getdate(),getdate(),@userID,@userID from RESX_SERVER_MESSAGES where RESX_CULTURE_ID = @defaultLanguage
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedStrings]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedStrings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[GetLocalizedStrings] @subSystem varchar(20), @cultureID varchar(10), @labelResourceIDs text, @clientMessagesResourceIDs text, @serverMessageResourceIDs text
as

--update RESX_LABELS set RESX_IS_USED = 1 where RESX_CULTURE_ID = @cultureID and RESX_ID in (select * from ConvertStringListToTable(@labelResourceIDs, '','')) 
--update RESX_CLIENT_MESSAGES set RESX_IS_USED = 1 where RESX_CULTURE_ID = @cultureID and RESX_ID in (select * from ConvertStringListToTable(@clientMessagesResourceIDs, '','')) 
--update RESX_SERVER_MESSAGES set RESX_IS_USED = 1 where RESX_CULTURE_ID = @cultureID and RESX_ID in (select * from ConvertStringListToTable(@serverMessageResourceIDs, '','')) 

select ''L'' as ''RESX_TYPE'', RESX_ID, RESX_VALUE from RESX_LABELS where RESX_CULTURE_ID = @cultureID and RESX_ID in (select * from ConvertStringListToTable(@labelResourceIDs, '','')) 
select ''C'' as ''RESX_TYPE'', RESX_ID, RESX_VALUE from RESX_CLIENT_MESSAGES where RESX_CULTURE_ID = @cultureID and RESX_ID in (select * from ConvertStringListToTable(@clientMessagesResourceIDs, '','')) 
select ''S'' as ''RESX_TYPE'', RESX_ID, RESX_VALUE from RESX_SERVER_MESSAGES where RESX_CULTURE_ID = @cultureID and RESX_ID in (select * from ConvertStringListToTable(@serverMessageResourceIDs, '','')) 

' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedLabel]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedLabel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[GetLocalizedLabel] @subSystem varchar(20), @cultureID varchar(10), @resourceID varchar(100)
as

--update RESX_LABELS set RESX_IS_USED = 1 where RESX_CULTURE_ID = @cultureID and RESX_ID =  @resourceID

select RESX_ID, RESX_VALUE from RESX_LABELS where RESX_CULTURE_ID = @cultureID and RESX_ID =  @resourceID


' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedServerMessage]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedServerMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[GetLocalizedServerMessage] @subSystem varchar(20), @cultureID varchar(10), @serverMessageResourceID varchar(100)
as

--update RESX_SERVER_MESSAGES set RESX_IS_USED = 1 where RESX_CULTURE_ID = @cultureID and RESX_ID =  @serverMessageResourceID

select RESX_ID, RESX_VALUE from RESX_SERVER_MESSAGES where RESX_CULTURE_ID = @cultureID and RESX_ID =  @serverMessageResourceID

' 
END
GO

/****** Object:  StoredProcedure [dbo].[GetTopReports]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTopReports]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[GetTopReports] 
@ReportOn varchar(50), 
@AuthenticationSource varchar(2),  
@fromDate varchar(10), 
@toDate  varchar(10)

as
begin
create table #tempDayWiseReport(TotalColor numeric(18,0) default 0, TotalBW numeric(18,0) default 0, Total numeric(18,0) default 0, WeekDays varchar(10))
create table #tempPagesWiseReport(Pages varchar(200), Jobs numeric(18,0) default 0, TotalPages numeric(18,0) default 0)
declare @dateCriteria varchar(1000)
declare @SQL varchar(max)
set @dateCriteria = '' ( JOB_END_DATE BETWEEN '''''' + @fromDate + '' 00:00'''' and '''''' +@toDate  + '' 23:59'''')''

--Top 10 Printers by pages
set @SQL=''SELECT 1 as TEMP_COUNT''
exec(@SQL)

--Top 10 Users by pages
set @SQL=''SELECT 2 as TEMP_COUNT''
exec(@SQL)

--Top  Printers  by B&W
set @SQL=''SELECT 3 as TEMP_COUNT''
exec(@SQL)

--Top  Printers  by Color
set @SQL=''SELECT 4 as TEMP_COUNT''
exec(@SQL)

--Top  Users  by B&W 
set @SQL=''SELECT 5 as TEMP_COUNT''
exec(@SQL)

--Top  Users  by Color
set @SQL=''SELECT 6 as TEMP_COUNT''
exec(@SQL)


--Weekday Report

--select sum(JOB_SHEET_COUNT_COLOR)as  TotalColor,sum(JOB_SHEET_COUNT_BW)as  TotalBW ,sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as  Total,''Monday'' as WeekDays  from T_JOB_LOG  where convert(varchar, job_end_date, 101) in( SELECT convert(varchar, [Dt], 101) FROM dbo.GetDatesforAday(''7/1/2008'',''11/04/2011'',''Monday''))
DECLARE @Days varchar(30)
 DECLARE @getDays CURSOR
 SET @getDays = CURSOR FOR
  select TokenVal from ConvertStringListToTable(''Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday'', '','') 

 OPEN @getDays
 FETCH NEXT
 FROM @getDays INTO @Days
 WHILE @@FETCH_STATUS = 0
 BEGIN

 PRINT @Days

insert into #tempDayWiseReport(TotalColor,TotalBW,Total,WeekDays) select sum(JOB_SHEET_COUNT_COLOR)as  TotalColor,sum(JOB_SHEET_COUNT_BW)as  TotalBW ,sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as  Total,@Days as WeekDays  from T_JOB_LOG  where JOB_STATUS = ''FINISHED''  and  convert(varchar, job_end_date, 101) in( SELECT convert(varchar, [Dt], 101) FROM dbo.GetDatesforAday(@fromDate,@ToDate,@Days))
--select sum(JOB_SHEET_COUNT_COLOR)as  TotalColor,sum(JOB_SHEET_COUNT_BW)as  TotalBW ,sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as  Total,@Days as WeekDays  from T_JOB_LOG  where convert(varchar, job_end_date, 101) in( SELECT convert(varchar, [Dt], 101) FROM dbo.GetDatesforAday(''7/1/2008'',''11/04/2011'',@Days))
 FETCH NEXT
 FROM @getDays INTO @Days
 END
 CLOSE @getDays
 DEALLOCATE @getDays
update #tempDayWiseReport set TotalColor=''0'' where TotalColor is null
update #tempDayWiseReport set TotalBW=''0'' where TotalBW is null
update #tempDayWiseReport set Total=''0'' where Total is null
select * from #tempDayWiseReport
truncate table #tempDayWiseReport
end
-------------------------------RangeWise page count------Total Breakdown------------------------------------
DECLARE @FirstPage varchar(50)
DECLARE @LastPage varchar(50)
--declare @Sql varchar(1000)
 DECLARE @Pages varchar(30)
 DECLARE @getPages CURSOR
 SET @getPages = CURSOR FOR
  select TokenVal from ConvertStringListToTable(''1-3,4-25,26-50,50+'', '','') 

 OPEN @getPages
 FETCH NEXT
 FROM @getPages INTO @Pages
 WHILE @@FETCH_STATUS = 0
 BEGIN

 --PRINT @Pages
 --select TokenVal from ConvertStringListToTable(@Pages, ''-'') 
 select top 1 @FirstPage=TokenVal from ConvertStringListToTable(@Pages, ''-'')
print @FirstPage
 select top 1 @LastPage=TokenVal from ConvertStringListToTable(@Pages, ''-'') where TokenVal not in(select top 1 TokenVal from ConvertStringListToTable(@Pages, ''-''))
print @LastPage
IF (@FirstPage<>''50+'')
   BEGIN
  set @Sql=''insert into #tempPagesWiseReport(Pages,Jobs,TotalPages)select ''''''+@Pages+'''''' pages,count(*) as Jobs,Sum(job_sheet_count_color + job_sheet_count_BW) TotalPages from T_JOB_LOG where JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED''''  and job_sheet_count_color + job_sheet_count_BW  between ''+@FirstPage+'' and ''+@LastPage+'' and convert(varchar, job_end_date, 101) BETWEEN convert(varchar, ''''''+@fromDate+'''''', 101)  and convert(varchar, ''''''+@toDate+'''''', 101)''
print @Sql
exec(@Sql)
   END
ELSE
   BEGIN
  set @Sql=''insert into #tempPagesWiseReport(Pages,Jobs,TotalPages)select ''''''+@Pages+'''''' pages,count(*) as Jobs,Sum(job_sheet_count_color + job_sheet_count_BW) TotalPages from T_JOB_LOG where JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED''''  and job_sheet_count_color + job_sheet_count_BW  > ''+@LastPage+'' and convert(varchar,job_end_date, 101) BETWEEN convert(varchar, ''''''+@fromDate+'''''', 101)  and convert(varchar, ''''''+@toDate+'''''', 101)''
print @Sql 
exec(@Sql) 
END
 
 FETCH NEXT
 FROM @getPages INTO @Pages
 END
 CLOSE @getPages
 DEALLOCATE @getPages
update #tempPagesWiseReport set Jobs=''0'' where Jobs is null
update #tempPagesWiseReport set TotalPages=''0'' where TotalPages is null
select * from #tempPagesWiseReport
truncate table #tempPagesWiseReport

-------------------------------------BuildTotalVolumeBreakdown------------------------------------

set @SQL=''select sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as Totalpages ,JOB_COLOR_MODE  FROM T_JOB_LOG where JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED''''  and '' + @dateCriteria + ''group by JOB_COLOR_MODE  order by Totalpages desc''
exec(@SQL)
-------------------------------------BuildTotalVolumeBreakdownPageSize------------------------------------
set @SQL=''SELECT sum(JOB_SHEET_COUNT_COLOR) as TotalPages,JOB_PAPER_SIZE  FROM T_JOB_LOG where JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED''''  and '' + @dateCriteria + ''group by JOB_PAPER_SIZE  order by TotalPages desc''
exec(@SQL)
-------------------------------------BuildTotalVolumeBreakdownPageSizeBW---------------------------------
set @SQL=''SELECT sum(JOB_SHEET_COUNT_BW) as TotalPages,JOB_PAPER_SIZE  FROM T_JOB_LOG where JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED''''  and '' + @dateCriteria + ''group by JOB_PAPER_SIZE  order by TotalPages desc''
exec(@SQL)
-------------------------------------BuildTotalVolumeBreakdownPrinters------------------------------------
set @SQL=''SELECT Top 5 sum(JOB_SHEET_COUNT_COLOR) as TotalColor, sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as Totalpages ,MFP_IP  FROM T_JOB_LOG
where JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED''''  and '' + @dateCriteria + ''group by MFP_IP  order by Totalpages desc''
exec(@SQL)
-------------------------------------BuildTotalVolumeBreakdownUsers------------------------------------
set @SQL=''SELECT Top 5 sum(JOB_SHEET_COUNT_COLOR) as TotalColor, sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as Totalpages ,USR_ID  FROM T_JOB_LOG
where JOB_STATUS = ''''FINISHED'''' or JOB_STATUS = ''''SUSPENDED''''  and '' + @dateCriteria + ''group by USR_ID  order by Totalpages desc''
exec(@SQL)
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetJobReport]    Script Date: 03/02/2012 19:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
  
CREATE procedure [dbo].[GetJobReport] @ReportOn varchar(50), @UserID varchar(50), @AuthenticationSource varchar(2),  @fromDate varchar(10), @toDate  varchar(10)  
  
as   
  
create table #JobReport(slno int identity, ReportOf nvarchar(100) default '''', UserID nvarchar(100) default '''',  ReportOn nvarchar(100) default '''', Total int default 0, TotalBW int default 0, TotalColor int default 0  
,Ledger int default 0  
,A3 int default 0  
,Legal int default 0  
,Letter int default 0  
,A4 int default 0  
,Other int default 0  
,Duplex_One_Sided float default 0  
,Duplex_Two_Sided float default 0  
,UserName nvarchar(100) default ''''  
,Department nvarchar(100) default ''''  
,ComputerName nvarchar(100) default ''''  
,LoginName nvarchar(100) default ''''  
,ModelName nvarchar(100) default ''''  
,SerialNumber nvarchar(100) default ''''  
,AuthenticationSource char(2) default ''''  
  
)  
declare @sql nvarchar(2000)  
declare @dateCriteria varchar(1000)  
declare @filter varchar(1000)  
declare @filterTempColumns varchar(1000)  
declare @filterWhereCondition varchar(1000)  
declare @filterWhereConditionTemp2 varchar(1000)  
declare @tempColumns varchar(1000)  
  
if @ReportOn = ''USR_ID''  
 begin  
  set @ReportOn = @ReportOn + '',USR_SOURCE,USR_DEPT,JOB_COMPUTER''  
  set @filterTempColumns = ''ReportOf,AuthenticationSource,UserDept,UserComputer,SheetCount''  
  set @filterWhereCondition = ''#JobReport.ReportOf = #temp.ReportOf and #JobReport.AuthenticationSource = #temp.AuthenticationSource and #JobReport.Department = #temp.UserDept and #JobReport.ComputerName = #temp.UserComputer''  
  set @filterWhereConditionTemp2 = ''#JobReport.ReportOf = #temp2.ReportOf and #JobReport.AuthenticationSource = #temp2.AuthenticationSource and #JobReport.Department = #temp2.UserDept and #JobReport.ComputerName = #temp2.UserComputer''  
  
  set @tempColumns = ''ReportOf,AuthenticationSource,Department,ComputerName''  
 end  
else  
 begin  
  set @ReportOn = @ReportOn + '',USR_SOURCE''  
  set @filterTempColumns = ''ReportOf,AuthenticationSource,SheetCount''  
  set @filterWhereCondition = ''#JobReport.ReportOf = #temp.ReportOf and #JobReport.AuthenticationSource = #temp.AuthenticationSource''  
  set @filterWhereConditionTemp2 = ''#JobReport.ReportOf = #temp2.ReportOf and #JobReport.AuthenticationSource = #temp2.AuthenticationSource''  
  set @tempColumns = ''ReportOf,AuthenticationSource''  
 end  
  
set @dateCriteria = '' ( JOB_END_DATE BETWEEN '''''' + @fromDate + '' 00:00'''' and '''''' +@toDate  + '' 23:59'''')''  
  
if @UserID != ''''  
 begin  
  set @dateCriteria = @dateCriteria + '' and USR_ID = N'''''' + @UserID + ''''''''  
 end  
if @AuthenticationSource != ''''  
 begin  
  set @dateCriteria = @dateCriteria + '' and USR_SOURCE = N'''''' + @AuthenticationSource + ''''''''  
 end  
  
  
set @sql = ''insert into #JobReport('' + @tempColumns+ '') select  '' + @ReportOn + '' from T_JOB_LOG where  JOB_STATUS=''''FINISHED'''' and'' + @dateCriteria   
--print(''one'');  
print @sql  
exec(@sql)  
  
create table #temp(ReportOf varchar(50), UserDept varchar(50), UserComputer varchar(50), SheetCount float default 0, AuthenticationSource char(2) default '''')  
create table #temp2(ReportOf varchar(50), UserDept varchar(50), UserComputer varchar(50), SheetCount float default 0 , AuthenticationSource char(2) default '''')  
   
-- Print Color   
set @sql = ''insert into #temp('' + @filterTempColumns + '') select '' + @ReportOn + '', sum(JOB_SHEET_COUNT_COLOR)  as JobCount from T_JOB_LOG where '' + @dateCriteria + '' and JOB_STATUS=''''FINISHED'''' group by '' + @ReportOn   
--print(''Print Color insert'');  
print @sql  
exec(@sql)  
set @sql = ''update #JobReport set TotalColor = SheetCount from #temp where '' + @filterWhereCondition   
--print(''Print Color update'');  
print @sql  
exec(@sql)  
  
-- Print Monochrome   
truncate table #temp  
set @sql = ''insert into #temp('' + @filterTempColumns + '') select '' + @ReportOn + '', sum(JOB_SHEET_COUNT_BW) as JobCount from T_JOB_LOG where '' + @dateCriteria + '' and JOB_STATUS=''''FINISHED'''' group by '' + @ReportOn   
--print(''Print Monochrome'');  
print @sql  
exec(@sql)  
set @sql = ''update #JobReport set TotalBW = SheetCount from #temp where '' + @filterWhereCondition   
exec(@sql)  
  
-- Print Duplex 1 Sided   
truncate table #temp  
set @sql = ''insert into #temp('' + @filterTempColumns + '') select '' + @ReportOn + '', (sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as JobCount from T_JOB_LOG where '' + @dateCriteria + '' and DUPLEX_MODE like ''''1%'''' and JOB_STATUS=''''FINISHED'''' and JOB_COLOR_MODE<>'''''''' group by '' + @ReportOn + '', JOB_COLOR_MODE''  
--print(''Print Duplex 1 Sided insert'');  
print @sql  
exec(@sql)  
  
truncate table #Temp2  
insert into #temp2(ReportOf, UserDept, UserComputer , SheetCount, AuthenticationSource) select ReportOf, UserDept, UserComputer ,sum(SheetCount) as SheetCount, AuthenticationSource from #temp group by ReportOf, UserDept, UserComputer, AuthenticationSource
  
  
set @sql = ''update #JobReport set Duplex_One_Sided = SheetCount from #temp2 where '' + @filterWhereConditionTemp2   
--print(''Print Duplex 1 Sided update'');  
print @sql  
exec(@sql)  
  
-- Print Duplex 2 Sided   
truncate table #temp  
set @sql = ''insert into #temp('' + @filterTempColumns + '') select '' + @ReportOn + '', (sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as JobCount from T_JOB_LOG where '' + @dateCriteria + '' and DUPLEX_MODE like ''''2%'''' and JOB_STATUS=''''FINISHED'''' and JOB_COLOR_MODE<>'''''''' group by '' + @ReportOn + '', JOB_COLOR_MODE''  
--print(''Print Duplex 2 Sided insert'');  
print @sql  
exec(@sql)  
  
truncate table #Temp2  
insert into #temp2(ReportOf, UserDept, UserComputer , SheetCount, AuthenticationSource) select ReportOf, UserDept, UserComputer ,sum(SheetCount) as SheetCount, AuthenticationSource from #temp group by ReportOf, UserDept, UserComputer, AuthenticationSource
  
  
set @sql = ''update #JobReport set Duplex_Two_Sided = SheetCount from #temp2 where '' + @filterWhereConditionTemp2   
--print(''Print Duplex 2 Sided update'');  
print @sql  
exec(@sql)  
  
declare @tokenValue varchar(30)  
  
DECLARE PaperSizeCursor CURSOR  
 LOCAL  
 FAST_FORWARD  
 READ_ONLY for select TokenVal from ConvertStringListToTable('',Ledger,A3,Legal,Letter,A4,Other'', '','')  
 OPEN PaperSizeCursor  
 FETCH NEXT FROM PaperSizeCursor into @tokenValue  
  
 WHILE @@FETCH_STATUS = 0  
  Begin  
   FETCH NEXT FROM PaperSizeCursor  
   INTO @tokenValue  
   truncate table #temp  
IF @tokenValue=''Other''  
BEGIN  
   set @sql = ''insert into #temp('' + @filterTempColumns + '') select '' + @ReportOn + '', (sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as JobCount from T_JOB_LOG where '' + @dateCriteria + '' and JOB_STATUS=''''FINISHED'''' and JOB_PAPER_SIZE not in(''''Le
dger'''',''''A3'''',''''Legal'''',''''Letter'''',''''A4'''') group by '' + @ReportOn + ''''  
   exec(@sql)  
   set @sql = ''update #JobReport set Other = SheetCount from #temp where '' + @filterWhereCondition   
   exec(@sql)  
  
END  
  
ELSE  
            set @sql = ''insert into #temp('' + @filterTempColumns + '') select '' + @ReportOn + '', (sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as JobCount from T_JOB_LOG where '' + @dateCriteria + '' and JOB_STATUS=''''FINISHED''''  and JOB_PAPER_SIZE =
 '''''' + @tokenValue + '''''' group by '' + @ReportOn + '', JOB_PAPER_SIZE''  
   exec(@sql)  
   set @sql = ''update #JobReport set '' + @tokenValue + '' = SheetCount from #temp where '' + @filterWhereCondition   
   exec(@sql)  
  End  
 CLOSE PaperSizeCursor  
 DEALLOCATE PaperSizeCursor  
  
update #JobReport set Total = TotalBW + TotalColor  
  
  
if @ReportOn = ''MFP_IP,USR_SOURCE''  
Begin  
 update #JobReport set SerialNumber = MFP_SERIALNUMBER, ModelName= MFP_MODEL from M_MFPS where #JobReport.ReportOf =  M_MFPS.MFP_IP  
 insert into #JobReport (SerialNumber,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided) select ''Total'', sum(Total), sum(TotalBW), sum(TotalColor), sum(Ledger), sum(A3), sum(Legal), sum(Letter), sum(A4), sum(Other)
, sum(Duplex_One_Sided), sum(Duplex_Two_Sided) from #JobReport  
   
    update #JobReport set Duplex_Two_Sided = Duplex_Two_Sided / 2  
  
 select SerialNumber,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided,ModelName from  #JobReport   
End  
  
if @ReportOn = ''USR_ID,USR_SOURCE,USR_DEPT,JOB_COMPUTER''  
Begin  
 update #JobReport set UserName = USR_NAME from M_USERS where #JobReport.ReportOf =  M_USERS.USR_ID and #JobReport.AuthenticationSource =  M_USERS.USR_SOURCE  
 update #JobReport set UserID = ReportOf   
 insert into #JobReport (UserName,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided) select ''Total'', sum(Total), sum(TotalBW), sum(TotalColor), sum(Ledger), sum(A3), sum(Legal), sum(Letter), sum(A4), sum(Other), sum(Duplex_One_Sided), sum(Duplex_Two_Sided) from #JobReport  
   
   update #JobReport set Duplex_Two_Sided = Duplex_Two_Sided / 2  
  
 select UserName,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided,Department,ComputerName, UserID from  #JobReport   
End  
  
if @ReportOn = ''USR_DEPT,USR_SOURCE''  
Begin  
 update #JobReport set Department = ReportOf   
 insert into #JobReport (Department,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided) select ''Total'', sum(Total), sum(TotalBW), sum(TotalColor), sum(Ledger), sum(A3), sum(Legal), sum(Letter), sum(A4), sum(Other), 
sum(Duplex_One_Sided), sum(Duplex_Two_Sided) from #JobReport  
   
  update #JobReport set Duplex_Two_Sided = Duplex_Two_Sided / 2  
  
 select Department,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided from  #JobReport   
End  
  
if @ReportOn = ''JOB_COMPUTER,USR_SOURCE''  
Begin  
 update #JobReport set ComputerName = ReportOf   
 insert into #JobReport (ComputerName,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided) select ''Total'', sum(Total), sum(TotalBW), sum(TotalColor), sum(Ledger), sum(A3), sum(Legal), sum(Letter), sum(A4), sum(Other)
, sum(Duplex_One_Sided), sum(Duplex_Two_Sided) from #JobReport  
   
 update #JobReport set Duplex_Two_Sided = Duplex_Two_Sided / 2  
    
 select ComputerName,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided from  #JobReport   
End  
  
  
  ' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPagedTableData]    Script Date: 03/12/2012 16:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPagedTableData]
  @datasrc nvarchar(200)
 ,@orderBy nvarchar(200)
 ,@fieldlist nvarchar(200) = '*'
 ,@filter nvarchar(200) = ''
 ,@pageNum int = 1
 ,@pageSize int = NULL
AS
  SET NOCOUNT ON
  DECLARE
     @STMT nvarchar(max)         -- SQL to execute
    ,@recct int                  -- total # of records (for GridView paging interface)

  IF LTRIM(RTRIM(@filter)) = '' SET @filter = '1 = 1'
  IF @pageSize IS NULL BEGIN
    SET @STMT =  'SELECT   ' + @fieldlist + 
                 'FROM     ' + @datasrc +
                 'WHERE    ' + @filter + 
                 'ORDER BY ' + @orderBy
    EXEC (@STMT)                 -- return requested records 
  END ELSE BEGIN
    SET @STMT =  'SELECT   @recct = COUNT(*)
                  FROM     ' + @datasrc + '
                  WHERE    ' + @filter
    EXEC sp_executeSQL @STMT, @params = N'@recct INT OUTPUT', @recct = @recct OUTPUT
    --SELECT @recct AS recct       -- return the total # of records

    DECLARE
      @lbound int,
      @ubound int

    SET @pageNum = ABS(@pageNum)
    SET @pageSize = ABS(@pageSize)
    IF @pageNum < 1 SET @pageNum = 1
    IF @pageSize < 1 SET @pageSize = 1
    SET @lbound = ((@pageNum - 1) * @pageSize)
    SET @ubound = @lbound + @pageSize + 1
    IF @lbound >= @recct BEGIN
      SET @ubound = @recct + 1
      SET @lbound = @ubound - (@pageSize + 1) -- return the last page of records if                                               -- no records would be on the
                                              -- specified page
    END
    SET @STMT =  'SELECT  ' + @fieldlist + '
                  FROM    (
                            SELECT  ROW_NUMBER() OVER(ORDER BY ' + @orderBy + ') AS row, *
                            FROM    ' + @datasrc + '
                            WHERE   ' + @filter + '
                          ) AS tbl
                  WHERE
                          row > ' + CONVERT(varchar(9), @lbound) + ' AND
                          row < ' + CONVERT(varchar(9), @ubound)
    EXEC (@STMT)                 -- return requested records 
  END
  GO
/****** Object:  Default [DF_APP_LANGUAGES_APP_CULTURE_DIR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_LANGUAGES_APP_CULTURE_DIR]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_LANGUAGES_APP_CULTURE_DIR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_LANGUAGES] ADD  CONSTRAINT [DF_APP_LANGUAGES_APP_CULTURE_DIR]  DEFAULT ('LTR') FOR [APP_CULTURE_DIR]
END


End
GO
/****** Object:  Default [DF_APP_LANGUAGES_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_LANGUAGES_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_LANGUAGES_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_LANGUAGES] ADD  CONSTRAINT [DF_APP_LANGUAGES_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_APP_SETTINGS_APPSETNG_KEY_ORDER]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_SETTINGS_APPSETNG_KEY_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_SETTINGS_APPSETNG_KEY_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_SETTINGS] ADD  CONSTRAINT [DF_APP_SETTINGS_APPSETNG_KEY_ORDER]  DEFAULT ((0)) FOR [APPSETNG_KEY_ORDER]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]  DEFAULT ((0)) FOR [CARD_DATA_ENABLED]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_DATA_ON]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_DATA_ON]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_DATA_ON]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_CARD_DATA_ON]  DEFAULT ('P') FOR [CARD_DATA_ON]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_POSITION_START]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_POSITION_START]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_POSITION_START]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_CARD_POSITION_START]  DEFAULT ((0)) FOR [CARD_POSITION_START]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]  DEFAULT ((0)) FOR [CARD_POSITION_LENGTH]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]') AND parent_object_id = OBJECT_ID(N'[dbo].[INVALID_CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[INVALID_CARD_CONFIGURATION] ADD  CONSTRAINT [DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]  DEFAULT ('P') FOR [CARD_DATA_ON]
END


End
GO
/****** Object:  Default [DF_M_COST_CENTERS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COST_CENTERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COST_CENTERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COST_CENTERS] ADD  CONSTRAINT [DF_M_COST_CENTERS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COST_CENTERS] ADD  CONSTRAINT [DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOW_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_M_COUNTRIES_REC_ACVITE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COUNTRIES_REC_ACVITE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COUNTRIES_REC_ACVITE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COUNTRIES] ADD  CONSTRAINT [DF_M_COUNTRIES_REC_ACVITE]  DEFAULT ((1)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_COUNTRIES_REC_USER]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COUNTRIES_REC_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COUNTRIES_REC_USER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COUNTRIES] ADD  CONSTRAINT [DF_M_COUNTRIES_REC_USER]  DEFAULT ((0)) FOR [REC_USER]
END


End
GO
/****** Object:  Default [DF_M_DEPARTMENTS_DEPT_SOURCE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_DEPARTMENTS_DEPT_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_DEPARTMENTS_DEPT_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_DEPARTMENTS] ADD  CONSTRAINT [DF_M_DEPARTMENTS_DEPT_SOURCE]  DEFAULT ('AD') FOR [DEPT_SOURCE]
END


End
GO
/****** Object:  Default [DF_M_DEPARTMENTS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_DEPARTMENTS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_DEPARTMENTS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_DEPARTMENTS] ADD  CONSTRAINT [DF_M_DEPARTMENTS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_JOB_TYPES_ITEM_ORDER]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TYPES_ITEM_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_JOB_CATEGORIES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TYPES_ITEM_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_JOB_CATEGORIES] ADD  CONSTRAINT [DF_T_JOB_TYPES_ITEM_ORDER]  DEFAULT ((0)) FOR [ITEM_ORDER]
END


End
GO
/****** Object:  Default [DF_M_JOB_TYPES_ITEM_ORDER]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_JOB_TYPES_ITEM_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_JOB_TYPES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_JOB_TYPES_ITEM_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_JOB_TYPES] ADD  CONSTRAINT [DF_M_JOB_TYPES_ITEM_ORDER]  DEFAULT ((0)) FOR [ITEM_ORDER]
END


End
GO
/****** Object:  Default [DF_M_MFP_GROUPS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFP_GROUPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFP_GROUPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFP_GROUPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFP_GROUPS] ADD  CONSTRAINT [DF_M_MFP_GROUPS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_ALLOW_NETWORK_PASSWORD]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_ALLOW_NETWORK_PASSWORD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]  DEFAULT ((0)) FOR [ALLOW_NETWORK_PASSWORD]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_SSO]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_SSO]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_SSO]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_SSO]  DEFAULT ((0)) FOR [MFP_SSO]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]  DEFAULT ((0)) FOR [MFP_LOCK_DOMAIN_FIELD]
END


End
GO
/****** Object:  Default [DF_M_MFPS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_REC_ACTIVE]  DEFAULT ((1)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_REC_DATE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_REC_DATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_REC_DATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_REC_DATE]  DEFAULT (getdate()) FOR [REC_DATE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_PRINT_API]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_PRINT_API]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_PRINT_API]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_PRINT_API]  DEFAULT (N'FTP') FOR [MFP_PRINT_API]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_EAM_ENABLED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_EAM_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_EAM_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_EAM_ENABLED]  DEFAULT ((0)) FOR [MFP_EAM_ENABLED]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_ACM_ENABLED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_ACM_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_ACM_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_ACM_ENABLED]  DEFAULT ((0)) FOR [MFP_ACM_ENABLED]
END


End
GO
/****** Object:  Default [DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_OSA_JOB_PROPERTIES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_OSA_JOB_PROPERTIES] ADD  CONSTRAINT [DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]  DEFAULT ((0)) FOR [JOB_PROPERTY_SETTABLE]
END


End
GO
/****** Object:  Default [DF_M_PAPER_SIZES_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_PAPER_SIZES_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_PAPER_SIZES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_PAPER_SIZES_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_PAPER_SIZES] ADD  CONSTRAINT [DF_M_PAPER_SIZES_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_SOURCE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_USR_SOURCE]  DEFAULT ('DB') FOR [USR_SOURCE]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_DEPARTMENT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_DEPARTMENT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_DEPARTMENT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_USR_DEPARTMENT]  DEFAULT ((0)) FOR [USR_DEPARTMENT]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_COSTCENTER]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_COSTCENTER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_COSTCENTER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_USR_COSTCENTER]  DEFAULT ((-1)) FOR [USR_COSTCENTER]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_ROLE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_ROLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_ROLE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_USR_ROLE]  DEFAULT (user_name()) FOR [USR_ROLE]
END


End
GO
/****** Object:  Default [DF_M_USERS_RETRY_COUNT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_RETRY_COUNT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_RETRY_COUNT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_RETRY_COUNT]  DEFAULT ((5)) FOR [RETRY_COUNT]
END


End
GO
/****** Object:  Default [DF_M_USERS_REC_CDATE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_REC_CDATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_REC_CDATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_REC_CDATE]  DEFAULT (getdate()) FOR [REC_CDATE]
END


End
GO
/****** Object:  Default [DF_M_USERS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_REC_ACTIVE]  DEFAULT ((1)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_USERS_ALLOW_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_ALLOW_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_ALLOW_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_ALLOW_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOW_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_CLIENT_MESSAGES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_CLIENT_MESSAGES] ADD  CONSTRAINT [DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]  DEFAULT ((0)) FOR [RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_RESX_LABELS_RESX_IS_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_LABELS_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_LABELS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_LABELS_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_LABELS] ADD  CONSTRAINT [DF_RESX_LABELS_RESX_IS_USED]  DEFAULT ((0)) FOR [RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_RESX_SERVER_MESSAGES_RESX_IS_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_SERVER_MESSAGES_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_SERVER_MESSAGES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_SERVER_MESSAGES_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_SERVER_MESSAGES] ADD  CONSTRAINT [DF_RESX_SERVER_MESSAGES_RESX_IS_USED]  DEFAULT ((0)) FOR [RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_T_AD_USERS_DEPARTMENT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AD_USERS_DEPARTMENT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AD_USERS_DEPARTMENT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AD_USERS] ADD  CONSTRAINT [DF_T_AD_USERS_DEPARTMENT]  DEFAULT ((0)) FOR [DEPARTMENT]
END


End
GO
/****** Object:  Default [DF_T_AD_USERS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AD_USERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AD_USERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AD_USERS] ADD  CONSTRAINT [DF_T_AD_USERS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_AUTO_REFILL_AUTO_REFILL_FOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTO_REFILL_AUTO_REFILL_FOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTO_REFILL_AUTO_REFILL_FOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTO_REFILL] ADD  CONSTRAINT [DF_T_AUTO_REFILL_AUTO_REFILL_FOR]  DEFAULT ('U') FOR [AUTO_REFILL_FOR]
END


End
GO
/****** Object:  Default [DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTO_REFILL] ADD  CONSTRAINT [DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]  DEFAULT ((1)) FOR [IS_REFILL_REQUIRED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]  DEFAULT ((0)) FOR [IS_JOB_ALLOWED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_JOB_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_PRINT_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_PRINT_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_PRINT_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_PRINT_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_DOC_FILING_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_COPY_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_COPY_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_COPY_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_COPY_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_OTHERS_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_OTHERS_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_OTHERS_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TO_HD_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]  DEFAULT ((0)) FOR [DEVICE_SEND_INTERNET_FAX_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_INTERNET_FAX_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_INTERNET_FAX_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_GROUP_MFPS_REC_ACTIVE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_GROUP_MFPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_GROUP_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_GROUP_MFPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_GROUP_MFPS] ADD  CONSTRAINT [DF_T_GROUP_MFPS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_SIZE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] ADD  CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_SIZE]  DEFAULT ((0)) FOR [JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] ADD  CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]  DEFAULT ((0)) FOR [JOB_DISPATCH_WITH_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] ADD  CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]  DEFAULT ((0)) FOR [JOB_DISPACTCH_DURATION]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_MFP_ID]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_MFP_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_MFP_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_MFP_ID]  DEFAULT ((0)) FOR [MFP_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_GRUP_ID]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_GRUP_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_GRUP_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_GRUP_ID]  DEFAULT ((0)) FOR [GRUP_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_USR_SOURCE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_USR_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_USR_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_USR_SOURCE]  DEFAULT ('DB') FOR [USR_SOURCE]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]  DEFAULT ((0)) FOR [JOB_SHEET_COUNT_COLOR]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]  DEFAULT ((0)) FOR [JOB_SHEET_COUNT_BW]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_PRICE_COLOR]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_PRICE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_PRICE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_JOB_PRICE_COLOR]  DEFAULT ((0)) FOR [JOB_PRICE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_PRICE_BW]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_PRICE_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_PRICE_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_JOB_PRICE_BW]  DEFAULT ((0)) FOR [JOB_PRICE_BW]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_REC_DATE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_REC_DATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_REC_DATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_REC_DATE]  DEFAULT (getdate()) FOR [REC_DATE]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]  DEFAULT ((0)) FOR [JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]  DEFAULT ((0)) FOR [JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]  DEFAULT ((0)) FOR [JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_JOB_SIZE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] ADD  CONSTRAINT [DF_T_JOB_TRANSMITTER_JOB_SIZE]  DEFAULT ((0)) FOR [JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] ADD  CONSTRAINT [DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]  DEFAULT ((0)) FOR [JOB_TRMSN_DURATION]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_LISTNER_PORT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_LISTNER_PORT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_LISTNER_PORT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] ADD  CONSTRAINT [DF_T_JOB_TRANSMITTER_LISTNER_PORT]  DEFAULT ((0)) FOR [LISTNER_PORT]
END


End
GO
/****** Object:  Default [DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_USAGE_PAPERSIZE]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_USAGE_PAPERSIZE] ADD  CONSTRAINT [DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]  DEFAULT ((0)) FOR [JOB_SHEET_COUNT]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_SIZE]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_SIZE]  DEFAULT ((0)) FOR [JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]  DEFAULT ((0)) FOR [JOB_CHANGED_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]  DEFAULT ((0)) FOR [JOB_RELEASE_WITH_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]  DEFAULT ((0)) FOR [JOB_PRINT_RELEASED]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]  DEFAULT ((0)) FOR [DELETE_AFTER_PRINT]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]  DEFAULT ((0)) FOR [JOB_RELEASE_NOTIFY]
END


End
GO
/****** Object:  Default [DF_T_SERVICE_MONITOR_SRVC_TIME]    Script Date: 03/02/2012 19:49:49 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_SERVICE_MONITOR_SRVC_TIME]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_SERVICE_MONITOR]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_SERVICE_MONITOR_SRVC_TIME]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_SERVICE_MONITOR] ADD  CONSTRAINT [DF_T_SERVICE_MONITOR_SRVC_TIME]  DEFAULT (getdate()) FOR [SRVC_TIME]
END


End
GO





/* Inset Default Data */

SET IDENTITY_INSERT [dbo].[INVALID_CARD_CONFIGURATION] ON
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (22, N'-1', N'DDR', N'-', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009FD101269DF5 AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (21, N'-1', N'DPR', N'-', 0, N' ', 0, 0, N'', N'', N'', 1, CAST(0x00009FD101269DF5 AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (3, N'BR', N'FSC', N'-', 0, N' ', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (4, N'BR', N'FSC', N'1', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (5, N'BR', N'FSC', N'2', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (6, N'BR', N'FSC', N'3', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (7, N'BR', N'FSC', N'4', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (8, N'BR', N'FSC', N'5', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (9, N'MS', N'FSC', N'-', 0, N' ', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (10, N'MS', N'FSC', N'1', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (11, N'MS', N'FSC', N'2', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (12, N'MS', N'FSC', N'3', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (13, N'MS', N'FSC', N'4', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (14, N'MS', N'FSC', N'5', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009F0C010A3BFA AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (28, N'PC', N'FSC', N'-', 1, N' ', 0, 0, N'', N'', N'', 1, CAST(0x00009FD101269DF5 AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (27, N'PC', N'FSC', N'1', 1, N'P', 2, 2, N'', N'', N'', 1, CAST(0x00009FD101269DF5 AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (26, N'PC', N'FSC', N'2', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009FD101269DF5 AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (25, N'PC', N'FSC', N'3', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009FD101269DF5 AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (24, N'PC', N'FSC', N'4', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009FD101269DF5 AS DateTime), NULL)
INSERT [dbo].[INVALID_CARD_CONFIGURATION] ([REC_SLNO], [CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (23, N'PC', N'FSC', N'5', 0, N'P', 0, 0, N'', N'', N'', 1, CAST(0x00009FD101269DF5 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[INVALID_CARD_CONFIGURATION] OFF
GO
SET IDENTITY_INSERT [dbo].[M_PAPER_SIZES] ON
INSERT [dbo].[M_PAPER_SIZES] ([SYS_ID], [PAPER_SIZE_NAME], [PAPER_SIZE_CATEGORY], [REC_ACTIVE]) VALUES (52, N'A4', N'A', 1)
INSERT [dbo].[M_PAPER_SIZES] ([SYS_ID], [PAPER_SIZE_NAME], [PAPER_SIZE_CATEGORY], [REC_ACTIVE]) VALUES (53, N'A3', N'A', 1)
INSERT [dbo].[M_PAPER_SIZES] ([SYS_ID], [PAPER_SIZE_NAME], [PAPER_SIZE_CATEGORY], [REC_ACTIVE]) VALUES (54, N'LETTER', N'L', 1)
INSERT [dbo].[M_PAPER_SIZES] ([SYS_ID], [PAPER_SIZE_NAME], [PAPER_SIZE_CATEGORY], [REC_ACTIVE]) VALUES (55, N'LEDGER', N'L', 1)
INSERT [dbo].[M_PAPER_SIZES] ([SYS_ID], [PAPER_SIZE_NAME], [PAPER_SIZE_CATEGORY], [REC_ACTIVE]) VALUES (56, N'LEGAL', N'L', 1)
INSERT [dbo].[M_PAPER_SIZES] ([SYS_ID], [PAPER_SIZE_NAME], [PAPER_SIZE_CATEGORY], [REC_ACTIVE]) VALUES (57, N'INVOICE', N'I', 1)
INSERT [dbo].[M_PAPER_SIZES] ([SYS_ID], [PAPER_SIZE_NAME], [PAPER_SIZE_CATEGORY], [REC_ACTIVE]) VALUES (58, N'EXECUTIVE', N'E', 1)
SET IDENTITY_INSERT [dbo].[M_PAPER_SIZES] OFF
GO
INSERT [dbo].[M_JOB_CATEGORIES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Copy', 1)
INSERT [dbo].[M_JOB_CATEGORIES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Doc-Filing', 5)
INSERT [dbo].[M_JOB_CATEGORIES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Fax', 4)
INSERT [dbo].[M_JOB_CATEGORIES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Print', 3)
INSERT [dbo].[M_JOB_CATEGORIES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Scan', 2)
GO

INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'-1', N'DDR', N'-', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'-1', N'DPR', N'-', 0, N' ', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'BR', N'FSC', N'-', 0, N' ', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'BR', N'FSC', N'1', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'BR', N'FSC', N'2', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'BR', N'FSC', N'3', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'BR', N'FSC', N'4', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'BR', N'FSC', N'5', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'MS', N'FSC', N'-', 0, N' ', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'MS', N'FSC', N'1', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'MS', N'FSC', N'2', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'MS', N'FSC', N'3', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'MS', N'FSC', N'4', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'MS', N'FSC', N'5', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'PC', N'FSC', N'-', 0, N' ', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'PC', N'FSC', N'1', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'PC', N'FSC', N'2', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'PC', N'FSC', N'3', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'PC', N'FSC', N'4', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)
INSERT [dbo].[CARD_CONFIGURATION] ([CARD_TYPE], [CARD_RULE], [CARD_SUB_RULE], [CARD_DATA_ENABLED], [CARD_DATA_ON], [CARD_POSITION_START], [CARD_POSITION_LENGTH], [CARD_DELIMETER_START], [CARD_DELIMETER_END], [CARD_CODE_VALUE], [REC_ACTIVE], [REC_CDATE], [REC_MDATE]) VALUES (N'PC', N'FSC', N'5', 0, N'P', 0, 0, N'', N'', N'', 1, getdate(), NULL)

GO

IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'T_AUTO_REFILL', N'COLUMN',N'IS_REFILL_REQUIRED'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is refill details are modified' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_AUTO_REFILL', @level2type=N'COLUMN',@level2name=N'IS_REFILL_REQUIRED'
GO
SET IDENTITY_INSERT [dbo].[T_AUTO_REFILL] ON
INSERT [dbo].[T_AUTO_REFILL] ([REC_SYSID], [AUTO_FILLING_TYPE], [AUTO_REFILL_FOR], [ADD_TO_EXIST_LIMITS], [AUTO_REFILL_ON], [AUTO_REFILL_VALUE], [LAST_REFILLED_ON], [IS_REFILL_REQUIRED]) VALUES (1, N'Manual', N'U', N'Yes', N'Every Day', N'11:55 PM', NULL, 1)
INSERT [dbo].[T_AUTO_REFILL] ([REC_SYSID], [AUTO_FILLING_TYPE], [AUTO_REFILL_FOR], [ADD_TO_EXIST_LIMITS], [AUTO_REFILL_ON], [AUTO_REFILL_VALUE], [LAST_REFILLED_ON], [IS_REFILL_REQUIRED]) VALUES (2, N'Manual', N'C', N'Yes', N'Every Day', N'11:55 PM', NULL, 1)
SET IDENTITY_INSERT [dbo].[T_AUTO_REFILL] OFF
GO

SET IDENTITY_INSERT [dbo].[AD_SETTINGS] ON
INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (1, N'DOMAIN_CONTROLLER', N'', N'Domain host from login screen')
INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (2, N'DOMAIN_NAME', N'', N'Domain Name')
INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (3, N'AD_USERNAME', N'', N'AD User name with admin permissions')
INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (4, N'AD_PASSWORD', N'', N'AD password with admin permissions')
INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (5, N'AD_PORT', N'389', N'Default port is 389')
INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (6, N'AD_FULLNAME', N'cn', N'cn,displayName')
SET IDENTITY_INSERT [dbo].[AD_SETTINGS] OFF
GO

SET IDENTITY_INSERT [dbo].[M_OSA_JOB_PROPERTIES] ON
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (1, N'Finishing', N'P', N'collate', 5, N'Collate', N'list', N'SORT,GROUP', N'SORT', NULL, 0, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (2, N'Main', N'P', N'color-mode', 2, N'Color Mode', N'list', N'AUTO,COLOR,MONOCHROME', N'AUTO', NULL, 1, N'PJL SET RENDERMODEL', N'CMYK4B,CMYK4B,G4')
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (3, N'Main', N'P', N'copies', 1, N'Copies', N'integer', N'1-999', NULL, NULL, 1, N'PJL SET QTY', NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (4, N'Main', N'P', N'duplex-dir', 4, N'Duplex Direction', N'list', N'BOOK,TABLET', N'BOOK', NULL, 0, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (5, N'Main', N'P', N'duplex-mode', 3, N'Duplex Mode', N'list', N'SIMPLEX,DUPLEX', N'SIMPLEX', NULL, 0, N'DUPLEX', N'OFF,ON')
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (6, N'Finishing', N'P', N'file-format', 4, N'File Format', N'list', N'AUTO,PCL,PCLXL,POSTSCRIPT', N'AUTO', NULL, 0, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (7, N'Finishing', N'P', N'file-name', 5, N'File Name', N'string', NULL, NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (8, N'Filing', N'P', N'filing', 3, N'Folder', N'list', N'QUICK,MAIN', N'QUICK', NULL, 1, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (9, N'Finishing', N'P', N'fit-to-page', 3, N'Fit To Page', N'boolean', N'true,false', N'true', NULL, 0, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (10, N'Finishing', N'P', N'offset', 8, N'Offset', N'boolean', N'true,false', N'true', NULL, 0, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (11, N'Finishing', N'P', N'orientation', 1, N'Orientation', N'list', N'PORTRAIT,LANDSCAPE', N'PORTRAIT', NULL, 0, N'', N'')
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (12, N'Finishing', N'P', N'output-tray', 4, N'Output Tray', N'list', N'AUTO,OUTTRAY1,OUTTRAY2,OUTTRAY3,OUTTRAY4,OUTTRAY5', N'AUTO', NULL, 1, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (13, N'Finishing', N'P', N'papersize', 2, N'Paper Size', N'list', N'LEDGER,LEGAL,LETTER,INVOICE,EXECUTIVE,A3,A4,A5,B4,B5,8K,16K,JAPANESE_POSTCARD_A6', N'AUTO', NULL, 0, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (14, N'Finishing', N'P', N'punch', 7, N'Punch', N'list', N'PUNCH_NONE,PUNCH', N'PUNCH_NONE', NULL, 1, N'PJL SET PUNCH', N'OFF,ON')
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (15, N'Filing', N'P', N'retention', 1, N'Retention', N'list', N'NONE,HOLD,HOLD_AFTER_PRINT,PROOF', N'NONE', NULL, 1, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (16, N'Filing', N'P', N'retention-password', 2, N'Retention Password', N'string', NULL, NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[M_OSA_JOB_PROPERTIES] ([REC_ID], [JOB_PROPERTY_CATEGORY], [JOB_TYPE], [JOB_PROPERTY], [JOB_ORDER], [JOB_PROPERTY_RESX], [JOB_PROPERTY_TYPE], [JOB_PROPERTY_ALLOWED], [JOB_PROPERTY_DEFAULT], [JOB_PROPERTY_VALIDATAION], [JOB_PROPERTY_SETTABLE], [JOB_PROPERTY_DRIVER_SETTING], [JOB_PROPERTY_DRIVER_VALUES]) VALUES (17, N'Finishing', N'P', N'staple', 6, N'Staple', N'list', N'STAPLE_NONE,STAPLE', N'STAPLE_NONE', NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[M_OSA_JOB_PROPERTIES] OFF
GO

SET IDENTITY_INSERT [dbo].[JOB_CONFIGURATION] ON
INSERT [dbo].[JOB_CONFIGURATION] ([SLNO], [JOBSETTING_KEY], [JOBSETTING_VALUE], [JOBSETTING_DISCRIPTION]) VALUES (1, N'JOB_RET_DAYS', N'7', NULL)
INSERT [dbo].[JOB_CONFIGURATION] ([SLNO], [JOBSETTING_KEY], [JOBSETTING_VALUE], [JOBSETTING_DISCRIPTION]) VALUES (2, N'JOB_RET_TIME', N'00:00', NULL)
INSERT [dbo].[JOB_CONFIGURATION] ([SLNO], [JOBSETTING_KEY], [JOBSETTING_VALUE], [JOBSETTING_DISCRIPTION]) VALUES (3, N'RETURN_TO_PRINT_JOBS', N'Enable', NULL)
INSERT [dbo].[JOB_CONFIGURATION] ([SLNO], [JOBSETTING_KEY], [JOBSETTING_VALUE], [JOBSETTING_DISCRIPTION]) VALUES (4, N'ON_NO_JOBS', N'Display Job List', NULL)
INSERT [dbo].[JOB_CONFIGURATION] ([SLNO], [JOBSETTING_KEY], [JOBSETTING_VALUE], [JOBSETTING_DISCRIPTION]) VALUES (5, N'PRINT_RETAIN_BUTTON_STATUS', N'Enable', NULL)
INSERT [dbo].[JOB_CONFIGURATION] ([SLNO], [JOBSETTING_KEY], [JOBSETTING_VALUE], [JOBSETTING_DISCRIPTION]) VALUES (6, N'SKIP_PRINT_SETTINGS', N'Disable', NULL)
INSERT [dbo].[JOB_CONFIGURATION] ([SLNO], [JOBSETTING_KEY], [JOBSETTING_VALUE], [JOBSETTING_DISCRIPTION]) VALUES (7, N'ANONYMOUS_PRINTING', N'Disable', NULL)
SET IDENTITY_INSERT [dbo].[JOB_CONFIGURATION] OFF
GO

INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Copy BW', 2)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Copy Color', 1)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Doc Filing BW', 8)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Doc Filing Color', 7)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Fax', 9)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Print BW', 4)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Print Color', 3)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Scan BW', 6)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Scan Color', 5)
INSERT [dbo].[M_JOB_TYPES] ([JOB_ID], [ITEM_ORDER]) VALUES (N'Settings', 10)
GO

SET IDENTITY_INSERT [dbo].[APP_SETTINGS] ON
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (1, N'GeneralSettings', N'Allowed retries for user login', N'LOGON_MAX_RETRY_COUNT', N'Allowed retries for user login', 3, N'5', N'int', N'Unlimited,1,2,3,4,5,6,7,8,9', N'-1,1,2,3,4,5,6,7,8,9', N'Unlimited', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (2, N'GeneralSettings', N'AUDIT_LOG', N'AUDIT_LOG', N'Audit Log', 4, N'Enable', N'string', N'Enable,Disable', N'Enable,Disable', N'Enable', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (3, N'GeneralSettings', N'Authentication Settings', N'AUTHENTICATION_SETTINGS', N'Authentication Settings', 1, N'DB', N'string', N'Database,Active Directory,Domain User', N'DB,AD,DM', N'AD Server', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (4, N'GeneralSettings', N'Duplex support', N'SUPPORT_FOR_DUPLEX', N'Support for Duplex', 10, N'Enable', N'string', N'Enable,Disable', N'Enable,Disable', N'Enable', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (5, N'GeneralSettings', N'Local Database Management', N'LOCAL_DATABASE_MANAGEMENT', N'Local Database Management', 5, N'Direct Manual Entry', N'string', N'Direct Manual Entry,Import', N'Direct Manual Entry,Import', N'Direct Manual Entry', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (14, N'GeneralSettings', N'MFP User Authentication', N'MFP_USER_AUTHENTICATION', N'MFP EA login authentication', 9, N'Login for All functions', N'string', N'Login for All functions,Login for MX-Print only', N'Login for All functions,Login for MX-SW100 only', N'Login for All functions', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (7, N'GeneralSettings', N'Return to Print Jobs', N'RETURN_TO_JOB_LIST', N'Return to Print Job list', 11, N'Enable', N'string', N'Enable,Disable', N'Enable,Disable', N'Enable', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (8, N'GeneralSettings', N'Self Register Device', N'SELF_REGISTER_DEVICE', N'Self register Device', 6, N'Enable', N'string', N'Enable,Disable', N'Enable,Disable', N'Enable', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (9, N'GeneralSettings', N'User Provisioning', N'USER_PROVISIONING', N'User Provisioning', 2, N'Self Registration', N'string', N'By Administrator Only,Self Registration,First Time Use', N'By Administrator Only,Self Registration,First Time Use', N'Self Registration', N'DROPDOWN')
INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE]) VALUES (10, N'ThemeSettings', N'Themes', N'THEMES', N'Themes', 7, N'Default', N'string', N'Default', NULL, N'Default', N'DROPDOWN')
SET IDENTITY_INSERT [dbo].[APP_SETTINGS] OFF
GO

SET IDENTITY_INSERT [dbo].[M_COUNTRIES] ON
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (-1, N'', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (19, N'Afghanistan', 0, 0, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (20, N'Albania', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (21, N'Algeria', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (22, N'American Samoa', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (23, N'Andorra', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (24, N'Angola', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (25, N'Anguilla', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (26, N'Antarctica', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (27, N'Antigua And Barbuda', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (28, N'Argentina', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (29, N'Armenia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (30, N'Aruba', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (31, N'Australia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (32, N'Austria', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (33, N'Azerbaijan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (34, N'Bahamas', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (35, N'Bahrain', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (36, N'Bangladesh', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (37, N'Barbados', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (38, N'Belarus', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (39, N'Belgium', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (40, N'Belize', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (41, N'Benin', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (42, N'Bermuda', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (43, N'Bhutan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (44, N'Bolivia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (45, N'Bosnia And Herzegowina', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (46, N'Botswana', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (47, N'Brazil', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (48, N'British Indian Ocean Territory', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (49, N'Brunei Darussalam', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (50, N'Bulgaria', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (51, N'Burkina Faso', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (52, N'Burundi', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (53, N'Cambodia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (54, N'Cameroon', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (2, N'Canada', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (55, N'Cape Verde', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (56, N'Cayman Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (57, N'Central African Republic', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (58, N'Chad', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (59, N'Chile', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (60, N'China', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (61, N'Christmas Island', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (62, N'Cocos (Keeling) Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (63, N'Colombia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (64, N'Comoros', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (65, N'Congo', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (3, N'Congo, The Dem Rep Of', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (66, N'Cook Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (67, N'Costa Rica', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (68, N'Cote D Ivoire', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (69, N'Croatia (Hrvatska)', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (70, N'Cyprus', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (71, N'Czech Republic', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (16, N'Denmark', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (72, N'Djibouti', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (73, N'Dominica', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (74, N'Dominican Republic', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (75, N'Ecuador', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (76, N'Egypt', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (77, N'El Salvador', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (78, N'Equatorial Guinea', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (79, N'Eritrea', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (80, N'Estonia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (81, N'Ethiopia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (82, N'Falkland (Malvinas) Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (83, N'Faroe Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (142, N'Federated States Of Micronesia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (84, N'Fiji', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (13, N'Finland', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (5, N'France', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (85, N'French Guiana', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (86, N'French Polynesia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (87, N'Gabon', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (88, N'Gambia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (89, N'Georgia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (6, N'Germany', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (90, N'Ghana', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (91, N'Gibraltar', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (92, N'Greece', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (93, N'Greenland', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (94, N'Grenada', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (95, N'Guadeloupe', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (96, N'Guam', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (97, N'Guatemala', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (7, N'Guernsey', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (98, N'Guinea', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (99, N'Guinea-Bissau', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (100, N'Guyana', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (101, N'Haiti', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (102, N'Holy See (Vatican City State)', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (103, N'Honduras', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (104, N'Hong Kong', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (105, N'Hungary', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (106, N'Iceland', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (108, N'India', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (109, N'Indonesia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (110, N'Ireland', 0, 1, 0, NULL)
GO
print 'Processed 100 total records'
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (8, N'Isle Of Man', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (111, N'Israel', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (11, N'Italy', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (112, N'Jamaica', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (10, N'Japan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (9, N'Jersey', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (113, N'Jordan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (114, N'Kazakhstan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (115, N'Kenya', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (116, N'Kiribati', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (118, N'Kuwait', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (119, N'Kyrgyzstan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (120, N'Laos', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (121, N'Latvia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (122, N'Lebanon', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (123, N'Lesotho', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (124, N'Liberia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (125, N'Liechtenstein', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (126, N'Lithuania', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (127, N'Luxembourg', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (128, N'Macau', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (129, N'Macedonia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (130, N'Madagascar', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (131, N'Malawi', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (132, N'Malaysia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (133, N'Maldives', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (134, N'Mali', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (135, N'Malta', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (136, N'Marshall Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (137, N'Martinique', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (138, N'Mauritania', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (139, N'Mauritius', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (140, N'Mayotte', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (141, N'Mexico', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (143, N'Moldova', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (144, N'Monaco', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (145, N'Mongolia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (18, N'Montenegro', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (146, N'Montserrat', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (147, N'Morocco', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (148, N'Mozambique', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (149, N'Myanmar', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (150, N'Namibia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (151, N'Nauru', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (152, N'Nepal', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (153, N'Netherlands Antilles', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (154, N'New Caledonia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (155, N'New Zealand', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (156, N'Nicaragua', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (157, N'Niger', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (158, N'Nigeria', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (159, N'Niue', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (160, N'Norfolk Island', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (161, N'Northern Mariana Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (17, N'Norway', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (162, N'Oman', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (163, N'Pakistan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (164, N'Palau', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (165, N'Panama', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (166, N'Papua New Guinea', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (167, N'Paraguay', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (168, N'Peru', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (169, N'Philippines', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (170, N'Pitcairn', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (171, N'Poland', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (172, N'Portugal', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (173, N'Puerto Rico', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (174, N'Qatar', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (175, N'Reunion', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (176, N'Romania', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (177, N'Russia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (178, N'Rwanda', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (195, N'S Georgia And S Sandwich Isl', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (179, N'Saint Kitts And Nevis', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (180, N'Saint Lucia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (181, N'Saint Vincent And Grenadines', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (182, N'Samoa', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (183, N'San Marino', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (184, N'Sao Tome And Principe', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (185, N'Saudi Arabia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (186, N'Senegal', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (107, N'Serbia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (187, N'Seychelles', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (188, N'Sierra Leone', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (189, N'Singapore', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (190, N'Slovakia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (191, N'Slovenia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (192, N'Solomon Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (193, N'Somalia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (194, N'South Africa', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (117, N'South Korea', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (14, N'Spain', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (196, N'Sri Lanka', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (197, N'St. Helena', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (198, N'St. Pierre And Miquelon', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (200, N'Suriname', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (201, N'Svalbard And Jan Mayen Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (202, N'Swaziland', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (15, N'Sweden', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (203, N'Switzerland', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (204, N'Taiwan', 0, 1, 0, NULL)
GO
print 'Processed 200 total records'
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (205, N'Tajikistan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (206, N'Tanzania, United Republic Of', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (207, N'Thailand', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (12, N'The Netherlands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (199, N'Timor-Leste', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (208, N'Togo', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (209, N'Tokelau', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (210, N'Tonga', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (211, N'Trinidad And Tobago', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (212, N'Tunisia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (213, N'Turkey', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (214, N'Turkmenistan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (215, N'Turks And Caicos Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (216, N'Tuvalu', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (217, N'Uganda', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (218, N'Ukraine', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (219, N'United Arab Emirates', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (4, N'United Kingdom', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (1, N'United States', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (221, N'Uruguay', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (220, N'Us Minor Outlying Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (222, N'Uzbekistan', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (223, N'Vanuatu', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (224, N'Venezuela', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (225, N'Viet Nam', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (226, N'Virgin Islands (British)', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (227, N'Virgin Islands (U.S.)', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (228, N'Wallis And Futuna Islands', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (229, N'Western Sahara', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (230, N'Yemen', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (231, N'Yugoslavia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (237, N'z', 0, 0, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (232, N'Zaire', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (233, N'Zambia', 0, 1, 0, NULL)
INSERT [dbo].[M_COUNTRIES] ([COUNTRY_ID], [COUNTRY_NAME], [COUNTRY_DEFAULT], [REC_ACTIVE], [REC_USER], [REC_DATE]) VALUES (234, N'Zimbabwe', 0, 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[M_COUNTRIES] OFF
GO

INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Copy BW', 100, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Copy Color', 50, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Doc Filing BW', 100, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Doc Filing Color', 50, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Fax', 100, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Print BW', 100, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Print Color', 50, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Scan BW', 100, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Scan Color', 50, 0, 0, 0, 1)
INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Settings', 50, 0, 0, 0, 1)
--INSERT [dbo].[T_JOB_PERMISSIONS_LIMITS] ([GRUP_ID], [PERMISSIONS_LIMITS_ON], [JOB_TYPE], [JOB_LIMIT], [JOB_USED], [ALERT_LIMIT], [ALLOWED_OVER_DRAFT], [JOB_ISALLOWED]) VALUES (1, 0, N'Settings', 50, 0, 0, 0, 1)
GO

SET IDENTITY_INSERT [dbo].[APP_LANGUAGES] ON
INSERT [dbo].[APP_LANGUAGES] ([REC_SLNO], [APP_CULTURE], [APP_NEUTRAL_CULTURE], [APP_LANGUAGE], [APP_CULTURE_DIR], [REC_ACTIVE]) VALUES (14, N'en-US', N'en', N'English (United States)', N'LTR', 1)
SET IDENTITY_INSERT [dbo].[APP_LANGUAGES] OFF
GO
/****** Object:  Table [dbo].[M_ROLES]    Script Date: 02/28/2012 09:32:10 ******/
INSERT [dbo].[M_ROLES] ([ROLE_ID], [ROLE_NAME]) VALUES (N'ADMIN', N'Administrator')
INSERT [dbo].[M_ROLES] ([ROLE_ID], [ROLE_NAME]) VALUES (N'USER', N'User/Team Member')
GO

/****** Object:  Table [dbo].[M_COST_CENTERS]    Script Date: 12/28/2011 10:56:17 ******/
DELETE FROM [dbo].[M_COST_CENTERS]
GO
/****** Object:  Table [dbo].[M_COST_CENTERS]    Script Date: 12/28/2011 10:56:17 ******/
SET IDENTITY_INSERT [dbo].[M_COST_CENTERS] ON
INSERT [dbo].[M_COST_CENTERS] ([COSTCENTER_ID], [COSTCENTER_NAME], [REC_ACTIVE], [REC_DATE], [REC_USER], [ALLOW_OVER_DRAFT]) VALUES (1, N'Default', 1, getdate(), N'administrator', NULL)
SET IDENTITY_INSERT [dbo].[M_COST_CENTERS] OFF

/****** Object:  Table [dbo].[M_MFP_GROUPS]    Script Date: 03/03/2012 01:34:55 ******/
DELETE FROM [dbo].[M_MFP_GROUPS]
GO
/****** Object:  Table [dbo].[M_MFP_GROUPS]    Script Date: 03/03/2012 01:34:55 ******/
SET IDENTITY_INSERT [dbo].[M_MFP_GROUPS] ON
INSERT [dbo].[M_MFP_GROUPS] ([GRUP_ID], [GRUP_NAME], [REC_ACTIVE], [REC_DATE], [REC_USER]) VALUES (1, N'Default', 1, CAST(0x0000A009001982DC AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[M_MFP_GROUPS] OFF

/****** Object:  Table [dbo].[APP_IMAGES]    Script Date: 03/12/2012 16:14:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_IMAGES]') AND type in (N'U'))
DROP TABLE [dbo].[APP_IMAGES]
GO
/****** Object:  Table [dbo].[APP_IMAGES]    Script Date: 03/12/2012 16:14:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_IMAGES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APP_IMAGES](
	[REC_SYS_ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[USER_ID] [numeric](18, 0) NULL,
	[BG_APP_NAME] [nvarchar](20)  NOT NULL,
	[BG_IMAGE_TYPE] [nvarchar](50)  NULL,
	[BG_DEFAULT_IMAGE_PATH] [nvarchar](255)  NULL,
	[BG_UPDATED_IMAGEPATH] [nvarchar](255)  NULL,
	[BG_IMAGE_EXTENSION] [nvarchar](10)  NULL,
	[BG_REC_HEIGHT] [numeric](18, 0) NULL,
	[BG_REC_WIDTH] [numeric](18, 0) NULL,
	[BG_ACT_HEIGHT] [numeric](18, 0) NULL,
	[BG_ACT_WIDTH] [numeric](18, 0) NULL,
	[BG_ALLOWED_SIZE_KB] [numeric](18, 0) NULL,
	[BG_GENERATED_DATE] [datetime] NULL,
	[BG_FOLDER_NAME] [nvarchar](50)  NULL,
	[APP_THEME] [nvarchar](150)  NULL,
	[APP_BACKGROUND_COLOR] [nvarchar](150)  NULL,
	[APP_TITTLEBAR_COLOR] [nvarchar](150)  NULL,
	[APP_FONT_COLOR] [nvarchar](50)  NULL,
	[BG_UPLOADED_DATE] [datetime] NULL,
	[BG_MODIFIED_DATE] [datetime] NULL,
	[REC_STATUS] [bit] NULL,
 CONSTRAINT [PK_APP_IMAGES] PRIMARY KEY CLUSTERED 
(
	[BG_APP_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[APP_IMAGES] ON
INSERT [dbo].[APP_IMAGES] ([REC_SYS_ID], [USER_ID], [BG_APP_NAME], [BG_IMAGE_TYPE], [BG_DEFAULT_IMAGE_PATH], [BG_UPDATED_IMAGEPATH], [BG_IMAGE_EXTENSION], [BG_REC_HEIGHT], [BG_REC_WIDTH], [BG_ACT_HEIGHT], [BG_ACT_WIDTH], [BG_ALLOWED_SIZE_KB], [BG_GENERATED_DATE], [BG_FOLDER_NAME], [APP_THEME], [APP_BACKGROUND_COLOR], [APP_TITTLEBAR_COLOR], [APP_FONT_COLOR], [BG_UPLOADED_DATE], [BG_MODIFIED_DATE], [REC_STATUS]) VALUES (CAST(2 AS Numeric(18, 0)), NULL, N'480X272', N'PSP Background', N'PSP_BG.png', NULL, N'.png', CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(272 AS Numeric(18, 0)), CAST(440 AS Numeric(18, 0)), CAST(650 AS Numeric(18, 0)), NULL, N'480X272', N'Green', NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[APP_IMAGES] ([REC_SYS_ID], [USER_ID], [BG_APP_NAME], [BG_IMAGE_TYPE], [BG_DEFAULT_IMAGE_PATH], [BG_UPDATED_IMAGEPATH], [BG_IMAGE_EXTENSION], [BG_REC_HEIGHT], [BG_REC_WIDTH], [BG_ACT_HEIGHT], [BG_ACT_WIDTH], [BG_ALLOWED_SIZE_KB], [BG_GENERATED_DATE], [BG_FOLDER_NAME], [APP_THEME], [APP_BACKGROUND_COLOR], [APP_TITTLEBAR_COLOR], [APP_FONT_COLOR], [BG_UPLOADED_DATE], [BG_MODIFIED_DATE], [REC_STATUS]) VALUES (CAST(1 AS Numeric(18, 0)), NULL, N'FORM', N'FORM Browser', NULL, NULL, NULL, CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), NULL, NULL, CAST(650 AS Numeric(18, 0)), NULL, N'FormBrowser', N'Green', NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[APP_IMAGES] ([REC_SYS_ID], [USER_ID], [BG_APP_NAME], [BG_IMAGE_TYPE], [BG_DEFAULT_IMAGE_PATH], [BG_UPDATED_IMAGEPATH], [BG_IMAGE_EXTENSION], [BG_REC_HEIGHT], [BG_REC_WIDTH], [BG_ACT_HEIGHT], [BG_ACT_WIDTH], [BG_ALLOWED_SIZE_KB], [BG_GENERATED_DATE], [BG_FOLDER_NAME], [APP_THEME], [APP_BACKGROUND_COLOR], [APP_TITTLEBAR_COLOR], [APP_FONT_COLOR], [BG_UPLOADED_DATE], [BG_MODIFIED_DATE], [REC_STATUS]) VALUES (CAST(3 AS Numeric(18, 0)), NULL, N'WEB', N'Administration Background', N'WEB_BG.jpg', NULL, N'.jpg', CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(681 AS Numeric(18, 0)), CAST(1024 AS Numeric(18, 0)), CAST(850 AS Numeric(18, 0)), NULL, N'Images', N'Green', NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[APP_IMAGES] ([REC_SYS_ID], [USER_ID], [BG_APP_NAME], [BG_IMAGE_TYPE], [BG_DEFAULT_IMAGE_PATH], [BG_UPDATED_IMAGEPATH], [BG_IMAGE_EXTENSION], [BG_REC_HEIGHT], [BG_REC_WIDTH], [BG_ACT_HEIGHT], [BG_ACT_WIDTH], [BG_ALLOWED_SIZE_KB], [BG_GENERATED_DATE], [BG_FOLDER_NAME], [APP_THEME], [APP_BACKGROUND_COLOR], [APP_TITTLEBAR_COLOR], [APP_FONT_COLOR], [BG_UPLOADED_DATE], [BG_MODIFIED_DATE], [REC_STATUS]) VALUES (CAST(4 AS Numeric(18, 0)), NULL, N'Wide-SVGA', N'MFP Background(WSVGA)', N'WSVGA_BG.png', NULL, N'.png', CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(550 AS Numeric(18, 0)), CAST(1030 AS Numeric(18, 0)), CAST(650 AS Numeric(18, 0)), NULL, N'Wide-SVGA', N'Green', NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[APP_IMAGES] ([REC_SYS_ID], [USER_ID], [BG_APP_NAME], [BG_IMAGE_TYPE], [BG_DEFAULT_IMAGE_PATH], [BG_UPDATED_IMAGEPATH], [BG_IMAGE_EXTENSION], [BG_REC_HEIGHT], [BG_REC_WIDTH], [BG_ACT_HEIGHT], [BG_ACT_WIDTH], [BG_ALLOWED_SIZE_KB], [BG_GENERATED_DATE], [BG_FOLDER_NAME], [APP_THEME], [APP_BACKGROUND_COLOR], [APP_TITTLEBAR_COLOR], [APP_FONT_COLOR], [BG_UPLOADED_DATE], [BG_MODIFIED_DATE], [REC_STATUS]) VALUES (CAST(5 AS Numeric(18, 0)), NULL, N'Wide-VGA', N'MFP Background(WVGA)', N'Wide-VGA_BG.png', NULL, N'.png', CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(392 AS Numeric(18, 0)), CAST(800 AS Numeric(18, 0)), CAST(650 AS Numeric(18, 0)), NULL, N'Wide-VGA', N'Green', NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[APP_IMAGES] OFF



/****** Object:  Table [dbo].[M_USERS]    Script Date: 03/09/2012 17:56:08 ******/
DELETE FROM [dbo].[M_USERS]
GO
/****** Object:  Table [dbo].[M_USERS]    Script Date: 03/09/2012 17:56:08 ******/
SET IDENTITY_INSERT [dbo].[M_USERS] ON
INSERT [dbo].[M_USERS] ([USR_ACCOUNT_ID], [USR_SOURCE], [USR_DOMAIN], [USR_ID], [USR_CARD_ID], [USR_NAME], [USR_PIN], [USR_PASSWORD], [USR_ATHENTICATE_ON], [USR_EMAIL], [USR_DEPARTMENT], [USR_COSTCENTER], [USR_AD_PIN_FIELD], [USR_ROLE], [RETRY_COUNT], [RETRY_DATE], [REC_CDATE], [REC_ACTIVE], [ALLOW_OVER_DRAFT]) VALUES (1896, N'DB', N'Local', N'admin', N'', N'', N'', N'S2QBAyUKAfs=', NULL, N'', 0, -1, NULL, N'admin', 5, NULL, CAST(0x0000A00F01271113 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[M_USERS] OFF


/****** Object:  Table [dbo].[M_SMTP_SETTINGS]    Script Date: 03/09/2012 18:05:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_SMTP_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[M_SMTP_SETTINGS]
GO
/****** Object:  Table [dbo].[M_SMTP_SETTINGS]    Script Date: 03/09/2012 18:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_SMTP_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_SMTP_SETTINGS](
	[REC_SYS_ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FROM_ADDRESS] [nvarchar](max)  NOT NULL,
	[CC_ADDRESS] [nvarchar](max)  NULL,
	[BCC_ADDRESS] [nvarchar](max)  NULL,
	[SMTP_HOST] [nvarchar](50)  NOT NULL,
	[SMTP_PORT] [int] NOT NULL,
	[DOMAIN_NAME] [nvarchar](50)  NOT NULL,
	[USERNAME] [nvarchar](50)  NOT NULL,
	[PASSWORD] [nvarchar](50)  NOT NULL,
 CONSTRAINT [PK_M_SMTP_SETTINGS] PRIMARY KEY CLUSTERED 
(
	[REC_SYS_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO



----------------------------------UPGRADE SCRIPT-------------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[RecordAuditLog]    Script Date: 03/17/2012 16:00:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordAuditLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecordAuditLog]


/****** Object:  StoredProcedure [dbo].[RecordAuditLog]    Script Date: 03/17/2012 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordAuditLog] 
	@messageSource nvarchar(30), 
	@messageType nvarchar(20), 
	@message nvarchar(max), 
	@suggestion nvarchar(max), 
	@exception nvarchar(max), 
	@stackTrace nvarchar(max),
	@messageOwner nvarchar(30)
as 


if exists (select APPSETNG_KEY from APP_SETTINGS where APPSETNG_KEY = 'AUDIT_LOG' and APPSETNG_VALUE = 'Enable')
begin
	insert into T_AUDIT_LOG
	(
		MSG_SOURCE,
		MSG_TYPE,
		MSG_TEXT,
		MSG_SUGGESTION,
		MSG_EXCEPTION,
		MSG_STACKSTRACE,
		REC_USER,
		REC_DATE
	) 	
	values
	(
		@messageSource,
		@messageType,
		@message,
		@suggestion,
		@exception,
		@stackTrace,
		@messageOwner,
		GetDate()		
	)
end
GO
/****** Object:  StoredProcedure [dbo].[RecordHelloEvent]    Script Date: 03/17/2012 16:01:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordHelloEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecordHelloEvent]

/****** Object:  StoredProcedure [dbo].[RecordHelloEvent]    Script Date: 03/17/2012 16:02:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordHelloEvent]
	@location nvarchar(100),
	@serialNumber nvarchar(50),
	@modelName nvarchar(50),
	@ipAddress nvarchar(30),
	@deviceId nvarchar(50),
	@accessAddress nvarchar(100),
	@isEAMEnabled bit

as 
if exists (SELECT MFP_IP FROM M_MFPS where MFP_IP = @ipAddress)
	begin
		update M_MFPS set 
			MFP_LOCATION = @location, 
			MFP_SERIALNUMBER = @serialNumber, 
			MFP_DEVICE_ID = @deviceId,
			MFP_EAM_ENABLED = @isEAMEnabled 
		where MFP_IP = @ipAddress
	end	
else
begin
	if exists (select APPSETNG_KEY from APP_SETTINGS where APPSETNG_KEY = 'Self Register Device' and APPSETNG_VALUE = 'Enable')
	begin
			insert into M_MFPS
			(
				MFP_LOCATION, MFP_SERIALNUMBER, MFP_IP, MFP_DEVICE_ID, MFP_NAME, MFP_SSO,
				MFP_LOCK_DOMAIN_FIELD, MFP_URL, MFP_LOGON_MODE, MFP_LOGON_AUTH_SOURCE, MFP_MANUAL_AUTH_TYPE, 
				MFP_CARDREADER_TYPE, ALLOW_NETWORK_PASSWORD, REC_ACTIVE, FTP_ADDRESS, FTP_PORT,
				FTP_PROTOCOL, MFP_PRINT_API, MFP_EAM_ENABLED
			)
			values
			(
				@location , @serialNumber, @ipAddress, @deviceId, @modelName , '1',
				'0', @accessAddress, 'Manual', 'DB', 'Username/Password',
				'PC', 'False' , '1', @ipAddress, '21', 'ftp', 'FTP', @isEAMEnabled
			)
			insert into T_GROUP_MFPS
			(
				GRUP_ID,MFP_IP,REC_ACTIVE,REC_DATE
			)
			values
			(
				'1',@ipAddress,'1', getdate()
			)
	end	
end
GO
/****** Object:  StoredProcedure [dbo].[ReportBuilder]    Script Date: 03/17/2012 16:02:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportBuilder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReportBuilder]

/****** Object:  StoredProcedure [dbo].[ReportBuilder]    Script Date: 03/17/2012 16:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ReportBuilder]
	@ReportOn varchar(50),
	@authenticationSource varchar(2),
	@fromDate varchar(10),
	@toDate  varchar(10)
	
as

select * into #joblog from T_JOB_LOG where (JOB_STATUS = 'FINISHED' or JOB_STATUS = 'SUSPENDED')  and REC_DATE BETWEEN @fromDate + ' 00:00:00' and  @toDate + ' 23:59:59' 

alter table #joblog add MFP_SERIALNUMBER nvarchar(50), MFP_MODEL nvarchar(50), MFP_LOCATION nvarchar(100)

create table #JobReport
(
	slno int identity,UserID nvarchar(100) default '',
	ReportOn nvarchar(100) default '', Total int default 0, TotalBW int default 0, TotalColor int default 0
	,A3BW int default 0
	,A3C int default 0
	,A4BW int default 0
	,A4C int default 0
	,OtherBW int default 0
	,OtherC int default 0
	,Duplex_One_Sided float default 0
	,Duplex_Two_Sided float default 0
)

declare @sqlQuery nvarchar(max)
declare @sqlGroupOn nvarchar(100)

--create table #reportGroup groupItem nvarchar(100)

--insert into #JobReport (ReportOn) select distinct JOB_COMPUTER from #joblog

set @sqlGroupOn = @ReportOn --'MFP_IP,GRUP_ID,JOB_COMPUTER,USR_ID'

set @sqlQuery = 'insert into #JobReport (ReportOn) select distinct ' +  @sqlGroupOn + ' from #joblog'
exec(@sqlQuery)
create table #TempGroupCount(itemGroup nvarchar(100), itemCount int default 0)


-- A3BW
set @sqlQuery = 'insert into #TempGroupCount select ' + @sqlGroupOn + ', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''MONOCHROME'', ''AUTO'') group by ' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set A3BW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A3C
set @sqlQuery = 'insert into #TempGroupCount select ' + @sqlGroupOn + ', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'', ''AUTO'') group by ' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like 'A3%' and JOB_COLOR_MODE in('FULL-COLOR', 'SINGLE-COLOR', 'DUAL-COLOR') group by MFP_IP
update #JobReport set A3C = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A4BW
set @sqlQuery = 'insert into #TempGroupCount select ' + @sqlGroupOn + ', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE in(''MONOCHROME'', ''AUTO'') group by ' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like 'A4%' and JOB_COLOR_MODE = 'MONOCHROME' group by MFP_IP
update #JobReport set A4BW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A4C
set @sqlQuery = 'insert into #TempGroupCount select ' + @sqlGroupOn + ', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'', ''AUTO'') group by ' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like 'A4%' and JOB_COLOR_MODE in('FULL-COLOR', 'SINGLE-COLOR', 'DUAL-COLOR') group by MFP_IP
update #JobReport set A4C = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- OtherBW
set @sqlQuery = 'insert into #TempGroupCount select ' + @sqlGroupOn + ', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE not like ''A4%'' and JOB_PAPER_SIZE not like ''A3%'' and JOB_COLOR_MODE in(''MONOCHROME'', ''AUTO'') group by ' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like 'A4%'  or JOB_PAPER_SIZE not like 'A3%') and JOB_COLOR_MODE = 'MONOCHROME' group by MFP_IP
update #JobReport set OtherBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- OtherC
set @sqlQuery = 'insert into #TempGroupCount select ' + @sqlGroupOn + ', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE not like ''A4%''  and JOB_PAPER_SIZE not like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'', ''AUTO'') group by ' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where (JOB_PAPER_SIZE not like 'A4%'  or JOB_PAPER_SIZE not like 'A3%') and JOB_COLOR_MODE in('FULL-COLOR', 'SINGLE-COLOR', 'DUAL-COLOR') group by MFP_IP
update #JobReport set OtherC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- Duplex_One_Sided
set @sqlQuery = 'insert into #TempGroupCount select ' + @sqlGroupOn + ', sum(JOB_SHEET_COUNT) from #joblog where DUPLEX_MODE = ''1SIDED'' group by ' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT) from #joblog where  DUPLEX_MODE = '1SIDED' group by MFP_IP
update #JobReport set Duplex_One_Sided = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- Duplex_Two_Sided
set @sqlQuery = 'insert into #TempGroupCount select ' + @sqlGroupOn + ', sum(JOB_SHEET_COUNT) from #joblog where DUPLEX_MODE <> ''1SIDED'' group by ' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT) from #joblog where  DUPLEX_MODE <> '1SIDED' group by MFP_IP
update #JobReport set Duplex_Two_Sided = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn 
update #JobReport set Duplex_Two_Sided = Duplex_Two_Sided / 2
truncate table #TempGroupCount



update #JobReport set TotalBW = A3BW + A4BW + OtherBW, TotalColor = A3C + A4C + OtherC
update #JobReport set Total = TotalBW + TotalColor


/*
,UserName nvarchar(100) default ''
	,Department nvarchar(100) default ''
	,ComputerName nvarchar(100) default ''
	,LoginName nvarchar(100) default ''
	,ModelName nvarchar(100) default ''
	,SerialNumber nvarchar(100) default ''
	,AuthenticationSource char(2) default ''
	,CostCenter nvarchar(100) default ''
	,GroupID nvarchar(100) default ''
*/


if @sqlGroupOn = 'USR_ID'
begin
	alter table #JobReport add UserName nvarchar(50) default ''
	update #JobReport set  UserName = M_USERS.USR_NAME  from M_USERS where M_USERS.USR_ID = #JobReport.ReportOn 
	insert into #JobReport(ReportOn,Total,TotalBW,TotalColor,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
	select 'Total',sum(Total),sum(TotalBW),sum(TotalColor),Sum(A3BW),sum(A3C),Sum(A4BW),Sum(A4C),sum(OtherBW),sum(OtherC),sum(Duplex_One_Sided),Sum(Duplex_Two_Sided) from #JobReport
	select ReportOn as UserID ,Total,TotalBW,TotalColor,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,UserName from #JobReport
end

if @sqlGroupOn = 'MFP_IP'
begin
	alter table #JobReport add ModelName nvarchar(50) default '', SerialNumber nvarchar(50) default '', Location nvarchar(50) default ''			
	update #JobReport set ModelName = M_MFPS.MFP_MODEL,  SerialNumber = M_MFPS.MFP_SERIALNUMBER, Location = M_MFPS.MFP_LOCATION from M_MFPS where  M_MFPS.MFP_IP = #JobReport.ReportOn
	insert into #JobReport(SerialNumber,Total,TotalBW,TotalColor,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
	select	'Total',sum(Total),sum(TotalBW),sum(TotalColor),Sum(A3BW),sum(A3C),Sum(A4BW),Sum(A4C),sum(OtherBW),sum(OtherC),sum(Duplex_One_Sided),Sum(Duplex_Two_Sided) from #JobReport
	select SerialNumber as SerialNumber ,Total,TotalBW,TotalColor,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,ModelName,Location from #JobReport
end

if @sqlGroupOn = 'GRUP_ID'
begin
--select * from #JobReport
	alter table #JobReport add CostCenter nvarchar(100) default ''
	update #JobReport set CostCenter = #joblog.COST_CENTER_NAME from  #joblog where #JobReport.ReportOn = CAST(#joblog.GRUP_ID AS varchar(100)) 
	insert into #JobReport(CostCenter,Total,TotalBW,TotalColor,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
	select	'Total',sum(Total),sum(TotalBW),sum(TotalColor),Sum(A3BW),sum(A3C),Sum(A4BW),Sum(A4C),sum(OtherBW),sum(OtherC),sum(Duplex_One_Sided),Sum(Duplex_Two_Sided) from #JobReport
	select CostCenter as CostCenter,Total,TotalBW,TotalColor,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided from #JobReport
end

if @sqlGroupOn = 'JOB_COMPUTER'
begin
	alter table #JobReport add ComputerName nvarchar(100) default ''
	update #JobReport set ComputerName = #joblog.JOB_COMPUTER from  #joblog where #JobReport.ReportOn = #joblog.JOB_COMPUTER
	insert into #JobReport(ComputerName,Total,TotalBW,TotalColor,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
	select	'Total',sum(Total),sum(TotalBW),sum(TotalColor),Sum(A3BW),sum(A3C),Sum(A4BW),Sum(A4C),sum(OtherBW),sum(OtherC),sum(Duplex_One_Sided),Sum(Duplex_Two_Sided) from #JobReport
	select ComputerName as ComputerName,Total,TotalBW,TotalColor,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided from #JobReport

end
GO
/****** Object:  StoredProcedure [dbo].[GetUserReport]    Script Date: 03/17/2012 16:03:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserReport]

/****** Object:  StoredProcedure [dbo].[GetUserReport]    Script Date: 03/17/2012 16:03:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetUserReport]
@ReportOn varchar(50),
@UserID varchar(50),
@AuthenticationSource varchar(2),
@fromDate varchar(10),
@toDate  varchar(10),
@userRole varchar(10)
as 
begin

declare @sql varchar(max)

--Inserting JOBLog table data to Temp1 Table

select R.USR_ID,
case 
when R.JOB_COLOR_MODE ='MONOCHROME' then 'Black and White'
ELSE ''
end 'BlackandWhite',
case 
when R.JOB_COLOR_MODE ='FULL-COLOR' then 'Colour' end 'Colour',
case  
when R.JOB_COLOR_MODE ='MONOCHROME' then R.JOB_SHEET_COUNT_BW 
ELSE ''
end BWJObs,
case  
when R.JOB_COLOR_MODE ='FULL-COLOR' then R.JOB_SHEET_COUNT_COLOR end ColourJObs,
case 
when R.JOB_PAPER_SIZE like 'A4%' then (R.JOB_SHEET_COUNT_BW) end A4BW,
case 
when R.JOB_PAPER_SIZE like 'A4%' then (R.JOB_SHEET_COUNT_COLOR) end A4C,
case 
when R.JOB_PAPER_SIZE like 'A3%' then (R.JOB_SHEET_COUNT_BW) end A3BW,
case 
when R.JOB_PAPER_SIZE like 'A3%' then (R.JOB_SHEET_COUNT_COLOR) end A3C,
case 
when R.JOB_PAPER_SIZE not in(select  JOB_PAPER_SIZE from T_JOB_LOG where JOB_PAPER_SIZE like 'A4%' or JOB_PAPER_SIZE like 'A3%' ) then (R.JOB_SHEET_COUNT_BW) end OtherBW,
case 
when R.JOB_PAPER_SIZE not in(select  JOB_PAPER_SIZE from T_JOB_LOG where JOB_PAPER_SIZE like 'A4%' or JOB_PAPER_SIZE like 'A3%' ) then (R.JOB_SHEET_COUNT_COLOR) end OtherC,
case R.DUPLEX_MODE
when '1SIDED' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR)
end 'ONESIDED',
case 
when  R.DUPLEX_MODE <>'1SIDED' then (R.JOB_SHEET_COUNT_BW+R.JOB_SHEET_COUNT_COLOR)
end 'TWOSIDED',
R.USR_DEPT,R.JOB_COMPUTER,R.MFP_IP,R.GRUP_ID,R.COST_CENTER_NAME,R.JOB_SHEET_COUNT,R.JOB_USRNAME,R.JOB_SHEET_COUNT_COLOR,R.JOB_SHEET_COUNT_BW,M.MFP_SERIALNUMBER,M.MFP_MODEL into #TEMP1  from T_JOB_LOG R left join M_MFPS M on R.MFP_IP=M.MFP_IP where (R.JOB_STATUS='FINISHED' or R.JOB_STATUS ='SUSPENDED') and R.JOB_COLOR_MODE<>'' and  R.JOB_END_DATE BETWEEN '' + @fromDate + ' 00:00' and '' +@toDate  + ' 23:59' and R.USR_ID=@UserID

--update #TEMP1 table null values
update #TEMP1 set A4BW=0 where A4BW is null
update #TEMP1 set A4C=0 where A4C is null
update #TEMP1 set A3BW=0 where A3BW is null
update #TEMP1 set A3C=0 where A3C is null
update #TEMP1 set OtherBW=0 where OtherBW is null
update #TEMP1 set OtherC=0 where OtherC is null
update #TEMP1 set ONESIDED=0 where ONESIDED is null
update #TEMP1 set TWOSIDED=0 where TWOSIDED is null

--Create #JobReport Table fro select satement
--select * from #TEMP1
create table #JobReport(slno int identity, ReportOf nvarchar(100) default '', UserID nvarchar(100) default '',
ReportOn nvarchar(100) default '', Total int default 0, TotalBW int default 0, TotalColor int default 0
,A3BW int default 0
,A3C int default 0
,A4BW int default 0
,A4C int default 0
,OtherBW int default 0
,OtherC int default 0
,Duplex_One_Sided float default 0
,Duplex_Two_Sided float default 0
,UserName nvarchar(100) default ''
,Department nvarchar(100) default ''
,ComputerName nvarchar(100) default ''
,LoginName nvarchar(100) default ''
,ModelName nvarchar(100) default ''
,SerialNumber nvarchar(100) default ''
,AuthenticationSource char(2) default ''
,CostCenter nvarchar(100) default ''
,GroupID nvarchar(100) default ''

)
--Total Calculation 
declare @TotalCalculation varchar(max)
declare @TotalColumnName varchar(50)
--Total text display column name based on reporton condiiotn
if(@ReportOn='USR_ID')
	  begin
		set @TotalColumnName='UserID'
      end
if(@ReportOn='MFP_IP')
	  begin
		set @TotalColumnName='SerialNumber'
      end
if(@ReportOn='GRUP_ID')
	  begin
		set @TotalColumnName='COSTCENTER'
      end
if(@ReportOn='JOB_COMPUTER')
	  begin
		set @TotalColumnName='ComputerName'
      end

				set @TotalCalculation='insert into #JobReport('+@TotalColumnName+',Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
				select ''Total'',(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as TOTAL,
				
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,
				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as TotalONESIDED,
				sum(TWOSIDED) / 2 TWOSIDED from #TEMP1'

--User ID 
 if(@ReportOn='USR_ID')
	  begin
				set @sql='insert into #JobReport(UserName,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,UserID,ComputerName)
				select JOB_USRNAME as ''UserName'',(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,
				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as ''Duplex_One_Side'',
				sum(TWOSIDED)/2 as ''Duplex_Two_Side'',' + @ReportOn + ',JOB_COMPUTER as [Computer Name] from #TEMP1
				group by  ' + @ReportOn + ',JOB_USRNAME,JOB_COMPUTER'
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
			select UserID,Total,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,ComputerName,UserName  from  #JobReport 
	  end

--MFP IP
if(@ReportOn='MFP_IP')
	  begin
				set @sql='insert into #JobReport(SerialNumber,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,ModelName)
				select MFP_SERIALNUMBER as SerialNumber,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,
				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as ''Duplex_One_Side'',
         		sum(TWOSIDED)/2 as ''Duplex_Two_Side'',MFP_MODEL from #TEMP1
				group by  ' + @ReportOn + ',MFP_SERIALNUMBER,MFP_MODEL'
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select SerialNumber,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,ModelName from  #JobReport 
	 end

--Department
if(@ReportOn='GRUP_ID')
	  begin
				set @sql='insert into #JobReport(CostCenter,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
				select COST_CENTER_NAME as CostCenter,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,
				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as ''Duplex_One_Side'',
				sum(TWOSIDED)/2 as ''Duplex_Two_Side'' from #TEMP1
				group by  '+@ReportOn+',COST_CENTER_NAME'
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select CostCenter,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided from  #JobReport 
	end

--Computer
if(@ReportOn='JOB_COMPUTER')
	  begin
				set @sql='insert into #JobReport(ComputerName,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
				select JOB_COMPUTER,(sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)) as Total,
				sum(A3BW) as TOTALA3BW,
				sum(A3C) as TOTALA3C,
				sum(A4BW) as TOTALA4BW,
				sum(A4C) as TOTALA4C,

				sum(OtherBW) as TOTALOTHERBW,sum(OtherC) as TOTALOTHERC,sum(ONESIDED) as ''Duplex_One_Side'',
				sum(TWOSIDED)/2 as ''Duplex_Two_Side'' from #TEMP1
				group by  ' + @ReportOn + ''
				exec(@sql)
				--Total Calculation Execute
				exec(@TotalCalculation)
				select ComputerName,Total,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided from  #JobReport 
	end

end
GO
/****** Object:  StoredProcedure [dbo].[QueueForJobRelease]    Script Date: 03/18/2012 02:08:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QueueForJobRelease]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[QueueForJobRelease]
/****** Object:  StoredProcedure [dbo].[QueueForJobRelease]    Script Date: 03/18/2012 02:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[QueueForJobRelease] 
	@serviceName varchar(100)
as

select * into #JobQueue from T_PRINT_JOBS where JOB_RELEASER_ASSIGNED = @serviceName  and JOB_PRINT_RELEASED = 'false' and datediff(ss, REC_DATE, getdate()) > 7
select * from #JobQueue
update T_PRINT_JOBS set JOB_PRINT_RELEASED = 'true' where REC_SYSID in (select REC_SYSID from #JobQueue)

select * from T_PRINT_JOBS where REC_SYSID in (select REC_SYSID from #JobQueue)
GO



/****** Object:  StoredProcedure [dbo].[RemovePermissions]    Script Date: 03/19/2012 12:12:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemovePermissions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RemovePermissions]
GO
/****** Object:  StoredProcedure [dbo].[RemovePermissions]    Script Date: 03/19/2012 12:12:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemovePermissions]
	@GRUPID int,
	@PermissionsAndLimitsOn tinyint
AS
BEGIN
	delete from T_JOB_PERMISSIONS_LIMITS where GRUP_ID=@GRUPID and PERMISSIONS_LIMITS_ON=@PermissionsAndLimitsOn
	if(@PermissionsAndLimitsOn = 0)
		begin
			Update M_COST_CENTERS set ALLOW_OVER_DRAFT='False' where COSTCENTER_ID=@GRUPID
		end	
	else
		begin
			Update M_USERS set ALLOW_OVER_DRAFT='False' where USR_ACCOUNT_ID=@GRUPID
		end
END
GO
/****** Object:  StoredProcedure [dbo].[GetPermissionsAndLimits]    Script Date: 03/21/2012 03:01:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPermissionsAndLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPermissionsAndLimits]
GO
/****** Object:  StoredProcedure [dbo].[GetPermissionsAndLimits]    Script Date: 03/21/2012 03:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetPermissionsAndLimits]
	@GroupId int,
	@LimitsBasedOn int

AS
Begin
	declare @rowsCount int

	select @rowsCount = count(*) from T_JOB_PERMISSIONS_LIMITS where GRUP_ID = @GroupId and PERMISSIONS_LIMITS_ON=@LimitsBasedOn
	if(@rowsCount > 0)
		begin
			select * from T_JOB_PERMISSIONS_LIMITS where GRUP_ID = @GroupId and PERMISSIONS_LIMITS_ON=@LimitsBasedOn			
		end
	else
	Begin
		select * from T_JOB_PERMISSIONS_LIMITS where GRUP_ID = '-1' and PERMISSIONS_LIMITS_ON=@LimitsBasedOn			
	end
End
GO
/****** Object:  Table [dbo].[T_ACCESS_RIGHTS]    Script Date: 03/12/2012 11:08:20 ******/
DELETE FROM [dbo].[T_ACCESS_RIGHTS]
GO
/****** Object:  Table [dbo].[T_ACCESS_RIGHTS]    Script Date: 03/12/2012 11:08:20 ******/
SET IDENTITY_INSERT [dbo].[T_ACCESS_RIGHTS] ON
INSERT [dbo].[T_ACCESS_RIGHTS] ([REC_ID], [ASSIGN_ON], [ASSIGN_TO], [MFP_OR_GROUP_ID], [USER_OR_COSTCENTER_ID], [USR_SOURCE]) VALUES (1, N'MFP Groups', N'Cost Center', N'Default', N'Default', N'DB')
SET IDENTITY_INSERT [dbo].[T_ACCESS_RIGHTS] OFF

-- Localization Script --
truncate table APP_LANGUAGES
-- APP_LANGUAGES
insert into APP_LANGUAGES(APP_CULTURE,APP_NEUTRAL_CULTURE,APP_LANGUAGE,APP_CULTURE_DIR,REC_ACTIVE)values(N'en-US',N'en', N'English (United States)', 'LTR','True')


-- Localized Strings 

truncate table RESX_LABELS

-- Language = en-US
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'Lagnuage Name', N'US English', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CANCEL', N'Cancel', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT', N'Department', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENTS', N'Departments', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_DEPARTMENT', N'Department Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_USERNAME', N'AD Username', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_PASSWORD', N'AD Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTIVE_DIRECTORY', N'Active Directory', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTIVE_DIRECTORY_DETAILS', N'Active Directory Server Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_ACTIVE', N'Enable Department', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE_LOGON', N'Enable Log on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_LOGIN_ENABLED', N'Is Login Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_DETAILS', N'Update Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REFRESH', N'Refresh', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE', N'Update', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ALL', N'Select All', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GENERAL_SETTINGS', N'General Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'POWERED_BY', N'Powered by', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGON_MODE', N'Logon Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGON', N'Login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGONNAME', N'Login Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISPLAYING', N'Displaying', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS', N'Jobs ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_TYPE', N'Job Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_TYPES', N'Job Type(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_COMPUTER', N'Job Computer', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_DATE', N'Job Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_ID', N'Job ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_CONFIGURATION', N'Job Configuration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_MODE', N'Job Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_NAME', N'Job Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_PRICE', N'Job Price', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_LOG', N'Job Log', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_STATUS', N'Job Status', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECTED_USERS', N'Selected Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXCEPTION', N'Exception', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATION_SOURCE', N'Authentication Source', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EDIT', N'Edit', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REQUIRED_FIELD', N'Required fields', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USE_SSO', N'Use SSO', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ROLE', N'User Role', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ID', N'User Id', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_MACHINE', N'User Machine', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_NAME', N'User Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_MANAGEMENT', N'User Management', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REPORTS', N'Reports', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DESCRIPTION', N'Description', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WIDTH', N'Width', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPUTERS', N'Computer(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPUTER_NAME', N'Computer Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CSV_USERS', N'CSV File Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SAMPLE_DATA', N'User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATA_DECODING_RULE', N'Data Decoding Rule (all card types)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PADDING_RULE', N'Data Padding Rule (all card types)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATE', N'Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN', N'Domain', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_NAME', N'Domain name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOBS', N'Print Jobs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_SETTINGS', N'Print Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT', N'Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_DELETE', N'Print & Delete', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_RELEASE', N'AccountingPlus', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_LIST', N'Print List', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTPIN', N'Print PIN', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_POSITION', N'By Position', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_DELIMITER', N'By Delimiter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SINGLE_COLOR', N'Single Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENFORCE_FACILITY_CODE', N'Enforce Facility Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FACILITY_CODE_SETTINGS', N'Facility Code Check Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SETTINGS', N'Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_SETTINGS', N'Print Job Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMAIL', N'Email', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMAIL_ID', N'Email Id', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END', N'End', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END_DELIMITER', N'End Delimiter (E)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESULT', N'Result', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISCOVER_DEVICE', N'Discover', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALLOWED_FORMAT', N'Allowed format (.CSV) ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILTER_ON', N'Filter On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_NEW_DEVICE', N'Add New MFPs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_NEW_USER', N'Add New User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PADDING_RULE_SETTING', N'Padding Rule Setting(Up to three (3) characters can be specified for each padding)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GENERATE_REPORT', N'Generate', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE', N'Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES', N'Devices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_ID', N'Device ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_NAME', N'Device Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL', N'Total', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_SHEET_COUNT', N'Total Sheet Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_RECORDS', N'Total Records', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_COUNT', N'Total Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_ON', N'Group On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUPS', N'Groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_ID', N'Group Id', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_CSV_FILE', N'Click here to upload users from CSV file.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SYNC_LDAP_D', N'Click here to Sync LDAP users.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD', N'Add', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD', N'Upload', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMPORT', N'Import', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IP_ADDRESS', N'IP Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_TYPE', N'Card Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_ID', N'Card ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_CONFIGURATION', N'Card Configuration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CONFIGURATION', N'Configuration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'READ_LENGTH_L', N'Read Length (L)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELETE', N'Delete', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_LOGON_TYPE', N'Manual Login Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_LOGIN', N'Manual Login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_TYPE', N'Manual Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MY_PROFILE', N'My Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MESSAGE', N'Message', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MESSAGE_TYPE', N'Message Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFPS', N'MFP(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_IP_ADDRESS', N'MFP IP Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_MAC_ADDRESS', N'MFP MAC Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_MODE', N'MFP Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'POST_PADDING', N'Post Padding (Z)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NAME', N'Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NEW_DEVICE', N'New Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_PAPER_SIZE', N'Job Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPER_SIZES', N'Paper Size(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD', N'Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN', N'PIN', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUDIT_LOG', N'Audit log', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTER', N'Register', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLOSE', N'Close', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAST_PRINT', N'Fast Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BLACK_WHITE', N'Monochrome', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE', N'Page', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_SIZE', N'Page Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERIAL_NUMBER', N'Serial Number', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SAVE', N'Save', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOCK_DOMAIN', N'Lock Domain', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOCK_DOMAIN_FIELD', N'Lock Domain Field', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE', N'Language', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MASTER_DATA', N'Master Data', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STACKSTRACE', N'Stacks Trace', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START', N'Start', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_POSITION', N'Start Position', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_POSITION_X', N'Start Position (X)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_DELIMITER', N'Start Delimiter (D)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DAYS', N'Days', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'THEMES', N'Themes', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TO', N'To', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELIMITER', N'Delimiter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'URL', N'URL', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'VERSION', N'Version', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FULL_COLOR', N'Full Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPLETE', N'Complete', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FROM', N'From', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRE_PADDING', N'Pre Padding (A)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PREVIEW', N'Preview', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PREVIEW_USERS', N'Preview Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_DEVICE', N'Select External Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATING_PLEASE_WAIT', N'Updating please wait', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TIME', N'Time', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACK', N'Back', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2_COLOR', N'2 Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'100_SHEET_LOWER_UPPER', N'100 Sheet Staple Lower Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'100_SHEET_MIDDLE_UPPER', N'100 Sheet Staple Middle Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'100_SHEET_STAPLE_UPPER', N'100 Sheet Staple Upper Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A3', N'A3', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A4', N'A4', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTION', N'Action', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTIVEDIRECTORY_SETTINGS', N'Active Directory settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_FULL_NAME_ATTRIBUTE', N'AD full name attribute', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_FULLNAME_FIELD', N'AD Full Name Field', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_PORT', N'AD port', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_LANGUAGE', N'Add Language', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_NEW_MFP', N'Add New MFPs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGON_MAX_RETRY_COUNT', N'Allowed retries for user login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ANONYMOUS_USER_PRINTING', N'Anonymous user printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLICATION_UI', N'Application  UI', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLICATION_NAME', N'Application Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLICATION_REGISTRATION', N'Registration & Activation', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLICATION_URL', N'Application Url', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATION_SETTINGS', N'Authentication settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO', N'Auto', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BOOK', N'Book', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_ADMINISTRATOR_ONLY', N'By Administrator only', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_IP_ADDRESS', N'By IP Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_IP_RANGE', N'By IP Range', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CANCELLED', N'Cancelled', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_DATA_CONFIGURATION', N'Card Data configuration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_LOGON', N'Card Log on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_READER_TYPE', N'Card Reader Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CENTER_TRAY', N'Center Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLEAR', N'Clear', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_EXPORT_CSV', N'Click here to export the job usage details to CSV', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXPORT_JOBDETAILS_EXCEL', N'Click here to export the job usage details to MS Excel', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_EXPORT_XML', N'Click here to export the job usage details to XML', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_HERE_REGISTER', N'Click here to register', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLIENT_MESSAGES', N'Client Messages ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLLATE', N'Collate', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLOR', N'Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLOR_MODE', N'Color Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLUMN_MAPPING', N'Column Mapping', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPUTERNAME', N'Computer Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CONTACT_INFO', N'Contact Info', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CONTINUE', N'Continue', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPIES', N'Copies', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPY', N'Copy', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD_ALIAS', N'Create password alias', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CSV_TEMPLATE_D', N'CSV Template', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CUSTOM_MESSAGES', N'Custom Messages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATABASE', N'Database ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATE_TIME_HEADER', N'Date/Time', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DAY_LEFT', N'day(s) left', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DISABLED', N'Device disabled. Please contact Administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_IS_NOT_REGISTERED', N'Device is not Registered to AccountingPlus Application. Please Register.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_IN_SUBNET', N'Devices in Subnet', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DIRECT_MANUAL_ENTRY', N'Direct manual entry', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISABLE', N'Disable', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISPLAY_JOB_LIST', N'Display job list', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DO_YOU_WANT_TO_DELETE_DATA', N'Do you want to delete data?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOCUMENTS_FOR', N'Documents for', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_ADMINISTRATOR', N'Domain Administrator', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_CONTROLLER', N'Domain controller', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX', N'Duplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX_DIR', N'Duplex Dir', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX_MODE', N'Duplex Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX_ONE_SIDED', N'Simplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX_TWO_SIDED', N'Duplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_CARD_IDS_COUNT', N'Duplicate Card IDs count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_PIN_IDS_COUNT', N'Duplicate PIN IDs count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_RECORD_COUNT', N'Duplicate record(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_RECORDS_FOUND', N'Duplicate records found  ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_USERS_COUNT', N'Duplicate User Name count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EDIT_USER_DETAILS', N'Edit user details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_CARDID_COUNT', N'Empty CardID(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_PASSWORD_COUNT', N'Empty Password(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_PINID_COUNT', N'Empty PIN ID(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_USERNAME_COUNT', N'Empty UserFullName(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_USERID_COUNT', N'Empty UserID(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE', N'Enable', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE_DEVICE', N'Enable Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END_IP', N'End IP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_LOGIN_DETAILS_TO_REGISTER_USER', N'Enter login details to register user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ERROR', N'Error', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ERROR_MESSAGE', N'Error Message', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ERROR_SOURCE', N'Error source', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_ACCOUNTING_CONTROL', N'External Accounting Control EAM', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_ACCOUNTING_UI', N'External Accounting UI', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_ACCOUNTING_WEBSERVICE', N'External Accounting Web Service', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FACILITY_CODE_CHECK', N'Facility Code Check', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FACILITY_CODE_CHECK_VALUE', N'Facility code check value', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILING', N'Filing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILTER_BY', N'Filter by', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIND_DEVICES_DUMMY', N'Find Devices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FINISHED', N'Finished', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FINISHER_LOWER_TRAY', N'Finisher Lower Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FINISHER_UPPER_TRAY', N'Finisher Upper Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FINISHING', N'Finishing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIRST_TIME_USE', N'First Time Use', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIT_TO_PAGE', N'Fit to Page', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FOLDER', N'Folder', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FROM_DATE', N'From Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'HIDE_SETTING', N'Hide Setting', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'HOLD', N'Hold', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'HOLD_AFTER_PRINT', N'Hold after Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'HOME', N'Home', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ID', N'ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IGNORE_DUPLICATES_AND_SAVE', N'Ignore duplicates and save.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMPORT_USERS', N'Import Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMPORTING_WILL_IGNORE_ALL_DUPLICATE_RECORDS', N'Importing will ignore all duplicate records.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INSTALLATION_DATE', N'Installation Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_CARDS', N'Invalid Cards', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_DETAILS', N'Invalid Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_EMAIL_ID_COUNT', N'Invalid Email ID count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LICENSE_ERROR', N'Invalid license error', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_PINS', N'Invalid PINs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_DEVICE_ENABLED', N'Is Device Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_LANGUAGE_ENABLED', N'Is Language Enabled', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_RETENTION', N'Job Retention', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_SETTINGS', N'Job Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_ID', N'Language ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LEDGER', N'Ledger', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LEGAL', N'Legal', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LENGTH', N'Length', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LETTER', N'Letter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LICENSE_ID', N'License ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOCAL_DATABASE_MANAGEMENT', N'Local database management', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOG_OUT', N'Log out', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGIN_DETAILS', N'Login details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGIN_FOR_ALL_FUNCTIONS', N'Login for all functions', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGIN_FOR_PRINT_RELEASE_ONLY', N'Login for AccountingPlus only ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGOUT', N'Logout', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAIN', N'Main', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LINK_MANAGE_LANGUAGE', N'Manage Language', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_LOGON', N'Manual Log on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_ERROR_DETAILS', N'MFP error details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_UI_LANGUAGE', N'MFP UI Language', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_USER_AUTHENTICATION', N'MFP user authentication', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MODEL_NAME', N'Model Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MODELNAME', N'Model Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MODULE', N'Module', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MONOCHROME', N'Monochrome', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NOT_APPLICABLE', N'N/A', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NAVIGATE_TO_MFP_MODE', N'Navigate to MFP Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NETWORK_PASSWORD', N'Remember Network Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO', N'No', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NONE', N'None', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NONNUMERIC_PINID_COUNT', N'Nonnumeric PIN ID(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NOTES', N'Notes', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_OF_LICENSE', N'Number of Licenses', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OFF_SET', N'Off Set', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OK', N'OK', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ON_NO_JOBS', N'On no jobs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ORIENTATION', N'Orientation', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OTHER', N'Other', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OUTPUT_TRAY', N'Output Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_IS_LOADING_PLEASE_WAIT', N'Page is Loading. Please wait.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPER_SIZE', N'Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD_OR_PIN', N'Password/PIN', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN_LOGON', N'Pin Log on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_COMPLETE_CARD_ENROLMENT', N'Please complete card enrollment.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_CONTACT_ADMINISTRATOR', N'Please contact AccountingPlusWeb Administrator', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_DEPLOY_VALID_LICENSE', N'Please deploy the valid license file and try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DELETE', N'Please select an', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_MOVE', N'Please select an option to move', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_WAIT', N'Please wait…', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_&_RETAIN_BUTTON_STATUS', N'Print & Retain button status', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTRELEASE_ABOUT', N'About', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_RELEASE_APPLICATION_ERROR_DETAILS', N'AccountingPlus Application error details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_FIRST_LAUNCH', N'AccountingPlus first launched on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ERROR_DETAILS', N'AccountingPlus MFP Application error details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTER_MODE', N'Printer Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTING_IN_PROGRESS', N'Printing in progress', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROOF', N'Proof', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PUNCH', N'Punch', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'QUEUED', N'Queued', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'QUICK', N'Quick', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'READY', N'Ready', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGIESTER_NOW', N'Register Now', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTEREDLICENCESE', N'Registered Licences : Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTRATION_DETAILS', N'Registration Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REQUEST_CODE', N'Request Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESPONSE_CODE', N'Response Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETENTION', N'Retention', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETRIVED_GROUPS_COUNT', N'Retrieved groups count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETURN_TO_JOB_LIST', N'Return to  Job list', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETURN_TO_PRINT_JOB_LIST', N'Return to print job list', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RIGHT_TRAY', N'Right Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SNO', N'S.No.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SADDLE_LOWER_TRAY', N'Saddle-Lower Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SADDLE_MIDDLE_TRAY', N'Saddle-Middle Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SADDLE_UPPER_TRAY', N'Saddle-Upper Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SCAN', N'Scan', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SECURE_SWIPE', N'Secure Swipe', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PASSWORD_FOR_FUTURE_LOGINS', N'Select password for future logins.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PRINT_OPTIONS', N'Select Print options', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELF_REGISTER_DEVICE', N'Self register device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELF_REGISTRATION', N'Self Registration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERIALNUMBER', N'Serial Number', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVER_MESSAGES', N'Server Messages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SETTING', N'Setting', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CONTACT_DETAILS', N'SHARP SOFTWARE DEVELOPMENT INDIA PVT. LTD', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SHOW_SETTING', N'Show Setting', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SIMPLEX', N'Simplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SKIP_PRINT_SETTINGS', N'Skip print settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SORT', N'Sort', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STAPLE', N'Staple', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_IP', N'Start IP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STARTED', N'Started', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STATUS', N'Status', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUCCESS', N'Success', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUGGESTION', N'Suggestion', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUPPORT_FOR_DUPLEX', N'Support for duplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_GO', N'Swipe and Go', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_YOUR_ID_CARD_TO_LOGIN', N'Swipe your ID card to login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TABLET', N'Tablet', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TO_DATE', N'To Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTALBW', N'Total BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTALCOLOR', N'Total Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRACE', N'Trace', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRIAL_DAYS', N'Trial Days', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRIAL_LICENCES', N'Trial Licences : Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRIAL_VERSION', N'Trial Version', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TYPE', N'Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNLIMITED', N'unlimited', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_MFP', N'Update MFP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADED_RECORD_COUNT', N'Uploaded record(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USE_PIN', N'Use PIN', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USE_WINDOWS_PASSWORD', N'Use Windows Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERID', N'User Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERNAME', N'User Full Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_NOT_FOUND', N'User not found', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_WANT_REGISTER', N'User not found. Do you want to register?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_PROVISIONING', N'User provisioning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_SOURCE', N'User Source', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS_FROM_CSV_OR_XML_COLUMNS', N'Users From CSV  columns', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS_FROM_DATABASE_COLUMNS', N'Users From Database columns', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS_SOURCE', N'Users Source', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'VALID_RECORDS_COUNT', N'Valid records count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'VALUE', N'Value', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WARNING', N'Warning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GRAND_TOTAL', N'Grand Total', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'Go', N'Go', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_DOMAIN', N'Invalid Domain Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOCATION', N'Location', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATION_SERVER', N'Authentication Server', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_USING', N'Print Using ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_DETAILS', N'FTP Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_PROTOCOL', N'FTP Protocol', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_ADDRESS', N'FTP Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_PORT', N'FTP Port', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTRELEASE_API', N'AccountingPlus API', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LENGTH_CARDID', N'Invalid Length CardID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LENGTH_PINID', N'Invalid Length PinID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_COMPLETE_REGISTRATION', N'Please complete registration data…', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_FULL_NAME', N'User Full Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'YES', N'Yes', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_ACCESS', N'Print Job Access', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUBMIT', N'Submit', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UI_DIRECTION', N'Language Direction', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_NEWUSER', N'Add New User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_USER', N'Update User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISPLAY_NAME', N'Display name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP', N'Group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'1_STAPLE', N'1 Staple', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2_STAPLE', N'2 Staple', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNLOCK_USERS', N'Enable User(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'XLS', N'XLS', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CSV', N'Click here to export to CSV', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'XML', N'XML', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNDO', N'UNDO', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'XML_TEMPLATE', N'XML AccountingPlus Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_JOBS_FOUND_REDIRECT', N'No job (s) found you will be redirected to MFP Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_OPTIONS', N'Options', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_DEPARTMENT_ENABLED', N'Is Department Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EAM', N'Is Job Log Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACM', N'Is ACM Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_MODEL', N'MFP Model', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REQUIRED_JOBLOG', N'Required for Job Log', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_BACK', N'Click here to go back', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_SAVE', N'Click here to save/update', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_RESET', N'Click here to reset', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_SETTINGS', N'Card Configuration Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBCONFIGURATION_SETTINGS', N'Job Configuration Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIND_DEVICES', N'Discover Devices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_TYPE', N'Print Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIRST_LOGON', N'First Login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'1', N'1', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2', N'2', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'3', N'3', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'4', N'4', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'5', N'5', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'6', N'6', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'7', N'7', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'8', N'8', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'9', N'9', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_USERS', N'Domain Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGIN_FOR_MX-PRINT_ONLY', N'Login for AccountingPlus only', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEFAULT', N'Default', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT', N'Select', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADMIN', N'Admin', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER', N'User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SINGLE_SIDE', N'Single side', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2_SIDED_BOOK', N'2 Sided (Book)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2_SIDED_TABLET', N'2 Sided (Tablet)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL', N'Manual', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD   ', N'Card          ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROXIMITY_CARD', N'Proximity', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAGENTIC_STRIPE', N'Magentic Stripe', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BARCODE_READER', N'Barcode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_AND_GO', N'Swipe and Go', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERNAME_PASSWORD', N'UserName/Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EAM_AND_ACM', N'EAM and ACM', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EAM_ONLY', N'EAM Only', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACM_ONLY    ', N'ACM Only          ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_PRINT', N'FTP Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OSA_PRINT', N'OSA Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP', N'FTP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTPS', N'FTPS              ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_USERNAME_PASSWORD', N'Manual(Username/Password)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_PIN', N'Manual(Pin)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_SECURE_SWIPE', N'Card(Secure Swipe)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_SWIPE_ANDGO', N'Card(Swipe and Go)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROXIMITY', N'Proximity', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAGNETIC_STRIPE', N'Magnetic Stripe', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BARCODE', N'Barcode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL                ', N'All                                 ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INFORMATION', N'Information', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CRITICALERROR', N'CriticalError', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CRITICALWARNING', N'CriticalWarning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS', N'Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPUTER', N'Computer', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CN', N'cn', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LEFT_RIGHT', N'Left to Right', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RIGHT_LEFT', N'Right to Left', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESX_CLIENT_MESSAGES', N'Client Messages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESX_SERVER_MESSAGES', N'Server Messages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESX_LABELS', N'Labels', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESET', N'Reset', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTIVE_DOMAIN', N'Active Directory / Domain Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SYNC_LDAP', N'Click here to Import LDAP users.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISABLE_USER', N'Disable User(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CSV_TEMPLATE', N'CSV AccountingPlus Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADFILE_DUPLICATE_DETAILS', N'Upload file duplicate records details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADFILE_DUPLICATE_USERID', N'Upload file duplicate UserID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADFILE_DUPLICATE_CARDID', N'Upload file duplicate CardID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADFILE_DUPLICATE_PINID', N'Upload file duplicate PinID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_LOGIN_TYPE', N'Card Login type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISABLE_LANGIAGES', N'Disable Language(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE_LANGUAGES', N'Enable Language(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISABLE_DEVICES', N'Disable Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE_DEVICES', N'Enable Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_GROUP', N'Group Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_ACTIVE', N'Enable Group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_GROUP_ENABLED', N'Is Group Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS', N'Permissions', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_USER', N'Domain Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_USED', N'Job Used', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_LIMIT', N'Page Limit', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_PROFILE', N'Price Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNIT_PRICE', N'Unit Price', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USR_GROUP', N'User Group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_PROFILE', N'Cost Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_GROUPS', N'MFP Groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_CENTER', N'Cost Center', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPER_SIZE_CATEGORY', N'Paper Size Category', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_PAPER_SIZE', N'Paper Size Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPERSIZE_ACTIVE', N'Paper Size Enabled', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_PAPER_SIZE_ENABLED', N'Is Paper Size Enabled ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_CATEGORY', N'Job Category', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICES', N'Prices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS', N'Limits', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL', N'Auto Refill', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_SETTINGS', N'Auto Refill Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REFILL_TYPE', N'Refill Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTOMATIC', N'Automatic', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_TO_EXISTING_LIMITS', N'Add to Existing Limits', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_ON', N'Auto Refill On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EVERY_DAY', N'Every Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EVERY_WEEK', N'Every Week', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EVERY_MONTH', N'Every Month', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_TIME', N'Auto Refill Time', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_WEEK', N'Auto Refill Week', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_DATE', N'Auto Refill Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MONDAY', N'Monday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TUESDAY', N'Tuesday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WEDNESDAY', N'Wednesday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'THURSDAY', N'Thursday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FRIDAY', N'Friday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SATURDAY', N'Saturday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUNDAY', N'Sunday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SET_PERMISSIONS_LIMITS', N'Set Permissions and Limits', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CATEGORY', N'Category', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_COST_CENTER', N'Select Cost Center', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_AVAILABLE', N'Limits Available', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OVER_DRAFT', N'Over Draft', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_COLOR', N'Print Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_MONOCHROME', N'Print BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SCAN_COLOR', N'Scan Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SCAN_MONOCHROME', N'Scan BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPY_COLOR', N'Copy Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPY_MONOCHROME', N'Copy BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN', N'Assign', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP', N'MFP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFPSS', N'MFPs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_CENTER_NAME', N'Cost Center Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_COST_CENTER_ENABLED', N'Is Cost Center Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_MFP_ENABLED', N'IS MFP Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_LIMITS_ON', N'Permissions and Limits On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELTE_SERVER_BACKUP', N'Click here to Delete server backup', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_TO_BACKUP', N'Click here to backup server', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_TO_RESTORE', N'Click here to restore server', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATABASE_NAME', N'DataBase Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_SIZE', N'Backup size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_DATE', N'Backup date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVER_NAME', N'Server Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_ON', N'Limits On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALLOW_OVER_DRAFT', N'Allow Over Draft', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALLOWED_OVER_DRAFT', N'Allowed Over Draft', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALERT_LIMIT', N'Alert Limit', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_ON', N'Permissions On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_MFPGROUP_COSTPROFILE', N'Assgin MFP Groups to Cost Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXECUTIVE_SUMMARY', N'Executive Summary', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CURRENT_VOLUMES', N'Current Volumes', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_DAYS', N'Total Number of Days', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_JOBS', N'Total Number of Jobs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_PAGES', N'Total Number of Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_USERS', N'Total Number of Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_BW_PAGES', N'Total Number of B&W Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_COLOR_PAGES', N'Total Number of Color Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CURRENT_ASSETS', N'Current Assets', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_DEVICES', N'Total Number of Devices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_WORKSTATION', N'Total Number of Workstation', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CURRENT_COSTS', N'Current Costs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PERPAGE', N'Average Cost Per Page', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PERUSER', N'Average Cost Per User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PERPRINTER', N'Average Cost Per Printer', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_BW_PRINTING', N'Cost of BW Printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_COLOR_PRINTING', N'Cost of Color Printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_COST_PRINTING', N'Total Cost of Printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTRAPOLATED_VALUES', N'Extrapolated Values', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PERUSER_PERDAY', N'Average Cost Per User Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PER_PRINTER_PERDAY', N'Average Cost Per Printer Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COLORPAGE_PERDAY', N'Average Color Pages Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_BWPAGE_PERDAY', N'Average BW Pages Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_TOTAL_PAGESPERDAY', N'Average Total Pages Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROJECTIONS', N'Projections', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_PER_MONTH', N'Cost Per Month', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_PER_QUARTER', N'Cost Per Quarter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTS_1YEAR', N'Costs (1 Year)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTS_3YEAR', N'Costs (3 Year)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGES_PER_MONTH', N'Pages Per month', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGES_PER_QUARTER', N'Pages Per Quarter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGES_PERYEAR', N'Pages Per (1 Year)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGES_PER3YEAR', N'Pages Per (3 Year)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_RANGE', N'Page Range', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_PAGES', N'Total Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERCENTAGE', N'Percentage', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTER', N'Printer', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENDDATE_NOT_GREATERTHAN_TODAYDATE', N'End date cannot be greater than today''s date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_DATA_FOR_DATE', N'No data found for the selected date range', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_PRINT_BREAKDOWN', N'Total Volume Breakdown - Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_PRINT_BREAKDOWN_JOBS', N'Total Volume Breakdown - Jobs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WEEKDAY_REPORT', N'WeekDay Report', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_10_PRINTERS', N'Top 10 Printers', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_10_USERS', N'Top 10 Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_USERS_BY_COLOR', N'Top Users by Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_USERS_BY_BW', N'Top Users by Black and White', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_PRINTER_BY_BW', N'Top Printers by Black and White', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_PRINTER_BY_COLOR', N'Top Printers by Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_CENTERS', N'Cost Centers', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLOR_UNITS', N'Color (price)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MONOCHROME_UNITS', N'Monochrome (Price)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACCESS_RIGHTS', N'Access Rights', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_MFP_TOGROUP', N'Assign MFP to Group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_TO_COSTPROFILE', N'Assign To Cost Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GRAPHICAL_REPORT', N'Graphical Report', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVOICE_REPORT', N'Invoice', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_RESTORE', N'Backup & Restore', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LICENCES', N'Registration & Activation', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLIENT_CODE', N'Client Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACC_INSTALLED_ON', N'AccountingPlus Installed On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERIAL_KEY', N'Serial Key', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_REGISTERED_LICENCES', N'Total Registered Licences', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTRATION_ADDLICENCES', N'Registration/Add Licences', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SYS_INFORMATION', N'System Information', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_INFORMATION', N'User Information', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PHONE_MOBILE', N'Phone/Mobile Number', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPANY', N'Company', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADDRESS', N'Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CITY', N'City', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STATE', N'State', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COUNTRY', N'Country', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ZIP_CODE', N'Zip Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTRATION_INFORMATION', N'Registration Information', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACC_REGISTRATION_SUCCESS', N'AccountingPlus Registered successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACCOUNTING_INFO_MENU', N'Accounting Info : Menu', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STANDARD_APPLICATION_CONTROL', N'Standard Application Setting', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADDRESS_FOR_APPLICATION_UI', N'Address for Application UI ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADDRESS_FOR_WEB_SERVICE', N'Address for Web Service', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A3BW', N'A3-BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A3C', N'A3-Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A4BW', N'A4-BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A4C', N'A4-Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OtherBW', N'Other-BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OtherC', N'Other-Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER', N'CostCenter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_ACTIVE', N'Enable Cost Center', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_USERS_TO_COSTCENTERS', N'Assign users to Cost Centers', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_MFP_TO_COSTPROFILE', N'Assign MFPs to Cost Profiles', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PREFERED_COST_CENTER', N'Preferred Cost Center for Printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLY_TO_ALL', N'Apply To ALL', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CUSTOM_THEME', N'Custom Thems', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_IMAGE', N'Select image type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_CUSTOMIMAGE', N'Upload custom image', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WALLPAPER', N'WallPaper', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAXIMUMSIZE_EXCEEDED', N'Uploaded file exceeded maximum size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAXIMUM_FILESIZE', N'Maximum allowed  file size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACC_DEVICEREGISTRATION_SUCCESS', N'Device registered successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_ACCESS', N'Job Access', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WALLPAPER_RECOMMENDED _HEIGHT', N'Recommended  height', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WALLPAPER_RECOMMENDED_WIDTH', N'Recommended  width', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SMTP_PORT', N'SMTP Port', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SMTP_HOST', N'SMTP Host', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FROM_ADDRESS', N'From Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CC_ADDRESS', N'CC Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BCC_ADDRESS', N'Bcc Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SMTP_SETTINGS', N'SMTP Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ANONYMOUS_DIRECT_MFPPRINT', N'Anonymous Direct Print to MFP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATE_DIRECTPRINT', N'Authenticate User For Direct Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MONOCHROME_COUNT', N'Monochrome (Units)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLOR_COUNT', N'Color (Units)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_PRICE', N'Total (Price)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_UNITS', N'Total (Units)', getdate(), getdate(), 'Build', 'Build')
-- Localized Strings 

truncate table RESX_CLIENT_MESSAGES

-- Language = en-US
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'Lagnuage Name', N'US English', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_FIELD_REQUIRED', N'Domain field cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD_REQUIRED', N'Password cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERID_REQUIRED', N'User ID cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONE_USER', N'Please select one user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONLY_ONE_USER', N'Please select only one user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONE_MFP', N'Please select one MFP.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALID_IP', N'Please enter Valid IP.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_NUMERICS', N'Enter numerics only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONEDEPARTMENT', N'Select only one Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DEPARTMENT', N'Select Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_NAME_EMPTY', N'Department name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DESCRIPTION_EMPTY', N'Description cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBLIST_SELECTONE', N'Please select one Job.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANUAGE_SELECT_ONLY_ONE', N'Please select only one Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_SELECT', N'Please select  Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DPR', N'Please check Data Decoding rule or uncheck Data Padding rule', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELETE_CONFIRMATION', N'Selected user(s) will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_DELETE_CONFIRMATION', N'All the details regarding Departments will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATE_FROM_TO_VALIDATION', N'Please ensure that End date is greater than or equal to Start date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DELETE_CONFIRM', N'Selected MFP(s) will be removed.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_RECORDS_LOG_CLEAR', N'All the records from the Log will be cleared. 
Do you want to continue ?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_DELETE_CONFIRM', N'All the details regarding selected Language will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_DELETE_CONFIRM', N'All the details regarding Departments will be deleted. 
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTJOBS_DELETE_CONFIRM', N'Selected Print Job(s) will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_CREATE_WEBSERVICEOBJECT', N'Failed to create web service object.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILE_DELETE_CONFIRM', N'Do you want to delete selected files?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELETED_FILES_DELETE', N'Please selected the files to be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PAGE_NUMBER', N'Please enter valid page number.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WARNING', N'Warning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_COUNT_LESS_THAN', N'Page count should be less than total Pages Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_DELETE_UNLOCK', N'Please select user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DELETE', N'Please select MFP(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNDO', N'Undo', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_SETTINGS', N'Invalid Settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONEGROUP', N'Select only one Group.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_GROUP', N'Select Group.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_NAME_EMPTY', N'Group name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_DELETE_CONFIRMATION', N'All the details regardingGroups will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONEPAPERSIZE', N'Select one Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PAPERSIZE', N'Select Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPERSIZE_NAME_EMPTY', N'Paper Size Name Empty', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_DELETE_CONFIRMATION', N'Do you want to delete selected Paper Size(s)?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONE_BACKUP', N'Please select one Backup', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESTORE_CONFIRM', N'Are you sure you want to Restore selected Backup?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELETE_CONFIRM', N'Are you sure you want to Delete selected Backup?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECTED_PRICE_PROFILE_CONFIRM', N'From now,selected price profile will be applicable.Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_CONFIRMATION', N'All the details regarding Cost Center will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONECOSTCENTER', N'Select only one Cost Center.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_COSTCENTER', N'Select Cost Center.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_NAME_EMPTY', N'Cost Center name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLY_PRICE_FOR_ALL', N'Do you want to Apply the price for all the Job Types?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONE_MFPGROUP', N'Please select one MFP Group.', getdate(), getdate(), 'Build', 'Build')
-- Localized Strings 

truncate table RESX_SERVER_MESSAGES

-- Language = en-US
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'Lagnuage Name', N'US English', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_DELETE_SUCCESS', N'User(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_DELETE_FAIL', N'Failed to delete user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_DELETE_SUCCESS', N'MFP(s) deleted Successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_DELETE_FAIL', N'Failed to delete MFP(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_DISCOVERY_SUCCESS', N'Device(s) discovered successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_DISCOVERY_FAIL', N'Discovery operation failed.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_USER_ROLE', N'Select user role.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_CONFIGURED_ANOTHER_USER', N'This card is already configured to another user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_UPDATE_FAIL', N'Failed to update user details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_UPDATE_SUCCESS', N'User details updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERID_ALREADY_EXIST', N'User name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN_ALREADY_USED', N'This PIN is already used by another user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ADD_SUCCESS', N'User details added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ADD_FAIL', N'Failed to add user details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_LOGON_TYPE', N'Please select a Logon type.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_AUTH_SOURCE', N'Please select an Authentication source.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_SINGLE_SIGN_ON', N'Please select single Sign on.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_CARD_TYPE', N'Please select a Card type.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_MANUAL_AUTH_TYPE', N'Please select Manual Authentication type.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_ALREADY_EXISTS', N'This MFP already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_ADD_SUCCESS', N'MFP details added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_ADD_FAIL', N'Failed to add MFP details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNABLE_TO_CONNECT', N'Unable to connect to AD server.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LDAP_SAVE_SUCCESS', N'LDAP Users saved successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LDAP_SAVE_FAIL', N'Failed to save LDAP user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUDIT_CLEAR_SUCCESS', N'Audit Log cleared successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUDIT_CLEAR_FAIL', N'Failed to clear Audit log.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_REG_ERROR', N'Invalid User.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_PROFILE_UPDATE_SUCCESS', N'User profile updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_UPDATE_SUCESS', N'Card configuration updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_UPDATE_FAIL', N'Failed to update Card settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_FILE', N'Please select the file to upload.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERNAME_EMPTY', N'User Name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGINNAME_EMPTY', N'Login Name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INSERT_SUCESS', N'User(s) inserted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INSERT_FAILED', N'Failed to insert users.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_CSV_XML', N'Please upload CSV file only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPTNAME_EXISTS', N'Department name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_SUCESS', N'Department added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_FAIL', N'Failed to add Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_DELETE_SUCESS', N'Department(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_DELETE_FAIL', N'Failed to delete Departments(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DEPARTMENT_FAIL', N'Selected Departments(s) cannot be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_UPDATE_SUCESS', N'Department updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_UPDATE_FAIL', N'Failed to update Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBSETT_SUCESS', N'Settings updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVICE_NOTRESPONDING', N'"PrintDataProvider" service is not responding, please start the service.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS_DELETED_SUCESS', N'Jobs(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS_DELETED_FAIL', N'Failed to delete Jobs(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGON_MAX_RETRY_COUNT', N'Allowed retries for user login.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ANONYMOUS_USER_PRINTING', N'Anonymous user printing.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATION_SETTINGS', N'Authentication Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DB_AUTHENTICATION', N'Data Base Authentication', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETURN_TO_JOB_LIST', N'Return to Print Job List', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_PROVISIONING', N'User Provisioning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SETTNG_UPDATE_SUCESS', N'Settings updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SETTNG_UPDATE_FAIL', N'Failed to update settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_LOGIN_ERROR', N'Failed to authenticate user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NORECORDS_EXISTS', N'Records not found.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ROOTFILE_MISSING', N'Root element is missing.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_CARD_ENTER_PASSWORD', N'Swipe your ID card and enter password to login.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_CARD', N'Swipe your ID card to login.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USER_DATA', N'Please enter your login data.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USER_PIN', N'Please enter user PIN number to login.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROVIDE_USER_DETAILS', N'Please provide user details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USER_PASSWORD', N'Please enter user password.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACCOUNT_DISABLED', N'User Account is disabled. Please consult your administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_CARD_ID', N'Invalid ID card. Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_PASSWORD', N'Invalid password. Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_NOT_FOUND', N'User not found. Please enter valid user Name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN_INFO_NOT_FOUND', N'PIN information not found.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXCEEDED_MAXIMUM_LOGIN', N'You have exceeded the maximum number of login attempts. Please consult your administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_PIN', N'Invalid PIN number. Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_SINGLE_JOB', N'Please select single job to view settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECTED_JOBS_TO_BE_DELETED', N'Please select job(s) to be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS_DELETE_SUCCEESS', N'Selected job(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS_DELETE_FAIL', N'Failed to delete selected jobs.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RELEASED_ON_DEVICE', N'Please select job(s) released on this device.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MORE_THAN_5_SELECTED', N'More than 5 jobs selected. A maximum 5 jobs can be released at a time.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_NOT_REGIESTER', N'Device is not registered to AccountingPlus application. Please register.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_INFO_NOT_FOUND_REGISTER', N'User information not found. Would you like to register now?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_CARD_ID', N'Please enter Card ID.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USERNAME', N'Please enter User name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_PASSWORD', N'Please enter Password.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_NAME_ALREADY_EXISTS', N'User with same name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_USER_DETAILS', N'Invalid User details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_USER_PIN', N'Invalid User PIN.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN_MINIMUM', N'PIN must be 4-10 digits.  Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_REGISTER', N'Failed to register.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_RESET_SUCCESS_DC', N'User(s) unlocked successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_RESET_FAIL', N'Failed to unlock user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_ACCOUNTING_LOGIN_DETAILS', N'External Accounting Login Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROVIDE_LOGIN_DETAILS', N'Please provide login details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIRST_TIME_USER_LOGIN', N'First Time User Login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_INFO_NOT_FOUND', N'Card information not found. Would you like to register now?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_INFO_NOT_FOUND_CONSULT_ADMIN', N'Card information not found. Please check with your administrator or try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_INFO_NOT_FOUND', N'User information not found.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVICE_RESPONDING', N'PrintDataProvider service responding properly.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_NOT_FOUND', N'Card information not found.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_DOMAIN', N'Please enter Domain name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_NAME_EMPTY', N'Department name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNSELECT_LOGIN_USER', N'Unselect logged in user and delete.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_FIELD_REQUIRED', N'Domain field cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD_REQUIRED', N'Password cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DESCRIPTION_EMPTY', N'Description cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REQUIRED_LICENCE', N'Field cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGULAREXPRESSION_NUMERICS', N'Only numerics are allowed.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_DATE_GREATER', N'Start date cannot be greater than Today''s date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END_DATE_GREATER', N'End date cannot be greater than Today''s date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARDID_EMPTY', N'CardID name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PINID_EMPTY', N'Print PIN name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_AD_USERNAME', N'Enter AD user name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_AD_USER_PASSWORD', N'Enter AD user password.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_AD_PORT', N'Enter AD port.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_SETTING_UPDATE_SUCCESS', N'Active Directory settings updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_SETTING_UPDATE_FAILED', N'Failed to update Active Directory settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REPORT_TO_ADMIN', N'Report to administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_LOG_ON_MODE', N'Please select Logon mode.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_CARD_READER_TYPE', N'Please select Card Reader type.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_LOGIN_NAME', N'Please enter User name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALDI_PIN', N'Please enter valid Print PIN.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELF_REGISTER_DEVICE', N'Self Register Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATA_ADDED_SUCCESSFULLY', N'Data added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATA_UPDATED_SUCCESSFULLY', N'Data updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_ADD_DATA', N'Failed to add data.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_UPDATE_DATA', N'Failed to update data.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATA_DELETED_SUCCESSFULLY', N'Data deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_ADD_SUCCESS', N'Language added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_ADD_FAIL', N'Failed to add Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_UPDATE_SUCCESS', N'Language updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_UPDATE_FAIL', N'Failed to update Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USER_LOGIN_NAME', N'Enter Login name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_USER_CREDENTIALS', N'Invalid user credentials.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_DOMAIN_CREDENTIALS', N'Invalid domain credentials.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARDID_ALREADY_USED', N'This CardID is already used by another user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DISABLED_CONTACT_ADMIN', N'Access to AccountingPlus application is disabled to this device. Please consult Administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LICENCE', N'Invalid Licence file. Please consult Administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_LOGIN_DISABLE_ERROR', N'User Disabled. Please consult Administrator for activation.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAXIMUM_RECORDS', N'At a time maximum of 10,000 records should be imported.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALID_EMAIL', N'Enter valid e-mail id.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALID_NAME_ALPHA', N'Enter Name as alphanumeric only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DEPARTMENT', N'Select Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_USER_AUTHENTICATION', N'MFP User Authentication', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DOES_NOT_SUPPORT_DRIVER', N'Job cannot be printed on this device. Please select another device or re-submit job using a PCL driver.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DOES_NOT_SUPPORT_COLOR', N'Job will printed in B&W. Press ''Continue'' to print or ''Cancel'' to cancel.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_DRIVER_JOBS_CANNOT_PRINT', N'All selected jobs cannot be printed on this device. Press ''Continue'' to print supported jobs or ''Cancel'' to cancel.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_CAN_NOT_BE_PROCESSED', N'Your job can not be processed at this time. Please press OK and consult the administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_SUCCESS', N'Print job(s) successfully submitted to device.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_DRIVER_NOT_SUPPORTED', N'Job cannot be printed on this device.
Please select another device or resubmit job using a PCL driver.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_ERROR', N'Your print job cannot be processed at this time. 
Please consult your administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_INVALID_SETTINGS', N'Some of the selected print settings are incompatible. 
Please check settings and try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_PAPER_SIZE_NOT_SUPPORTED', N'Paper size is not available on this device. 
Press continue to select an alternate paper size for your job.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_LICENCES_ARE_IN_USE', N'All license(s) are currently in use.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRIAL_EXPIRED', N'The trial period has ended please register AccountingPlus to continue operation.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXCEPTION_OCCURED', N'Exception Occurred.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SOAP_EXCEPTION_OCCURED', N'SOAP Exception Occurred.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_FILES_TO_BE_DELETE', N'Please selected the files to be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILE_DELETE_CONFIRM', N'Do you want to delete selected files?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_COPIES', N'Invalid Copies count.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPIES_COUNT_NOT_LESS_THAN_ONE', N'Copies count should not be less than one.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_CREATE_WEB_SERVIE', N'Failed to create Web service object.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANUAGE_SELECT_ONLY_ONE', N'Please select only one Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_SELECT', N'Please select one Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_USER_TRY_AGAIN', N'Invalid user. Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_VALID_CSV_XML', N'Please upload valid CSV  files only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVICE_IS_NOT_RESPONDING', N'PrintDataProvider service is not responding. Please start the service.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LICENSE', N'Invalid License. Please consult administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_LICENCES_USE', N'All licences are in use.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISCOVERY_IS_IN_PROGRESS', N'Discovery is in progress.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_REC_TO_EXPORT', N'No record to export.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_LOAD_REPORT', N'Failed to load report.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MESSAGE_UPDATED_SUCESSFULLY', N'Message updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_ENTER_RESPONSE_CODE', N'Please enter response code.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGIESTRATION_SUCCESS', N'Registration completed successfully. Thank you for using our product', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_RESPONSE_CODE', N'Invalid response code.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESPONSE_CODE_ALREADY_USED', N'Response code already used.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESTRICTED_AREA', N'Access denied. Only Administrators can access this page.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END_IP_GREATER', N'End IP should be greater than Start IP.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DISABLEUSER', N'Please Select Disable User(s) Only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_SETTINGS', N'Invalid Settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_FROM_DATE', N'Invalid From Date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_TO_DATE', N'Invalid To Date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REG_COMPLETED', N'Registration completed successfully.Thank you for using our product.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_CODE', N'Invalid code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_CAN_NOT_BE_PRINTED', N'Job can not be printed on this device, please select another device or resubmit job using a PCL driver', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_WILL_BE_PRINT_BW', N'Job will printed in B&W Press Continue to print or Cancel', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_SUCCESS_RELEASED', N'The Print Job has been successfully released', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNABLE_TO_AUTHENTICATE', N'Unable to authenticate with Active Directory. Please consult your administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ARE_YOU_SURE_DELETE', N'Are you sure you want to delete ?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_UPDATE_SUCCESS', N'MFP details updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_SAVE_FAIL', N'Failed to save Domain user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_SAVE_SUCESS', N'Domain Users saved successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PR_CONFIGURED_TO_EAM_ONLY', N'AccountingPlus cannot be accessed, it is currently configured to EAM only mode.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PR_CONFIGURED_TO_ACM_ONLY', N'AccountingPlus cannot be accessed, it is currently configured to ACM only mode.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_JOB_RETENTION_DAYS', N'Please enter job retention day(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_JOB_RETENTION_TIME', N'Please enter job retention time', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_TIME_FORMAT', N'Invalid time format', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_SELECT_JOB', N'Please select job (s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERFULLNAME_EMPTY', N'User Full Name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERDETAILS_NOTFOUND', N'User not found please login with valid user credentials', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_IP', N'Invalid IP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IP_REQUIRED', N'IP required', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_ENTER_USER_FULL', N'Please enter user full name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_USER_EDIT_UNLOCK', N'Select a user to edit and Unlock', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DIABLED_USERS', N'Select disabled user(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_USERS', N'Select user(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_DETAILS_NOT_FOUND', N'User Details not found', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_FULL_NAME_EMPTY', N'User Full Name cannot be empty', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_HERE_TO_UPLOAD_USERS', N'Click Here to upload Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_HERE_TO_SYNC_USERS', N'Click Here to  Sync  LDAP Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_HERE_TO_EDIT', N'Click here to edit text', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FROM_DATE_LESS_THAN_TO', N'From date should be less than or equal to To date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COULD_NOT_FIND_THE_PRINT_JOB_IN_SERVER', N'Could not find the print job in server.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTRATION_DEVICE_NOT_RESPONDING', N'User registration is succesfull. Device is not responding. please restart the device.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_RECORDS_LOG_CLEAR', N'All the records from the Log will be cleared Do you want to continue ?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_INFO_NOT_FOUND_CHECK_WITH_ADMIN', N'User information not found.  Please check with your administrator or try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_LOG_CLEAR_SUCCESS', N'All the records from the Log will be cleared.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALID_IP', N'Please enter Valid IP.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_BACK', N'Click here to go back', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_SAVE', N'Click here to save/update', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_RESET', N'Click here to reset', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_RESET_SUCCESS', N'User(s) enabled successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_LOCK_SUCCESS', N'User(s) disabled  successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_LOCK_FAIL', N'Failed to disable User(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_ALPHANUMERIC_ONLY', N'Please enter valid User Name. Only alpha numeric values are allowed', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGES_RESET_SUCCESS', N'Language(s) enabled successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGES_LOCK_SUCCESS', N'Language(s) disabled  successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGES_LOCK_FAIL', N'Failed to disable Language(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGES_RESET_FAIL', N'Failed to enabled Language(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_LOCK_SUCCESS', N'Device(s) disabled  successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_LOCK_FAIL', N'Failed to disable Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_RESET_SUCCESS', N'Device(s) enabled successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_RESET_FAIL', N'Failed to enabled Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_EXISTS', N'Group name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_SUCESS', N'Group added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_FAIL', N'Failed to add Group.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_DELETE_SUCESS', N'Group(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_DELETE_FAIL', N'Failed to delete Group(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_GROUP_FAIL', N'Selected Groups(s) cannot be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_UPDATE_SUCESS', N'Group updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_UPDATE_FAIL', N'Failed to update Group.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_NAME_EMPTY', N'Group name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS_ASSIGNED_GROUPS', N'Users assigned to selected group successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_ASSGIN_USER_GROUPS', N'Failed to assign user to groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_UPDATE_SUCESS', N'Permissions updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_UPDATE_FAIL', N'Failed to update Permissions', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_DELETE_SUCESS', N'Price profile deleted successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_DELETE_FAIL', N'Failed to delete the price profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_UPDATE_SUCESS', N'Price details updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_UPDATE_FAIL', N'Failed to update Price details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_INSERT_SUCESS', N'Price profile added successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_INSERT_FAIL', N'Failed to add price profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROFILE_UPDATE_SUCESS', N'Price profile updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROFILE_UPDATE_FAIL', N'Failed to update price profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_NOT_SET_FOR_THIS_GROUP', N'Limit(s) are not set for selected group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_UPDATE_SUCCESS', N'Permissions and Limits updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_UPDATE_FAIL', N'Failed to update Permissions and Limits.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_ASSIGNED_GROUPS', N'Devices assigned to groups successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_ASSGIN_DEVICES_GROUPS', N'Failed to assign devices to groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_EXISTS', N'Paper Size already exists', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_SUCESS', N'Paper Size added successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_FAIL', N'Failed to add Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_DELETE_SUCESS', N'Paper Size deleted succesfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PAPAERSIZE_FAIL', N'Select Paper Size fail', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_DELETE_FAIL', N'Failed to delete Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_UPDATE_FAIL', N'Failed to update Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_UPDATE_SUCESS', N'Paper Size updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_LIMITS_UPDATE_SUCCESS', N'Permissions and Limits Updated Successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_LIMITS_UPDATE_FAILED', N'Permissions and Limits Updated Failed', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TOASSGIN_COST_PROFILE', N'Failed to assgin cost profile to MFP groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_PROFILE_ASSGINTOMFP_SUCCESS', N'Cost profile assgined to MFP groups successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_PERMISSIONS_FOR_GROUP', N'Access rights restricted for this Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_LOW_CONTACT_ADMIN', N'Your Limits are low. Please contact administrator', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'VIEW_DETAILS', N'View Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ASSING_SUCCESS', N'Users Assigned to Cost Centers successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ASSING_FAIL', N'Failed to Assign User to Cost Centers', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USR_GRUP_TO_MFP_GRUP', N'User Groups Assigned to MFP Groups Successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USR_GRUP_TO_MFP_GRUP_FAIL', N'Failed to assign Groups to MFP Groups Successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_CENTER_EXIST', N'Cost Center already exists', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_SUCCESS_ON', N'Backup created successfully on ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_SUCCESS', N'Backup created successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_DELETE_SUCCESS', N'Server backup successfully deleted', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESTORE_SUCCESS', N'Restore successfully completed', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_COST_CENTER', N'Select Cost Center', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_NOT_REGISTERED', N'Device not registered to AccountingPlus. Please Consult Administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_NAME_EMPTY', N'Cost Center name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_DELETE_SUCESS', N'Cost Center(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_DELETE_FAIL', N'Failed to delete Cost Center(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_UPDATE_SUCESS', N'Cost Center updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_UPDATE_FAIL', N'Failed to update Cost Center.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_EXISTS', N'Cost Center name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_SUCESS', N'Cost Center added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_FAIL', N'Failed to add Cost Center.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_COST_PROFILE', N'Please Select a Cost Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_LIMITS_NOT_SET', N'Permissions and Limits are not set', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACCESS_RIGHTS_ASSIGN_SUCCESS', N'Access rights assigned succesfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ADMIN_ERROR', N'The user id admin OR administrator will not allow to register, try with some other user id', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNSELECT_LOGIN_USERDISABLE', N'Unselect logged in user and disable.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMAGE_TYPE', N'Image Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMAGE_PATH', N'Image Path', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_TO_DEFAULT', N'Updated custom them to default theme sucesfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_UPDATE_DEFAULT', N'Failed to update custom them to default theme', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_TO_SELECT', N'Updated default theme to selected theme successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_UPDATE_SELECT', N'Failed to updated default theme to selected theme ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_BG_IMAGE', N'Please select backgroung image type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_BG_IMAGE', N'Please select custom image', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_EXTENSIONS_LIMIT', N'Please upload jpeg,png,gif images only', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_LIMITS_UPDATE_SUCCESS', N'Auto Refill type changed successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_FROM_ADDRESS', N'Enter from address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_SMTP_HOST', N'Enter SMTP Host', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_SMTP_PORT', N'Enter SMTP Port number', getdate(), getdate(), 'Build', 'Build')
-- Localization Script --
truncate table APP_LANGUAGES
-- APP_LANGUAGES
insert into APP_LANGUAGES(APP_CULTURE,APP_NEUTRAL_CULTURE,APP_LANGUAGE,APP_CULTURE_DIR,REC_ACTIVE)values(N'en-US',N'en', N'English (United States)', 'LTR','True')


-- Localized Strings 

truncate table RESX_LABELS

-- Language = en-US
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'Lagnuage Name', N'US English', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CANCEL', N'Cancel', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT', N'Department', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENTS', N'Departments', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_DEPARTMENT', N'Department Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_USERNAME', N'AD Username', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_PASSWORD', N'AD Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTIVE_DIRECTORY', N'Active Directory', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTIVE_DIRECTORY_DETAILS', N'Active Directory Server Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_ACTIVE', N'Enable Department', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE_LOGON', N'Enable Log on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_LOGIN_ENABLED', N'Is Login Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_DETAILS', N'Update Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REFRESH', N'Refresh', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE', N'Update', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ALL', N'Select All', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GENERAL_SETTINGS', N'General Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'POWERED_BY', N'Powered by', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGON_MODE', N'Logon Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGON', N'Login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGONNAME', N'Login Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISPLAYING', N'Displaying', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS', N'Jobs ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_TYPE', N'Job Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_TYPES', N'Job Type(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_COMPUTER', N'Job Computer', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_DATE', N'Job Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_ID', N'Job ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_CONFIGURATION', N'Job Configuration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_MODE', N'Job Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_NAME', N'Job Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_PRICE', N'Job Price', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_LOG', N'Job Log', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_STATUS', N'Job Status', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECTED_USERS', N'Selected Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXCEPTION', N'Exception', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATION_SOURCE', N'Authentication Source', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EDIT', N'Edit', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REQUIRED_FIELD', N'Required fields', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USE_SSO', N'Use SSO', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ROLE', N'User Role', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ID', N'User Id', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_MACHINE', N'User Machine', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_NAME', N'User Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_MANAGEMENT', N'User Management', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REPORTS', N'Reports', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DESCRIPTION', N'Description', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WIDTH', N'Width', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPUTERS', N'Computer(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPUTER_NAME', N'Computer Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CSV_USERS', N'CSV File Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SAMPLE_DATA', N'User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATA_DECODING_RULE', N'Data Decoding Rule (all card types)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PADDING_RULE', N'Data Padding Rule (all card types)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATE', N'Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN', N'Domain', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_NAME', N'Domain name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOBS', N'Print Jobs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_SETTINGS', N'Print Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT', N'Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_DELETE', N'Print & Delete', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_RELEASE', N'AccountingPlus', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_LIST', N'Print List', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTPIN', N'Print PIN', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_POSITION', N'By Position', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_DELIMITER', N'By Delimiter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SINGLE_COLOR', N'Single Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENFORCE_FACILITY_CODE', N'Enforce Facility Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FACILITY_CODE_SETTINGS', N'Facility Code Check Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SETTINGS', N'Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_SETTINGS', N'Print Job Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMAIL', N'Email', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMAIL_ID', N'Email Id', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END', N'End', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END_DELIMITER', N'End Delimiter (E)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESULT', N'Result', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISCOVER_DEVICE', N'Discover', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALLOWED_FORMAT', N'Allowed format (.CSV) ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILTER_ON', N'Filter On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_NEW_DEVICE', N'Add New MFPs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_NEW_USER', N'Add New User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PADDING_RULE_SETTING', N'Padding Rule Setting(Up to three (3) characters can be specified for each padding)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GENERATE_REPORT', N'Generate', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE', N'Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES', N'Devices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_ID', N'Device ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_NAME', N'Device Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL', N'Total', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_SHEET_COUNT', N'Total Sheet Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_RECORDS', N'Total Records', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_COUNT', N'Total Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_ON', N'Group On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUPS', N'Groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_ID', N'Group Id', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_CSV_FILE', N'Click here to upload users from CSV file.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SYNC_LDAP_D', N'Click here to Sync LDAP users.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD', N'Add', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD', N'Upload', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMPORT', N'Import', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IP_ADDRESS', N'IP Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_TYPE', N'Card Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_ID', N'Card ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_CONFIGURATION', N'Card Configuration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CONFIGURATION', N'Configuration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'READ_LENGTH_L', N'Read Length (L)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELETE', N'Delete', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_LOGON_TYPE', N'Manual Login Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_LOGIN', N'Manual Login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_TYPE', N'Manual Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MY_PROFILE', N'My Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MESSAGE', N'Message', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MESSAGE_TYPE', N'Message Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFPS', N'MFP(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_IP_ADDRESS', N'MFP IP Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_MAC_ADDRESS', N'MFP MAC Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_MODE', N'MFP Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'POST_PADDING', N'Post Padding (Z)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NAME', N'Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NEW_DEVICE', N'New Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_PAPER_SIZE', N'Job Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPER_SIZES', N'Paper Size(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD', N'Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN', N'PIN', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUDIT_LOG', N'Audit log', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTER', N'Register', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLOSE', N'Close', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAST_PRINT', N'Fast Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BLACK_WHITE', N'Monochrome', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE', N'Page', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_SIZE', N'Page Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERIAL_NUMBER', N'Serial Number', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SAVE', N'Save', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOCK_DOMAIN', N'Lock Domain', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOCK_DOMAIN_FIELD', N'Lock Domain Field', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE', N'Language', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MASTER_DATA', N'Master Data', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STACKSTRACE', N'Stacks Trace', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START', N'Start', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_POSITION', N'Start Position', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_POSITION_X', N'Start Position (X)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_DELIMITER', N'Start Delimiter (D)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DAYS', N'Days', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'THEMES', N'Themes', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TO', N'To', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELIMITER', N'Delimiter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'URL', N'URL', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'VERSION', N'Version', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FULL_COLOR', N'Full Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPLETE', N'Complete', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FROM', N'From', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRE_PADDING', N'Pre Padding (A)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PREVIEW', N'Preview', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PREVIEW_USERS', N'Preview Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_DEVICE', N'Select External Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATING_PLEASE_WAIT', N'Updating please wait', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TIME', N'Time', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACK', N'Back', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2_COLOR', N'2 Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'100_SHEET_LOWER_UPPER', N'100 Sheet Staple Lower Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'100_SHEET_MIDDLE_UPPER', N'100 Sheet Staple Middle Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'100_SHEET_STAPLE_UPPER', N'100 Sheet Staple Upper Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A3', N'A3', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A4', N'A4', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTION', N'Action', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTIVEDIRECTORY_SETTINGS', N'Active Directory settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_FULL_NAME_ATTRIBUTE', N'AD full name attribute', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_FULLNAME_FIELD', N'AD Full Name Field', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_PORT', N'AD port', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_LANGUAGE', N'Add Language', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_NEW_MFP', N'Add New MFPs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGON_MAX_RETRY_COUNT', N'Allowed retries for user login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ANONYMOUS_USER_PRINTING', N'Anonymous user printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLICATION_UI', N'Application  UI', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLICATION_NAME', N'Application Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLICATION_REGISTRATION', N'Registration & Activation', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLICATION_URL', N'Application Url', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATION_SETTINGS', N'Authentication settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO', N'Auto', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BOOK', N'Book', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_ADMINISTRATOR_ONLY', N'By Administrator only', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_IP_ADDRESS', N'By IP Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BY_IP_RANGE', N'By IP Range', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CANCELLED', N'Cancelled', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_DATA_CONFIGURATION', N'Card Data configuration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_LOGON', N'Card Log on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_READER_TYPE', N'Card Reader Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CENTER_TRAY', N'Center Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLEAR', N'Clear', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_EXPORT_CSV', N'Click here to export the job usage details to CSV', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXPORT_JOBDETAILS_EXCEL', N'Click here to export the job usage details to MS Excel', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_EXPORT_XML', N'Click here to export the job usage details to XML', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_HERE_REGISTER', N'Click here to register', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLIENT_MESSAGES', N'Client Messages ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLLATE', N'Collate', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLOR', N'Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLOR_MODE', N'Color Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLUMN_MAPPING', N'Column Mapping', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPUTERNAME', N'Computer Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CONTACT_INFO', N'Contact Info', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CONTINUE', N'Continue', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPIES', N'Copies', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPY', N'Copy', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD_ALIAS', N'Create password alias', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CSV_TEMPLATE_D', N'CSV Template', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CUSTOM_MESSAGES', N'Custom Messages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATABASE', N'Database ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATE_TIME_HEADER', N'Date/Time', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DAY_LEFT', N'day(s) left', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DISABLED', N'Device disabled. Please contact Administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_IS_NOT_REGISTERED', N'Device is not Registered to AccountingPlus Application. Please Register.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_IN_SUBNET', N'Devices in Subnet', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DIRECT_MANUAL_ENTRY', N'Direct manual entry', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISABLE', N'Disable', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISPLAY_JOB_LIST', N'Display job list', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DO_YOU_WANT_TO_DELETE_DATA', N'Do you want to delete data?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOCUMENTS_FOR', N'Documents for', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_ADMINISTRATOR', N'Domain Administrator', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_CONTROLLER', N'Domain controller', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX', N'Duplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX_DIR', N'Duplex Dir', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX_MODE', N'Duplex Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX_ONE_SIDED', N'Simplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLEX_TWO_SIDED', N'Duplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_CARD_IDS_COUNT', N'Duplicate Card IDs count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_PIN_IDS_COUNT', N'Duplicate PIN IDs count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_RECORD_COUNT', N'Duplicate record(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_RECORDS_FOUND', N'Duplicate records found  ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DUPLICATE_USERS_COUNT', N'Duplicate User Name count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EDIT_USER_DETAILS', N'Edit user details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_CARDID_COUNT', N'Empty CardID(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_PASSWORD_COUNT', N'Empty Password(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_PINID_COUNT', N'Empty PIN ID(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_USERNAME_COUNT', N'Empty UserFullName(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EMPTY_USERID_COUNT', N'Empty UserID(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE', N'Enable', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE_DEVICE', N'Enable Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END_IP', N'End IP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_LOGIN_DETAILS_TO_REGISTER_USER', N'Enter login details to register user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ERROR', N'Error', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ERROR_MESSAGE', N'Error Message', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ERROR_SOURCE', N'Error source', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_ACCOUNTING_CONTROL', N'External Accounting Control EAM', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_ACCOUNTING_UI', N'External Accounting UI', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_ACCOUNTING_WEBSERVICE', N'External Accounting Web Service', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FACILITY_CODE_CHECK', N'Facility Code Check', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FACILITY_CODE_CHECK_VALUE', N'Facility code check value', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILING', N'Filing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILTER_BY', N'Filter by', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIND_DEVICES_DUMMY', N'Find Devices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FINISHED', N'Finished', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FINISHER_LOWER_TRAY', N'Finisher Lower Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FINISHER_UPPER_TRAY', N'Finisher Upper Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FINISHING', N'Finishing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIRST_TIME_USE', N'First Time Use', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIT_TO_PAGE', N'Fit to Page', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FOLDER', N'Folder', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FROM_DATE', N'From Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'HIDE_SETTING', N'Hide Setting', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'HOLD', N'Hold', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'HOLD_AFTER_PRINT', N'Hold after Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'HOME', N'Home', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ID', N'ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IGNORE_DUPLICATES_AND_SAVE', N'Ignore duplicates and save.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMPORT_USERS', N'Import Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMPORTING_WILL_IGNORE_ALL_DUPLICATE_RECORDS', N'Importing will ignore all duplicate records.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INSTALLATION_DATE', N'Installation Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_CARDS', N'Invalid Cards', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_DETAILS', N'Invalid Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_EMAIL_ID_COUNT', N'Invalid Email ID count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LICENSE_ERROR', N'Invalid license error', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_PINS', N'Invalid PINs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_DEVICE_ENABLED', N'Is Device Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_LANGUAGE_ENABLED', N'Is Language Enabled', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_RETENTION', N'Job Retention', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_SETTINGS', N'Job Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_ID', N'Language ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LEDGER', N'Ledger', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LEGAL', N'Legal', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LENGTH', N'Length', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LETTER', N'Letter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LICENSE_ID', N'License ID', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOCAL_DATABASE_MANAGEMENT', N'Local database management', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOG_OUT', N'Log out', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGIN_DETAILS', N'Login details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGIN_FOR_ALL_FUNCTIONS', N'Login for all functions', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGIN_FOR_PRINT_RELEASE_ONLY', N'Login for AccountingPlus only ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGOUT', N'Logout', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAIN', N'Main', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LINK_MANAGE_LANGUAGE', N'Manage Language', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_LOGON', N'Manual Log on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_ERROR_DETAILS', N'MFP error details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_UI_LANGUAGE', N'MFP UI Language', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_USER_AUTHENTICATION', N'MFP user authentication', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MODEL_NAME', N'Model Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MODELNAME', N'Model Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MODULE', N'Module', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MONOCHROME', N'Monochrome', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NOT_APPLICABLE', N'N/A', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NAVIGATE_TO_MFP_MODE', N'Navigate to MFP Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NETWORK_PASSWORD', N'Remember Network Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO', N'No', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NONE', N'None', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NONNUMERIC_PINID_COUNT', N'Nonnumeric PIN ID(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NOTES', N'Notes', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_OF_LICENSE', N'Number of Licenses', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OFF_SET', N'Off Set', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OK', N'OK', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ON_NO_JOBS', N'On no jobs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ORIENTATION', N'Orientation', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OTHER', N'Other', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OUTPUT_TRAY', N'Output Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_IS_LOADING_PLEASE_WAIT', N'Page is Loading. Please wait.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPER_SIZE', N'Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD_OR_PIN', N'Password/PIN', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN_LOGON', N'Pin Log on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_COMPLETE_CARD_ENROLMENT', N'Please complete card enrollment.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_CONTACT_ADMINISTRATOR', N'Please contact AccountingPlusWeb Administrator', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_DEPLOY_VALID_LICENSE', N'Please deploy the valid license file and try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DELETE', N'Please select an', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_MOVE', N'Please select an option to move', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_WAIT', N'Please wait…', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_&_RETAIN_BUTTON_STATUS', N'Print & Retain button status', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTRELEASE_ABOUT', N'About', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_RELEASE_APPLICATION_ERROR_DETAILS', N'AccountingPlus Application error details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_FIRST_LAUNCH', N'AccountingPlus first launched on', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ERROR_DETAILS', N'AccountingPlus MFP Application error details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTER_MODE', N'Printer Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTING_IN_PROGRESS', N'Printing in progress', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROOF', N'Proof', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PUNCH', N'Punch', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'QUEUED', N'Queued', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'QUICK', N'Quick', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'READY', N'Ready', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGIESTER_NOW', N'Register Now', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTEREDLICENCESE', N'Registered Licences : Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTRATION_DETAILS', N'Registration Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REQUEST_CODE', N'Request Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESPONSE_CODE', N'Response Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETENTION', N'Retention', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETRIVED_GROUPS_COUNT', N'Retrieved groups count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETURN_TO_JOB_LIST', N'Return to  Job list', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETURN_TO_PRINT_JOB_LIST', N'Return to print job list', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RIGHT_TRAY', N'Right Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SNO', N'S.No.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SADDLE_LOWER_TRAY', N'Saddle-Lower Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SADDLE_MIDDLE_TRAY', N'Saddle-Middle Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SADDLE_UPPER_TRAY', N'Saddle-Upper Tray', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SCAN', N'Scan', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SECURE_SWIPE', N'Secure Swipe', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PASSWORD_FOR_FUTURE_LOGINS', N'Select password for future logins.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PRINT_OPTIONS', N'Select Print options', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELF_REGISTER_DEVICE', N'Self register device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELF_REGISTRATION', N'Self Registration', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERIALNUMBER', N'Serial Number', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVER_MESSAGES', N'Server Messages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SETTING', N'Setting', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CONTACT_DETAILS', N'SHARP SOFTWARE DEVELOPMENT INDIA PVT. LTD', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SHOW_SETTING', N'Show Setting', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SIMPLEX', N'Simplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SKIP_PRINT_SETTINGS', N'Skip print settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SORT', N'Sort', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STAPLE', N'Staple', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_IP', N'Start IP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STARTED', N'Started', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STATUS', N'Status', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUCCESS', N'Success', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUGGESTION', N'Suggestion', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUPPORT_FOR_DUPLEX', N'Support for duplex', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_GO', N'Swipe and Go', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_YOUR_ID_CARD_TO_LOGIN', N'Swipe your ID card to login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TABLET', N'Tablet', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TO_DATE', N'To Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTALBW', N'Total BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTALCOLOR', N'Total Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRACE', N'Trace', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRIAL_DAYS', N'Trial Days', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRIAL_LICENCES', N'Trial Licences : Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRIAL_VERSION', N'Trial Version', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TYPE', N'Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNLIMITED', N'unlimited', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_MFP', N'Update MFP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADED_RECORD_COUNT', N'Uploaded record(s) count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USE_PIN', N'Use PIN', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USE_WINDOWS_PASSWORD', N'Use Windows Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERID', N'User Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERNAME', N'User Full Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_NOT_FOUND', N'User not found', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_WANT_REGISTER', N'User not found. Do you want to register?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_PROVISIONING', N'User provisioning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_SOURCE', N'User Source', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS_FROM_CSV_OR_XML_COLUMNS', N'Users From CSV  columns', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS_FROM_DATABASE_COLUMNS', N'Users From Database columns', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS_SOURCE', N'Users Source', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'VALID_RECORDS_COUNT', N'Valid records count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'VALUE', N'Value', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WARNING', N'Warning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GRAND_TOTAL', N'Grand Total', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'Go', N'Go', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_DOMAIN', N'Invalid Domain Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOCATION', N'Location', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATION_SERVER', N'Authentication Server', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_USING', N'Print Using ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_DETAILS', N'FTP Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_PROTOCOL', N'FTP Protocol', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_ADDRESS', N'FTP Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_PORT', N'FTP Port', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTRELEASE_API', N'AccountingPlus API', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LENGTH_CARDID', N'Invalid Length CardID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LENGTH_PINID', N'Invalid Length PinID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_COMPLETE_REGISTRATION', N'Please complete registration data…', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_FULL_NAME', N'User Full Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'YES', N'Yes', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_ACCESS', N'Print Job Access', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUBMIT', N'Submit', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UI_DIRECTION', N'Language Direction', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_NEWUSER', N'Add New User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_USER', N'Update User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISPLAY_NAME', N'Display name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP', N'Group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'1_STAPLE', N'1 Staple', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2_STAPLE', N'2 Staple', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNLOCK_USERS', N'Enable User(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'XLS', N'XLS', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CSV', N'Click here to export to CSV', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'XML', N'XML', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNDO', N'UNDO', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'XML_TEMPLATE', N'XML AccountingPlus Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_JOBS_FOUND_REDIRECT', N'No job (s) found you will be redirected to MFP Mode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_OPTIONS', N'Options', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_DEPARTMENT_ENABLED', N'Is Department Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EAM', N'Is Job Log Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACM', N'Is ACM Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_MODEL', N'MFP Model', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REQUIRED_JOBLOG', N'Required for Job Log', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_BACK', N'Click here to go back', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_SAVE', N'Click here to save/update', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_RESET', N'Click here to reset', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_SETTINGS', N'Card Configuration Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBCONFIGURATION_SETTINGS', N'Job Configuration Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIND_DEVICES', N'Discover Devices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_TYPE', N'Print Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIRST_LOGON', N'First Login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'1', N'1', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2', N'2', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'3', N'3', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'4', N'4', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'5', N'5', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'6', N'6', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'7', N'7', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'8', N'8', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'9', N'9', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_USERS', N'Domain Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGIN_FOR_MX-PRINT_ONLY', N'Login for AccountingPlus only', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEFAULT', N'Default', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT', N'Select', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADMIN', N'Admin', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER', N'User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SINGLE_SIDE', N'Single side', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2_SIDED_BOOK', N'2 Sided (Book)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'2_SIDED_TABLET', N'2 Sided (Tablet)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL', N'Manual', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD   ', N'Card          ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROXIMITY_CARD', N'Proximity', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAGENTIC_STRIPE', N'Magentic Stripe', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BARCODE_READER', N'Barcode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_AND_GO', N'Swipe and Go', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERNAME_PASSWORD', N'UserName/Password', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EAM_AND_ACM', N'EAM and ACM', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EAM_ONLY', N'EAM Only', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACM_ONLY    ', N'ACM Only          ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP_PRINT', N'FTP Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OSA_PRINT', N'OSA Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTP', N'FTP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FTPS', N'FTPS              ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_USERNAME_PASSWORD', N'Manual(Username/Password)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_PIN', N'Manual(Pin)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_SECURE_SWIPE', N'Card(Secure Swipe)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_SWIPE_ANDGO', N'Card(Swipe and Go)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROXIMITY', N'Proximity', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAGNETIC_STRIPE', N'Magnetic Stripe', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BARCODE', N'Barcode', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL                ', N'All                                 ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INFORMATION', N'Information', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CRITICALERROR', N'CriticalError', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CRITICALWARNING', N'CriticalWarning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS', N'Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPUTER', N'Computer', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CN', N'cn', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LEFT_RIGHT', N'Left to Right', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RIGHT_LEFT', N'Right to Left', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESX_CLIENT_MESSAGES', N'Client Messages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESX_SERVER_MESSAGES', N'Server Messages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESX_LABELS', N'Labels', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESET', N'Reset', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACTIVE_DOMAIN', N'Active Directory / Domain Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SYNC_LDAP', N'Click here to Import LDAP users.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISABLE_USER', N'Disable User(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CSV_TEMPLATE', N'CSV AccountingPlus Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADFILE_DUPLICATE_DETAILS', N'Upload file duplicate records details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADFILE_DUPLICATE_USERID', N'Upload file duplicate UserID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADFILE_DUPLICATE_CARDID', N'Upload file duplicate CardID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOADFILE_DUPLICATE_PINID', N'Upload file duplicate PinID(s) Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_LOGIN_TYPE', N'Card Login type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISABLE_LANGIAGES', N'Disable Language(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE_LANGUAGES', N'Enable Language(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISABLE_DEVICES', N'Disable Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENABLE_DEVICES', N'Enable Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_GROUP', N'Group Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_ACTIVE', N'Enable Group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_GROUP_ENABLED', N'Is Group Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS', N'Permissions', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_USER', N'Domain Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_USED', N'Job Used', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_LIMIT', N'Page Limit', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_PROFILE', N'Price Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNIT_PRICE', N'Unit Price', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USR_GROUP', N'User Group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_PROFILE', N'Cost Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_GROUPS', N'MFP Groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_CENTER', N'Cost Center', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPER_SIZE_CATEGORY', N'Paper Size Category', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_PAPER_SIZE', N'Paper Size Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPERSIZE_ACTIVE', N'Paper Size Enabled', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_PAPER_SIZE_ENABLED', N'Is Paper Size Enabled ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_CATEGORY', N'Job Category', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICES', N'Prices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS', N'Limits', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL', N'Auto Refill', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_SETTINGS', N'Auto Refill Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REFILL_TYPE', N'Refill Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTOMATIC', N'Automatic', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADD_TO_EXISTING_LIMITS', N'Add to Existing Limits', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_ON', N'Auto Refill On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EVERY_DAY', N'Every Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EVERY_WEEK', N'Every Week', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EVERY_MONTH', N'Every Month', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_TIME', N'Auto Refill Time', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_WEEK', N'Auto Refill Week', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTO_REFILL_DATE', N'Auto Refill Date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MONDAY', N'Monday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TUESDAY', N'Tuesday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WEDNESDAY', N'Wednesday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'THURSDAY', N'Thursday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FRIDAY', N'Friday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SATURDAY', N'Saturday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SUNDAY', N'Sunday', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SET_PERMISSIONS_LIMITS', N'Set Permissions and Limits', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CATEGORY', N'Category', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_COST_CENTER', N'Select Cost Center', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_AVAILABLE', N'Limits Available', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OVER_DRAFT', N'Over Draft', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_COLOR', N'Print Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_MONOCHROME', N'Print BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SCAN_COLOR', N'Scan Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SCAN_MONOCHROME', N'Scan BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPY_COLOR', N'Copy Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPY_MONOCHROME', N'Copy BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN', N'Assign', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP', N'MFP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFPSS', N'MFPs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_CENTER_NAME', N'Cost Center Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_COST_CENTER_ENABLED', N'Is Cost Center Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IS_MFP_ENABLED', N'IS MFP Enabled?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_LIMITS_ON', N'Permissions and Limits On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELTE_SERVER_BACKUP', N'Click here to Delete server backup', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_TO_BACKUP', N'Click here to backup server', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_TO_RESTORE', N'Click here to restore server', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATABASE_NAME', N'DataBase Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_SIZE', N'Backup size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_DATE', N'Backup date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVER_NAME', N'Server Name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_ON', N'Limits On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALLOW_OVER_DRAFT', N'Allow Over Draft', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALLOWED_OVER_DRAFT', N'Allowed Over Draft', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALERT_LIMIT', N'Alert Limit', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_ON', N'Permissions On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_MFPGROUP_COSTPROFILE', N'Assgin MFP Groups to Cost Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXECUTIVE_SUMMARY', N'Executive Summary', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CURRENT_VOLUMES', N'Current Volumes', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_DAYS', N'Total Number of Days', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_JOBS', N'Total Number of Jobs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_PAGES', N'Total Number of Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_USERS', N'Total Number of Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_BW_PAGES', N'Total Number of B&W Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_COLOR_PAGES', N'Total Number of Color Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CURRENT_ASSETS', N'Current Assets', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_DEVICES', N'Total Number of Devices', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_NO_WORKSTATION', N'Total Number of Workstation', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CURRENT_COSTS', N'Current Costs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PERPAGE', N'Average Cost Per Page', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PERUSER', N'Average Cost Per User', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PERPRINTER', N'Average Cost Per Printer', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_BW_PRINTING', N'Cost of BW Printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_COLOR_PRINTING', N'Cost of Color Printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_COST_PRINTING', N'Total Cost of Printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTRAPOLATED_VALUES', N'Extrapolated Values', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PERUSER_PERDAY', N'Average Cost Per User Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COST_PER_PRINTER_PERDAY', N'Average Cost Per Printer Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_COLORPAGE_PERDAY', N'Average Color Pages Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_BWPAGE_PERDAY', N'Average BW Pages Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AVERAGE_TOTAL_PAGESPERDAY', N'Average Total Pages Per Day', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROJECTIONS', N'Projections', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_PER_MONTH', N'Cost Per Month', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_PER_QUARTER', N'Cost Per Quarter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTS_1YEAR', N'Costs (1 Year)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTS_3YEAR', N'Costs (3 Year)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGES_PER_MONTH', N'Pages Per month', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGES_PER_QUARTER', N'Pages Per Quarter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGES_PERYEAR', N'Pages Per (1 Year)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGES_PER3YEAR', N'Pages Per (3 Year)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_RANGE', N'Page Range', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_PAGES', N'Total Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERCENTAGE', N'Percentage', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTER', N'Printer', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENDDATE_NOT_GREATERTHAN_TODAYDATE', N'End date cannot be greater than today''s date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_DATA_FOR_DATE', N'No data found for the selected date range', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_PRINT_BREAKDOWN', N'Total Volume Breakdown - Pages', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_PRINT_BREAKDOWN_JOBS', N'Total Volume Breakdown - Jobs', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WEEKDAY_REPORT', N'WeekDay Report', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_10_PRINTERS', N'Top 10 Printers', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_10_USERS', N'Top 10 Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_USERS_BY_COLOR', N'Top Users by Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_USERS_BY_BW', N'Top Users by Black and White', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_PRINTER_BY_BW', N'Top Printers by Black and White', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOP_PRINTER_BY_COLOR', N'Top Printers by Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_CENTERS', N'Cost Centers', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLOR_UNITS', N'Color (price)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MONOCHROME_UNITS', N'Monochrome (Price)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACCESS_RIGHTS', N'Access Rights', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_MFP_TOGROUP', N'Assign MFP to Group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_TO_COSTPROFILE', N'Assign To Cost Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GRAPHICAL_REPORT', N'Graphical Report', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVOICE_REPORT', N'Invoice', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_RESTORE', N'Backup & Restore', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LICENCES', N'Registration & Activation', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLIENT_CODE', N'Client Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACC_INSTALLED_ON', N'AccountingPlus Installed On', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERIAL_KEY', N'Serial Key', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_REGISTERED_LICENCES', N'Total Registered Licences', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTRATION_ADDLICENCES', N'Registration/Add Licences', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SYS_INFORMATION', N'System Information', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_INFORMATION', N'User Information', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PHONE_MOBILE', N'Phone/Mobile Number', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COMPANY', N'Company', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADDRESS', N'Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CITY', N'City', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STATE', N'State', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COUNTRY', N'Country', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ZIP_CODE', N'Zip Code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTRATION_INFORMATION', N'Registration Information', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACC_REGISTRATION_SUCCESS', N'AccountingPlus Registered successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACCOUNTING_INFO_MENU', N'Accounting Info : Menu', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'STANDARD_APPLICATION_CONTROL', N'Standard Application Setting', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADDRESS_FOR_APPLICATION_UI', N'Address for Application UI ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ADDRESS_FOR_WEB_SERVICE', N'Address for Web Service', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A3BW', N'A3-BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A3C', N'A3-Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A4BW', N'A4-BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'A4C', N'A4-Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OtherBW', N'Other-BW', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'OtherC', N'Other-Color', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER', N'CostCenter', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_ACTIVE', N'Enable Cost Center', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_USERS_TO_COSTCENTERS', N'Assign users to Cost Centers', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ASSIGN_MFP_TO_COSTPROFILE', N'Assign MFPs to Cost Profiles', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PREFERED_COST_CENTER', N'Preferred Cost Center for Printing', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLY_TO_ALL', N'Apply To ALL', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CUSTOM_THEME', N'Custom Thems', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_IMAGE', N'Select image type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_CUSTOMIMAGE', N'Upload custom image', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WALLPAPER', N'WallPaper', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAXIMUMSIZE_EXCEEDED', N'Uploaded file exceeded maximum size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAXIMUM_FILESIZE', N'Maximum allowed  file size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACC_DEVICEREGISTRATION_SUCCESS', N'Device registered successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_ACCESS', N'Job Access', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WALLPAPER_RECOMMENDED _HEIGHT', N'Recommended  height', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WALLPAPER_RECOMMENDED_WIDTH', N'Recommended  width', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SMTP_PORT', N'SMTP Port', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SMTP_HOST', N'SMTP Host', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FROM_ADDRESS', N'From Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CC_ADDRESS', N'CC Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BCC_ADDRESS', N'Bcc Address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SMTP_SETTINGS', N'SMTP Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ANONYMOUS_DIRECT_MFPPRINT', N'Anonymous Direct Print to MFP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATE_DIRECTPRINT', N'Authenticate User For Direct Print', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MONOCHROME_COUNT', N'Monochrome (Units)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COLOR_COUNT', N'Color (Units)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_PRICE', N'Total (Price)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_LABELS(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TOTAL_UNITS', N'Total (Units)', getdate(), getdate(), 'Build', 'Build')
-- Localized Strings 

truncate table RESX_CLIENT_MESSAGES

-- Language = en-US
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'Lagnuage Name', N'US English', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_FIELD_REQUIRED', N'Domain field cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD_REQUIRED', N'Password cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERID_REQUIRED', N'User ID cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONE_USER', N'Please select one user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONLY_ONE_USER', N'Please select only one user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONE_MFP', N'Please select one MFP.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALID_IP', N'Please enter Valid IP.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_NUMERICS', N'Enter numerics only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONEDEPARTMENT', N'Select only one Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DEPARTMENT', N'Select Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_NAME_EMPTY', N'Department name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DESCRIPTION_EMPTY', N'Description cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBLIST_SELECTONE', N'Please select one Job.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANUAGE_SELECT_ONLY_ONE', N'Please select only one Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_SELECT', N'Please select  Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DPR', N'Please check Data Decoding rule or uncheck Data Padding rule', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELETE_CONFIRMATION', N'Selected user(s) will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_DELETE_CONFIRMATION', N'All the details regarding Departments will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATE_FROM_TO_VALIDATION', N'Please ensure that End date is greater than or equal to Start date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DELETE_CONFIRM', N'Selected MFP(s) will be removed.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_RECORDS_LOG_CLEAR', N'All the records from the Log will be cleared. 
Do you want to continue ?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_DELETE_CONFIRM', N'All the details regarding selected Language will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_DELETE_CONFIRM', N'All the details regarding Departments will be deleted. 
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINTJOBS_DELETE_CONFIRM', N'Selected Print Job(s) will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_CREATE_WEBSERVICEOBJECT', N'Failed to create web service object.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILE_DELETE_CONFIRM', N'Do you want to delete selected files?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELETED_FILES_DELETE', N'Please selected the files to be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PAGE_NUMBER', N'Please enter valid page number.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'WARNING', N'Warning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAGE_COUNT_LESS_THAN', N'Page count should be less than total Pages Count', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_DELETE_UNLOCK', N'Please select user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DELETE', N'Please select MFP(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNDO', N'Undo', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_SETTINGS', N'Invalid Settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONEGROUP', N'Select only one Group.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_GROUP', N'Select Group.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_NAME_EMPTY', N'Group name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_DELETE_CONFIRMATION', N'All the details regardingGroups will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONEPAPERSIZE', N'Select one Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PAPERSIZE', N'Select Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPERSIZE_NAME_EMPTY', N'Paper Size Name Empty', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_DELETE_CONFIRMATION', N'Do you want to delete selected Paper Size(s)?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONE_BACKUP', N'Please select one Backup', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESTORE_CONFIRM', N'Are you sure you want to Restore selected Backup?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DELETE_CONFIRM', N'Are you sure you want to Delete selected Backup?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECTED_PRICE_PROFILE_CONFIRM', N'From now,selected price profile will be applicable.Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_CONFIRMATION', N'All the details regarding Cost Center will be deleted.
Do you want to continue?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONECOSTCENTER', N'Select only one Cost Center.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_COSTCENTER', N'Select Cost Center.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_NAME_EMPTY', N'Cost Center name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'APPLY_PRICE_FOR_ALL', N'Do you want to Apply the price for all the Job Types?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_CLIENT_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_ONE_MFPGROUP', N'Please select one MFP Group.', getdate(), getdate(), 'Build', 'Build')
-- Localized Strings 

truncate table RESX_SERVER_MESSAGES

-- Language = en-US
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'Lagnuage Name', N'US English', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_DELETE_SUCCESS', N'User(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_DELETE_FAIL', N'Failed to delete user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_DELETE_SUCCESS', N'MFP(s) deleted Successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_DELETE_FAIL', N'Failed to delete MFP(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_DISCOVERY_SUCCESS', N'Device(s) discovered successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_DISCOVERY_FAIL', N'Discovery operation failed.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_USER_ROLE', N'Select user role.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_CONFIGURED_ANOTHER_USER', N'This card is already configured to another user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_UPDATE_FAIL', N'Failed to update user details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_UPDATE_SUCCESS', N'User details updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERID_ALREADY_EXIST', N'User name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN_ALREADY_USED', N'This PIN is already used by another user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ADD_SUCCESS', N'User details added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ADD_FAIL', N'Failed to add user details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_LOGON_TYPE', N'Please select a Logon type.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_AUTH_SOURCE', N'Please select an Authentication source.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_SINGLE_SIGN_ON', N'Please select single Sign on.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_CARD_TYPE', N'Please select a Card type.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_MANUAL_AUTH_TYPE', N'Please select Manual Authentication type.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_ALREADY_EXISTS', N'This MFP already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_ADD_SUCCESS', N'MFP details added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_ADD_FAIL', N'Failed to add MFP details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNABLE_TO_CONNECT', N'Unable to connect to AD server.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LDAP_SAVE_SUCCESS', N'LDAP Users saved successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LDAP_SAVE_FAIL', N'Failed to save LDAP user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUDIT_CLEAR_SUCCESS', N'Audit Log cleared successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUDIT_CLEAR_FAIL', N'Failed to clear Audit log.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_REG_ERROR', N'Invalid User.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_PROFILE_UPDATE_SUCCESS', N'User profile updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_UPDATE_SUCESS', N'Card configuration updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_UPDATE_FAIL', N'Failed to update Card settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_FILE', N'Please select the file to upload.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERNAME_EMPTY', N'User Name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGINNAME_EMPTY', N'Login Name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INSERT_SUCESS', N'User(s) inserted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INSERT_FAILED', N'Failed to insert users.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_CSV_XML', N'Please upload CSV file only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPTNAME_EXISTS', N'Department name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_SUCESS', N'Department added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_FAIL', N'Failed to add Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_DELETE_SUCESS', N'Department(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_DELETE_FAIL', N'Failed to delete Departments(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DEPARTMENT_FAIL', N'Selected Departments(s) cannot be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_UPDATE_SUCESS', N'Department updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_UPDATE_FAIL', N'Failed to update Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBSETT_SUCESS', N'Settings updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVICE_NOTRESPONDING', N'"PrintDataProvider" service is not responding, please start the service.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS_DELETED_SUCESS', N'Jobs(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS_DELETED_FAIL', N'Failed to delete Jobs(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LOGON_MAX_RETRY_COUNT', N'Allowed retries for user login.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ANONYMOUS_USER_PRINTING', N'Anonymous user printing.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AUTHENTICATION_SETTINGS', N'Authentication Settings', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DB_AUTHENTICATION', N'Data Base Authentication', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RETURN_TO_JOB_LIST', N'Return to Print Job List', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_PROVISIONING', N'User Provisioning', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SETTNG_UPDATE_SUCESS', N'Settings updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SETTNG_UPDATE_FAIL', N'Failed to update settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_LOGIN_ERROR', N'Failed to authenticate user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NORECORDS_EXISTS', N'Records not found.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ROOTFILE_MISSING', N'Root element is missing.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_CARD_ENTER_PASSWORD', N'Swipe your ID card and enter password to login.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SWIPE_CARD', N'Swipe your ID card to login.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USER_DATA', N'Please enter your login data.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USER_PIN', N'Please enter user PIN number to login.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROVIDE_USER_DETAILS', N'Please provide user details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USER_PASSWORD', N'Please enter user password.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACCOUNT_DISABLED', N'User Account is disabled. Please consult your administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_CARD_ID', N'Invalid ID card. Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_PASSWORD', N'Invalid password. Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_NOT_FOUND', N'User not found. Please enter valid user Name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN_INFO_NOT_FOUND', N'PIN information not found.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXCEEDED_MAXIMUM_LOGIN', N'You have exceeded the maximum number of login attempts. Please consult your administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_PIN', N'Invalid PIN number. Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_SINGLE_JOB', N'Please select single job to view settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECTED_JOBS_TO_BE_DELETED', N'Please select job(s) to be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS_DELETE_SUCCEESS', N'Selected job(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOBS_DELETE_FAIL', N'Failed to delete selected jobs.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RELEASED_ON_DEVICE', N'Please select job(s) released on this device.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MORE_THAN_5_SELECTED', N'More than 5 jobs selected. A maximum 5 jobs can be released at a time.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_NOT_REGIESTER', N'Device is not registered to AccountingPlus application. Please register.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_INFO_NOT_FOUND_REGISTER', N'User information not found. Would you like to register now?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_CARD_ID', N'Please enter Card ID.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USERNAME', N'Please enter User name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_PASSWORD', N'Please enter Password.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_NAME_ALREADY_EXISTS', N'User with same name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_USER_DETAILS', N'Invalid User details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_USER_PIN', N'Invalid User PIN.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PIN_MINIMUM', N'PIN must be 4-10 digits.  Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_REGISTER', N'Failed to register.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_RESET_SUCCESS_DC', N'User(s) unlocked successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_RESET_FAIL', N'Failed to unlock user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXTERNAL_ACCOUNTING_LOGIN_DETAILS', N'External Accounting Login Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROVIDE_LOGIN_DETAILS', N'Please provide login details.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FIRST_TIME_USER_LOGIN', N'First Time User Login', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_INFO_NOT_FOUND', N'Card information not found. Would you like to register now?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_INFO_NOT_FOUND_CONSULT_ADMIN', N'Card information not found. Please check with your administrator or try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_INFO_NOT_FOUND', N'User information not found.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVICE_RESPONDING', N'PrintDataProvider service responding properly.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARD_NOT_FOUND', N'Card information not found.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_DOMAIN', N'Please enter Domain name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEPARTMENT_NAME_EMPTY', N'Department name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNSELECT_LOGIN_USER', N'Unselect logged in user and delete.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_FIELD_REQUIRED', N'Domain field cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PASSWORD_REQUIRED', N'Password cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DESCRIPTION_EMPTY', N'Description cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REQUIRED_LICENCE', N'Field cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGULAREXPRESSION_NUMERICS', N'Only numerics are allowed.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'START_DATE_GREATER', N'Start date cannot be greater than Today''s date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END_DATE_GREATER', N'End date cannot be greater than Today''s date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARDID_EMPTY', N'CardID name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PINID_EMPTY', N'Print PIN name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_AD_USERNAME', N'Enter AD user name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_AD_USER_PASSWORD', N'Enter AD user password.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_AD_PORT', N'Enter AD port.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_SETTING_UPDATE_SUCCESS', N'Active Directory settings updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'AD_SETTING_UPDATE_FAILED', N'Failed to update Active Directory settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REPORT_TO_ADMIN', N'Report to administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_LOG_ON_MODE', N'Please select Logon mode.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_CARD_READER_TYPE', N'Please select Card Reader type.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_LOGIN_NAME', N'Please enter User name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALDI_PIN', N'Please enter valid Print PIN.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELF_REGISTER_DEVICE', N'Self Register Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATA_ADDED_SUCCESSFULLY', N'Data added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATA_UPDATED_SUCCESSFULLY', N'Data updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_ADD_DATA', N'Failed to add data.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_UPDATE_DATA', N'Failed to update data.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DATA_DELETED_SUCCESSFULLY', N'Data deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_ADD_SUCCESS', N'Language added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_ADD_FAIL', N'Failed to add Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_UPDATE_SUCCESS', N'Language updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_UPDATE_FAIL', N'Failed to update Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_USER_LOGIN_NAME', N'Enter Login name.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_USER_CREDENTIALS', N'Invalid user credentials.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_DOMAIN_CREDENTIALS', N'Invalid domain credentials.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CARDID_ALREADY_USED', N'This CardID is already used by another user.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DISABLED_CONTACT_ADMIN', N'Access to AccountingPlus application is disabled to this device. Please consult Administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LICENCE', N'Invalid Licence file. Please consult Administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_LOGIN_DISABLE_ERROR', N'User Disabled. Please consult Administrator for activation.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MAXIMUM_RECORDS', N'At a time maximum of 10,000 records should be imported.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALID_EMAIL', N'Enter valid e-mail id.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALID_NAME_ALPHA', N'Enter Name as alphanumeric only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DEPARTMENT', N'Select Department.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_USER_AUTHENTICATION', N'MFP User Authentication', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DOES_NOT_SUPPORT_DRIVER', N'Job cannot be printed on this device. Please select another device or re-submit job using a PCL driver.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_DOES_NOT_SUPPORT_COLOR', N'Job will printed in B&W. Press ''Continue'' to print or ''Cancel'' to cancel.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_DRIVER_JOBS_CANNOT_PRINT', N'All selected jobs cannot be printed on this device. Press ''Continue'' to print supported jobs or ''Cancel'' to cancel.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_CAN_NOT_BE_PROCESSED', N'Your job can not be processed at this time. Please press OK and consult the administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_SUCCESS', N'Print job(s) successfully submitted to device.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_DRIVER_NOT_SUPPORTED', N'Job cannot be printed on this device.
Please select another device or resubmit job using a PCL driver.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_ERROR', N'Your print job cannot be processed at this time. 
Please consult your administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_INVALID_SETTINGS', N'Some of the selected print settings are incompatible. 
Please check settings and try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_PAPER_SIZE_NOT_SUPPORTED', N'Paper size is not available on this device. 
Press continue to select an alternate paper size for your job.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_LICENCES_ARE_IN_USE', N'All license(s) are currently in use.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'TRIAL_EXPIRED', N'The trial period has ended please register AccountingPlus to continue operation.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'EXCEPTION_OCCURED', N'Exception Occurred.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SOAP_EXCEPTION_OCCURED', N'SOAP Exception Occurred.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_FILES_TO_BE_DELETE', N'Please selected the files to be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FILE_DELETE_CONFIRM', N'Do you want to delete selected files?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_COPIES', N'Invalid Copies count.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COPIES_COUNT_NOT_LESS_THAN_ONE', N'Copies count should not be less than one.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_CREATE_WEB_SERVIE', N'Failed to create Web service object.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANUAGE_SELECT_ONLY_ONE', N'Please select only one Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGE_SELECT', N'Please select one Language.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_USER_TRY_AGAIN', N'Invalid user. Please try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_VALID_CSV_XML', N'Please upload valid CSV  files only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SERVICE_IS_NOT_RESPONDING', N'PrintDataProvider service is not responding. Please start the service.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_LICENSE', N'Invalid License. Please consult administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_LICENCES_USE', N'All licences are in use.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DISCOVERY_IS_IN_PROGRESS', N'Discovery is in progress.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_REC_TO_EXPORT', N'No record to export.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TO_LOAD_REPORT', N'Failed to load report.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MESSAGE_UPDATED_SUCESSFULLY', N'Message updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_ENTER_RESPONSE_CODE', N'Please enter response code.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGIESTRATION_SUCCESS', N'Registration completed successfully. Thank you for using our product', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_RESPONSE_CODE', N'Invalid response code.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESPONSE_CODE_ALREADY_USED', N'Response code already used.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESTRICTED_AREA', N'Access denied. Only Administrators can access this page.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'END_IP_GREATER', N'End IP should be greater than Start IP.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DISABLEUSER', N'Please Select Disable User(s) Only.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_SETTINGS', N'Invalid Settings.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_FROM_DATE', N'Invalid From Date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_TO_DATE', N'Invalid To Date.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REG_COMPLETED', N'Registration completed successfully.Thank you for using our product.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_CODE', N'Invalid code', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_CAN_NOT_BE_PRINTED', N'Job can not be printed on this device, please select another device or resubmit job using a PCL driver', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_WILL_BE_PRINT_BW', N'Job will printed in B&W Press Continue to print or Cancel', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRINT_JOB_SUCCESS_RELEASED', N'The Print Job has been successfully released', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNABLE_TO_AUTHENTICATE', N'Unable to authenticate with Active Directory. Please consult your administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ARE_YOU_SURE_DELETE', N'Are you sure you want to delete ?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MFP_UPDATE_SUCCESS', N'MFP details updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_SAVE_FAIL', N'Failed to save Domain user(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DOMAIN_SAVE_SUCESS', N'Domain Users saved successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PR_CONFIGURED_TO_EAM_ONLY', N'AccountingPlus cannot be accessed, it is currently configured to EAM only mode.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PR_CONFIGURED_TO_ACM_ONLY', N'AccountingPlus cannot be accessed, it is currently configured to ACM only mode.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_JOB_RETENTION_DAYS', N'Please enter job retention day(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_JOB_RETENTION_TIME', N'Please enter job retention time', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_TIME_FORMAT', N'Invalid time format', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_SELECT_JOB', N'Please select job (s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERFULLNAME_EMPTY', N'User Full Name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERDETAILS_NOTFOUND', N'User not found please login with valid user credentials', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'INVALID_IP', N'Invalid IP', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IP_REQUIRED', N'IP required', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PLEASE_ENTER_USER_FULL', N'Please enter user full name', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_USER_EDIT_UNLOCK', N'Select a user to edit and Unlock', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_DIABLED_USERS', N'Select disabled user(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_USERS', N'Select user(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_DETAILS_NOT_FOUND', N'User Details not found', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_FULL_NAME_EMPTY', N'User Full Name cannot be empty', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_HERE_TO_UPLOAD_USERS', N'Click Here to upload Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_HERE_TO_SYNC_USERS', N'Click Here to  Sync  LDAP Users', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_HERE_TO_EDIT', N'Click here to edit text', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FROM_DATE_LESS_THAN_TO', N'From date should be less than or equal to To date', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COULD_NOT_FIND_THE_PRINT_JOB_IN_SERVER', N'Could not find the print job in server.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'REGISTRATION_DEVICE_NOT_RESPONDING', N'User registration is succesfull. Device is not responding. please restart the device.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ALL_RECORDS_LOG_CLEAR', N'All the records from the Log will be cleared Do you want to continue ?', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_INFO_NOT_FOUND_CHECK_WITH_ADMIN', N'User information not found.  Please check with your administrator or try again.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'JOB_LOG_CLEAR_SUCCESS', N'All the records from the Log will be cleared.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_VALID_IP', N'Please enter Valid IP.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_BACK', N'Click here to go back', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_SAVE', N'Click here to save/update', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'CLICK_RESET', N'Click here to reset', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_RESET_SUCCESS', N'User(s) enabled successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_LOCK_SUCCESS', N'User(s) disabled  successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_LOCK_FAIL', N'Failed to disable User(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_ALPHANUMERIC_ONLY', N'Please enter valid User Name. Only alpha numeric values are allowed', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGES_RESET_SUCCESS', N'Language(s) enabled successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGES_LOCK_SUCCESS', N'Language(s) disabled  successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGES_LOCK_FAIL', N'Failed to disable Language(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LANGUAGES_RESET_FAIL', N'Failed to enabled Language(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_LOCK_SUCCESS', N'Device(s) disabled  successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_LOCK_FAIL', N'Failed to disable Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_RESET_SUCCESS', N'Device(s) enabled successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_RESET_FAIL', N'Failed to enabled Device(s)', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_EXISTS', N'Group name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_SUCESS', N'Group added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_FAIL', N'Failed to add Group.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_DELETE_SUCESS', N'Group(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_DELETE_FAIL', N'Failed to delete Group(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_GROUP_FAIL', N'Selected Groups(s) cannot be deleted.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_UPDATE_SUCESS', N'Group updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_UPDATE_FAIL', N'Failed to update Group.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'GROUP_NAME_EMPTY', N'Group name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USERS_ASSIGNED_GROUPS', N'Users assigned to selected group successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_ASSGIN_USER_GROUPS', N'Failed to assign user to groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_UPDATE_SUCESS', N'Permissions updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_UPDATE_FAIL', N'Failed to update Permissions', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_DELETE_SUCESS', N'Price profile deleted successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_DELETE_FAIL', N'Failed to delete the price profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_UPDATE_SUCESS', N'Price details updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_UPDATE_FAIL', N'Failed to update Price details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_INSERT_SUCESS', N'Price profile added successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PRICE_INSERT_FAIL', N'Failed to add price profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROFILE_UPDATE_SUCESS', N'Price profile updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PROFILE_UPDATE_FAIL', N'Failed to update price profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_NOT_SET_FOR_THIS_GROUP', N'Limit(s) are not set for selected group', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_UPDATE_SUCCESS', N'Permissions and Limits updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_UPDATE_FAIL', N'Failed to update Permissions and Limits.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICES_ASSIGNED_GROUPS', N'Devices assigned to groups successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_ASSGIN_DEVICES_GROUPS', N'Failed to assign devices to groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_EXISTS', N'Paper Size already exists', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_SUCESS', N'Paper Size added successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_FAIL', N'Failed to add Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_DELETE_SUCESS', N'Paper Size deleted succesfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_PAPAERSIZE_FAIL', N'Select Paper Size fail', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_DELETE_FAIL', N'Failed to delete Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_UPDATE_FAIL', N'Failed to update Paper Size', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PAPAERSIZE_UPDATE_SUCESS', N'Paper Size updated successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_LIMITS_UPDATE_SUCCESS', N'Permissions and Limits Updated Successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_LIMITS_UPDATE_FAILED', N'Permissions and Limits Updated Failed', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_TOASSGIN_COST_PROFILE', N'Failed to assgin cost profile to MFP groups', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_PROFILE_ASSGINTOMFP_SUCCESS', N'Cost profile assgined to MFP groups successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'NO_PERMISSIONS_FOR_GROUP', N'Access rights restricted for this Device', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'LIMITS_LOW_CONTACT_ADMIN', N'Your Limits are low. Please contact administrator', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'VIEW_DETAILS', N'View Details', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ASSING_SUCCESS', N'Users Assigned to Cost Centers successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ASSING_FAIL', N'Failed to Assign User to Cost Centers', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USR_GRUP_TO_MFP_GRUP', N'User Groups Assigned to MFP Groups Successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USR_GRUP_TO_MFP_GRUP_FAIL', N'Failed to assign Groups to MFP Groups Successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COST_CENTER_EXIST', N'Cost Center already exists', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_SUCCESS_ON', N'Backup created successfully on ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_SUCCESS', N'Backup created successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'BACKUP_DELETE_SUCCESS', N'Server backup successfully deleted', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'RESTORE_SUCCESS', N'Restore successfully completed', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_COST_CENTER', N'Select Cost Center', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'DEVICE_NOT_REGISTERED', N'Device not registered to AccountingPlus. Please Consult Administrator.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_NAME_EMPTY', N'Cost Center name cannot be empty.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_DELETE_SUCESS', N'Cost Center(s) deleted successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_DELETE_FAIL', N'Failed to delete Cost Center(s).', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_UPDATE_SUCESS', N'Cost Center updated successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_UPDATE_FAIL', N'Failed to update Cost Center.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_EXISTS', N'Cost Center name already exists.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_SUCESS', N'Cost Center added successfully.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'COSTCENTER_FAIL', N'Failed to add Cost Center.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_COST_PROFILE', N'Please Select a Cost Profile', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'PERMISSIONS_LIMITS_NOT_SET', N'Permissions and Limits are not set', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ACCESS_RIGHTS_ASSIGN_SUCCESS', N'Access rights assigned succesfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'USER_ADMIN_ERROR', N'The user id admin OR administrator will not allow to register, try with some other user id', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UNSELECT_LOGIN_USERDISABLE', N'Unselect logged in user and disable.', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMAGE_TYPE', N'Image Type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'IMAGE_PATH', N'Image Path', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_TO_DEFAULT', N'Updated custom them to default theme sucesfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_UPDATE_DEFAULT', N'Failed to update custom them to default theme', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPDATE_TO_SELECT', N'Updated default theme to selected theme successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'FAILED_UPDATE_SELECT', N'Failed to updated default theme to selected theme ', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'SELECT_BG_IMAGE', N'Please select backgroung image type', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_BG_IMAGE', N'Please select custom image', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'UPLOAD_EXTENSIONS_LIMIT', N'Please upload jpeg,png,gif images only', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'MANUAL_LIMITS_UPDATE_SUCCESS', N'Auto Refill type changed successfully', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_FROM_ADDRESS', N'Enter from address', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_SMTP_HOST', N'Enter SMTP Host', getdate(), getdate(), 'Build', 'Build')
insert into RESX_SERVER_MESSAGES(RESX_MODULE, RESX_CULTURE_ID, RESX_ID, RESX_VALUE, REC_CDATE, REC_MDATE, REC_AUTHOR, REC_EDITOR) values( N'', N'en-US', N'ENTER_SMTP_PORT', N'Enter SMTP Port number', getdate(), getdate(), 'Build', 'Build')
