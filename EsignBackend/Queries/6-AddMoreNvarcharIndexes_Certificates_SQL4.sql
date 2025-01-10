USE [Esign_new]
GO
SET ANSI_PADDING ON
GO

/****** Object:  Index [CustomerID_Foreign]    Script Date: 30/12/2024 14:59:05 ******/
CREATE NONCLUSTERED INDEX CustomerID_Foreign ON [dbo].[certificates]
(
	[customerid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CompanyCert_Where_OrderBy]    Script Date: 30/12/2024 15:00:19 ******/
CREATE NONCLUSTERED INDEX [CompanyCert_Where_OrderBy] ON [dbo].[certificates]
(
	[company] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Hpnumber_Where_OrderBy]    Script Date: 30/12/2024 15:00:56 ******/
CREATE NONCLUSTERED INDEX [Hpnumber_Where_OrderBy] ON [dbo].[certificates]
(
	[hpnumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
