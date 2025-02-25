USE [Esign]
GO

/****** Object:  Index [Securityquestion_Foreign]    Script Date: 25/02/2025 7:15:20 ******/
CREATE NONCLUSTERED INDEX [Securityquestion_Foreign] ON [dbo].[certificates]
(
	[securityquestion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
