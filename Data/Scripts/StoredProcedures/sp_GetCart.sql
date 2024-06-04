SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GetCart]
	@userId int = 0
AS
BEGIN
  SELECT c.Amount, c.CurrentProductId, cp.NetPrice,
		 cp.NetPrice * c.Amount as NetTotal,
		 ROUND(cp.NetPrice * c.Amount * ((100 + cat.VatRate) / 100), 2) as GrossTotal,
		 ROUND(cp.NetPrice * ((100 + cat.VatRate) / 100), 2) as GrossPrice,
		 cp.Name as ProductName, cp.CategoryName
  FROM ProductsInCart as c
  JOIN CurrentProducts as cp
  ON cp.Id = c.CurrentProductId
  JOIN Categories as cat
  ON cat.Name = cp.CategoryName
  WHERE c.UserId = @userId
END



	


