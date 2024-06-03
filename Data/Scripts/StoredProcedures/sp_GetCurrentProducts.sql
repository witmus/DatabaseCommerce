USE [zbd]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GetCurrentProducts]
AS
BEGIN
  SELECT cp.Id, cp.Name, cp.NetPrice, cp.CategoryName, cp.IsDiscount,
		 c.VatRate, cp.NetPrice * ((100 + c.VatRate) / 100) as GrossPrice,
		 hp.netprice as NetLowest, hp.netprice * ((100 + hp.VatRate) / 100) as GrossLowest
  FROM CurrentProducts as cp
	LEFT JOIN Categories as c
	ON cp.CategoryName = c.Name
	OUTER APPLY
	(
		select top 1 netprice, vatrate
		from HistoricProducts
		where CurrentProductId = cp.Id
		and cp.IsDiscount = 1
		and CreatedTimestamp > CURRENT_TIMESTAMP - 30
		order by netprice
	) hp
END



	


