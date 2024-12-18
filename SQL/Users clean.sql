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

 Date: 18/12/2024 10:06:46
*/


-- ----------------------------
-- Table structure for Users
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type IN ('U'))
	DROP TABLE [dbo].[Users]
GO

CREATE TABLE [dbo].[Users] (
  [UserID] int  IDENTITY(1,1) NOT NULL,
  [UserName] nvarchar(50) COLLATE Arabic_CI_AS  NOT NULL,
  [PasswordHash] nvarchar(256) COLLATE Arabic_CI_AS  NOT NULL,
  [Salt] nvarchar(128) COLLATE Arabic_CI_AS  NOT NULL,
  [FullName] nvarchar(100) COLLATE Arabic_CI_AS  NULL,
  [Role] nvarchar(50) COLLATE Arabic_CI_AS  NULL,
  [Picture] varbinary(max)  NULL,
  [Department] varchar(255) COLLATE Arabic_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[Users] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for Users
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Users]', RESEED, 1)
GO


-- ----------------------------
-- Uniques structure for table Users
-- ----------------------------
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ__Users__536C85E474AE94DB] UNIQUE NONCLUSTERED ([UserName] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Users
-- ----------------------------
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK__Users__1788CCACE81744E6] PRIMARY KEY CLUSTERED ([UserID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

