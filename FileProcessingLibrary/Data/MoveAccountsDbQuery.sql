DECLARE @resultTable TABLE (
	transactionId int,
	curr money,
	over30 money,
	over60 money,
	over90 money)

INSERT INTO @resultTable SELECT InvoiceBalance.TransactionId,Curr,Over30,Over60,Over90 FROM InvoiceBalance 
	INNER JOIN AccountInfo ON InvoiceBalance.TransactionId = AccountInfo.TransactionId
	WHERE (DATEDIFF(Day,TranDate,CONVERT(DATE,GETDATE())) >= 85) 
		AND TranDetail != 'Total Customer'
		AND TranDetail != 'Prior Balance'
		AND TranDate != '0001-01-01'
		AND Over90 = 0.00

UPDATE Inv
	SET Over90 = CASE
		WHEN Inv.Curr != 0.00 THEN Inv.Curr
		WHEN Inv.Over30 != 0.00 THEN Inv.Over30
		WHEN Inv.Over60 != 0.00 THEN Inv.Over60
	END
From InvoiceBalance Inv INNER JOIN @resultTable Result ON Inv.TransactionId = Result.transactionId
 