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
		 hp.VatRate, cp.NetPrice * c.VatRate as VatPrice
  FROM CurrentProducts as cp
	LEFT JOIN Categories as c
	ON cp.CategoryName = c.Name
	LEFT  JOIN HistoricProducts as hp
	ON hp.Id = (select TOP 1 hp.Id from HistoricProducts
				where hp.CurrentProductId = cp.Id
				and cp.IsDiscount = 1
				and hp.CreatedTimestamp < CURRENT_TIMESTAMP - 30
				order by hp.NetPrice)
END



	


