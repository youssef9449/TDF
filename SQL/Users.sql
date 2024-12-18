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

 Date: 18/12/2024 13:54:11
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
-- Records of Users
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Users] ON
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'1', N'admin', N'JDjjgbgE6E1o/em1BVgowIUiK9cdUTg8G/rGM5vPCK8=', N'hZnA+w+cbQTdRG4BfDmWXw==', N'Administrator', N'Admin', NULL, N'All')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'2', N'Hala.I', N'p5SzdwKoW1IV9AoOos0y+eyx4eZ+7sVp/655i5dAvgI=', N'2UjVOQzS4yKU3kLjdsIKXw==', N'Hala Ibrahim El Sayed Saleh', N'Admin', NULL, N'Founder')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'3', N'Loay.S', N'BsGrAJ5NfW3Ctrzbj0XxtgGG8qDvF2oqERN+bQWVniA=', N'Dbw1ijm7w9CBb4hL+rsf9w==', N'Loay Sherif Hussein Hosny', N'Admin', NULL, N'CEO')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'4', N'Selwan.A', N'YffLKsaGwc5mCRKMreERINysJuVIuvIEEgfZRYdwZAs=', N'lOEI6IY3s2QEJDU2j4Re2w==', N'Selwan Awad Mohamed Ali Eleiwa', N'Manager', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'5', N'Sara.N', N'oWVKNpeZkYgVJVknnuL8Ct7IVXIVC4L8Hlg9kKPQZXc=', N'lznqmbx5aKp0oPpKM54joA==', N'Sara Nashaat Nashed', N'Team Leader', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'6', N'Shrouk.A', N'9ueJxMrSWhyM14PQuV4Sy3rwHGLMU/WKQwjI3Uv6N7w=', N'u5FVyJjz5SlVpGlmnjnGPg==', N'Shrouk Ashraf Abdel Fattah Yahia', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'7', N'Nada.A', N'RwjTf87z4NTIcL5Xb8tsz2G1JepE8tiqtIQ2+ixBY/k=', N'R6yf2da19gQZ6vck+yqH+Q==', N'Nada Ahmed Anwar Hussein', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'8', N'Yara.A', N'cEYjbUipPdFf3eX2D88XJhc06cjhiZvdAU5i2sCNHac=', N'mD+2ap3fNoTJBZtV1ZCQuA==', N'Yara Amr Abdel Hamid Zaki Dash', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'9', N'Yara.O', N'lL+ARl4WdjGm9u+0LNQMSKY9WmLnsKWwfuMmnBWrVBU=', N'NFm84Kdq/NXvJ+w9ecqcfA==', N'Yara Osman Khaled Mohamed Sayed ElMalky', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'10', N'Nouran.M', N'CCGaolLbBYjZxOQoqMaIiwUiyqofQNudvvmQ6M02Y58=', N'T7sJibR7LmKBFf34SIu1YA==', N'Nouran Mohamed El sayed Saad Saad Montaser', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'11', N'Rana.H', N'HPwufx3Gq6SXDzn7fXvJNHWUxTCmcM+pSXQx5X9TGII=', N'Vv3cVT0ZjiPCC3WnIjSCqQ==', N'Rana Hossam el Din Mostafa Khalifa', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'12', N'Ahmed.K', N'wt3mKV62/hd7ItHm5SK9mGhYFK3jPbon6a/ShREwRBg=', N'6EOLTDJfGCQLsORJgz8xvQ==', N'Ahmed Kilany', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'13', N'Karim.F', N'jGR3Tib2ER7XLRwGYqCSJGH2WQPWyLB74alJ37X/eS8=', N'f1N616NYirca8mKweCuj/g==', N'Karim Fawzy', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'14', N'Jana.E', N'je8q0eID34UfDnL8vU2oN6BUa927/HEv+1JwvoJrg2Y=', N'UgC+RlucGEyCDw0aEy1mxA==', N'Jana Eleraky', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'15', N'Nour.M', N'B8roU+cIfh1yjbmblc4NbsMw7KABYeBZrfP3lHp/jik=', N'WOmRwaKq266M41g7P96fMQ==', N'Nour Mohamed El sayed Hafez Malek', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'16', N'Andrew.A', N'gYbvEGhMt6aU7EuURHhfIu68VjQayLuFofxo796pWQ4=', N'Lkt139da58VU8XuJmoOWBQ==', N'Andrew Amir Afif Foud Metry', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'17', N'Madonna.F', N't33LW+BkmRXMmEmq+zLHScxGtgm8B58neDEUjg7gv8g=', N'uzaolKS4XcJX5lC30tQD3Q==', N'Madonna Fekry Khalil', N'User', NULL, N'Schematic Design / 3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'18', N'Sameh.O', N'vhMOBJ1f3aNIIR0jsuT6q/cUqiFyM3pYv0ri6Yhf67M=', N'MHrV31DvhI548S6kDK4ymQ==', N'Sameh Osama Hussein Awad', N'Admin', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'19', N'Mohamed.A', N'pe1pYnPKnKwVMJZkm5XceTwXkESAL2imPQgmQz8+j4s=', N'RREHW/tjH0w9m4u20X8H0w==', N'Mohamed Ali Abdel Gelil Selim', N'User', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'20', N'Mahmoud.M', N'HmewV0DHM31gbMeuv/88JyM9J0fGaVfFMv4IG3NSn+4=', N'nKXbjbVklg1h6MRlVsn/sQ==', N'Mahmoud Mohamed Rashad Mahmoud', N'User', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'21', N'Hisham.M', N'8BwnkNSGAzGG6EgGgRxLWjMvCT0pI1WhFu/LmwNW/08=', N'mTJzORrwB//bPfIotrx+sg==', N'Hisham Mohamed Helmy', N'User', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'22', N'Youssef.S', N'9bMuhBZZ18QiuepyhvuTL2GhWljV57WV63muqVEeDLc=', N'8mMHLFL2P8Dix94oy0gVQA==', N'Youssef Samir Ahmed Mohamed', N'User', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'23', N'Eissa.M', N'AyKuppS52ZbBAjg+XzH2bPo1AZpcsc6uPl0zrAFZSYM=', N'si4FYnGiJuKZvu7azt71zw==', N'Eissa Mohamed Mahmoud Eldafrawy', N'Manager', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'24', N'Emad.E', N'EDrFbulNP2xGFX3nsPB7+Z7lRGMHkL0SLIg1bQYNl/8=', N'EuLDt45l/RNMtkxkMFvdEg==', N'Emad El deen Amer Mohamed', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'25', N'Nagah.M', N'JeYTO7UBcIHAi097aUwkbGsHIPSset7afbIbl/J8Mek=', N'KOR+X1kYjwKDzi97UJN43Q==', N'Nagah Mohamed Zaki Abdel Aal', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'26', N'Mousa.H', N'xi9Ar4fvsAYxBzEbLxsRUDPvHdFbcrLDsY1gxHrfjgY=', N'mbMc44Y42bau22pzLOKmLQ==', N'Mousa Hussein Hassan', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'27', N'Mohamed.H', N'QlM4bzDwKJq9VdRFpenxzIE47N9mbiqY/zgQV/BN/7w=', N'e1hEygBV1ho0CP2jDy8UAg==', N'Mohamed Hamada Ahmed Masoud', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'28', N'Walid.M', N'JDRYILZblu08Q6QEBD27XHtT+lPO+looka/uLJoH66w=', N'iKo+Z4LH1SrAQkC2e9e33g==', N'Walid Mohamed Ibrahim Mohamed', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'29', N'Menna.T', N'wAH2ptqMpkSO/MOlxC8FoZmpnuOa91ZtIUcgwNUFaKY=', N'RzQjWHUVbm0Q3zyruXnPfQ==', N'Menna Tallah Ani El Sayed Shehata Mohamed', N'Manager', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'30', N'Hana.Abdelaziz', N'scg5Ls3fY99Ovc6SVraGTrrUAzPKiHRd3EYUAdcIfGs=', N'm1KhvjajjZ3RvFB6dVWDTA==', N'Hana Abdelaziz', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'31', N'Nada.S', N'UbuZgZ8G9oZ0rWxk1M5Awo5k9ZYYUUAEmiTjWBRuLRY=', N'XUqBzkJN/gEX4ACqsBmzgA==', N'Nada Sharaf Hamed El Sherbiny', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'32', N'Nadeen.A', N'GRBGGQl4cwAVFobeMpnjPvq/MIwQz4rKFx9gIpavc3U=', N'FhvAtC1WD6Y7kd9jBkykZw==', N'Nadeen abdel Wahab', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'33', N'Nourhan.A', N'oRZoL/LxJ7pkBCiT8daNx/eASZQXCQu28hOjnTSChw0=', N'UOLvZOAAoPcbkYVoJkFaDQ==', N'Nourhan Ahmed', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'34', N'Yousra.A', N'qvhMZKmjqZO7rR9VigmlzgDtzNENym0BKmybAwQcJMQ=', N'a+AXTZLKZNz4h9Af2SiU6w==', N'Yousra Amr Mahmoud Atallah', N'Manager', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'35', N'Aya.M', N'5NCCrjwPDdhJ1ZotX+Ofzhf6NYXzmpqaHswyKTruq88=', N'FZYx0jqy8/Hzsuad4gdmqw==', N'Aya Magdy Abdel Naby Shoeib', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'36', N'Habiba.S', N'7U+BtouhvRYDHX0LnhsCil0n3s7ifwc3I9tUmeNZnlI=', N'fzTd6O6FV/9toZYTmVkZxg==', N'Habiba Salama', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'37', N'Alia.M', N'yB2dJ7Ds50HIXA4bt6qFn3WyztCd6TzuWh6iH9K2hvk=', N'gcpDe9Yfh8YGnx2Y7ASTSQ==', N'Alia Mostafa', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'38', N'Abdelrahman.H', N'CPDDyVPGXNlSWM8w71o3W4kBxd5RHtKgN5aJZ9H+UaA=', N'F763SvjtYiam9Hrj6Gb/PQ==', N'Abdelrahman Hafez Saad Hafez', N'Team Leader', NULL, N'Site Supervision')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'39', N'Sherif.A', N'VNiOqPjDZpIDE2qllJp9ujfcmOdD0+hC0+hymGGXnFY=', N'VVSMdiM1KZvl1MoIqkCybw==', N'Sherif Ashraf El Mohamady Abdo', N'User', NULL, N'Site Supervision')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'40', N'Omar.h', N'j5RjLFGmz9C0vW8s6EWkWo67+vswSnUE4+XDN6iz4KU=', N'NYMCU8FevNHBuW1laPZlIA==', N'Omar hossam eldin hafez', N'User', NULL, N'Site Supervision')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'41', N'Nader.T', N'Od8pt3B1+B/AJY/7D4IxBnKco5l+yg47B+LGsmFZZLI=', N'gdfqIIffiW+OEqg/39f6yQ==', N'Nader Talaat', N'User', NULL, N'Site Supervision')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'42', N'Micheal.M', N'QXbZY7ZNhWulTtXIm7THe+5KRw/A5/Yp42ejEh35pTU=', N'F8KGiD3AC7PlcHqzvbeoaQ==', N'Micheal Medhat Mounir Habib', N'Manager', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'43', N'Amira.R', N'i7PSmMr5iv9hkpRugghufmJ+fvCt3l09njaQOg/ix2c=', N'XEUrCx+ur3uRfZ+HsiZtGg==', N'Amira Radwan Ghobashy Aly', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'44', N'Mohamed.M', N'4i8kOzJ14rMvh126LvEl559NLume7t7a2po73QcQSvI=', N'TZn0BDeYw4Up7XfdxxPLRA==', N'Mohamed Mahmoud Zaghloul', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'45', N'Reem.A', N'w/n5qLpF+w80vpWn4eRE7RlI4ypZk2DqcRiz279SwMI=', N'U7NZrnd9/8FmfkWA5r5T3Q==', N'Reem Ali Kamal Farid Abdel Gawad', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'46', N'Nourhan.S', N'0SsdqyH7WozbviSNdeF5ChMee95W9HpuLKxupd5IuZ0=', N'psec5SF1QgZgU7QFocPh+Q==', N'Nourhan Salem', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'47', N'Radwa.M', N'mo/5xWYC75kWMG7bKkKmhfCtRtT9I/YvlCyZj7shKrk=', N'rCxFtjiJ5s0XWQ8k2EWRlA==', N'Radwa Mohamed', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'48', N'Marina.R', N'WnhZa9OBSZReAdAeA+wSvxp7jDmamJiliUgw237/b+o=', N'vrO1KYIXZTE70xZ3yFyNDQ==', N'Marina Romany', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'49', N'Mark.M', N'jPaNRry6aGKBcuXUjZUN789QMm6IKHBCKFmI8sWUG6E=', N'3in1Z7HId8LBO/OlWHm66Q==', N'Mark Morgan', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'50', N'Joeseph.A', N'dRFFCKgf6kVBMVYFAXh3X1flsg1zR9C/sZvyX3WxJ8A=', N'1YdzGNxjWvyUSdb5rq38OQ==', N'Joeseph Ayad', N'User', NULL, N'Technical')
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO


-- ----------------------------
-- Auto increment value for Users
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Users]', RESEED, 50)
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

