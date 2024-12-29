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

 Date: 21/12/2024 22:22:17
*/


-- ----------------------------
-- Table structure for AnnualLeave
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AnnualLeave]') AND type IN ('U'))
	DROP TABLE [dbo].[AnnualLeave]
GO

CREATE TABLE [dbo].[AnnualLeave] (
  [UserID] int  NULL,
  [FullName] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Annual] int  NULL,
  [CasualLeave] int  NULL,
  [AnnualUsed] int DEFAULT 0 NULL,
  [CasualUsed] int DEFAULT 0 NULL,
  [AnnualBalance] AS (isnull([Annual],(0))-isnull([AnnualUsed],(0))),
  [CasualBalance] AS (isnull([CasualLeave],(0))-isnull([CasualUsed],(0)))

)
GO

ALTER TABLE [dbo].[AnnualLeave] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Foreign Keys structure for table AnnualLeave
-- ----------------------------
ALTER TABLE [dbo].[AnnualLeave] ADD CONSTRAINT [FK_AnnualLeave_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

