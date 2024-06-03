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
	/*DECLARE @vatRateTable TABLE (vr decimal(6,2));
	insert into @vatRateTable select top(1) c.vatRate from inserted join Categories as c on c.Name = inserted.CategoryName;

	DECLARE @vatRate decimal(6,2);
	select @vatRate = vr from @vatRateTable;*/

	INSERT INTO dbo.HistoricProducts
	(Name, NetPrice, CreatedTimestamp, CurrentProductId, VatRate)
	select i.name, netprice, CURRENT_TIMESTAMP, id, c.VatRate from inserted as i
	join Categories as c on i.CategoryName = c.name
END
