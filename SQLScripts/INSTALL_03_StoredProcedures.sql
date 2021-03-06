USE [AccountingPlusDB]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_NumXWeekDaysinMonth]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_NumXWeekDaysinMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_NumXWeekDaysinMonth]
GO
/****** Object:  UserDefinedFunction [dbo].[GetDatesforAday]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDatesforAday]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetDatesforAday]
GO
/****** Object:  UserDefinedFunction [dbo].[ConvertStringListToTable]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConvertStringListToTable]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ConvertStringListToTable]
GO
/****** Object:  StoredProcedure [dbo].[UserQuickJobTypeSummary]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserQuickJobTypeSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserQuickJobTypeSummary]
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_TABLE_519_VERSION]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_TABLE_519_VERSION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPGRADE_TABLE_519_VERSION]
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_TABLE_440_VERSION]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_TABLE_440_VERSION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPGRADE_TABLE_440_VERSION]
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_DATA_519_VERSION]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_DATA_519_VERSION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPGRADE_DATA_519_VERSION]
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_DATA_440_VERSION]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_DATA_440_VERSION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPGRADE_DATA_440_VERSION]
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_12]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_12]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPGRADE_12]
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_11_UpdatePermissionsAndLimts]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_11_UpdatePermissionsAndLimts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPGRADE_11_UpdatePermissionsAndLimts]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUsageLimits]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateUsageLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateUsageLimits]
GO
/****** Object:  StoredProcedure [dbo].[UpdateRetryCount]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateRetryCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateRetryCount]
GO
/****** Object:  StoredProcedure [dbo].[UpdateLimitsToUnsharedCostCenter]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateLimitsToUnsharedCostCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateLimitsToUnsharedCostCenter]
GO
/****** Object:  StoredProcedure [dbo].[UpdateLimitsTosharedCostCenter]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateLimitsTosharedCostCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateLimitsTosharedCostCenter]
GO
/****** Object:  StoredProcedure [dbo].[UpdateFileTransferDetails]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateFileTransferDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateFileTransferDetails]
GO
/****** Object:  StoredProcedure [dbo].[TST_ResetData]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_ResetData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TST_ResetData]
GO
/****** Object:  StoredProcedure [dbo].[TST_CreateData]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_CreateData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TST_CreateData]
GO
/****** Object:  StoredProcedure [dbo].[TST_BuildUsers]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_BuildUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TST_BuildUsers]
GO
/****** Object:  StoredProcedure [dbo].[TST_BuildMFPs]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_BuildMFPs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TST_BuildMFPs]
GO
/****** Object:  StoredProcedure [dbo].[TST_BuildMFPGroups]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_BuildMFPGroups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TST_BuildMFPGroups]
GO
/****** Object:  StoredProcedure [dbo].[TST_BuildJobLog]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_BuildJobLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TST_BuildJobLog]
GO
/****** Object:  StoredProcedure [dbo].[testreport]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[testreport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[testreport]
GO
/****** Object:  StoredProcedure [dbo].[TestingMfpIP]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestingMfpIP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TestingMfpIP]
GO
/****** Object:  StoredProcedure [dbo].[SetAccessRightForSelfRegistration]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetAccessRightForSelfRegistration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetAccessRightForSelfRegistration]
GO
/****** Object:  StoredProcedure [dbo].[ReportPrintCopiesCR]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportPrintCopiesCR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReportPrintCopiesCR]
GO
/****** Object:  StoredProcedure [dbo].[ReportPrintCopies]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportPrintCopies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReportPrintCopies]
GO
/****** Object:  StoredProcedure [dbo].[ReportBuilderJobTYPE]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportBuilderJobTYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReportBuilderJobTYPE]
GO
/****** Object:  StoredProcedure [dbo].[ReportBuilder]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportBuilder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReportBuilder]
GO
/****** Object:  StoredProcedure [dbo].[RemovePermissions]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemovePermissions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RemovePermissions]
GO
/****** Object:  StoredProcedure [dbo].[RemoveMfpsFromGroup]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveMfpsFromGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RemoveMfpsFromGroup]
GO
/****** Object:  StoredProcedure [dbo].[RemoveMfpOrDeviceFromCostProfile]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveMfpOrDeviceFromCostProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RemoveMfpOrDeviceFromCostProfile]
GO
/****** Object:  StoredProcedure [dbo].[ReleaeLocks]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReleaeLocks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReleaeLocks]
GO
/****** Object:  StoredProcedure [dbo].[RecordJobEvent]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordJobEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecordJobEvent]
GO
/****** Object:  StoredProcedure [dbo].[RecordHelloEvent]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordHelloEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecordHelloEvent]
GO
/****** Object:  StoredProcedure [dbo].[RecordAuditLog]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordAuditLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecordAuditLog]
GO
/****** Object:  StoredProcedure [dbo].[QuickJobTypeSummary]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuickJobTypeSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[QuickJobTypeSummary]
GO
/****** Object:  StoredProcedure [dbo].[QueueForJobRelease]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QueueForJobRelease]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[QueueForJobRelease]
GO
/****** Object:  StoredProcedure [dbo].[ManageSessions]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ManageSessions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ManageSessions]
GO
/****** Object:  StoredProcedure [dbo].[ManageFirstLogOn]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ManageFirstLogOn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ManageFirstLogOn]
GO
/****** Object:  StoredProcedure [dbo].[InsertMultipleRecords]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertMultipleRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertMultipleRecords]
GO
/****** Object:  StoredProcedure [dbo].[InsertMultipleJobLogs]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertMultipleJobLogs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertMultipleJobLogs]
GO
/****** Object:  StoredProcedure [dbo].[ImportADUsers]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportADUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportADUsers]
GO
/****** Object:  StoredProcedure [dbo].[GETUSERS]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETUSERS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GETUSERS]
GO
/****** Object:  StoredProcedure [dbo].[GetUserReport]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserReport]
GO
/****** Object:  StoredProcedure [dbo].[GetUserPinCountDetails]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserPinCountDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserPinCountDetails]
GO
/****** Object:  StoredProcedure [dbo].[GetTopReports]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTopReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTopReports]
GO
/****** Object:  StoredProcedure [dbo].[GetSlicedData]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSlicedData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSlicedData]
GO
/****** Object:  StoredProcedure [dbo].[GetREPORTNEW]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetREPORTNEW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetREPORTNEW]
GO
/****** Object:  StoredProcedure [dbo].[GetRandomDate]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRandomDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRandomDate]
GO
/****** Object:  StoredProcedure [dbo].[GetPricing]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPricing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPricing]
GO
/****** Object:  StoredProcedure [dbo].[GetPermissionsAndLimits]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPermissionsAndLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPermissionsAndLimits]
GO
/****** Object:  StoredProcedure [dbo].[GetPagedTableData]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPagedTableData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPagedTableData]
GO
/****** Object:  StoredProcedure [dbo].[GetPagedData]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPagedData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPagedData]
GO
/****** Object:  StoredProcedure [dbo].[GetNotes]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetNotes]
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedValues]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLocalizedValues]
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedStrings]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedStrings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLocalizedStrings]
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedServerMessage]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedServerMessage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLocalizedServerMessage]
GO
/****** Object:  StoredProcedure [dbo].[GetLocalizedLabel]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLocalizedLabel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLocalizedLabel]
GO
/****** Object:  StoredProcedure [dbo].[GetJobUsageDetails]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobUsageDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobUsageDetails]
GO
/****** Object:  StoredProcedure [dbo].[GetJobUsage]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobUsage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobUsage]
GO
/****** Object:  StoredProcedure [dbo].[GetJobReport]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobReport]
GO
/****** Object:  StoredProcedure [dbo].[GetJobPrice]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobPrice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobPrice]
GO
/****** Object:  StoredProcedure [dbo].[GetJobLogforCR]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobLogforCR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetJobLogforCR]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceUnits]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceUnits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetInvoiceUnits]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoice]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetInvoice]
GO
/****** Object:  StoredProcedure [dbo].[GetGroupMFPs]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGroupMFPs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetGroupMFPs]
GO
/****** Object:  StoredProcedure [dbo].[GetGroupJobPermissionsAndLimits]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGroupJobPermissionsAndLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetGroupJobPermissionsAndLimits]
GO
/****** Object:  StoredProcedure [dbo].[GetGraphicalReports]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGraphicalReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetGraphicalReports]
GO
/****** Object:  StoredProcedure [dbo].[GetFleetReports]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFleetReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetFleetReports]
GO
/****** Object:  StoredProcedure [dbo].[GetExecutiveSummary]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetExecutiveSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetExecutiveSummary]
GO
/****** Object:  StoredProcedure [dbo].[GetDevieLoginData]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDevieLoginData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDevieLoginData]
GO
/****** Object:  StoredProcedure [dbo].[GetDeviceReport]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDeviceReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDeviceReport]
GO
/****** Object:  StoredProcedure [dbo].[GetCrystalReportData]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCrystalReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCrystalReportData]
GO
/****** Object:  StoredProcedure [dbo].[GetCostProfileMfpsOrGroups]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCostProfileMfpsOrGroups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCostProfileMfpsOrGroups]
GO
/****** Object:  StoredProcedure [dbo].[GetCostCenterAccessRights12]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCostCenterAccessRights12]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCostCenterAccessRights12]
GO
/****** Object:  StoredProcedure [dbo].[GetCostCenterAccessRights]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCostCenterAccessRights]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCostCenterAccessRights]
GO
/****** Object:  StoredProcedure [dbo].[GetAuditLog]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAuditLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAuditLog]
GO
/****** Object:  StoredProcedure [dbo].[GetActiveDirectoryDetails]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetActiveDirectoryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetActiveDirectoryDetails]
GO
/****** Object:  StoredProcedure [dbo].[GetAccessRights]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAccessRights]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAccessRights]
GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomPin]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerateRandomPin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GenerateRandomPin]
GO
/****** Object:  StoredProcedure [dbo].[ExecuteData]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExecuteData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExecuteData]
GO
/****** Object:  StoredProcedure [dbo].[EmailSetting]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmailSetting]
GO
/****** Object:  StoredProcedure [dbo].[DeviceUsageSummary]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeviceUsageSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeviceUsageSummary]
GO
/****** Object:  StoredProcedure [dbo].[DeviceMfpIP]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeviceMfpIP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeviceMfpIP]
GO
/****** Object:  StoredProcedure [dbo].[DeleteUsers]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteUsers]
GO
/****** Object:  StoredProcedure [dbo].[DeleteDomains]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteDomains]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteDomains]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCostCenter]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteCostCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteCostCenter]
GO
/****** Object:  StoredProcedure [dbo].[CounterDetails]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CounterDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CounterDetails]
GO
/****** Object:  StoredProcedure [dbo].[CopyDefaultPermissionsAndLimits]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyDefaultPermissionsAndLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CopyDefaultPermissionsAndLimits]
GO
/****** Object:  StoredProcedure [dbo].[CopyCostCenterPermissionsAndLimits]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyCostCenterPermissionsAndLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CopyCostCenterPermissionsAndLimits]
GO
/****** Object:  StoredProcedure [dbo].[CardMapping]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardMapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CardMapping]
GO
/****** Object:  StoredProcedure [dbo].[BuildJobSummary]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BuildJobSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BuildJobSummary]
GO
/****** Object:  StoredProcedure [dbo].[Billing]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Billing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Billing]
GO
/****** Object:  StoredProcedure [dbo].[Backup_Restore]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Backup_Restore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Backup_Restore]
GO
/****** Object:  StoredProcedure [dbo].[AutoRefill]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AutoRefill]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AutoRefill]
GO
/****** Object:  StoredProcedure [dbo].[AssignPeremissionsandLimits]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssignPeremissionsandLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AssignPeremissionsandLimits]
GO
/****** Object:  StoredProcedure [dbo].[AssignMfpOrGroupToCostProfile]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssignMfpOrGroupToCostProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AssignMfpOrGroupToCostProfile]
GO
/****** Object:  StoredProcedure [dbo].[AddUsersToGroup]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUsersToGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddUsersToGroup]
GO
/****** Object:  StoredProcedure [dbo].[AddMfpsToGroup]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddMfpsToGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddMfpsToGroup]
GO
/****** Object:  StoredProcedure [dbo].[AddLDAPGRoupUserDetails]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddLDAPGRoupUserDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddLDAPGRoupUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[AddLanguage]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddLanguage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddLanguage]
GO
/****** Object:  StoredProcedure [dbo].[AddAccessRights]    Script Date: 26/11/2015 13:01:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddAccessRights]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddAccessRights]
GO
/****** Object:  StoredProcedure [dbo].[AddAccessRights]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddAccessRights]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[AddAccessRights] @assignOn varchar(150),@assignTo varchar(50),@assigningId varchar(50),@selectedValues text,@userSource varchar(50)
as
begin
 
 select TokenVal into #SelectedValues from ConvertStringListToTable(@selectedValues, '','')  
if (@assignTo=''User'')
begin
	 insert into  T_ACCESS_RIGHTS(USR_SOURCE,ASSIGN_ON,ASSIGN_TO,MFP_OR_GROUP_ID,USER_OR_COSTCENTER_ID) select @userSource,@assignOn,@assignTo,@assigningId,TokenVal from #SelectedValues
end
else if(@assignTo=''Cost Center'')
     --print(''CostCenter'')
	 insert into  T_ACCESS_RIGHTS(USR_SOURCE,ASSIGN_ON,ASSIGN_TO,MFP_OR_GROUP_ID,USER_OR_COSTCENTER_ID) select @userSource,@assignOn,@assignTo,@assigningId,TokenVal from #SelectedValues
end' 
END
GO
/****** Object:  StoredProcedure [dbo].[AddLanguage]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[AddLDAPGRoupUserDetails]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddLDAPGRoupUserDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE proc [dbo].[AddLDAPGRoupUserDetails]
--Parameter for M_COST_CENTERS
@GRUPUserName nvarchar(150),
@Rec_Active bit,
@Rec_Date nvarchar(50),
@Rec_User nvarchar(20),
@ALLOWEDoverDraft bit,
@IsShared bit,
----Parameter for M_USERS
@USR_SOURCE varchar(2),
@USR_Domain nvarchar(50) ,
@USR_ID nvarchar(100),
@USR_Card_ID nvarchar(1000),
@USR_NAME nvarchar(100),
@USR_PIN nvarchar(1000),
@USR_PWD nvarchar(200),
@USR_Authenticate_ON nvarchar(50),
@USR_Email nvarchar(100),
@USR_DEPARTMENT int,
@USR_COSTCENTER int,
@USR_AD_PIN_FIELD nvarchar(50),
@USR_ROLE nvarchar(10),
@RETRY_COUNT int,
@RETRY_DATE nvarchar(50),
@REC_CDATE nvarchar(50),
@REC_ACTIVE_M_Users bit,
@ALLOW_OVER_DRAFT bit,
@ISUSER_LOGGEDIN_MFP bit,
@USR_MY_ACCOUNT bit


as
set nocount on
declare @COSTCENTER_ID as int
declare @USR_ACCOUNT_ID as int
declare @rowsCount int
BEGIN

--Insert/update into M_COST_CENTERS
	 BEGIN
			If Not Exists(Select COSTCENTER_ID from M_COST_CENTERS where COSTCENTER_NAME = @GRUPUserName)
				BEGIN
					Insert into M_COST_CENTERS(COSTCENTER_NAME,REC_ACTIVE,REC_DATE,REC_USER,ALLOW_OVER_DRAFT,IS_SHARED,USR_SOURCE)
					values(@GRUPUserName,@Rec_Active,getdate(),@Rec_User,@ALLOWEDoverDraft,@IsShared,@USR_SOURCE)
					SELECT @COSTCENTER_ID=@@IDENTITY
				END
			ELSE
				BEGIN
					UPDATE M_COST_CENTERS SET
					IS_SHARED=@IsShared				
					where COSTCENTER_NAME=@GRUPUserName 
				END
	 END

--Insert/update into M_USERS
  BEGIN
	IF Not Exists(Select USR_ID from M_USERS where USR_ID = @USR_ID)

		
	    Insert Into M_USERS(USR_SOURCE,USR_DOMAIN,USR_ID,USR_CARD_ID,USR_NAME,USR_PIN,USR_PASSWORD,USR_ATHENTICATE_ON,USR_EMAIL
		,USR_DEPARTMENT,USR_COSTCENTER,USR_AD_PIN_FIELD,USR_ROLE,RETRY_COUNT,RETRY_DATE,REC_CDATE,REC_ACTIVE,ALLOW_OVER_DRAFT
		,ISUSER_LOGGEDIN_MFP,USR_MY_ACCOUNT)
		values(@USR_SOURCE,@USR_Domain,@USR_ID,@USR_Card_ID,@USR_NAME,@USR_PIN,@USR_PWD,@USR_Authenticate_ON,@USR_Email,
		@USR_DEPARTMENT,@USR_COSTCENTER,@USR_AD_PIN_FIELD,@USR_ROLE,@RETRY_COUNT,getdate(),getdate(),@REC_ACTIVE_M_Users,

		@ALLOW_OVER_DRAFT,@ISUSER_LOGGEDIN_MFP,@USR_MY_ACCOUNT) 
	ELSE 
	-- IF Not Exists(Select USR_ID from M_USERS where USR_ID=''Admin'')
print @USR_ID
	   Update M_USERS set 
	   USR_SOURCE=@USR_SOURCE,
	   USR_DOMAIN=@USR_DOMAIN,
	   USR_ID=@USR_ID,
	   USR_CARD_ID=@USR_CARD_ID,
	   USR_NAME=@USR_NAME,
	   USR_PIN=@USR_PIN,
	   USR_PASSWORD=@USR_PWD,
	   USR_ATHENTICATE_ON=@USR_Authenticate_ON,
	   USR_EMAIL=@USR_EMAIL,
	   USR_DEPARTMENT=@USR_DEPARTMENT,
	   USR_COSTCENTER=@USR_COSTCENTER,
	   USR_AD_PIN_FIELD=@USR_AD_PIN_FIELD,
	   USR_ROLE=@USR_ROLE,
	   RETRY_COUNT=@RETRY_COUNT,
	   RETRY_DATE=getdate(),
	   REC_CDATE=getdate(),
	   REC_ACTIVE=@REC_ACTIVE,
	   ALLOW_OVER_DRAFT=@ALLOW_OVER_DRAFT,
	   ISUSER_LOGGEDIN_MFP=@ISUSER_LOGGEDIN_MFP,
	   USR_MY_ACCOUNT=@USR_MY_ACCOUNT
	   where USR_ID=@USR_ID and USR_ID <> ''admin''
	   SELECT @USR_ACCOUNT_ID= @@IDENTITY	
   END
   
 --Insert/update into T_COSTCENTER_USERS
 BEGIN
	declare @usrRecID int
	SELECT @COSTCENTER_ID=COSTCENTER_ID FROM M_COST_CENTERS WHERE COSTCENTER_NAME = @GRUPUserName
	SELECT @usrRecID = USR_ACCOUNT_ID from M_USERS where USR_ID = @USR_ID
--   IF @COSTCENTER_ID is null
--      BEGIN
print @GRUPUserName
--          SELECT @COSTCENTER_ID=COSTCENTER_ID FROM M_COST_CENTERS WHERE COSTCENTER_NAME = @GRUPUserName
--			
print @COSTCENTER_ID
print @usrRecID
--      END 
	 IF Not Exists(Select REC_ID from T_COSTCENTER_USERS where USR_ID = @USR_ID and COST_CENTER_ID = @COSTCENTER_ID)
		 Insert Into T_COSTCENTER_USERS(USR_ACCOUNT_ID,USR_ID,COST_CENTER_ID,USR_SOURCE)
		 values(@usrRecID,@USR_ID,@COSTCENTER_ID ,@USR_SOURCE )	
   END
    
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AddMfpsToGroup]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddMfpsToGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[AddMfpsToGroup] @groupID int, @mfpList text

as

--if @mfpList <> ''''
begin
	select TokenVal into #SelectedMFPs from ConvertStringListToTable(@mfpList, '','')
	insert into T_GROUP_MFPS(GRUP_ID, MFP_IP, REC_ACTIVE, REC_DATE) select @groupID, TokenVal, 1, getdate() from #SelectedMFPs
end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AddUsersToGroup]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUsersToGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[AddUsersToGroup] @groupId varchar(150),@selectedValues text
as
begin
	select TokenVal into #SelectedValues from ConvertStringListToTable(@selectedValues, '','')  
	select USR_ACCOUNT_ID,USR_SOURCE,USR_ID into #SelectedUsers from M_USERS where USR_ACCOUNT_ID in(select TokenVal from #SelectedValues)
	insert into  T_COSTCENTER_USERS(USR_ACCOUNT_ID,USR_ID,USR_SOURCE,COST_CENTER_ID) select USR_ACCOUNT_ID,USR_ID,USR_SOURCE,@groupId from #SelectedUsers 
end' 
END
GO
/****** Object:  StoredProcedure [dbo].[AssignMfpOrGroupToCostProfile]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssignMfpOrGroupToCostProfile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[AssignMfpOrGroupToCostProfile] @selectedCostCenter int, @selectedMFPs text, @assignedTo nvarchar(20)
as

select TokenVal into #SelectedMFPs from ConvertStringListToTable(@selectedMFPs, '','')

insert into T_ASSGIN_COST_PROFILE_MFPGROUPS(COST_PROFILE_ID, ASSIGNED_TO, MFP_GROUP_ID, REC_DATE)
select @selectedCostCenter, @assignedTo, TokenVal, getdate() from #SelectedMFPs

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AssignPeremissionsandLimits]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AssignPeremissionsandLimits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AssignPeremissionsandLimits] 
	@SelectedCostCenter nvarchar(50), 
	@UsersList text, 
	@sessionID nvarchar(50), 
	@LimitsOn int, 
	@IsOverDraftAllowed bit,
	@IsUpdateForCostCenter bit,
	@isApplytoAllUsers bit
as
Begin


	select TokenVal as TokanCostCenters into #CostCenters from ConvertStringListToTable(@SelectedCostCenter,'','')
	create table #Users(USR_ACCOUNT_ID int)
	if @isApplytoAllUsers = ''1''
		begin
		
			select USR_ACCOUNT_ID into #CostCenterUsers from T_COSTCENTER_USERS where COST_CENTER_ID in(select TokanCostCenters from #CostCenters)
			insert into #Users(USR_ACCOUNT_ID)select USR_ACCOUNT_ID from #CostCenterUsers
			drop table #CostCenterUsers
		end
	else
		begin
		
			select TokenVal as TokanUsers into #SelectedUsers from ConvertStringListToTable(@UsersList,'','')
			insert into #Users(USR_ACCOUNT_ID)select TokanUsers from #SelectedUsers
			drop table #SelectedUsers
		end

	select * into #PermissionsLimits from T_JOB_PERMISSIONS_LIMITS_TEMP where PERMISSIONS_LIMITS_ON=@LimitsOn and SESSION_ID=@sessionID
	
	select * into #FinalTable from #CostCenters cross join  #Users,#PermissionsLimits
	-- Get the Existing Limits of selected Users
	
	
	select * into #ExistingLimits from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID in (select TokanCostCenters from #CostCenters) and USER_ID in (select USR_ACCOUNT_ID from #Users)
	select * from #ExistingLimits
	if @LimitsOn = ''1''
		begin

		
			--select * from #PermissionsLimits
			--select * from #ExistingLimits
			
			-- Update the Existing Limits Temp table
			Update #ExistingLimits set #ExistingLimits.JOB_LIMIT = #PermissionsLimits.JOB_LIMIT,#ExistingLimits.ALERT_LIMIT = #PermissionsLimits.ALERT_LIMIT,#ExistingLimits.ALLOWED_OVER_DRAFT=#PermissionsLimits.ALLOWED_OVER_DRAFT,#ExistingLimits.JOB_ISALLOWED=#PermissionsLimits.JOB_ISALLOWED
			from #PermissionsLimits,#ExistingLimits
			where #PermissionsLimits.PERMISSIONS_LIMITS_ON = @LimitsOn and #ExistingLimits.JOB_TYPE = #PermissionsLimits.JOB_TYPE COLLATE SQL_Latin1_General_CP1_CI_AS
			
			--select * from #ExistingLimits
			-- Delete Old data from T_JOB_PERMISSIONS_LIMITS
			delete from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=@SelectedCostCenter and USER_ID in (select USR_ACCOUNT_ID from #Users)

			-- Insert all data to T_JOB_PERMISSIONS_LIMITS
			insert into T_JOB_PERMISSIONS_LIMITS(COSTCENTER_ID,USER_ID,PERMISSIONS_LIMITS_ON,JOB_TYPE,JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED)
			select TokanCostCenters,USR_ACCOUNT_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED from #FinalTable
			
			-- Re-update Job Used Count
			update T_JOB_PERMISSIONS_LIMITS set JOB_USED = #ExistingLimits.JOB_USED from #ExistingLimits 
			where T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID=@SelectedCostCenter and T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimits.USER_ID and T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimits.JOB_TYPE 
			
			-- Update Over Draft for the selected Cost Center
			update M_USERS set ALLOW_OVER_DRAFT= @IsOverDraftAllowed where USR_ACCOUNT_ID in (select USR_ACCOUNT_ID from #Users)

			-- Drop all temp tables
			Drop table #CostCenters,#Users,#PermissionsLimits,#FinalTable,#ExistingLimits

		end
	else
		begin
			select * from #PermissionsLimits
			select * from #ExistingLimits

			-- Update the Existing Limits Temp table
			Update #ExistingLimits set #ExistingLimits.JOB_LIMIT = #PermissionsLimits.JOB_LIMIT,#ExistingLimits.ALERT_LIMIT = #PermissionsLimits.ALERT_LIMIT,#ExistingLimits.ALLOWED_OVER_DRAFT=#PermissionsLimits.ALLOWED_OVER_DRAFT,#ExistingLimits.JOB_ISALLOWED=#PermissionsLimits.JOB_ISALLOWED
			from #PermissionsLimits,#ExistingLimits
			where #PermissionsLimits.PERMISSIONS_LIMITS_ON = @LimitsOn and #ExistingLimits.JOB_TYPE = #PermissionsLimits.JOB_TYPE COLLATE SQL_Latin1_General_CP1_CI_AS
			select * from #ExistingLimits
			-- Delete Old data from T_JOB_PERMISSIONS_LIMITS
			delete from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID in (select TokanCostCenters from #CostCenters) and USER_ID in (select USR_ACCOUNT_ID from #Users)
			-- Insert all data to T_JOB_PERMISSIONS_LIMITS
			insert into T_JOB_PERMISSIONS_LIMITS(COSTCENTER_ID,USER_ID,PERMISSIONS_LIMITS_ON,JOB_TYPE,JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED)
			select TokanCostCenters,USR_ACCOUNT_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED from #FinalTable
			-- Re-update Job Used Count
			update T_JOB_PERMISSIONS_LIMITS set JOB_USED = #ExistingLimits.JOB_USED from #ExistingLimits 
			where T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID in (select TokanCostCenters from #CostCenters) and T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimits.USER_ID and T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimits.JOB_TYPE 
	
			-- Check if the permissions and Limits to be copied for the Cost Center Also
			if @IsUpdateForCostCenter = ''1''
				begin

					declare @CostCenterCount int
					select @CostCenterCount=count(*) from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID in (select TokanCostCenters from #CostCenters) and USER_ID=''-1'' and PERMISSIONS_LIMITS_ON=@LimitsOn
					if @CostCenterCount =''0''
						begin

							insert into T_JOB_PERMISSIONS_LIMITS(COSTCENTER_ID,[USER_ID],PERMISSIONS_LIMITS_ON,JOB_TYPE,JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED)
							select @SelectedCostCenter,''-1'',PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED from #PermissionsLimits
						end
					else
						begin
print ''9th''
--							update T_JOB_PERMISSIONS_LIMITS set JOB_LIMIT=#PermissionsLimits.JOB_LIMIT,ALERT_LIMIT = #PermissionsLimits.ALERT_LIMIT,ALLOWED_OVER_DRAFT = #PermissionsLimits.ALLOWED_OVER_DRAFT,JOB_ISALLOWED = #PermissionsLimits.JOB_ISALLOWED
--							from #PermissionsLimits
--							where T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = @SelectedCostCenter and [USER_ID] = ''-1'' and T_JOB_PERMISSIONS_LIMITS.PERMISSIONS_LIMITS_ON=@LimitsOn
						end
				end

			-- Update Over Draft for the selected Cost Center
			update M_COST_CENTERS set ALLOW_OVER_DRAFT=@IsOverDraftAllowed where COSTCENTER_ID in (select TokanCostCenters from #CostCenters)
			-- Drop all temp tables
			Drop table #CostCenters,#Users,#PermissionsLimits,#FinalTable,#ExistingLimits
					
		end
End' 
END
GO
/****** Object:  StoredProcedure [dbo].[AutoRefill]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AutoRefill]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AutoRefill]
@refillFor VARCHAR (5)
AS
DECLARE @addwithOldLimits AS VARCHAR (10);
DECLARE @refillMode AS VARCHAR (20);
DECLARE @AllUserscount AS INT;
SELECT @refillMode = AUTO_FILLING_TYPE,
       @addwithOldLimits = ADD_TO_EXIST_LIMITS
FROM   T_AUTO_REFILL
WHERE  AUTO_REFILL_FOR = @refillFor;
PRINT @refillMode;
PRINT @refillFor;
PRINT @addwithOldLimits;
IF @refillMode = ''Automatic''
    BEGIN
        IF @refillFor = ''C'' -- C = Cost Center  
            BEGIN
                IF @addwithOldLimits = ''Yes''
                    BEGIN
                        -- Copy permissions and Limits for All the users in the Cost Center
                        EXECUTE UpdateLimitsToUnsharedCostCenter ''1'';
                        --Copy Permissions and Limits for the shared Cost Centers only - Not for the users in the cost center
                        EXECUTE UpdateLimitsTosharedCostCenter ''1'';
                    END
                ELSE
                    BEGIN
                        -- Copy permissions and Limits for All the users in the Cost Center
                        EXECUTE UpdateLimitsToUnsharedCostCenter ''0'';
                        --Copy Permissions and Limits for the shared Cost Centers only - Not for the users in the cost center
                        EXECUTE UpdateLimitsTosharedCostCenter ''0'';
                    END
                UPDATE  T_AUTO_REFILL
                    SET LAST_REFILLED_ON   = getdate(),
                        IS_REFILL_REQUIRED = ''0''
                WHERE   AUTO_REFILL_FOR = @refillFor;
            END
        ELSE --************** U = For Users ************** 
            BEGIN
                -- Build Data for Permissions and Limits for Users from configured data (T_JOB_PERMISSIONS_LIMITS_AUTOREFILL)  
                SELECT   USR_ACCOUNT_ID,
                         USR_SOURCE,
                         GRUP_ID,
                         PERMISSIONS_LIMITS_ON,
                         JOB_TYPE,
                         JOB_LIMIT,
                         JOB_ISALLOWED,
                         ALERT_LIMIT,
                         ALLOWED_OVER_DRAFT
                INTO     #UserDefaultPL
                FROM     M_USERS, T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                WHERE    PERMISSIONS_LIMITS_ON = 1
                         AND (CONVERT (VARCHAR (10), LAST_REFILLED_ON, 111) <> (SELECT CONVERT (VARCHAR (10), GETDATE(), 111))
                              OR LAST_REFILLED_ON IS NULL)
                         AND GRUP_ID = ''-1''
                         AND USR_ACCOUNT_ID NOT IN (SELECT DISTINCT GRUP_ID
                                                    FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL)
                GROUP BY GRUP_ID, USR_ACCOUNT_ID, USR_SOURCE, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED, ALERT_LIMIT, ALLOWED_OVER_DRAFT
                ORDER BY USR_ACCOUNT_ID, USR_SOURCE, JOB_TYPE;
                SET @AllUserscount = (SELECT COUNT(*)
                                      FROM   #UserDefaultPL);
                IF @AllUserscount > 0
                    BEGIN
                        -- Update GRUP_ID = COSTCENTER_ID as both are same  
                        UPDATE  #UserDefaultPL
                            SET GRUP_ID = USR_ACCOUNT_ID;
                        --select * from #UserDefaultPL  
                        UPDATE  T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                            SET LAST_REFILLED_ON = (SELECT getdate())
                        WHERE   GRUP_ID = ''-1'';
                        -- update with remaining limits  
                        IF @addwithOldLimits = ''Yes''
                            BEGIN
                                TRUNCATE TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP;
                                -- Copy PL in to Temp Table  
                                INSERT INTO T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP (COSTCENTER_ID, USER_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
                                SELECT COSTCENTER_ID,
                                       USER_ID,
                                       PERMISSIONS_LIMITS_ON,
                                       JOB_TYPE,
                                       JOB_LIMIT,
                                       JOB_USED,
                                       ALERT_LIMIT,
                                       ALLOWED_OVER_DRAFT,
                                       JOB_ISALLOWED
                                FROM   T_JOB_PERMISSIONS_LIMITS
                                WHERE  COSTCENTER_ID = ''-1''
                                       AND [USER_ID] <> -1;
                                --select * from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP  
                                UPDATE  #UserDefaultPL
                                    SET #UserDefaultPL.JOB_LIMIT = #UserDefaultPL.JOB_LIMIT + (T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_LIMIT - T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_USED)
                                FROM    T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP
                                WHERE   #UserDefaultPL.JOB_LIMIT < 214748360
                                        AND #UserDefaultPL.GRUP_ID = T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.USER_ID
                                        AND #UserDefaultPL.JOB_TYPE = T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_TYPE;
                                UPDATE  #UserDefaultPL
                                    SET #UserDefaultPL.JOB_LIMIT = 2147483647
                                WHERE   #UserDefaultPL.JOB_LIMIT > 2147483647;
                                TRUNCATE TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP;
                            END
                        -- Delete old Permissions and Limits  
                        --Print ''Delete old Permissions and Limits''  
                        DELETE T_JOB_PERMISSIONS_LIMITS
                        WHERE  PERMISSIONS_LIMITS_ON = 1
                               AND [USER_ID] <> -1
                               AND [USER_ID] NOT IN (SELECT DISTINCT GRUP_ID
                                                     FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL);
                        -- insert auto refilled data  
                        --Print ''insert auto refilled data''  
                        INSERT INTO T_JOB_PERMISSIONS_LIMITS (COSTCENTER_ID, USER_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED, ALERT_LIMIT, ALLOWED_OVER_DRAFT)
                        SELECT ''-1'',
                               GRUP_ID,
                               PERMISSIONS_LIMITS_ON,
                               JOB_TYPE,
                               JOB_LIMIT,
                               JOB_ISALLOWED,
                               ALERT_LIMIT,
                               ALLOWED_OVER_DRAFT
                        FROM   #UserDefaultPL;
                    END
                DECLARE @userID AS INT;
                DECLARE @count AS INT;
                DECLARE @getUserID AS CURSOR;
                SET @getUserID = CURSOR
                    FOR SELECT USR_ACCOUNT_ID
                        FROM   M_USERS
                        WHERE  REC_ACTIVE = 1;
                OPEN @getUserID;
                FETCH NEXT FROM @getUserID INTO @userID;
                WHILE @@FETCH_STATUS = 0
                    BEGIN
                        PRINT @userID;
                        SELECT   USR_ACCOUNT_ID,
                                 USR_SOURCE,
                                 GRUP_ID,
                                 PERMISSIONS_LIMITS_ON,
                                 JOB_TYPE,
                                 JOB_LIMIT,
                                 JOB_ISALLOWED,
                                 ALERT_LIMIT,
                                 ALLOWED_OVER_DRAFT
                        INTO     #UserDefaultTemp
                        FROM     M_USERS, T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                        WHERE    PERMISSIONS_LIMITS_ON = 1
                                 AND (CONVERT (VARCHAR (10), LAST_REFILLED_ON, 111) <> (SELECT CONVERT (VARCHAR (10), GETDATE(), 111))
                                      OR LAST_REFILLED_ON IS NULL)
                                 AND USR_ACCOUNT_ID = @userID
                        GROUP BY GRUP_ID, USR_ACCOUNT_ID, USR_SOURCE, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED, ALERT_LIMIT, ALLOWED_OVER_DRAFT
                        ORDER BY USR_ACCOUNT_ID, USR_SOURCE, JOB_TYPE;
                        SELECT *
                        INTO   #UserDefaultPLU
                        FROM   #UserDefaultTemp
                        WHERE  GRUP_ID = @userID;
                        --select * from #UserDefaultPLU  
                        UPDATE  T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                            SET LAST_REFILLED_ON = (SELECT getdate())
                        WHERE   GRUP_ID IN (SELECT DISTINCT #UserDefaultPLU.GRUP_ID
                                            FROM   #UserDefaultPLU);
                        SET @count = (SELECT count(*)
                                      FROM   #UserDefaultPLU);
                        DROP TABLE #UserDefaultTemp;
                        PRINT @count;
                        IF @count > 0
                            BEGIN
                                -- update with remaining limits  
                                IF @addwithOldLimits = ''Yes''
                                    BEGIN
                                        -- truncate table T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP  
                                        -- Copy PL in to Temp Table  
                                        INSERT INTO T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP (COSTCENTER_ID, USER_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
                                        SELECT COSTCENTER_ID,
                                               [USER_ID],
                                               PERMISSIONS_LIMITS_ON,
                                               JOB_TYPE,
                                               JOB_LIMIT,
                                               JOB_USED,
                                               ALERT_LIMIT,
                                               ALLOWED_OVER_DRAFT,
                                               JOB_ISALLOWED
                                        FROM   T_JOB_PERMISSIONS_LIMITS
                                        WHERE  COSTCENTER_ID = ''-1''
                                               AND [USER_ID] = @userID;
                                        SELECT getdate();
                                        PRINT @userID;
                                        SELECT *
                                        FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP;
                                        UPDATE  #UserDefaultPLU
                                            SET #UserDefaultPLU.JOB_LIMIT = #UserDefaultPLU.JOB_LIMIT + (T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_LIMIT - T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_USED)
                                        FROM    T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP
                                        WHERE   #UserDefaultPLU.JOB_LIMIT < 214748360
                                                AND #UserDefaultPLU.GRUP_ID = T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.USER_ID
                                                AND #UserDefaultPLU.JOB_TYPE = T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.JOB_TYPE;
                                        UPDATE  #UserDefaultPLU
                                            SET #UserDefaultPLU.JOB_LIMIT = 2147483647
                                        WHERE   #UserDefaultPLU.JOB_LIMIT > 2147483647;
                                        --truncate table T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP  
                                        SELECT *
                                        FROM   #UserDefaultPLU;
                                    END
                                -- Delete old Permissions and Limits  
                                DELETE T_JOB_PERMISSIONS_LIMITS
                                WHERE  PERMISSIONS_LIMITS_ON = 1
                                       AND [USER_ID] = @userID
                                       AND COSTCENTER_ID = ''-1'';
                                PRINT @userID;
                                -- insert auto refilled data  
                                INSERT INTO T_JOB_PERMISSIONS_LIMITS (COSTCENTER_ID, [USER_ID], PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_ISALLOWED, ALERT_LIMIT, ALLOWED_OVER_DRAFT)
                                SELECT ''-1'',
                                       GRUP_ID,
                                       PERMISSIONS_LIMITS_ON,
                                       JOB_TYPE,
                                       JOB_LIMIT,
                                       JOB_ISALLOWED,
                                       ALERT_LIMIT,
                                       ALLOWED_OVER_DRAFT
                                FROM   #UserDefaultPLU;
                            END
                        DROP TABLE #UserDefaultPLU;
                        FETCH NEXT FROM @getUserID INTO @userID;
                    END
                CLOSE @getUserID;
                DEALLOCATE @getUserID;
                UPDATE  T_AUTO_REFILL
                    SET LAST_REFILLED_ON   = getdate(),
                        IS_REFILL_REQUIRED = ''0''
                WHERE   AUTO_REFILL_FOR = @refillFor;
            END
    END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Backup_Restore]    Script Date: 26/11/2015 13:01:29 ******/
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
@BackupPath varchar(2000),
@UserSpecificName varchar(100)

	
AS
BEGIN
if @BackuporRestore = ''Backup''
begin

