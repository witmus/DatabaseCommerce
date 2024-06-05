SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_UpdateAddress
	@userId int,
	@town varchar(100),
	@street varchar(100),
	@buildingNumber int,
	@apartmentNumber int = NULL,
	@zipCode varchar(10),
	@country varchar(100)
AS
BEGIN
	-- check if user exists
	IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @userId)
		BEGIN
			RAISERROR(50002,11,1);
			RETURN;
		END

	-- check if user has address
	IF NOT EXISTS (SELECT 1 FROM Addresses WHERE UserId = @userId)
		BEGIN
			RAISERROR(50008,11,1);
			RETURN;
		END

	UPDATE Addresses SET
		Town = @town,
		Street = @street,
		BuildingNumber = @buildingNumber,
		ApartmentNumber = @apartmentNumber,
		ZipCode = @zipCode,
		Country = @country
	WHERE UserId = @userId;
END
GO