﻿/*
Deployment script for azure-sql-database846

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "azure-sql-database846"
:setvar DefaultFilePrefix "azure-sql-database846"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [sys].[databases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
/*
The column [dbo].[customer2].[active] is being dropped, data loss could occur.

The column [dbo].[customer2].[create_date] is being dropped, data loss could occur.

The column [dbo].[customer2].[email] is being dropped, data loss could occur.

The column [dbo].[customer2].[first_name] is being dropped, data loss could occur.

The column [dbo].[customer2].[last_name] is being dropped, data loss could occur.

The column [dbo].[customer2].[last_update] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[customer2])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Altering Table [dbo].[customer2]...';


GO
ALTER TABLE [dbo].[customer2] DROP COLUMN [active], COLUMN [create_date], COLUMN [email], COLUMN [first_name], COLUMN [last_name], COLUMN [last_update];


GO
PRINT N'Update complete.';


GO
