USE [AccountingPlusDB]
GO
/****** Object:  Table [dbo].[M_ROLES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_ROLES]') AND type in (N'U'))
DROP TABLE [dbo].[M_ROLES]
GO
/****** Object:  Table [dbo].[T_AD_USERS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]') AND type in (N'U'))
DROP TABLE [dbo].[T_AD_USERS]
GO
/****** Object:  Table [dbo].[T_AD_SYNC]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AD_SYNC]') AND type in (N'U'))
DROP TABLE [dbo].[T_AD_SYNC]
GO
/****** Object:  Table [dbo].[M_DEPARTMENTS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]') AND type in (N'U'))
DROP TABLE [dbo].[M_DEPARTMENTS]
GO
/****** Object:  Table [dbo].[T_MFP_ACCESS_RIGHTS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_MFP_ACCESS_RIGHTS]') AND type in (N'U'))
DROP TABLE [dbo].[T_MFP_ACCESS_RIGHTS]
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]
GO
/****** Object:  Table [dbo].[T_JOB_USAGE_PAPERSIZE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_USAGE_PAPERSIZE]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_USAGE_PAPERSIZE]
GO
/****** Object:  Table [dbo].[T_JOB_LOG]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_LOG]
GO
/****** Object:  Table [dbo].[T_JOB_DISPATCHER]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_DISPATCHER]
GO
/****** Object:  Table [dbo].[T_AUTOREFILL_LIMITS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]') AND type in (N'U'))
DROP TABLE [dbo].[T_AUTOREFILL_LIMITS]
GO
/****** Object:  Table [dbo].[SQL_EXECUTION]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SQL_EXECUTION]') AND type in (N'U'))
DROP TABLE [dbo].[SQL_EXECUTION]
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]
GO
/****** Object:  Table [dbo].[M_JOB_CATEGORIES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_JOB_CATEGORIES]') AND type in (N'U'))
DROP TABLE [dbo].[M_JOB_CATEGORIES]
GO
/****** Object:  Table [dbo].[T_DEVICE_FLEETS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]') AND type in (N'U'))
DROP TABLE [dbo].[T_DEVICE_FLEETS]
GO
/****** Object:  Table [dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]') AND type in (N'U'))
DROP TABLE [dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]
GO
/****** Object:  Table [dbo].[T_AUDIT_LOG]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUDIT_LOG]') AND type in (N'U'))
DROP TABLE [dbo].[T_AUDIT_LOG]
GO
/****** Object:  Table [dbo].[M_JOB_TYPES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_JOB_TYPES]') AND type in (N'U'))
DROP TABLE [dbo].[M_JOB_TYPES]
GO
/****** Object:  Table [dbo].[M_SRV_TOKENS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_SRV_TOKENS]') AND type in (N'U'))
DROP TABLE [dbo].[M_SRV_TOKENS]
GO
/****** Object:  Table [dbo].[APP_VERSION]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_VERSION]') AND type in (N'U'))
DROP TABLE [dbo].[APP_VERSION]
GO
/****** Object:  Table [dbo].[T_JOB_TRANSMITTER]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_TRANSMITTER]
GO
/****** Object:  Table [dbo].[APP_IMAGES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_IMAGES]') AND type in (N'U'))
DROP TABLE [dbo].[APP_IMAGES]
GO
/****** Object:  Table [dbo].[T_ACCESS_RIGHTS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ACCESS_RIGHTS]') AND type in (N'U'))
DROP TABLE [dbo].[T_ACCESS_RIGHTS]
GO
/****** Object:  Table [dbo].[M_COUNTRIES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]') AND type in (N'U'))
DROP TABLE [dbo].[M_COUNTRIES]
GO
/****** Object:  Table [dbo].[M_SMTP_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_SMTP_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[M_SMTP_SETTINGS]
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]
GO
/****** Object:  Table [dbo].[M_PRICE_PROFILES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PRICE_PROFILES]') AND type in (N'U'))
DROP TABLE [dbo].[M_PRICE_PROFILES]
GO
/****** Object:  Table [dbo].[T_JOB_TRACKER]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_TRACKER]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_TRACKER]
GO
/****** Object:  Table [dbo].[M_USER_GROUPS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_USER_GROUPS]') AND type in (N'U'))
DROP TABLE [dbo].[M_USER_GROUPS]
GO
/****** Object:  Table [dbo].[M_MFPS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MFPS]') AND type in (N'U'))
DROP TABLE [dbo].[M_MFPS]
GO
/****** Object:  Table [dbo].[TEMP_CARD_MAPPING]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_CARD_MAPPING]') AND type in (N'U'))
DROP TABLE [dbo].[TEMP_CARD_MAPPING]
GO
/****** Object:  Table [dbo].[JOB_COMPLETED_STATUS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JOB_COMPLETED_STATUS]') AND type in (N'U'))
DROP TABLE [dbo].[JOB_COMPLETED_STATUS]
GO
/****** Object:  Table [dbo].[M_COST_CENTERS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]') AND type in (N'U'))
DROP TABLE [dbo].[M_COST_CENTERS]
GO
/****** Object:  Table [dbo].[T_PROXY_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PROXY_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[T_PROXY_SETTINGS]
GO
/****** Object:  Table [dbo].[T_COSTCENTER_USERS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_COSTCENTER_USERS]') AND type in (N'U'))
DROP TABLE [dbo].[T_COSTCENTER_USERS]
GO
/****** Object:  Table [dbo].[RESX_SERVER_MESSAGES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_SERVER_MESSAGES]') AND type in (N'U'))
DROP TABLE [dbo].[RESX_SERVER_MESSAGES]
GO
/****** Object:  Table [dbo].[T_CURRENT_JOBS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENT_JOBS]') AND type in (N'U'))
DROP TABLE [dbo].[T_CURRENT_JOBS]
GO
/****** Object:  Table [dbo].[AccountingPlus_Users]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountingPlus_Users]') AND type in (N'U'))
DROP TABLE [dbo].[AccountingPlus_Users]
GO
/****** Object:  Table [dbo].[T_JOBS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOBS]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOBS]
GO
/****** Object:  Table [dbo].[JOB_CONFIGURATION]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JOB_CONFIGURATION]') AND type in (N'U'))
DROP TABLE [dbo].[JOB_CONFIGURATION]
GO
/****** Object:  Table [dbo].[RESX_LABELS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_LABELS]') AND type in (N'U'))
DROP TABLE [dbo].[RESX_LABELS]
GO
/****** Object:  Table [dbo].[M_USERS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_USERS]') AND type in (N'U'))
DROP TABLE [dbo].[M_USERS]
GO
/****** Object:  Table [dbo].[RESX_CLIENT_MESSAGES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_CLIENT_MESSAGES]') AND type in (N'U'))
DROP TABLE [dbo].[RESX_CLIENT_MESSAGES]
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]') AND type in (N'U'))
DROP TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS]
GO
/****** Object:  Table [dbo].[M_OSA_JOB_PROPERTIES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_OSA_JOB_PROPERTIES]') AND type in (N'U'))
DROP TABLE [dbo].[M_OSA_JOB_PROPERTIES]
GO
/****** Object:  Table [dbo].[CostCenterDefaultPL]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CostCenterDefaultPL]') AND type in (N'U'))
DROP TABLE [dbo].[CostCenterDefaultPL]
GO
/****** Object:  Table [dbo].[T_AUTO_REFILL]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]') AND type in (N'U'))
DROP TABLE [dbo].[T_AUTO_REFILL]
GO
/****** Object:  Table [dbo].[T_PRINT_JOB_WEB_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOB_WEB_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[T_PRINT_JOB_WEB_SETTINGS]
GO
/****** Object:  Table [dbo].[CARD_CONFIGURATION]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]') AND type in (N'U'))
DROP TABLE [dbo].[CARD_CONFIGURATION]
GO
/****** Object:  Table [dbo].[T_CURRENT_SESSIONS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENT_SESSIONS]') AND type in (N'U'))
DROP TABLE [dbo].[T_CURRENT_SESSIONS]
GO
/****** Object:  Table [dbo].[T_PRICES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRICES]') AND type in (N'U'))
DROP TABLE [dbo].[T_PRICES]
GO
/****** Object:  Table [dbo].[APP_LANGUAGES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]') AND type in (N'U'))
DROP TABLE [dbo].[APP_LANGUAGES]
GO
/****** Object:  Table [dbo].[M_PAPER_SIZES]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PAPER_SIZES]') AND type in (N'U'))
DROP TABLE [dbo].[M_PAPER_SIZES]
GO
/****** Object:  Table [dbo].[T_ASSIGN_MFP_USER_GROUPS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ASSIGN_MFP_USER_GROUPS]') AND type in (N'U'))
DROP TABLE [dbo].[T_ASSIGN_MFP_USER_GROUPS]
GO
/****** Object:  Table [dbo].[INVALID_CARD_CONFIGURATION]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[INVALID_CARD_CONFIGURATION]') AND type in (N'U'))
DROP TABLE [dbo].[INVALID_CARD_CONFIGURATION]
GO
/****** Object:  Table [dbo].[T_GROUP_MFPS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_GROUP_MFPS]') AND type in (N'U'))
DROP TABLE [dbo].[T_GROUP_MFPS]
GO
/****** Object:  Table [dbo].[T_SERVICE_MONITOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_SERVICE_MONITOR]') AND type in (N'U'))
DROP TABLE [dbo].[T_SERVICE_MONITOR]
GO
/****** Object:  Table [dbo].[APP_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[APP_SETTINGS]
GO
/****** Object:  Table [dbo].[T_PRINT_JOBS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]') AND type in (N'U'))
DROP TABLE [dbo].[T_PRINT_JOBS]
GO
/****** Object:  Table [dbo].[AD_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AD_SETTINGS]') AND type in (N'U'))
DROP TABLE [dbo].[AD_SETTINGS]
GO
/****** Object:  Table [dbo].[M_MFP_GROUPS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MFP_GROUPS]') AND type in (N'U'))
DROP TABLE [dbo].[M_MFP_GROUPS]
GO
/****** Object:  Default [DF_APP_LANGUAGES_APP_CULTURE_DIR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_LANGUAGES_APP_CULTURE_DIR]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_LANGUAGES_APP_CULTURE_DIR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_LANGUAGES] DROP CONSTRAINT [DF_APP_LANGUAGES_APP_CULTURE_DIR]
END


End
GO
/****** Object:  Default [DF_APP_LANGUAGES_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_LANGUAGES_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_LANGUAGES_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_LANGUAGES] DROP CONSTRAINT [DF_APP_LANGUAGES_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_APP_SETTINGS_APPSETNG_KEY_ORDER]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_SETTINGS_APPSETNG_KEY_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_SETTINGS_APPSETNG_KEY_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_SETTINGS] DROP CONSTRAINT [DF_APP_SETTINGS_APPSETNG_KEY_ORDER]
END


End
GO
/****** Object:  Default [DF__APP_SETTI__REC_A__2E06CDA9]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__APP_SETTI__REC_A__2E06CDA9]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__APP_SETTI__REC_A__2E06CDA9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_SETTINGS] DROP CONSTRAINT [DF__APP_SETTI__REC_A__2E06CDA9]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_DATA_ON]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_DATA_ON]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_DATA_ON]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_CARD_DATA_ON]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_POSITION_START]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_POSITION_START]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_POSITION_START]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_CARD_POSITION_START]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] DROP CONSTRAINT [DF_CARD_CONFIGURATION_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]') AND parent_object_id = OBJECT_ID(N'[dbo].[INVALID_CARD_CONFIGURATION]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[INVALID_CARD_CONFIGURATION] DROP CONSTRAINT [DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]
END


End
GO
/****** Object:  Default [DF_JOB_COMPLETED_STATUS_REC_STATUS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_JOB_COMPLETED_STATUS_REC_STATUS]') AND parent_object_id = OBJECT_ID(N'[dbo].[JOB_COMPLETED_STATUS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_JOB_COMPLETED_STATUS_REC_STATUS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[JOB_COMPLETED_STATUS] DROP CONSTRAINT [DF_JOB_COMPLETED_STATUS_REC_STATUS]
END


End
GO
/****** Object:  Default [DF_M_COST_CENTERS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COST_CENTERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COST_CENTERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COST_CENTERS] DROP CONSTRAINT [DF_M_COST_CENTERS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COST_CENTERS] DROP CONSTRAINT [DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_M_COUNTRIES_REC_ACVITE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COUNTRIES_REC_ACVITE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COUNTRIES_REC_ACVITE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COUNTRIES] DROP CONSTRAINT [DF_M_COUNTRIES_REC_ACVITE]
END


End
GO
/****** Object:  Default [DF_M_COUNTRIES_REC_USER]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COUNTRIES_REC_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COUNTRIES_REC_USER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COUNTRIES] DROP CONSTRAINT [DF_M_COUNTRIES_REC_USER]
END


End
GO
/****** Object:  Default [DF_M_DEPARTMENTS_DEPT_SOURCE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_DEPARTMENTS_DEPT_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_DEPARTMENTS_DEPT_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_DEPARTMENTS] DROP CONSTRAINT [DF_M_DEPARTMENTS_DEPT_SOURCE]
END


End
GO
/****** Object:  Default [DF_M_DEPARTMENTS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_DEPARTMENTS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_DEPARTMENTS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_DEPARTMENTS] DROP CONSTRAINT [DF_M_DEPARTMENTS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_JOB_TYPES_ITEM_ORDER]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TYPES_ITEM_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_JOB_CATEGORIES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TYPES_ITEM_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_JOB_CATEGORIES] DROP CONSTRAINT [DF_T_JOB_TYPES_ITEM_ORDER]
END


End
GO
/****** Object:  Default [DF_M_JOB_TYPES_ITEM_ORDER]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_JOB_TYPES_ITEM_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_JOB_TYPES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_JOB_TYPES_ITEM_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_JOB_TYPES] DROP CONSTRAINT [DF_M_JOB_TYPES_ITEM_ORDER]
END


End
GO
/****** Object:  Default [DF_M_MFP_GROUPS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFP_GROUPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFP_GROUPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFP_GROUPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFP_GROUPS] DROP CONSTRAINT [DF_M_MFP_GROUPS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_ALLOW_NETWORK_PASSWORD]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_ALLOW_NETWORK_PASSWORD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_SSO]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_SSO]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_SSO]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_SSO]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]
END


End
GO
/****** Object:  Default [DF_M_MFPS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_REC_DATE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_REC_DATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_REC_DATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_REC_DATE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_PRINT_API]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_PRINT_API]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_PRINT_API]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_PRINT_API]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_EAM_ENABLED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_EAM_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_EAM_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_EAM_ENABLED]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_ACM_ENABLED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_ACM_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_ACM_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] DROP CONSTRAINT [DF_M_MFPS_MFP_ACM_ENABLED]
END


End
GO
/****** Object:  Default [DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_OSA_JOB_PROPERTIES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_OSA_JOB_PROPERTIES] DROP CONSTRAINT [DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]
END


End
GO
/****** Object:  Default [DF_M_PAPER_SIZES_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_PAPER_SIZES_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_PAPER_SIZES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_PAPER_SIZES_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_PAPER_SIZES] DROP CONSTRAINT [DF_M_PAPER_SIZES_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_SOURCE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_USR_SOURCE]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_DEPARTMENT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_DEPARTMENT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_DEPARTMENT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_USR_DEPARTMENT]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_COSTCENTER]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_COSTCENTER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_COSTCENTER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_USR_COSTCENTER]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_ROLE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_ROLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_ROLE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_USR_ROLE]
END


End
GO
/****** Object:  Default [DF_M_USERS_RETRY_COUNT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_RETRY_COUNT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_RETRY_COUNT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_RETRY_COUNT]
END


End
GO
/****** Object:  Default [DF_M_USERS_REC_CDATE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_REC_CDATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_REC_CDATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_REC_CDATE]
END


End
GO
/****** Object:  Default [DF_M_USERS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_USERS_ALLOW_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_ALLOW_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_ALLOW_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF_M_USERS_ALLOW_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF__M_USERS__ISUSER___2FEF161B]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__M_USERS__ISUSER___2FEF161B]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__M_USERS__ISUSER___2FEF161B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] DROP CONSTRAINT [DF__M_USERS__ISUSER___2FEF161B]
END


End
GO
/****** Object:  Default [DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_CLIENT_MESSAGES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_CLIENT_MESSAGES] DROP CONSTRAINT [DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_RESX_LABELS_RESX_IS_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_LABELS_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_LABELS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_LABELS_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_LABELS] DROP CONSTRAINT [DF_RESX_LABELS_RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_RESX_SERVER_MESSAGES_RESX_IS_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_SERVER_MESSAGES_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_SERVER_MESSAGES]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_SERVER_MESSAGES_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_SERVER_MESSAGES] DROP CONSTRAINT [DF_RESX_SERVER_MESSAGES_RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_T_AD_USERS_DEPARTMENT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AD_USERS_DEPARTMENT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AD_USERS_DEPARTMENT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AD_USERS] DROP CONSTRAINT [DF_T_AD_USERS_DEPARTMENT]
END


End
GO
/****** Object:  Default [DF_T_AD_USERS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AD_USERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AD_USERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AD_USERS] DROP CONSTRAINT [DF_T_AD_USERS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_AUTO_REFILL_AUTO_REFILL_FOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTO_REFILL_AUTO_REFILL_FOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTO_REFILL_AUTO_REFILL_FOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTO_REFILL] DROP CONSTRAINT [DF_T_AUTO_REFILL_AUTO_REFILL_FOR]
END


End
GO
/****** Object:  Default [DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTO_REFILL] DROP CONSTRAINT [DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] DROP CONSTRAINT [DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] DROP CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_GROUP_MFPS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_GROUP_MFPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_GROUP_MFPS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_GROUP_MFPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_GROUP_MFPS] DROP CONSTRAINT [DF_T_GROUP_MFPS_REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_SIZE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] DROP CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] DROP CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] DROP CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_MFP_ID]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_MFP_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_MFP_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_MFP_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_GRUP_ID]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_GRUP_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_GRUP_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_GRUP_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_USR_SOURCE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_USR_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_USR_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_USR_SOURCE]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_PRICE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_PRICE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_PRICE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_JOB_PRICE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_PRICE_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_PRICE_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_PRICE_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_JOB_PRICE_BW]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_REC_DATE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_REC_DATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_REC_DATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF_T_JOB_LOG_REC_DATE]
END


End
GO
/****** Object:  Default [DF__T_JOB_LOG__JOB_U__0CA5D9DE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__T_JOB_LOG__JOB_U__0CA5D9DE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__T_JOB_LOG__JOB_U__0CA5D9DE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] DROP CONSTRAINT [DF__T_JOB_LOG__JOB_U__0CA5D9DE]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF__T_JOB_PER__COSTC__603D47BB]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__T_JOB_PER__COSTC__603D47BB]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__T_JOB_PER__COSTC__603D47BB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF__T_JOB_PER__COSTC__603D47BB]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] DROP CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_JOB_SIZE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] DROP CONSTRAINT [DF_T_JOB_TRANSMITTER_JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] DROP CONSTRAINT [DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_LISTNER_PORT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_LISTNER_PORT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_LISTNER_PORT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] DROP CONSTRAINT [DF_T_JOB_TRANSMITTER_LISTNER_PORT]
END


End
GO
/****** Object:  Default [DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_USAGE_PAPERSIZE]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_USAGE_PAPERSIZE] DROP CONSTRAINT [DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_SIZE]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] DROP CONSTRAINT [DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]
END


End
GO
/****** Object:  Default [DF_T_SERVICE_MONITOR_SRVC_TIME]    Script Date: 06/06/2012 13:34:33 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_SERVICE_MONITOR_SRVC_TIME]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_SERVICE_MONITOR]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_SERVICE_MONITOR_SRVC_TIME]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_SERVICE_MONITOR] DROP CONSTRAINT [DF_T_SERVICE_MONITOR_SRVC_TIME]
END


End
GO
/****** Object:  Table [dbo].[M_MFP_GROUPS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MFP_GROUPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_MFP_GROUPS](
	[GRUP_ID] [int] IDENTITY(1,1) NOT NULL,
	[GRUP_NAME] [nvarchar](50)  NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](50)  NULL,
 CONSTRAINT [PK_M_MFP_GROUPS] PRIMARY KEY CLUSTERED 
(
	[GRUP_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[AD_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AD_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AD_SETTINGS](
	[SLNO] [int] IDENTITY(1,1) NOT NULL,
	[AD_SETTING_KEY] [nvarchar](100)  NULL,
	[AD_SETTING_VALUE] [nvarchar](1000)  NULL,
	[AD_SETTING_DESCRIPTION] [nvarchar](50)  NULL,
	[AD_DOMAIN_NAME] [varchar](100)  NULL
)
END
GO
/****** Object:  Table [dbo].[T_PRINT_JOBS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_PRINT_JOBS](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[JOB_RELEASER_ASSIGNED] [nvarchar](50)  NULL,
	[USER_SOURCE] [varchar](50)  NULL,
	[USER_ID] [varchar](50)  NULL,
	[JOB_ID] [nvarchar](300)  NULL,
	[JOB_FILE] [nvarchar](300)  NULL,
	[JOB_SIZE] [bigint] NULL,
	[JOB_SETTINGS_ORIGINAL] [ntext]  NULL,
	[JOB_SETTINGS_REQUEST] [ntext]  NULL,
	[JOB_CHANGED_SETTINGS] [bit] NULL,
	[JOB_RELEASE_WITH_SETTINGS] [bit] NULL,
	[JOB_FTP_PATH] [varchar](500)  NULL,
	[JOB_FTP_ID] [nvarchar](50)  NULL,
	[JOB_FTP_PASSWORD] [nvarchar](50)  NULL,
	[JOB_PRINT_RELEASED] [bit] NULL,
	[DELETE_AFTER_PRINT] [bit] NULL,
	[JOB_RELEASE_NOTIFY] [bit] NULL,
	[JOB_RELEASE_NOTIFY_EMAIL] [varchar](max)  NULL,
	[JOB_RELEASE_NOTIFY_SMS] [varchar](500)  NULL,
	[JOB_RELEASE_REQUEST_FROM] [nvarchar](50)  NULL,
	[REC_DATE] [datetime] NULL,
	[USER_DOMAIN] [nvarchar](200)  NULL,
	[JOB_REQUEST_MFP] [nvarchar](50) NULL,
	[JOB_REQUEST_DATE] [datetime] NULL CONSTRAINT [DF_T_PRINT_JOBS_JOB_REQUEST_DATE]  DEFAULT (getdate()),
	[JOB_PRINT_REQUEST_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK_T_PRINT_JOBS] PRIMARY KEY CLUSTERED 
(
	[REC_SYSID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[APP_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APP_SETTINGS](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[APPSETNG_CATEGORY] [nvarchar](30)  NOT NULL,
	[APPSETNG_KEY] [nvarchar](50)  NOT NULL,
	[APPSETNG_RESX_ID] [varchar](100)  NULL,
	[APPSETNG_KEY_DESC] [nvarchar](50)  NULL,
	[APPSETNG_KEY_ORDER] [tinyint] NULL,
	[APPSETNG_VALUE] [nvarchar](50)  NULL,
	[APPSETNG_VALUE_TYPE] [varchar](20)  NULL,
	[ADS_LIST] [nvarchar](150)  NULL,
	[ADS_LIST_VALUES] [nvarchar](150)  NULL,
	[ADS_DEF_VALUE] [nvarchar](50)  NULL,
	[CONTROL_TYPE] [nvarchar](50)  NULL,
	[REC_ACTIVE] [bit] NULL,
 CONSTRAINT [PK_APP_SETTINGS] PRIMARY KEY CLUSTERED 
(
	[APPSETNG_CATEGORY] ASC,
	[APPSETNG_KEY] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO

/****** Object:  Table [dbo].[T_SERVICE_MONITOR]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_SERVICE_MONITOR]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_SERVICE_MONITOR](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[SRVC_NAME] [nvarchar](max)  NULL,
	[SRVC_PORT] [smallint] NULL,
	[SRVC_STAUS] [nvarchar](10)  NULL,
	[SRVC_TIME] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[T_GROUP_MFPS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_GROUP_MFPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_GROUP_MFPS](
	[GRUP_ID] [int] NOT NULL,
	[MFP_IP] [nvarchar](50)  NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20)  NULL
)
END
GO
/****** Object:  Table [dbo].[INVALID_CARD_CONFIGURATION]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[INVALID_CARD_CONFIGURATION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[INVALID_CARD_CONFIGURATION](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[CARD_TYPE] [nvarchar](50)  NOT NULL,
	[CARD_RULE] [varchar](3)  NOT NULL,
	[CARD_SUB_RULE] [varchar](10)  NOT NULL,
	[CARD_DATA_ENABLED] [bit] NULL,
	[CARD_DATA_ON] [char](1)  NULL,
	[CARD_POSITION_START] [int] NULL,
	[CARD_POSITION_LENGTH] [int] NULL,
	[CARD_DELIMETER_START] [nvarchar](50)  NULL,
	[CARD_DELIMETER_END] [nvarchar](50)  NULL,
	[CARD_CODE_VALUE] [nvarchar](50)  NULL,
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
/****** Object:  Table [dbo].[T_ASSIGN_MFP_USER_GROUPS]    Script Date: 06/06/2012 13:34:33 ******/
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
	[REC_USER] [nvarchar](50)  NULL
)
END
GO
/****** Object:  Table [dbo].[M_PAPER_SIZES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PAPER_SIZES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_PAPER_SIZES](
	[SYS_ID] [int] IDENTITY(1,1) NOT NULL,
	[PAPER_SIZE_NAME] [nvarchar](50)  NULL,
	[PAPER_SIZE_CATEGORY] [nvarchar](50)  NULL,
	[REC_ACTIVE] [bit] NULL
)
END
GO
/****** Object:  Table [dbo].[APP_LANGUAGES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APP_LANGUAGES](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[APP_CULTURE] [varchar](10)  NOT NULL,
	[APP_NEUTRAL_CULTURE] [varchar](10)  NULL,
	[APP_LANGUAGE] [nvarchar](50)  NULL,
	[APP_CULTURE_DIR] [varchar](3)  NULL,
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
/****** Object:  Table [dbo].[T_PRICES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRICES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_PRICES](
	[PRICE_PROFILE_ID] [int] NOT NULL,
	[JOB_TYPE] [varchar](20)  NOT NULL,
	[PAPER_SIZE] [nvarchar](50)  NULL,
	[PRICE_PERUNIT_COLOR] [float] NULL,
	[PRICE_PERUNIT_BLACK] [float] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20)  NULL
)
END
GO
/****** Object:  Table [dbo].[T_CURRENT_SESSIONS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENT_SESSIONS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_CURRENT_SESSIONS](
	[CLIENT_SESSION_ID] [nvarchar](50)  NOT NULL,
	[CLIENT_MACHINE_ID] [nvarchar](50)  NOT NULL,
	[LAST_ACCESSTIME] [datetime] NULL,
 CONSTRAINT [PK_T_CURRENT_SESSIONS] PRIMARY KEY CLUSTERED 
(
	[CLIENT_SESSION_ID] ASC,
	[CLIENT_MACHINE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[CARD_CONFIGURATION]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CARD_CONFIGURATION](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[CARD_TYPE] [nvarchar](50)  NOT NULL,
	[CARD_RULE] [varchar](3)  NOT NULL,
	[CARD_SUB_RULE] [varchar](10)  NOT NULL,
	[CARD_DATA_ENABLED] [bit] NULL,
	[CARD_DATA_ON] [char](1)  NULL,
	[CARD_POSITION_START] [int] NULL,
	[CARD_POSITION_LENGTH] [int] NULL,
	[CARD_DELIMETER_START] [nvarchar](50)  NULL,
	[CARD_DELIMETER_END] [nvarchar](50)  NULL,
	[CARD_CODE_VALUE] [nvarchar](50)  NULL,
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
/****** Object:  Table [dbo].[T_PRINT_JOB_WEB_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOB_WEB_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_PRINT_JOB_WEB_SETTINGS](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[USR_ID] [varchar](30)  NOT NULL,
	[JOB_NAME] [nvarchar](150)  NOT NULL,
	[DRIVER_PRINT_SETTING] [varchar](50)  NOT NULL,
	[DRIVER_PRINT_SETTING_VALUE] [nvarchar](150)  NULL,
	[OSA_SETTING] [varchar](50)  NULL,
	[OSA_SETTING_VALUE] [nvarchar](150)  NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](50)  NULL,
	[REC_EDITOR] [nvarchar](50)  NULL,
 CONSTRAINT [PK_T_PRINT_JOB_WEB_SETTINGS] PRIMARY KEY CLUSTERED 
(
	[USR_ID] ASC,
	[JOB_NAME] ASC,
	[DRIVER_PRINT_SETTING] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_AUTO_REFILL]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AUTO_REFILL](
	[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
	[AUTO_FILLING_TYPE] [nvarchar](50)  NOT NULL,
	[AUTO_REFILL_FOR] [varchar](5)  NULL,
	[ADD_TO_EXIST_LIMITS] [nvarchar](50)  NOT NULL,
	[AUTO_REFILL_ON] [nvarchar](50)  NOT NULL,
	[AUTO_REFILL_VALUE] [nvarchar](50)  NOT NULL,
	[LAST_REFILLED_ON] [datetime] NULL,
	[IS_REFILL_REQUIRED] [bit] NULL
)
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'T_AUTO_REFILL', N'COLUMN',N'IS_REFILL_REQUIRED'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is refill details are modified' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_AUTO_REFILL', @level2type=N'COLUMN',@level2name=N'IS_REFILL_REQUIRED'
GO
/****** Object:  Table [dbo].[CostCenterDefaultPL]    Script Date: 06/06/2012 13:34:33 ******/
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
	[JOB_TYPE] [varchar](20)  NOT NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL
)
END
GO
/****** Object:  Table [dbo].[M_OSA_JOB_PROPERTIES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_OSA_JOB_PROPERTIES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_OSA_JOB_PROPERTIES](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[JOB_PROPERTY_CATEGORY] [varchar](50)  NULL,
	[JOB_TYPE] [char](1)  NOT NULL,
	[JOB_PROPERTY] [varchar](25)  NOT NULL,
	[JOB_ORDER] [tinyint] NULL,
	[JOB_PROPERTY_RESX] [varchar](25)  NULL,
	[JOB_PROPERTY_TYPE] [varchar](50)  NULL,
	[JOB_PROPERTY_ALLOWED] [varchar](500)  NULL,
	[JOB_PROPERTY_DEFAULT] [varchar](30)  NULL,
	[JOB_PROPERTY_VALIDATAION] [varchar](100)  NULL,
	[JOB_PROPERTY_SETTABLE] [bit] NULL,
	[JOB_PROPERTY_DRIVER_SETTING] [varchar](25)  NULL,
	[JOB_PROPERTY_DRIVER_VALUES] [varchar](500)  NULL,
 CONSTRAINT [PK_M_OSA_JOB_PROPERTIES_1] PRIMARY KEY CLUSTERED 
(
	[JOB_TYPE] ASC,
	[JOB_PROPERTY] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS](
	[COSTCENTER_ID] [int] NOT NULL,
	[USER_ID] [int] NOT NULL,
	[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
	[JOB_TYPE] [varchar](30)  NOT NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_USED] [bigint] NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
 CONSTRAINT [PK_T_JOB_PERMISSIONS_LIMITS] PRIMARY KEY CLUSTERED 
(
	[COSTCENTER_ID] ASC,
	[USER_ID] ASC,
	[PERMISSIONS_LIMITS_ON] ASC,
	[JOB_TYPE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[RESX_CLIENT_MESSAGES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_CLIENT_MESSAGES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RESX_CLIENT_MESSAGES](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[RESX_MODULE] [nvarchar](30)  NULL,
	[RESX_CULTURE_ID] [varchar](50)  NOT NULL,
	[RESX_ID] [varchar](50)  NOT NULL,
	[RESX_VALUE] [nvarchar](2000)  NULL,
	[RESX_IS_USED] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](50)  NULL,
	[REC_EDITOR] [nvarchar](50)  NULL,
 CONSTRAINT [PK_RESX_CLIENT_MESSAGES] PRIMARY KEY CLUSTERED 
(
	[RESX_ID] ASC,
	[RESX_CULTURE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[M_USERS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_USERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_USERS](
	[USR_ACCOUNT_ID] [int] IDENTITY(1000,1) NOT NULL,
	[USR_SOURCE] [varchar](2)  NOT NULL,
	[USR_DOMAIN] [nvarchar](50)  NOT NULL,
	[USR_ID] [nvarchar](100)  NOT NULL,
	[USR_CARD_ID] [nvarchar](1000)  NULL,
	[USR_NAME] [nvarchar](100)  NULL,
	[USR_PIN] [nvarchar](1000)  NULL,
	[USR_PASSWORD] [nvarchar](200)  NULL,
	[USR_ATHENTICATE_ON] [nvarchar](50)  NULL,
	[USR_EMAIL] [varchar](100)  NULL,
	[USR_DEPARTMENT] [int] NOT NULL,
	[USR_COSTCENTER] [int] NULL,
	[USR_AD_PIN_FIELD] [varchar](50)  NULL,
	[USR_ROLE] [varchar](10)  NOT NULL,
	[RETRY_COUNT] [tinyint] NULL,
	[RETRY_DATE] [datetime] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_ACTIVE] [bit] NULL,
	[ALLOW_OVER_DRAFT] [bit] NULL,
	[ISUSER_LOGGEDIN_MFP] [bit] NULL,
	[USR_MY_ACCOUNT] [bit] NULL ,
	[USER_COMMAND][nvarchar](50)  NULL,
	[DEPARTMENT] [nvarchar](200) NULL,
	[COMPANY] [nvarchar](200) NULL,
	[MANAGER] [nvarchar](200) NULL,
	[EXTERNAL_SOURCE] [nvarchar](200) NULL,
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
/****** Object:  Table [dbo].[RESX_LABELS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_LABELS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RESX_LABELS](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[RESX_MODULE] [nvarchar](30)  NULL,
	[RESX_CULTURE_ID] [varchar](50)  NOT NULL,
	[RESX_ID] [varchar](50)  NOT NULL,
	[RESX_VALUE] [nvarchar](250)  NULL,
	[RESX_IS_USED] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](50)  NULL,
	[REC_EDITOR] [nvarchar](50)  NULL,
 CONSTRAINT [PK_RESX_LABELS] PRIMARY KEY CLUSTERED 
(
	[RESX_CULTURE_ID] ASC,
	[RESX_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[JOB_CONFIGURATION]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JOB_CONFIGURATION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[JOB_CONFIGURATION](
	[SLNO] [int] IDENTITY(1,1) NOT NULL,
	[JOBSETTING_KEY] [varchar](100)  NOT NULL,
	[JOBSETTING_VALUE] [varchar](50)  NOT NULL,
	[JOBSETTING_DISCRIPTION] [varchar](200)  NULL
)
END
GO
/****** Object:  Table [dbo].[T_JOBS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOBS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOBS](
	[SLNO] [int] IDENTITY(1,1) NOT NULL,
	[USER_ID] [varchar](100)  NOT NULL,
	[JOB_ID] [varchar](200)  NOT NULL,
	[FILE_TYPE] [varchar](50)  NOT NULL,
	[FILE_DATA] [image] NULL,
	[CDATE] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[AccountingPlus_Users]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountingPlus_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountingPlus_Users](
	[USR_ACCOUNT_ID] [nvarchar](50)  NULL,
	[USR_SOURCE] [nvarchar](50)  NULL,
	[USR_DOMAIN] [nvarchar](50)  NULL,
	[USR_ID] [nvarchar](50)  NULL,
	[USR_CARD_ID] [nvarchar](50)  NULL,
	[USR_NAME] [nvarchar](50)  NULL,
	[USR_PIN] [nvarchar](50)  NULL,
	[USR_PASSWORD] [nvarchar](50)  NULL,
	[USR_ATHENTICATE_ON] [nvarchar](50)  NULL,
	[USR_EMAIL] [nvarchar](50)  NULL,
	[USR_DEPARTMENT] [nvarchar](50)  NULL,
	[USR_AD_PIN_FIELD] [nvarchar](50)  NULL,
	[USR_ROLE] [nvarchar](50)  NULL,
	[RETRY_COUNT] [nvarchar](50)  NULL,
	[RETRY_DATE] [nvarchar](50)  NULL,
	[REC_CDATE] [nvarchar](50)  NULL,
	[REC_ACTIVE] [nvarchar](50)  NULL,
	[ALLOW_OVER_DRAFT] [nvarchar](50)  NULL
)
END
GO
/****** Object:  Table [dbo].[T_CURRENT_JOBS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENT_JOBS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_CURRENT_JOBS](
	[JOB_ID] [int] IDENTITY(1,1) NOT NULL,
	[JOB_NAME] [nvarchar](500)  NULL,
	[JOB_DATE] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[RESX_SERVER_MESSAGES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RESX_SERVER_MESSAGES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RESX_SERVER_MESSAGES](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[RESX_MODULE] [nvarchar](30)  NULL,
	[RESX_CULTURE_ID] [varchar](50)  NOT NULL,
	[RESX_ID] [varchar](50)  NOT NULL,
	[RESX_VALUE] [nvarchar](2000)  NULL,
	[RESX_IS_USED] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](50)  NULL,
	[REC_EDITOR] [nvarchar](50)  NULL,
 CONSTRAINT [PK_RESX_SERVER_MESSAGES] PRIMARY KEY CLUSTERED 
(
	[RESX_CULTURE_ID] ASC,
	[RESX_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_COSTCENTER_USERS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_COSTCENTER_USERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_COSTCENTER_USERS](
	[REC_ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[USR_ACCOUNT_ID] [int] NULL,
	[USR_ID] [nvarchar](50)  NOT NULL,
	[COST_CENTER_ID] [nvarchar](50)  NOT NULL,
	[USR_SOURCE] [nvarchar](2)  NULL
)
END
GO
/****** Object:  Table [dbo].[T_PROXY_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PROXY_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_PROXY_SETTINGS](
	[REC_ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[PROXY_ENABLED] [nvarchar](50)  NULL,
	[SERVER_URL] [nvarchar](1000)  NULL,
	[DOMAIN_NAME] [nvarchar](100)  NULL,
	[USER_NAME] [nvarchar](100)  NULL,
	[USER_PASSWORD] [nvarchar](100)  NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[REC_STATUS] [bit] NULL,
 CONSTRAINT [PK_T_PROXY_SETTINGS] PRIMARY KEY CLUSTERED 
(
	[REC_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[M_COST_CENTERS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_COST_CENTERS](
	[COSTCENTER_ID] [int] IDENTITY(1,1) NOT NULL,
	[COSTCENTER_NAME] [nvarchar](50)  NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20)  NULL,
	[ALLOW_OVER_DRAFT] [bit] NULL,
	[IS_SHARED] [bit] NULL,
	[USR_SOURCE] [varchar](2) NULL,
 CONSTRAINT [PK_M_COST_CENTERS] PRIMARY KEY CLUSTERED 
(
	[COSTCENTER_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[JOB_COMPLETED_STATUS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JOB_COMPLETED_STATUS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[JOB_COMPLETED_STATUS](
	[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
	[JOB_COMPLETED_TPYE] [nvarchar](50)  NULL,
	[REC_STATUS] [bit] NULL
)
END
GO
/****** Object:  Table [dbo].[TEMP_CARD_MAPPING]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_CARD_MAPPING]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TEMP_CARD_MAPPING](
	[SESSION_ID] [nvarchar](100)  NULL,
	[USR_SOURCE] [varchar](2)  NULL,
	[USR_ID] [nvarchar](100)  NULL,
	[USR_CARD_ID] [nvarchar](1000)  NULL,
	[USR_PIN] [nvarchar](1000)  NULL,
	[USR_DOMAIN] [nvarchar](50) NULL
)
END
GO
/****** Object:  Table [dbo].[M_MFPS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MFPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_MFPS](
	[MFP_ID] [int] IDENTITY(1,1) NOT NULL,
	[MFP_IP] [nvarchar](30) NOT NULL,
	[MFP_HOST_NAME] [nvarchar](200) NULL,
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
	[LAST_LOGGEDIN_USER] [nvarchar](100) NULL,
	[LAST_LOGGEDIN_TIME] [datetime] NULL,
	[EMAIL_ID] [nvarchar](100) NULL,
	[EMAIL_HOST] [nvarchar](50) NULL,
	[EMAIL_PORT] [nvarchar](5) NULL,
	[EMAIL_USERNAME] [nvarchar](100) NULL,
	[EMAIL_PASSWORD] [nvarchar](100) NULL,
	[EMAIL_REQUIRE_SSL] [bit] NULL CONSTRAINT [DF_M_MFPS_EMAIL_REQUIRE_SSL]  DEFAULT ((0)),
	[EMAIL_DIRECT_PRINT] [bit] NULL CONSTRAINT [DF_M_MFPS_EMAIL_DIRECT_PRINT]  DEFAULT ((0)),
	[EMAIL_MESSAGE_COUNT] [varchar](50) NULL,
	[OSA_IC_CARD] [bit] NULL CONSTRAINT [DF_M_MFPS_OSA_IC_CARD]  DEFAULT ((0)),
	[MFP_COMMAND1] [nvarchar](max) NULL,
	[MFP_COMMAND2] [nvarchar](max) NULL,
	[MFP_NOTES] [nvarchar](max) NULL,
	[MFP_R_DATE] [datetime] NULL,
	[MFP_ASP_SESSION] [nvarchar](200) NULL,
	[MFP_UI_SESSION] [nvarchar](200) NULL,
	[MFP_LAST_UPDATE] [datetime] NULL,
	[MFP_STATUS] [bit] NULL,
	[MFP_GUEST] [bit] NULL,
	[LAST_PRINT_USER] [nvarchar](100) NULL,
	[LAST_PRINT_TIME] [datetime] NULL,
	[EMAIL_ID_ADMIN][nvarchar](100) NULL,
	
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
/****** Object:  Table [dbo].[M_USER_GROUPS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_USER_GROUPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_USER_GROUPS](
	[GRUP_ID] [int] IDENTITY(1,1) NOT NULL,
	[GRUP_NAME] [nvarchar](50)  NOT NULL,
	[GRUP_SOURCE] [varchar](2)  NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20)  NULL,
	[AUTH_TYPE] [nvarchar](50)  NULL,
	[SYNC_STATUS] [nvarchar](50)  NULL,
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
/****** Object:  Table [dbo].[T_JOB_TRACKER]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_TRACKER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_TRACKER](
	[REC_SYS_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SRVC_NAME] [nvarchar](50)  NULL,
	[SRVC_PORT] [smallint] NULL,
	[JOB_TEMP_FILE] [nvarchar](255)  NULL,
	[JOB_PRN_FILE] [nvarchar](255)  NULL,
	[JOB_SIZE] [decimal](18, 0) NULL,
	[JOB_NAME] [nvarchar](255)  NULL,
	[JOB_MACHINE_NAME] [nvarchar](100)  NULL,
	[JOB_DRIVER_NAME] [nvarchar](150)  NULL,
	[JOB_DRIVER_TYPE] [nvarchar](100)  NULL,
	[JOB_SPOOL_START_TIME] [nvarchar](150)  NULL,
	[JOB_USER_SOURCE] [varchar](2)  NULL,
	[JOB_USER_NAME] [nvarchar](100)  NULL,
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
/****** Object:  Table [dbo].[M_PRICE_PROFILES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PRICE_PROFILES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_PRICE_PROFILES](
	[PRICE_PROFILE_ID] [int] IDENTITY(1,1) NOT NULL,
	[PRICE_PROFILE_NAME] [nvarchar](50)  NOT NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](20)  NULL,
 CONSTRAINT [PK_M_PRICE_PROFILES] PRIMARY KEY CLUSTERED 
(
	[PRICE_PROFILE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP](
	[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
	[JOB_TYPE] [varchar](30)  NOT NULL,
	[SESSION_ID] [nvarchar](150)  NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_USED] [bigint] NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
	[C_DATE] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[M_SMTP_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_SMTP_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_SMTP_SETTINGS](
	[REC_SYS_ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FROM_ADDRESS] [nvarchar](max) NOT NULL,
	[CC_ADDRESS] [nvarchar](max) NULL,
	[BCC_ADDRESS] [nvarchar](max) NULL,
	[SMTP_HOST] [nvarchar](50) NOT NULL,
	[SMTP_PORT] [int] NOT NULL,
	[DOMAIN_NAME] [nvarchar](50) NOT NULL,
	[USERNAME] [nvarchar](50) NOT NULL,
	[PASSWORD] [nvarchar](50) NOT NULL,
	[REQUIRE_SSL] [bit] NULL,
 CONSTRAINT [PK_M_SMTP_SETTINGS] PRIMARY KEY CLUSTERED 
(
	[REC_SYS_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[M_COUNTRIES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_COUNTRIES](
	[COUNTRY_ID] [int] IDENTITY(1,1) NOT NULL,
	[COUNTRY_NAME] [nvarchar](50)  NOT NULL,
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
/****** Object:  Table [dbo].[T_ACCESS_RIGHTS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ACCESS_RIGHTS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_ACCESS_RIGHTS](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[ASSIGN_ON] [nvarchar](30)  NOT NULL,
	[ASSIGN_TO] [nvarchar](30)  NULL,
	[MFP_OR_GROUP_ID] [nvarchar](50)  NULL,
	[USER_OR_COSTCENTER_ID] [nvarchar](50)  NULL,
	[USR_SOURCE] [varchar](2)  NULL,
	[REC_STATUS] [bit] NULL  DEFAULT ((1))
)
END
GO
/****** Object:  Table [dbo].[APP_IMAGES]    Script Date: 06/06/2012 13:34:33 ******/
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
/****** Object:  Table [dbo].[T_JOB_TRANSMITTER]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_TRANSMITTER](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[USER_SOURCE] [varchar](2)  NULL,
	[USER_ID] [nvarchar](50)  NULL,
	[JOB_DRIVER] [nvarchar](50)  NULL,
	[JOB_FILE] [nvarchar](255)  NULL,
	[JOB_SIZE] [int] NULL,
	[JOB_TRMSN_START] [datetime] NULL,
	[JOB_TRRX_END] [datetime] NULL,
	[JOB_TRMSN_DURATION] [int] NULL,
	[JOB_TRMSN_STATUS] [nvarchar](20)  NULL,
	[LISTNER_PORT] [int] NULL,
	[TRNSMTR_OS] [nvarchar](100)  NULL,
	[TRNSMTR_NAME] [nvarchar](50)  NULL,
	[TRNSMTR_IP] [varchar](50)  NULL,
	[TRNSMTR_PROCESSOR] [nvarchar](100)  NULL,
	[REC_DATE] [datetime] NULL,
 CONSTRAINT [PK_T_JOB_TRANSMITTER] PRIMARY KEY CLUSTERED 
(
	[REC_SYSID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[APP_VERSION]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_VERSION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APP_VERSION](
	[VERSION] [varchar](20)  NULL
)
END
GO
/****** Object:  Table [dbo].[M_SRV_TOKENS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_SRV_TOKENS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_SRV_TOKENS](
	[TOKEN_ID] [int] NULL,
	[MSTR_SRV_URL] [nchar](10)  NULL
)
END
GO
/****** Object:  Table [dbo].[M_JOB_TYPES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_JOB_TYPES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_JOB_TYPES](
	[JOB_ID] [varchar](30)  NOT NULL,
	[ITEM_ORDER] [smallint] NULL,
 CONSTRAINT [PK_M_JOB_TYPES] PRIMARY KEY CLUSTERED 
(
	[JOB_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_AUDIT_LOG]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUDIT_LOG]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AUDIT_LOG](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[MSG_SOURCE] [varchar](30)  NULL,
	[MSG_TYPE] [nvarchar](20)  NULL,
	[MSG_TEXT] [nvarchar](max)  NULL,
	[MSG_SUGGESTION] [nvarchar](max)  NULL,
	[MSG_EXCEPTION] [nvarchar](max)  NULL,
	[MSG_STACKSTRACE] [nvarchar](max)  NULL,
	[REC_USER] [nvarchar](30)  NULL,
	[REC_DATE] [datetime] NOT NULL,
	[SERVER_IP] [varchar](50)  NULL,
	[SERVER_LOCATION] [nvarchar](100)  NULL,
	[SERVER_TOKEN_ID] [int] NULL,
 CONSTRAINT [PK_T_AUDIT_LOG] PRIMARY KEY CLUSTERED 
(
	[REC_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_ASSGIN_COST_PROFILE_MFPGROUPS](
	[COST_PROFILE_ID] [int] NOT NULL,
	[ASSIGNED_TO] [nvarchar](30)  NULL,
	[MFP_GROUP_ID] [nvarchar](50)  NOT NULL,
	[REC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](50)  NULL
)
END
GO
/****** Object:  Table [dbo].[T_DEVICE_FLEETS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_DEVICE_FLEETS](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[DEVICE_ID] [nvarchar](30)  NOT NULL,
	[DEVICE_STATUS] [nvarchar](30)  NULL,
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
/****** Object:  Table [dbo].[M_JOB_CATEGORIES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_JOB_CATEGORIES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_JOB_CATEGORIES](
	[JOB_ID] [varchar](50)  NOT NULL,
	[ITEM_ORDER] [smallint] NULL
)
END
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL](
	[GRUP_ID] [int] NOT NULL,
	[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
	[JOB_TYPE] [varchar](50)  NOT NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_USED] [bigint] NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
	[LAST_REFILLED_ON] [datetime] NULL,
 CONSTRAINT [PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] PRIMARY KEY CLUSTERED 
(
	[GRUP_ID] ASC,
	[PERMISSIONS_LIMITS_ON] ASC,
	[JOB_TYPE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[SQL_EXECUTION]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SQL_EXECUTION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SQL_EXECUTION](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[IP_ADDRESS] [nvarchar](50)  NULL,
	[SQL_QUERY] [ntext]  NULL,
	[REC_DATE] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[T_AUTOREFILL_LIMITS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AUTOREFILL_LIMITS](
	[GRUP_ID] [int] NOT NULL,
	[JOB_TYPE] [nvarchar](50)  NOT NULL,
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
/****** Object:  Table [dbo].[T_JOB_DISPATCHER]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_DISPATCHER](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[USER_SOURCE] [varchar](2)  NULL,
	[USER_ID] [nvarchar](50)  NULL,
	[JOB_DRIVER] [nvarchar](50)  NULL,
	[JOB_FILE] [nvarchar](255)  NULL,
	[JOB_SIZE] [int] NULL,
	[JOB_SETTINGS_FILE_NAME] [nvarchar](100)  NULL,
	[JOB_SETTINGS] [ntext]  NULL,
	[JOB_DESTINATION_ADDRESS] [nvarchar](500)  NULL,
	[JOB_DESTINATION_USER_ID] [varchar](50)  NULL,
	[JOB_DESTINATION_USER_PASSWORD] [varchar](50)  NULL,
	[JOB_DISPATCH_WITH_SETTINGS] [bit] NULL,
	[JOB_DISPATCH_START] [datetime] NULL,
	[JOB_DISPATCH_END] [datetime] NULL,
	[JOB_DISPACTCH_DURATION] [int] NULL,
	[JOB_DISPATCH_STATUS] [nvarchar](20)  NULL,
	[DISPATCHER_SRVR_NAME] [nvarchar](50)  NULL,
	[DISPATCHER_SRVR_IP] [varchar](50)  NULL,
	[DISPATCHER_SRVR_OS] [nvarchar](100)  NULL,
	[DISPATCHER_SRVR_PROCESSOR] [nvarchar](100)  NULL,
	[REC_DATE] [datetime] NULL,
 CONSTRAINT [PK_T_JOB_DISPATCHER] PRIMARY KEY CLUSTERED 
(
	[REC_SYSID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_JOB_LOG]    Script Date: 06/06/2012 13:34:33 ******/
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
	[COST_CENTER_NAME] [nvarchar](100)  NULL,
	[MFP_IP] [varchar](40)  NULL,
	[MFP_MACADDRESS] [varchar](50)  NULL,
	[USR_ID] [varchar](30)  NULL,
	[USR_DEPT] [nvarchar](100)  NULL,
	[USR_SOURCE] [varchar](2)  NULL,
	[JOB_ID] [varchar](50)  NULL,
	[JOB_MODE] [varchar](30)  NULL,
	[JOB_TYPE] [varchar](20)  NULL,
	[JOB_COMPUTER] [nvarchar](50)  NULL,
	[JOB_USRNAME] [nvarchar](50)  NULL,
	[JOB_START_DATE] [datetime] NULL,
	[JOB_END_DATE] [datetime] NULL,
	[JOB_COLOR_MODE] [varchar](20)  NULL,
	[JOB_SHEET_COUNT_COLOR] [int] NULL,
	[JOB_SHEET_COUNT_BW] [int] NULL,
	[JOB_SHEET_COUNT] [int] NOT NULL,
	[JOB_PRICE_COLOR] [float] NULL,
	[JOB_PRICE_BW] [float] NULL,
	[JOB_PRICE_TOTAL] [float] NULL,
	[JOB_STATUS] [varchar](30)  NULL,
	[JOB_PAPER_SIZE] [varchar](100)  NULL,
	[JOB_FILE_NAME] [nvarchar](255)  NULL,
	[DUPLEX_MODE] [varchar](20)  NULL,
	[REC_DATE] [datetime] NULL,
	[JOB_PAPER_SIZE_ORIGINAL] [varchar](100)  NULL,
	[JOB_USED_UPDATED] [bit] NULL,
	[OSA_JOB_COUNT] [int] NULL,
	[DOMAIN_NAME] [nvarchar](100)  NULL,
	[USER_ACCOUNT_ID] [int] NULL,
	[SERVER_IP] [varchar](50)  NULL,
	[SERVER_LOCATION] [nvarchar](100)  NULL,
	[SERVER_TOKEN_ID] [int] NULL,
	[JOB_JOB_NAME] [nvarchar](255) NULL,
	[NOTE] [ntext] NULL,
	[JOB_BALANCE_UPdated] [nchar](10) NULL,
	[DEPARTMENT] [nvarchar](200) NULL,
	[COMPANY] [nvarchar](200) NULL,
	[LOCATION] [nvarchar](200) NULL,
	[MFPNAME] [nvarchar](200) NULL,
	[EXTERNAL_SOURCE] [nvarchar](10) NULL,
 CONSTRAINT [PK_T_JOB_LOG_1] PRIMARY KEY CLUSTERED 
