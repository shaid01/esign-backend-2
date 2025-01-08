USE [Esign]
GO
SET ANSI_PADDING ON
GO

update buusers
set usergroup = 'מנהל'
where usergroup = 'אדמין'
GO

update buusers
set usergroup = 'מנפיק'
where usergroup = 'מחדש'
GO

update buusers
set usergroup = 'תומך'
where usergroup = 'אורח'
GO

update buusers
set usergroup = 'תומך'
where usergroup = 'מתאם' OR usergroup = '' OR usergroup IS NULL
GO