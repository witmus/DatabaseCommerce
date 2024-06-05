SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_SignUp
	@firstName varchar(100),
	@lastName varchar(100),
	@phoneNumber varchar(20),
	@email varchar(100),
	@password varchar(100)
AS
BEGIN
	-- check if user exists
	IF EXISTS (SELECT 1 FROM Users WHERE Email = @email)
		BEGIN
			RAISERROR(50007,11,1);
			RETURN;
		END

	INSERT INTO Users(FirstName, LastName, PhoneNumber, Email, PasswordHash)
	VALUES (@firstName, @lastName, @phoneNumber, @email, CAST(HASHBYTES('SHA2_512', @password) as VARCHAR));
END
GO