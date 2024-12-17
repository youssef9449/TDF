/*
 Navicat Premium Data Transfer

 Source Server         : Connection
 Source Server Type    : SQL Server
 Source Server Version : 15002000 (15.00.2000)
 Source Host           : YOUSSEF-PC:1433
 Source Catalog        : Users
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000 (15.00.2000)
 File Encoding         : 65001

 Date: 15/12/2024 07:25:29
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
  [RequestDepartment] varchar(255) COLLATE Arabic_CI_AS  NOT NULL
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
ALTER TABLE [dbo].[Requests] ADD CONSTRAINT [FK__DayOffReq__UserI__412EB0B6] FOREIGN KEY ([RequestUserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

