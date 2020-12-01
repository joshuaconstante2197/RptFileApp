IF DB_ID('RptFileApp') IS NULL
	BEGIN
		CREATE Database RptFileApp
	END
ELSE
	BEGIN
		USE RptFileApp
			IF OBJECT_ID('dbo.AccountHeader','U') IS NOT NULL
				BEGIN
					DROP TABLE dbo.AccountHeader;
				END

			IF OBJECT_ID('dbo.AccountInfo','U') IS NOT NULL
				BEGIN
					DROP TABLE dbo.AccountInfo;
				END

			IF OBJECT_ID('dbo.InvoiceBalances','U') IS NOT NULL
				BEGIN
					DROP TABLE dob.InvoiceBalances;
				END
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
	InvoiceNumber varchar(10),
	ReferenceNumber varchar(20)
	PRIMARY KEY(ArCode),
	FOREIGN KEY(ArCode) REFERENCES dbo.AccountHeader(ArCode)
)

Create TABLE dbo.InvoiceBalance(ArCode varchar(12),
	Balance money,
	Curr money,
	Over30 money,
	Over60 money,
	Over90 money,
	PRIMARY KEY(InvoiceNumber),
	FOREIGN KEY(InvoiceNumber) REFERENCES dbo.AccountInfo(InvoiceNumber)
)

	