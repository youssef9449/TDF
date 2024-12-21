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

 Date: 16/12/2024 00:10:24
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

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'1', N'admin', N'efcaHAoXw2Kv9HPJz1M3bvFRV5eiADL5zMvr6tTLTnw=', N'ZBgrxXVOvJ/fNkVHAkAJxQ==', N'Administrator', N'Admin', NULL, N'All')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'2', N'Nour.M', N'EVC68YiSNr+VaJmSrcdy1I/5in1C98H6tMEI8uYEI6Y=', N'UWR1xuptlB81O2EQH4bguQ==', N'Nour Mohamed El sayed Hafez Malek', N'User', NULL, N'3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'3', N'Andrew.A', N'fEbwTWpCKO1KGo7r8b4wQoapkZ/0g69gcqnrL/WIB8c=', N'lDPytFdoeX7PWGDwsRr09g==', N'Andrew Amir Afif Foud Metry', N'User', NULL, N'3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'4', N'Madonna.F', N'reub3PYjQUZjh0R2mrpYK/8z5p1YLhkM5Xj1t2MzZbI=', N'nW3aueVhIetb72vAfJZKdw==', N'Madonna Fekry Khalil', N'User', NULL, N'3D')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'5', N'Sameh.O', N'4YWVTK5h8HtgFXor295QvUJ9awRUkpfUnYqSUpGFGQQ=', N'ED/PCD+QB/Ni/xtb1mUeEg==', N'Sameh Osama Hussein Awad', N'Manager', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'6', N'Mohamed.A', N's7Tzm49bOrxdU3SvJbhjIn3fv6P21c17umrwyrrkRcI=', N'I7wYH066A2VyHCcQuj8aEA==', N'Mohamed Ali Abdel Gelil Selim', N'User', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'7', N'Mahmoud.M', N'K5zM9BwzfEedbnGwXffpMPPX1xPjSR+b2uf/t9j237Q=', N'y5jkp4q4n/6reoxQZ2MKqw==', N'Mahmoud Mohamed Rashad Mahmoud', N'User', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'8', N'Hisham.M', N'6N7u2KUpfYZDKO83Te0/kZSUA3PFuQBjVW0FtYiexbg=', N'KCnuO6tMMNmG++U+8Q+XHw==', N'Hisham Mohamed Helmy', N'User', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'9', N'Youssef.S', N'5GxuEZvvKirQ8X8Zy06WOzsqNhcD8T1/tNNY6jSymTY=', N'7fpK/fL5q24ievMlVKKAJQ==', N'Youssef Samir Ahmed Mohamed', N'User', NULL, N'Accounting')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'10', N'Emad.E', N'OQt/ndwZ010Z9J5Sk4vKHdz7j9xLyJ1bsr02SDiiwtM=', N'4ZVab3SROBmuTcFtd8k0xQ==', N'Emad El deen Amer Mohamed', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'11', N'Nagah.M', N'vpYPMJfB1iC99P3J9l8K3YngrqIUT1KjAObn1GEd29Q=', N'WQQ0XTJFldWpIBbkXa3U0Q==', N'Nagah Mohamed Zaki Abdel Aal', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'12', N'Mousa.H', N'VMXtc4cMLecCOdpJFEC3lJAgXhNLxIxaLsGvEpjbtOk=', N'+/zd3UPgov9An5kgADDHGw==', N'Mousa Hussein Hassan', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'13', N'Salma.M', N'pXleXrENsv0nmWWUxDGEA4vUJpoXwM51izh7RwQGeVE=', N'wjHUX6lGqROE39/y7LWoQQ==', N'Salma Mohamed Mohamed Essam Abdelshafik', N'User', NULL, N'Administration')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'14', N'Mohamed.H', N'jJ5F/6QtCaFVghImqFVDVaRaxAEK06AAxG7judqqjqY=', N'RWd7dr3Hd/yR21pdRYki1w==', N'Mohamed Hamada Ahmed Masoud', N'User', NULL, N'Administrative Affairs')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'15', N'Walid.M', N'f0uIVfRhwQoit3LERB8mPLWQv5ecHK1WlmY/YLt+5uw=', N'b2oCg9pGvsvefI0RFvSM9g==', N'Walid Mohamed Ibrahim Mohamed', N'User', NULL, N'Administrative Affairs')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'16', N'Hana.Abdelaziz', N'wbNSKFX+FbvX+VOVEKIzyIKAs2se0+YQoRd3u9Q0gHQ=', N'HqEiN5Y6NKw9DETb2R2Wow==', N'Hana Abdelaziz', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'17', N'Nada.S', N'KN8d9FnNPU1Ugzdr/Sj9u4oWL4WGO8CCOLK8tM0kPUo=', N'l9Z37TLKOhVWWBHUsQfaHw==', N'Nada Sharaf Hamed El Sherbiny', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'18', N'Menna.T', N'QibHQGZte4wbvSn9Squ1mu2M5VwNAFb0uYZugk3IB9I=', N'voSiFC5q805oqJrJUqvRVg==', N'Menna Tallah Ani El Sayed Shehata Mohamed', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'19', N'Nadeenabdel.W', N'sjUXkXC2zMqB0oVudORJCn+amKxS6R0yoIo7LiUbGjw=', N'9pFZsCePp7mBar9v3iN9JQ==', N'Nadeenabdel Wahab', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'20', N'Nourhan.A', N'6g3MBRZQGYFfTCx5yACGIIhXW6Xh47U3ibWyfdUHfl0=', N'ICDxNPsPW4K2lLzYo9du4w==', N'Nourhan Ahmed', N'User', NULL, N'Experience Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'21', N'Yousra.A', N'e2q/ya1c3t9KLWGznfaPo4KYbOuPTYe4SFhPQppw90U=', N'Iaaglbt1B0JfX+20HFBauw==', N'Yousra Amr Mahmoud Atallah', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'22', N'Hana.Amr', N'NdfFdMqijyuwjYPZzVMv/afpJ6SViqLjhobjA9SZh4c=', N'9b86c5KWKmPG7tDc7Z1Vrg==', N'Hana Amr Mahmoud Roushdy Elbadrawy', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'23', N'Aya.M', N'mkuEUgWO77p+UjQeI5pNj0+1UHMq7/rxw8MnsLbi0i4=', N'jj/F9oZX8eTogItDb8Ba3Q==', N'Aya Magdy Abdel Naby Shoeib', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'24', N'Farah.A', N'/4ruawHfVL9DLmPHZ3GSV6hN+2Pd9UGWFtYOeZpceUk=', N'B82FZt5vr0+rwjUv7gkwAw==', N'Farah Afify', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'25', N'Habiba.S', N'L6WhSbSFO9OPI5tjpHHjeShhacU3O5P9M408ax45d4g=', N'3e0zt1mI/i17Uj7Oh5re+Q==', N'Habiba Salama', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'26', N'Alia.M', N'lqu93KKUoG7rhHR+6iYx6TPd035g32S++wS2BQ8c+Hk=', N'+Qs0lWZ+uAeZlfBWH2+/Pw==', N'Alia Mostafa', N'User', NULL, N'FF&A Team')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'27', N'Hala.I', N'fIUdEahHiYFzzNOqvEuLhI38p7tFKt+R0lH1h/HHQKU=', N'Z9LEOeLKl2BalKpUSugNzw==', N'Hala Ibrahim El Sayed Saleh', N'Admin', NULL, N'Founder & CEO')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'28', N'Eissa.M', N't/vwRoXg6IQt2g3xE7eaxOyGZa9O8k4a2Us1hA2aQW4=', N'IbOdsdg3z0bqeYeKmEdR0A==', N'Eissa Mohamed Mahmoud Eldafrawy', N'User', NULL, N'Human Resources')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'29', N'Selwan.A', N'm49gAZ9kSNBS7lPhaAyNRQQFYF9jhkZIwpLx0ELHeTk=', N'PzesI0MXy/uwig9YlpMNfg==', N'Selwan Awad Mohamed Ali Eleiwa', N'User', NULL, N'Projects Management')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'30', N'Shrouk.A', N'yj5hkYXyZ17l/YzG6T9v0hGWk8xYD3CdEBW+75Piy5E=', N'WaGGidjSXH5x6KBZm1GAXw==', N'Shrouk Ashraf Abdel Fattah Yahia', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'31', N'Nada.A', N'PA9lF1Sn/GCWouyhDYoBWGeYoJVldZyivc2opJZX9Dw=', N'KeUbpvSB+bpNU8cDaCsG2g==', N'Nada Ahmed Anwar Hussein', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'32', N'Yara.A', N'Y2Fbl0bxyIamHGQyRW6ZBVkYpAhMkNiffiE0nImka0w=', N'brkt6ZyM2ah6v7/NvU7M8A==', N'Yara Amr Abdel Hamid Zaki Dash', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'33', N'Yara.O', N'mPOqehoMVj9aHRnM6OslyI4qozztubMua6p45gQi458=', N'ze29b7WGBEg7FaLFJw+cug==', N'Yara Osman Khaled Mohamed Sayed ElMalky', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'34', N'Nouran.M', N'WcYw1Hbp2mUiZ/ILblMfgARMOIv3Z5CKMbAl89qPM00=', N'pQvjj1FPCOKMFFFoo6U8Fw==', N'Nouran Mohamed El sayed Saad Saad Montaser', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'35', N'Rana.H', N'YLVyXF/NsziA39p79p+eJdtDfX4DtiJPMH9c6sVsr3s=', N'zCWR724Fk5DxJ9C4UKZHOw==', N'Rana Hossam el Din Mostafa Khalifa', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'36', N'Sara.N', N'7PwuJOJUfqMN7tS9MJ/jV6fk5IID1sc5DsMrB1HRogw=', N'nWTCRRV08xGMRt6wqZ+sNw==', N'Sara Nashaat Nashed', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'37', N'Ahmed.K', N'NHyfhMBMIOpYFTlJe4W40IKfKOBa5ybfu2NRiGdyeS8=', N'8HGRE3L5xNKGNhfVMjZNRA==', N'Ahmed Kilany', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'38', N'Karim.F', N'r+3e9AYjTrOickCYaMPczMT5ErfTCtcz635Yu462A8s=', N'gdOnfxQpuMN5cDL6eVSNqg==', N'Karim Fawzy', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'39', N'Jana.E', N'PWXjnBuIjSc4RWUckJWZs6oCClHWRzR+gK5mz73YUm4=', N'MXyuUrVl37HL73ZprWV/GA==', N'Jana Eleraky', N'User', NULL, N'Schematic Design')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'40', N'Sherif.A', N'QNn4IrQGWB9CNiULfUevKLOkhjCtmkNYtbmVWbMTk0A=', N'wjvIE4kz6YL70dONp5rBUg==', N'Sherif Ashraf El Mohamady Abdo', N'User', NULL, N'Site Supervision')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'41', N'Abdelrahman.H', N'O/GGDubfVs0zFR15ukW6fE6qhFE4jbEAmRy5RlfedKU=', N'x2498cOkIp1fbJ8ipunF5A==', N'Abdelrahman Hafez Saad Hafez', N'User', NULL, N'Site Supervision')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'42', N'Omar.h', N'/cFat4/krbDZs6bmwaF0mpEyNY29w+XSVOJWo3eq+tA=', N'SMP9wtmUbQeE3uQl6HW1nQ==', N'Omar hossam eldin hafez', N'User', NULL, N'Site Supervision')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'43', N'Nader.T', N'zSHBjArdRRTSRvg85GKrzsoEKRUzbGOSk87GKt+xnVU=', N'6bJVTpilNanbWR7eN8dU1A==', N'Nader Talaat', N'User', NULL, N'Site Supervision')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'44', N'Loay.S', N'VjNo9JLdJPqYR+EUx6YuU9FDW9PUOYy39g/fWey6M0I=', N'/pPftFxGDY7cjm1dLid8QA==', N'Loay Sherif Hussein Hosny', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'45', N'Micheal.M', N'mGYsn86f5Xb5fe5Jgz+tHBFXod0+p9F5ZWAukU0kbeM=', N'gqj0FlIO/sYbIw+kPK+ZuA==', N'Micheal Medhat Mounir Habib', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'46', N'Amira.R', N'BeB7K3ualA/fqy69jv/Ef3lVuHALesZc3oEIDT52f7U=', N'VF8fidFRWKZeowU+SLDNcw==', N'Amira Radwan Ghobashy Aly', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'47', N'Mohamed.M', N'7wQMl9JF2trtiGEUBXi3dSUTWYlqZP+GSzVwmwT6ucA=', N'kDAaRxooGY+Hzvfas25ahg==', N'Mohamed Mahmoud Zaghloul', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'48', N'Reem.A', N'4beKKScLIVicxpDU7e8f4W5yrDLcShfYnP5Ox5ooGTk=', N'rKYGT0zunB5LrhZzSoHz6A==', N'Reem Ali Kamal Farid Abdel Gawad', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'49', N'Maggi.W', N'iilAeA97uK4Lq3eiUMI8jA3FLOXiREmt0CEcluMuTlE=', N'U4lTfkoRuzNKx5cfmsmWkg==', N'Maggi Wageeh Kedees Youssef', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'50', N'Nourhan.S', N'BQ57Iw3w4SADhhGhO+QJv6OitnsytxZKQwfKtGiUSqo=', N'/zBAoRu+ug9J7ALx0A6nTw==', N'Nourhan Salem', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'51', N'Radwa.M', N'8JR6lcL8t6LMEAiUkDMTbSeUFn5z1WmXnb45CYiF0mg=', N'Vu2X1SZmN+BRQb+vJEfXuw==', N'Radwa Mohamed', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'52', N'Marina.R', N'MkqVzn5F+cv3o0GSWPvU/NpDJn+RoQk+1dHtndnI81M=', N'04rL1rKNFdnMscdP1NtKkw==', N'Marina Romany', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'53', N'Mark.M', N'c5hRKxQNR15g02+Czk+DF5No5/fP3cZh2Ud8xlg/NTY=', N'uzOyB6G5E0N4RUTIPog//w==', N'Mark Morgan', N'User', NULL, N'Technical')
GO

INSERT INTO [dbo].[Users] ([UserID], [UserName], [PasswordHash], [Salt], [FullName], [Role], [Picture], [Department]) VALUES (N'54', N'Joeseph.A', N'sihc2+gRHl9ADq43SOJ6vYYGBovZjBfUUUtymDZN2/M=', N'Zyx1A5qAsDjS5A1aKluhDg==', N'Joeseph Ayad', N'User', NULL, N'Technical')
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO


-- ----------------------------
-- Auto increment value for Users
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Users]', RESEED, 54)
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

