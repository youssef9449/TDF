/*
 Navicat Premium Dump SQL

 Source Server         : Server
 Source Server Type    : SQL Server
 Source Server Version : 16001000 (16.00.1000)
 Source Host           : TDF-SQL-SRV:1433
 Source Catalog        : Users
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16001000 (16.00.1000)
 File Encoding         : 65001

 Date: 25/12/2024 11:15:26
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
-- Records of AnnualLeave
-- ----------------------------
INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'1', N'Administrator', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'2', N'Mohamed Ali Selim', N'14', N'7', N'0', N'0')
GO


-- ----------------------------
-- Foreign Keys structure for table AnnualLeave
-- ----------------------------
ALTER TABLE [dbo].[AnnualLeave] ADD CONSTRAINT [FK_AnnualLeave_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

