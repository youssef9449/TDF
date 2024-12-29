/*
 Navicat Premium Dump SQL

 Source Server         : SQL
 Source Server Type    : SQL Server
 Source Server Version : 16001135 (16.00.1135)
 Source Host           : TDF41:1433
 Source Catalog        : Users
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16001135 (16.00.1135)
 File Encoding         : 65001

 Date: 24/12/2024 17:11:22
*/


-- ----------------------------
-- Table structure for Requests
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Requests]') AND type IN ('U'))
	DROP TABLE [dbo].[Requests]
GO

CREATE TABLE [dbo].[Requests] (
  [RequestID] int  IDENTITY(1,1) NOT NULL,
  [RequestUserID] int  NOT NULL,
  [RequestFromDay] date  NOT NULL,
  [RequestReason] nvarchar(255) COLLATE Arabic_CI_AS  NULL,
  [RequestStatus] nvarchar(50) COLLATE Arabic_CI_AS DEFAULT 'Pending' NULL,
  [RequestBeginningTime] time(7)  NULL,
  [RequestEndingTime] time(7)  NULL,
  [RequestType] varchar(255) COLLATE Arabic_CI_AS  NOT NULL,
  [RequestRejectReason] nvarchar(255) COLLATE Arabic_CI_AS  NULL,
  [RequestToDay] date  NULL,
  [RequestUserFullName] varchar(255) COLLATE Arabic_CI_AS  NOT NULL,
  [RequestCloser] varchar(255) COLLATE Arabic_CI_AS  NULL,
  [RequestDepartment] varchar(255) COLLATE Arabic_CI_AS  NOT NULL,
  [RequestNumberOfDays] int  NULL
)
GO

ALTER TABLE [dbo].[Requests] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for Requests
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Requests]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Requests
-- ----------------------------
ALTER TABLE [dbo].[Requests] ADD CONSTRAINT [PK__DayOffRe__33A8519A6AB6E96A] PRIMARY KEY CLUSTERED ([RequestID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table Requests
-- ----------------------------
ALTER TABLE [dbo].[Requests] ADD CONSTRAINT [FK__DayOffReq__UserI__412EB0B6] FOREIGN KEY ([RequestUserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

