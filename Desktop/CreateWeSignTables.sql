IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [attachments] (
    [id] int NOT NULL,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [description] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [file1] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ticketid] float NULL
);
GO

CREATE TABLE [budesign] (
    [id] int NOT NULL,
    [lable] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [fieldname] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [description] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [value1] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [file1] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);
GO

CREATE TABLE [bulanguages] (
    [id] int NOT NULL,
    [title] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [description] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [parentid] float NULL,
    [entityid] float NULL
);
GO

CREATE TABLE [bumodulecoderep] (
    [id] int NOT NULL,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [times] float NULL,
    [text1] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [text2] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tid] float NULL,
    [parentid] float NULL,
    [status] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [applytoall] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);
GO

CREATE TABLE [bumodulefields] (
    [id] int NOT NULL,
    [tname] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tdisplay] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ttype] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tlength] float NULL,
    [tdefault] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tdir] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tcontrol] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tpriority] float NULL,
    [tid] float NULL,
    [tfilter] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tmultiupdate] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tdata] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tmust] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [thelp] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tmin] float NULL,
    [tmax] float NULL,
    [tlistupdate] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tnotr] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [tlist] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);
GO

CREATE TABLE [bumodules] (
    [id] int NOT NULL,
    [title] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [display] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [astree] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addsearch] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addnav] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [createtable] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [created] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [multilang] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [icon] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addcsv] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addsearchbar] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addmultiactions] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addduplicatebutton] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addpages] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [priority] int NULL,
    [addtrans] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [showid] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addloadcsv] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [addprint] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [monthviewfield] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [navgrouptitle] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [updateduserid] float NULL,
    [updateddate] datetime NULL,
    [defaultorder] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [permissions] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);
GO

CREATE TABLE [busettings] (
    [id] int NOT NULL,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [explain1] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [description] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [priority] float NULL,
    [parentid] int NULL
);
GO

CREATE TABLE [buusers] (
    [id] int NOT NULL IDENTITY,
    [username] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [userlevel] nvarchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [usergroup] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [firstname] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [lastname] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [email] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [phone] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [pass] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [picture] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [updateduserid] float NULL,
    [updateddate] datetime NULL,
    [parentid] float NULL,
    [permissions] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [allowedips] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [expires] datetime NULL,
    [remarks] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [sessionvalues] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [provider] float NULL,
    [elang] float NULL,
    [departmantid] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_buusers] PRIMARY KEY ([id])
);
GO

CREATE TABLE [callpriority] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_callpriority] PRIMARY KEY ([id])
);
GO

CREATE TABLE [callstatus] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [color] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_callstatus] PRIMARY KEY ([id])
);
GO

CREATE TABLE [certificatermearks] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_certificatermearks] PRIMARY KEY ([id])
);
GO

CREATE TABLE [certificates] (
    [id] int NOT NULL IDENTITY,
    [project] float NULL,
    [company] nvarchar(50) NULL,
    [hpnumber] nvarchar(50) NULL,
    [email] nvarchar(70) NULL,
    [issuedate] datetime NULL,
    [expiredate] datetime NULL,
    [expire] float NULL,
    [smartobject] float NULL,
    [certificatestatus] float NULL,
    [customerid] float NULL,
    [subproject] float NULL,
    [docstype] float NULL,
    [passportid] nvarchar(50) NULL,
    [licenceid] nvarchar(50) NULL,
    [hotem] nvarchar(50) NULL,
    [securityquestion] float NULL,
    [securityansware] nvarchar(max) NULL,
    [remarksdesc] nvarchar(max) NULL,
    [job] nvarchar(50) NULL,
    [identify] float NULL,
    [certificateissuer] float NULL,
    [issuerplace] float NULL,
    [remarks] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_certificates] PRIMARY KEY ([id])
);
GO

