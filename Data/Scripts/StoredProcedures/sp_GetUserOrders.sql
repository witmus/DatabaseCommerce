USE zbd;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_GetUserOrders
	@userId int = 0
AS
BEGIN
  SELECT o.InvoiceNumber, o.IncomeDate,
  ROUND(SUM(hp.NetPrice * (100 + hp.VatRate) / 100 * po.Amount),2) as GrossTotal
  FROM Orders as o
  JOIN ProductsOrders as po
  ON po.OrderInvoiceNumber = o.InvoiceNumber
  JOIN HistoricProducts as hp
  ON hp.Id = po.HistoricProductId
  WHERE UserId = @userId 
  GROUP BY o.InvoiceNumber, o.IncomeDate
END
GO
