USE zbd
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_SubmitOrder
	@invoiceNumber varchar(20) = '', 
	@userId int
AS
BEGIN
	--check if cart is empty
	IF NOT EXISTS (SELECT 1 FROM CartProducts where UserId = @userId)
		BEGIN
			RAISERROR(50006,11,1);
			RETURN;
		END

	--fetch cart positions to be transformed into order positions
	DECLARE @cartItems TABLE (productId int, amount int);
	INSERT INTO @cartItems 
	SELECT CurrentProductId, amount
	FROM CartProducts
	WHERE UserId = @userId;
	
	--create order header
	INSERT INTO Orders(UserId, InvoiceNumber, IncomeDate)
	VALUES (@userId, @invoiceNumber, CURRENT_TIMESTAMP);

	--create order positions
	INSERT INTO ProductsOrders(OrderInvoiceNumber, HistoricProductId, Amount)
	SELECT @invoiceNumber, hp.Id, ci.Amount FROM @cartItems as ci
	OUTER APPLY 
	(
		SELECT TOP 1 Id
		FROM HistoricProducts
		WHERE CurrentProductId = ci.productId
		ORDER BY CreatedTimestamp DESC
	) hp;

	--clear cart
	DELETE FROM CartProducts 
	WHERE UserId = @userId;
END
GO