CREATE TABLE [certificateshistory] (
    [id] int NOT NULL IDENTITY,
    [customerid] float NULL,
    [email] nvarchar(70) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [project] float NULL,
    [subproject] float NULL,
    [company] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [hpnumber] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [hotem] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [issuedate] datetime NULL,
    [expire] float NULL,
    [expiredate] datetime NULL,
    [certificatestatus] float NULL,
    [smartobject] float NULL,
    [remarksdesc] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [securityquestion] float NULL,
    [securityansware] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [docstype] float NULL,
    [passportid] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [licenceid] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [identify] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [certificateissuer] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [issuerplace] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [certificateid] float NULL,
    [updateduserid] float NULL,
    [updateddate] datetime NULL,
    [remarks] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_certificateshistory] PRIMARY KEY ([id])
);
GO

CREATE TABLE [certificatesstatus] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [color] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_certificatesstatus] PRIMARY KEY ([id])
);
GO

CREATE TABLE [characters] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_characters] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [custident] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [active] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_custident] PRIMARY KEY ([id])
);
GO

CREATE TABLE [customers] (
    [id] int NOT NULL IDENTITY,
    [idnumber] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [firstname] nvarchar(50) NULL,
    [lastname] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [phone1] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [mobile1] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [securityquestion] float NULL,
    [securityansware] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [certificates] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [temp] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [calleruserid] float NULL,
    [address] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [email] nvarchar(70) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [company] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_customers] PRIMARY KEY ([id])
);
GO

CREATE TABLE [departmants] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_departmants] PRIMARY KEY ([id])
);
GO

CREATE TABLE [docstype] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_docstype] PRIMARY KEY ([id])
);
GO

CREATE TABLE [expirationtype] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_expirationtype] PRIMARY KEY ([id])
);
GO

CREATE TABLE [isscert] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [active] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_isscert] PRIMARY KEY ([id])
);
GO

CREATE TABLE [issplace] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [active] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_issplace] PRIMARY KEY ([id])
);
GO

CREATE TABLE [progressreport] (
    [id] int NOT NULL,
    [ticketid] float NULL,
    [description] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [updateduserid] float NULL,
    [updateddate] datetime NULL
);
GO

CREATE TABLE [projects] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_projects] PRIMARY KEY ([id])
);
GO

CREATE TABLE [securityquestions] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_securityquestions] PRIMARY KEY ([id])
);
GO

CREATE TABLE [smartobjects] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_smartobjects] PRIMARY KEY ([id])
);
GO

CREATE TABLE [subproject] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [project] float NULL,
    CONSTRAINT [PK_subproject] PRIMARY KEY ([id])
);
GO

CREATE TABLE [tickets] (
    [id] int NOT NULL,
    [departmantid] float NULL,
    [status] float NULL,
    [priorityd] float NULL,
    [updateduserid] float NULL,
    [updateddate] datetime NULL,
    [transferto] float NULL,
    [schedule] datetime NULL,
    [lastupdater] float NULL,
    [lastdescription] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [calleruserid] float NULL,
    [donedate] datetime NULL,
    [closerequest] nvarchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [duedate] datetime NULL,
    [ticketname] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [comefrom] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [sulution] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [project] float NULL
);
GO

CREATE TABLE [userviews] (
    [id] int NOT NULL,
    [title] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [sessionvalues] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [userid] int NULL
);
GO

