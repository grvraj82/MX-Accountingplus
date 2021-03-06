USE [AccountingPlusDB]
GO
declare @BuildVersion varchar(50)
declare @TableCount int
select @TableCount=count(*)  from sys.tables where [name]='APP_VERSION'
	if(@TableCount=0)
	begin
	  set @BuildVersion = '1.1.440.0'
	end
	else
	  begin
		select @BuildVersion = VERSION from APP_VERSION
	  end

 if(SUBSTRING(@BuildVersion, 5, 3) >= 586 and SUBSTRING(@BuildVersion, 5, 3) <=699)

BEGIN
	/* 750  */
		ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_ID nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_HOST nvarchar(50) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_PORT nvarchar(5) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_USERNAME nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_PASSWORD nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_REQUIRE_SSL bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD EMAIL_DIRECT_PRINT bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD OSA_IC_CARD bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD [MFP_COMMAND1] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_COMMAND2] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_NOTES] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_R_DATE] [datetime] NULL
		ALTER TABLE [T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]  ADD [LAST_REFILLED_ON] [datetime] NULL		
		ALTER TABLE M_MFPS  ADD [MFP_ASP_SESSION] nvarchar(200) NULL
		ALTER TABLE M_MFPS  ADD [MFP_UI_SESSION] nvarchar(200) NULL
		ALTER TABLE M_MFPS  ADD [MFP_LAST_UPDATE] [datetime] NULL
		ALTER TABLE M_MFPS  ADD [MFP_STATUS] bit NULL DEFAULT ((0))

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

     if not exists(select * from sys.columns 
            where Name = N'JOB_REQUEST_MFP' and Object_ID = Object_ID(N'T_PRINT_JOBS'))    
begin
		ALTER TABLE T_PRINT_JOBS ADD JOB_REQUEST_MFP  nvarchar(50) NULL
	    ALTER TABLE T_PRINT_JOBS ADD JOB_REQUEST_DATE datetime NULL
end
	if not exists(select * from sys.columns 
            where Name = N'JOB_REQUEST_MFP' and Object_ID = Object_ID(N'T_PRINT_JOBS')) 
	begin
	ALTER TABLE M_USERS ADD USR_MY_ACCOUNT bit NULL Default ((1))
	end
	ALTER TABLE T_ACCESS_RIGHTS ADD REC_STATUS bit NULL Default ((1))

    ALTER TABLE M_MFPS  ADD EMAIL_MESSAGE_COUNT  varchar(50) NULL 
    ALTER TABLE M_SMTP_SETTINGS ADD REQUIRE_SSL bit NULL DEFAULT ((0))	
	ALTER TABLE [M_COST_CENTERS]  ADD [USR_SOURCE] [varchar](2) NULL
	ALTER TABLE [T_JOB_LOG]  ADD [JOB_JOB_NAME] [nvarchar](255) NULL
	
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

	
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [dbo].[LOG_CATEGORIES](
		[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
		[LOG_TYPE] [nvarchar](50) NULL,
		[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
	) ON [PRIMARY]
	END
END

if(SUBSTRING(@BuildVersion, 5, 3) >= 753 and SUBSTRING(@BuildVersion, 5, 3) <=760 )

BEGIN
		ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_ID nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_HOST nvarchar(50) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_PORT nvarchar(5) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_USERNAME nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_PASSWORD nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_REQUIRE_SSL bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD EMAIL_DIRECT_PRINT bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD OSA_IC_CARD bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD [MFP_COMMAND1] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_COMMAND2] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_NOTES] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_R_DATE] [datetime] NULL		
		ALTER TABLE M_MFPS  ADD [MFP_ASP_SESSION] nvarchar(200) NULL
		ALTER TABLE M_MFPS  ADD [MFP_UI_SESSION] nvarchar(200) NULL
		ALTER TABLE M_MFPS  ADD [MFP_LAST_UPDATE] [datetime] NULL
		ALTER TABLE M_MFPS  ADD [MFP_STATUS] bit NULL DEFAULT ((0))

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

	if not exists(select * from sys.columns 
            where Name = N'JOB_REQUEST_MFP' and Object_ID = Object_ID(N'T_PRINT_JOBS'))    
