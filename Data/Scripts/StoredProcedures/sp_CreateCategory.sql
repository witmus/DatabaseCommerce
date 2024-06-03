USE zbd
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_CreateCategory
	@name varchar(63),
	@vatRate decimal(6,2)
AS
BEGIN
	IF @name IN (select Name from Categories)
		BEGIN
			RAISERROR(50005,11,1);
			RETURN;
		END
		
	INSERT INTO 
	Categories(Name, VatRate)
	values (@name, @vatRate);
END
GO