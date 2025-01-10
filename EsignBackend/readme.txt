OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF doesn't work in MSSQL14

"the query had to wait for 83 seconds for Memory Grant during execution" - reason NVARCHAR(MAX)?

upgrade
	"Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.20"

install
	"Microsoft.EntityFrameworkCore" Version="7.0.20"
	"Microsoft.EntityFrameworkCore.Design" Version="7.0.20"
	"Microsoft.EntityFrameworkCore.Tools" Version="7.0.20"

remove    <PackageReference Include="EntityFramework" Version="6.5.1" />

cd EsignBackend
(obsolete)Enable-Migrations -ContextTypeName EsignBackend.Models.AppDbContext

add-migration AddIndex_Customers_IdNumber
(module created: "20241212120039_AddIndexes_Customers_Certificates.cs")

Update-Database -verbose

USE [Esign_new]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504140755_InitialCreate', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504141102_test', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504141354_testup', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504141535_testup1', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504141640_testup2', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504151715_testup3', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504152251_testup4', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505055111_callpriority', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505061617_callpriorityUp', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505062135_issplace', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505062836_pks', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505063740_pksPt2', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505064837_pksPt3', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505065013_pksPt4', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505070947_subproject', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505091338_certificateAndProjectBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505092604_certificateAndSubProjectBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505093635_certificateAndIssuerPlaceBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505094542_certificateAndIsscertBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505095119_certificateAndCustomerIdentifierBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505101224_certificateAndCustomerBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505104506_certificateAndCerstatusBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505105010_certificateAndSmartObjectBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505105454_certificateAndExpirationTypeBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505110233_certificateAndSecurityquestionBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210505113551_certificateAndDocstypeBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210509072800_customerAndSecurityQuestionrBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210509072947_customerAndSecurityQuestiondoubleBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210509073311_customerAndSecurityQuestionintBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210509082515_customerAndSecurityQuestionRelationAddedUpdBind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210509082804_customerAndSecurityQuestionRelationAddedUpd1Bind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210509083308_customerAndSecurityQuestionRelationAddedUpd2Bind', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210512071935_historyCertificateRelationsBinding', N'5.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210512073706_historyCertificateRelationsBindingUpdate', N'5.0.2')
--INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241212120039_AddIndexes_Customers_Certificates', N'7.0.20')
GO


Reset Entity Network:
https://weblog.west-wind.com/posts/2016/jan/13/resetting-entity-framework-migrations-to-a-clean-slate