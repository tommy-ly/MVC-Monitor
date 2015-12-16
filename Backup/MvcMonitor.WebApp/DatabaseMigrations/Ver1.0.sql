/****** MVC Monitor baseline database creation script (v1.0) ******/

/* Acts on MvcMonitor and MvcMonitorTest databases */
/*	- Creates the tables to hold ErrorModel objects */

USE [MvcMonitor]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorModel](
	[Id] [uniqueidentifier] NOT NULL,
	[ErrorId] [uniqueidentifier] NULL,
	[Application] [varchar](255) NULL,
	[Time] [datetime] NULL,
	[Username] [ntext] NULL,
	[Host] [ntext] NULL,
	[Url] [ntext] NULL,
	[StatusCode] [int] NULL,
	[RequestMethod] [ntext] NULL,
	[UserAgent] [ntext] NULL,
	[ExceptionType] [ntext] NULL,
	[ExceptionMessage] [ntext] NULL,
	[ExceptionSource] [ntext] NULL,
	[ExceptionStackTrace] [ntext] NULL,
	[ServerName] [ntext] NULL,
	[ServerPort] [int] NULL,
	[ServerPortSecure] [ntext] NULL,
	[ServerApplicationPath] [ntext] NULL,
	[ServerApplicationPathTranslated] [ntext] NULL,
	[QueryString] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

USE [MvcMonitorTest]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorModel](
	[Id] [uniqueidentifier] NOT NULL,
	[ErrorId] [uniqueidentifier] NULL,
	[Application] [varchar](255) NULL,
	[Time] [datetime] NULL,
	[Username] [ntext] NULL,
	[Host] [ntext] NULL,
	[Url] [ntext] NULL,
	[StatusCode] [int] NULL,
	[RequestMethod] [ntext] NULL,
	[UserAgent] [ntext] NULL,
	[ExceptionType] [ntext] NULL,
	[ExceptionMessage] [ntext] NULL,
	[ExceptionSource] [ntext] NULL,
	[ExceptionStackTrace] [ntext] NULL,
	[ServerName] [ntext] NULL,
	[ServerPort] [int] NULL,
	[ServerPortSecure] [ntext] NULL,
	[ServerApplicationPath] [ntext] NULL,
	[ServerApplicationPathTranslated] [ntext] NULL,
	[QueryString] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
