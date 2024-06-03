USE [zbd]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER TRIGGER [dbo].[tr_OnCurrentProductsInsertUpdate] 
   ON  [dbo].[CurrentProducts] 
   AFTER INSERT,UPDATE
AS 
BEGIN
	INSERT INTO dbo.HistoricProducts
	(Name, NetPrice, CreatedTimestamp, CurrentProductId, VatRate)
	SELECT i.Name, NetPrice, CURRENT_TIMESTAMP, Id, c.VatRate from inserted as i
	JOIN Categories as c on i.CategoryName = c.name
END
