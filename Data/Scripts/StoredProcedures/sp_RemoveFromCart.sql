USE [zbd];
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_RemoveFromCart
	@userId int,
	@productId int
AS
BEGIN
  DELETE FROM dbo.CartProducts
  WHERE UserId = @userId and CurrentProductId = @productId
END
GO