DECLARE
@MyFileName varchar(100)
SELECT
@MyFileName = (SELECT @BackupPath +@UserSpecificName+''_''+''AP_'' + convert(varchar(50),GetDate(),112) +''_''+REPLACE(CONVERT(VARCHAR(20),GETDATE(),108),'':'','''') +''.bak'')
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
		 
		SELECT TOP ( 500 )
		 s.database_name,
		 m.physical_device_name,
		 cast(CAST(s.backup_size / 1000000 AS int) as varchar(14))
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
/****** Object:  StoredProcedure [dbo].[Billing]    Script Date: 6/15/2016 11:44:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Billing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[Billing]
			
		@fromDate varchar(10),
		@toDate  varchar(10),
		@JobStatus varchar(100),
		@selectedCC nvarchar(100),
		@selectedMFP nvarchar(50),
		@authSource nvarchar(50),
		@domainName nvarchar(50),
		@sheetCount nvarchar(5)
		

AS
declare @rowCountJL int 
declare @selectTable varchar(10)
declare @sqlQuery nvarchar(max)
declare @sqlGroupOn nvarchar(100)
declare @sheetOrPaperCount nvarchar(50)

--set @sqlQuery = ''''
--exec @sqlQuery
--set @sqlGroupOn = ''MFP_IP,COST_CENTER_NAME''

if @sheetCount = ''A4''
begin
set @sheetOrPaperCount = ''OSA_JOB_COUNT''
end

if @sheetCount = ''A3''
begin
set @sheetOrPaperCount = ''JOB_SHEET_COUNT''
end

set @sqlGroupOn = ''COST_CENTER_NAME,MFP_IP''
create table #TempFilterMFP(FilterValue nvarchar(max))
insert into #TempFilterMFP(FilterValue) select TokenVal from ConvertStringListToTable(@selectedMFP, '','')

create table #TempFilterCostCenter(FilterValue nvarchar(max))
insert into #TempFilterCostCenter(FilterValue) select TokenVal from ConvertStringListToTable(@selectedCC, '','')

delete from #TempFilterMFP where FilterValue = N''-1''
delete from #TempFilterCostCenter where FilterValue = N''-1''
select * into #joblog from T_JOB_LOG where 1 = 2


set @sqlQuery = ''insert into #joblog(MFP_ID,GRUP_ID,COST_CENTER_NAME,MFP_IP,MFP_MACADDRESS,USR_ID,USR_DEPT,USR_SOURCE,JOB_ID,JOB_MODE,JOB_TYPE,JOB_COMPUTER,JOB_USRNAME,JOB_START_DATE,JOB_END_DATE,JOB_COLOR_MODE,JOB_SHEET_COUNT_COLOR,JOB_SHEET_COUNT_BW,JOB_SHEET_COUNT,JOB_PRICE_COLOR,JOB_PRICE_BW,JOB_PRICE_TOTAL,JOB_STATUS,JOB_PAPER_SIZE,JOB_FILE_NAME,DUPLEX_MODE,REC_DATE,JOB_PAPER_SIZE_ORIGINAL,JOB_USED_UPDATED,OSA_JOB_COUNT,DOMAIN_NAME,USER_ACCOUNT_ID,SERVER_IP,SERVER_LOCATION,SERVER_TOKEN_ID,JOB_JOB_NAME,NOTE,JOB_BALANCE_UPdated,DEPARTMENT,COMPANY,LOCATION,MFPNAME) select MFP_ID,GRUP_ID,COST_CENTER_NAME,MFP_IP,MFP_MACADDRESS,USR_ID,USR_DEPT,USR_SOURCE,JOB_ID,JOB_MODE,JOB_TYPE,JOB_COMPUTER,JOB_USRNAME,JOB_START_DATE,JOB_END_DATE,JOB_COLOR_MODE,JOB_SHEET_COUNT_COLOR,JOB_SHEET_COUNT_BW,JOB_SHEET_COUNT,JOB_PRICE_COLOR,JOB_PRICE_BW,JOB_PRICE_TOTAL,JOB_STATUS,JOB_PAPER_SIZE,JOB_FILE_NAME,DUPLEX_MODE,REC_DATE,JOB_PAPER_SIZE_ORIGINAL,JOB_USED_UPDATED,OSA_JOB_COUNT,DOMAIN_NAME,USER_ACCOUNT_ID,SERVER_IP,SERVER_LOCATION,SERVER_TOKEN_ID,JOB_JOB_NAME,NOTE,JOB_BALANCE_UPdated,DEPARTMENT,COMPANY,LOCATION,MFPNAME from T_JOB_LOG where 1 = 1 ''

set @sqlQuery = @sqlQuery + '' and JOB_STATUS in (select JOB_COMPLETED_TPYE from JOB_COMPLETED_STATUS where REC_STATUS = 1)''
set @sqlQuery = @sqlQuery + '' and (REC_DATE BETWEEN '''''' + @fromDate + '' 00:00:00'''' and '''''' + @toDate + '' 23:59:59'''')'' 



if @selectedMFP <> ''-1''
begin
	set @sqlQuery = @sqlQuery + '' and MFP_IP in(select FilterValue from #TempFilterMFP)''
end


if @selectedCC <> ''-1''
	begin
		set @sqlQuery = @sqlQuery + '' and GRUP_ID in(select FilterValue from #TempFilterCostCenter)''
	end

if @authSource = ''DB''
	begin
	set @sqlQuery = @sqlQuery + '' and DOMAIN_NAME = ''''Local'''''' 
	end


if @authSource = ''AD''
begin
print @authSource
	if @domainName = ''All''
	
		begin
		print @domainName
		set @sqlQuery = @sqlQuery + '' and DOMAIN_NAME not in (''''Local'''')'' 
		end
	else
	
		begin
		print ''123''
		print @domainName
		set @sqlQuery = @sqlQuery + '' and DOMAIN_NAME = '''''' + @domainName + '''''''' 
		end
end



print @sqlQuery
exec(@sqlQuery)
--SET IDENTITY_INSERT #joblog OFF
--insert into #joblog(COST_CENTER_NAME,MFP_IP,MFP_MACADDRESS) select COST_CENTER_NAME,MFP_IP,MFP_MACADDRESS from T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) ) and (GRUP_ID in (select TokenVal from ConvertStringListToTable(@selectedCC, ''''))) and (MFP_IP in (select TokenVal from ConvertStringListToTable(@selectedMFP, '''')))  and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 
--SET IDENTITY_INSERT #joblog ON

alter table #joblog add  MFP_MODEL nvarchar(50), MFP_LOCATION nvarchar(100)

create table #JobReport
(
      slno int identity,
      ReportOn nvarchar(100) default '''', MFP_IP nvarchar(100) default '''', ModelName nvarchar(100) default '''',Location nvarchar(200) default '''',GRUP_ID nvarchar(50),COST_CENTERNAME nvarchar(50), TotalPrint int default 0,TotalCopy int default 0,TotalScan int default 0,TotalFax int default 0
      ,PrintBW int default 0
      ,PrintC int default 0
      ,CopyBW int default 0
      ,CopyC int default 0
      ,ScanBW int default 0
      ,ScanC int default 0  
      ,FaxBW int default 0
      ,FaxC int default 0
	  ,Price float default 0
)

print @sqlGroupOn
set @sqlQuery = ''insert into #JobReport (''+ replace(@sqlGroupOn, ''COST_CENTER_NAME'', ''COST_CENTERNAME'') +'') select '' + @sqlGroupOn + '' from #joblog group by  '' + @sqlGroupOn + '' order by '' + @sqlGroupOn
exec(@sqlQuery)
create table #TempGroupCount(itemGroup nvarchar(100),itemGroup1 nvarchar(100), itemCount float default 0)
print @sqlQuery

-- PrintBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(''+@sheetOrPaperCount+'') from #joblog where  JOB_COLOR_MODE in(''''MONOCHROME'''') and JOB_MODE = ''''PRINT'''' or JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery

--select * from #TempGroupCount
update #JobReport set PrintBW = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount


-- PrintC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(''+@sheetOrPaperCount+'') from #joblog where  JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''PRINT'''' or JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery

--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set PrintC = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount

--CopyBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(''+@sheetOrPaperCount+'') from #joblog where  JOB_COLOR_MODE in(''''MONOCHROME'''') and JOB_MODE = ''''COPY'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set CopyBW = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount

--CopyC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(''+@sheetOrPaperCount+'') from #joblog where  JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''COPY'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set CopyC = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount

--ScanBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(''+@sheetOrPaperCount+'') from #joblog where JOB_COLOR_MODE in(''''MONOCHROME'''') and JOB_MODE in(''''SCANNER'''',''''Doc Filing Scan'''') group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set ScanBW = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount

--ScanC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(''+@sheetOrPaperCount+'') from #joblog where  JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE in(''''SCANNER'''',''''Doc Filing Scan'''') group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set ScanC = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount

--FaxBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(''+@sheetOrPaperCount+'') from #joblog where  JOB_COLOR_MODE in(''''MONOCHROME'''') and JOB_MODE = ''''FAX'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set FaxBW = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount

--FaxC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(''+@sheetOrPaperCount+'') from #joblog where  JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''FAX'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set FaxC = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount

--Price
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', isnull(sum(cast(cast(JOB_PRICE_TOTAL AS float) as DECIMAL(20, 2))),0)  from #joblog  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery

--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set Price = itemCount from #TempGroupCount where itemGroup = #JobReport.COST_CENTERNAME and itemGroup1 = #JobReport.MFP_IP
truncate table #TempGroupCount


	  update #JobReport set TotalPrint = PrintBW +PrintC,TotalCopy = CopyBW+CopyC,TotalScan = ScanBW+ScanC,TotalFax = FaxC+FaxBW



      update #JobReport set ModelName = M_MFPS.MFP_MODEL,  Location = M_MFPS.MFP_LOCATION  from M_MFPS where  M_MFPS.MFP_IP = #JobReport.MFP_IP

	  update #JobReport set #JobReport.COST_CENTERNAME = M_COST_CENTERS.COSTCENTER_NAME from M_COST_CENTERS where M_COST_CENTERS.COSTCENTER_ID = #JobReport.GRUP_ID

	  update #JobReport set #JobReport.COST_CENTERNAME = ''My Account'' where #JobReport.GRUP_ID = ''-1''
	  update #JobReport set #JobReport.COST_CENTERNAME = ''-'' where #JobReport.COST_CENTERNAME = '''' or  #JobReport.COST_CENTERNAME = null
      insert into #JobReport(ReportOn,Location,ModelName,GRUP_ID,COST_CENTERNAME, PrintBW,PrintC,TotalPrint,CopyBW,CopyC,TotalCopy ,ScanBW,ScanC,TotalScan,FaxBW,FaxC,TotalFax,Price)
      select    '''','''', ''Total'','''','''',isnull(sum(PrintBW),0),isnull(sum(PrintC),0),isnull(sum(TotalPrint),0),isnull(Sum(CopyBW),0),isnull(sum(CopyC),0),isnull(sum(TotalCopy),0),isnull(sum(ScanBW),0),isnull(sum(ScanC),0),isnull(sum(TotalScan),0), isnull(Sum(FaxBW),0),isnull(Sum(FaxC),0),isnull(sum(TotalFax),0),isnull(sum(cast(cast(Price AS float) as DECIMAL(28, 2))),0) from #JobReport
      --select SerialNumber as SerialNumber ,Total,TotalBW,TotalColor,A3PrintBW,A4PrintBW,A3PrintC,A4PrintC,OtherPrintBW,OtherPrintC,Duplex_One_Sided,Duplex_Two_Sided,ModelName,Location,MFPHOSTNAME,MFPIP from #JobReport
 --declare @columnName nvarchar(200)
 --set @columnName = @sqlGroupOn
 --set @columnName = replace(

set @sqlGroupOn = replace(@sqlGroupOn, ''COST_CENTER_NAME'', ''COST_CENTERNAME'')

declare @columnName varchar(100)
set @columnName = Replace(@sqlGroupOn, ''COST_CENTERNAME'', ''COST_CENTERNAME as [Cost Center]'')
set @columnName = Replace(@columnName, ''MFP_IP'', ''MFP_IP as [IP Address]'')

set @sqlQuery = ''select '' + @columnName + '', ModelName, Location, PrintBW, PrintC, TotalPrint, CopyBW, CopyC, TotalCopy, ScanBW, ScanC, TotalScan, FaxBW, FaxC, TotalFax, Price from #JobReport'' 
exec(@sqlQuery)
	 



' 
END
GO

/****** Object:  StoredProcedure [dbo].[BuildJobSummary]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BuildJobSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[BuildJobSummary] @selectedColumns varchar(100), @dataFilter nvarchar(max)
as 

declare @sqlQuery nvarchar(max)
declare @crossUpdateColumns varchar(300)
set @crossUpdateColumns =''''
CREATE TABLE #Summary(
	[Color] [int] NULL default 0,
	[ColorPrice] [float] NULL default 0,
	[BW] [int] NULL default 0,
	[BWPrice] [float] NULL default 0,
	[Location] nvarchar (200) NULL ,
	[Model] nvarchar (50) NULL,
	[Copy] [int] NULL default 0,
	[Print] [int] NULL default 0,
	[Scan] [int] NULL default 0,
	[Fax] [int] NULL default 0,
	[Duplex] [int] NULL default 0,
	[Total] [int] NULL default 0,
	[Price] [float] NULL default 0
) ON [PRIMARY]

create table #TempCounter(Total int)

select * into #SelectedColumns from ConvertStringListToTable(REPLACE(@selectedColumns, '' '', ''''), '','')
alter table #SelectedColumns add REC_SLNO int identity

--select * from  #SelectedColumns




declare @totalRows int
declare @rowIndex int
declare @columnName varchar(100)
if @dataFilter = '''' or @dataFilter = null
begin
	set @dataFilter = '' 1 = 1''
end

set @dataFilter = '' 1 = 1 and '' + @dataFilter
select @totalRows = count(*) from #SelectedColumns

set @rowIndex = 1
while @rowIndex <= @totalRows
begin
	select @columnName = TokenVal from #SelectedColumns where REC_SLNO = @rowIndex
	set @sqlQuery = ''alter table #Summary add ['' + @columnName + ''] nvarchar(150)''
	exec(@sqlQuery)
	set @sqlQuery = ''alter table #TempCounter add ['' + @columnName + ''] nvarchar(150)''
	exec(@sqlQuery)
	
	set @crossUpdateColumns += '' #TempCounter.'' + @columnName + '' = #Summary.'' + @columnName + '' and ''
	set @rowIndex = @rowIndex + 1
end

set @crossUpdateColumns = @crossUpdateColumns + '' 1 = 1''

--set @dataFilter = replace(@dataFilter, ''MFP_IP='', ''MFP_IP in'')
print @dataFilter


		set @sqlQuery = ''insert into #Summary('' + @selectedColumns + '',Color,ColorPrice,BW,BWPrice) select '' + @selectedColumns + '', sum(JOB_SHEET_COUNT_COLOR) as Color, isnull(sum(cast(cast(JOB_PRICE_COLOR AS float) as DECIMAL(20, 2))),0) as ColorPrice, sum(JOB_SHEET_COUNT_BW) as BW, isnull(sum(cast(cast(JOB_PRICE_BW AS float) as DECIMAL(20, 2))),0) as BWPrice from T_JOB_LOG WITH (NOLOCK) where '' + @dataFilter + '' group by '' + @selectedColumns 
		exec (@sqlQuery)
	



 --sum(JOB_SHEET_COUNT_COLOR) + sum(JOB_SHEET_COUNT_BW)
-- Print 
truncate table #TempCounter
set @sqlQuery = ''insert into #TempCounter('' + @selectedColumns + '',Total) select '' + @selectedColumns + '', sum(OSA_JOB_COUNT) as TotalCount from T_JOB_LOG WITH (NOLOCK) where '' + @dataFilter + '' and JOB_MODE = ''''PRINT'''' or JOB_MODE = ''''Doc Filing Print''''   group by '' + @selectedColumns 
exec(@sqlQuery)

set @sqlQuery = ''update #Summary set [Print] = #TempCounter.Total from #TempCounter where '' + @crossUpdateColumns
exec(@sqlQuery)

-- Scan
truncate table #TempCounter
set @sqlQuery = ''insert into #TempCounter('' + @selectedColumns + '',Total) select '' + @selectedColumns + '', sum(OSA_JOB_COUNT) as TotalCount from T_JOB_LOG WITH (NOLOCK) where '' + @dataFilter + '' and JOB_MODE = ''''SCANNER'''' or JOB_MODE = ''''Doc Filing Scan'''' group by '' + @selectedColumns 
exec(@sqlQuery)

set @sqlQuery = ''update #Summary set [Scan] = #TempCounter.Total from #TempCounter where '' + @crossUpdateColumns
exec(@sqlQuery)

-- Copy
truncate table #TempCounter
set @sqlQuery = ''insert into #TempCounter('' + @selectedColumns + '',Total) select '' + @selectedColumns + '', sum(OSA_JOB_COUNT) as TotalCount from T_JOB_LOG WITH (NOLOCK) where '' + @dataFilter + '' and JOB_MODE = ''''COPY'''' group by '' + @selectedColumns 
exec(@sqlQuery)

set @sqlQuery = ''update #Summary set [Copy] = #TempCounter.Total from #TempCounter where '' + @crossUpdateColumns
exec(@sqlQuery)

-- Fax
truncate table #TempCounter
set @sqlQuery = ''insert into #TempCounter('' + @selectedColumns + '',Total) select '' + @selectedColumns + '', sum(OSA_JOB_COUNT) as TotalCount from T_JOB_LOG WITH (NOLOCK) where '' + @dataFilter + '' and JOB_MODE = ''''FAX'''' group by '' + @selectedColumns 
exec(@sqlQuery)

-- Duplex
truncate table #TempCounter
set @sqlQuery = ''insert into #TempCounter('' + @selectedColumns + '',Total) select '' + @selectedColumns + '', count(JOB_SHEET_COUNT) as TotalCount from T_JOB_LOG WITH (NOLOCK) where '' + @dataFilter + '' and DUPLEX_MODE <> ''''1SIDED'''' group by '' + @selectedColumns 
exec(@sqlQuery)


set @sqlQuery = ''update #Summary set [Duplex] = #TempCounter.Total from #TempCounter where '' + @crossUpdateColumns
exec(@sqlQuery)


--select * from #TempCounter

update #Summary set [Total] = [Copy] + [Print] + [Scan] + [Fax] , [Price] = [ColorPrice] + [BWPrice]
if CHARINDEX(''MFP_IP'', @selectedColumns) > 0
begin
	set @sqlQuery = ''update #Summary set #Summary.MFP_IP = M_MFPS.MFP_HOST_NAME + '''' ['''' + M_MFPS.MFP_IP + '''']'''' from M_MFPS WITH (NOLOCK) where #Summary.MFP_IP = M_MFPS.MFP_IP and M_MFPS.MFP_IP <> M_MFPS.MFP_HOST_NAME and M_MFPS.MFP_HOST_NAME is not null''
	exec (@sqlQuery)
end

set @sqlQuery = ''select '' + @selectedColumns + '',[Color],[ColorPrice],[BW],[BWPrice],[Copy],[Print],[Scan],[Fax],[Duplex],[Total],[Price] from #Summary order by '' + @selectedColumns
print @sqlQuery
exec (@sqlQuery)


select sum([Color]) as [Color],sum([ColorPrice]) as [ColorPrice],sum([BW]) as [BW],sum([BWPrice]) as [BWPrice],sum([Copy]) as [Copy],sum([Print]) as [Print] ,sum([Scan]) as [Scan],sum([Fax]) as [Fax],sum([Duplex]) as [Duplex],sum([Total]) as [Total] ,isnull(sum(cast(cast(Price AS float) as DECIMAL(28, 2))),0) as [Price] from #Summary

drop table #Summary ' 
END
GO
/****** Object:  StoredProcedure [dbo].[CardMapping]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardMapping]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[CardMapping]  
as  
begin  
update M_USERS set USR_CARD_ID =TEMP_CARD_MAPPING.USR_CARD_ID,USR_PIN=TEMP_CARD_MAPPING.USR_PIN  from TEMP_CARD_MAPPING where M_USERS.USR_SOURCE=TEMP_CARD_MAPPING.USR_SOURCE and M_USERS.USR_ID=TEMP_CARD_MAPPING.USR_ID  and M_USERS.USR_DOMAIN=TEMP_CARD_MAPPING.USR_DOMAIN
truncate table TEMP_CARD_MAPPING  
end  ' 
END
GO
/****** Object:  StoredProcedure [dbo].[CopyCostCenterPermissionsAndLimits]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyCostCenterPermissionsAndLimits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CopyCostCenterPermissionsAndLimits]
	@CostCenterID int,
	@UserID int,
	@PermissionsAndLimitsOn int

AS
Begin
	select * into #CostCenterPermissionsAndLimits from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=@CostCenterID and USER_ID=''-1'' and PERMISSIONS_LIMITS_ON=''0''
	--select * from #CostCenterPermissionsAndLimits
	Declare @rowsCount int
	select @rowsCount = count(*) from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=@CostCenterID and USER_ID=@UserID and PERMISSIONS_LIMITS_ON=@PermissionsAndLimitsOn
	if @rowsCount = 0
	begin
		insert into T_JOB_PERMISSIONS_LIMITS(COSTCENTER_ID,USER_ID,PERMISSIONS_LIMITS_ON,JOB_TYPE,JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED) 
		select COSTCENTER_ID,@UserID,@PermissionsAndLimitsOn,JOB_TYPE,JOB_LIMIT,''0'',ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED from #CostCenterPermissionsAndLimits
	end
	drop table #CostCenterPermissionsAndLimits
End' 
END
GO
/****** Object:  StoredProcedure [dbo].[CopyDefaultPermissionsAndLimits]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyDefaultPermissionsAndLimits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CopyDefaultPermissionsAndLimits]
	@UserID int,
	@PermissionsAndLimitsOn int

AS
Begin
	select * into #DefaultPermissionsAndLimits from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=''1'' and USER_ID=''-1'' and PERMISSIONS_LIMITS_ON=''0''	
	Declare @rowsCount int
	select @rowsCount = count(*) from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=''-1'' and USER_ID=@UserID and PERMISSIONS_LIMITS_ON=@PermissionsAndLimitsOn
	if @rowsCount = 0
	begin
		insert into T_JOB_PERMISSIONS_LIMITS(COSTCENTER_ID,USER_ID,PERMISSIONS_LIMITS_ON,JOB_TYPE,JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED) 
		select ''-1'',@UserID,@PermissionsAndLimitsOn,JOB_TYPE,JOB_LIMIT,''0'',ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED from #DefaultPermissionsAndLimits		
	end
	drop table #DefaultPermissionsAndLimits
End' 
END
GO
/****** Object:  StoredProcedure [dbo].[CounterDetails]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CounterDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[CounterDetails]
@ipaddress nvarchar(30)
as 

declare @serialNumber nvarchar(50)

select @serialNumber=MFP_SERIALNUMBER from M_MFPS where MFP_IP=@ipaddress



--Select Distinct top 1  T1.REC_CDATE,T1.MAC_ADDRESS,T1.MODEL_NAME,T1.SERIAL_NUMBER,T1.PRINT_TOTAL,T1.PRINT_COLOR,T1.PRINT_BW,T1.DUPLEX,T1.COPIES,T1.COPY_BW,T1.COPY_COLOR,T1.TWO_COLOR_COPY_COUNT,T1.SINGLE_COLOR_COPY_COUNT,T1.BW_TOTAL_COUNT,T1.FULL_COLOR_TOTAL_COUNT,T1.TWO_COLOR_TOTAL_COUNT,T1.SINGLE_COLOR_TOTAL_COUNT,T1.BW_OTHER_COUNT,T1.FULL_COLOR_OTHER_COUNT,T1.SCAN_TO_HDD,T1.BW_SCAN_TO_HDD,T1.COLOR_SCAN_TO_HDD,T1.TWO_COLOR_SCAN_HDD,T1.TOTAL_DOC_FILING_PRINT,T1.BW_DOC_FILING_PRINT,T1.COLOR_DOC_FILING_PRINT,T1.TWO_COLOR_DOC_FILING_PRINT,T1.DOCUMENT_FEEDER,T1.FAX_SEND,T1.FAX_RECEIVE,T1.IFAX_SEND_COUNT,T1.TOTAL_SCAN_TO_EMAIL_FTP,T1.BW_SCAN,T1.COLOR_SCAN,T2.TRAY1,T2.TRAY2,T2.TRAY3,T2.TRAY4,T2.TRAY5,T2.TRAY6,T3.CYAN,T3.YELLOW,T3.MAGENTA,T3.BLACK From MFP_CLICK T1,MFP_PAPER T2,MFP_TONER T3 WHERE T1.SERIAL_NUMBER=T2.SERIAL_NUMBER and T2.SERIAL_NUMBER = T3.SERIAL_NUMBER and T1.SERIAL_NUMBER=@serialNumber order by REC_CDATE desc

Select Distinct top 1  T1.REC_ID,T1.MAC_ADDRESS,T1.MODEL_NAME,T1.SERIAL_NUMBER,T1.PRINT_TOTAL,T1.PRINT_COLOR,T1.PRINT_BW,T1.DUPLEX,T1.COPIES,T1.COPY_BW,T1.COPY_COLOR,T1.TWO_COLOR_COPY_COUNT,T1.SINGLE_COLOR_COPY_COUNT,T1.BW_TOTAL_COUNT,T1.FULL_COLOR_TOTAL_COUNT,T1.TWO_COLOR_TOTAL_COUNT,T1.SINGLE_COLOR_TOTAL_COUNT,T1.BW_OTHER_COUNT,T1.FULL_COLOR_OTHER_COUNT,T1.SCAN_TO_HDD,T1.BW_SCAN_TO_HDD,T1.COLOR_SCAN_TO_HDD,T1.TWO_COLOR_SCAN_HDD,T1.TOTAL_DOC_FILING_PRINT,T1.BW_DOC_FILING_PRINT,T1.COLOR_DOC_FILING_PRINT,T1.TWO_COLOR_DOC_FILING_PRINT,T1.DOCUMENT_FEEDER,T1.TOTAL_SCAN_TO_EMAIL_FTP,T1.BW_SCAN,T1.COLOR_SCAN,T1.REC_CDATE,T1.REC_MDATE,T1.FAX_SEND,T1.FAX_RECEIVE,T1.IFAX_SEND_COUNT,T2.TRAY1,T2.TRAY2,T2.TRAY3,T2.TRAY4,T2.TRAY5,T2.TRAY6,T3.CYAN,T3.YELLOW,T3.MAGENTA,T3.BLACK From MFP_CLICK T1,MFP_PAPER T2,MFP_TONER T3 WHERE T1.SERIAL_NUMBER=T2.SERIAL_NUMBER and T2.SERIAL_NUMBER = T3.SERIAL_NUMBER and T1.SERIAL_NUMBER=@serialNumber order by REC_ID desc

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCostCenter]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteCostCenter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[DeleteCostCenter] @CostCenterList text
AS
BEGIN
	select TokenVal into #SelectedValues from ConvertStringListToTable(@CostCenterList, '','')
	
	-- Delete Users assigned to the selected Cost Centers
	delete from T_COSTCENTER_USERS where COST_CENTER_ID in (select TokenVal from #SelectedValues)
	
	-- Delete Permissions and Limits
	delete from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID in (select TokenVal from #SelectedValues)

	-- Delete the Cost Center List
	delete from M_COST_CENTERS where COSTCENTER_ID in (select TokenVal from #SelectedValues)
	
	update T_ACCESS_RIGHTS set REC_STATUS = ''0'' where  USER_OR_COSTCENTER_ID in (select TokenVal from #SelectedValues)
	
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteDomains]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteDomains]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[DeleteDomains] @DomainsList text
AS
BEGIN
	select TokenVal into #SelectedValues from ConvertStringListToTable(@DomainsList, '','')
	
	-- Delete Domains
	delete from AD_SETTINGS where AD_DOMAIN_NAME in (select TokenVal from #SelectedValues)
	
	
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUsers]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[DeleteUsers] @selectedUsers text 
as 
select TokenVal into #SelectedValues from ConvertStringListToTable(@selectedUsers, '','')
delete from #SelectedValues where TokenVal in (select USR_ACCOUNT_ID from M_USERS where USR_ID=''admin'' and USR_SOURCE=''DB'')
delete from M_USERS where USR_ACCOUNT_ID in (select TokenVal from #SelectedValues)
delete from T_ACCESS_RIGHTS where ASSIGN_TO = ''User'' and USER_OR_COSTCENTER_ID in (select TokenVal from #SelectedValues)
delete from T_JOB_PERMISSIONS_LIMITS where PERMISSIONS_LIMITS_ON = 1 and USER_ID in (select TokenVal from #SelectedValues)
delete from T_COSTCENTER_USERS where USR_ACCOUNT_ID in (select TokenVal from #SelectedValues)' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeviceMfpIP]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeviceMfpIP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[DeviceMfpIP]
@ipaddress nvarchar(30)
as 

declare @serialNumber nvarchar(50)

select @serialNumber=MFP_SERIALNUMBER from M_MFPS where MFP_IP=@ipaddress

Select Distinct T1.REC_CDATE,T1.MAC_ADDRESS,T1.MODEL_NAME,T1.SERIAL_NUMBER,T1.PRINT_TOTAL,T1.PRINT_COLOR,T1.PRINT_BW,T1.DUPLEX,T1.COPIES,T1.COPY_BW,T1.COPY_COLOR,T1.TWO_COLOR_COPY_COUNT,T1.SINGLE_COLOR_COPY_COUNT,T1.BW_TOTAL_COUNT,T1.FULL_COLOR_TOTAL_COUNT,T1.TWO_COLOR_TOTAL_COUNT,T1.SINGLE_COLOR_TOTAL_COUNT,T1.BW_OTHER_COUNT,T1.FULL_COLOR_OTHER_COUNT,T1.SCAN_TO_HDD,T1.BW_SCAN_TO_HDD,T1.COLOR_SCAN_TO_HDD,T1.TWO_COLOR_SCAN_HDD,T1.TOTAL_DOC_FILING_PRINT,T1.BW_DOC_FILING_PRINT,T1.COLOR_DOC_FILING_PRINT,T1.TWO_COLOR_DOC_FILING_PRINT,T1.DOCUMENT_FEEDER,T1.FAX_SEND,T1.FAX_RECEIVE,T1.IFAX_SEND_COUNT,T1.TOTAL_SCAN_TO_EMAIL_FTP,T1.BW_SCAN,T1.COLOR_SCAN,T2.TRAY1,T2.TRAY2,T2.TRAY3,T2.TRAY4,T2.TRAY5,T2.TRAY6,T3.CYAN,T3.YELLOW,T3.MAGENTA,T3.BLACK From MFP_CLICK T1,MFP_PAPER T2,MFP_TONER T3 WHERE T1.SERIAL_NUMBER=T2.SERIAL_NUMBER and T2.SERIAL_NUMBER = T3.SERIAL_NUMBER and T1.SERIAL_NUMBER=@serialNumber order by REC_CDATE desc




' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeviceUsageSummary]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeviceUsageSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[DeviceUsageSummary]
as 

select 
REC_ID, ''                                                  '' as REC_CID, GetDate() as READING_DATE, 0 as DEVICE_ID,MAC_ADDRESS,''IP Address of MFP....................'' as IP_ADDRESS,MODEL_NAME,SERIAL_NUMBER,
PRINT_TOTAL,PRINT_COLOR,PRINT_BW,DUPLEX,COPIES,COPY_BW,COPY_COLOR,TWO_COLOR_COPY_COUNT,
SINGLE_COLOR_COPY_COUNT,BW_TOTAL_COUNT,FULL_COLOR_TOTAL_COUNT,TWO_COLOR_TOTAL_COUNT,
SINGLE_COLOR_TOTAL_COUNT,BW_OTHER_COUNT,FULL_COLOR_OTHER_COUNT,SCAN_TO_HDD,BW_SCAN_TO_HDD,
COLOR_SCAN_TO_HDD,TOTAL_DOC_FILING_PRINT,BW_DOC_FILING_PRINT,COLOR_DOC_FILING_PRINT,
TWO_COLOR_DOC_FILING_PRINT,DOCUMENT_FEEDER,TOTAL_SCAN_TO_EMAIL_FTP,BW_SCAN,COLOR_SCAN,TWO_COLOR_SCAN_HDD,FAX_SEND,FAX_RECEIVE,IFAX_SEND_COUNT
into #DeviceUsageSummary from MFP_CLICK where 1 = 2

alter table #DeviceUsageSummary add CYAN int default 0
alter table #DeviceUsageSummary add YELLOW int default 0
alter table #DeviceUsageSummary add MAGENTA int default 0
alter table #DeviceUsageSummary add BLACK int default 0

alter table #DeviceUsageSummary add TRAY1 int default 0
alter table #DeviceUsageSummary add TRAY2 int default 0
alter table #DeviceUsageSummary add TRAY3 int default 0
alter table #DeviceUsageSummary add TRAY4 int default 0

alter table #DeviceUsageSummary add TRAY5 int default 0
alter table #DeviceUsageSummary add TRAY6 int default 0
alter table #DeviceUsageSummary add TRAY7 int default 0
alter table #DeviceUsageSummary add TRAY8 int default 0
alter table #DeviceUsageSummary add REC_CDATE datetime 
alter table #DeviceUsageSummary add REC_MDATE datetime 


insert into #DeviceUsageSummary(REC_CID,READING_DATE,DEVICE_ID, MAC_ADDRESS,IP_ADDRESS,MODEL_NAME,SERIAL_NUMBER) select 
'''', GetDate(), MFP_ID, MFP_MAC_ADDRESS, MFP_IP, MFP_MODEL, MFP_SERIALNUMBER from M_MFPS

declare @totalDevices int
declare @serialNumber nvarchar(50)

select @totalDevices = COUNT(REC_ID) from #DeviceUsageSummary

select * into #TONERDETAILS from MFP_TONER where 1 = 2
select * into #PAPERDETAILS from MFP_PAPER where 1 = 2
select * into #CLICKDETAILS from MFP_CLICK where 1 = 2


while @totalDevices > 0
begin
	--print @totalDevices
	
		select @serialNumber = SERIAL_NUMBER from #DeviceUsageSummary where REC_ID = @totalDevices
		
		--select top 1 * into #TONER from MFP_TONER where SERIAL_NUMBER = @serialNumber order by REC_ID desc
		
		insert into #TONERDETAILS(SERIAL_NUMBER,CYAN,YELLOW,MAGENTA,BLACK) select top 1 SERIAL_NUMBER,CYAN,YELLOW,MAGENTA,BLACK from MFP_TONER where SERIAL_NUMBER = @serialNumber order by REC_ID desc
		update #DeviceUsageSummary set 
			#DeviceUsageSummary.CYAN = #TONERDETAILS.CYAN,
			#DeviceUsageSummary.YELLOW = #TONERDETAILS.YELLOW,
			#DeviceUsageSummary.MAGENTA = #TONERDETAILS.MAGENTA,
			#DeviceUsageSummary.BLACK = #TONERDETAILS.BLACK 
		from #TONERDETAILS where  #DeviceUsageSummary.SERIAL_NUMBER = #TONERDETAILS.SERIAL_NUMBER

		
		insert into #CLICKDETAILS
		(
			SERIAL_NUMBER,PRINT_TOTAL,PRINT_COLOR,PRINT_BW,DUPLEX,COPIES,COPY_BW,COPY_COLOR,
			TWO_COLOR_COPY_COUNT,SINGLE_COLOR_COPY_COUNT,BW_TOTAL_COUNT,FULL_COLOR_TOTAL_COUNT,TWO_COLOR_TOTAL_COUNT,SINGLE_COLOR_TOTAL_COUNT,BW_OTHER_COUNT,FULL_COLOR_OTHER_COUNT,SCAN_TO_HDD,BW_SCAN_TO_HDD,COLOR_SCAN_TO_HDD,TOTAL_DOC_FILING_PRINT,BW_DOC_FILING_PRINT,COLOR_DOC_FILING_PRINT,TWO_COLOR_DOC_FILING_PRINT,DOCUMENT_FEEDER,TOTAL_SCAN_TO_EMAIL_FTP,
			BW_SCAN,COLOR_SCAN,REC_CDATE,REC_MDATE,TWO_COLOR_SCAN_HDD,FAX_SEND,FAX_RECEIVE,IFAX_SEND_COUNT
		) 
		select top 1 
			SERIAL_NUMBER,PRINT_TOTAL,PRINT_COLOR,PRINT_BW,DUPLEX,COPIES,COPY_BW,COPY_COLOR,
			TWO_COLOR_COPY_COUNT,SINGLE_COLOR_COPY_COUNT,BW_TOTAL_COUNT,FULL_COLOR_TOTAL_COUNT,TWO_COLOR_TOTAL_COUNT,SINGLE_COLOR_TOTAL_COUNT,BW_OTHER_COUNT,FULL_COLOR_OTHER_COUNT,SCAN_TO_HDD,BW_SCAN_TO_HDD,COLOR_SCAN_TO_HDD,TOTAL_DOC_FILING_PRINT,BW_DOC_FILING_PRINT,COLOR_DOC_FILING_PRINT,TWO_COLOR_DOC_FILING_PRINT,DOCUMENT_FEEDER,TOTAL_SCAN_TO_EMAIL_FTP,
			BW_SCAN,COLOR_SCAN,REC_CDATE,REC_MDATE,TWO_COLOR_SCAN_HDD,FAX_SEND,FAX_RECEIVE,IFAX_SEND_COUNT
			from MFP_CLICK where SERIAL_NUMBER = @serialNumber order by REC_ID desc 
	
		
		update #DeviceUsageSummary set
			#DeviceUsageSummary.PRINT_TOTAL = #CLICKDETAILS.PRINT_TOTAL,
			#DeviceUsageSummary.PRINT_COLOR = #CLICKDETAILS.PRINT_COLOR,
			#DeviceUsageSummary.PRINT_BW = #CLICKDETAILS.PRINT_BW,
			#DeviceUsageSummary.DUPLEX = #CLICKDETAILS.DUPLEX,
			#DeviceUsageSummary.COPIES = #CLICKDETAILS.COPIES,
			#DeviceUsageSummary.COPY_BW = #CLICKDETAILS.COPY_BW,
			#DeviceUsageSummary.COPY_COLOR = #CLICKDETAILS.COPY_COLOR,
			#DeviceUsageSummary.TWO_COLOR_COPY_COUNT = #CLICKDETAILS.TWO_COLOR_COPY_COUNT,
			#DeviceUsageSummary.SINGLE_COLOR_COPY_COUNT = #CLICKDETAILS.SINGLE_COLOR_COPY_COUNT,
			#DeviceUsageSummary.BW_TOTAL_COUNT = #CLICKDETAILS.BW_TOTAL_COUNT,
			#DeviceUsageSummary.FULL_COLOR_TOTAL_COUNT = #CLICKDETAILS.FULL_COLOR_TOTAL_COUNT,
			#DeviceUsageSummary.TWO_COLOR_TOTAL_COUNT = #CLICKDETAILS.TWO_COLOR_TOTAL_COUNT,
			#DeviceUsageSummary.SINGLE_COLOR_TOTAL_COUNT = #CLICKDETAILS.SINGLE_COLOR_TOTAL_COUNT,
			#DeviceUsageSummary.BW_OTHER_COUNT = #CLICKDETAILS.BW_OTHER_COUNT,
			#DeviceUsageSummary.FULL_COLOR_OTHER_COUNT = #CLICKDETAILS.FULL_COLOR_OTHER_COUNT,
			#DeviceUsageSummary.SCAN_TO_HDD = #CLICKDETAILS.SCAN_TO_HDD,
			#DeviceUsageSummary.BW_SCAN_TO_HDD = #CLICKDETAILS.BW_SCAN_TO_HDD,
			#DeviceUsageSummary.COLOR_SCAN_TO_HDD = #CLICKDETAILS.COLOR_SCAN_TO_HDD,
			#DeviceUsageSummary.TOTAL_DOC_FILING_PRINT = #CLICKDETAILS.TOTAL_DOC_FILING_PRINT,
			#DeviceUsageSummary.BW_DOC_FILING_PRINT = #CLICKDETAILS.BW_DOC_FILING_PRINT,
			#DeviceUsageSummary.COLOR_DOC_FILING_PRINT = #CLICKDETAILS.COLOR_DOC_FILING_PRINT,
			#DeviceUsageSummary.TWO_COLOR_DOC_FILING_PRINT = #CLICKDETAILS.TWO_COLOR_DOC_FILING_PRINT,
			#DeviceUsageSummary.DOCUMENT_FEEDER = #CLICKDETAILS.DOCUMENT_FEEDER,
			#DeviceUsageSummary.TOTAL_SCAN_TO_EMAIL_FTP = #CLICKDETAILS.TOTAL_SCAN_TO_EMAIL_FTP,
			#DeviceUsageSummary.BW_SCAN = #CLICKDETAILS.BW_SCAN,
			#DeviceUsageSummary.COLOR_SCAN = #CLICKDETAILS.COLOR_SCAN,
			#DeviceUsageSummary.REC_CDATE = #CLICKDETAILS.REC_CDATE,
			#DeviceUsageSummary.REC_MDATE = #CLICKDETAILS.REC_MDATE,
			#DeviceUsageSummary.TWO_COLOR_SCAN_HDD = #CLICKDETAILS.TWO_COLOR_SCAN_HDD,
			#DeviceUsageSummary.FAX_SEND = #CLICKDETAILS.FAX_SEND,
			#DeviceUsageSummary.FAX_RECEIVE = #CLICKDETAILS.FAX_RECEIVE,
			#DeviceUsageSummary.IFAX_SEND_COUNT = #CLICKDETAILS.IFAX_SEND_COUNT
			
		from #CLICKDETAILS where  #DeviceUsageSummary.SERIAL_NUMBER = #CLICKDETAILS.SERIAL_NUMBER
	
		insert into #PAPERDETAILS(SERIAL_NUMBER,TRAY1,TRAY2,TRAY3,TRAY4,TRAY5,TRAY6,TRAY7,TRAY8) select top 1 SERIAL_NUMBER,TRAY1,TRAY2,TRAY3,TRAY4,TRAY5,TRAY6,TRAY7,TRAY8 from MFP_PAPER where SERIAL_NUMBER = @serialNumber order by REC_ID desc
		
		update #DeviceUsageSummary set
			#DeviceUsageSummary.TRAY1 = #PAPERDETAILS.TRAY1,
			#DeviceUsageSummary.TRAY2 = #PAPERDETAILS.TRAY2,
			#DeviceUsageSummary.TRAY3 = #PAPERDETAILS.TRAY3,
			#DeviceUsageSummary.TRAY4 = #PAPERDETAILS.TRAY4,
			#DeviceUsageSummary.TRAY5 = #PAPERDETAILS.TRAY5,
			#DeviceUsageSummary.TRAY6 = #PAPERDETAILS.TRAY6,
			#DeviceUsageSummary.TRAY7 = #PAPERDETAILS.TRAY7,
			#DeviceUsageSummary.TRAY8 = #PAPERDETAILS.TRAY8
		from #PAPERDETAILS where  #DeviceUsageSummary.SERIAL_NUMBER = #PAPERDETAILS.SERIAL_NUMBER
		
	set @totalDevices = @totalDevices - 1
	
end

update #DeviceUsageSummary set REC_CID = SUB_CID from M_COUNTER_SUBSCRIPTION
select * from #DeviceUsageSummary' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmailSetting]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailSetting]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[EmailSetting] 
	
AS
BEGIN
	select MFP_ID,MFP_IP,EMAIL_ID,EMAIL_HOST,EMAIL_PORT,EMAIL_USERNAME,EMAIL_PASSWORD,EMAIL_REQUIRE_SSL,EMAIL_DIRECT_PRINT,
FTP_PROTOCOL,FTP_ADDRESS,FTP_PORT,FTP_USER_ID,FTP_USER_PASSWORD
from M_MFPS 

SELECT DISTINCT EMAIL_ID FROM M_MFPS 
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ExecuteData]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExecuteData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ExecuteData]
AS
BEGIN
	declare @buildVersion nvarchar(50)
	declare @SQL nvarchar(2000)
	select @buildVersion = VERSION from APP_VERSION
	--print SUBSTRING(@buildVersion, 5, 3)
	--print (@buildVersion)
	if(SUBSTRING(@buildVersion, 5, 3) < 519)
		begin
			set @SQL=''UPGRADE_TABLE_440_VERSION''
			exec @SQL
			set @SQL=''UPGRADE_DATA_440_VERSION''
			exec @SQL
		end
	else if(SUBSTRING(@buildVersion, 5, 3) > 519 and  SUBSTRING(@buildVersion, 5, 3) < 550)
	begin
		set @SQL=''UPGRADE_TABLE_519_VERSION''
		exec @SQL
		set @SQL=''UPGRADE_DATA_519_VERSION''
		exec @SQL
	end
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomPin]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerateRandomPin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GenerateRandomPin]
	@userIDs nvarchar (MAX),
	@selectedSource nvarchar (4)
AS
BEGIN

DECLARE @userID INT
DECLARE @getUserID AS CURSOR;
DECLARE @start nvarchar(50)
DECLARE @end nvarchar(50)
DECLARE @Length nvarchar(50)
DECLARE @sqlquery nvarchar(max)
DECLARE @pinQuery varchar(max)
DECLARE @DataSource NVARCHAR(max)
DECLARE @SQL NVARCHAR(max)
declare @qwe varchar (max)


set @Length = (select APPSETNG_VALUE from APP_SETTINGS where APPSETNG_KEY = ''Pin Length'' )

set @start = (select  LEFT( CAST(''10'' AS VARCHAR(20)) +REPLICATE(''0'', @Length), @Length))
set @end =(select  LEFT( CAST(''99'' AS VARCHAR(20)) +REPLICATE(''9'', @Length), @Length))



IF @userIDs = '''' 

	BEGIN
	
		SET @getUserID = CURSOR FOR SELECT USR_ACCOUNT_ID FROM dbo.M_USERS WHERE usr_source in(SELECT TokenVal FROM ConvertStringListToTable(@selectedSource, '',''))  and   USR_PIN = '''' OR USR_PIN = NULL 
		OPEN @getUserID;
		FETCH NEXT FROM @getUserID INTO @userID;
		WHILE @@FETCH_STATUS = 0
			BEGIN
			--PRINT @userID
				set @DataSource =  ''(select RIGHT(''+@start+'' + CAST(ABS(CHECKSUM(NEWID())) % ''+@end+'' AS varchar(''+@Length+'')),''+@Length+''))''
				SET @SQL = N''SELECT  @pinQuery =''+ @DataSource;
				EXEC sp_executesql @SQL, N''@pinQuery varchar(MAX) OUTPUT'', @pinQuery OUTPUT;
				set @pinQuery = ''''+@pinQuery+''''
				print @pinQuery
				IF NOT EXISTS (SELECT USR_PIN FROM dbo.M_USERS WHERE usr_source in(SELECT TokenVal FROM ConvertStringListToTable(@selectedSource, '',''))  and USR_ACCOUNT_ID = @userID and  USR_PIN <> '''' OR USR_PIN <> NULL ) 
					BEGIN
						IF NOT EXISTS (SELECT USER_COMMAND FROM M_USERS where USER_COMMAND = @pinQuery)
							begin
								set @sqlquery = ''update M_USERS set USR_PIN = ''''''+@pinQuery+'''''' , USER_COMMAND = ''''''+@pinQuery+'''''' where USR_ACCOUNT_ID = ''+ cast( CONVERT(VARCHAR(12), @userID) as varchar)
								exec (@sqlquery)
							end

					END

				FETCH NEXT FROM @getUserID INTO @userID;
				END
				CLOSE @getUserID
				DEALLOCATE @getUserID
			END
Else	
BEGIN
		SET @getUserID = CURSOR FOR SELECT TokenVal FROM ConvertStringListToTable(@userIDs, '','');
		OPEN @getUserID;
		FETCH NEXT FROM @getUserID INTO @userID;
		WHILE @@FETCH_STATUS = 0
			BEGIN
			--PRINT @userID
				set @DataSource =  ''(select RIGHT(''+@start+'' + CAST(ABS(CHECKSUM(NEWID())) % ''+@end+'' AS varchar(''+@Length+'')),''+@Length+''))''
				SET @SQL = N''SELECT  @pinQuery =''+ @DataSource;
				EXEC sp_executesql @SQL, N''@pinQuery varchar(MAX) OUTPUT'', @pinQuery OUTPUT;

				IF NOT EXISTS (SELECT USR_PIN FROM dbo.M_USERS WHERE usr_source in(SELECT TokenVal FROM ConvertStringListToTable(@selectedSource, '',''))  and USR_ACCOUNT_ID = @userID and  USR_PIN <> '''' OR USR_PIN <> NULL  ) 
					BEGIN
						IF NOT EXISTS (SELECT USER_COMMAND FROM M_USERS where USER_COMMAND = @pinQuery)
							begin
								set @sqlquery = ''update M_USERS set USR_PIN = ''''''+@pinQuery+'''''', USER_COMMAND = ''''''+@pinQuery+'''''' where USR_ACCOUNT_ID = ''+ cast( CONVERT(VARCHAR(12), @userID) as varchar)
								exec (@sqlquery)
							end

					END

				FETCH NEXT FROM @getUserID INTO @userID;
				END
				CLOSE @getUserID
				DEALLOCATE @getUserID
			END
END

if @userIDs = ''''
begin
	if exists(select USR_ACCOUNT_ID from M_USERS WHERE usr_source in(SELECT TokenVal FROM ConvertStringListToTable(@selectedSource, '',''))  and  USR_PIN <> '''' OR USR_PIN <> NULL )
		begin
			exec [GenerateRandomPin] '''' ,@selectedSource
		end
end

' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAccessRights]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAccessRights]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetAccessRights]
@userAccountID nvarchar(50), 
@selectedCostCenter nvarchar(50),  
@deviceIpAddress nvarchar(50),
@limitsOn nvarchar(50)

as
begin
declare @count INT 
declare @SQL varchar(max)
	if(@limitsOn = ''Cost Center'')
		begin
			declare @MFPID int
			-- Get the MFPID for the Given IP address
			select @MFPID = MFP_ID from M_MFPS where MFP_IP = ''''+@deviceIpAddress+''''
			-- Check Access Rights for Cost Center and MFP
			select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=''''+@selectedCostCenter+'''' and MFP_OR_GROUP_ID='''' + @MFPID + '''' and REC_STATUS=''1''
			print (@SQL)
			if(@SQL = ''0'')
			begin
				-- Check Access Rights for Cost Center and MFP Group
				select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=''''+@selectedCostCenter+'''' and REC_STATUS=''1'' and MFP_OR_GROUP_ID in (select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
				print (@SQL)
			end
		end
	else
		begin
			declare @MFPIDUSER int
			declare @MYACCOUNT varchar(max)
			declare @USRMYACCOUNT varchar(max)
			-- Get the MFPID for the Given IP address
			select @MYACCOUNT =  APPSETNG_VALUE from APP_SETTINGS where APPSETNG_KEY = ''My Account''
			print @MYACCOUNT
			select @USRMYACCOUNT = USR_MY_ACCOUNT from M_USERS where USR_ACCOUNT_ID = ''''+@userAccountID+''''
			print @USRMYACCOUNT
			if(@MYACCOUNT = ''Enable'')
				begin
					if(@USRMYACCOUNT = ''1'')
						begin
							select @MFPIDUSER = MFP_ID from M_MFPS where MFP_IP = ''''+@deviceIpAddress+''''
							select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID='''' + @userAccountID + '''' and MFP_OR_GROUP_ID='''' + @MFPIDUSER + '''' and REC_STATUS=''1''
							print (@SQL)
							if(@SQL = ''0'')
								begin
									select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=@userAccountID and REC_STATUS=''1'' and MFP_OR_GROUP_ID in (select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
									--print (@SQL)
								end
						end
					else 
						begin
							if (@USRMYACCOUNT is null)	
								begin
									if(@MYACCOUNT = ''Enable'')
										begin
											select @sql = ''1''
										end
									else
										begin
											select @sql = ''0''
										end
								
								end
							if(@USRMYACCOUNT = ''0'')
								begin
									select @SQL = ''0''
								end
						end
				end
			else
				begin
				 select @SQL = ''0''
				end
		end
	select @SQL as count
end' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetActiveDirectoryDetails]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetActiveDirectoryDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetActiveDirectoryDetails]
as 
set nocount on
create table #ActiveDirectoryDetails
(
	SerialNumber int identity,	
    AD_DOMAIN_NAME nvarchar(200),
	PIN_FIELD nvarchar(200),
	AD_PASSWORD nvarchar(200),
	IS_CARD_ENABLED nvarchar(200),
	AD_PORT nvarchar(200),
	IS_PIN_ENABLED nvarchar(200),
	AD_FULLNAME nvarchar(200),
	DOMAIN_NAME nvarchar(200),
	DOMAIN_CONTROLLER nvarchar(200),
	AD_USERNAME nvarchar(200),
	CARD_FIELD nvarchar(200),
	AD_ALIAS nvarchar(200)
)

declare @totalADSettings int
set @totalADSettings = 11

declare @AD_SETTING_KEY nvarchar(200)
declare @AD_SETTING_VALUE nvarchar(200)
declare @AD_DOMAIN_NAME nvarchar(200)

DECLARE adSettingsCursor CURSOR  
 LOCAL  
 FAST_FORWARD  
 READ_ONLY for select AD_SETTING_KEY, AD_SETTING_VALUE, AD_DOMAIN_NAME from AD_SETTINGS order by SLNO      
 OPEN adSettingsCursor  
 FETCH NEXT FROM adSettingsCursor into @AD_SETTING_KEY, @AD_SETTING_VALUE, @AD_DOMAIN_NAME   
 
 declare @sqlQuery nvarchar(2000)
 declare @settingsCount int
 set @settingsCount = 0 

 declare @settingsIndex int
 set @settingsIndex = 0 

 WHILE @@FETCH_STATUS = 0  
  Begin  
	--print @AD_SETTING_KEY
	
	if @settingsCount % 11 = 0
		Begin
			set @settingsIndex = @settingsIndex + 1
			set @sqlQuery = ''insert into #ActiveDirectoryDetails('' + @AD_SETTING_KEY + '',AD_DOMAIN_NAME) values('''''' + @AD_SETTING_VALUE + '''''', '''''' +  @AD_DOMAIN_NAME + '''''')''
			--print @sqlQuery
			exec(@sqlQuery)

		End
	else
		Begin
			--print @settingsIndex
			if @AD_SETTING_KEY = ''AD_PASSWORD''
				update #ActiveDirectoryDetails set AD_PASSWORD = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''IS_CARD_ENABLED''
				update #ActiveDirectoryDetails set IS_CARD_ENABLED = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''AD_PORT''
				update #ActiveDirectoryDetails set AD_PORT = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''IS_PIN_ENABLED''
				update #ActiveDirectoryDetails set IS_PIN_ENABLED = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''AD_FULLNAME''
				update #ActiveDirectoryDetails set AD_FULLNAME = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''DOMAIN_NAME''
				update #ActiveDirectoryDetails set DOMAIN_NAME = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''DOMAIN_CONTROLLER''
				update #ActiveDirectoryDetails set DOMAIN_CONTROLLER = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''AD_USERNAME''
				update #ActiveDirectoryDetails set AD_USERNAME = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''CARD_FIELD''
				update #ActiveDirectoryDetails set CARD_FIELD = @AD_SETTING_VALUE where SerialNumber = @settingsIndex
			else if @AD_SETTING_KEY = ''AD_ALIAS''
				update #ActiveDirectoryDetails set AD_ALIAS = @AD_SETTING_VALUE where SerialNumber = @settingsIndex

		End
	set @settingsCount = @settingsCount + 1

	FETCH NEXT FROM adSettingsCursor  
	into @AD_SETTING_KEY, @AD_SETTING_VALUE, @AD_DOMAIN_NAME
	
  end

select * from #ActiveDirectoryDetails' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAuditLog]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetCostCenterAccessRights]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCostCenterAccessRights]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetCostCenterAccessRights] 
@userSysID nvarchar(50),
@userSource varchar(2),
@deviceIpAddress nvarchar(50)

AS
BEGIN
	create table #tempCostCenters (COSTCENTERID int,COSTCENTERNAME nvarchar(100), ISACCESSALLOWED int)
	declare @SQL int
	declare @MFPID int
	declare @MYACCOUNT varchar(max)
	declare @USRMYACCOUNT varchar(max)
	-- Get the MFPID for the Given IP address
	select @MYACCOUNT =  APPSETNG_VALUE from APP_SETTINGS where APPSETNG_KEY = ''My Account''
	--print @MYACCOUNT
	select @USRMYACCOUNT = USR_MY_ACCOUNT from M_USERS where USR_ACCOUNT_ID = ''''+@userSysID+''''
	--print @USRMYACCOUNT
		if(@MYACCOUNT = ''Enable'')
				begin
					if(@USRMYACCOUNT = ''1'')
						begin
							select @MFPID = MFP_ID from M_MFPS where MFP_IP = ''''+@deviceIpAddress+''''
							select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID='''' + @userSysID + '''' and MFP_OR_GROUP_ID='''' + @MFPID + '''' and REC_STATUS =''1''
							print (@SQL)
							if(@SQL = ''0'')
								begin
									select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=@userSysID and REC_STATUS =''1'' and MFP_OR_GROUP_ID in (select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
									--select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=@userAccountID and MFP_OR_GROUP_ID in (select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
									--print (@SQL)
								end
						end
					else 
						begin
							if (@USRMYACCOUNT is null)	
								begin
									if(@MYACCOUNT = ''Enable'')
										begin
											select @sql = ''1''
										end
									else
										begin
											select @sql = ''0''
										end
								
								end
							if(@USRMYACCOUNT = ''0'')
								begin
									select @SQL = ''0''
								end
						end
				end
			else
				begin
				 select @SQL = ''0''
				end

	insert into #tempCostCenters(COSTCENTERID,COSTCENTERNAME, ISACCESSALLOWED) values(@userSysID,@userSysID, @SQL)

	DECLARE @CostCenterID INT
	DECLARE @getCostCenterID CURSOR
	SET @getCostCenterID = CURSOR FOR
	SELECT COST_CENTER_ID FROM T_COSTCENTER_USERS where USR_ACCOUNT_ID=@userSysID and USR_SOURCE=@userSource
	OPEN @getCostCenterID
	FETCH NEXT
		FROM @getCostCenterID INTO @CostCenterID
		WHILE @@FETCH_STATUS = 0
		BEGIN
			declare @CCenterName nvarchar(100)
			declare @MFPIDCC int
			-- Get the MFPID for the Given IP address
			select @MFPIDCC = MFP_ID from M_MFPS where MFP_IP = ''''+@deviceIpAddress+''''
			select @CCenterName = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID =''''+@CostCenterID+'''' -- Unique value
			-- Check Access Rights for Cost Center and MFP
			select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=''''+@CostCenterID+'''' and MFP_OR_GROUP_ID='''' + @MFPIDCC + '''' and REC_STATUS =''1''
			--print (@SQL)
			if(@SQL = ''0'')
			begin
				-- Check Access Rights for Cost Center and MFP Group
				select @SQL = count(*) from  T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=''''+@CostCenterID+'''' and REC_STATUS =''1'' and MFP_OR_GROUP_ID in (select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
				print (@SQL)
			end
			Declare @costCenterName nvarchar(100)
			select @costCenterName = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID=@CostCenterID
			insert into #tempCostCenters(COSTCENTERID,COSTCENTERNAME, ISACCESSALLOWED) values(@CostCenterID,@costCenterName, @SQL)
		FETCH NEXT
		FROM @getCostCenterID INTO @CostCenterID
		END
select * from #tempCostCenters
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetCostCenterAccessRights12]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCostCenterAccessRights12]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetCostCenterAccessRights12] 
@userSysID nvarchar(50),
@userSource varchar(2),
@deviceIpAddress nvarchar(50)

AS
BEGIN
	create table #tempCostCenters (COSTCENTERID int,COSTCENTERNAME nvarchar(100), ISACCESSALLOWED int)
	declare @SQL int
	declare @MFPID int
	-- Get the MFPID for the Given IP address
	select @MFPID = MFP_ID from M_MFPS where MFP_IP = ''''+@deviceIpAddress+''''
	select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID='''' + @userSysID + '''' and MFP_OR_GROUP_ID='''' + @MFPID + ''''
	if(@SQL = ''0'')
		begin
			select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=@userSysID and MFP_OR_GROUP_ID in (select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
		end
	insert into #tempCostCenters(COSTCENTERID,COSTCENTERNAME, ISACCESSALLOWED) values(@userSysID,@userSysID, @SQL)

	DECLARE @CostCenterID INT
	DECLARE @getCostCenterID CURSOR
	SET @getCostCenterID = CURSOR FOR
	SELECT COST_CENTER_ID FROM T_COSTCENTER_USERS where USR_ACCOUNT_ID=@userSysID and USR_SOURCE=@userSource
	OPEN @getCostCenterID
	FETCH NEXT
		FROM @getCostCenterID INTO @CostCenterID
		WHILE @@FETCH_STATUS = 0
		BEGIN
			declare @CCenterName nvarchar(100)
			declare @MFPIDCC int
			-- Get the MFPID for the Given IP address
			select @MFPIDCC = MFP_ID from M_MFPS where MFP_IP = ''''+@deviceIpAddress+''''
			select @CCenterName = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID =''''+@CostCenterID+'''' -- Unique value
			-- Check Access Rights for Cost Center and MFP
			select @SQL = count(*) from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=''''+@CostCenterID+'''' and MFP_OR_GROUP_ID='''' + @MFPIDCC + ''''
			--print (@SQL)
			if(@SQL = ''0'')
			begin
				-- Check Access Rights for Cost Center and MFP Group
				select @SQL = count(*) from  T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID=''''+@CostCenterID+'''' and MFP_OR_GROUP_ID in (select GRUP_ID from T_GROUP_MFPS where MFP_IP=''''+@deviceIpAddress+'''')
				--print (@SQL)
			end
			Declare @costCenterName nvarchar(100)
			select @costCenterName = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID=@CostCenterID
			insert into #tempCostCenters(COSTCENTERID,COSTCENTERNAME, ISACCESSALLOWED) values(@CostCenterID,@costCenterName, @SQL)
		FETCH NEXT
		FROM @getCostCenterID INTO @CostCenterID
		END
select * from #tempCostCenters
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetCostProfileMfpsOrGroups]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCostProfileMfpsOrGroups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetCostProfileMfpsOrGroups] @costProfileID int, @assignedTo varchar(20), @excludeMfpsOrGroups bit, @mfpFilter varchar(max) 

as 

if @excludeMfpsOrGroups = 1
	begin
		if @mfpFilter <> ''''
			begin				
				if @assignedTo = ''MFP''					
					select MFP_HOST_NAME,MFP_IP,MFP_LOCATION,MFP_MODEL from M_MFPS where MFP_HOST_NAME like @mfpFilter and MFP_IP in ( select  MFP_GROUP_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @costProfileID and ASSIGNED_TO = @assignedTo ) and REC_ACTIVE = ''1'' order by MFP_HOST_NAME
				else
					select * from M_MFP_GROUPS where GRUP_NAME like @mfpFilter and GRUP_ID in ( select  MFP_GROUP_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @costProfileID and ASSIGNED_TO = @assignedTo )and REC_ACTIVE = 1 order by GRUP_NAME 
			end
		else
			begin				
				if @assignedTo = ''MFP''				
					select MFP_HOST_NAME,MFP_IP,MFP_LOCATION,MFP_MODEL from M_MFPS where MFP_IP in ( select  MFP_GROUP_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @costProfileID and ASSIGNED_TO = @assignedTo) and REC_ACTIVE = 1 order by MFP_HOST_NAME
				else					
					select * from M_MFP_GROUPS where GRUP_ID in ( select  MFP_GROUP_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @costProfileID and ASSIGNED_TO = @assignedTo )and REC_ACTIVE = 1 order by GRUP_NAME
			end
	end
else
	begin
		if @mfpFilter <> ''''
			begin		
				if @assignedTo = ''MFP''
					select MFP_HOST_NAME,MFP_IP,MFP_LOCATION,MFP_MODEL from M_MFPS where MFP_HOST_NAME like @mfpFilter and MFP_IP not in ( select  MFP_GROUP_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @costProfileID and ASSIGNED_TO = @assignedTo ) and REC_ACTIVE = ''1'' order by MFP_HOST_NAME					
				else
					select * from M_MFP_GROUPS where GRUP_NAME like @mfpFilter and GRUP_ID not in ( select  MFP_GROUP_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @costProfileID and ASSIGNED_TO = @assignedTo )and REC_ACTIVE = 1 order by GRUP_NAME
			end
		else
			begin
				if @assignedTo = ''MFP''
					select MFP_HOST_NAME,MFP_IP,MFP_LOCATION,MFP_MODEL  from M_MFPS where MFP_IP not in ( select  MFP_GROUP_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @costProfileID and ASSIGNED_TO = @assignedTo) and REC_ACTIVE = 1 order by MFP_HOST_NAME
				else
					select * from M_MFP_GROUPS where GRUP_ID not in ( select  MFP_GROUP_ID from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @costProfileID and ASSIGNED_TO = @assignedTo ) and REC_ACTIVE = 1 order by GRUP_NAME
			end
	end' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetCrystalReportData]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCrystalReportData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetCrystalReportData]
	-- Add the parameters for the stored procedure here
	@fromDate datetime,
	@toDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
			Select REC_SLNO as SlNO,
			GRUP_ID as GroupID,
			COST_CENTER_NAME as CostCenter,
			T_JOB_LOG.MFP_IP as MFPIP,
			MFP_MACADDRESS as MFPMACADDRESS,
			USR_ID as UserID,
			USR_SOURCE as UserSource,
			JOB_MODE as JobMode,
			JOB_COMPUTER as JobComputer,
			JOB_USRNAME as UserFullName,
			JOB_START_DATE as JobStartDate,
			JOB_END_DATE as JobEndDate,
			JOB_COLOR_MODE as JobColormode,
			JOB_SHEET_COUNT_COLOR as TotalColor,
			JOB_SHEET_COUNT_BW as TotalBW,
			JOB_SHEET_COUNT as Total,
			JOB_PRICE_COLOR as JobPriceColor,
			JOB_PRICE_BW as JobPriceBW,
			JOB_PRICE_TOTAL as JobPriceTotal,
			JOB_STATUS as JobStatus,
			JOB_PAPER_SIZE as PaperSize,
			T_JOB_LOG.REC_DATE as date,
			DEPARTMENT as Department,
			COMPANY as Company,
			Location as Location,
			T_JOB_LOG.MFPNAME as MFPNAME
		

from T_JOB_LOG  where T_JOB_LOG.REC_DATE between @fromDate+''00:00'' and  @toDate+''23:59'' order by T_JOB_LOG.REC_DATE 
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetDeviceReport]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDeviceReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetDeviceReport] 
@ReportOn varchar(50), 
@fromDate varchar(10), 
@toDate  varchar(10),
@JobStatus varchar(200)


AS
BEGIN
	create table #tempDeviceReport (Device varchar(50), CopyColor numeric(18,0) default 0, CopyBW numeric(18,0) default 0, ScanColor numeric(18,0) default 0, ScanBw numeric(18,0) default 0,FaxColor numeric(18,0) default 0,FaxBW numeric(18,0) default 0,PrintColor numeric(18,0) default 0,PrintBW numeric(18,0) default 0)
	declare @dateCriteria varchar(1000)
	declare @SQL varchar(max)
	set @dateCriteria = '' ( JOB_START_DATE BETWEEN '''''' + @fromDate + '' 00:00'''' and '''''' + @toDate + '' 23:59'''')''
	declare @sqlQuery nvarchar(max)
	select * into #joblog from T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) )  and JOB_START_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 
	
	set @sqlQuery = ''insert into #tempDeviceReport (Device) select distinct MFP_IP from #joblog''
	
	--select distinct MFP_IP from #joblog

	update #tempDeviceReport set CopyColor=''0'' where CopyColor is null
	update #tempDeviceReport set CopyBW=''0'' where CopyBW is null
	update #tempDeviceReport set ScanColor=''0'' where ScanColor is null
	update #tempDeviceReport set ScanBw=''0'' where ScanBw is null
	update #tempDeviceReport set FaxColor=''0'' where FaxColor is null
	update #tempDeviceReport set FaxBW=''0'' where FaxBW is null
	update #tempDeviceReport set PrintColor=''0'' where PrintColor is null
	update #tempDeviceReport set PrintBW=''0'' where PrintBW is null
	select *from #tempDeviceReport

   
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetDevieLoginData]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDevieLoginData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetDevieLoginData]
	@DeviceIP nvarchar(20),
	@DeviceModel nvarchar(50)
AS

Begin
	-- Get MFP User Authentication and Authentication Settings
	select APPSETNG_VALUE,ADS_DEF_VALUE from APP_SETTINGS where APPSETNG_KEY in (''MFP User Authentication'',''Authentication Settings'')
	
	-- Get Device Details
	select * from M_MFPS where MFP_IP= @DeviceIP
	
	-- Get Device Theme
	Declare @AppTheme nvarchar(20)
	select @AppTheme = APP_THEME from M_MFPS where MFP_IP = @DeviceIP
	if(@AppTheme = null or @AppTheme = '''')
		begin
			select APP_THEME from APP_IMAGES where BG_APP_NAME = @DeviceModel
		end
	else
		begin
			select APP_THEME from M_MFPS where MFP_IP = @DeviceIP
		end

	--Provide Domain Name
	select AD_SETTING_VALUE from AD_SETTINGS where AD_SETTING_KEY = N''DOMAIN_NAME''
end


' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetExecutiveSummary]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetExecutiveSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[GetExecutiveSummary]
@fromDate varchar(10), 
@toDate  varchar(10),
@JobStatus varchar(200)
as
begin
declare @dateCriteria varchar(1000)
declare @SQL varchar(max)
declare @JobComPleted  varchar(1000)
set @JobComPleted = @JobStatus
create table #tempDuplex(TotalPages int default 0, Duplex_mode varchar(50))
set @dateCriteria = '' ( JOB_END_DATE BETWEEN '''''' + @fromDate + '' 00:00'''' and '''''' +@toDate  + '' 23:59'''')''

set @SQL=''SELECT DATEDIFF(day,'''''' + @fromDate + '' 00:00'''','''''' +@toDate  + '' 23:59'''') as TotalNumberofDays''
exec(@SQL)



set @SQL=''SELECT COUNT(*) as TotalNumberofJobs FROM T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT count(DISTINCT USR_ID) as TotalNumberofUsers FROM T_JOB_LOG  where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT case 
when SUM(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)  is null then 0
ELSE SUM(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)
end TotalPages
FROM T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT case 
when SUM(JOB_SHEET_COUNT_BW)  is null then 0
ELSE SUM(JOB_SHEET_COUNT_BW)
end BWPages
FROM T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT case 
when SUM(JOB_SHEET_COUNT_COLOR)  is null then 0
ELSE SUM(JOB_SHEET_COUNT_COLOR)
end ColorPages
FROM T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT count(DISTINCT MFP_IP) as TotalNumberofDevices FROM T_JOB_LOG  where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''''
exec(@SQL)

set @SQL=''SELECT count(DISTINCT JOB_Computer) as TotalNumberofWorkStations FROM T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and JOB_Computer<>''''''''  and '' + @dateCriteria + ''''
exec(@SQL)
end

set @SQL=''insert into #tempDuplex select sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as TotalPages,''''1SIDED'''' as DuplexMode  from T_JOB_LOG where duplex_Mode like ''''1%''''and '' + @dateCriteria + '' and (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))''

exec(@SQL)

set @SQL=''insert into #tempDuplex select sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as TotalPages,''''2SIDED'''' as DuplexMode  from T_JOB_LOG where duplex_Mode like ''''2%''''and '' + @dateCriteria + ''and (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))''

exec(@SQL)
update #tempDuplex set TotalPages=''0'' where TotalPages is null and duplex_mode = ''2SIDED''
update #tempDuplex set TotalPages=''0'' where TotalPages is null and duplex_mode = ''1SIDED''
set @SQL=''select * from #tempDuplex order by Duplex_mode desc''
exec(@SQL)

set @SQL=''SELECT   sum(JOB_SHEET_COUNT_COLOR) as ColorPages,sum(JOB_SHEET_COUNT_BW) as BWPages,JOB_PAPER_SIZE   FROM T_JOB_LOG
where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + '' and JOB_PAPER_SIZE like ''''A3%'''' group by JOB_PAPER_SIZE  order by JOB_PAPER_SIZE desc ''
exec(@SQL)
set @SQL=''SELECT   sum(JOB_SHEET_COUNT_COLOR) as ColorPages,sum(JOB_SHEET_COUNT_BW) as BWPages,JOB_PAPER_SIZE   FROM T_JOB_LOG
where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(''''''+@JobComPleted+'''''' , '''''''')))  and '' + @dateCriteria + '' and JOB_PAPER_SIZE like ''''A4%'''' group by JOB_PAPER_SIZE  order by JOB_PAPER_SIZE desc ''
exec(@SQL)
set @SQL=''SELECT   sum(JOB_SHEET_COUNT_COLOR) as ColorPages,sum(JOB_SHEET_COUNT_BW) as BWPages,JOB_PAPER_SIZE   FROM T_JOB_LOG
where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + '' and JOB_PAPER_SIZE Not like ''''A3%'''' and JOB_PAPER_SIZE Not like ''''A4%'''' group by JOB_PAPER_SIZE  order by JOB_PAPER_SIZE desc ''
exec(@SQL)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetFleetReports]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGraphicalReports]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGroupJobPermissionsAndLimits]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGroupJobPermissionsAndLimits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetGroupJobPermissionsAndLimits]
	@CostCenterID int,
	@UserID int,
	@LimitsOn int
AS
BEGIN
	if @LimitsOn = ''1''
		begin
			Declare @IsCostCenterShared int
			select @IsCostCenterShared = IS_SHARED from M_COST_CENTERS where COSTCENTER_ID=''1''
			--print (@IsCostCenterShared)
			if @IsCostCenterShared = ''0''
				begin
					exec CopyDefaultPermissionsAndLimits @UserID, @LimitsOn
				end
		end
	else if @LimitsOn = ''0''
		begin
			exec CopyCostCenterPermissionsAndLimits @CostCenterID,@UserID, @LimitsOn	
		end
print(@CostCenterID)
print(@UserID)
print(@LimitsOn)
select * from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=@CostCenterID and USER_ID=@UserID and PERMISSIONS_LIMITS_ON=@LimitsOn

END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetGroupMFPs]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGroupMFPs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetGroupMFPs] @MFPGroupID int, @excludeGroupMfps bit, @hostFilter varchar(max) 
as

if @excludeGroupMfps = 1
	begin
		if @hostFilter <> ''''
			select * from M_MFPS where MFP_HOST_NAME like @hostFilter and MFP_IP in ( select  MFP_IP from T_GROUP_MFPS where GRUP_ID = @MFPGroupID) and REC_ACTIVE = 1 order by MFP_HOST_NAME
		else
			select * from M_MFPS where MFP_IP in ( select  MFP_IP from T_GROUP_MFPS where GRUP_ID = @MFPGroupID) and REC_ACTIVE = 1 order by MFP_HOST_NAME
	end
else
	begin
		if @hostFilter <> ''''
			select * from M_MFPS where MFP_HOST_NAME like @hostFilter and MFP_IP not in ( select  MFP_IP from T_GROUP_MFPS where GRUP_ID = @MFPGroupID) and REC_ACTIVE = 1 order by MFP_HOST_NAME
		else
			select * from M_MFPS where MFP_IP not in ( select  MFP_IP from T_GROUP_MFPS where GRUP_ID = @MFPGroupID) and REC_ACTIVE = 1 order by MFP_HOST_NAME
	end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetInvoice]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetInvoiceUnits]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceUnits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetInvoiceUnits] 
	@startDate DateTime , 
	@endDate DateTime,
	@CostCenter nvarchar(50),
	@userID nvarchar(30),
	@JobStaus varchar(100)
AS
BEGIN
	declare @ReportOn nvarchar(1000)
	declare @SQL varchar(max)

	create table #FileredLog(SlNo int identity, GRUP_ID int, MFP_IP nvarchar(20),USR_ID varchar(30),JOB_MODE varchar(30), JOB_START_DATE datetime, JOB_END_DATE datetime, JOB_COLOR_MODE varchar(20), JOB_SHEET_COUNT_COLOR int default 0, JOB_SHEET_COUNT_BW int default 0, JOB_PRICE_COLOR float default 0, JOB_PRICE_BW float default 0)
	
	if @startDate != '''' and @endDate != ''''
		begin
			print (''1'')
			insert into #FileredLog(GRUP_ID,MFP_IP,USR_ID,JOB_MODE,JOB_START_DATE,JOB_END_DATE,JOB_COLOR_MODE,JOB_SHEET_COUNT_COLOR,JOB_SHEET_COUNT_BW,JOB_PRICE_COLOR,JOB_PRICE_BW) select GRUP_ID,MFP_IP,USR_ID,JOB_MODE,JOB_START_DATE,JOB_END_DATE,JOB_COLOR_MODE,JOB_SHEET_COUNT_COLOR,JOB_SHEET_COUNT_BW,JOB_PRICE_COLOR,JOB_PRICE_BW from T_JOB_LOG where (JOB_START_DATE >= @startDate + '' 00:00'' and JOB_END_DATE <= @endDate+ '' 23:59'') and (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStaus, '''')) )
		end
	else
		begin
			print (''2'')
			insert into #FileredLog(GRUP_ID,MFP_IP,USR_ID,JOB_MODE,JOB_START_DATE,JOB_END_DATE,JOB_COLOR_MODE,JOB_SHEET_COUNT_COLOR, JOB_SHEET_COUNT_BW, JOB_PRICE_COLOR,JOB_PRICE_BW) select GRUP_ID, MFP_IP, USR_ID,JOB_MODE, JOB_START_DATE, JOB_END_DATE, JOB_COLOR_MODE, JOB_SHEET_COUNT_COLOR, JOB_SHEET_COUNT_BW, JOB_PRICE_COLOR,JOB_PRICE_BW from T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStaus, '''')) )
		end
	--select * from #FileredLog
	-- If Cost Center and Users are set to ALL (-1)
	if @CostCenter = ''-1'' and @userID = ''-1'' 
		begin
			set @ReportOn = ''1=1''			
		end

	-- If Cost center is Selected and User 	
	if @CostCenter != ''-1'' and @userID = ''-1'' 
		begin
			set @ReportOn = ''GRUP_ID=''''''+@CostCenter+''''''''
		end
	-- If User only selected
	if @CostCenter = ''-1'' and @userID != ''-1'' 
		begin
			set @ReportOn = ''USR_ID=''''''+@userID+''''''''
		end

	-- IF Both CostCenter and User is Slected
	if @CostCenter != ''-1'' and @userID != ''-1'' 
		begin
			set @ReportOn = ''GRUP_ID=''''''+@CostCenter+'''''' and USR_ID=''''''+@userID+''''''''
		end
	--select * from #FileredLog
	set @SQL = ''select JOB_MODE, sum(JOB_PRICE_COLOR) as COLOR_PRICE, sum(JOB_PRICE_BW) as MONOCHROME_PRICE,sum(JOB_SHEET_COUNT_COLOR) as COLOR_COUNT,sum(JOB_SHEET_COUNT_BW) as MONOCHROME_COUNT from #FileredLog where 1=1 and '' + @ReportOn + '' group by JOB_MODE order by JOB_MODE''
	print @SQL
	exec(@SQL)
	drop table #FileredLog
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetJobLogforCR]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobLogforCR]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<G.R.Varadharaj>
-- Create date: <1/23/2015,,>
-- Description:	<Joblog >
-- =============================================
CREATE PROCEDURE [dbo].[GetJobLogforCR]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select * into #temp from T_JOB_LOG 
	exec(''alter table #temp add COST_CENTER varchar(50)'')
	update #temp set COST_CENTER = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID = GRUP_ID
	update #temp set COST_CENTER = ''MyAccount''  where  GRUP_ID= ''-1''
	select * from #temp
	drop table #temp


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetJobPrice]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJobPrice]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetJobPrice]
@mfpIPAddress varchar(40), 
@costCenterID int,
@jobType varchar(20), 
@paperSize varchar(20),
@limitsOn varchar(20)

as 

declare @costCenterName nvarchar(50)
declare @costProfileID int
declare @rowsCount int
declare @colorPricePerUnit float
declare @monochromePricePerUnit float

if(@jobType = ''SCANNER'') set @jobType = ''SCAN''

select GRUP_ID, cast('''' as nvarchar(50)) as GRUP_NAME into #mfpGroups from T_GROUP_MFPS where MFP_IP = @mfpIPAddress

--update #mfpGroups set #mfpGroups.GRUP_NAME = M_MFP_GROUPS.GRUP_NAME from M_MFP_GROUPS where M_MFP_GROUPS.GRUP_ID = #mfpGroups.GRUP_ID

select COST_PROFILE_ID, cast('''' as nvarchar(50)) as COST_PROFILE_NAME, ASSIGNED_TO, MFP_GROUP_ID, cast('''' as nvarchar(50)) as MFP_GRUP_NAME into #costProfiles 
from T_ASSGIN_COST_PROFILE_MFPGROUPS
where MFP_GROUP_ID = @mfpIPAddress or MFP_GROUP_ID in (select cast(GRUP_ID as varchar(40)) from #mfpGroups)

--update #costProfiles set  MFP_GRUP_NAME = GRUP_NAME from M_MFP_GROUPS where ASSIGNED_TO = ''MFP Group'' and M_MFP_GROUPS.GRUP_ID = #costProfiles.MFP_GROUP_ID
--update #costProfiles set  COST_PROFILE_NAME = PRICE_PROFILE_NAME from M_PRICE_PROFILES where PRICE_PROFILE_ID = COST_PROFILE_ID

-- if user selects cost center [user group], then the preference will be Cost profile assigned to MFP Group
-- if user select cost center as My Account then the preference will be Cost profile assigned with MFP

select @rowsCount = count(COST_PROFILE_ID) from #costProfiles where ASSIGNED_TO = ''MFP Group''
--select * from #costProfiles
--print (@costCenterID)
--print(@rowsCount)	
if (@rowsCount >= 1) and (@limitsOn = ''Cost Center'' or @limitsOn = ''User'')   -- Default Cost Center ID = 1
	begin

		select @costProfileID = COST_PROFILE_ID from #costProfiles where ASSIGNED_TO = ''MFP Group''
	end
else
	begin

		select @costProfileID = COST_PROFILE_ID from #costProfiles where ASSIGNED_TO = ''MFP''
	end

select @colorPricePerUnit = PRICE_PERUNIT_COLOR,  @monochromePricePerUnit = PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = @costProfileID and JOB_TYPE = @jobType and PAPER_SIZE = @paperSize
if @colorPricePerUnit is null set @colorPricePerUnit = 0
if @monochromePricePerUnit is null set @monochromePricePerUnit = 0
select @colorPricePerUnit as ''ColorPrice'', @monochromePricePerUnit as ''MonochromePrice''
--select @costProfileID
--select * from #mfpGroups
--select * from #costProfiles

' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetJobReport]    Script Date: 26/11/2015 13:01:29 ******/
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
 update #JobReport set SerialNumber = MFP_SERIALNUMBER, ModelName= MFP_MODEL from M_MFPS where #JobReport.ReportOf COLLATE Latin1_General_CI_AS =  M_MFPS.MFP_IP COLLATE Latin1_General_CI_AS  
 insert into #JobReport (SerialNumber,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided) select ''Total'', sum(Total), sum(TotalBW), sum(TotalColor), sum(Ledger), sum(A3), sum(Legal), sum(Letter), sum(A4), sum(Other)
, sum(Duplex_One_Sided), sum(Duplex_Two_Sided) from #JobReport  
   
    update #JobReport set Duplex_Two_Sided = Duplex_Two_Sided / 2  
  
 select SerialNumber,Total,TotalBW,TotalColor,Ledger,A3,Legal,Letter,A4,Other,Duplex_One_Sided,Duplex_Two_Sided,ModelName from  #JobReport   
End  
  
if @ReportOn = ''USR_ID,USR_SOURCE,USR_DEPT,JOB_COMPUTER''  
Begin  
 update #JobReport set UserName = USR_NAME from M_USERS where #JobReport.ReportOf COLLATE Latin1_General_CI_AS =  M_USERS.USR_ID COLLATE Latin1_General_CI_AS and #JobReport.AuthenticationSource COLLATE Latin1_General_CI_AS =  M_USERS.USR_SOURCE COLLATE Latin1_General_CI_AS  
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
/****** Object:  StoredProcedure [dbo].[GetJobUsage]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetJobUsageDetails]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetLocalizedLabel]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetLocalizedServerMessage]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetLocalizedStrings]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetLocalizedValues]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetNotes]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNotes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetNotes]
AS
BEGIN
	SET NOCOUNT ON;
	select SRV_NOTES as NOTES  from T_SRV where SRV_NOTES is not NULL order by SRV_R_DATE ASC
	select MFP_NOTES as NOTES  from M_MFPS where MFP_NOTES is not NULL order by MFP_R_DATE ASC 
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPagedData]    Script Date: 26/11/2015 13:01:29 ******/
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
FROM sysobjects o with (nolock)    
JOIN syscolumns c with (nolock)  on o.id=c.id  
JOIN systypes t with (nolock)  on c.xusertype=t.xusertype  
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
SELECT '' + @Fields + '' FROM '' + @Tables + '' with (nolock) WHERE '' + @strSortColumn + @operator + '' @SortColumn '' + @strSimpleFilter + '' '' + @strGroup + '' ORDER BY '' + @Sort + ''  
''  
)  


--select * from M_MFPS with (nolock) where REC_ACTIVE = 1  ' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPagedTableData]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPagedTableData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetPagedTableData]
  @datasrc nvarchar(200)
 ,@orderBy nvarchar(200)
 ,@fieldlist nvarchar(200) = ''*''
 ,@filter nvarchar(200) = ''''
 ,@pageNum int = 1
 ,@pageSize int = NULL
AS
  SET NOCOUNT ON
  DECLARE
     @STMT nvarchar(max)         -- SQL to execute
    ,@recct int                  -- total # of records (for GridView paging interface)

  IF LTRIM(RTRIM(@filter)) = '''' SET @filter = ''1 = 1''
  IF @pageSize IS NULL BEGIN
    SET @STMT =  ''SELECT   '' + @fieldlist + 
                 ''FROM     '' + @datasrc +
                 ''WHERE    '' + @filter + 
                 ''ORDER BY '' + @orderBy
    EXEC (@STMT)                 -- return requested records 
  END ELSE BEGIN
    SET @STMT =  ''SELECT   @recct = COUNT(*)
                  FROM     '' + @datasrc + ''
                  WHERE    '' + @filter
    EXEC sp_executeSQL @STMT, @params = N''@recct INT OUTPUT'', @recct = @recct OUTPUT
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
    SET @STMT =  ''SELECT  '' + @fieldlist + ''
                  FROM    (
                            SELECT  ROW_NUMBER() OVER(ORDER BY '' + @orderBy + '') AS row, *
                            FROM    '' + @datasrc + ''
                            WHERE   '' + @filter + ''
                          ) AS tbl
                  WHERE
                          row > '' + CONVERT(varchar(9), @lbound) + '' AND
                          row < '' + CONVERT(varchar(9), @ubound)
    EXEC (@STMT)                 -- return requested records 
  END' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPermissionsAndLimits]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPermissionsAndLimits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetPermissionsAndLimits]
	@costCenterId int,
	@UserId int,
	@LimitsBasedOn int

AS
Begin
	declare @rowsCount int
	declare @autoRefillStatus nvarchar(30)
	if @LimitsBasedOn = ''1'' -- = User
		begin
			-- Check permissions and Limits are set for the user
			select @autoRefillStatus = AUTO_FILLING_TYPE from T_AUTO_REFILL where AUTO_REFILL_FOR=''C''
			select @rowsCount = count(*) from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=''-1'' and USER_ID=@UserId and PERMISSIONS_LIMITS_ON=@LimitsBasedOn
			if @rowsCount = ''0''
				begin
					if @autoRefillStatus <> ''Automatic''
						begin
							-- Copy Permissions and Limits of Default Group to the User
							exec CopyDefaultPermissionsAndLimits @UserId,@LimitsBasedOn
							--print (''Executed CopyDefaultPermissionsAndLimits'')
						end
				end
			select * from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=''-1'' and USER_ID=@UserId and PERMISSIONS_LIMITS_ON=@LimitsBasedOn
		end
	else --  limits Based on Cost Center
		begin
			declare @isCostCenterShared bit
			select @isCostCenterShared = IS_SHARED from M_COST_CENTERS where COSTCENTER_ID=@costCenterId
			if @isCostCenterShared = ''1''
				begin
					select * from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=@costCenterId and USER_ID=''-1'' and PERMISSIONS_LIMITS_ON=@LimitsBasedOn
					--print (''Retrived Cost Center Details - Shared'')
				end
			else
				begin
					-- Check user has permissions and Limits on selected Cost Center else copy
					select @rowsCount = count(*) from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=@costCenterId and USER_ID=@UserId and PERMISSIONS_LIMITS_ON=@LimitsBasedOn
					if @rowsCount =''0''
						begin
							if @autoRefillStatus <> ''Automatic''
								begin
									exec CopyCostCenterPermissionsAndLimits @costCenterId,@UserId,@LimitsBasedOn
									--print (''Executed CopyCostCenterPermissionsAndLimits - Not Shared'')	
								end
						end
					select * from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=@costCenterId and USER_ID=@UserId and PERMISSIONS_LIMITS_ON=@LimitsBasedOn
				end
		end
End	

' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPricing]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetRandomDate]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRandomDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[GetRandomDate]
as
	select dateadd(month, -1 * abs(convert(varbinary, newid()) % (90 * 12)), getdate()) as RandomDate
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetREPORTNEW]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetSlicedData]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[GetTopReports]    Script Date: 26/11/2015 13:01:29 ******/
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
@toDate  varchar(10),
@JobStatus varchar(200)

as
begin
create table #tempDayWiseReport(TotalColor numeric(18,0) default 0, TotalBW numeric(18,0) default 0, Total numeric(18,0) default 0, WeekDays varchar(10))
create table #tempPagesWiseReport(Pages varchar(200), Jobs numeric(18,0) default 0, TotalPages numeric(18,0) default 0)
declare @dateCriteria varchar(1000)
declare @SQL varchar(max)

set @dateCriteria = '' ( JOB_END_DATE BETWEEN '''''' + @fromDate + '' 00:00'''' and '''''' + @toDate + '' 23:59'''')''

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

insert into #tempDayWiseReport(TotalColor,TotalBW,Total,WeekDays) select sum(JOB_SHEET_COUNT_COLOR)as  TotalColor,sum(JOB_SHEET_COUNT_BW)as  TotalBW ,sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as  Total,@Days as WeekDays  from T_JOB_LOG  where JOB_STATUS  in (select TokenVal from ConvertStringListToTable(@JobStatus, '',''))  and  convert(varchar, job_end_date, 101) in( SELECT convert(varchar, [Dt], 101) FROM dbo.GetDatesforAday(@fromDate,@ToDate,@Days))
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
declare @JobComPleted  varchar(1000)
set @JobComPleted = @JobStatus
set @SQL=''select sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as Totalpages ,JOB_COLOR_MODE  FROM T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''group by JOB_COLOR_MODE  order by Totalpages desc''
exec(@SQL)
print(@SQL)
-------------------------------------BuildTotalVolumeBreakdownPageSize------------------------------------
set @SQL=''SELECT sum(JOB_SHEET_COUNT_COLOR) as TotalPages,JOB_PAPER_SIZE  FROM T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''group by JOB_PAPER_SIZE  order by TotalPages desc''
exec(@SQL)
print(@SQL)
-------------------------------------BuildTotalVolumeBreakdownPageSizeBW---------------------------------
set @SQL=''SELECT sum(JOB_SHEET_COUNT_BW) as TotalPages,JOB_PAPER_SIZE  FROM T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(''''''+@JobComPleted+'''''' , '''''''')))  and '' + @dateCriteria + ''group by JOB_PAPER_SIZE  order by TotalPages desc''
exec(@SQL)
print(@SQL)
-------------------------------------BuildTotalVolumeBreakdownPrinters------------------------------------
set @SQL=''SELECT Top 5 sum(JOB_SHEET_COUNT_COLOR) as TotalColor, sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as Totalpages ,MFP_IP  FROM T_JOB_LOG
where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''group by MFP_IP  order by Totalpages desc''
exec(@SQL)
print(@SQL)
-------------------------------------BuildTotalVolumeBreakdownUsers------------------------------------
set @SQL=''SELECT Top 5 sum(JOB_SHEET_COUNT_COLOR) as TotalColor, sum(JOB_SHEET_COUNT_BW) as TotalBW,sum(JOB_SHEET_COUNT_COLOR+JOB_SHEET_COUNT_BW)as Totalpages ,USR_ID  FROM T_JOB_LOG
where (JOB_STATUS in (select TokenVal from ConvertStringListToTable( ''''''+@JobComPleted+''''''  , '''''''')))  and '' + @dateCriteria + ''group by USR_ID  order by Totalpages desc''
exec(@SQL)
print(@SQL)
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserPinCountDetails]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserPinCountDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetUserPinCountDetails]
	
