USE zbd
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_UpdateCurrentProduct 
	@id int = 0,
	@name varchar(100) = '', 
	@netPrice decimal(19,2) = 0,
	@isDiscount bit = 0
AS
BEGIN
	UPDATE dbo.CurrentProducts
	SET Name = @name, NetPrice = @netPrice, IsDiscount = @isDiscount
	WHERE Id = @id;
END
GO