CREATE TABLE [Persons] (
    [Id] int NOT NULL IDENTITY,
    [ProjectId] int NULL,
    CONSTRAINT [PK_Persons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Persons_projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [projects] ([id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Persons_ProjectId] ON [Persons] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504140755_InitialCreate', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [attachments] ADD [Name] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504141102_test', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[attachments]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [attachments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [attachments] DROP COLUMN [Name];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504141354_testup', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [attachments] ADD [Name] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504141535_testup1', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[attachments]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [attachments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [attachments] DROP COLUMN [Name];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504141640_testup2', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504151715_testup3', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504152251_testup4', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [callpriority] ADD CONSTRAINT [PK_Callpriority] PRIMARY KEY ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505055111_callpriority', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [callpriority] ADD CONSTRAINT [PK_Callpriority] PRIMARY KEY ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505061617_callpriorityUp', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [issplace] ADD CONSTRAINT [PK_Issplace] PRIMARY KEY ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505062135_issplace', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [securityquestions] ADD CONSTRAINT [PK_Securityquestions] PRIMARY KEY ([id]);
GO

ALTER TABLE [subproject] ADD CONSTRAINT [PK_Subproject] PRIMARY KEY ([id]);
GO

ALTER TABLE [smartobjects] ADD CONSTRAINT [PK_Smartobjects] PRIMARY KEY ([id]);
GO

ALTER TABLE [projects] ADD CONSTRAINT [PK_Projects] PRIMARY KEY ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505062836_pks', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [isscert] ADD CONSTRAINT [PK_Isscert] PRIMARY KEY ([id]);
GO

ALTER TABLE [expirationtype] ADD CONSTRAINT [PK_Expirationtype] PRIMARY KEY ([id]);
GO

ALTER TABLE [docstype] ADD CONSTRAINT [PK_Docstype] PRIMARY KEY ([id]);
GO

ALTER TABLE [custident] ADD CONSTRAINT [PK_Custident] PRIMARY KEY ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505063740_pksPt2', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [customers] ADD CONSTRAINT [PK_Customers] PRIMARY KEY ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505064837_pksPt3', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [certificates] ADD CONSTRAINT [PK_certificates] PRIMARY KEY ([id]);
GO

ALTER TABLE [certificatesstatus] ADD CONSTRAINT [PK_certificatesstatus] PRIMARY KEY ([id]);
GO

ALTER TABLE [departmants] ADD CONSTRAINT [PK_departmants] PRIMARY KEY ([id]);
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [PK_certificateshistory] PRIMARY KEY ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505065013_pksPt4', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[subproject]') AND [c].[name] = N'project');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [subproject] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [subproject] ALTER COLUMN [project] int NOT NULL;
ALTER TABLE [subproject] ADD DEFAULT 0 FOR [project];
GO

CREATE INDEX [IX_subproject_project] ON [subproject] ([project]);
GO

ALTER TABLE [subproject] ADD CONSTRAINT [FK_subproject_projects_project] FOREIGN KEY ([project]) REFERENCES [projects] ([id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505070947_subproject', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'project');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [certificates] ALTER COLUMN [project] int NOT NULL;
ALTER TABLE [certificates] ADD DEFAULT 0 FOR [project];
GO

CREATE INDEX [IX_certificates_project] ON [certificates] ([project]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_projects_project] FOREIGN KEY ([project]) REFERENCES [projects] ([id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505091338_certificateAndProjectBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'subproject');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [certificates] ALTER COLUMN [subproject] int NULL;
GO

CREATE INDEX [IX_certificates_subproject] ON [certificates] ([subproject]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_subproject_subproject] FOREIGN KEY ([subproject]) REFERENCES [subproject] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505092604_certificateAndSubProjectBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'issuerplace');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [certificates] ALTER COLUMN [issuerplace] int NULL;
GO

CREATE INDEX [IX_certificates_issuerplace] ON [certificates] ([issuerplace]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_issplace_issuerplace] FOREIGN KEY ([issuerplace]) REFERENCES [issplace] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505093635_certificateAndIssuerPlaceBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'certificateissuer');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [certificates] ALTER COLUMN [certificateissuer] int NULL;
GO

CREATE INDEX [IX_certificates_certificateissuer] ON [certificates] ([certificateissuer]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_isscert_certificateissuer] FOREIGN KEY ([certificateissuer]) REFERENCES [isscert] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505094542_certificateAndIsscertBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'identify');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [certificates] ALTER COLUMN [identify] int NULL;
GO

CREATE INDEX [IX_certificates_identify] ON [certificates] ([identify]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_custident_identify] FOREIGN KEY ([identify]) REFERENCES [custident] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505095119_certificateAndCustomerIdentifierBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'customerid');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [certificates] ALTER COLUMN [customerid] int NOT NULL;
ALTER TABLE [certificates] ADD DEFAULT 0 FOR [customerid];
GO

CREATE INDEX [IX_certificates_customerid] ON [certificates] ([customerid]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_customers_customerid] FOREIGN KEY ([customerid]) REFERENCES [customers] ([id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505101224_certificateAndCustomerBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'certificatestatus');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [certificates] ALTER COLUMN [certificatestatus] int NULL;
GO

CREATE INDEX [IX_certificates_certificatestatus] ON [certificates] ([certificatestatus]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_certificatesstatus_certificatestatus] FOREIGN KEY ([certificatestatus]) REFERENCES [certificatesstatus] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505104506_certificateAndCerstatusBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'smartobject');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [certificates] ALTER COLUMN [smartobject] int NULL;
GO

CREATE INDEX [IX_certificates_smartobject] ON [certificates] ([smartobject]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_smartobjects_smartobject] FOREIGN KEY ([smartobject]) REFERENCES [smartobjects] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505105010_certificateAndSmartObjectBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'expire');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [certificates] ALTER COLUMN [expire] int NULL;
GO

CREATE INDEX [IX_certificates_expire] ON [certificates] ([expire]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_expirationtype_expire] FOREIGN KEY ([expire]) REFERENCES [expirationtype] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505105454_certificateAndExpirationTypeBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'securityquestion');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [certificates] ALTER COLUMN [securityquestion] int NULL;
GO

