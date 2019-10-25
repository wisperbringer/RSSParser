CREATE DATABASE topics
Go

USE topics
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/25/2019 10:36:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sources]    Script Date: 10/25/2019 10:36:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[ItemParsePath] [nvarchar](max) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Sources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopicCategories]    Script Date: 10/25/2019 10:36:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopicCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TopicGuid] [nvarchar](450) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_TopicCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 10/25/2019 10:36:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Guid] [nvarchar](450) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[PublisDate] [datetime2](7) NOT NULL,
	[Creator] [nvarchar](max) NULL,
	[SourceId] [int] NOT NULL,
 CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TopicCategories]  WITH CHECK ADD  CONSTRAINT [FK_TopicCategories_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopicCategories] CHECK CONSTRAINT [FK_TopicCategories_Categories_CategoryId]
GO
ALTER TABLE [dbo].[TopicCategories]  WITH CHECK ADD  CONSTRAINT [FK_TopicCategories_Topics_TopicGuid] FOREIGN KEY([TopicGuid])
REFERENCES [dbo].[Topics] ([Guid])
GO
ALTER TABLE [dbo].[TopicCategories] CHECK CONSTRAINT [FK_TopicCategories_Topics_TopicGuid]
GO
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD  CONSTRAINT [FK_Topics_Sources_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Sources] ([Id])
GO
ALTER TABLE [dbo].[Topics] CHECK CONSTRAINT [FK_Topics_Sources_SourceId]
GO