AS
BEGIN
	
	SET NOCOUNT ON;

select count(*) as [count] from M_USERS where REC_ACTIVE = 1 and USR_SOURCE = ''DB'';
;

select count(*) as [count] from M_USERS where REC_ACTIVE = 1 and USR_SOURCE = ''DB'' and (USR_PIN <> '''' OR USR_PIN <> NULL) ;
select count(*) as [count] from M_USERS where REC_ACTIVE = 1 and USR_SOURCE = ''AD'' and (USR_PIN <> '''' OR USR_PIN <> NULL) ;

select count(*) as [count] from M_USERS where REC_ACTIVE = 1 and USR_SOURCE = ''DB'' and (USR_PIN IS NULL or USR_PIN = '''');
select count(*) as [count] from M_USERS where REC_ACTIVE = 1 and USR_SOURCE = ''AD'' and (USR_PIN IS NULL or USR_PIN = '''');

select count(*) as [count] from M_USERS where REC_ACTIVE = 1 and USR_SOURCE = ''AD''


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserReport]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[GetUserReport]
@ReportOn varchar(50),
@UserID varchar(50),
@AuthenticationSource varchar(2),
@fromDate varchar(10),
@toDate  varchar(10),
@userRole varchar(10),
@JobStaus varchar(100)

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
R.USR_DEPT,R.JOB_COMPUTER,R.MFP_IP,R.GRUP_ID,R.COST_CENTER_NAME,R.JOB_SHEET_COUNT,R.JOB_USRNAME,R.JOB_SHEET_COUNT_COLOR,R.JOB_SHEET_COUNT_BW,M.MFP_SERIALNUMBER,M.MFP_MODEL into #TEMP1  from T_JOB_LOG R left join M_MFPS M on R.MFP_IP=M.MFP_IP where (R.JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStaus, '''')) ) and R.JOB_COLOR_MODE<>'''' and  R.JOB_END_DATE BETWEEN '''' + @fromDate + '' 00:00'' and '''' +@toDate  + '' 23:59'' and R.USR_ID=@UserID

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
/****** Object:  StoredProcedure [dbo].[GETUSERS]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETUSERS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GETUSERS]
as
BEGIN
	select * from M_USERS
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[ImportADUsers]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportADUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ImportADUsers] 
	@sessionID varchar(50), 
	@selectedUsers ntext, 
	@importAllUsers varchar(10), 
	@userSource varchar(5), 
	@userRole varchar(10),
	@userPinSelected bit,
	@userCardSelected bit,
	@domainName nvarchar(50),
	@cardField nvarchar(100),
	@pinField nvarchar(100),
	@isImportDepartMent bit
as

create table #SelectedUsers(UserID nvarchar(100))
delete from T_AD_USERS where C_DATE < DATEADD(dd,-2,getdate())
if @importAllUsers = ''YES''
	begin
		insert into #SelectedUsers(UserID) select USER_ID from T_AD_USERS where SESSION_ID=@sessionID and DOMAIN=@domainName
	end
else
	begin
		insert into #SelectedUsers(UserID) select TokenVal from ConvertStringListToTable(@selectedUsers, '''')
	end

select UserID from  #SelectedUsers
if @userPinSelected = ''0'' and @userCardSelected = ''0''
	begin
		insert into M_USERS(USR_SOURCE,USR_DOMAIN,USR_ID,USR_NAME,USR_EMAIL,USR_ROLE,REC_CDATE,REC_ACTIVE,DEPARTMENT,COMPANY) 
		select USR_SOURCE,DOMAIN,USER_ID,USER_NAME,EMAIL,@userRole,C_DATE,REC_ACTIVE,DEPARTMENT,COMPANY from T_AD_USERS 
		where USER_ID in (select UserID from #SelectedUsers) and USER_ID not in (select USR_ID from M_USERS where USR_SOURCE = @userSource and USR_DOMAIN= @domainName) and SESSION_ID=@sessionID

		update M_USERS set USR_NAME = T_AD_USERS.USER_NAME, USR_EMAIL = T_AD_USERS.EMAIL, DEPARTMENT = T_AD_USERS.DEPARTMENT, USR_ROLE= @userRole, COMPANY=T_AD_USERS.COMPANY from T_AD_USERS
		where M_USERS.USR_ID = T_AD_USERS.USER_ID and M_USERS.USR_SOURCE = @userSource and M_USERS.USR_DOMAIN = @domainName and T_AD_USERS.SESSION_ID=@sessionID and M_USERS.USR_ID in (select UserID from #SelectedUsers)
	end
else if @userPinSelected = ''1'' and @userCardSelected = ''0''
	begin
		insert into M_USERS(USR_SOURCE,USR_DOMAIN,USR_ID,USR_NAME,USR_EMAIL,USR_ROLE,REC_CDATE,REC_ACTIVE,DEPARTMENT,USR_PIN,COMPANY) 
		select USR_SOURCE,DOMAIN,USER_ID,USER_NAME,EMAIL,@userRole,C_DATE,REC_ACTIVE,DEPARTMENT, AD_PIN,COMPANY from T_AD_USERS 
		where USER_ID in (select UserID from #SelectedUsers) and USER_ID not in (select USR_ID from M_USERS where USR_SOURCE = @userSource  and USR_DOMAIN= @domainName) and SESSION_ID=@sessionID

		update M_USERS set USR_NAME = T_AD_USERS.USER_NAME, USR_EMAIL = T_AD_USERS.EMAIL, DEPARTMENT = T_AD_USERS.DEPARTMENT, USR_ROLE= @userRole, USR_PIN = AD_PIN,COMPANY=T_AD_USERS.COMPANY from T_AD_USERS
		where M_USERS.USR_ID = T_AD_USERS.USER_ID and M_USERS.USR_SOURCE = @userSource and M_USERS.USR_DOMAIN = @domainName and T_AD_USERS.SESSION_ID=@sessionID and M_USERS.USR_ID in (select UserID from #SelectedUsers)
	end
else if @userPinSelected = ''0'' and @userCardSelected = ''1''
	begin
		insert into M_USERS(USR_SOURCE,USR_DOMAIN,USR_ID,USR_NAME,USR_EMAIL,USR_ROLE,REC_CDATE,REC_ACTIVE,DEPARTMENT,USR_CARD_ID,COMPANY) 
		select USR_SOURCE,DOMAIN,USER_ID,USER_NAME,EMAIL,@userRole,C_DATE,REC_ACTIVE,DEPARTMENT, AD_CARD,COMPANY  from T_AD_USERS 
		where USER_ID in (select UserID from #SelectedUsers) and USER_ID not in (select USR_ID from M_USERS where USR_SOURCE = @userSource and USR_DOMAIN= @domainName) and SESSION_ID=@sessionID

		update M_USERS set USR_NAME = T_AD_USERS.USER_NAME, USR_EMAIL = T_AD_USERS.EMAIL, DEPARTMENT = T_AD_USERS.DEPARTMENT, USR_ROLE= @userRole, USR_CARD_ID = AD_CARD,COMPANY=T_AD_USERS.COMPANY from T_AD_USERS
		where M_USERS.USR_ID = T_AD_USERS.USER_ID and M_USERS.USR_SOURCE = @userSource and M_USERS.USR_DOMAIN = @domainName and T_AD_USERS.SESSION_ID=@sessionID and M_USERS.USR_ID in (select UserID from #SelectedUsers)
	end

else if @userPinSelected = ''1'' and @userCardSelected = ''1''
	begin
		insert into M_USERS(USR_SOURCE,USR_DOMAIN,USR_ID,USR_NAME,USR_EMAIL,USR_ROLE,REC_CDATE,REC_ACTIVE,DEPARTMENT,USR_PIN,USR_CARD_ID,COMPANY) 
		select USR_SOURCE,DOMAIN,USER_ID,USER_NAME,EMAIL,@userRole,C_DATE,REC_ACTIVE,DEPARTMENT, AD_PIN, AD_CARD,COMPANY  from T_AD_USERS 
		where USER_ID in (select UserID from #SelectedUsers) and USER_ID not in (select USR_ID from M_USERS where USR_SOURCE = @userSource  and USR_DOMAIN= @domainName) and SESSION_ID=@sessionID

		update M_USERS set USR_NAME = T_AD_USERS.USER_NAME, USR_EMAIL = T_AD_USERS.EMAIL, DEPARTMENT = T_AD_USERS.DEPARTMENT, USR_ROLE= @userRole, USR_PIN = AD_PIN, USR_CARD_ID = AD_CARD,COMPANY=T_AD_USERS.COMPANY from T_AD_USERS
		where M_USERS.USR_ID = T_AD_USERS.USER_ID and M_USERS.USR_SOURCE = @userSource and M_USERS.USR_DOMAIN = @domainName  and T_AD_USERS.SESSION_ID=@sessionID and M_USERS.USR_ID in (select UserID from #SelectedUsers)
	end

-- UPDATE CARD Filed in AD settings
	if @userCardSelected = ''0''
	  begin
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=''False'' where AD_SETTING_KEY=''IS_CARD_ENABLED'' and AD_DOMAIN_NAME= @domainName
	  end
	else
	  begin
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=''True'' where AD_SETTING_KEY=''IS_CARD_ENABLED'' and AD_DOMAIN_NAME= @domainName
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=@cardField where AD_SETTING_KEY=''CARD_FIELD'' and AD_DOMAIN_NAME= @domainName
	  end

-- UPDATE PIN FIELD
	if @userPinSelected = ''0''
	  begin
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=''False'' where AD_SETTING_KEY=''IS_PIN_ENABLED'' and AD_DOMAIN_NAME= @domainName
	  end
	else
	  begin
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=''True'' where AD_SETTING_KEY=''IS_PIN_ENABLED'' and AD_DOMAIN_NAME= @domainName
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=@pinField where AD_SETTING_KEY=''PIN_FIELD'' and AD_DOMAIN_NAME= @domainName
	  end

-- Assign users to Default Cost Center
select UserID from  #SelectedUsers

--delete from T_COSTCENTER_USERS where USR_ID in (select UserID from  #SelectedUsers) and COST_CENTER_ID=''1'' and USR_SOURCE=''AD''
--insert into T_COSTCENTER_USERS(USR_ID,COST_CENTER_ID,USR_SOURCE) select UserID, ''1'', ''AD'' from #SelectedUsers
---------------------------------------------------------------------------------------------------------
--To import department as costcenter
--1.Find distinct department
--2.Create groups for departments
if(@isImportDepartMent = ''True'')
begin
				DECLARE @department AS nvarchar(200);
                DECLARE @getDepartment AS CURSOR;
                SET @getDepartment = CURSOR FOR SELECT distinct Department FROM M_USERS WHERE  DEPARTMENT is not null and USR_ID in (select UserID from  #SelectedUsers)
                OPEN @getDepartment;
                FETCH NEXT FROM @getDepartment INTO @department;
                WHILE @@FETCH_STATUS = 0
                    BEGIN
						print @department;
							-- verify department name already exists
						IF not EXISTS (SELECT COSTCENTER_NAME FROM M_COST_CENTERS WHERE COSTCENTER_NAME= @department)
						insert into M_COST_CENTERS (COSTCENTER_NAME,REC_ACTIVE,REC_DATE,REC_USER,ALLOW_OVER_DRAFT,IS_SHARED,USR_SOURCE) values (@department,1,GETDATE(),''admin'',1,0,''AD'')

						FETCH NEXT FROM @getDepartment INTO @department;
					END
					CLOSE @getDepartment;
					DEALLOCATE @getDepartment;
--3.Assign department users in T_COSTCENTER_USERS

				DECLARE @departmentG AS nvarchar(200);
                DECLARE @getDepartmentG AS CURSOR;
                SET @getDepartmentG = CURSOR FOR SELECT distinct Department FROM M_USERS WHERE  DEPARTMENT is not null and USR_ID in (select UserID from  #SelectedUsers)
                OPEN @getDepartmentG;
                FETCH NEXT FROM @getDepartmentG INTO @departmentG;
                WHILE @@FETCH_STATUS = 0
                    BEGIN
						print @departmentG;
							select USR_ACCOUNT_ID,USR_SOURCE,USR_ID into #temp  from M_USERS where M_USERS.DEPARTMENT = @departmentG
						
						ALTER TABLE #temp ADD CostcenterID int
						
						update #temp set #temp.CostcenterID = (select COSTCENTER_ID from M_COST_CENTERS where COSTCENTER_NAME = @departmentG)
						select * from #temp
						delete  from T_COSTCENTER_USERS where COST_CENTER_ID = (select distinct CostcenterID from #temp )

						insert into T_COSTCENTER_USERS (USR_ACCOUNT_ID,USR_ID,COST_CENTER_ID,USR_SOURCE) select USR_ACCOUNT_ID,USR_ID,CostcenterID,USR_SOURCE from #temp
						drop table #temp
						FETCH NEXT FROM @getDepartmentG INTO @departmentG;
					END
					CLOSE @getDepartmentG;
					DEALLOCATE @getDepartmentG; 
					END' 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertMultipleJobLogs]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertMultipleRecords]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[ManageFirstLogOn]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[ManageSessions]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[QueueForJobRelease]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QueueForJobRelease]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[QueueForJobRelease] 
	@serviceName varchar(100)
as

select * into #JobQueue from T_PRINT_JOBS where JOB_RELEASER_ASSIGNED = @serviceName  and JOB_PRINT_RELEASED = ''false'' and datediff(ss, REC_DATE, getdate()) > 7
select * from #JobQueue
update T_PRINT_JOBS set JOB_PRINT_RELEASED = ''true'' where REC_SYSID in (select REC_SYSID from #JobQueue)
delete from T_PRINT_JOBS where REC_DATE < dateadd(minute,-30,getdate())
select distinct JOB_REQUEST_MFP from #JobQueue
--select * from T_PRINT_JOBS where REC_SYSID in (select REC_SYSID from #JobQueue)' 
END
GO
/****** Object:  StoredProcedure [dbo].[QuickJobTypeSummary]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuickJobTypeSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[QuickJobTypeSummary](
	@fromDate varchar(10),
	@toDate  varchar(10),
	@JobStatus varchar(100)
)

AS

create table #QuickJobType
(	
	JobType nvarchar(50),
	Color int default 0,
	BW int default 0,
	Total int default 0
)

insert into #QuickJobType select ''Print'', isnull (sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG where  JOB_MODE in (''PRINT'',''Doc Filing Print'') and (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) ) and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 
insert into #QuickJobType select ''Copy'', isnull (sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG where JOB_MODE = ''COPY'' and (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) ) and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 
insert into #QuickJobType select ''Scan'', isnull (sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG where JOB_MODE in (''SCANNER'',''Doc Filing Scan'') and (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) ) and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 

--insert into #QuickJobType select ''Scan'', isnull (sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG where JOB_MODE = ''SCANNER'' and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 
--insert into #QuickJobType select ''Doc Filing Scan'',isnull ( sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG where JOB_MODE=''Doc Filing Scan'' and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 
--insert into #QuickJobType select ''Doc Filing Print'',isnull ( sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG where JOB_MODE=''Doc Filing Print'' and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 


select * from #QuickJobType
drop table #QuickJobType
' 
END
GO
/****** Object:  StoredProcedure [dbo].[RecordAuditLog]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordAuditLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[RecordAuditLog] 
	@messageSource nvarchar(30), 
	@messageType nvarchar(20), 
	@message nvarchar(max), 
	@suggestion nvarchar(max), 
	@exception nvarchar(max), 
	@stackTrace nvarchar(max),
	@messageOwner nvarchar(30),
	@serverIPAddress varchar(50),
	@serverLocation nvarchar(100), 
	@serverTokenID int
as 

if exists (select APPSETNG_KEY from APP_SETTINGS where APPSETNG_KEY = ''AUDIT_LOG'' and APPSETNG_VALUE = ''Enable'')
begin
	if exists (select LOG_TYPE from LOG_CATEGORIES where REC_STATUS = 1 and  LOG_TYPE = @messageType)
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
			REC_DATE,
			SERVER_IP,
			SERVER_LOCATION,
			SERVER_TOKEN_ID
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
			GetDate(),
			@serverIPAddress,
			@serverLocation, 
			@serverTokenID	
		)
	end

end

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RecordHelloEvent]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordHelloEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[RecordHelloEvent]
	@location nvarchar(100),
	@serialNumber nvarchar(50),
	@modelName nvarchar(50),
	@ipAddress nvarchar(30),
	@hostName nvarchar(100),
	@deviceId nvarchar(50),
	@accessAddress nvarchar(100),
	@isEAMEnabled bit

as 
begin
if(@serialNumber <> '''')
begin
	if exists (SELECT MFP_IP FROM M_MFPS where MFP_SERIALNUMBER = @serialNumber)
		begin
			update M_MFPS set 
				MFP_LOCATION = @location, 
				MFP_DEVICE_ID = @deviceId,
				MFP_EAM_ENABLED = @isEAMEnabled, 
				MFP_IP = @ipAddress,
				FTP_ADDRESS = @ipAddress,
				REC_ACTIVE = 1
				where MFP_SERIALNUMBER = @serialNumber 
		 
			update M_MFPS set LAST_LOGGEDIN_TIME=null, LAST_LOGGEDIN_USER=null where MFP_IP=@ipAddress
		end	
	else if exists (SELECT MFP_IP FROM M_MFPS where MFP_IP = @ipAddress )
		begin
		update M_MFPS set 
				MFP_LOCATION = @location, 
				MFP_DEVICE_ID = @deviceId,
				MFP_EAM_ENABLED = @isEAMEnabled, 
				MFP_IP = @ipAddress,
				FTP_ADDRESS = @ipAddress,
				REC_ACTIVE = 1,
				MFP_SERIALNUMBER = @serialNumber 
				where MFP_IP = @ipAddress
		end
	else 
	begin
		if exists (select APPSETNG_KEY from APP_SETTINGS where APPSETNG_KEY = ''Self Register Device'' and APPSETNG_VALUE = ''Enable'')
		begin
				insert into M_MFPS
				(
					MFP_LOCATION, MFP_SERIALNUMBER, MFP_IP, MFP_DEVICE_ID, MFP_NAME, MFP_SSO,
					MFP_LOCK_DOMAIN_FIELD, MFP_URL, MFP_LOGON_MODE, MFP_LOGON_AUTH_SOURCE, MFP_MANUAL_AUTH_TYPE, 
					MFP_CARDREADER_TYPE, ALLOW_NETWORK_PASSWORD, REC_ACTIVE, FTP_ADDRESS, FTP_PORT,
					FTP_PROTOCOL, MFP_PRINT_API, MFP_EAM_ENABLED,MFP_HOST_NAME
				)
				values
				(
					@location , @serialNumber, @ipAddress, @deviceId, @modelName , ''1'',
					''0'', @accessAddress, ''Manual'', ''DB'', ''Username/Password'',
					''PC'', ''False'' , ''1'', @ipAddress, ''21'', ''ftp'', ''FTP'', @isEAMEnabled,@hostName
				)
				insert into T_GROUP_MFPS
				(
					GRUP_ID,MFP_IP,REC_ACTIVE,REC_DATE
				)
				values
				(
					''1'',@ipAddress,''1'', getdate()
				)
		end	
	end
end
end' 
END
GO

/****** Object:  StoredProcedure [dbo].[RecordJobEvent]    Script Date: 2/19/2016 2:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecordJobEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
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
 @limitsOn varchar(20),  
 @osaJobCount int,  
 @costCenter int,  
 @serverIPAddress varchar(50),  
 @serverLocation nvarchar(100),   
 @serverTokenID int,
@userNameExternal nvarchar(50)   
as  
  
declare @jobType nvarchar(30)  
declare @costCenterAssigned nvarchar(100)  
declare @userName nvarchar(50)  
declare @userID varchar(30)  
declare @domainName nvarchar(50)  
declare @isUserExists bit    
declare @isMFPExists bit    
declare @itemCount int  
declare @colorPricePerUnit float  
declare @monochromePricePerUnit float  
declare @authenticationsource varchar(10)  
declare @userDepartment nvarchar(30)  
set @userDepartment = ''''  
declare @referenceNO nvarchar(50)  
declare @colorPriceTotal float  
declare @monochromeTotal float  
declare @jobPriceTotal float  
  
declare @totalSheetCount int  

declare @department nvarchar(200)  
declare @company nvarchar(200) 
declare @location nvarchar(200) 
declare @mfpName nvarchar(200) 
declare @externalUser nvarchar(10) 
  
set @costCenterAssigned = ''''  
set @jobType = ''''  
  
if exists( select MFP_IP from M_MFPS where MFP_IP = @deviceIP)  
begin  
 print ''MFP found IP : '' + @deviceIP   
   
 select @userName = USR_NAME,  @userID = USR_ID, @domainName =USR_DOMAIN, @authenticationsource= USR_SOURCE from M_USERS where USR_ACCOUNT_ID = @userAccountId  

 set @externalUser = (select EXTERNAL_SOURCE from M_USERS where USR_ACCOUNT_ID = @userAccountId)

 print ''User found User ID: '' + @userID  
 if(@userAccountId = ''100'')  
 begin  
  set @userID = ''Unknown''  
  set @costCenter = ''1''  
  set @userName = ''Unknown''  
 end  
  if(@userAccountId = ''-500'')  
 begin  
  set @userID = ''System-FAX-RX''  
  set @costCenter = ''1''  
  set @userName = ''System-FAX-RX''  
 end  
 if  @userID <> ''''  
  begin  
   print ''User found User ID: '' + @userID  
  
  set @department = (select DEPARTMENT from M_USERS where USR_ID =  @userID)
  set @company = (select COMPANY from M_USERS where USR_ID =  @userID)
  set @location = (select MFP_LOCATION from M_MFPS where MFP_IP =  @deviceIP)
  set @mfpName = (select MFP_NAME from M_MFPS where MFP_IP = @deviceIP)
   -- Authentication Source  
   --select @authenticationsource = MFP_LOGON_AUTH_SOURCE from M_MFPS where MFP_IP = @deviceIP  
   -- TBD : Check whether permissions are defined for the user on MFP  
  
   -- Get cost Center Assigned  
     
   select @costCenterAssigned = COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID = @costCenter  
     
   if @costCenterAssigned <> null  print ''Cost Center : '' + @costCenterAssigned  
        
   -- Get Price for Paper Size, Cost Center, MFP  
   create table #jobPrice(colorPrice float, monochromePrice float)  
   insert into #jobPrice exec GetJobPrice  @deviceIP, @costCenter , @jobMode, @paperSize, @limitsOn  
   select @colorPricePerUnit = colorPrice, @monochromePricePerUnit = monochromePrice from #jobPrice   
     
   set @colorPriceTotal = @colorPricePerUnit * @colorSheetCount  
   set @monochromeTotal = @monochromePricePerUnit * @monochromeSheetCount  
   set @jobPriceTotal = @colorPriceTotal + @monochromeTotal  
   set @totalSheetCount = @monochromeSheetCount + @colorSheetCount  
  
   declare @recordsCount int  
      print ''record count''       
   select @recordsCount = count(REC_SLNO) from T_JOB_LOG where JOB_ID = @jobNo and JOB_MODE = @jobMode  
   print @recordsCount
         
   if(@recordsCount >= 0)
    begin 
		print ''insert''		
		if exists(select REC_SLNO from T_JOB_LOG where JOB_ID = @jobNo and JOB_STATUS = ''ERROR'' )
		begin
			Update T_JOB_LOG set JOB_SHEET_COUNT_COLOR = 0, JOB_SHEET_COUNT_BW = 0, JOB_SHEET_COUNT = 0,OSA_JOB_COUNT = 0
			Where JOB_ID = @jobNo and JOB_STATUS = ''ERROR'' and MFP_IP = @deviceIP
		end
		--delete  T_JOB_LOG where JOB_ID=@jobNo and JOB_STATUS=''ERROR''
		if(@externalUser = ''email'')
		begin
		insert into T_JOB_LOG
		 (  
		  MFP_IP, MFP_MACADDRESS, GRUP_ID, USR_ID, USR_SOURCE,  
		  USR_DEPT, JOB_ID, JOB_MODE, JOB_TYPE, JOB_USRNAME,   
		  JOB_COMPUTER, JOB_START_DATE, JOB_END_DATE, JOB_COLOR_MODE, JOB_SHEET_COUNT_COLOR,  
		  JOB_SHEET_COUNT_BW, JOB_SHEET_COUNT, JOB_PRICE_COLOR, JOB_PRICE_BW, JOB_PRICE_TOTAL,  
		  JOB_STATUS, JOB_PAPER_SIZE_ORIGINAL, JOB_PAPER_SIZE, JOB_FILE_NAME, DUPLEX_MODE,OSA_JOB_COUNT,DOMAIN_NAME,USER_ACCOUNT_ID,SERVER_IP,SERVER_LOCATION,SERVER_TOKEN_ID,COST_CENTER_NAME,
		  DEPARTMENT,COMPANY,LOCATION,MFPNAME,EXTERNAL_SOURCE
		 )  
		 values  
		 (  
		  @deviceIP, @deviceMacAddress, @costCenter, @userID, @authenticationsource,  
		  @userDepartment, @jobNo, @jobMode, @jobType, @userName,  
		  @jobComputer, @startDate, @endDate, @colorMode, @colorSheetCount,  
		  @monochromeSheetCount, @totalSheetCount, @colorPriceTotal, @monochromeTotal, @jobPriceTotal,  
		  @jobStatus, @originalPaperSize, @paperSize, @fileName, @duplexMode, @osaJobCount, @domainName,@userAccountId,@serverIPAddress,@serverLocation,@serverTokenID,@costCenterAssigned,
		  @department,@company,@location,@mfpName,''email''
		 )
		end
		else
		begin
		insert into T_JOB_LOG
		 (  
		  MFP_IP, MFP_MACADDRESS, GRUP_ID, USR_ID, USR_SOURCE,  
		  USR_DEPT, JOB_ID, JOB_MODE, JOB_TYPE, JOB_USRNAME,   
		  JOB_COMPUTER, JOB_START_DATE, JOB_END_DATE, JOB_COLOR_MODE, JOB_SHEET_COUNT_COLOR,  
		  JOB_SHEET_COUNT_BW, JOB_SHEET_COUNT, JOB_PRICE_COLOR, JOB_PRICE_BW, JOB_PRICE_TOTAL,  
		  JOB_STATUS, JOB_PAPER_SIZE_ORIGINAL, JOB_PAPER_SIZE, JOB_FILE_NAME, DUPLEX_MODE,OSA_JOB_COUNT,DOMAIN_NAME,USER_ACCOUNT_ID,SERVER_IP,SERVER_LOCATION,SERVER_TOKEN_ID,COST_CENTER_NAME,
		  DEPARTMENT,COMPANY,LOCATION,MFPNAME
		 )  
		 values  
		 (  
		  @deviceIP, @deviceMacAddress, @costCenter, @userID, @authenticationsource,  
		  @userDepartment, @jobNo, @jobMode, @jobType, @userName,  
		  @jobComputer, @startDate, @endDate, @colorMode, @colorSheetCount,  
		  @monochromeSheetCount, @totalSheetCount, @colorPriceTotal, @monochromeTotal, @jobPriceTotal,  
		  @jobStatus, @originalPaperSize, @paperSize, @fileName, @duplexMode, @osaJobCount, @domainName,@userAccountId,@serverIPAddress,@serverLocation,@serverTokenID,@costCenterAssigned,
		  @department,@company,@location,@mfpName
		 )
		 end
		if exists(select REC_SLNO from T_JOB_LOG where JOB_ID = @jobNo and JOB_STATUS = ''ERROR'' )
		begin
			set	@referenceNO = (select top (1) REC_SLNO from T_JOB_LOG order by REC_DATE  DESC)
			Update T_JOB_LOG set NOTE =  ''Please refer to Reference No : '' + @referenceNO + '' for detail job log''
			Where JOB_ID = @jobNo and JOB_STATUS = ''ERROR''
			
		end
		  
    end  
  -- else  
	 --  begin  
		--if(@jobMode = ''Doc Filing Print'' )  
		-- begin  
	        
		--  truncate table #jobPrice  
		--  insert into #jobPrice exec GetJobPrice  @deviceIP, @costCenter , @jobMode, @paperSize, @limitsOn  
		--  declare @recID int        
		--  select @recID = REC_SLNO, @colorSheetCount = JOB_SHEET_COUNT_COLOR, @monochromeSheetCount = JOB_SHEET_COUNT_BW from T_JOB_LOG where JOB_ID = @jobNo and MFP_IP =@deviceIP  
		--  select @colorPricePerUnit = colorPrice, @monochromePricePerUnit = monochromePrice from #jobPrice   
	     
		--  set @colorPriceTotal = @colorPricePerUnit * @colorSheetCount  
		--  set @monochromeTotal = @monochromePricePerUnit * @monochromeSheetCount  
		--  set @jobPriceTotal = @colorPriceTotal + @monochromeTotal  
		--  set @totalSheetCount = @monochromeSheetCount + @colorSheetCount  
	  
		--  update T_JOB_LOG set JOB_STATUS = @jobStatus, JOB_PRICE_COLOR = @colorPriceTotal, JOB_PRICE_BW = @monochromeTotal, JOB_PRICE_TOTAL = @colorPriceTotal + @monochromeTotal, JOB_PAPER_SIZE= @paperSize, OSA_JOB_COUNT = @osaJobCount where REC_SLNO = @recID  
		-- end  
		--else  
		-- begin    
		  
		  
		--  update T_JOB_LOG set OSA_JOB_COUNT = @osaJobCount where JOB_ID = @jobNo and MFP_IP =@deviceIP  
		-- end  
	 --  end  
	 end  
end  
update M_MFPS set MFP_LAST_UPDATE = getdate(), MFP_STATUS = 1 where MFP_IP = @deviceIP


select JOB_USED_UPDATED from T_JOB_LOG where JOB_ID = @jobNo and JOB_MODE = @jobMode and MFP_IP = @deviceIP 
select @colorPriceTotal as colorPrice, @monochromeTotal as BWPrice,@jobPriceTotal as jobPriceTotal, @totalSheetCount as TotalSheetCount ' 
END
GO

/****** Object:  StoredProcedure [dbo].[ReleaeLocks]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReleaeLocks]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ReleaeLocks] 
AS
BEGIN

Begin try

			SELECT 

			 blocking_session_id AS BlockingSessionID,

			 session_id AS VictimSessionID,     

			 (SELECT [text] FROM sys.sysprocesses 

			  CROSS APPLY sys.dm_exec_sql_text([sql_handle]) 

			  WHERE spid = blocking_session_id) AS BlockingQuery,
	  
			 [text] AS VictimQuery,

			 wait_time/1000 AS WaitDurationSecond,

			 wait_type AS WaitType,

			 percent_complete AS BlockingQueryCompletePercent

		into #LOCKS FROM sys.dm_exec_requests 

		CROSS APPLY sys.dm_exec_sql_text([sql_handle]) 

		WHERE blocking_session_id > 0 and database_id = DB_ID(''AccountingPlusDB'')


		declare @totalLocks int
		declare @blockingSessionID int
		declare @sqlQuery varchar(50)

		select distinct BlockingSessionID into #LOCKSTOBEKILLED from #LOCKS
		alter table #LOCKSTOBEKILLED add REC_SLNO int identity

		select @totalLocks = count(BlockingSessionID) from #LOCKSTOBEKILLED
		if @totalLocks > 0
		begin
			while(@totalLocks > 0)
			Begin
				select @blockingSessionID = BlockingSessionID from #LOCKSTOBEKILLED where REC_SLNO = @totalLocks
				set @sqlQuery = ''KILL '' + cast(@blockingSessionID  as nvarchar(50))
				Exec(@sqlQuery)
				set @totalLocks = @totalLocks - 1
			end
		end


		select * from #LOCKS
end try

begin catch


end catch

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveMfpOrDeviceFromCostProfile]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveMfpOrDeviceFromCostProfile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[RemoveMfpOrDeviceFromCostProfile] @selectedCostCenter int, @selectedMFPs text, @assignedTo nvarchar(20)
as
select TokenVal into #SelectedMFPs from ConvertStringListToTable(@selectedMFPs, '','')

delete from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID = @selectedCostCenter
and ASSIGNED_TO = @assignedTo and MFP_GROUP_ID in (select TokenVal from #SelectedMFPs)
' 
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveMfpsFromGroup]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveMfpsFromGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[RemoveMfpsFromGroup] @groupID int, @mfpList text

as

--if @mfpList <> ''''
begin
	select TokenVal into #SelectedMFPs from ConvertStringListToTable(@mfpList, '','')
	delete from T_GROUP_MFPS where GRUP_ID = @groupID and MFP_IP in (select TokenVal from #SelectedMFPs)
end' 
END
GO
/****** Object:  StoredProcedure [dbo].[RemovePermissions]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemovePermissions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[RemovePermissions]
	@GRUPID int,
	@PermissionsAndLimitsOn tinyint
AS
BEGIN
	delete from T_JOB_PERMISSIONS_LIMITS where [USER_ID]=@GRUPID and PERMISSIONS_LIMITS_ON=@PermissionsAndLimitsOn
	if(@PermissionsAndLimitsOn = 0)
		begin
			Update M_COST_CENTERS set ALLOW_OVER_DRAFT=''False'' where COSTCENTER_ID=@GRUPID
		end	
	else
		begin
			Update M_USERS set ALLOW_OVER_DRAFT=''False'' where USR_ACCOUNT_ID=@GRUPID
		end
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ReportBuilder]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportBuilder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ReportBuilder]
	@ReportOn varchar(50),
	@authenticationSource varchar(2),
	@fromDate varchar(10),
	@toDate  varchar(10),
	@JobStaus varchar(100)
	
as

select * into #joblog from T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStaus, '''')) )  and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 

alter table #joblog add MFP_SERIALNUMBER nvarchar(50), MFP_MODEL nvarchar(50), MFP_LOCATION nvarchar(100)

create table #JobReport
(
	slno int identity,UserID nvarchar(100) default '''',
	ReportOn nvarchar(100) default '''', Total int default 0, TotalBW int default 0, TotalColor int default 0
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

set @sqlGroupOn = @ReportOn --''MFP_IP,GRUP_ID,JOB_COMPUTER,USR_ID''

set @sqlQuery = ''insert into #JobReport (ReportOn) select distinct '' +  @sqlGroupOn + '' from #joblog''
exec(@sqlQuery)
create table #TempGroupCount(itemGroup nvarchar(100), itemCount int default 0)
print @sqlQuery

-- A3BW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set A3BW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A3C
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A3C = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A4BW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set A4BW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A4C
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A4C = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- OtherBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set OtherBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- OtherC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE not like ''''A4%''''  and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set OtherC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- Duplex_One_Sided
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where DUPLEX_MODE = ''''1SIDED'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT) from #joblog where  DUPLEX_MODE = ''1SIDED'' group by MFP_IP
update #JobReport set Duplex_One_Sided = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- Duplex_Two_Sided
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where DUPLEX_MODE <> ''''1SIDED'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT) from #joblog where  DUPLEX_MODE <> ''1SIDED'' group by MFP_IP
update #JobReport set Duplex_Two_Sided = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn 
update #JobReport set Duplex_Two_Sided = Duplex_Two_Sided / 2
truncate table #TempGroupCount



update #JobReport set TotalBW = A3BW + A4BW + OtherBW, TotalColor = A3C + A4C + OtherC
update #JobReport set Total = TotalBW + TotalColor


/*
,UserName nvarchar(100) default ''''
	,Department nvarchar(100) default ''''
	,ComputerName nvarchar(100) default ''''
	,LoginName nvarchar(100) default ''''
	,ModelName nvarchar(100) default ''''
	,SerialNumber nvarchar(100) default ''''
	,AuthenticationSource char(2) default ''''
	,CostCenter nvarchar(100) default ''''
	,GroupID nvarchar(100) default ''''
*/


if @sqlGroupOn = ''USR_ID''
begin
	alter table #JobReport add UserName nvarchar(50) default ''''
	update #JobReport set  UserName = M_USERS.USR_NAME  from M_USERS where M_USERS.USR_ID = #JobReport.ReportOn 
	insert into #JobReport(ReportOn,Total,TotalBW,TotalColor,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
	
	select ''Total'',sum(Total),sum(TotalBW),sum(TotalColor),Sum(A3BW),sum(A3C),Sum(A4BW),Sum(A4C),sum(OtherBW),sum(OtherC),sum(Duplex_One_Sided),Sum(Duplex_Two_Sided) from #JobReport
	select ReportOn as UserID ,Total,TotalBW,TotalColor,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,UserName from #JobReport
	Select * from #JobReport
end

if @sqlGroupOn = ''MFP_IP''
begin
	alter table #JobReport add ModelName nvarchar(50) default '''', SerialNumber nvarchar(50) default '''', Location nvarchar(50) default '''',MFPHOSTNAME nvarchar(100) default '''',MFPIP nvarchar(50) default ''''			
	update #JobReport set ModelName = M_MFPS.MFP_MODEL,  SerialNumber = M_MFPS.MFP_SERIALNUMBER, Location = M_MFPS.MFP_LOCATION, MFPHOSTNAME = M_MFPS.MFP_HOST_NAME, MFPIP = #JobReport.ReportOn  from M_MFPS where  M_MFPS.MFP_IP = #JobReport.ReportOn
	insert into #JobReport(SerialNumber,Total,TotalBW,TotalColor,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
	select	''Total'',sum(Total),sum(TotalBW),sum(TotalColor),Sum(A3BW),sum(A3C),Sum(A4BW),Sum(A4C),sum(OtherBW),sum(OtherC),sum(Duplex_One_Sided),Sum(Duplex_Two_Sided) from #JobReport
	select SerialNumber as SerialNumber ,Total,TotalBW,TotalColor,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided,ModelName,Location,MFPHOSTNAME,MFPIP from #JobReport
end

if @sqlGroupOn = ''GRUP_ID''
begin
--select * from #JobReport
	alter table #JobReport add CostCenter nvarchar(100) default ''''
	update #JobReport set CostCenter = #joblog.COST_CENTER_NAME from  #joblog where #JobReport.ReportOn = CAST(#joblog.GRUP_ID AS varchar(100)) 
	insert into #JobReport(CostCenter,Total,TotalBW,TotalColor,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
	select	''Total'',sum(Total),sum(TotalBW),sum(TotalColor),Sum(A3BW),sum(A3C),Sum(A4BW),Sum(A4C),sum(OtherBW),sum(OtherC),sum(Duplex_One_Sided),Sum(Duplex_Two_Sided) from #JobReport
	select CostCenter as CostCenter,Total,TotalBW,TotalColor,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided from #JobReport
end

if @sqlGroupOn = ''JOB_COMPUTER''
begin
	alter table #JobReport add ComputerName nvarchar(100) default ''''
	update #JobReport set ComputerName = #joblog.JOB_COMPUTER from  #joblog where #JobReport.ReportOn = #joblog.JOB_COMPUTER
	insert into #JobReport(ComputerName,Total,TotalBW,TotalColor,A3BW,A3C,A4BW,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided)
	select	''Total'',sum(Total),sum(TotalBW),sum(TotalColor),Sum(A3BW),sum(A3C),Sum(A4BW),Sum(A4C),sum(OtherBW),sum(OtherC),sum(Duplex_One_Sided),Sum(Duplex_Two_Sided) from #JobReport
	select ComputerName as ComputerName,Total,TotalBW,TotalColor,A3BW,A4BW,A3C,A4C,OtherBW,OtherC,Duplex_One_Sided,Duplex_Two_Sided from #JobReport

end

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ReportBuilderJobTYPE]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportBuilderJobTYPE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ReportBuilderJobTYPE]
      @ReportOn varchar(50),
      @authenticationSource varchar(2),
      @fromDate varchar(10),
      @toDate  varchar(10),
      @JobStatus varchar(100)
      
      
