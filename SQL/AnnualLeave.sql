/*
 Navicat Premium Data Transfer

 Source Server         : SQLEXPRESS
 Source Server Type    : SQL Server
 Source Server Version : 15002000 (15.00.2000)
 Source Host           : TDF40\SQLEXPRESS:1433
 Source Catalog        : Users
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000 (15.00.2000)
 File Encoding         : 65001

 Date: 18/12/2024 14:33:15
*/


-- ----------------------------
-- Table structure for AnnualLeave
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AnnualLeave]') AND type IN ('U'))
	DROP TABLE [dbo].[AnnualLeave]
GO

CREATE TABLE [dbo].[AnnualLeave] (
  [UserID] int  NOT NULL,
  [FullName] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Annual] int  NOT NULL,
  [Casual Leave
] int  NOT NULL
)
GO

ALTER TABLE [dbo].[AnnualLeave] SET (LOCK_ESCALATION = TABLE)
GO