CREATE INDEX [IX_certificates_securityquestion] ON [certificates] ([securityquestion]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_securityquestions_securityquestion] FOREIGN KEY ([securityquestion]) REFERENCES [securityquestions] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505110233_certificateAndSecurityquestionBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'docstype');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [certificates] ALTER COLUMN [docstype] int NULL;
GO

CREATE INDEX [IX_certificates_docstype] ON [certificates] ([docstype]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_docstype_docstype] FOREIGN KEY ([docstype]) REFERENCES [docstype] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210505113551_certificateAndDocstypeBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210509072800_customerAndSecurityQuestionrBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[customers]') AND [c].[name] = N'securityquestion');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [customers] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [customers] ALTER COLUMN [securityquestion] float NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210509072947_customerAndSecurityQuestiondoubleBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[customers]') AND [c].[name] = N'securityquestion');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [customers] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [customers] ALTER COLUMN [securityquestion] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210509073311_customerAndSecurityQuestionintBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210509082515_customerAndSecurityQuestionRelationAddedUpdBind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [certificates] DROP CONSTRAINT [FK_certificates_securityquestions_securityquestion];
GO

DROP INDEX [IX_certificates_securityquestion] ON [certificates];
DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificates]') AND [c].[name] = N'securityquestion');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [certificates] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [certificates] ALTER COLUMN [securityquestion] int NOT NULL;
ALTER TABLE [certificates] ADD DEFAULT 0 FOR [securityquestion];
CREATE INDEX [IX_certificates_securityquestion] ON [certificates] ([securityquestion]);
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_securityquestions_securityquestion] FOREIGN KEY ([securityquestion]) REFERENCES [securityquestions] ([id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210509082804_customerAndSecurityQuestionRelationAddedUpd1Bind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [certificates] DROP CONSTRAINT [FK_certificates_securityquestions_securityquestion];
GO

ALTER TABLE [certificates] ADD CONSTRAINT [FK_certificates_securityquestions_securityquestion] FOREIGN KEY ([securityquestion]) REFERENCES [securityquestions] ([id]) ON DELETE NO ACTION;
GO

ALTER TABLE [customers] ADD CONSTRAINT [FK_customers_securityquestions_securityquestion] FOREIGN KEY ([securityquestion]) REFERENCES [securityquestions] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210509083308_customerAndSecurityQuestionRelationAddedUpd2Bind', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'updateduserid');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [updateduserid] int NULL;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'subproject');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [subproject] int NOT NULL;
ALTER TABLE [certificateshistory] ADD DEFAULT 0 FOR [subproject];
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'smartobject');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [smartobject] int NULL;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'securityquestion');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [securityquestion] int NULL;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'project');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [project] int NOT NULL;
ALTER TABLE [certificateshistory] ADD DEFAULT 0 FOR [project];
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'expire');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [expire] int NULL;
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'docstype');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [docstype] int NULL;
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'customerid');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [customerid] int NOT NULL;
ALTER TABLE [certificateshistory] ADD DEFAULT 0 FOR [customerid];
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'certificatestatus');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [certificatestatus] int NOT NULL;
ALTER TABLE [certificateshistory] ADD DEFAULT 0 FOR [certificatestatus];
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[certificateshistory]') AND [c].[name] = N'certificateid');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [certificateshistory] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [certificateshistory] ALTER COLUMN [certificateid] int NULL;
GO

