SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE sp_Login
	@email varchar(100),
	@password varchar(100)
AS
BEGIN
  SELECT Id FROM Users
  WHERE Email = @email
  AND PasswordHash = CAST(HASHBYTES('SHA2_512', @password) AS VARCHAR)
END
GO
