USE [ProjectManagement]
GO

/****** Object:  Table [dbo].[Task]    Script Date: 03/07/2019 12:04:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Task](
	[Task_ID] [int] IDENTITY(1,1) NOT NULL,
	[Parent_ID] [int] NULL,
	[Project_ID] [int] NULL,
	[TaskName] [varchar](max) NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Priority] [int] NULL,
	[Status] [varchar](100) NULL,
	[User_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Task_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([Parent_ID])
REFERENCES [dbo].[ParentTask] ([Parent_ID])
GO