as

select * into #joblog from T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) )  and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 

alter table #joblog add MFP_SERIALNUMBER nvarchar(50), MFP_MODEL nvarchar(50), MFP_LOCATION nvarchar(100)

create table #JobReport
(
      slno int identity,UserID nvarchar(100) default '''',
      ReportOn nvarchar(100) default '''',Total int default 0, TotalBW int default 0, TotalColor int default 0
      ,A3PrintBW int default 0
      ,A3PrintC int default 0
      ,A3CopyBW int default 0
      ,A3CopyC int default 0
      ,A3ScanBW int default 0
      ,A3ScanC int default 0  
      ,A3FaxBW int default 0
      ,A3FaxC int default 0
      ,A4PrintBW int default 0      
      ,A4PrintC int default 0
      ,A4CopyBW int default 0
      ,A4CopyC int default 0
      ,A4ScanBW int default 0
      ,A4ScanC int default 0
      ,A4FaxBW int default 0
      ,A4FaxC int default 0
      ,OtherPrintBW int default 0
      ,OtherPrintC int default 0
      ,OtherCopyBW int default 0
      ,OtherCopyC int default 0
      ,OtherScanBW int default 0
      ,OtherScanC int default 0
      ,OtherFaxBW int default 0
      ,OtherFaxC int default 0
      ,DocFilingScanBW int default 0
      ,DocFilingScanC int default 0
      ,DocFilingScanBWOthers int default 0
      ,DocFilingScanCOthers int default 0
      ,DocFilingScanA3BW int default 0
      ,DocFilingScanA3C int default 0
      ,DocFilingPrintBW int default 0
      ,DocFilingPrintC int default 0
      ,DocFilingPrintA3BW int default 0
      ,DocFilingPrintA3C int default 0
      ,DocFilingPrintBWOthers int default 0
      ,DocFilingPrintCOthers int default 0
      ,Duplex_One_Sided float default 0
      ,Duplex_Two_Sided float default 0   
)