(
	[REC_SLNO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_JOB_USAGE_PAPERSIZE]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_USAGE_PAPERSIZE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_USAGE_PAPERSIZE](
	[REC_SLNO] [int] NOT NULL,
	[DEVICE_ID] [varchar](30)  NULL,
	[JOB_TYPE] [nvarchar](30)  NOT NULL,
	[JOB_PAPER_SIZE] [varchar](20)  NULL,
	[JOB_SHEET_COUNT] [int] NULL
)
END
GO
/****** Object:  Table [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP](
	[COSTCENTER_ID] [int] NOT NULL,
	[USER_ID] [int] NOT NULL,
	[PERMISSIONS_LIMITS_ON] [tinyint] NOT NULL,
	[JOB_TYPE] [varchar](30)  NOT NULL,
	[JOB_LIMIT] [bigint] NULL,
	[JOB_USED] [bigint] NULL,
	[ALERT_LIMIT] [int] NULL,
	[ALLOWED_OVER_DRAFT] [int] NULL,
	[JOB_ISALLOWED] [bit] NOT NULL,
 CONSTRAINT [PK_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_1] PRIMARY KEY CLUSTERED 
(
	[COSTCENTER_ID] ASC,
	[USER_ID] ASC,
	[PERMISSIONS_LIMITS_ON] ASC,
	[JOB_TYPE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[T_MFP_ACCESS_RIGHTS]    Script Date: 06/06/2012 13:34:33 ******/
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
/****** Object:  Table [dbo].[M_DEPARTMENTS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_DEPARTMENTS](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[DEPT_NAME] [nvarchar](100)  NOT NULL,
	[DEPT_SOURCE] [char](2)  NULL,
	[DEPT_DESC] [nvarchar](250)  NULL,
	[DEPT_RESX_ID] [varchar](50)  NULL,
	[REC_ACTIVE] [bit] NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[REC_AUTHOR] [nvarchar](20)  NULL,
	[REC_USER] [nvarchar](20)  NULL,
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
/****** Object:  Table [dbo].[T_AD_SYNC]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AD_SYNC]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AD_SYNC](
	[REC_SYS_ID] [int] IDENTITY(1,1) NOT NULL,
	[AD_SYNC_STATUS] [bit] NULL,
	[AD_SYNC_ON] [nvarchar](50)  NULL,
	[AD_SYNC_VALUE] [nvarchar](50)  NULL,
	[AD_LAST_SYNCED_ON] [datetime] NULL,
	[AD_IS_SYNC_REQUIRED] [bit] NULL,
	[AD_SYNC_USERS] [bit] NULL,
	[AD_SYNC_COSTCENTER] [bit] NULL,
	[AD_IMPORT_DEP_CC] [bit] NULL
)
END
GO
/****** Object:  Table [dbo].[T_AD_USERS]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_AD_USERS](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[USER_ID] [nvarchar](200)  NULL,
	[SESSION_ID] [nvarchar](100)  NULL,
	[USR_SOURCE] [varchar](2)  NULL,
	[USR_ROLE] [varchar](10)  NULL,
	[DOMAIN] [nvarchar](50)  NULL,
	[FIRST_NAME] [nvarchar](200)  NULL,
	[LAST_NAME] [nvarchar](200)  NULL,
	[EMAIL] [varchar](100)  NULL,
	[RESIDENCE_ADDRESS] [nvarchar](max)  NULL,
	[COMPANY] [varchar](50)  NULL,
	[STATE] [nvarchar](50)  NULL,
	[COUNTRY] [nvarchar](50)  NULL,
	[PHONE] [nvarchar](20)  NULL,
	[EXTENSION] [nvarchar](50)  NULL,
	[FAX] [nvarchar](50)  NULL,
	
	[USER_NAME] [nvarchar](200)  NULL,
	[CN] [nvarchar](200)  NULL,
	[DISPLAY_NAME] [nvarchar](200)  NULL,
	[FULL_NAME] [nvarchar](200)  NULL,
	[C_DATE] [datetime] NULL,
	[REC_ACTIVE] [bit] NULL,
	[AD_PIN] [nvarchar](1000)  NULL,
	[AD_CARD] [nvarchar](1000)  NULL,
	[DEPARTMENT] [nvarchar](200) NULL
	
)
END
GO
/****** Object:  Table [dbo].[LOG_CATEGORIES]    Script Date: 01/29/2014 14:55:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
DROP TABLE [dbo].[LOG_CATEGORIES]
GO
/****** Object:  Table [dbo].[LOG_CATEGORIES]    Script Date: 01/29/2014 14:55:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LOG_CATEGORIES](
	[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
	[LOG_TYPE] [nvarchar](50) NULL,
	[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[T_SRV]    Script Date: 07/24/2013 14:18:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_SRV]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_SRV](
	[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
	[SRV_MESSAGE_1] [nvarchar](max) NULL,
	[SRV_MESSAGE_2] [nvarchar](max) NULL,
	[SRV_NOTES] [nvarchar](max) NULL,
	[SRV_R_DATE] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[M_ROLES]    Script Date: 06/06/2012 13:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_ROLES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_ROLES](
	[ROLE_ID] [varchar](10)  NOT NULL,
	[ROLE_NAME] [nvarchar](50)  NULL,
 CONSTRAINT [PK_M_ROLES] PRIMARY KEY CLUSTERED 
(
	[ROLE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO

/****** Object:  Table [dbo].[T_QUERY_ANALYZER]    Script Date: 09/20/2013 17:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_QUERY_ANALYZER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_QUERY_ANALYZER](
	[REC_ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[QUERY_TEXT] [nvarchar](max) NULL,
	[QUERY_EXCEPTION_DETAILS] [nvarchar](50) NULL,
	[USER_ID] [nvarchar](50) NULL,
	[EXECUTED_DATETIME] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[REC_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Default [DF_APP_LANGUAGES_APP_CULTURE_DIR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_LANGUAGES_APP_CULTURE_DIR]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_LANGUAGES_APP_CULTURE_DIR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_LANGUAGES] ADD  CONSTRAINT [DF_APP_LANGUAGES_APP_CULTURE_DIR]  DEFAULT ('LTR') FOR [APP_CULTURE_DIR]
END


End
GO
/****** Object:  Default [DF_APP_LANGUAGES_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_LANGUAGES_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_LANGUAGES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_LANGUAGES_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_LANGUAGES] ADD  CONSTRAINT [DF_APP_LANGUAGES_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_APP_SETTINGS_APPSETNG_KEY_ORDER]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_APP_SETTINGS_APPSETNG_KEY_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_APP_SETTINGS_APPSETNG_KEY_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_SETTINGS] ADD  CONSTRAINT [DF_APP_SETTINGS_APPSETNG_KEY_ORDER]  DEFAULT ((0)) FOR [APPSETNG_KEY_ORDER]
END


End
GO
/****** Object:  Default [DF__APP_SETTI__REC_A__2E06CDA9]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__APP_SETTI__REC_A__2E06CDA9]') AND parent_object_id = OBJECT_ID(N'[dbo].[APP_SETTINGS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__APP_SETTI__REC_A__2E06CDA9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[APP_SETTINGS] ADD  CONSTRAINT [DF__APP_SETTI__REC_A__2E06CDA9]  DEFAULT ((1)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_CARD_DATA_ENABLED]  DEFAULT ((0)) FOR [CARD_DATA_ENABLED]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_DATA_ON]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_DATA_ON]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_DATA_ON]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_CARD_DATA_ON]  DEFAULT ('P') FOR [CARD_DATA_ON]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_POSITION_START]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_POSITION_START]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_POSITION_START]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_CARD_POSITION_START]  DEFAULT ((0)) FOR [CARD_POSITION_START]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_CARD_POSITION_LENGTH]  DEFAULT ((0)) FOR [CARD_POSITION_LENGTH]
END


End
GO
/****** Object:  Default [DF_CARD_CONFIGURATION_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CARD_CONFIGURATION_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CARD_CONFIGURATION_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CARD_CONFIGURATION] ADD  CONSTRAINT [DF_CARD_CONFIGURATION_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]') AND parent_object_id = OBJECT_ID(N'[dbo].[INVALID_CARD_CONFIGURATION]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[INVALID_CARD_CONFIGURATION] ADD  CONSTRAINT [DF_INVALID_CARD_CONFIGURATION_CARD_DATA_ON]  DEFAULT ('P') FOR [CARD_DATA_ON]
END


End
GO
/****** Object:  Default [DF_JOB_COMPLETED_STATUS_REC_STATUS]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_JOB_COMPLETED_STATUS_REC_STATUS]') AND parent_object_id = OBJECT_ID(N'[dbo].[JOB_COMPLETED_STATUS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_JOB_COMPLETED_STATUS_REC_STATUS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[JOB_COMPLETED_STATUS] ADD  CONSTRAINT [DF_JOB_COMPLETED_STATUS_REC_STATUS]  DEFAULT ((0)) FOR [REC_STATUS]
END


End
GO
/****** Object:  Default [DF_M_COST_CENTERS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COST_CENTERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COST_CENTERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COST_CENTERS] ADD  CONSTRAINT [DF_M_COST_CENTERS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COST_CENTERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COST_CENTERS] ADD  CONSTRAINT [DF_M_COST_CENTERS_ALLOW_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOW_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_M_COUNTRIES_REC_ACVITE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COUNTRIES_REC_ACVITE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COUNTRIES_REC_ACVITE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COUNTRIES] ADD  CONSTRAINT [DF_M_COUNTRIES_REC_ACVITE]  DEFAULT ((1)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_COUNTRIES_REC_USER]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_COUNTRIES_REC_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_COUNTRIES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_COUNTRIES_REC_USER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_COUNTRIES] ADD  CONSTRAINT [DF_M_COUNTRIES_REC_USER]  DEFAULT ((0)) FOR [REC_USER]
END


End
GO
/****** Object:  Default [DF_M_DEPARTMENTS_DEPT_SOURCE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_DEPARTMENTS_DEPT_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_DEPARTMENTS_DEPT_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_DEPARTMENTS] ADD  CONSTRAINT [DF_M_DEPARTMENTS_DEPT_SOURCE]  DEFAULT ('AD') FOR [DEPT_SOURCE]
END


End
GO
/****** Object:  Default [DF_M_DEPARTMENTS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_DEPARTMENTS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_DEPARTMENTS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_DEPARTMENTS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_DEPARTMENTS] ADD  CONSTRAINT [DF_M_DEPARTMENTS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_JOB_TYPES_ITEM_ORDER]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TYPES_ITEM_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_JOB_CATEGORIES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TYPES_ITEM_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_JOB_CATEGORIES] ADD  CONSTRAINT [DF_T_JOB_TYPES_ITEM_ORDER]  DEFAULT ((0)) FOR [ITEM_ORDER]
END


End
GO
/****** Object:  Default [DF_M_JOB_TYPES_ITEM_ORDER]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_JOB_TYPES_ITEM_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_JOB_TYPES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_JOB_TYPES_ITEM_ORDER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_JOB_TYPES] ADD  CONSTRAINT [DF_M_JOB_TYPES_ITEM_ORDER]  DEFAULT ((0)) FOR [ITEM_ORDER]
END


End
GO
/****** Object:  Default [DF_M_MFP_GROUPS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFP_GROUPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFP_GROUPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFP_GROUPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFP_GROUPS] ADD  CONSTRAINT [DF_M_MFP_GROUPS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_ALLOW_NETWORK_PASSWORD]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_ALLOW_NETWORK_PASSWORD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_ALLOW_NETWORK_PASSWORD]  DEFAULT ((0)) FOR [ALLOW_NETWORK_PASSWORD]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_SSO]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_SSO]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_SSO]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_SSO]  DEFAULT ((0)) FOR [MFP_SSO]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_LOCK_DOMAIN_FIELD]  DEFAULT ((0)) FOR [MFP_LOCK_DOMAIN_FIELD]
END


End
GO
/****** Object:  Default [DF_M_MFPS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_REC_ACTIVE]  DEFAULT ((1)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_REC_DATE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_REC_DATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_REC_DATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_REC_DATE]  DEFAULT (getdate()) FOR [REC_DATE]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_PRINT_API]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_PRINT_API]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_PRINT_API]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_PRINT_API]  DEFAULT (N'FTP') FOR [MFP_PRINT_API]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_EAM_ENABLED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_EAM_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_EAM_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_EAM_ENABLED]  DEFAULT ((0)) FOR [MFP_EAM_ENABLED]
END


End
GO
/****** Object:  Default [DF_M_MFPS_MFP_ACM_ENABLED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_MFPS_MFP_ACM_ENABLED]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_MFPS_MFP_ACM_ENABLED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_MFPS] ADD  CONSTRAINT [DF_M_MFPS_MFP_ACM_ENABLED]  DEFAULT ((0)) FOR [MFP_ACM_ENABLED]
END


End
GO
/****** Object:  Default [DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_OSA_JOB_PROPERTIES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_OSA_JOB_PROPERTIES] ADD  CONSTRAINT [DF_M_OSA_JOB_PROPERTIES_JOB_PROPERTY_SETTABLE]  DEFAULT ((0)) FOR [JOB_PROPERTY_SETTABLE]
END


End
GO
/****** Object:  Default [DF_M_PAPER_SIZES_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_PAPER_SIZES_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_PAPER_SIZES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_PAPER_SIZES_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_PAPER_SIZES] ADD  CONSTRAINT [DF_M_PAPER_SIZES_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_SOURCE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_USR_SOURCE]  DEFAULT ('DB') FOR [USR_SOURCE]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_DEPARTMENT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_DEPARTMENT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_DEPARTMENT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_USR_DEPARTMENT]  DEFAULT ((0)) FOR [USR_DEPARTMENT]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_COSTCENTER]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_COSTCENTER]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_COSTCENTER]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_USR_COSTCENTER]  DEFAULT ((-1)) FOR [USR_COSTCENTER]
END


End
GO
/****** Object:  Default [DF_M_USERS_USR_ROLE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_USR_ROLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_ROLE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_USR_ROLE]  DEFAULT (user_name()) FOR [USR_ROLE]
END


End
GO
/****** Object:  Default [DF_M_USERS_RETRY_COUNT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_RETRY_COUNT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_RETRY_COUNT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_RETRY_COUNT]  DEFAULT ((0)) FOR [RETRY_COUNT]
END


End
GO
/****** Object:  Default [DF_M_USERS_REC_CDATE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_REC_CDATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_REC_CDATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_REC_CDATE]  DEFAULT (getdate()) FOR [REC_CDATE]
END


End
GO
/****** Object:  Default [DF_M_USERS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_REC_ACTIVE]  DEFAULT ((1)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_M_USERS_ALLOW_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_M_USERS_ALLOW_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_ALLOW_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF_M_USERS_ALLOW_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOW_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF__M_USERS__ISUSER___2FEF161B]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__M_USERS__ISUSER___2FEF161B]') AND parent_object_id = OBJECT_ID(N'[dbo].[M_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__M_USERS__ISUSER___2FEF161B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[M_USERS] ADD  CONSTRAINT [DF__M_USERS__ISUSER___2FEF161B]  DEFAULT ((0)) FOR [ISUSER_LOGGEDIN_MFP]
END


End
GO
/****** Object:  Default [DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_CLIENT_MESSAGES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_CLIENT_MESSAGES] ADD  CONSTRAINT [DF_RESX_CLIENT_MESSAGES_RESX_IS_USED]  DEFAULT ((0)) FOR [RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_RESX_LABELS_RESX_IS_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_LABELS_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_LABELS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_LABELS_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_LABELS] ADD  CONSTRAINT [DF_RESX_LABELS_RESX_IS_USED]  DEFAULT ((0)) FOR [RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_RESX_SERVER_MESSAGES_RESX_IS_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RESX_SERVER_MESSAGES_RESX_IS_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[RESX_SERVER_MESSAGES]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RESX_SERVER_MESSAGES_RESX_IS_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RESX_SERVER_MESSAGES] ADD  CONSTRAINT [DF_RESX_SERVER_MESSAGES_RESX_IS_USED]  DEFAULT ((0)) FOR [RESX_IS_USED]
END


End
GO
/****** Object:  Default [DF_T_AD_USERS_DEPARTMENT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AD_USERS_DEPARTMENT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AD_USERS_DEPARTMENT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AD_USERS] ADD  CONSTRAINT [DF_T_AD_USERS_DEPARTMENT]  DEFAULT ((0)) FOR [DEPARTMENT]
END


End
GO
/****** Object:  Default [DF_T_AD_USERS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AD_USERS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AD_USERS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AD_USERS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AD_USERS] ADD  CONSTRAINT [DF_T_AD_USERS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_AUTO_REFILL_AUTO_REFILL_FOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTO_REFILL_AUTO_REFILL_FOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTO_REFILL_AUTO_REFILL_FOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTO_REFILL] ADD  CONSTRAINT [DF_T_AUTO_REFILL_AUTO_REFILL_FOR]  DEFAULT ('U') FOR [AUTO_REFILL_FOR]
END


End
GO
/****** Object:  Default [DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTO_REFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTO_REFILL] ADD  CONSTRAINT [DF_T_AUTO_REFILL_IS_REFILL_REQUIRED]  DEFAULT ((1)) FOR [IS_REFILL_REQUIRED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_IS_JOB_ALLOWED]  DEFAULT ((0)) FOR [IS_JOB_ALLOWED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_AUTOREFILL_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_AUTOREFILL_LIMITS] ADD  CONSTRAINT [DF_T_AUTOREFILL_LIMITS_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_PRINT_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_PRINT_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_PRINT_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_PRINT_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_PRINT_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_DOC_FILING_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_DOC_FILING_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_DOC_FILING_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_COPY_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_COPY_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_COPY_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_COPY_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_COPY_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_BW]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_OTHERS_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_OTHERS_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_OTHERS_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_OUTPUT_OTHERS_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_OUTPUT_OTHERS_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_BW]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_BW]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TO_HD_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TO_HD_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_SCAN_TO_HD_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_BW]  DEFAULT ((0)) FOR [DEVICE_SEND_INTERNET_FAX_BW]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_TWO_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_INTERNET_FAX_TWO_COLOR]
END


End
GO
/****** Object:  Default [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_DEVICE_FLEETS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_DEVICE_FLEETS] ADD  CONSTRAINT [DF_T_DEVICE_FLEETS_DEVICE_SEND_INTERNET_FAX_FULL_COLOR]  DEFAULT ((0)) FOR [DEVICE_SEND_INTERNET_FAX_FULL_COLOR]
END


End
GO
/****** Object:  Default [DF_T_GROUP_MFPS_REC_ACTIVE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_GROUP_MFPS_REC_ACTIVE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_GROUP_MFPS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_GROUP_MFPS_REC_ACTIVE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_GROUP_MFPS] ADD  CONSTRAINT [DF_T_GROUP_MFPS_REC_ACTIVE]  DEFAULT ((0)) FOR [REC_ACTIVE]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_SIZE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] ADD  CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_SIZE]  DEFAULT ((0)) FOR [JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] ADD  CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_DISPATCH_WITH_SETTINGS]  DEFAULT ((0)) FOR [JOB_DISPATCH_WITH_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_DISPATCHER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_DISPATCHER] ADD  CONSTRAINT [DF_T_JOB_DISPATCHER_JOB_DISPACTCH_DURATION]  DEFAULT ((0)) FOR [JOB_DISPACTCH_DURATION]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_MFP_ID]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_MFP_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_MFP_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_MFP_ID]  DEFAULT ((0)) FOR [MFP_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_GRUP_ID]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_GRUP_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_GRUP_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_GRUP_ID]  DEFAULT ((0)) FOR [GRUP_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_USR_SOURCE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_USR_SOURCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_USR_SOURCE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_USR_SOURCE]  DEFAULT ('DB') FOR [USR_SOURCE]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_JOB_SHEET_COUNT_COLOR]  DEFAULT ((0)) FOR [JOB_SHEET_COUNT_COLOR]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_JOB_SHEET_COUNT_BW]  DEFAULT ((0)) FOR [JOB_SHEET_COUNT_BW]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_PRICE_COLOR]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_PRICE_COLOR]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_PRICE_COLOR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_JOB_PRICE_COLOR]  DEFAULT ((0)) FOR [JOB_PRICE_COLOR]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_JOB_PRICE_BW]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_JOB_PRICE_BW]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_JOB_PRICE_BW]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_JOB_PRICE_BW]  DEFAULT ((0)) FOR [JOB_PRICE_BW]
END


End
GO
/****** Object:  Default [DF_T_JOB_LOG_REC_DATE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_LOG_REC_DATE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_LOG_REC_DATE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF_T_JOB_LOG_REC_DATE]  DEFAULT (getdate()) FOR [REC_DATE]
END


End
GO
/****** Object:  Default [DF__T_JOB_LOG__JOB_U__0CA5D9DE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__T_JOB_LOG__JOB_U__0CA5D9DE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_LOG]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__T_JOB_LOG__JOB_U__0CA5D9DE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_LOG] ADD  CONSTRAINT [DF__T_JOB_LOG__JOB_U__0CA5D9DE]  DEFAULT ((0)) FOR [JOB_USED_UPDATED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_JOB_ISALLOWED]  DEFAULT ((0)) FOR [JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_JOB_ISALLOWED]  DEFAULT ((0)) FOR [JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF__T_JOB_PER__COSTC__603D47BB]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__T_JOB_PER__COSTC__603D47BB]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__T_JOB_PER__COSTC__603D47BB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF__T_JOB_PER__COSTC__603D47BB]  DEFAULT ((-1)) FOR [COSTCENTER_ID]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_AUTOREFILL_TEMP_JOB_ISALLOWED]  DEFAULT ((0)) FOR [JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_LIMIT]  DEFAULT ((0)) FOR [JOB_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_USED]  DEFAULT ((0)) FOR [JOB_USED]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALERT_LIMIT]  DEFAULT ((0)) FOR [ALERT_LIMIT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_ALLOWED_OVER_DRAFT]  DEFAULT ((0)) FOR [ALLOWED_OVER_DRAFT]
END


End
GO
/****** Object:  Default [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_PERMISSIONS_LIMITS_TEMP] ADD  CONSTRAINT [DF_T_JOB_PERMISSIONS_LIMITS_TEMP_JOB_ISALLOWED]  DEFAULT ((0)) FOR [JOB_ISALLOWED]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_JOB_SIZE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] ADD  CONSTRAINT [DF_T_JOB_TRANSMITTER_JOB_SIZE]  DEFAULT ((0)) FOR [JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] ADD  CONSTRAINT [DF_T_JOB_TRANSMITTER_JOB_TRMSN_DURATION]  DEFAULT ((0)) FOR [JOB_TRMSN_DURATION]
END


End
GO
/****** Object:  Default [DF_T_JOB_TRANSMITTER_LISTNER_PORT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_TRANSMITTER_LISTNER_PORT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_TRANSMITTER]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_TRANSMITTER_LISTNER_PORT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_TRANSMITTER] ADD  CONSTRAINT [DF_T_JOB_TRANSMITTER_LISTNER_PORT]  DEFAULT ((0)) FOR [LISTNER_PORT]
END


End
GO
/****** Object:  Default [DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_JOB_USAGE_PAPERSIZE]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_JOB_USAGE_PAPERSIZE] ADD  CONSTRAINT [DF_T_JOB_USAGE_PAPERSIZE_JOB_SHEET_COUNT]  DEFAULT ((0)) FOR [JOB_SHEET_COUNT]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_SIZE]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_SIZE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_SIZE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_SIZE]  DEFAULT ((0)) FOR [JOB_SIZE]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_CHANGED_SETTINGS]  DEFAULT ((0)) FOR [JOB_CHANGED_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_RELEASE_WITH_SETTINGS]  DEFAULT ((0)) FOR [JOB_RELEASE_WITH_SETTINGS]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_PRINT_RELEASED]  DEFAULT ((0)) FOR [JOB_PRINT_RELEASED]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_DELETE_AFTER_PRINT]  DEFAULT ((0)) FOR [DELETE_AFTER_PRINT]
END


End
GO
/****** Object:  Default [DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_PRINT_JOBS] ADD  CONSTRAINT [DF_T_PRINT_JOBS_JOB_RELEASE_NOTIFY]  DEFAULT ((0)) FOR [JOB_RELEASE_NOTIFY]
END


End
GO
/****** Object:  Default [DF_T_SERVICE_MONITOR_SRVC_TIME]    Script Date: 06/06/2012 13:34:33 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_SERVICE_MONITOR_SRVC_TIME]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_SERVICE_MONITOR]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_SERVICE_MONITOR_SRVC_TIME]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_SERVICE_MONITOR] ADD  CONSTRAINT [DF_T_SERVICE_MONITOR_SRVC_TIME]  DEFAULT (getdate()) FOR [SRVC_TIME]
END


End

GO
/****** Object:  Table [dbo].[T_MY_ACCOUNT]    Script Date: 07/11/2014 10:36:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_MY_ACCOUNT]') AND type in (N'U'))
DROP TABLE [dbo].[T_MY_ACCOUNT]
GO
/****** Object:  Table [dbo].[T_MY_ACCOUNT]    Script Date: 07/11/2014 10:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_MY_ACCOUNT]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_MY_ACCOUNT](
	[REC_NUMBER] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ACC_USR_ID] [nvarchar](10) NULL,
	[ACC_USER_NAME] [nchar](100) NULL,
	[ACC_AMOUNT] [nvarchar](500) NULL,
	[JOB_TYPE] [nchar](50) NULL,
	[JOB_LOG_ID] [varchar](50) NULL,
	[REMARKS] [nvarchar](50) NULL,
	[ACC_DATE] [datetime] NULL,
	[REC_USER] [nvarchar](50) NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[RECHARGE_ID] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[T_CURRENCY_SETTING]    Script Date: 6/16/2014 3:48:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_CURRENCY_SETTING]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_CURRENCY_SETTING](
	[REC_SLNO] [int] IDENTITY(1,1) NOT NULL,
	[CUR_SYM_TYPE] [nvarchar](50) NULL,
	[CUR_SYM_TXT] [ntext] NULL,
	[CUR_SYM_IMG] [nvarchar](max) NULL,
	[CUR_APPEND]  nvarchar(3) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[T_MY_BALANCE]    Script Date: 06/24/2014 16:19:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_MY_BALANCE]') AND type in (N'U'))
DROP TABLE [dbo].[T_MY_BALANCE]
GO
/****** Object:  Default [DF_T_MY_BALANCE_IS_RECHARGE]    Script Date: 06/24/2014 16:19:29 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_MY_BALANCE_IS_RECHARGE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_MY_BALANCE]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_MY_BALANCE_IS_RECHARGE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_MY_BALANCE] DROP CONSTRAINT [DF_T_MY_BALANCE_IS_RECHARGE]
END


End
GO
/****** Object:  Table [dbo].[T_MY_BALANCE]    Script Date: 06/24/2014 16:19:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_MY_BALANCE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_MY_BALANCE](
	[REC_SLNO] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RECHARGE_ID] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IS_RECHARGE] [bit] NOT NULL,
	[USER_ID] [nvarchar](100) NULL,
	[AMOUNT] [nvarchar](500) NULL,
	[RECHARGE_DEVICE] [nvarchar](50) NULL,
	[RECHARGE_BY] [nvarchar](50) NULL,
	[REFERENCE_NO] [nvarchar](50) NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[NOTE] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Default [DF_T_MY_BALANCE_IS_RECHARGE]    Script Date: 06/24/2014 16:19:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_T_MY_BALANCE_IS_RECHARGE]') AND parent_object_id = OBJECT_ID(N'[dbo].[T_MY_BALANCE]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_T_MY_BALANCE_IS_RECHARGE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[T_MY_BALANCE] ADD  CONSTRAINT [DF_T_MY_BALANCE_IS_RECHARGE]  DEFAULT ((0)) FOR [IS_RECHARGE]
END


End
GO




/****** Object:  Table [dbo].[MFP_CLICK]    Script Date: 2/18/2015 3:27:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFP_CLICK]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MFP_CLICK](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[MAC_ADDRESS] [nvarchar](200) NULL,
	[MODEL_NAME] [nvarchar](200) NULL,
	[SERIAL_NUMBER] [nvarchar](50) NULL,
	[PRINT_TOTAL] [int] NULL CONSTRAINT [DF_MFP_CLICK_PRINT_TOTAL]  DEFAULT ((0)),
	[PRINT_COLOR] [int] NULL CONSTRAINT [DF_MFP_CLICK_PRINT_COLOR]  DEFAULT ((0)),
	[PRINT_BW] [int] NULL CONSTRAINT [DF_MFP_CLICK_PRINT_BW]  DEFAULT ((0)),
	[DUPLEX] [int] NULL CONSTRAINT [DF_MFP_CLICK_DUPLEX]  DEFAULT ((0)),
	[COPIES] [int] NULL CONSTRAINT [DF_MFP_CLICK_COPIES]  DEFAULT ((0)),
	[COPY_BW] [int] NULL CONSTRAINT [DF_MFP_CLICK_COPY_BW]  DEFAULT ((0)),
	[COPY_COLOR] [int] NULL CONSTRAINT [DF_MFP_CLICK_COPY_COLOR]  DEFAULT ((0)),
	[TWO_COLOR_COPY_COUNT] [int] NULL CONSTRAINT [DF_MFP_CLICK_TWO_COLOR_COPY_COUNT]  DEFAULT ((0)),
	[SINGLE_COLOR_COPY_COUNT] [int] NULL CONSTRAINT [DF_MFP_CLICK_SINGLE_COLOR_COPY_COUNT]  DEFAULT ((0)),
	[BW_TOTAL_COUNT] [int] NULL CONSTRAINT [DF_MFP_CLICK_BW_TOTAL_COUNT]  DEFAULT ((0)),
	[FULL_COLOR_TOTAL_COUNT] [int] NULL CONSTRAINT [DF_MFP_CLICK_FULL_COLOR_TOTAL_COUNT]  DEFAULT ((0)),
	[TWO_COLOR_TOTAL_COUNT] [int] NULL CONSTRAINT [DF_MFP_CLICK_TWO_COLOR_TOTAL_COUNT]  DEFAULT ((0)),
	[SINGLE_COLOR_TOTAL_COUNT] [int] NULL CONSTRAINT [DF_MFP_CLICK_SINGLE_COLOR_TOTAL_COUNT]  DEFAULT ((0)),
	[BW_OTHER_COUNT] [int] NULL CONSTRAINT [DF_MFP_CLICK_BW_OTHER_COUNT]  DEFAULT ((0)),
	[FULL_COLOR_OTHER_COUNT] [int] NULL CONSTRAINT [DF_MFP_CLICK_FULL_COLOR_OTHER_COUNT]  DEFAULT ((0)),
	[SCAN_TO_HDD] [int] NULL CONSTRAINT [DF_MFP_CLICK_SCAN_TO_HDD]  DEFAULT ((0)),
	[BW_SCAN_TO_HDD] [int] NULL CONSTRAINT [DF_MFP_CLICK_BW_SCAN_TO_HDD]  DEFAULT ((0)),
	[COLOR_SCAN_TO_HDD] [int] NULL CONSTRAINT [DF_MFP_CLICK_COLOR_SCAN_TO_HDD]  DEFAULT ((0)),
	[TWO_COLOR_SCAN_HDD] [int] NULL,
	[TOTAL_DOC_FILING_PRINT] [int] NULL CONSTRAINT [DF_MFP_CLICK_TOTAL_DOC_FILING_PRINT]  DEFAULT ((0)),
	[BW_DOC_FILING_PRINT] [int] NULL CONSTRAINT [DF_MFP_CLICK_BW_DOC_FILING_PRINT]  DEFAULT ((0)),
	[COLOR_DOC_FILING_PRINT] [int] NULL CONSTRAINT [DF_MFP_CLICK_COLOR_DOC_FILING_PRINT]  DEFAULT ((0)),
	[TWO_COLOR_DOC_FILING_PRINT] [int] NULL CONSTRAINT [DF_MFP_CLICK_TWO_COLOR_DOC_FILING_PRINT]  DEFAULT ((0)),
	[DOCUMENT_FEEDER] [int] NULL CONSTRAINT [DF_MFP_CLICK_DOCUMENT_FEEDER]  DEFAULT ((0)),
	[TOTAL_SCAN_TO_EMAIL_FTP] [int] NULL CONSTRAINT [DF_MFP_CLICK_TOTAL_SCAN_TO_EMAIL_FTP]  DEFAULT ((0)),
	[BW_SCAN] [int] NULL CONSTRAINT [DF_MFP_CLICK_BW_SCAN]  DEFAULT ((0)),
	[COLOR_SCAN] [int] NULL CONSTRAINT [DF_MFP_CLICK_COLOR_SCAN]  DEFAULT ((0)),
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
	[FAX_SEND] [int] NULL,
	[FAX_RECEIVE] [int] NULL,
	[IFAX_SEND_COUNT] [int] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MFP_PAPER]    Script Date: 2/18/2015 3:27:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFP_PAPER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MFP_PAPER](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[MAC_ADDRESS] [nvarchar](200) NULL,
	[MODEL_NAME] [nvarchar](200) NULL,
	[SERIAL_NUMBER] [nvarchar](200) NULL,
	[TRAY1] [int] NULL CONSTRAINT [DF_MFP_PAPER_TRAY1]  DEFAULT ((0)),
	[TRAY2] [int] NULL CONSTRAINT [DF_MFP_PAPER_TRAY2]  DEFAULT ((0)),
	[TRAY3] [int] NULL CONSTRAINT [DF_MFP_PAPER_TRAY3]  DEFAULT ((0)),
	[TRAY4] [int] NULL CONSTRAINT [DF_MFP_PAPER_TRAY4]  DEFAULT ((0)),
	[TRAY5] [int] NULL CONSTRAINT [DF_MFP_PAPER_TRAY5]  DEFAULT ((0)),
	[TRAY6] [int] NULL CONSTRAINT [DF_MFP_PAPER_TRAY6]  DEFAULT ((0)),
	[TRAY7] [int] NULL CONSTRAINT [DF_MFP_PAPER_TRAY7]  DEFAULT ((0)),
	[TRAY8] [int] NULL CONSTRAINT [DF_MFP_PAPER_TRAY8]  DEFAULT ((0)),
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MFP_TONER]    Script Date: 2/18/2015 3:27:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFP_TONER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MFP_TONER](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[MODEL_NAME] [nvarchar](200) NULL,
	[MAC_ADDRESS] [nvarchar](200) NULL,
	[SERIAL_NUMBER] [nvarchar](200) NULL,
	[CYAN] [nvarchar](50) NULL,
	[YELLOW] [nvarchar](50) NULL,
	[MAGENTA] [nvarchar](50) NULL,
	[BLACK] [nvarchar](50) NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[M_COUNTER_SUBSCRIPTION]    Script Date: 2/18/2015 3:27:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_COUNTER_SUBSCRIPTION]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_COUNTER_SUBSCRIPTION](
	[REC_ID] [int] IDENTITY(1,1) NOT NULL,
	[SUB_CID] [nvarchar](50) NOT NULL,
	[SUB_TOKEN1] [nvarchar](50) NULL,
	[SUB_TOKEN2] [nvarchar](50) NULL,
	[SUB_TOKEN3] [nvarchar](50) NULL,
	[SUB_TOKEN4] [nvarchar](50) NULL,
	[SUB_TOKEN5] [nvarchar](50) NULL,
	[REC_CDATE] [datetime] NULL,
	[REC_MDATE] [datetime] NULL,
 CONSTRAINT [PK_M_COUNTER_SUBSCRIPTION] PRIMARY KEY CLUSTERED 
(
	[SUB_CID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[T_PRINT_JOBS_SF]    Script Date: 2/18/2015 3:27:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_PRINT_JOBS_SF]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_PRINT_JOBS_SF](
	[REC_SYSID] [bigint] IDENTITY(1,1) NOT NULL,
	[JOB_RELEASER_ASSIGNED] [nvarchar](50) NULL,
	[USER_SOURCE] [varchar](50) NULL,
	[USER_ID] [varchar](50) NULL,
	[JOB_ID] [nvarchar](300) NULL,
	[JOB_FILE] [nvarchar](300) NULL,
	[JOB_SIZE] [bigint] NULL CONSTRAINT [DF_T_PRINT_JOBS_SF_JOB_SIZE]  DEFAULT ((0)),
	[JOB_SETTINGS_ORIGINAL] [ntext] NULL,
	[JOB_SETTINGS_REQUEST] [ntext] NULL,
	[JOB_CHANGED_SETTINGS] [bit] NULL CONSTRAINT [DF_T_PRINT_JOBS_SF_JOB_CHANGED_SETTINGS]  DEFAULT ((0)),
	[JOB_RELEASE_WITH_SETTINGS] [bit] NULL CONSTRAINT [DF_T_PRINT_JOBS_SF_JOB_RELEASE_WITH_SETTINGS]  DEFAULT ((0)),
	[JOB_FTP_PATH] [varchar](500) NULL,
	[JOB_FTP_ID] [nvarchar](50) NULL,
	[JOB_FTP_PASSWORD] [nvarchar](50) NULL,
	[JOB_PRINT_RELEASED] [bit] NULL CONSTRAINT [DF_T_PRINT_JOBS_SF_JOB_PRINT_RELEASED]  DEFAULT ((0)),
	[DELETE_AFTER_PRINT] [bit] NULL CONSTRAINT [DF_T_PRINT_JOBS_SF_DELETE_AFTER_PRINT]  DEFAULT ((0)),
	[JOB_RELEASE_NOTIFY] [bit] NULL CONSTRAINT [DF_T_PRINT_JOBS_SF_JOB_RELEASE_NOTIFY]  DEFAULT ((0)),
	[JOB_RELEASE_NOTIFY_EMAIL] [varchar](max) NULL,
	[JOB_RELEASE_NOTIFY_SMS] [varchar](500) NULL,
	[JOB_RELEASE_REQUEST_FROM] [nvarchar](50) NULL,
	[REC_DATE] [datetime] NULL,
	[USER_DOMAIN] [nvarchar](200) NULL,
	[JOB_REQUEST_MFP] [nvarchar](50) NULL,
	[JOB_REQUEST_DATE] [datetime] NULL CONSTRAINT [DF_T_PRINT_JOBS_SF_JOB_REQUEST_DATE]  DEFAULT (getdate()),
	[JOB_PRINT_REQUEST_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK_T_PRINT_JOBS_SF] PRIMARY KEY CLUSTERED 
(
	[REC_SYSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[USER_MEMBER_OF]    Script Date: 2/18/2015 3:27:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USER_MEMBER_OF]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[USER_MEMBER_OF](
	[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
	[GROUP_NAME] [nvarchar](200) NULL,
	[GROUP_USER] [int] NULL,
	[REC_CDATE] [datetime] NULL CONSTRAINT [DF_USER_MEMBER_OF_REC_CDATE]  DEFAULT (getdate()),
	[REC_MDATE] [datetime] NULL CONSTRAINT [DF_USER_MEMBER_OF_REC_MDATE]  DEFAULT (getdate())
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[APP_NXT_SETTING]    Script Date: 7/29/2015 11:21:32 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_NXT_SETTING]') AND type in (N'U'))
DROP TABLE [dbo].[APP_NXT_SETTING]
GO
/****** Object:  Table [dbo].[APP_NXT_SETTING]    Script Date: 7/29/2015 11:21:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_NXT_SETTING]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[APP_NXT_SETTING](
	[COMMAND1] [nvarchar](500) NULL,
	[COMMAND2] [nvarchar](500) NULL,
	[COMMAND3] [nvarchar](500) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EMAIL_PERMISSION_LIMITS]    Script Date: 2/19/2016 2:41:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EMAIL_PERMISSION_LIMITS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EMAIL_PERMISSION_LIMITS](
	[JOB_TYPE] [varchar](30) NULL,
	[JOB_LIMIT] [bigint] NULL CONSTRAINT [DF_EMAIL_PERMISSION_LIMITS_JOB_LIMIT]  DEFAULT ((0)),
	[ALERT_LIMIT] [int] NULL CONSTRAINT [DF_EMAIL_PERMISSION_LIMITS_ALERT_LIMIT]  DEFAULT ((0)),
	[ALLOWED_OVER_DRAFT] [int] NULL CONSTRAINT [DF_EMAIL_PERMISSION_LIMITS_ALLOWED_OVER_DRAFT]  DEFAULT ((0)),
	[JOB_ISALLOWED] [bit] NOT NULL CONSTRAINT [DF_EMAIL_PERMISSION_LIMITS_JOB_ISALLOWED]  DEFAULT ((0))
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMAIL_PRINT_SETTINGS]    Script Date: 2/19/2016 2:41:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EMAIL_PRINT_SETTINGS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EMAIL_PRINT_SETTINGS](
	[SLNO] [int] NOT NULL,
	[EMAILSETTING_KEY] [nvarchar](100) NOT NULL,
	[EMAILSETTING_VALUE] [nvarchar](max) NULL,
	[EMAILSETTING_DISCRIPTION] [varchar](200) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
