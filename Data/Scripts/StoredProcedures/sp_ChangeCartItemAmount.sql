SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_ChangeCartItemAmount
	@userId int,
	@productId int,
	@amount int
AS
BEGIN
	UPDATE ProductsInCart
	SET Amount = @amount
	WHERE UserId = @userId and CurrentProductId = @productId;	
END
GO