declare @sqlQuery nvarchar(max)
declare @sqlGroupOn nvarchar(100)

--create table #reportGroup groupItem nvarchar(100)

--insert into #JobReport (ReportOn) select distinct JOB_COMPUTER from #joblog

set @sqlGroupOn = @ReportOn --''MFP_IP,GRUP_ID,JOB_COMPUTER,USR_ID,JOB_TYPE''

set @sqlQuery = ''insert into #JobReport (ReportOn) select distinct '' +  @sqlGroupOn + '' from #joblog''
exec(@sqlQuery)
create table #TempGroupCount(itemGroup nvarchar(100), itemCount int default 0)
print @sqlQuery

-- A3PrintBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''PRINT'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set A3PrintBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A3PrintC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''PRINT'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A3PrintC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A3COPYBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''COPY'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set A3CopyBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A3COPYC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''COPY'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A3CopyC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A3ScanBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''SCANNER'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set A3ScanBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A3ScanC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''SCANNER'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A3ScanC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A3FaxBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''FAX'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set A3FaxBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A3FaxC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''FAX'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A3FaxC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A4PRINTBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''PRINT'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set A4PrintBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A4PRINTC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''PRINT'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A4PrintC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A4COPYBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''COPY'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set A4CopyBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A4COPYC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''COPY'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A4CopyC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- A4SCANBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''SCANNER'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set A4ScanBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A4SCANC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''SCANNER'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A4ScanC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- OtherPRINTBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''PRINT'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set OtherPrintBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- OtherPRINTC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE not like ''''A4%''''  and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''PRINT''''  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set OtherPrintC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A4FaxBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''FAX'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set A4FaxBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A4FaxC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''FAX'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set A4FaxC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--OtherCopyBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''COPY'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set OtherCopyBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--OtherCopyC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE not like ''''A4%''''  and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''COPY'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set OtherCopyC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- OtherScanBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''SCANNER'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set OtherScanBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--OtherScanC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''SCANNER'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set OtherScanC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--OtherFaxBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''FAX'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set OtherFaxBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--OtherFaxC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''FAX'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A3%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set OtherFaxC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A4DocFilingPrintBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingPrintBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--DocFilingPrintA3BW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingPrintA3BW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--DocFilingPrintA3C
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingPrintA3C = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--DocFilingPrintBWOthers
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingPrintBWOthers = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--DocFilingPrintCOthers
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingPrintCOthers = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A4DocFilingPrintC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Print''''  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set DocFilingPrintC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A4DocFilingScanBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Scan'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingScanBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--A4DocFilingScanC
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A4%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Scan'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''A4%'' and JOB_COLOR_MODE in(''FULL-COLOR'', ''SINGLE-COLOR'', ''DUAL-COLOR'') group by MFP_IP
update #JobReport set DocFilingScanC = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--DocFilingScanA3BW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Scan'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingScanA3BW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--DocFilingScanA3C
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Scan'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingScanA3C = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--DocFilingScanBWOthers
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''MONOCHROME'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Scan'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingScanBWOthers = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--DocFilingScanCOthers
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog where JOB_PAPER_SIZE not like ''''A4%'''' and JOB_PAPER_SIZE not like ''''A3%'''' and JOB_COLOR_MODE in(''''FULL-COLOR'''', ''''SINGLE-COLOR'''', ''''DUAL-COLOR'''', ''''AUTO'''') and JOB_MODE = ''''Doc Filing Scan'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT_BW) from #joblog where (JOB_PAPER_SIZE not like ''A4%''  or JOB_PAPER_SIZE not like ''A3%'') and JOB_COLOR_MODE = ''MONOCHROME'' group by MFP_IP
update #JobReport set DocFilingScanCOthers = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- Duplex_One_Sided
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where DUPLEX_MODE = ''''1SIDED'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT) from #joblog where  DUPLEX_MODE = ''1SIDED'' group by MFP_IP
update #JobReport set Duplex_One_Sided = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- Duplex_Two_Sided
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where DUPLEX_MODE <> ''''1SIDED'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT) from #joblog where  DUPLEX_MODE <> ''1SIDED'' group by MFP_IP
update #JobReport set Duplex_Two_Sided = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn 
update #JobReport set Duplex_Two_Sided = Duplex_Two_Sided / 2
truncate table #TempGroupCount

update #JobReport set TotalBW = A3PrintBW + A3CopyBW +A3ScanBW + A3FaxBW + A4PrintBW + A4CopyBW + A4ScanBW + A4FaxBW + OtherPrintBW  + OtherCopyBW + OtherScanBW + OtherFaxBW + DocFilingPrintBW + DocFilingScanBW + DocFilingPrintBWOthers + DocFilingScanBWOthers + DocFilingPrintA3BW + DocFilingScanA3BW , 
TotalColor = A3PrintC + A3ScanC + A3CopyC + A3FaxC + A4PrintC + A4CopyC  + A4ScanC + A4FaxC + OtherPrintC + OtherCopyC + OtherScanC + OtherFaxC + DocFilingPrintC + DocFilingScanC + DocFilingPrintCOthers + DocFilingScanCOthers + DocFilingPrintA3C + DocFilingScanA3C
update #JobReport set Total = TotalBW + TotalColor 

/*
,UserName nvarchar(100) default ''''
      ,Department nvarchar(100) default ''''
      ,ComputerName nvarchar(100) default ''''
      ,LoginName nvarchar(100) default ''''
      ,ModelName nvarchar(100) default ''''
      ,SerialNumber nvarchar(100) default ''''
      ,AuthenticationSource char(2) default ''''
      ,CostCenter nvarchar(100) default ''''
      ,GroupID nvarchar(100) default ''''
