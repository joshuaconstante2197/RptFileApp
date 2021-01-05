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
	TransactionId int,
	TranDate date DEFAULT NULL,
	TranDetail varchar(50),
	DueDate date DEFAULT NULL,
	InvoiceNumber varchar(10),
	ReferenceNumber varchar(20),
	FOREIGN KEY(ArCode) REFERENCES dbo.AccountHeader(ArCode) ON DELETE CASCADE,
	PRIMARY KEY(ArCode,TransactionId)
)
Create TABLE dbo.InvoiceBalance(ArCode varchar(12),
	TransactionId int,
	InvoiceNumber varchar (10),
	Balance money,
	Curr money,
	Over30 money,
	Over60 money,
	Over90 money,
	FOREIGN KEY(ArCode, TransactionId) REFERENCES dbo.AccountInfo(ArCode, TransactionId) ON DELETE CASCADE
)
Create Table dbo.Comment(ArCode varchar(12),
	TransactionId int,
	Commnent varchar(max),
	CommentDate date,
	FOREIGN KEY(ArCode, TransactionId) REFERENCES dbo.AccountInfo(ArCode, TransactionId) ON DELETE CASCADE
)


	