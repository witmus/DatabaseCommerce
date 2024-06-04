SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GetOrderReceiverWithAddress]
  @invoiceNumber varchar(20)
AS
BEGIN
  SELECT TOP 1 
  u.FirstName + ' ' + u.LastName as ReceiverName,
  a.Town, a.Street, a.ZipCode, a.BuildingNumber, a.ApartmentNumber
  FROM Users as u
  JOIN Addresses as a
  ON a.UserId = u.Id
  WHERE u.Id in (
	SELECT UserId 
	FROM Orders
	WHERE InvoiceNumber = @invoiceNumber)
END



	


