USE [Esign_new]
GO

/****** Object:  Index [ExpiryIssueDates_Where_OrderBy]    Script Date: 02/01/2025 8:23:15 ******/
CREATE NONCLUSTERED INDEX [ExpiryIssueDates_Where_OrderBy] ON [dbo].[certificates]
(
	[expiredate] ASC,
	[issuedate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [IssueExpiryDates_Where]    Script Date: 02/01/2025 13:05:17 ******/
CREATE NONCLUSTERED INDEX [IssueExpiryDates_Where] ON [dbo].[certificates]
(
	[issuedate] ASC,
	[expiredate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