*/


if @sqlGroupOn = ''USR_ID''
begin

      alter table #JobReport add UserName nvarchar(50) default ''''
      update #JobReport set  UserName = M_USERS.USR_NAME  from M_USERS where M_USERS.USR_ID = #JobReport.ReportOn 
      insert into #JobReport(ReportOn,Total,TotalBW,TotalColor,A3PrintBW,A3PrintC,A3CopyBW,A3CopyC,A3ScanBW,A3ScanC,A4PrintBW,A4PrintC,OtherPrintBW,OtherPrintC,A4CopyBW,A4CopyC,OtherCopyBW,OtherCopyC, OtherScanBW,OtherScanC,A4FaxBW,A3FaxBW,OtherFaxBW,A4FaxC,A3FaxC,OtherFaxC,DocFilingPrintA3C,DocFilingScanA3C,DocFilingPrintBW,DocFilingPrintC,DocFilingPrintA3BW,DocFilingScanBW,DocFilingScanC,DocFilingScanA3BW,DocFilingScanBWOthers,DocFilingScanCOthers,DocFilingPrintBWOthers,DocFilingPrintCOthers,A4ScanBW,A4ScanC, Duplex_One_Sided,Duplex_Two_Sided)
      
      select ''Total'',isnull(sum(Total),0),isnull(sum(TotalBW),0),isnull(sum(TotalColor),0),isnull(Sum(A3PrintBW),0),isnull(sum(A3PrintC),0),isnull(sum(A3CopyBW),0),isnull(sum(A3CopyC),0),isnull(sum(A3ScanBW),0),isnull(sum(A3ScanC),0), isnull(Sum(A4PrintBW),0),isnull(Sum(A4PrintC),0),isnull(sum(OtherPrintBW),0),isnull(sum(OtherPrintC),0),isnull(sum (A4CopyBW),0),isnull(SUM(A4CopyC),0),isnull(SUM(OtherCopyBW),0),isnull(SUM(OtherCopyC),0),isnull(SUM(OtherScanBW),0),isnull(SUM(OtherScanC),0),isnull(SUM(A4FaxBW),0),isnull(SUM(A3FaxBW),0),isnull(SUM(OtherFaxBW),0),isnull(SUM(A4FaxC),0),isnull(SUM(A3FaxC),0),isnull(SUM(OtherFaxC),0),isnull(SUM(DocFilingPrintA3C),0),isnull(SUM(DocFilingScanA3C),0),isnull(SUM(DocFilingPrintBW),0),isnull(SUM(DocFilingPrintC),0),isnull(SUM(DocFilingPrintA3BW),0),isnull(SUM(DocFilingScanBW),0),isnull(SUM(DocFilingScanC),0),isnull(SUM(DocFilingScanA3BW),0),isnull(SUM(DocFilingScanBWOthers),0),isnull(SUM(DocFilingScanCOthers),0),isnull(SUM(DocFilingPrintBWOthers),0),isnull(SUM(DocFilingPrintCOthers),0),isnull(SUM(A4ScanBW),0),isnull(SUM(A4ScanC),0), isnull(sum(Duplex_One_Sided),0),isnull(Sum(Duplex_Two_Sided),0) from #JobReport
      --select ReportOn as UserID ,Total,TotalBW,TotalColor,A3PrintBW,A4PrintBW,A3PrintC,A4PrintC,OtherPrintBW,OtherPrintC,A4CopyBW,A3CopyC,OtherCopyBW,OtherCopyC,OtherScanBW,OtherScanC, Duplex_One_Sided,Duplex_Two_Sided,UserName from #JobReport
      Select * from #JobReport
end

if @sqlGroupOn = ''MFP_IP''
begin
      alter table #JobReport add ModelName nvarchar(50) default '''', SerialNumber nvarchar(50) default '''', Location nvarchar(50) default '''',MFPHOSTNAME nvarchar(100) default '''',MFPIP nvarchar(50) default ''''                
      update #JobReport set ModelName = M_MFPS.MFP_MODEL,  SerialNumber = M_MFPS.MFP_SERIALNUMBER, Location = M_MFPS.MFP_LOCATION, MFPHOSTNAME = M_MFPS.MFP_HOST_NAME, MFPIP = #JobReport.ReportOn  from M_MFPS where  M_MFPS.MFP_IP = #JobReport.ReportOn
      insert into #JobReport(SerialNumber,Total,TotalBW,TotalColor,A3PrintBW,A3PrintC,A3CopyBW,A3CopyC,A3ScanBW,A3ScanC,A4PrintBW,A4PrintC,OtherPrintBW,OtherPrintC,A4CopyBW,A4CopyC,OtherCopyBW,OtherCopyC, OtherScanBW,OtherScanC,A4FaxBW,A3FaxBW,OtherFaxBW,A4FaxC,A3FaxC,OtherFaxC,DocFilingPrintA3C,DocFilingScanA3C,DocFilingPrintBW,DocFilingPrintC,DocFilingPrintA3BW,DocFilingScanBW,DocFilingScanC,DocFilingScanA3BW,DocFilingScanBWOthers,DocFilingScanCOthers,DocFilingPrintBWOthers,DocFilingPrintCOthers,A4ScanBW,A4ScanC, Duplex_One_Sided,Duplex_Two_Sided)
      select      ''Total'',isnull(sum(Total),0),isnull(sum(TotalBW),0),isnull(sum(TotalColor),0),isnull(Sum(A3PrintBW),0),isnull(sum(A3PrintC),0),isnull(sum(A3CopyBW),0),isnull(sum(A3CopyC),0),isnull(sum(A3ScanBW),0),isnull(sum(A3ScanC),0), isnull(Sum(A4PrintBW),0),isnull(Sum(A4PrintC),0),isnull(sum(OtherPrintBW),0),isnull(sum(OtherPrintC),0),isnull(sum (A4CopyBW),0),isnull(SUM(A4CopyC),0),isnull(SUM(OtherCopyBW),0),isnull(SUM(OtherCopyC),0),isnull(SUM(OtherScanBW),0),isnull(SUM(OtherScanC),0),isnull(SUM(A4FaxBW),0),isnull(SUM(A3FaxBW),0),isnull(SUM(OtherFaxBW),0),isnull(SUM(A4FaxC),0),isnull(SUM(A3FaxC),0),isnull(SUM(OtherFaxC),0),isnull(SUM(DocFilingPrintA3C),0),isnull(SUM(DocFilingScanA3C),0),isnull(SUM(DocFilingPrintBW),0),isnull(SUM(DocFilingPrintC),0),isnull(SUM(DocFilingPrintA3BW),0),isnull(SUM(DocFilingScanBW),0),isnull(SUM(DocFilingScanC),0),isnull(SUM(DocFilingScanA3BW),0),isnull(SUM(DocFilingScanBWOthers),0),isnull(SUM(DocFilingScanCOthers),0),isnull(SUM(DocFilingPrintBWOthers),0),isnull(SUM(DocFilingPrintCOthers),0),isnull(SUM(A4ScanBW),0),isnull(SUM(A4ScanC),0), isnull(sum(Duplex_One_Sided),0),isnull(Sum(Duplex_Two_Sided),0) from #JobReport
      --select SerialNumber as SerialNumber ,Total,TotalBW,TotalColor,A3PrintBW,A4PrintBW,A3PrintC,A4PrintC,OtherPrintBW,OtherPrintC,Duplex_One_Sided,Duplex_Two_Sided,ModelName,Location,MFPHOSTNAME,MFPIP from #JobReport
      Select * from #JobReport
end

if @sqlGroupOn = ''GRUP_ID''
begin
--select * from #JobReport
      alter table #JobReport add CostCenter nvarchar(100) default ''''
      update #JobReport set CostCenter = #joblog.COST_CENTER_NAME from  #joblog where #JobReport.ReportOn = CAST(#joblog.GRUP_ID AS varchar(100)) 
      insert into #JobReport(CostCenter,Total,TotalBW,TotalColor,A3PrintBW,A3PrintC,A3CopyBW,A3CopyC,A3ScanBW,A3ScanC,A4PRINTBW,A4PRINTC,OtherPRINTBW,OtherPRINTC,A4CopyBW,A4CopyC,OtherCopyBW,OtherCopyC, OtherScanBW,OtherScanC,A4FaxBW,A3FaxBW,OtherFaxBW,A4FaxC,A3FaxC,OtherFaxC,DocFilingPrintA3C,DocFilingScanA3C,DocFilingPrintBW,DocFilingPrintC,DocFilingPrintA3BW,DocFilingScanBW,DocFilingScanC,DocFilingScanA3BW,DocFilingScanBWOthers,DocFilingScanCOthers,DocFilingPrintBWOthers,DocFilingPrintCOthers,A4ScanBW,A4ScanC, Duplex_One_Sided,Duplex_Two_Sided)
      select      ''Total'',isnull(sum(Total),0),isnull(sum(TotalBW),0),isnull(sum(TotalColor),0),isnull(Sum(A3PrintBW),0),isnull(sum(A3PrintC),0),isnull(sum(A3CopyBW),0),isnull(sum(A3CopyC),0),isnull(sum(A3ScanBW),0),isnull(sum(A3ScanC),0), isnull(Sum(A4PrintBW),0),isnull(Sum(A4PrintC),0),isnull(sum(OtherPrintBW),0),isnull(sum(OtherPrintC),0),isnull(sum (A4CopyBW),0),isnull(SUM(A4CopyC),0),isnull(SUM(OtherCopyBW),0),isnull(SUM(OtherCopyC),0),isnull(SUM(OtherScanBW),0),isnull(SUM(OtherScanC),0),isnull(SUM(A4FaxBW),0),isnull(SUM(A3FaxBW),0),isnull(SUM(OtherFaxBW),0),isnull(SUM(A4FaxC),0),isnull(SUM(A3FaxC),0),isnull(SUM(OtherFaxC),0),isnull(SUM(DocFilingPrintA3C),0),isnull(SUM(DocFilingScanA3C),0),isnull(SUM(DocFilingPrintBW),0),isnull(SUM(DocFilingPrintC),0),isnull(SUM(DocFilingPrintA3BW),0),isnull(SUM(DocFilingScanBW),0),isnull(SUM(DocFilingScanC),0),isnull(SUM(DocFilingScanA3BW),0),isnull(SUM(DocFilingScanBWOthers),0),isnull(SUM(DocFilingScanCOthers),0),isnull(SUM(DocFilingPrintBWOthers),0),isnull(SUM(DocFilingPrintCOthers),0),isnull(SUM(A4ScanBW),0),isnull(SUM(A4ScanC),0), isnull(sum(Duplex_One_Sided),0),isnull(Sum(Duplex_Two_Sided),0) from #JobReport
      --select CostCenter as CostCenter,Total,TotalBW,TotalColor,A3PrintBW,A4PrintBW,A3PrintC,A4PrintC,OtherPrintBW,OtherPRINTC,Duplex_One_Sided,Duplex_Two_Sided from #JobReport
      Select * from #JobReport
end

if @sqlGroupOn = ''JOB_COMPUTER''
begin
      alter table #JobReport add ComputerName nvarchar(100) default ''''
      update #JobReport set ComputerName = #joblog.JOB_COMPUTER from  #joblog where #JobReport.ReportOn = #joblog.JOB_COMPUTER
      insert into #JobReport(ComputerName,Total,TotalBW,TotalColor,A3PrintBW,A3PrintC,A3CopyBW,A3CopyC,A3ScanBW,A3ScanC,A4PrintBW,A4PrintC,OtherPrintBW,OtherPrintC,A4CopyBW,A4CopyC,OtherCopyBW,OtherCopyC, OtherScanBW,OtherScanC,A4FaxBW,A3FaxBW,OtherFaxBW,A4FaxC,A3FaxC,OtherFaxC,DocFilingPrintA3C,DocFilingScanA3C,DocFilingPrintBW,DocFilingPrintC,DocFilingPrintA3BW,DocFilingScanBW,DocFilingScanC,DocFilingScanA3BW,DocFilingScanBWOthers,DocFilingScanCOthers,DocFilingPrintBWOthers,DocFilingPrintCOthers,A4ScanBW,A4ScanC, Duplex_One_Sided,Duplex_Two_Sided)
      select      ''Total'',isnull(sum(Total),0),isnull(sum(TotalBW),0),isnull(sum(TotalColor),0),isnull(Sum(A3PrintBW),0),isnull(sum(A3PrintC),0),isnull(sum(A3CopyBW),0),isnull(sum(A3CopyC),0),isnull(sum(A3ScanBW),0),isnull(sum(A3ScanC),0), isnull(Sum(A4PrintBW),0),isnull(Sum(A4PrintC),0),isnull(sum(OtherPrintBW),0),isnull(sum(OtherPrintC),0),isnull(sum (A4CopyBW),0),isnull(SUM(A4CopyC),0),isnull(SUM(OtherCopyBW),0),isnull(SUM(OtherCopyC),0),isnull(SUM(OtherScanBW),0),isnull(SUM(OtherScanC),0),isnull(SUM(A4FaxBW),0),isnull(SUM(A3FaxBW),0),isnull(SUM(OtherFaxBW),0),isnull(SUM(A4FaxC),0),isnull(SUM(A3FaxC),0),isnull(SUM(OtherFaxC),0),isnull(SUM(DocFilingPrintA3C),0),isnull(SUM(DocFilingScanA3C),0),isnull(SUM(DocFilingPrintBW),0),isnull(SUM(DocFilingPrintC),0),isnull(SUM(DocFilingPrintA3BW),0),isnull(SUM(DocFilingScanBW),0),isnull(SUM(DocFilingScanC),0),isnull(SUM(DocFilingScanA3BW),0),isnull(SUM(DocFilingScanBWOthers),0),isnull(SUM(DocFilingScanCOthers),0),isnull(SUM(DocFilingPrintBWOthers),0),isnull(SUM(DocFilingPrintCOthers),0),isnull(SUM(A4ScanBW),0),isnull(SUM(A4ScanC),0), isnull(sum(Duplex_One_Sided),0),isnull(Sum(Duplex_Two_Sided),0) from #JobReport
      --select ComputerName as ComputerName,Total,TotalBW,TotalColor,A3PrintBW,A4PrintBW,A3PrintC,A4PrintC,OtherPrintBW,OtherPrintC,Duplex_One_Sided,Duplex_Two_Sided from #JobReport
      Select * from #JobReport
end


' 
END
GO
/****** Object:  StoredProcedure [dbo].[ReportPrintCopies]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportPrintCopies]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ReportPrintCopies]
      @ReportOn varchar(50),
      @authenticationSource varchar(2),
      @fromDate varchar(10),
      @toDate  varchar(10),
      @JobStatus varchar(100)
      
      
as

select * into #joblog from T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) )  and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 

alter table #joblog add MFP_SERIALNUMBER nvarchar(50), MFP_MODEL nvarchar(50), MFP_LOCATION nvarchar(100)

create table #JobReport
(
      slno int identity,
      ReportOn nvarchar(100) default '''',Total int default 0, TotalBW int default 0, TotalColor int default 0
      ,Copy int default 0
      ,Prints int default 0
      ,Scan int default 0
      ,Duplex int default 0
	  ,Pricing float default 0.00
       
)
create table #ColorBW
(
      Total int default 0
      ,JobMode nvarchar (50)
 )

 create table #CopyPrint
(
      Total int default 0
      ,JobType nvarchar (50)
 )
declare @TotalValueBW int
declare @TotalValueColor int

declare @TotalValueCopy int
declare @TotalValuePrint int

declare @sqlQuery nvarchar(max)
declare @sqlGroupOn nvarchar(100)

--create table #reportGroup groupItem nvarchar(100)

--insert into #JobReport (ReportOn) select distinct JOB_COMPUTER from #joblog

set @sqlGroupOn = @ReportOn --''MFP_IP,GRUP_ID,JOB_COMPUTER,USR_ID,JOB_TYPE''

set @sqlQuery = ''insert into #JobReport (ReportOn) select distinct '' +  @sqlGroupOn + '' from #joblog''
exec(@sqlQuery)
create table #TempGroupCount(itemGroup nvarchar(100), itemCount int default 0)
print @sqlQuery



--Copy
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where  JOB_MODE = ''''COPY''''  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set Copy = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--Prints
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where  JOB_MODE = ''''PRINT'''' or JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set Prints = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--Scan
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where  JOB_MODE = ''''SCANNER'''' or JOB_MODE = ''''Doc Filing Scan'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set Scan = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

-- Duplex
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT/2) from #joblog where DUPLEX_MODE <> ''''1SIDED'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT) from #joblog where  DUPLEX_MODE <> ''1SIDED'' group by MFP_IP
update #JobReport set Duplex = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn 
truncate table #TempGroupCount


--TotalBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set TotalBW = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

--TotalColor
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set TotalColor = itemCount from #TempGroupCount where itemGroup = #JobReport.ReportOn
truncate table #TempGroupCount

update #JobReport set Total = TotalBW + TotalColor 

--Pricing
create table #TempGroupCountPrice(itemGroup nvarchar(100), itemCount float default 0.00)

set @sqlQuery = ''insert into #TempGroupCountPrice select '' + @sqlGroupOn + '', isnull(sum(cast(cast(JOB_PRICE_TOTAL AS float) as DECIMAL(20, 2))),0)   from #joblog  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReport set Pricing =  itemCount from #TempGroupCountPrice where itemGroup = #JobReport.ReportOn

truncate table #TempGroupCountPrice

/*
,UserName nvarchar(100) default ''''
      ,Department nvarchar(100) default ''''
      ,ComputerName nvarchar(100) default ''''
      ,LoginName nvarchar(100) default ''''
      ,ModelName nvarchar(100) default ''''
      ,SerialNumber nvarchar(100) default ''''
      ,AuthenticationSource char(2) default ''''
      ,CostCenter nvarchar(100) default ''''
      ,GroupID nvarchar(100) default ''''
*/


