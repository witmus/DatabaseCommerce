use zbd_prod;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GetCurrentProductsPage]
	@offset int,
	@pageSize int
AS
BEGIN
	SELECT cp.Id, cp.Name, cp.NetPrice, cp.CategoryName, cp.IsDiscount, c.VatRate,
		   cp.NetPrice * ((100 + c.VatRate) / 100) as GrossPrice,
		   hp.netprice as NetLowest, hp.netprice * ((100 + hp.VatRate) / 100) as GrossLowest
	FROM CurrentProducts as cp
	LEFT JOIN Categories as c
	ON cp.CategoryName = c.Name
	OUTER APPLY
	(
		SELECT TOP 1 NetPrice, VatRate
		FROM HistoricProducts
		WHERE CurrentProductId = cp.Id
		AND cp.IsDiscount = 1
		AND CreatedTimestamp > CURRENT_TIMESTAMP - 30
		ORDER BY NetPrice
	) hp
	ORDER BY cp.Id
	OFFSET @offset ROWS
	FETCH NEXT @pageSize ROWS ONLY;
END
