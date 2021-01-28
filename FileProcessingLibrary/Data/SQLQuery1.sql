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
);

CREATE TABLE dbo.AccountInfo (ArCode varchar(12),
	TransactionId int IDENTITY (1,1),
	TranDate date DEFAULT NULL,
	TranDetail varchar(50),
	DueDate date DEFAULT NULL,
	InvoiceNumber varchar(10),
	ReferenceNumber varchar(20),
	FOREIGN KEY(ArCode) REFERENCES dbo.AccountHeader(ArCode) ON DELETE CASCADE,
	PRIMARY KEY(ArCode,TransactionId)
);
Create TABLE dbo.InvoiceBalance(ArCode varchar(12),
	TransactionId int,
	Balance money,
	Curr money,
	Over30 money,
	Over60 money,
	Over90 money,
	FOREIGN KEY(ArCode, TransactionId) REFERENCES dbo.AccountInfo(ArCode, TransactionId) ON DELETE CASCADE,
	PRIMARY KEY(ArCode,TransactionId)
);
Create TABLE dbo.Comment(ArCode varchar(12),
	TransactionId int,
	CommentId int IDENTITY(1,1),
	CommentText varchar(max),
	CommentTime datetime,
	FOREIGN KEY(ArCode, TransactionId) REFERENCES dbo.AccountInfo(ArCode, TransactionId) ON DELETE CASCADE,
	PRIMARY KEY(ArCode,TransactionId,CommentId)
);
Create TABLE dbo.Files(DocumentId int IDENTITY (1,1),
	FileName varchar(100),
	FileExtension varchar(10),
	DataFile varbinary(MAX),
	CreatedOn datetime,
	TypeOfFile varchar(20),
	PRIMARY KEY(DocumentId)
);
Create TABLE dbo.TotalAR(TotalId int IDENTITY(1,1),
	UploadDate date,
	Balance money,
	Curr money,
	Over30 money,
	Over60 money,
	Over90 money
)








	