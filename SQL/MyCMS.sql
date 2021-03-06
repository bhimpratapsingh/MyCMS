Use MASTER
go

CREATE DATABASE [MyCMS]

GO

USE [MyCMS]
GO

CREATE TABLE [dbo].[Category](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientLogo]    Script Date: 7/31/2021 7:07:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientLogo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Image] [varchar](250) NULL,
	[SortOrder] [int] NULL,
	[MakePublic] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Header]    Script Date: 7/31/2021 7:07:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Header](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Header] [varchar](max) NULL,
	[Image] [varchar](max) NULL,
	[SortOrder] [int] NULL,
	[MakePublic] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[Type] [int] NULL,
	[BackColor] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainContent]    Script Date: 7/31/2021 7:07:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainContent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[HeaderId] [bigint] NULL,
	[Content] [varchar](max) NULL,
	[Image] [varchar](max) NULL,
	[SortOrder] [int] NULL,
	[MakePublic] [bit] NULL,
	[AddPage] [bit] NULL,
	[PageTitle] [varchar](max) NULL,
	[FileName] [varchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubContent]    Script Date: 7/31/2021 7:07:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubContent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MainContentId] [bigint] NULL,
	[Content] [varchar](max) NULL,
	[Image] [varchar](max) NULL,
	[SortOrder] [int] NULL,
	[MakePublic] [bit] NULL,
	[AddPage] [bit] NULL,
	[PageTitle] [varchar](max) NULL,
	[FileName] [varchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubContentList]    Script Date: 7/31/2021 7:07:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubContentList](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SubContentId] [bigint] NULL,
	[Content] [varchar](max) NULL,
	[SortOrder] [int] NULL,
	[MakePublic] [bit] NULL,
	[FileName] [varchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 7/31/2021 7:07:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[designation] [varchar](255) NOT NULL,
	[username] [varchar](255) NOT NULL,
	[password] [varbinary](255) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[status] [bit] NOT NULL,
	[createdby] [bigint] NOT NULL,
	[createdon] [datetime] NOT NULL,
	[createdbyip] [varchar](20) NULL,
	[modifiedby] [bigint] NULL,
	[modifiedon] [datetime] NULL,
	[modifiedbyip] [varchar](20) NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_AuthenticateUser]    Script Date: 7/31/2021 7:07:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_AuthenticateUser]
(
	@Username VARCHAR(255),
	@Password VARCHAR(255)
)
as
begin
	declare @ErrorCode int = 0
	declare @UserId bigint = null
	declare @UserStatus bit = null

	-- check if user is valid	
	select @UserId = id from users where username = @Username and [password] = HASHBYTES('SHA2_256', @Password)
	if @UserId is null
		set @ErrorCode = 1	-- Invalid credentials

	if (@ErrorCode = 0)
	begin
		-- check if user is active
		select @UserStatus = status from users where username = @Username and [password] = @Password
		if @UserStatus = 0
			set @ErrorCode = 2	-- user is in-active
	end
	select @ErrorCode ErrorCode
end


GO


SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Services')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Products')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Careers')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [name], [designation], [username], [password], [email], [status], [createdby], [createdon], [createdbyip], [modifiedby], [modifiedon], [modifiedbyip]) VALUES (1, N'admin', N'amdinistrator', N'admin', 0xA665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3, N'admin@email.com', 1, 1, CAST(N'2018-09-06T00:00:00.000' AS DateTime), N'10.0.0.0', 2, CAST(N'2018-11-30T14:24:45.363' AS DateTime), N'::1')
SET IDENTITY_INSERT [dbo].[users] OFF