begin
		ALTER TABLE T_PRINT_JOBS ADD JOB_REQUEST_MFP  nvarchar(50) NULL
	    ALTER TABLE T_PRINT_JOBS ADD JOB_REQUEST_DATE datetime NULL
end
    ALTER TABLE M_MFPS  ADD EMAIL_MESSAGE_COUNT  varchar(50) NULL 
    ALTER TABLE M_SMTP_SETTINGS ADD REQUIRE_SSL bit NULL DEFAULT ((0))
	ALTER TABLE [T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]  ADD [LAST_REFILLED_ON] [datetime] NULL
	ALTER TABLE [M_COST_CENTERS]  ADD [USR_SOURCE] [varchar](2) NULL
	ALTER TABLE [T_JOB_LOG]  ADD [JOB_JOB_NAME] [nvarchar](255) NULL
	
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

	
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [dbo].[LOG_CATEGORIES](
		[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
		[LOG_TYPE] [nvarchar](50) NULL,
		[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
	) ON [PRIMARY]
	END
END


if(SUBSTRING(@BuildVersion, 5, 3) >= 761 and SUBSTRING(@BuildVersion, 5, 3) <=814 )

BEGIN
		print 'Build 793'
		ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_ID nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_HOST nvarchar(50) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_PORT nvarchar(5) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_USERNAME nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_PASSWORD nvarchar(100) NULL
		ALTER TABLE M_MFPS  ADD EMAIL_REQUIRE_SSL bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD EMAIL_DIRECT_PRINT bit NULL DEFAULT ((0))
        ALTER TABLE M_MFPS  ADD EMAIL_MESSAGE_COUNT  varchar(50) NULL 
        ALTER TABLE M_SMTP_SETTINGS ADD REQUIRE_SSL bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD OSA_IC_CARD bit NULL DEFAULT ((0))
		ALTER TABLE M_MFPS  ADD [MFP_COMMAND1] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_COMMAND2] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_NOTES] [nvarchar](max) NULL
		ALTER TABLE M_MFPS  ADD [MFP_R_DATE] [datetime] NULL
		ALTER TABLE [T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]  ADD [LAST_REFILLED_ON] [datetime] NULL		
		ALTER TABLE M_MFPS  ADD [MFP_ASP_SESSION] nvarchar(200) NULL
		ALTER TABLE M_MFPS  ADD [MFP_UI_SESSION] nvarchar(200) NULL
		ALTER TABLE M_MFPS  ADD [MFP_LAST_UPDATE] [datetime] NULL
		ALTER TABLE M_MFPS  ADD [MFP_STATUS] bit NULL DEFAULT ((0))

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
ALTER TABLE [M_COST_CENTERS]  ADD [USR_SOURCE] [varchar](2) NULL
	ALTER TABLE [T_JOB_LOG]  ADD [JOB_JOB_NAME] [nvarchar](255) NULL
	
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

	
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [dbo].[LOG_CATEGORIES](
		[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
		[LOG_TYPE] [nvarchar](50) NULL,
		[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
	) ON [PRIMARY]
	END
END

if(SUBSTRING(@BuildVersion, 5, 3) >= 815 and SUBSTRING(@BuildVersion, 5, 3) <=864 )

BEGIN
print 'Build 815'
	ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL
	ALTER TABLE M_MFPS  ADD EMAIL_MESSAGE_COUNT  varchar(50) NULL 
	ALTER TABLE M_MFPS  ADD [MFP_COMMAND1] [nvarchar](max) NULL
	ALTER TABLE M_MFPS  ADD [MFP_COMMAND2] [nvarchar](max) NULL
	ALTER TABLE M_MFPS  ADD [MFP_NOTES] [nvarchar](max) NULL
	ALTER TABLE M_MFPS  ADD [MFP_R_DATE] [datetime] NULL
	ALTER TABLE M_MFPS  ADD OSA_IC_CARD bit NULL DEFAULT ((0))
	ALTER TABLE [T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]  ADD [LAST_REFILLED_ON] [datetime] NULL
	ALTER TABLE [M_COST_CENTERS]  ADD [USR_SOURCE] [varchar](2) NULL
	ALTER TABLE [T_JOB_LOG]  ADD [JOB_JOB_NAME] [nvarchar](255) NULL	
	ALTER TABLE M_MFPS  ADD [MFP_ASP_SESSION] nvarchar(200) NULL
	ALTER TABLE M_MFPS  ADD [MFP_UI_SESSION] nvarchar(200) NULL
	ALTER TABLE M_MFPS  ADD [MFP_LAST_UPDATE] [datetime] NULL
	ALTER TABLE M_MFPS  ADD [MFP_STATUS] bit NULL DEFAULT ((0))
	
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
	
	
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [dbo].[LOG_CATEGORIES](
		[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
		[LOG_TYPE] [nvarchar](50) NULL,
		[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
	) ON [PRIMARY]
	END
	
END
if(SUBSTRING(@BuildVersion, 5, 3) >= 865 and SUBSTRING(@BuildVersion, 5, 3) <=897 )

BEGIN

ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL
ALTER TABLE M_MFPS  ADD [MFP_COMMAND1] [nvarchar](max) NULL
ALTER TABLE M_MFPS  ADD [MFP_COMMAND2] [nvarchar](max) NULL
ALTER TABLE M_MFPS  ADD [MFP_NOTES] [nvarchar](max) NULL
ALTER TABLE M_MFPS  ADD [MFP_R_DATE] [datetime] NULL
ALTER TABLE [T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]  ADD [LAST_REFILLED_ON] [datetime] NULL
ALTER TABLE M_MFPS  ADD [MFP_ASP_SESSION] nvarchar(200) NULL
ALTER TABLE M_MFPS  ADD [MFP_UI_SESSION] nvarchar(200) NULL
ALTER TABLE M_MFPS  ADD [MFP_LAST_UPDATE] [datetime] NULL
ALTER TABLE M_MFPS  ADD [MFP_STATUS] bit NULL DEFAULT ((0))

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
	ALTER TABLE [M_COST_CENTERS]  ADD [USR_SOURCE] [varchar](2) NULL
	ALTER TABLE [T_JOB_LOG]  ADD [JOB_JOB_NAME] [nvarchar](255) NULL
	
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
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [dbo].[LOG_CATEGORIES](
		[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
		[LOG_TYPE] [nvarchar](50) NULL,
		[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
	) ON [PRIMARY]
	END

		
END

if(SUBSTRING(@BuildVersion, 5, 3) >= 898 and SUBSTRING(@BuildVersion, 5, 3) <=906 )

BEGIN
	ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL
	ALTER TABLE [T_JOB_PERMISSIONS_LIMITS_AUTOREFILL]  ADD [LAST_REFILLED_ON] [datetime] NULL
	ALTER TABLE [M_COST_CENTERS]  ADD [USR_SOURCE] [varchar](2) NULL
	ALTER TABLE [T_JOB_LOG]  ADD [JOB_JOB_NAME] [nvarchar](255) NULL	
	ALTER TABLE M_MFPS  ADD [MFP_ASP_SESSION] nvarchar(200) NULL
	ALTER TABLE M_MFPS  ADD [MFP_UI_SESSION] nvarchar(200) NULL
	ALTER TABLE M_MFPS  ADD [MFP_LAST_UPDATE] [datetime] NULL
	ALTER TABLE M_MFPS  ADD [MFP_STATUS] bit NULL DEFAULT ((0))
	
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
	
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [dbo].[LOG_CATEGORIES](
		[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
		[LOG_TYPE] [nvarchar](50) NULL,
		[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
	) ON [PRIMARY]
	END

END

if(SUBSTRING(@BuildVersion, 5, 3) >= 907 and SUBSTRING(@BuildVersion, 5, 3) <=913 )

BEGIN
	ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL
	ALTER TABLE [M_COST_CENTERS]  ADD [USR_SOURCE] [varchar](2) NULL
	ALTER TABLE [T_JOB_LOG]  ADD [JOB_JOB_NAME] [nvarchar](255) NULL	
	ALTER TABLE M_MFPS  ADD [MFP_ASP_SESSION] nvarchar(200) NULL
	ALTER TABLE M_MFPS  ADD [MFP_UI_SESSION] nvarchar(200) NULL
	ALTER TABLE M_MFPS  ADD [MFP_LAST_UPDATE] [datetime] NULL
	ALTER TABLE M_MFPS  ADD [MFP_STATUS] bit NULL DEFAULT ((0))
	
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
	
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [dbo].[LOG_CATEGORIES](
		[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
		[LOG_TYPE] [nvarchar](50) NULL,
		[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
	) ON [PRIMARY]
	END
	
END

if(SUBSTRING(@BuildVersion, 5, 3) >= 914 and SUBSTRING(@BuildVersion, 5, 3) <=962 )

BEGIN
	
	ALTER TABLE [M_MFPS]  ADD [MFP_ASP_SESSION] [nvarchar](200) NULL
	ALTER TABLE [M_MFPS]  ADD [MFP_UI_SESSION] [nvarchar](200) NULL	
	ALTER TABLE M_MFPS  ADD [MFP_LAST_UPDATE] [datetime] NULL
	ALTER TABLE M_MFPS  ADD [MFP_STATUS] bit NULL DEFAULT ((0))	
	
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_CATEGORIES]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [dbo].[LOG_CATEGORIES](
		[REC_SYSID] [int] IDENTITY(1,1) NOT NULL,
		[LOG_TYPE] [nvarchar](50) NULL,
		[REC_STATUS] [bit] NULL CONSTRAINT [DF_LOG_CATEGORIES_REC_STATUS]  DEFAULT ((0))
	) ON [PRIMARY]
	END
	ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL


END

if(SUBSTRING(@BuildVersion, 5, 3) >= 963 and SUBSTRING(@BuildVersion, 5, 3) <=978 )
BEGIN
ALTER TABLE [M_USERS]  ADD [USER_COMMAND] [nvarchar](50) NULL	
END

if not exists(select * from sys.columns 
            where Name = N'USR_MY_ACCOUNT' and Object_ID = Object_ID(N'M_USERS')) 
begin
	ALTER TABLE M_USERS ADD USR_MY_ACCOUNT bit NULL Default ((1))
end

if not exists(select * from sys.columns 
            where Name = N'OSA_IC_CARD' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD OSA_IC_CARD bit NULL DEFAULT ((0))
end

if not exists(select * from sys.columns 
            where Name = N'NOTE' and Object_ID = Object_ID(N'T_JOB_LOG')) 
begin
	ALTER TABLE T_JOB_LOG  ADD NOTE ntext NULL 
end

if not exists(select * from sys.columns 
            where Name = N'EMAIL_MESSAGE_COUNT' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD EMAIL_MESSAGE_COUNT  varchar(50) NULL 
end

if not exists(select * from sys.columns 
            where Name = N'MFP_GUEST' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD MFP_GUEST  bit NULL 
end

if not exists(select * from sys.columns 
            where Name = N'LAST_PRINT_USER' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD LAST_PRINT_USER  nvarchar(100) NULL 
end
if not exists(select * from sys.columns 
            where Name = N'LAST_PRINT_TIME' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD LAST_PRINT_TIME  [datetime] NULL 
end

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_M_USERS_USR_MY_ACCOUNT]') AND type = 'D')
BEGIN
ALTER TABLE M_USERS DROP CONSTRAINT DF_M_USERS_USR_MY_ACCOUNT 
END

--ALTER TABLE M_USERS ADD CONSTRAINT DF_M_USERS_USR_MY_ACCOUNT DEFAULT NULL FOR USR_MY_ACCOUNT

if not exists(select * from sys.columns 
            where Name = N'AD_SYNC_USERS' and Object_ID = Object_ID(N'T_AD_SYNC')) 
begin
	ALTER TABLE T_AD_SYNC  ADD AD_SYNC_USERS  bit NULL 
end

if not exists(select * from sys.columns 
            where Name = N'AD_SYNC_COSTCENTER' and Object_ID = Object_ID(N'T_AD_SYNC')) 
begin
	ALTER TABLE T_AD_SYNC  ADD AD_SYNC_COSTCENTER  bit NULL 
end

if not exists(select * from sys.columns 
            where Name = N'AD_IMPORT_DEP_CC' and Object_ID = Object_ID(N'T_AD_SYNC')) 
begin
	ALTER TABLE T_AD_SYNC  ADD AD_IMPORT_DEP_CC  bit NULL 
end

DECLARE @ConstraintName VARCHAR(100)
SET @ConstraintName = (SELECT TOP 1 s.name FROM sys.sysobjects s JOIN sys.syscolumns c ON s.parent_obj=c.id
                        WHERE s.xtype='d' AND c.cdefault=s.id 
                        AND parent_obj = OBJECT_ID('T_AD_USERS') AND c.name ='DEPARTMENT')
 
IF @ConstraintName IS NOT NULL
BEGIN
print @ConstraintName
   EXEC('ALTER TABLE [dbo].[T_AD_USERS] DROP CONSTRAINT ' + @ConstraintName)
END


if exists(select * from sys.columns where Name = N'DEPARTMENT' and Object_ID = Object_ID(N'T_AD_USERS')) 
			Begin
				ALTER TABLE T_AD_USERS DROP COLUMN DEPARTMENT	
			END



if not exists(select * from sys.columns 
            where Name = N'DEPARTMENT' and Object_ID = Object_ID(N'T_AD_USERS')) 
begin
	ALTER TABLE T_AD_USERS  ADD DEPARTMENT  nvarchar(200) NULL 
end


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



if not exists(select * from sys.columns 
            where Name = N'JOB_BALANCE_UPdated' and Object_ID = Object_ID(N'T_JOB_LOG')) 
begin
	ALTER TABLE T_JOB_LOG  ADD JOB_BALANCE_UPdated  nvarchar(200) NULL 
end
if not exists(select * from sys.columns 
            where Name = N'JOB_BALANCE_UPdated' and Object_ID = Object_ID(N'T_JOB_LOG')) 
begin
	ALTER TABLE T_JOB_LOG  ADD JOB_BALANCE_UPdated  nchar(10) NULL 
end


if not exists(select * from sys.columns 
            where Name = N'DEPARTMENT' and Object_ID = Object_ID(N'T_JOB_LOG')) 
begin
	ALTER TABLE T_JOB_LOG  ADD DEPARTMENT  nvarchar(200) NULL 
end


if not exists(select * from sys.columns 
            where Name = N'COMPANY' and Object_ID = Object_ID(N'T_JOB_LOG')) 
begin
	ALTER TABLE T_JOB_LOG  ADD COMPANY  nvarchar(200) NULL 
end


if not exists(select * from sys.columns 
            where Name = N'LOCATION' and Object_ID = Object_ID(N'T_JOB_LOG')) 
begin
	ALTER TABLE T_JOB_LOG  ADD LOCATION  nvarchar(200) NULL 
end


if not exists(select * from sys.columns 
            where Name = N'MFPNAME' and Object_ID = Object_ID(N'T_JOB_LOG')) 
begin
	ALTER TABLE T_JOB_LOG  ADD MFPNAME  nvarchar(200) NULL 
end

if not exists(select * from sys.columns 
            where Name = N'JOB_PRINT_REQUEST_BY' and Object_ID = Object_ID(N'T_PRINT_JOBS')) 
begin
	ALTER TABLE T_PRINT_JOBS  ADD JOB_PRINT_REQUEST_BY  nvarchar(50) NULL 
end

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


if not exists(select * from sys.columns 
            where Name = N'CUR_APPEND' and Object_ID = Object_ID(N'T_CURRENCY_SETTING')) 
begin
	ALTER TABLE T_CURRENCY_SETTING  ADD CUR_APPEND  nvarchar(3) NULL 
end

if not exists(select * from sys.columns 
            where Name = N'MFP_GUEST' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD MFP_GUEST  bit NULL 
end

if not exists(select * from sys.columns 
            where Name = N'LAST_PRINT_USER' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD LAST_PRINT_USER  nvarchar(100) NULL 
end
if not exists(select * from sys.columns 
            where Name = N'LAST_PRINT_TIME' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD LAST_PRINT_TIME  [datetime] NULL 
end


if not exists(select * from sys.columns 
            where Name = N'DEPARTMENT' and Object_ID = Object_ID(N'M_USERS')) 
begin
	ALTER TABLE M_USERS  ADD DEPARTMENT  nvarchar(200) NULL 
end

if not exists(select * from sys.columns 
            where Name = N'COMPANY' and Object_ID = Object_ID(N'M_USERS')) 
begin
	ALTER TABLE M_USERS  ADD COMPANY  nvarchar(200) NULL 
end

if not exists(select * from sys.columns 
            where Name = N'MANAGER' and Object_ID = Object_ID(N'M_USERS')) 
begin
	ALTER TABLE M_USERS  ADD MANAGER  nvarchar(200) NULL 
end


if not exists(select * from sys.columns 
            where Name = N'AD_SYNC_USERS' and Object_ID = Object_ID(N'T_AD_SYNC')) 
begin
	ALTER TABLE T_AD_SYNC  ADD AD_SYNC_USERS  bit NULL
end

if not exists(select * from sys.columns 
            where Name = N'AD_SYNC_COSTCENTER' and Object_ID = Object_ID(N'T_AD_SYNC')) 
begin
	ALTER TABLE T_AD_SYNC  ADD AD_SYNC_COSTCENTER  bit NULL
end

if not exists(select * from sys.columns 
            where Name = N'AD_IMPORT_DEP_CC' and Object_ID = Object_ID(N'T_AD_SYNC')) 
begin
	ALTER TABLE T_AD_SYNC  ADD AD_IMPORT_DEP_CC  bit NULL
end



if not exists(select * from sys.columns 
            where Name = N'DEPARTMENT' and Object_ID = Object_ID(N'T_AD_USERS')) 
begin
	ALTER TABLE T_AD_USERS  ADD DEPARTMENT  nvarchar(200)
end


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Summary]') AND type in (N'U'))
DROP TABLE [dbo].[Summary]
GO
/****** Object:  Table [dbo].[M_APP]    Script Date: 26/11/2015 14:57:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_APP]') AND type in (N'U'))
DROP TABLE [dbo].[M_APP]
GO
/****** Object:  Table [dbo].[APP_NXT_SETTING]    Script Date: 26/11/2015 14:57:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_NXT_SETTING]') AND type in (N'U'))
DROP TABLE [dbo].[APP_NXT_SETTING]
GO
/****** Object:  Table [dbo].[APP_NXT_SETTING]    Script Date: 26/11/2015 14:57:20 ******/
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
/****** Object:  Table [dbo].[M_APP]    Script Date: 26/11/2015 14:57:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_APP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[M_APP](
	[REC_SLNO] [int] NULL,
	[COMMAND1] [nvarchar](200) NULL,
	[COMMAND2] [nvarchar](200) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Summary]    Script Date: 26/11/2015 14:57:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Summary]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Summary](
	[COST_CENTER_NAME] [nvarchar](100) NULL,
	[MFPNAME] [nvarchar](100) NULL,
	[JOB_USRNAME] [nvarchar](100) NULL,
	[Color] [int] NULL CONSTRAINT [DF_Summary_Color]  DEFAULT ((0)),
	[ColorPrice] [float] NULL,
	[BW] [int] NULL,
	[BWPrice] [float] NULL,
	[Copy] [int] NULL,
	[Print] [int] NULL,
	[Scan] [int] NULL,
	[Fax] [int] NULL,
	[Duplex] [int] NULL,
	[Total] [int] NULL,
	[Price] [float] NULL
) ON [PRIMARY]
END
GO
if not exists(select * from sys.columns 
            where Name = N'EMAIL_ID_ADMIN' and Object_ID = Object_ID(N'M_MFPS')) 
begin
	ALTER TABLE M_MFPS  ADD EMAIL_ID_ADMIN  [nvarchar](100) NULL
end



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

if not exists(select * from sys.columns 
            where Name = N'EXTERNAL_SOURCE' and Object_ID = Object_ID(N'M_USERS')) 
begin
	ALTER TABLE M_USERS  ADD EXTERNAL_SOURCE  [nvarchar](200) NULL
end

if not exists(select * from sys.columns 
            where Name = N'EXTERNAL_SOURCE' and Object_ID = Object_ID(N'T_JOB_LOG')) 
begin
	ALTER TABLE T_JOB_LOG  ADD EXTERNAL_SOURCE  [nvarchar](10) NULL
end