/*
 Navicat Premium Data Transfer

 Source Server         : SQL
 Source Server Type    : SQL Server
 Source Server Version : 16001000 (16.00.1000)
 Source Host           : TDF41:1433
 Source Catalog        : Users
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16001000 (16.00.1000)
 File Encoding         : 65001

 Date: 19/12/2024 17:52:09
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
INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'2', N'Hala Ibrahim El Sayed Saleh', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'3', N'Loay Sherif Hussein Hosny', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'4', N'Selwan Awad Mohamed Ali Eleiwa', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'5', N'Sara Nashaat Nashed', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'6', N'Shrouk Ashraf Abdel Fattah Yahia', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'7', N'Nada Ahmed Anwar Hussein', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'8', N'Yara Amr Abdel Hamid Zaki Dash', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'9', N'Yara Osman Khaled Mohamed Sayed ElMalky', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'10', N'Nouran Mohamed El sayed Saad Saad Montaser', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'11', N'Rana Hossam el Din Mostafa Khalifa', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'12', N'Ahmed Kilany', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'13', N'Karim Fawzy', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'14', N'Jana Eleraky', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'15', N'Nour Mohamed El sayed Hafez Malek', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'16', N'Andrew Amir Afif Foud Metry', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'17', N'Madonna Fekry Khalil', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'18', N'Sameh Osama Hussein Awad', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'19', N'Mohamed Ali Abdel Gelil Selim', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'20', N'Mahmoud Mohamed Rashad Mahmoud', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'21', N'Hisham Mohamed Helmy', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'22', N'Youssef Samir Ahmed Mohamed', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'23', N'Eissa Mohamed Mahmoud Eldafrawy', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'24', N'Emad El deen Amer Mohamed', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'25', N'Nagah Mohamed Zaki Abdel Aal', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'26', N'Mousa Hussein Hassan', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'27', N'Mohamed Hamada Ahmed Masoud', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'28', N'Walid Mohamed Ibrahim Mohamed', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'29', N'Menna Tallah Ani El Sayed Shehata Mohamed', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'30', N'Hana Abdelaziz', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'31', N'Nada Sharaf Hamed El Sherbiny', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'32', N'Nadeen abdel Wahab', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'33', N'Nourhan Ahmed', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'34', N'Yousra Amr Mahmoud Atallah', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'35', N'Aya Magdy Abdel Naby Shoeib', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'36', N'Habiba Salama', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'37', N'Alia Mostafa', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'38', N'Abdelrahman Hafez Saad Hafez', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'39', N'Sherif Ashraf El Mohamady Abdo', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'40', N'Omar hossam eldin hafez', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'41', N'Nader Talaat', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'42', N'Micheal Medhat Mounir Habib', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'43', N'Amira Radwan Ghobashy Aly', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'44', N'Mohamed Mahmoud Zaghloul', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'45', N'Reem Ali Kamal Farid Abdel Gawad', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'46', N'Nourhan Salem', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'47', N'Radwa Mohamed', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'48', N'Marina Romany', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'49', N'Mark Morgan', N'14', N'7', N'0', N'0')
GO

INSERT INTO [dbo].[AnnualLeave] ([UserID], [FullName], [Annual], [CasualLeave], [AnnualUsed], [CasualUsed]) VALUES (N'50', N'Joeseph Ayad', N'14', N'7', N'0', N'0')
GO