if @sqlGroupOn = ''USR_ID''
begin

      alter table #JobReport add UserName nvarchar(50) default ''''
      update #JobReport set  UserName = M_USERS.USR_NAME  from M_USERS where M_USERS.USR_ID = #JobReport.ReportOn 
	  update #JobReport set  UserName = #JobReport.ReportOn  where UserName is null
	  	  set @TotalValueBW = (select isnull(sum(TotalBW),0) from #JobReport)
	   set @TotalValueColor = (select isnull(sum(TotalColor),0) from #JobReport)
	     set @TotalValueCopy = (select isnull(sum(Copy),0) from #JobReport)
	   set @TotalValuePrint = (select isnull(sum(Prints),0) from #JobReport)
       SELECT DISTINCT TOP(10) Total , UserName FROM #JobReport ORDER BY Total DESC
	  insert into #JobReport(ReportOn,Total,TotalBW,TotalColor,Copy,Prints,Scan,Duplex,Pricing,UserName)
      
      select '''', isnull(sum(Total),0),isnull(sum(TotalBW),0),isnull(sum(TotalColor),0),isnull(Sum(Copy),0),isnull(sum(Prints),0),isnull(sum(Scan),0),isnull(sum(Duplex),0),isnull(sum(cast(cast(pricing AS float) as DECIMAL(28, 2))),0),''Total'' from #JobReport

      Select * from #JobReport

	   insert into #ColorBW (Total, JobMode) values (@TotalValueBW,''B/W'') 
	   insert into #ColorBW (Total, JobMode) values (@TotalValueColor,''Color'') 

	   insert into #CopyPrint (Total, JobType) values (@TotalValueCopy,''Copy'') 
	   insert into #CopyPrint (Total, JobType) values (@TotalValuePrint,''Print'') 

	   select * from #ColorBW
	   select * from #CopyPrint
	  
end

if @sqlGroupOn = ''MFP_IP''
begin
      alter table #JobReport add ModelName nvarchar(50) default '''', SerialNumber nvarchar(50) default '''', Location nvarchar(50) default '''',MFPHOSTNAME nvarchar(100) default '''',MFPIP nvarchar(50) default ''''                
      update #JobReport set MFPHOSTNAME = M_MFPS.MFP_HOST_NAME, ModelName = M_MFPS.MFP_MODEL,  SerialNumber = M_MFPS.MFP_SERIALNUMBER, Location = M_MFPS.MFP_LOCATION,  MFPIP = #JobReport.ReportOn  from M_MFPS where  M_MFPS.MFP_IP = #JobReport.ReportOn
	  set @TotalValueBW = (select isnull(sum(TotalBW),0) from #JobReport)
	   set @TotalValueColor = (select isnull(sum(TotalColor),0) from #JobReport)
	     set @TotalValueCopy = (select isnull(sum(Copy),0) from #JobReport)
	   set @TotalValuePrint = (select isnull(sum(Prints),0) from #JobReport)
	    SELECT DISTINCT TOP(10) Total , MFPHOSTNAME FROM #JobReport ORDER BY Total DESC
       insert into #JobReport(SerialNumber,Total,TotalBW,TotalColor,Copy,Prints,Scan,Duplex,Pricing,MFPHOSTNAME)
	  
       select '''', isnull(sum(Total),0),isnull(sum(TotalBW),0),isnull(sum(TotalColor),0),isnull(Sum(Copy),0),isnull(sum(Prints),0),isnull(sum(Scan),0),isnull(sum(Duplex),0),isnull(sum(cast(cast(pricing AS float) as DECIMAL(28, 2))),0),''Total'' from #JobReport
      
      Select * from #JobReport

	   insert into #ColorBW (Total, JobMode) values (@TotalValueBW,''B/W'') 
	   insert into #ColorBW (Total, JobMode) values (@TotalValueColor,''Color'') 
	    insert into #CopyPrint (Total, JobType) values (@TotalValueCopy,''Copy'') 
	   insert into #CopyPrint (Total, JobType) values (@TotalValuePrint,''Print'') 
	   select * from #ColorBW
	    select * from #CopyPrint
		
end

if @sqlGroupOn = ''GRUP_ID''
begin
--select * from #JobReport
      alter table #JobReport add CostCenter nvarchar(100) default ''''
      update #JobReport set CostCenter = #joblog.COST_CENTER_NAME from  #joblog where #JobReport.ReportOn = CAST(#joblog.GRUP_ID AS varchar(100)) 
	  set @TotalValueBW = (select isnull(sum(TotalBW),0) from #JobReport)
	   set @TotalValueColor = (select isnull(sum(TotalColor),0) from #JobReport)
	     set @TotalValueCopy = (select isnull(sum(Copy),0) from #JobReport)
	   set @TotalValuePrint = (select isnull(sum(Prints),0) from #JobReport)
	   SELECT DISTINCT TOP(10) Total , CostCenter FROM #JobReport ORDER BY Total DESC
     insert into #JobReport(CostCenter,Total,TotalBW,TotalColor,Copy,Prints,Scan,Duplex,Pricing)
	  
       select ''Total'',isnull(sum(Total),0),isnull(sum(TotalBW),0),isnull(sum(TotalColor),0),isnull(Sum(Copy),0),isnull(sum(Prints),0),isnull(sum(Scan),0),isnull(sum(Duplex),0),isnull(sum(cast(cast(pricing AS float) as DECIMAL(28, 2))),0) from #JobReport
      
      Select * from #JobReport

	   insert into #ColorBW (Total, JobMode) values (@TotalValueBW,''B/W'') 
	   insert into #ColorBW (Total, JobMode) values (@TotalValueColor,''Color'') 
	    insert into #CopyPrint (Total, JobType) values (@TotalValueCopy,''Copy'') 
	   insert into #CopyPrint (Total, JobType) values (@TotalValuePrint,''Print'') 

	   select * from #ColorBW
	    select * from #CopyPrint
		
end







' 
END
GO
/****** Object:  StoredProcedure [dbo].[ReportPrintCopiesCR]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportPrintCopiesCR]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ReportPrintCopiesCR]
      @ReportOn varchar(50),
      @authenticationSource varchar(2),
      @fromDate varchar(10),
      @toDate  varchar(10),
      @JobStatus varchar(100)
      
      
as
create table #JobReportGeneric
(
     DataColumn1 nvarchar(100) default ''-'', 
	 DataColumn2 nvarchar(100) default 0, 
	 DataColumn3 nvarchar(100) default 0, 
	 DataColumn4 nvarchar(100) default 0, 
	 DataColumn5 nvarchar(100) default 0, 
	 DataColumn6 nvarchar(100) default 0, 
	 DataColumn7 nvarchar(100) default 0, 
	 DataColumn8 nvarchar(100) default 0, 
	 DataColumn9 nvarchar(100) default 0, 
	 DataColumn10 nvarchar(100) default 0
)
select * into #joblog from T_JOB_LOG where (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) )  and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 

alter table #joblog add MFP_SERIALNUMBER nvarchar(50), MFP_MODEL nvarchar(50), MFP_LOCATION nvarchar(100)

 

declare @TotalValueBW int
declare @TotalValueColor int

declare @TotalValueCopy int
declare @TotalValuePrint int

declare @sqlQuery nvarchar(max)
declare @sqlGroupOn nvarchar(100)

--create table #reportGroup groupItem nvarchar(100)

--insert into #JobReportGeneric (ReportOn) select distinct JOB_COMPUTER from #joblog

set @sqlGroupOn = @ReportOn --''MFP_IP,GRUP_ID,JOB_COMPUTER,USR_ID,JOB_TYPE''

set @sqlQuery = ''insert into #JobReportGeneric (DataColumn1) select distinct '' +  @sqlGroupOn + '' from #joblog''
exec(@sqlQuery)
create table #TempGroupCount(itemGroup nvarchar(100), itemCount int default 0)
print @sqlQuery



--Copy
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where  JOB_MODE = ''''COPY''''  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReportGeneric set DataColumn2 = itemCount from #TempGroupCount where itemGroup = #JobReportGeneric.DataColumn1
truncate table #TempGroupCount

--Prints
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where  JOB_MODE = ''''PRINT'''' or JOB_MODE = ''''Doc Filing Print'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReportGeneric set DataColumn3 = itemCount from #TempGroupCount where itemGroup = #JobReportGeneric.DataColumn1
truncate table #TempGroupCount

--Scan
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where  JOB_MODE = ''''SCANNER'''' or JOB_MODE = ''''Doc Filing Scan'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReportGeneric set DataColumn4 = itemCount from #TempGroupCount where itemGroup = #JobReportGeneric.DataColumn1
truncate table #TempGroupCount

-- Duplex
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT) from #joblog where DUPLEX_MODE <> ''''1SIDED'''' group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
--insert into #TempGroupCount select MFP_IP, sum(JOB_SHEET_COUNT) from #joblog where  DUPLEX_MODE <> ''1SIDED'' group by MFP_IP
update #JobReportGeneric set DataColumn5 = itemCount from #TempGroupCount where itemGroup = #JobReportGeneric.DataColumn1
truncate table #TempGroupCount


--TotalBW
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_BW) from #joblog  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReportGeneric set DataColumn6 = itemCount from #TempGroupCount where itemGroup = #JobReportGeneric.DataColumn1
truncate table #TempGroupCount

--TotalColor
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_SHEET_COUNT_COLOR) from #joblog  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReportGeneric set DataColumn7 = itemCount from #TempGroupCount where itemGroup = #JobReportGeneric.DataColumn1
truncate table #TempGroupCount

update #JobReportGeneric set DataColumn8 = DataColumn6 + DataColumn7 

--Pricing
set @sqlQuery = ''insert into #TempGroupCount select '' + @sqlGroupOn + '', sum(JOB_PRICE_TOTAL) from #joblog  group by '' + @sqlGroupOn
exec(@sqlQuery)
print @sqlQuery
update #JobReportGeneric set DataColumn9 = itemCount from #TempGroupCount where itemGroup = #JobReportGeneric.DataColumn1
truncate table #TempGroupCount





if @sqlGroupOn = ''USR_ID''
begin

      alter table [#JobReportGeneric] add UserName nvarchar(50) default ''''
      update [#JobReportGeneric] set  UserName = M_USERS.USR_NAME  from M_USERS where M_USERS.USR_ID = [#JobReportGeneric].DataColumn1 
	  update [#JobReportGeneric] set  UserName = [#JobReportGeneric].DataColumn1  where DataColumn1 is null
	  Select * from [#JobReportGeneric]
    

	  
end

if @sqlGroupOn = ''MFP_IP''
begin
      alter table [#JobReportGeneric] add ModelName nvarchar(50) default '''', SerialNumber nvarchar(50) default '''', Location nvarchar(50) default '''',MFPHOSTNAME nvarchar(100) default '''',MFPIP nvarchar(50) default ''''                
      update [#JobReportGeneric] set ModelName = M_MFPS.MFP_MODEL,  SerialNumber = M_MFPS.MFP_SERIALNUMBER, Location = M_MFPS.MFP_LOCATION, MFPHOSTNAME = M_MFPS.MFP_HOST_NAME, MFPIP = #JobReportGeneric.DataColumn1  from M_MFPS where  M_MFPS.MFP_IP = #JobReportGeneric.DataColumn1
	  
	 Select * from [#JobReportGeneric]
     --  insert into #JobReportGeneric(SerialNumber,Total,TotalBW,TotalColor,Copy,Prints,Scan,Duplex,Pricing)
	  
     
	  
end

if @sqlGroupOn = ''GRUP_ID''
begin
--select * from #JobReportGeneric
      alter table [#JobReportGeneric] add CostCenter nvarchar(100) default ''''
      update [#JobReportGeneric] set CostCenter = #joblog.COST_CENTER_NAME from  #joblog where #JobReportGeneric.DataColumn1 = CAST(#joblog.GRUP_ID AS varchar(100)) 
	Select * from [#JobReportGeneric]
     --insert into #JobReportGeneric(CostCenter,Total,TotalBW,TotalColor,Copy,Prints,Scan,Duplex,Pricing)
	  
      


		
end





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SetAccessRightForSelfRegistration]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetAccessRightForSelfRegistration]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SetAccessRightForSelfRegistration] @UserSysID nvarchar(200),@UserSource varchar(5), @DeviceIPAddress Nvarchar(50)
AS
BEGIN
	
	declare @DeviceID int
	select @DeviceID = MFP_ID from M_MFPS where MFP_IP=@DeviceIPAddress
	if not exists (select REC_ID from T_ACCESS_RIGHTS where ASSIGN_ON=''MFP'' and ASSIGN_TO=''User'' and MFP_OR_GROUP_ID=@DeviceID and USER_OR_COSTCENTER_ID=@UserSysID and USR_SOURCE=@UserSource and REC_STATUS =''1'')
	begin
		insert into T_ACCESS_RIGHTS(ASSIGN_ON,ASSIGN_TO,MFP_OR_GROUP_ID,USER_OR_COSTCENTER_ID,USR_SOURCE)
		values(''MFP'', ''User'',@DeviceID,@UserSysID,@UserSource)
	end
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[TestingMfpIP]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestingMfpIP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[TestingMfpIP]
@ipaddress nvarchar(30)
as 

declare @serialNumber nvarchar(50)

select @serialNumber=MFP_SERIALNUMBER from M_MFPS where MFP_IP=@ipaddress



Select Distinct T1.REC_CDATE,T1.MAC_ADDRESS,T1.MODEL_NAME,T1.SERIAL_NUMBER,T1.PRINT_TOTAL,T1.PRINT_COLOR,T1.PRINT_BW,T1.DUPLEX,T1.COPIES,T1.COPY_BW,T1.COPY_COLOR,T1.TWO_COLOR_COPY_COUNT,T1.SINGLE_COLOR_COPY_COUNT,T1.BW_TOTAL_COUNT,T1.FULL_COLOR_TOTAL_COUNT,T1.TWO_COLOR_TOTAL_COUNT,T1.SINGLE_COLOR_TOTAL_COUNT,T1.BW_OTHER_COUNT,T1.FULL_COLOR_OTHER_COUNT,T1.SCAN_TO_HDD,T1.BW_SCAN_TO_HDD,T1.COLOR_SCAN_TO_HDD,T1.TWO_COLOR_SCAN_HDD,T1.TOTAL_DOC_FILING_PRINT,T1.BW_DOC_FILING_PRINT,T1.COLOR_DOC_FILING_PRINT,T1.TWO_COLOR_DOC_FILING_PRINT,T1.DOCUMENT_FEEDER,T1.FAX_SEND,T1.FAX_RECEIVE,T1.IFAX_SEND_COUNT,T1.TOTAL_SCAN_TO_EMAIL_FTP,T1.BW_SCAN,T1.COLOR_SCAN,T2.TRAY1,T2.TRAY2,T2.TRAY3,T2.TRAY4,T2.TRAY5,T2.TRAY6,T3.CYAN,T3.YELLOW,T3.MAGENTA,T3.BLACK From MFP_CLICK T1,MFP_PAPER T2,MFP_TONER T3 WHERE T1.SERIAL_NUMBER=T2.SERIAL_NUMBER and T2.SERIAL_NUMBER = T3.SERIAL_NUMBER and T1.SERIAL_NUMBER=@serialNumber order by REC_CDATE desc



' 
END
GO
/****** Object:  StoredProcedure [dbo].[testreport]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  StoredProcedure [dbo].[TST_BuildJobLog]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_BuildJobLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[TST_BuildJobLog] @totalRecords int

as 
SET NOCOUNT ON
 
declare @randomDate DateTime
declare @rowIndex int
set @rowIndex = @totalRecords

while @rowIndex > 0
	begin
print @randomDate
		select @randomDate = dateadd(month, -1 * abs(convert(varbinary, newid()) % (90 * 12)), getdate())
		insert into [T_JOB_LOG]
				   (
				    [MFP_IP]
				   ,[USR_ID]
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
				   ,[JOB_PRICE_TOTAL]
				   ,[JOB_STATUS]
				   ,[JOB_PAPER_SIZE]
				   ,[JOB_FILE_NAME]
				   ,[DUPLEX_MODE]
				   ,[REC_DATE])
			 VALUES
				   (
				   ''172.29.240.280''
				   ,''John''
				   ,''DB''
				   ,''JOB_ID''
				   ,''PRINT''
				   ,''''
				   ,''JOB_COMPUTER''
				   ,''JOB_USRNAME''
				   ,''Jun 24 2012  1:11PM''
				   ,''Jun 24 2012  1:11PM''
				   ,''MONOCHROME''
				   ,4
				   ,1
				   ,8
				   ,0
				   ,0
				   ,0
				   ,''FINISHED''
				   ,''A3''
				   ,''JOB_FILE_NAME''
				   ,''1SIDED''
				   ,@randomDate)
		set @rowIndex = @rowIndex - 1
	end
select COUNT(REC_SLNO) from T_JOB_LOG as TotalRecords
' 
END
GO
/****** Object:  StoredProcedure [dbo].[TST_BuildMFPGroups]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_BuildMFPGroups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[TST_BuildMFPGroups] @totalRecords int
as 

declare @rowIndex int
set @rowIndex = @totalRecords
declare @mfpGroupName varchar(100)


while @rowIndex > 0
	begin
		set @mfpGroupName = ''MFP Group_'' + cast(@rowIndex as nvarchar(20))
		
		insert into M_MFP_GROUPS(GRUP_NAME,REC_ACTIVE)
		select  @mfpGroupName, 1
		set @rowIndex = @rowIndex - 1
    end' 
END
GO
/****** Object:  StoredProcedure [dbo].[TST_BuildMFPs]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_BuildMFPs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[TST_BuildMFPs] @totalRecords int
as 

declare @rowIndex int
set @rowIndex = @totalRecords
declare @mfpIP varchar(100)
declare @mfpHostName varchar(100)
declare @MfpLocation varchar(100)

while @rowIndex > 0
	begin
		set @mfpIP = ''172.28.29.'' + cast(@rowIndex as nvarchar(20))
		set @mfpHostName = ''Host Name_'' + cast(@rowIndex as nvarchar(20))		
		set @MfpLocation = ''Location_'' + cast(@rowIndex as nvarchar(20))
		insert into M_MFPS(MFP_IP,MFP_HOST_NAME,MFP_LOCATION,REC_ACTIVE)
		select  @mfpIP, @mfpHostName, @MfpLocation, 1
		set @rowIndex = @rowIndex - 1
    end' 
END
GO
/****** Object:  StoredProcedure [dbo].[TST_BuildUsers]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_BuildUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[TST_BuildUsers] @userSource varchar(2), @totalRecords int
as 

declare @rowIndex int
set @rowIndex = @totalRecords
declare @userID varchar(100)
declare @userName varchar(100)

while @rowIndex > 0
	begin
		set @userID = ''User_'' + cast(@rowIndex as nvarchar(20))
		set @userName = ''User Name_'' + cast(@rowIndex as nvarchar(20))
		insert into M_USERS(USR_SOURCE,USR_DOMAIN,USR_ID,USR_NAME,REC_ACTIVE)
		select  @userSource, ''User Domain'', @userID, @userName, 1
		set @rowIndex = @rowIndex - 1
    end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[TST_CreateData]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_CreateData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create procedure [dbo].[TST_CreateData]
as

exec TST_BuildUsers ''DB'', 50000
exec TST_BuildMFPs 1000
exec TST_BuildMFPGroups 2000' 
END
GO
/****** Object:  StoredProcedure [dbo].[TST_ResetData]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TST_ResetData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[TST_ResetData]
as 

delete from M_USERS where USR_PASSWORD is null
delete from M_MFP_GROUPS where GRUP_NAME like ''MFP Group_%''
delete from M_MFPS where MFP_SERIALNUMBER is null' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateFileTransferDetails]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateFileTransferDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[UpdateFileTransferDetails] 
	-- Add the parameters for the stored procedure here
@RecSysId nvarchar (max)

AS
BEGIN

	SET NOCOUNT ON;
    update T_PRINT_JOBS set JOB_PRINT_RELEASED = 0 where REC_SYSID in (select * from ConvertStringListToTable(@RecSysId, '',''))

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateLimitsTosharedCostCenter]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateLimitsTosharedCostCenter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateLimitsTosharedCostCenter]
@addwithOldLimits BIT
AS
BEGIN
    DECLARE @ccCount AS INT;
    DECLARE @AutorefillCount AS INT;
    DECLARE @AutorefillACount AS INT;
    SET @ccCount = (SELECT count(*)
                    FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                    WHERE  GRUP_ID = ''-2'');
    PRINT @ccCount;
    IF @ccCount > 0
        BEGIN
            -- 1.Get the Shared Cost Centers List
            SELECT COSTCENTER_ID
            INTO   #CostCenters
            FROM   M_COST_CENTERS
            WHERE  IS_SHARED = ''1''
                   AND COSTCENTER_ID NOT IN (SELECT DISTINCT GRUP_ID
                                             FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                                             WHERE  PERMISSIONS_LIMITS_ON = 0);
            --select * from #CostCenters	
            -- 2. Get the AutoRefill Permissions and Limtis for Cost Center
            SELECT *
            INTO   #AutoRefillPLB
            FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
            WHERE  (GRUP_ID = ''-2'')
                   AND (CONVERT (VARCHAR (10), LAST_REFILLED_ON, 111) <> (SELECT CONVERT (VARCHAR (10), GETDATE(), 111))
                        OR LAST_REFILLED_ON IS NULL);
            --select * from #AutoRefillPL
            SET @AutorefillCount = (SELECT COUNT(*)
                                    FROM   #AutoRefillPLB);
            IF @AutorefillCount > 0
                BEGIN
                    -- Update Last refilled date
                    UPDATE  T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                        SET LAST_REFILLED_ON = (SELECT getdate())
                    WHERE   GRUP_ID IN (SELECT DISTINCT #AutoRefillPLB.GRUP_ID
                                        FROM   #AutoRefillPLB);
                    -- 3. Combine all records into Final Table
                    SELECT *
                    INTO   #FinalTable
                    FROM   #CostCenters CROSS JOIN #AutoRefillPLB;
                    UPDATE  #FinalTable
                        SET #FinalTable.GRUP_ID = ''-1'';
                    --select * from #FinalTable
                    -- 4. Get the Existing Limits if any set for the cost center
                    SELECT *
                    INTO   #ExistingLimits
                    FROM   T_JOB_PERMISSIONS_LIMITS
                    WHERE  COSTCENTER_ID IN (SELECT COSTCENTER_ID
                                             FROM   #CostCenters)
                           AND PERMISSIONS_LIMITS_ON = ''0'';
                    --select * from #ExistingLimits
                    -- 5. Update the Existing Cost Center Permissions and Limits
                    UPDATE  #ExistingLimits
                        SET #ExistingLimits.JOB_LIMIT          = (#ExistingLimits.JOB_LIMIT + #AutoRefillPLB.JOB_LIMIT),
                            #ExistingLimits.ALERT_LIMIT        = #AutoRefillPLB.ALERT_LIMIT,
                            #ExistingLimits.ALLOWED_OVER_DRAFT = #AutoRefillPLB.ALLOWED_OVER_DRAFT,
                            #ExistingLimits.JOB_ISALLOWED      = #AutoRefillPLB.JOB_ISALLOWED
                    FROM    #AutoRefillPLB, #ExistingLimits
                    WHERE   #AutoRefillPLB.PERMISSIONS_LIMITS_ON = ''0''
                            AND #ExistingLimits.JOB_TYPE = #AutoRefillPLB.JOB_TYPE;
                    --select * from #ExistingLimits
                    -- Update the Job Limit if it is Exceeded Int.Max
                    UPDATE  #ExistingLimits
                        SET #ExistingLimits.JOB_USED = 0;
                    -- Update the Job Limit if it is Exceeded Int.Max
                    UPDATE  #ExistingLimits
                        SET #ExistingLimits.JOB_LIMIT = 2147483647
                    WHERE   #ExistingLimits.JOB_LIMIT > 2147483647;
                    -- 6. Delete existing data from T_JOB_PERMISSIONS_LIMITS
                    DELETE T_JOB_PERMISSIONS_LIMITS
                    WHERE  COSTCENTER_ID IN (SELECT COSTCENTER_ID
                                             FROM   #CostCenters)
                           AND PERMISSIONS_LIMITS_ON = ''0'';
                    -- 7. Insert all data to T_JOB_PERMISSIONS_LIMITS
                    INSERT INTO T_JOB_PERMISSIONS_LIMITS (COSTCENTER_ID, USER_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
                    SELECT COSTCENTER_ID,
                           GRUP_ID,
                           PERMISSIONS_LIMITS_ON,
                           JOB_TYPE,
                           JOB_LIMIT,
                           JOB_USED,
                           ALERT_LIMIT,
                           ALLOWED_OVER_DRAFT,
                           JOB_ISALLOWED
                    FROM   #FinalTable;
                    -- 8. Re-update Job Used Count
                    IF @addwithOldLimits = ''1''
                        BEGIN
                            UPDATE  T_JOB_PERMISSIONS_LIMITS
                                SET JOB_LIMIT = #ExistingLimits.JOB_LIMIT,
                                    JOB_USED  = #ExistingLimits.JOB_USED
                            FROM    #ExistingLimits
                            WHERE   T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = #ExistingLimits.COSTCENTER_ID
                                    AND T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimits.USER_ID
                                    AND T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimits.JOB_TYPE;
                        END
                    ELSE
                        BEGIN
                            UPDATE  T_JOB_PERMISSIONS_LIMITS
                                SET JOB_USED = #ExistingLimits.JOB_USED
                            FROM    #ExistingLimits
                            WHERE   T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = #ExistingLimits.COSTCENTER_ID
                                    AND T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimits.USER_ID
                                    AND T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimits.JOB_TYPE;
                        END
                    -- 9. Drop all temp tables
                    DROP TABLE #CostCenters, #FinalTable, #ExistingLimits;
                END
            DROP TABLE #AutoRefillPLB;
        END
    -- 1.Get the Shared Cost Centers List
    SELECT COSTCENTER_ID
    INTO   #CostCentersA
    FROM   M_COST_CENTERS
    WHERE  IS_SHARED = ''1''
           AND COSTCENTER_ID IN (SELECT DISTINCT GRUP_ID
                                 FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                                 WHERE  PERMISSIONS_LIMITS_ON = 0);
    --select * from #CostCentersA	
    -- 2. Get the AutoRefill Permissions and Limtis for Cost Center
    SELECT *
    INTO   #AutoRefillPLAS
    FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
    WHERE  (GRUP_ID IN (SELECT COSTCENTER_ID
                        FROM   M_COST_CENTERS
                        WHERE  IS_SHARED = ''1''
                               AND COSTCENTER_ID IN (SELECT DISTINCT GRUP_ID
                                                     FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                                                     WHERE  PERMISSIONS_LIMITS_ON = 0)))
           AND (CONVERT (VARCHAR (10), LAST_REFILLED_ON, 111) <> (SELECT CONVERT (VARCHAR (10), GETDATE(), 111))
                OR LAST_REFILLED_ON IS NULL);
    --select * from #AutoRefillPLA
    SET @AutorefillACount = (SELECT COUNT(*)
                             FROM   #AutoRefillPLAS);
    IF @AutorefillACount > 0
        BEGIN
            -- Update Last refilled date
            UPDATE  T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                SET LAST_REFILLED_ON = (SELECT getdate())
            WHERE   GRUP_ID IN (SELECT DISTINCT #AutoRefillPLAS.GRUP_ID
                                FROM   #AutoRefillPLAS);
            -- 3. Combine all records into Final Table
            SELECT *
            INTO   #FinalTableA
            FROM   #CostCentersA
                   INNER JOIN
                   #AutoRefillPLAS
                   ON #CostCentersA.COSTCENTER_ID = #AutoRefillPLAS.GRUP_ID;
            UPDATE  #FinalTableA
                SET #FinalTableA.GRUP_ID = ''-1'';
            --select * from #FinalTableA
            -- 4. Get the Existing Limits if any set for the cost center
            SELECT *
            INTO   #ExistingLimitsA
            FROM   T_JOB_PERMISSIONS_LIMITS
            WHERE  COSTCENTER_ID IN (SELECT COSTCENTER_ID
                                     FROM   #CostCentersA)
                   AND PERMISSIONS_LIMITS_ON = ''0'';
            --select * from #ExistingLimitsA
            -- 5. Update the Existing Cost Center Permissions and Limits
            UPDATE  #ExistingLimitsA
                SET #ExistingLimitsA.JOB_LIMIT          = (#ExistingLimitsA.JOB_LIMIT + #AutoRefillPLAS.JOB_LIMIT),
                    #ExistingLimitsA.ALERT_LIMIT        = #AutoRefillPLAS.ALERT_LIMIT,
                    #ExistingLimitsA.ALLOWED_OVER_DRAFT = #AutoRefillPLAS.ALLOWED_OVER_DRAFT,
                    #ExistingLimitsA.JOB_ISALLOWED      = #AutoRefillPLAS.JOB_ISALLOWED
            FROM    #AutoRefillPLAS, #ExistingLimitsA
            WHERE   #AutoRefillPLAS.PERMISSIONS_LIMITS_ON = ''0''
                    AND #ExistingLimitsA.JOB_TYPE = #AutoRefillPLAS.JOB_TYPE
                    AND #ExistingLimitsA.COSTCENTER_ID = #AutoRefillPLAS.GRUP_ID;
            --select * from #ExistingLimits
            -- Update the Job Limit if it is Exceeded Int.Max
            UPDATE  #ExistingLimitsA
                SET #ExistingLimitsA.JOB_USED = 0;
            -- Update the Job Limit if it is Exceeded Int.Max
            UPDATE  #ExistingLimitsA
                SET #ExistingLimitsA.JOB_LIMIT = 2147483647
            WHERE   #ExistingLimitsA.JOB_LIMIT > 2147483647;
            -- 6. Delete existing data from T_JOB_PERMISSIONS_LIMITS
            DELETE T_JOB_PERMISSIONS_LIMITS
            WHERE  COSTCENTER_ID IN (SELECT DISTINCT COSTCENTER_ID
                                     FROM   #FinalTableA)
                   AND PERMISSIONS_LIMITS_ON = ''0'';
            -- 7. Insert all data to T_JOB_PERMISSIONS_LIMITS
            INSERT INTO T_JOB_PERMISSIONS_LIMITS (COSTCENTER_ID, USER_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
            SELECT COSTCENTER_ID,
                   GRUP_ID,
                   PERMISSIONS_LIMITS_ON,
                   JOB_TYPE,
                   JOB_LIMIT,
                   JOB_USED,
                   ALERT_LIMIT,
                   ALLOWED_OVER_DRAFT,
                   JOB_ISALLOWED
            FROM   #FinalTableA;
            -- 8. Re-update Job Used Count
            IF @addwithOldLimits = ''1''
                BEGIN
                    UPDATE  T_JOB_PERMISSIONS_LIMITS
                        SET JOB_LIMIT = #ExistingLimitsA.JOB_LIMIT,
                            JOB_USED  = #ExistingLimitsA.JOB_USED
                    FROM    #ExistingLimitsA
                    WHERE   T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = #ExistingLimitsA.COSTCENTER_ID
                            AND T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimitsA.USER_ID
                            AND T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimitsA.JOB_TYPE;
                END
            ELSE
                BEGIN
                    UPDATE  T_JOB_PERMISSIONS_LIMITS
                        SET JOB_USED = #ExistingLimitsA.JOB_USED
                    FROM    #ExistingLimitsA
                    WHERE   T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = #ExistingLimitsA.COSTCENTER_ID
                            AND T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimitsA.USER_ID
                            AND T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimitsA.JOB_TYPE;
                END
            -- 9. Drop all temp tables
            DROP TABLE #CostCentersA, #FinalTableA, #ExistingLimitsA;
        END
    DROP TABLE #AutoRefillPLAS;
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateLimitsToUnsharedCostCenter]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateLimitsToUnsharedCostCenter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateLimitsToUnsharedCostCenter]
@addwithOldLimits BIT
AS
BEGIN
    DECLARE @CostCenterID AS INT;
    DECLARE @CCCount AS INT;
    DECLARE @ALLCCcount AS INT;
    DECLARE @AutorefillCount AS INT;
    DECLARE @AutorefillACount AS INT;
    DECLARE @getCostCenterID AS CURSOR;
    SET @getCostCenterID = CURSOR
        FOR SELECT COSTCENTER_ID
            FROM   M_COST_CENTERS
            WHERE  IS_SHARED = ''0'';
    OPEN @getCostCenterID;
    FETCH NEXT FROM @getCostCenterID INTO @CostCenterID;
    WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT @CostCenterID;
            -- 1. Convert Cost Center in to Temp Table
            --	select TokenVal as TokanCostCenters into #CostCenters from ConvertStringListToTable(CAST(@CostCenterID as varchar(100)),'','')
            SET @ALLCCcount = (SELECT count(*)
                               FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                               WHERE  GRUP_ID = ''-2'');
            SET @CCCount = (SELECT count(*)
                            FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                            WHERE  GRUP_ID = @CostCenterID);
            PRINT @ALLCCcount;
            PRINT @CCCount;
            IF (@ALLCCcount > 0
                AND @CCCount = 0)
                BEGIN
                    -- 2. Get the AutoRefill Permissions and Limtis for Cost Center
                    SELECT *
                    INTO   #AutoRefillPL
                    FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                    WHERE  (GRUP_ID = ''-2'')
                           AND (CONVERT (VARCHAR (10), LAST_REFILLED_ON, 111) <> (SELECT CONVERT (VARCHAR (10), GETDATE(), 111))
                                OR LAST_REFILLED_ON IS NULL);
                    --select * from #AutoRefillPL
                    -- Update Last refilled date
                    --update T_JOB_PERMISSIONS_LIMITS_AUTOREFILL set LAST_REFILLED_ON = (select getdate()) where GRUP_ID = (select distinct  #AutoRefillPL.GRUP_ID from #AutoRefillPL); 
                    SET @AutorefillCount = (SELECT COUNT(*)
                                            FROM   #AutoRefillPL);
                    IF @AutorefillCount > 0
                        BEGIN
                            -- 3. Get the Users in the selected Cost Center
                            SELECT T_COSTCENTER_USERS.USR_ACCOUNT_ID
                            INTO   #CostCenterUsers
                            FROM   T_COSTCENTER_USERS
                            WHERE  T_COSTCENTER_USERS.COST_CENTER_ID = @CostCenterID;
                            INSERT  INTO #CostCenterUsers (USR_ACCOUNT_ID)
                            VALUES                       (''-1'');
                            --select * from #CostCenterUsers
                            -- 4. Combine all records into Final Table
                            SELECT *
                            INTO   #FinalTable
                            FROM   #CostCenterUsers CROSS JOIN #AutoRefillPL;
                            UPDATE  #FinalTable
                                SET GRUP_ID = @CostCenterID;
                            --select * from #FinalTable
                            -- 5. Get the Existing Limits if any set for the cost center
                            --	  Get it for the whole cost center
                            SELECT *
                            INTO   #ExistingLimits
                            FROM   T_JOB_PERMISSIONS_LIMITS
                            WHERE  COSTCENTER_ID = @CostCenterID
                                   AND PERMISSIONS_LIMITS_ON = ''0'';
                            SELECT *
                            FROM   #ExistingLimits;
                            -- 6. Update the Limits, Alert Limit, Over Draft and Permission for the existing records
                            UPDATE  #ExistingLimits
                                SET #ExistingLimits.JOB_LIMIT          = (#ExistingLimits.JOB_LIMIT + #AutoRefillPL.JOB_LIMIT),
                                    #ExistingLimits.ALERT_LIMIT        = #AutoRefillPL.ALERT_LIMIT,
                                    #ExistingLimits.ALLOWED_OVER_DRAFT = #AutoRefillPL.ALLOWED_OVER_DRAFT,
                                    #ExistingLimits.JOB_ISALLOWED      = #AutoRefillPL.JOB_ISALLOWED
                            FROM    #AutoRefillPL, #ExistingLimits
                            WHERE   #AutoRefillPL.PERMISSIONS_LIMITS_ON = ''0''
                                    AND #ExistingLimits.JOB_TYPE = #AutoRefillPL.JOB_TYPE;
                            SELECT *
                            FROM   #ExistingLimits;
                            -- Update Job_Used for 	#ExistingLimits
                            UPDATE  #ExistingLimits
                                SET #ExistingLimits.JOB_USED = 0;
                            -- Update the Job Limit if it is Exceeded Int.Max
                            UPDATE  #ExistingLimits
                                SET #ExistingLimits.JOB_LIMIT = 2147483647
                            WHERE   #ExistingLimits.JOB_LIMIT > 2147483647;
                            -- 7. Delete Old data from T_JOB_PERMISSIONS_LIMITS
                            DELETE T_JOB_PERMISSIONS_LIMITS
                            WHERE  COSTCENTER_ID = @CostCenterID
                                   AND PERMISSIONS_LIMITS_ON = ''0'';
                            -- 8. Insert all data to T_JOB_PERMISSIONS_LIMITS
                            INSERT INTO T_JOB_PERMISSIONS_LIMITS (COSTCENTER_ID, [USER_ID], PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
                            SELECT @CostCenterID,
                                   #FinalTable.USR_ACCOUNT_ID,
                                   PERMISSIONS_LIMITS_ON,
                                   JOB_TYPE,
                                   JOB_LIMIT,
                                   JOB_USED,
                                   ALERT_LIMIT,
                                   ALLOWED_OVER_DRAFT,
                                   JOB_ISALLOWED
                            FROM   #FinalTable;
                            -- 9. Re-update Job Used Count
                            IF @addwithOldLimits = ''1''
                                BEGIN
                                    -- Update with old limits
                                    UPDATE  T_JOB_PERMISSIONS_LIMITS
                                        SET JOB_LIMIT = #ExistingLimits.JOB_LIMIT,
                                            JOB_USED  = #ExistingLimits.JOB_USED
                                    FROM    #ExistingLimits
                                    WHERE   T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = @CostCenterID
                                            AND T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimits.USER_ID
                                            AND T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimits.JOB_TYPE;
                                END
                            ELSE
                                BEGIN
                                    -- Update only new limits
                                    UPDATE  T_JOB_PERMISSIONS_LIMITS
                                        SET JOB_USED = #ExistingLimits.JOB_USED
                                    FROM    #ExistingLimits
                                    WHERE   T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = @CostCenterID
                                            AND T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimits.USER_ID
                                            AND T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimits.JOB_TYPE;
                                END
                            -- 10. Drop all temp tables
                            DROP TABLE #FinalTable, #CostCenterUsers, #ExistingLimits;
                        END
                    DROP TABLE #AutoRefillPL;
                END
            ELSE
                BEGIN
                    PRINT @CostCenterID;
                   
                
                    -- 2. Get the AutoRefill Permissions and Limtis for Cost Center
					
                    SELECT *
                    INTO   #AutoRefillPLA
                    FROM   T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                    WHERE  (PERMISSIONS_LIMITS_ON = ''0'')
                           AND GRUP_ID = @CostCenterID
                           AND (CONVERT (VARCHAR (10), LAST_REFILLED_ON, 111) <> (SELECT CONVERT (VARCHAR (10), GETDATE(), 111))
                                OR LAST_REFILLED_ON IS NULL);
                    --select * from #AutoRefillPLA
                    SET @AutorefillACount = (SELECT COUNT(*)
                                             FROM   #AutoRefillPLA);
                    IF @AutorefillACount > 0
                        BEGIN
                            -- Update Last refilled date
                            UPDATE  T_JOB_PERMISSIONS_LIMITS_AUTOREFILL
                                SET LAST_REFILLED_ON = (SELECT getdate())
                            WHERE   GRUP_ID IN (SELECT DISTINCT #AutoRefillPLA.GRUP_ID
                                                FROM   #AutoRefillPLA);

							--Select TokenVal from costcenterID
							 SELECT TokenVal AS TokanCostCenters
							 INTO   #CostCentersA1
							 FROM   ConvertStringListToTable (CAST (@CostCenterID AS VARCHAR (100)), '','');
                            -- 3. Get the Users in the selected Cost Center
                            SELECT T_COSTCENTER_USERS.USR_ACCOUNT_ID
                            INTO   #CostCenterUsersA
                            FROM   T_COSTCENTER_USERS
                            WHERE  T_COSTCENTER_USERS.COST_CENTER_ID = @CostCenterID;
                            INSERT  INTO #CostCenterUsersA (USR_ACCOUNT_ID)
                            VALUES                        (''-1'');
                            --select * from #CostCenterUsers
                            -- 4. Combine all records into Final Table
                            SELECT *
                            INTO   #FinalTableA
                            FROM   #CostCenterUsersA, #CostCentersA1
                                   INNER JOIN
                                   #AutoRefillPLA
                                   ON #CostCentersA1.TokanCostCenters = #AutoRefillPLA.GRUP_ID;
                            --select * from #FinalTable
                            -- 5. Get the Existing Limits if any set for the cost center
                            --	  Get it for the whole cost center
							
                            SELECT *
                            INTO   #ExistingLimitsA
                            FROM   T_JOB_PERMISSIONS_LIMITS
                            WHERE  COSTCENTER_ID = @CostCenterID
                                   AND PERMISSIONS_LIMITS_ON = ''0'';
                            --select * from #ExistingLimits
                            -- 6. Update the Limits, Alert Limit, Over Draft and Permission for the existing records
                            UPDATE  #ExistingLimitsA
                                SET #ExistingLimitsA.JOB_LIMIT          = (#ExistingLimitsA.JOB_LIMIT + #AutoRefillPLA.JOB_LIMIT),
                                    #ExistingLimitsA.ALERT_LIMIT        = #AutoRefillPLA.ALERT_LIMIT,
                                    #ExistingLimitsA.ALLOWED_OVER_DRAFT = #AutoRefillPLA.ALLOWED_OVER_DRAFT,
                                    #ExistingLimitsA.JOB_ISALLOWED      = #AutoRefillPLA.JOB_ISALLOWED
                            FROM    #AutoRefillPLA, #ExistingLimitsA
                            WHERE   #AutoRefillPLA.PERMISSIONS_LIMITS_ON = ''0''
                                    AND #ExistingLimitsA.JOB_TYPE = #AutoRefillPLA.JOB_TYPE
                                    AND #ExistingLimitsA.COSTCENTER_ID = #AutoRefillPLA.GRUP_ID;
                            --select * from #ExistingLimits
                            -- Update Job_Used for 	#ExistingLimits
                            UPDATE  #ExistingLimitsA
                                SET #ExistingLimitsA.JOB_USED = 0;
                            -- Update the Job Limit if it is Exceeded Int.Max
                            UPDATE  #ExistingLimitsA
                                SET #ExistingLimitsA.JOB_LIMIT = 2147483647
                            WHERE   #ExistingLimitsA.JOB_LIMIT > 2147483647;
                            -- 7. Delete Old data from T_JOB_PERMISSIONS_LIMITS
                            DELETE T_JOB_PERMISSIONS_LIMITS
                            WHERE  COSTCENTER_ID = @CostCenterID
                                   AND PERMISSIONS_LIMITS_ON = ''0'';
                            -- 8. Insert all data to T_JOB_PERMISSIONS_LIMITS
                            INSERT INTO T_JOB_PERMISSIONS_LIMITS (COSTCENTER_ID, USER_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
                            SELECT @CostCenterID,
                                   USR_ACCOUNT_ID,
                                   PERMISSIONS_LIMITS_ON,
                                   JOB_TYPE,
                                   JOB_LIMIT,
                                   JOB_USED,
                                   ALERT_LIMIT,
                                   ALLOWED_OVER_DRAFT,
                                   JOB_ISALLOWED
                            FROM   #FinalTableA;
                            -- 9. Re-update Job Used Count
                            IF @addwithOldLimits = ''1''
                                BEGIN
                                    -- Update with old limits
                                    UPDATE  T_JOB_PERMISSIONS_LIMITS
                                        SET JOB_LIMIT = #ExistingLimitsA.JOB_LIMIT,
                                            JOB_USED  = #ExistingLimitsA.JOB_USED
                                    FROM    #ExistingLimitsA
                                    WHERE   T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = @CostCenterID
                                            AND T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimitsA.USER_ID
                                            AND T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimitsA.JOB_TYPE;
                                END
                            ELSE
                                BEGIN
                                    -- Update only new limits
                                    UPDATE  T_JOB_PERMISSIONS_LIMITS
                                        SET JOB_USED = #ExistingLimitsA.JOB_USED
                                    FROM    #ExistingLimitsA
                                    WHERE   T_JOB_PERMISSIONS_LIMITS.COSTCENTER_ID = @CostCenterID
                                            AND T_JOB_PERMISSIONS_LIMITS.USER_ID = #ExistingLimitsA.USER_ID
                                            AND T_JOB_PERMISSIONS_LIMITS.JOB_TYPE = #ExistingLimitsA.JOB_TYPE;
                                END
                            -- 10. Drop all temp tables
                            DROP TABLE #CostCentersA1,#FinalTableA, #CostCenterUsersA, #ExistingLimitsA;
                        END
                    DROP TABLE #AutoRefillPLA;
--					IF EXISTS (SELECT * FROM sys.tables WHERE name LIKE ''#CostCentersA1%'')
--					DROP TABLE #CostCentersA1
                END
            FETCH NEXT FROM @getCostCenterID INTO @CostCenterID;
        END
    CLOSE @getCostCenterID;
    DEALLOCATE @getCostCenterID;
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRetryCount]    Script Date: 26/11/2015 13:01:29 ******/
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

---Delete old date records
update M_USERS set  RETRY_DATE = null , RETRY_COUNT = 0 where RETRY_DATE < getdate()
update M_USERS set  RETRY_COUNT = 0 where RETRY_COUNT is null

if(@UserID <> ''admin'')
begin
	update M_USERS set RETRY_COUNT = RETRY_COUNT + 1 where USR_ID = @UserID and USR_SOURCE = @AuthType
		
	if @AllowedCount != -1
		begin
			declare @currentRetryCount int 

			select @currentRetryCount = RETRY_COUNT from M_USERS where USR_ID = @UserID and USR_SOURCE = @AuthType

			if @currentRetryCount >= @AllowedCount
			Begin
				update M_USERS set REC_ACTIVE = 0 where USR_ID=@userID and USR_SOURCE=@AuthType
				
			End
	end
end
select REC_ACTIVE from M_USERS where USR_ID=@userID and USR_SOURCE=@AuthType

' 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUsageLimits]    Script Date: 26/11/2015 13:01:29 ******/
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
	@jobMode varchar(30),
	@userSystemId int
 
AS
begin
	declare @UserGroupID int
	declare @RecordsCount int

	set @UserGroupID = @GroupID
	select @RecordsCount = count([USER_ID]) from T_JOB_PERMISSIONS_LIMITS where [USER_ID] = @GroupID and PERMISSIONS_LIMITS_ON=@LimitsOn
	print @RecordsCount
	if(@RecordsCount >= 1)
	begin
		set @UserGroupID = ''-1''
	end
	
    --select REC_SLNO from T_JOB_LOG where JOB_ID = @jobID and JOB_MODE = @jobMode and JOB_USED_UPDATED = ''0''
    
	if exists(select REC_SLNO from T_JOB_LOG where JOB_ID = @jobID and JOB_MODE = @jobMode)
		begin
			
			--print ''LOG Exists''
						
			if @LimitsOn = ''0'' -- Cost Center
				begin
					--print ''Update limits for cost Center''
					--update T_JOB_PERMISSIONS_LIMITS set JOB_USED = JOB_USED + @SheetCount where COSTCENTER_ID= @GroupID and [USER_ID]=''-1'' and JOB_TYPE = @JobType and PERMISSIONS_LIMITS_ON=@LimitsOn
					-- Check whether  Cost center  limits are shared or not
					declare @isLimitsAreShared bit
					
					select @isLimitsAreShared = IS_SHARED from M_COST_CENTERS where COSTCENTER_ID = @GroupID
					print @isLimitsAreShared
					if @isLimitsAreShared = 1
						begin
							--print ''Limits are Shared''
							--update T_JOB_PERMISSIONS_LIMITS set JOB_USED = JOB_USED + @SheetCount where COSTCENTER_ID= ''-1'' and [USER_ID]=@userSystemId and JOB_TYPE = @JobType and PERMISSIONS_LIMITS_ON=@LimitsOn
							update T_JOB_PERMISSIONS_LIMITS set JOB_USED = JOB_USED + @SheetCount where COSTCENTER_ID= @GroupID and JOB_TYPE = @JobType and PERMISSIONS_LIMITS_ON=@LimitsOn
						end
					else
						begin
							--print ''Limits are Not Shared''
							update T_JOB_PERMISSIONS_LIMITS set JOB_USED = JOB_USED + @SheetCount where COSTCENTER_ID= @GroupID and [USER_ID]=@userSystemId and JOB_TYPE = @JobType and PERMISSIONS_LIMITS_ON=@LimitsOn
						end
				end
			else			   -- User
				begin
					--print ''Update limits for cost Center''
					update T_JOB_PERMISSIONS_LIMITS set JOB_USED = JOB_USED + @SheetCount where COSTCENTER_ID= ''-1'' and [USER_ID]=@userSystemId and JOB_TYPE = @JobType and PERMISSIONS_LIMITS_ON=@LimitsOn
				end

				update T_JOB_LOG set JOB_USED_UPDATED = ''1'' where JOB_ID = @jobID and JOB_MODE = @jobMode
		end
end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_11_UpdatePermissionsAndLimts]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_11_UpdatePermissionsAndLimts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[UPGRADE_11_UpdatePermissionsAndLimts]
as

-- Update Table T_JOB_PERMISSIONS_LIMITS
if not EXISTS(select USER_ID from T_JOB_PERMISSIONS_LIMITS where USER_ID=''-1'' and COSTCENTER_ID=''1'' and PERMISSIONS_LIMITS_ON=''0'')
	begin
		UPDATE T_JOB_PERMISSIONS_LIMITS set USER_ID=''-1'' where COSTCENTER_ID =''1'' and PERMISSIONS_LIMITS_ON=''0''
	end

ALTER TABLE M_JOB_CATEGORIES ALTER column JOB_ID varchar (50) NOT NULL
ALTER TABLE M_JOB_TYPES ALTER column JOB_ID varchar (50) NOT NULL  

ALTER TABLE T_JOB_PERMISSIONS_LIMITS DROP CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS; 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS ALTER COLUMN JOB_TYPE varchar(50) NOT NULL; 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS ADD CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS PRIMARY KEY (GRUP_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE); 

ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL DROP CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL; 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL ALTER COLUMN JOB_TYPE varchar(50) NOT NULL; 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL ADD CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL PRIMARY KEY (GRUP_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE); 


ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP DROP CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP; 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP ALTER COLUMN JOB_TYPE varchar(50) NOT NULL; 
ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP ADD CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP PRIMARY KEY (GRUP_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE); 

UPDATE T_JOB_PERMISSIONS_LIMITS set JOB_TYPE=''Doc Filing Scan BW'' where JOB_TYPE=''Doc Filing BW''
UPDATE T_JOB_PERMISSIONS_LIMITS set JOB_TYPE=''Doc Filing Scan Color'' where JOB_TYPE=''Doc Filing Color''

UPDATE M_JOB_CATEGORIES set JOB_ID=''Doc Filing Scan'' where JOB_ID=''Doc-Filing''
UPDATE M_JOB_CATEGORIES set ITEM_ORDER=''6'' where JOB_ID=''Fax''

UPDATE M_JOB_TYPES set JOB_ID=''Doc Filing Scan BW'',ITEM_ORDER=''10'' where JOB_ID=''Doc Filing BW''
UPDATE M_JOB_TYPES set JOB_ID=''Doc Filing Scan Color'',ITEM_ORDER=''9'' where JOB_ID=''Doc Filing Color''

INSERT INTO M_JOB_TYPES(JOB_ID, ITEM_ORDER) values(''Doc Filing Print BW'',''8'')
INSERT INTO M_JOB_TYPES(JOB_ID, ITEM_ORDER) values(''Doc Filing Print Color'',''7'')
UPDATE M_JOB_TYPES set ITEM_ORDER=''11'' where JOB_ID=''Fax''
UPDATE M_JOB_TYPES set ITEM_ORDER=''12'' where JOB_ID=''Settings''

INSERT INTO M_JOB_CATEGORIES(JOB_ID, ITEM_ORDER) values(''Doc Filing Print'',''4'')


declare @rowCount int
select @rowCount = count(*) from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL

if(@rowCount > 0 )
begin
	
	UPDATE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL set JOB_TYPE=''Doc Filing Scan BW'' where JOB_TYPE=''Doc Filing BW''
	UPDATE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL set JOB_TYPE=''Doc Filing Scan Color'' where JOB_TYPE=''Doc Filing Color''


	insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL 
	(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
	values 
	(-1, 0, ''Doc Filing Print Color'', 0, 0, 0, 0, 0)

	insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL 
	(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
	values 
	(-1, 0, ''Doc Filing Print BW'', 0, 0, 0, 0, 0)

	insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL 
	(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
	values 
	(-1, 1, ''Doc Filing Print Color'', 0, 0, 0, 0, 0)

	insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL 
	(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
	values 
	(-1, 1, ''Doc Filing Print BW'', 0, 0, 0, 0, 0)

end

-- insert Permissions and Limits for Users and Cost centers
insert into T_JOB_PERMISSIONS_LIMITS 
([USER_ID], PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
values 
(1, 0, ''Doc Filing Print Color'', 50, 0, 0, 0, 1)

insert into T_JOB_PERMISSIONS_LIMITS 
([USER_ID], PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
values 
(1, 0, ''Doc Filing Print BW'', 100, 0, 0, 0, 1)

select distinct [USER_ID],PERMISSIONS_LIMITS_ON into #CostCenters from T_JOB_PERMISSIONS_LIMITS where [USER_ID] > 1 

insert into T_JOB_PERMISSIONS_LIMITS 
(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
select GRUP_ID, PERMISSIONS_LIMITS_ON , ''Doc Filing Print BW'', 0, 0, 0, 0, 0 from #CostCenters

insert into T_JOB_PERMISSIONS_LIMITS 
(GRUP_ID, PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED)
select GRUP_ID, PERMISSIONS_LIMITS_ON , ''Doc Filing Print Color'', 0, 0, 0, 0, 0 from #CostCenters
' 
END
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_12]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_12]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create procedure [dbo].[UPGRADE_12]
as 
select 1' 
END
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_DATA_440_VERSION]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_DATA_440_VERSION]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UPGRADE_DATA_440_VERSION]
AS
BEGIN
	
	DELETE FROM [dbo].[JOB_COMPLETED_STATUS]
	
	SET IDENTITY_INSERT [dbo].[JOB_COMPLETED_STATUS] ON
	INSERT [dbo].[JOB_COMPLETED_STATUS] ([REC_SYSID], [JOB_COMPLETED_TPYE], [REC_STATUS]) VALUES (1, N''Error'', 1)
	INSERT [dbo].[JOB_COMPLETED_STATUS] ([REC_SYSID], [JOB_COMPLETED_TPYE], [REC_STATUS]) VALUES (2, N''Finished'', 1)
	INSERT [dbo].[JOB_COMPLETED_STATUS] ([REC_SYSID], [JOB_COMPLETED_TPYE], [REC_STATUS]) VALUES (3, N''Cancelled'', 1)
	INSERT [dbo].[JOB_COMPLETED_STATUS] ([REC_SYSID], [JOB_COMPLETED_TPYE], [REC_STATUS]) VALUES (4, N''Suspended'', 1)
	SET IDENTITY_INSERT [dbo].[JOB_COMPLETED_STATUS] OFF

	SET IDENTITY_INSERT [dbo].[AD_SETTINGS] ON
	INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (7, N''IS_CARD_ENABLED'', N''False'', N''Is card field Enabled'')
	INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (8, N''CARD_FIELD'', NULL, N''Card field for Sync'')
	INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (9, N''IS_PIN_ENABLED'', N''False'', N''Is PIN field enabled'')
	INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (10, N''PIN_FIELD'', NULL, N''PIN Field for Sync'')
	INSERT [dbo].[AD_SETTINGS] ([SLNO], [AD_SETTING_KEY], [AD_SETTING_VALUE], [AD_SETTING_DESCRIPTION]) VALUES (11, N''AD_ALIAS'', NULL, N''Alias Name'')
	SET IDENTITY_INSERT [dbo].[AD_SETTINGS] OFF
	

	update M_COST_CENTERS set IS_SHARED = 0 where COSTCENTER_ID = 1
	
	update JOB_CONFIGURATION set JOBSETTING_VALUE=''Enable'' where JOBSETTING_KEY=''ANONYMOUS_PRINTING''
	
	IF NOT EXISTS(SELECT NULL FROM T_JOB_PERMISSIONS_LIMITS WHERE COSTCENTER_ID =''1'' and PERMISSIONS_LIMITS_ON=''0'' and COSTCENTER_ID =''1'')
	BEGIN
	  UPDATE  T_JOB_PERMISSIONS_LIMITS set USER_ID=''-1'' where COSTCENTER_ID =''1'' and PERMISSIONS_LIMITS_ON=''0''
	END
	
	Update M_MFPS set MFP_HOST_NAME = MFP_IP where MFP_HOST_NAME IS NULL
	Update M_MFPS set MFP_HOST_NAME = MFP_IP where MFP_HOST_NAME ='' ''
	
	update JOB_COMPLETED_STATUS set JOB_COMPLETED_TPYE=''Canceled'' where JOB_COMPLETED_TPYE=''Cancelled''
	
	SET IDENTITY_INSERT [dbo].[M_MFP_GROUPS] ON
	UPDATE M_MFP_GROUPS set REC_ACTIVE =''0'' where GRUP_ID=''1''
	SET IDENTITY_INSERT [dbo].[M_MFP_GROUPS] OFF

	update [AD_SETTINGS] set AD_DOMAIN_NAME=(select AD_DOMAIN_NAME from [AD_SETTINGS] where AD_SETTING_KEY=''DOMAIN_CONTROLLER'')
	
	truncate table T_ACCESS_RIGHTS
	
	/****** Object:  Table [dbo].[APP_SETTINGS]    Script Date: 06/06/2012 13:41:26 ******/
	SET IDENTITY_INSERT [dbo].[APP_SETTINGS] ON
	INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE], [REC_ACTIVE]) VALUES (15, N''GeneralSettings'', N''Time out'', N''TIME_OUT'', N''Application Time Out'', 14, N''15'', N''int'', NULL, NULL, N''15'', N''DROPDOWN'', 1)
	INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE], [REC_ACTIVE]) VALUES (19, N''GeneralSettings'', N''Enable Auto Login'', N''ENABLE_AUTO_LOGIN'', N''Enable automatic login'', 15, N''Disable'', N''string'', N''Enable,Disable'', N''Enable,Disable'', N''Disable'', N''DROPDOWN'', 1)
	INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE], [REC_ACTIVE]) VALUES (21, N''GeneralSettings'', N''Auto Login Time Out'', N''AUTO_LOGIN_TIME_OUT'', N''Time out value for auto Login'', 16, N''1'', N''int'', NULL, NULL, N''1'', N''DROPDOWN'', 1)
	SET IDENTITY_INSERT [dbo].[APP_SETTINGS] OFF
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_DATA_519_VERSION]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_DATA_519_VERSION]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UPGRADE_DATA_519_VERSION]
AS
BEGIN
	/****** Object:  Table [dbo].[APP_SETTINGS]    Script Date: 06/06/2012 13:41:26 ******/
	SET IDENTITY_INSERT [dbo].[APP_SETTINGS] ON
	INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE], [REC_ACTIVE]) VALUES (15, N''GeneralSettings'', N''Time out'', N''TIME_OUT'', N''Application Time Out'', 14, N''15'', N''int'', NULL, NULL, N''15'', N''DROPDOWN'', 1)
	INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE], [REC_ACTIVE]) VALUES (19, N''GeneralSettings'', N''Enable Auto Login'', N''ENABLE_AUTO_LOGIN'', N''Enable automatic login'', 15, N''Disable'', N''string'', N''Enable,Disable'', N''Enable,Disable'', N''Disable'', N''DROPDOWN'', 1)
	INSERT [dbo].[APP_SETTINGS] ([REC_SLNO], [APPSETNG_CATEGORY], [APPSETNG_KEY], [APPSETNG_RESX_ID], [APPSETNG_KEY_DESC], [APPSETNG_KEY_ORDER], [APPSETNG_VALUE], [APPSETNG_VALUE_TYPE], [ADS_LIST], [ADS_LIST_VALUES], [ADS_DEF_VALUE], [CONTROL_TYPE], [REC_ACTIVE]) VALUES (21, N''GeneralSettings'', N''Auto Login Time Out'', N''AUTO_LOGIN_TIME_OUT'', N''Time out value for auto Login'', 16, N''1'', N''int'', NULL, NULL, N''1'', N''DROPDOWN'', 1)
	SET IDENTITY_INSERT [dbo].[APP_SETTINGS] OFF
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_TABLE_440_VERSION]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_TABLE_440_VERSION]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UPGRADE_TABLE_440_VERSION]
AS
BEGIN
declare @SQL nvarchar(2000)
		/****** Object:  Table [dbo].[JOB_COMPLETED_STATUS]    Script Date: 04/20/2012 15:35:46 ******/
		IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[JOB_COMPLETED_STATUS]'') AND type in (N''U''))
		DROP TABLE [dbo].[JOB_COMPLETED_STATUS]
		/****** Object:  Table [dbo].[TEMP_CARD_MAPPING]    Script Date: 04/20/2012 15:36:10 ******/
		IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[TEMP_CARD_MAPPING]'') AND type in (N''U''))
		DROP TABLE [dbo].[TEMP_CARD_MAPPING]
		/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]    Script Date: 04/26/2012 17:37:49 ******/
		IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'') AND type in (N''U''))
		DROP TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]
		/****** Object:  Table [dbo].[JOB_COMPLETED_STATUS]    Script Date: 04/20/2012 15:35:32 ******/
		SET ANSI_NULLS ON
		
		SET QUOTED_IDENTIFIER ON
		
		CREATE TABLE [dbo].[JOB_COMPLETED_STATUS](
			[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
			[JOB_COMPLETED_TPYE] [nvarchar](50) NULL,
			[REC_STATUS] [bit] NULL CONSTRAINT [DF_JOB_COMPLETED_STATUS_REC_STATUS]  DEFAULT ((0))
		) ON [PRIMARY]

		/****** Object:  Table [dbo].[TEMP_CARD_MAPPING]    Script Date: 04/20/2012 15:36:33 ******/
		SET ANSI_NULLS ON
		
		SET QUOTED_IDENTIFIER ON
		
		SET ANSI_PADDING ON
		
		CREATE TABLE [dbo].[TEMP_CARD_MAPPING](
			[SESSION_ID] [nvarchar](100) NULL,
			[USR_SOURCE] [varchar](2) NULL,
			[USR_ID] [nvarchar](100) NULL,
			[USR_CARD_ID] [nvarchar](1000) NULL,
			[USR_PIN] [nvarchar](1000) NULL
		) ON [PRIMARY]

		SET ANSI_PADDING OFF
		/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]    Script Date: 04/26/2012 17:38:37 ******/
		SET ANSI_NULLS ON
		
		SET QUOTED_IDENTIFIER ON
		
		SET ANSI_PADDING ON
		
		CREATE TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP](
			[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
			[JOB_TYPE] [varchar](30) NOT NULL,
			[SESSION_ID] [nvarchar](150) NULL,
			[JOB_LIMIT] [bigint] NULL CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]  DEFAULT ((0)),
			[JOB_USED] [bigint] NULL CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]  DEFAULT ((0)),
			[ALERT_LIMIT] [int] NULL CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]  DEFAULT ((0)),
			[ALLOWED_OVER_DRAFT] [int] NULL CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]  DEFAULT ((0)),
			[JOB_ISALLOWED] [bit] NOT NULL CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]  DEFAULT ((0)),
			[C_DATE] [datetime] NULL
		) ON [PRIMARY]

		
		SET ANSI_PADDING OFF

		/****** Object:  Table [dbo].[APP_VERSION]    Script Date: 05/03/2012 11:29:38 ******/
		SET ANSI_NULLS ON
		
		SET QUOTED_IDENTIFIER ON
		
		SET ANSI_PADDING ON
		
		CREATE TABLE [dbo].[APP_VERSION](
			[VERSION] [varchar](20) NULL
		) ON [PRIMARY]

		
		SET ANSI_PADDING OFF

		ALTER TABLE M_COST_CENTERS ADD COSTCENTER_SOURCE varchar (2) NULL 
		ALTER TABLE M_MFPS  ADD MFP_HOST_NAME varchar (200) NULL 
		ALTER TABLE AD_SETTINGS  ADD AD_DOMAIN_NAME varchar (100) NULL 
		ALTER TABLE M_COST_CENTERS  ADD IS_SHARED bit NULL 
		ALTER TABLE T_COSTCENTER_USERS  ADD REC_ID NUMERIC(18,0) IDENTITY
		ALTER TABLE T_COSTCENTER_USERS  ADD USR_ACCOUNT_ID  int NULL 
		ALTER TABLE T_JOB_LOG  ADD USER_ACCOUNT_ID  int NULL 
		ALTER TABLE T_JOB_LOG  ADD DOMAIN_NAME  varchar (100) NULL 

		DECLARE @SQLQUERY NVARCHAR(200)
		DECLARE @PKVALUE NVARCHAR(50)
		SELECT @PKVALUE=NAME FROM SYSOBJECTS WHERE XTYPE = ''PK'' AND PARENT_OBJ = OBJECT_ID(''T_JOB_PERMISSIONS_LIMITS'');
		SET @SQLQUERY=''ALTER TABLE T_JOB_PERMISSIONS_LIMITS DROP CONSTRAINT ''+@PKVALUE+''''
		EXEC(@SQLQUERY)
		  --ALTER TABLE T_JOB_PERMISSIONS_LIMITS DROP CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS
		ALTER TABLE T_JOB_PERMISSIONS_LIMITS  ADD COSTCENTER_ID INT NOT NULL  DEFAULT ((1))

		ALTER TABLE T_JOB_PERMISSIONS_LIMITS 
		ADD CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS PRIMARY KEY (COSTCENTER_ID,GRUP_ID,PERMISSIONS_LIMITS_ON,JOB_TYPE) 


		DECLARE @SQLQUERY1 NVARCHAR(200)
		DECLARE @PKVALUE1 NVARCHAR(50)
		SELECT @PKVALUE1=NAME FROM SYSOBJECTS WHERE XTYPE = ''PK'' AND PARENT_OBJ = OBJECT_ID(''T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP'');
		SET @SQLQUERY1=''ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP DROP CONSTRAINT ''+@PKVALUE1+''''
		EXEC(@SQLQUERY1)
		  --ALTER TABLE T_JOB_PERMISSIONS_LIMITS DROP CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS
		ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP ALTER COLUMN JOB_TYPE varchar(30) not null

		ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP  ADD COSTCENTER_ID INT NOT NULL  DEFAULT ((1)) 
		ALTER TABLE T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP ADD CONSTRAINT PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP PRIMARY KEY (COSTCENTER_ID,GRUP_ID,PERMISSIONS_LIMITS_ON,JOB_TYPE) 

		set @SQL=''sp_RENAME ''''T_JOB_PERMISSIONS_LIMITS.GRUP_ID'''' , ''''USER_ID'''', ''''COLUMN''''''
		exec @SQL
		set @SQL=''sp_RENAME ''''T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP.GRUP_ID'''' , ''''USER_ID'''', ''''COLUMN''''''
		exec @SQL	

		ALTER TABLE T_JOB_LOG   ADD SERVER_IP varchar (50) NULL 
		ALTER TABLE T_JOB_LOG   ADD SERVER_LOCATION nvarchar (100) NULL 
		ALTER TABLE T_JOB_LOG   ADD SERVER_TOKEN_ID int NULL 

		ALTER TABLE T_AUDIT_LOG   ADD SERVER_IP varchar (50) NULL 
		ALTER TABLE T_AUDIT_LOG   ADD SERVER_LOCATION nvarchar (100) NULL 
		ALTER TABLE T_AUDIT_LOG   ADD SERVER_TOKEN_ID int NULL 
	
		
		CREATE TABLE [dbo].[T_AD_SYNC](
			[REC_SYS_ID] [int] IDENTITY(1,1) NOT NULL,
			[AD_SYNC_STATUS] [bit] NULL,
			[AD_SYNC_ON] [nvarchar](50) NULL,
			[AD_SYNC_VALUE] [nvarchar](50) NULL,
			[AD_LAST_SYNCED_ON] [datetime] NULL,
			[AD_IS_SYNC_REQUIRED] [bit] NULL
		) 
		

		ALTER TABLE [dbo].[T_AD_USERS]
		ALTER COLUMN [SESSION_ID] NVARCHAR(100) NULL
		
		ALTER TABLE [dbo].[AD_SETTINGS]
		ALTER COLUMN [AD_SETTING_VALUE] NVARCHAR(1000) NULL
		

		ALTER TABLE T_PRINT_JOBS ADD USER_DOMAIN nvarchar(200) NULL
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[UPGRADE_TABLE_519_VERSION]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPGRADE_TABLE_519_VERSION]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UPGRADE_TABLE_519_VERSION]
AS
BEGIN
	declare @SQL nvarchar(2000)
	/****** Object:  Table [dbo].[T_AD_SYNC]    Script Date: 06/06/2012 13:47:49 ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	
	CREATE TABLE [dbo].[T_AD_SYNC](
		[REC_SYS_ID] [int] IDENTITY(1,1) NOT NULL,
		[AD_SYNC_STATUS] [bit] NULL,
		[AD_SYNC_ON] [nvarchar](50) NULL,
		[AD_SYNC_VALUE] [nvarchar](50) NULL,
		[AD_LAST_SYNCED_ON] [datetime] NULL,
		[AD_IS_SYNC_REQUIRED] [bit] NULL
	) ON [PRIMARY]

	ALTER TABLE [dbo].[T_AD_USERS]
	ALTER COLUMN [SESSION_ID] NVARCHAR(100) NULL
	
	ALTER TABLE [dbo].[AD_SETTINGS]
	ALTER COLUMN [AD_SETTING_VALUE] NVARCHAR(1000) NULL

	ALTER TABLE T_PRINT_JOBS ADD USER_DOMAIN nvarchar(200) NULL
	
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[UserQuickJobTypeSummary]    Script Date: 26/11/2015 13:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserQuickJobTypeSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[UserQuickJobTypeSummary](
	@UserID varchar(50),
	@fromDate varchar(10),
	@toDate  varchar(10),
	@JobStatus varchar(100)
)

AS

create table #UserQuickJobType
(	
	JobType nvarchar(50),
	Color int default 0,
	BW int default 0,
	Total int default 0
)

insert into #UserQuickJobType select ''Print'', isnull (sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG WHERE USR_ID = @UserID and (JOB_MODE in (select JOB_MODE from T_JOB_LOG WHERE JOB_MODE in (''PRINT'',''Doc Filing Print'')))and (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) ) and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 
insert into #UserQuickJobType select ''Copy'', isnull (sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG WHERE USR_ID = @UserID and (JOB_MODE in (select JOB_MODE from T_JOB_LOG WHERE JOB_MODE = ''COPY''))and (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) ) and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 
insert into #UserQuickJobType select ''Scan'', isnull (sum(JOB_SHEET_COUNT_COLOR),0) as Color,isnull (sum(JOB_SHEET_COUNT_BW),0) as BW,isnull (sum(JOB_SHEET_COUNT),0) as Total from T_JOB_LOG WHERE USR_ID = @UserID and (JOB_MODE in (select JOB_MODE from T_JOB_LOG WHERE JOB_MODE in (''SCANNER'',''Doc Filing Scan'')))and (JOB_STATUS in (select TokenVal from ConvertStringListToTable(@JobStatus, '''')) ) and REC_DATE BETWEEN @fromDate + '' 00:00:00'' and  @toDate + '' 23:59:59'' 

