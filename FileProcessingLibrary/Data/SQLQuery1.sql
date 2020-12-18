use [master]
GO
IF DB_ID('RptFileApp') IS NOT NULL
BEGIN
	ALTER DATABASE RptFileApp SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE RptFileApp SET ONLINE;
	DROP DATABASE RptFileApp;

END

IF DB_ID('RptFileApp') IS NULL
BEGIN
	CREATE DATABASE RptFileApp
END

USE RptFileApp
GO

CREATE TABLE dbo.AccountHeader (ArCode varchar(12) PRIMARY KEY,
	AccountName varchar(50),
	AccountPhoneNumber varchar(50)
) 

CREATE TABLE dbo.AccountInfo (ArCode varchar(12),
	TranDate date,
	TranDetail varchar(50),
	DueDate date,
	InvoiceNumber varchar(10) UNIQUE,
	ReferenceNumber varchar(20)
	PRIMARY KEY(InvoiceNumber),
	FOREIGN KEY(ArCode) REFERENCES dbo.AccountHeader(ArCode) ON DELETE CASCADE
)

Create TABLE dbo.InvoiceBalance(ArCode varchar(12) FOREIGN KEY(ArCode) REFERENCES dbo.AccountHeader(ArCode) ON DELETE CASCADE,
	Balance money,
	Curr money,
	Over30 money,
	Over60 money,
	Over90 money,
	InvoiceNumber varchar (10),
	PRIMARY KEY(InvoiceNumber),
	FOREIGN KEY(InvoiceNumber) REFERENCES dbo.AccountInfo(InvoiceNumber)
)

	