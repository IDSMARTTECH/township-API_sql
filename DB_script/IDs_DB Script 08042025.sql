USE [IDS_ASSDB]
GO
/****** Object:  Table [dbo].[[tblModules]]]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[[tblModules]]]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[[tblModules]]](
	[ModuleID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_[tblModules]]] PRIMARY KEY CLUSTERED 
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[[tblProfile]]]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[[tblProfile]]]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[[tblProfile]]](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[profilename] [nvarchar](100) NOT NULL,
	[uid] [int] NOT NULL,
	[userId] [int] NULL,
	[isactive] [bit] NULL,
	[isdeleted] [bit] NULL,
	[createdby] [int] NULL,
	[createdon] [datetime2](7) NULL,
	[updatedby] [int] NULL,
	[updatedon] [datetime2](7) NULL,
 CONSTRAINT [PK_[tblProfile]]] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[[tblProfileDetails]]]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[[tblProfileDetails]]]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[[tblProfileDetails]]](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[profileid] [int] NOT NULL,
	[moduleId] [int] NOT NULL,
	[CanInsert] [bit] NULL,
	[CanUpdate] [bit] NULL,
	[CanDelete] [bit] NULL,
	[CanView] [bit] NULL,
	[isactive] [bit] NULL,
	[isdeleted] [bit] NULL,
	[createdby] [int] NULL,
	[createdon] [datetime2](7) NULL,
	[updatedby] [int] NULL,
	[updatedon] [datetime2](7) NULL,
 CONSTRAINT [PK_[tblProfileDetails]]] PRIMARY KEY CLUSTERED 
(
	[profileid] ASC,
	[moduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[[tblRole]]]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[[tblRole]]]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[[tblRole]]](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_[tblRole]]] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[[tblUser123]]]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[[tblUser123]]]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[[tblUser123]]](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UID] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_[tblUser]]] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[DependentLandowners]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DependentLandowners]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DependentLandowners](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[CSN] [nvarchar](max) NOT NULL,
	[IDNumber] [nvarchar](max) NOT NULL,
	[TagNumber] [nvarchar](max) NOT NULL,
	[PANnumber] [nvarchar](max) NOT NULL,
	[PassportNo] [nvarchar](max) NOT NULL,
	[LicenseNo] [nvarchar](max) NOT NULL,
	[AadharCardId] [nvarchar](max) NOT NULL,
	[VoterID] [nvarchar](max) NOT NULL,
	[Firstname] [nvarchar](max) NOT NULL,
	[MiddletName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[BloodGroup] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NULL,
	[MobileNo] [nvarchar](max) NOT NULL,
	[CardIssueDate] [datetime2](7) NULL,
	[CardPrintingDate] [datetime2](7) NULL,
	[LogicalDeleted] [nvarchar](max) NOT NULL,
	[DependLandOwnerIssueDate] [datetime2](7) NULL,
 CONSTRAINT [PK_DependentLandowners] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[DependentResident]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DependentResident]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DependentResident](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[CSN] [nvarchar](max) NOT NULL,
	[IDNumber] [nvarchar](max) NOT NULL,
	[TagNumber] [nvarchar](max) NOT NULL,
	[PANnumber] [nvarchar](max) NOT NULL,
	[PassportNo] [nvarchar](max) NOT NULL,
	[LicenseNo] [nvarchar](max) NOT NULL,
	[ICEno] [nvarchar](max) NOT NULL,
	[AadharCardId] [nvarchar](max) NOT NULL,
	[VoterID] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddletName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[BloodGroup] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NULL,
	[EmailID] [nvarchar](max) NOT NULL,
	[MobileNo] [nvarchar](max) NOT NULL,
	[LandLine] [nvarchar](max) NOT NULL,
	[Building] [nvarchar](max) NOT NULL,
	[FlatNumber] [nvarchar](max) NOT NULL,
	[CardIssueDate] [datetime2](7) NULL,
	[CardPrintingDate] [datetime2](7) NULL,
	[RegistrationIssueDate] [datetime2](7) NULL,
	[LogicalDeleted] [int] NOT NULL,
 CONSTRAINT [PK_DependentResident] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[DependentTenent]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DependentTenent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DependentTenent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[CSN] [nvarchar](max) NOT NULL,
	[IDNumber] [nvarchar](max) NOT NULL,
	[TagNumber] [nvarchar](max) NOT NULL,
	[PANnumber] [nvarchar](max) NOT NULL,
	[PassportNo] [nvarchar](max) NOT NULL,
	[LicenseNo] [nvarchar](max) NOT NULL,
	[ICEno] [nvarchar](max) NOT NULL,
	[AadharCardId] [nvarchar](max) NOT NULL,
	[VoterID] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddletName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[BloodGroup] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NULL,
	[EmailID] [nvarchar](max) NOT NULL,
	[MobileNo] [nvarchar](max) NOT NULL,
	[LandLine] [nvarchar](max) NOT NULL,
	[Building] [nvarchar](max) NOT NULL,
	[FlatNumber] [nvarchar](max) NOT NULL,
	[CardIssueDate] [datetime2](7) NULL,
	[CardPrintingDate] [datetime2](7) NULL,
	[RegistrationIssueDate] [datetime2](7) NULL,
	[Aggreement_From] [datetime2](7) NULL,
	[Aggreement_To] [datetime2](7) NULL,
	[LogicalDeleted] [int] NOT NULL,
 CONSTRAINT [PK_DependentTenent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Landowners]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Landowners]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Landowners](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CSN] [nvarchar](max) NOT NULL,
	[IDNumber] [nvarchar](max) NOT NULL,
	[TagNumber] [nvarchar](max) NOT NULL,
	[PANnumber] [nvarchar](max) NOT NULL,
	[PassportNo] [nvarchar](max) NOT NULL,
	[LicenseNo] [nvarchar](max) NOT NULL,
	[ICEno] [nvarchar](max) NOT NULL,
	[AadharCardId] [nvarchar](max) NOT NULL,
	[VoterID] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddletName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[BloodGroup] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NULL,
	[EmailID] [nvarchar](max) NOT NULL,
	[MobileNo] [nvarchar](max) NOT NULL,
	[LandLine] [nvarchar](max) NOT NULL,
	[Building] [nvarchar](max) NOT NULL,
	[FlatNumber] [nvarchar](max) NOT NULL,
	[CardIssueDate] [datetime2](7) NULL,
	[CardPrintingDate] [datetime2](7) NULL,
	[LogicalDeleted] [int] NOT NULL,
	[LandOwnerIssueDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Landowners] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PrimaryResident]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrimaryResident]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PrimaryResident](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CSN] [nvarchar](max) NOT NULL,
	[IDNumber] [nvarchar](max) NOT NULL,
	[TagNumber] [nvarchar](max) NOT NULL,
	[PANnumber] [nvarchar](max) NOT NULL,
	[PassportNo] [nvarchar](max) NOT NULL,
	[LicenseNo] [nvarchar](max) NOT NULL,
	[ICEno] [nvarchar](max) NOT NULL,
	[AadharCardId] [nvarchar](max) NOT NULL,
	[VoterID] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddletName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[BloodGroup] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NULL,
	[EmailID] [nvarchar](max) NOT NULL,
	[MobileNo] [nvarchar](max) NOT NULL,
	[LandLine] [nvarchar](max) NOT NULL,
	[Building] [nvarchar](max) NOT NULL,
	[FlatNumber] [nvarchar](max) NOT NULL,
	[CardIssueDate] [datetime2](7) NULL,
	[CardPrintingDate] [datetime2](7) NULL,
	[RegistrationIssueDate] [datetime2](7) NULL,
	[LogicalDeleted] [int] NOT NULL,
 CONSTRAINT [PK_PrimaryResident] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PrimaryTenent]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrimaryTenent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PrimaryTenent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RID] [int] NOT NULL,
	[CSN] [nvarchar](max) NOT NULL,
	[IDNumber] [nvarchar](max) NOT NULL,
	[TagNumber] [nvarchar](max) NOT NULL,
	[PANnumber] [nvarchar](max) NOT NULL,
	[PassportNo] [nvarchar](max) NOT NULL,
	[LicenseNo] [nvarchar](max) NOT NULL,
	[ICEno] [nvarchar](max) NOT NULL,
	[AadharCardId] [nvarchar](max) NOT NULL,
	[VoterID] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddletName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[BloodGroup] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NULL,
	[EmailID] [nvarchar](max) NOT NULL,
	[MobileNo] [nvarchar](max) NOT NULL,
	[LandLine] [nvarchar](max) NOT NULL,
	[Building] [nvarchar](max) NOT NULL,
	[FlatNumber] [nvarchar](max) NOT NULL,
	[TenentType] [int] NOT NULL,
	[CardIssueDate] [datetime2](7) NULL,
	[CardPrintingDate] [datetime2](7) NULL,
	[RegistrationIssueDate] [datetime2](7) NULL,
	[Aggreement_From] [datetime2](7) NULL,
	[Aggreement_To] [datetime2](7) NULL,
	[LogicalDeleted] [int] NOT NULL,
 CONSTRAINT [PK_PrimaryTenent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblModuleData]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblModuleData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblModuleData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[TypeID] [int] NOT NULL,
	[ModuleType] [nvarchar](150) NULL,
	[ParentID] [int] NOT NULL,
	[isactive] [bit] NULL,
	[createdby] [int] NULL,
	[createdon] [datetime2](7) NULL,
	[updatedby] [int] NULL,
	[updatedon] [datetime2](7) NULL,
	[Discriminator] [nvarchar](10) NULL,
 CONSTRAINT [PK_tblModuleData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblModuleData_temp]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblModuleData_temp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblModuleData_temp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[TypeID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[isactive] [bit] NULL,
	[createdby] [int] NULL,
	[createdon] [datetime2](7) NULL,
	[updatedby] [int] NULL,
	[updatedon] [datetime2](7) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 08/04/2025 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uid] [varchar](50) NOT NULL,
	[name] [varchar](250) NOT NULL,
	[password] [varchar](50) NULL,
	[email] [varchar](150) NULL,
	[phone] [varchar](20) NULL,
	[isactive] [bit] NULL,
	[isdeleted] [bit] NULL,
	[createdby] [int] NULL,
	[createdon] [datetime] NULL,
	[updatedby] [int] NULL,
	[updatedon] [datetime] NULL,
	[roleid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_[tblProfile]]_[tblUser]]_userId]') AND parent_object_id = OBJECT_ID(N'[dbo].[[tblProfile]]]'))
ALTER TABLE [dbo].[[tblProfile]]]  WITH CHECK ADD  CONSTRAINT [FK_[tblProfile]]_[tblUser]]_userId] FOREIGN KEY([userId])
REFERENCES [dbo].[[tblUser123]]] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_[tblProfile]]_[tblUser]]_userId]') AND parent_object_id = OBJECT_ID(N'[dbo].[[tblProfile]]]'))
ALTER TABLE [dbo].[[tblProfile]]] CHECK CONSTRAINT [FK_[tblProfile]]_[tblUser]]_userId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_[tblUser]]_[tblRole]]_RoleID]') AND parent_object_id = OBJECT_ID(N'[dbo].[[tblUser123]]]'))
ALTER TABLE [dbo].[[tblUser123]]]  WITH CHECK ADD  CONSTRAINT [FK_[tblUser]]_[tblRole]]_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[[tblRole]]] ([RoleID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_[tblUser]]_[tblRole]]_RoleID]') AND parent_object_id = OBJECT_ID(N'[dbo].[[tblUser123]]]'))
ALTER TABLE [dbo].[[tblUser123]]] CHECK CONSTRAINT [FK_[tblUser]]_[tblRole]]_RoleID]
GO
