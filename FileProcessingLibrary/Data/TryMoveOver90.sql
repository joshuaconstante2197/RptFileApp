BEGIN
	DECLARE	
			@resultTable TABLE
			(curr money,
			over30 money,
			over60 money,
			tranId int);
	DECLARE @tr int;
			

	Insert Into @resultTable 
		SELECT Curr, Over30,Over60,InvoiceBalance.TransactionId FROM InvoiceBalance
		INNER JOIN AccountInfo ON InvoiceBalance.TransactionId = AccountInfo.TransactionId
		WHERE (DATEDIFF(Day,TranDate,CONVERT(DATE,GETDATE())) > 90) 
			AND TranDetail != 'Total Customer' 
			AND TranDetail != 'Prior Balance'
			AND TranDate != '0001-01-01' 
			AND Over90 = 0.00;
		
	SELECT * FROM @resultTable res INNER JOIN InvoiceBalance ON res.tranId = TransactionId
		
	
END