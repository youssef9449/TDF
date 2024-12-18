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

 Date: 16/12/2024 00:10:36
*/


-- ----------------------------
-- Table structure for Departments
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Departments]') AND type IN ('U'))
	DROP TABLE [dbo].[Departments]
GO

CREATE TABLE [dbo].[Departments] (
  [Department] nvarchar(255) COLLATE Arabic_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Departments] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Departments
-- ----------------------------
INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'3D')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Accounting')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Administration')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Administrative Affairs
')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Experience Team')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'FF&A Team')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Founder & CEO')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Human Resources')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Projects Management')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Schematic Design')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Site Supervision')
GO

INSERT INTO [dbo].[Departments] ([Department]) VALUES (N'Technical')
GO

