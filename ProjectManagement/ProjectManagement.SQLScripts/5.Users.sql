USE [ProjectManagement]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 03/07/2019 12:35:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Employee_ID] [int] NULL,
	[Project_ID] [int] NULL,
	[Task_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([Project_ID])
REFERENCES [dbo].[Project] ([Project_ID])
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([Task_ID])
REFERENCES [dbo].[Task] ([Task_ID])
GO


