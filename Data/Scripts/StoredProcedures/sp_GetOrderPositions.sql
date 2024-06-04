SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GetOrderPositions]
	@invoiceNumber varchar(20)
AS
BEGIN
	SELECT hp.Name, po.Amount, hp.NetPrice, hp.VatRate,
	ROUND(hp.NetPrice * (100 + hp.VatRate) / 100, 2) as GrossPrice,
	ROUND(hp.NetPrice * po.Amount, 2) as NetTotal,
	ROUND(hp.NetPrice * po.Amount * hp.VatRate / 100, 2) as VatTotal,
	ROUND(hp.NetPrice * po.Amount * (100 + hp.VatRate) / 100, 2) as GrossTotal
	FROM Orders as o
	JOIN ProductOrders as po
	ON po.OrderInvoiceNumber = o.InvoiceNumber
	JOIN HistoricProducts as hp
	ON hp.Id = po.HistoricProductId
	WHERE o.InvoiceNumber = @invoiceNumber
END



	


