SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_AddAddress
	@email varchar(100),
	@town varchar(100),
	@street varchar(100),
	@buildingNumber int,
	@apartmentNumber int = NULL,
	@zipCode varchar(10),
	@country varchar(100)
AS
BEGIN
	DECLARE @userId int;
	SELECT @userId = Id FROM Users WHERE Email = @email;

	INSERT INTO Addresses(UserId, Town, Street, BuildingNumber, ApartmentNumber, ZipCode, Country)
	VALUES(@userId, @town, @street, @buildingNumber, @apartmentNumber, @zipCode, @country);
END
GO