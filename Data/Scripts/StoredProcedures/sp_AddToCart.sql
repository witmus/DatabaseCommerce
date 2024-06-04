SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_AddToCart
	@userId int,
	@productId int,
	@amount int
AS
BEGIN
	-- check if user exists
	IF @userId NOT IN (select Id from Users)
		BEGIN
			RAISERROR(50002,11,1);
			RETURN;
		END
	
	-- check if product exists
	IF @productId NOT IN (select Id from CurrentProducts)
		BEGIN
			RAISERROR(50003,11,1);
			RETURN;
		END

	-- check if product amount is a positive integer
	IF @amount < 1
		BEGIN
			RAISERROR(50004,11,1);
			RETURN;
		END

	-- if product is already in cart update amount, if not create cart position
	IF EXISTS (select 1 from ProductsInCart where UserId = @userId and CurrentProductId = @productId)
		EXEC sp_ChangeCartItemAmount @userId, @productId, @amount;
	ELSE
		INSERT INTO 
		ProductsInCart(UserId, CurrentProductId, Amount)
		values (@userId, @productId, @amount);
END
GO