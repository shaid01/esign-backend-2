/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_certificates
	(
	id int NOT NULL IDENTITY (1, 1),
	project float(53) NULL,
	company nvarchar(150) NULL,
	hpnumber nvarchar(50) NULL,
	email nvarchar(150) NULL,
	issuedate datetime NULL,
	expiredate datetime NULL,
	expire float(53) NULL,
	smartobject float(53) NULL,
	certificatestatus float(53) NULL,
	customerid float(53) NULL,
	subproject float(53) NULL,
	docstype float(53) NULL,
	passportid nvarchar(50) NULL,
	licenceid nvarchar(50) NULL,
	hotem nvarchar(50) NULL,
	securityquestion float(53) NULL,
	securityansware nvarchar(MAX) NULL,
	remarksdesc nvarchar(MAX) NULL,
	job nvarchar(MAX) NULL,
	identify float(53) NULL,
	certificateissuer float(53) NULL,
	issuerplace float(53) NULL,
	remarks nvarchar(MAX) NULL,
	ducrypt nvarchar(2) NULL,
	attorneylicensenumber nvarchar(50) NULL,
	notariolicensenumber nvarchar(50) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_certificates SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_certificates ON
GO
IF EXISTS(SELECT * FROM dbo.certificates)
	 EXEC('INSERT INTO dbo.Tmp_certificates (id, project, company, hpnumber, email, issuedate, expiredate, expire, smartobject, certificatestatus, customerid, subproject, docstype, passportid, licenceid, hotem, securityquestion, securityansware, remarksdesc, job, identify, certificateissuer, issuerplace, remarks, ducrypt, attorneylicensenumber, notariolicensenumber)
		SELECT id, project, CONVERT(nvarchar(150), company), hpnumber, CONVERT(nvarchar(150), email), issuedate, expiredate, expire, smartobject, certificatestatus, customerid, subproject, docstype, passportid, licenceid, hotem, securityquestion, securityansware, remarksdesc, job, identify, certificateissuer, issuerplace, remarks, ducrypt, attorneylicensenumber, notariolicensenumber FROM dbo.certificates WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_certificates OFF
GO
DROP TABLE dbo.certificates
GO
EXECUTE sp_rename N'dbo.Tmp_certificates', N'certificates', 'OBJECT' 
GO
ALTER TABLE dbo.certificates ADD CONSTRAINT
	PK_certificates PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IssueDate_OrderBy_Desc ON dbo.certificates
	(
	issuedate DESC
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT
