USE zbd
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_InsertCurrentProduct
	@name varchar(100) = '', 
	@netPrice decimal(19,2) = 0,
	@isDiscount bit = 0,
	@categoryName varchar(63)
AS
BEGIN
	IF @categoryName NOT IN (select Name from Categories)
		BEGIN
			RAISERROR(50001,11,1);
			RETURN;
		END
	
	INSERT INTO 
	CurrentProducts(Name, NetPrice, IsDiscount, CategoryName)
	values (@name, @netPrice, @isDiscount, @categoryName);
END
GO