select * from #UserQuickJobType
drop table #UserQuickJobType' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[ConvertStringListToTable]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetDatesforAday]    Script Date: 26/11/2015 13:01:29 ******/
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
/****** Object:  UserDefinedFunction [dbo].[udf_NumXWeekDaysinMonth]    Script Date: 26/11/2015 13:01:29 ******/
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

/****** Object:  StoredProcedure [dbo].[UpdateColumnMapping]    Script Date: 1/21/2016 4:15:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateColumnMapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateColumnMapping]
GO
/****** Object:  StoredProcedure [dbo].[UpdateColumnMapping]    Script Date: 1/21/2016 4:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateColumnMapping]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Varadharaj.G.R
-- Create date: 1/21/2016
-- Description:	Update Column Mapping 
-- =============================================
CREATE PROCEDURE [dbo].[UpdateColumnMapping]
	@userPinSelected bit,
	@userCardSelected bit,
	@domainName nvarchar(50),
	@cardField nvarchar(100),
	@pinField nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- UPDATE CARD Filed in AD settings
	if @userCardSelected = ''0''
	  begin
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=''False'' where AD_SETTING_KEY=''IS_CARD_ENABLED'' and AD_DOMAIN_NAME= @domainName
	  end
	else
	  begin
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=''True'' where AD_SETTING_KEY=''IS_CARD_ENABLED'' and AD_DOMAIN_NAME= @domainName
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=@cardField where AD_SETTING_KEY=''CARD_FIELD'' and AD_DOMAIN_NAME= @domainName
	  end

-- UPDATE PIN FIELD
	if @userPinSelected = ''0''
	  begin
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=''False'' where AD_SETTING_KEY=''IS_PIN_ENABLED'' and AD_DOMAIN_NAME= @domainName
	  end
	else
	  begin
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=''True'' where AD_SETTING_KEY=''IS_PIN_ENABLED'' and AD_DOMAIN_NAME= @domainName
		UPDATE AD_SETTINGS set AD_SETTING_VALUE=@pinField where AD_SETTING_KEY=''PIN_FIELD'' and AD_DOMAIN_NAME= @domainName
	  end
END
' 
END
GO


/****** Object:  StoredProcedure [dbo].[ExternalUserPermissionLimits]    Script Date: 2/19/2016 2:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExternalUserPermissionLimits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[ExternalUserPermissionLimits] 
@UserID nvarchar (200)

AS
BEGIN

declare @userAccountID int 

set @userAccountID = (select USR_ACCOUNT_ID from M_USERS where USR_ID = @UserID and External_source = ''email'')

select * into #DefaultPermissionsAndLimits from EMAIL_PERMISSION_LIMITS 


insert into T_JOB_PERMISSIONS_LIMITS(COSTCENTER_ID,USER_ID,PERMISSIONS_LIMITS_ON,JOB_TYPE,JOB_LIMIT,JOB_USED,ALERT_LIMIT,ALLOWED_OVER_DRAFT,JOB_ISALLOWED) 
		select -1,@userAccountID,1,JOB_TYPE,JOB_LIMIT,''0'',ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED from #DefaultPermissionsAndLimits		
		drop table #DefaultPermissionsAndLimits

END
' 
END
GO