CREATE INDEX [IX_certificateshistory_certificateid] ON [certificateshistory] ([certificateid]);
GO

CREATE INDEX [IX_certificateshistory_certificatestatus] ON [certificateshistory] ([certificatestatus]);
GO

CREATE INDEX [IX_certificateshistory_docstype] ON [certificateshistory] ([docstype]);
GO

CREATE INDEX [IX_certificateshistory_expire] ON [certificateshistory] ([expire]);
GO

CREATE INDEX [IX_certificateshistory_project] ON [certificateshistory] ([project]);
GO

CREATE INDEX [IX_certificateshistory_securityquestion] ON [certificateshistory] ([securityquestion]);
GO

CREATE INDEX [IX_certificateshistory_smartobject] ON [certificateshistory] ([smartobject]);
GO

CREATE INDEX [IX_certificateshistory_subproject] ON [certificateshistory] ([subproject]);
GO

CREATE INDEX [IX_certificateshistory_updateduserid] ON [certificateshistory] ([updateduserid]);
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_buusers_updateduserid] FOREIGN KEY ([updateduserid]) REFERENCES [buusers] ([id]) ON DELETE NO ACTION;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_certificates_certificateid] FOREIGN KEY ([certificateid]) REFERENCES [certificates] ([id]) ON DELETE NO ACTION;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_certificatesstatus_certificatestatus] FOREIGN KEY ([certificatestatus]) REFERENCES [certificatesstatus] ([id]) ON DELETE CASCADE;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_customers_updateduserid] FOREIGN KEY ([updateduserid]) REFERENCES [customers] ([id]) ON DELETE NO ACTION;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_docstype_docstype] FOREIGN KEY ([docstype]) REFERENCES [docstype] ([id]) ON DELETE NO ACTION;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_expirationtype_expire] FOREIGN KEY ([expire]) REFERENCES [expirationtype] ([id]) ON DELETE NO ACTION;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_projects_project] FOREIGN KEY ([project]) REFERENCES [projects] ([id]) ON DELETE CASCADE;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_securityquestions_securityquestion] FOREIGN KEY ([securityquestion]) REFERENCES [securityquestions] ([id]) ON DELETE NO ACTION;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_smartobjects_smartobject] FOREIGN KEY ([smartobject]) REFERENCES [smartobjects] ([id]) ON DELETE NO ACTION;
GO

ALTER TABLE [certificateshistory] ADD CONSTRAINT [FK_certificateshistory_subproject_subproject] FOREIGN KEY ([subproject]) REFERENCES [subproject] ([id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210512071935_historyCertificateRelationsBinding', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210512073706_historyCertificateRelationsBindingUpdate', N'5.0.2');
GO

COMMIT;
GO

