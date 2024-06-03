USE zbd;

DELETE FROM addresses;
DELETE FROM ProductsOrders;
DELETE FROM orders;
DELETE FROM cartproducts;
DELETE FROM HistoricProducts;
DELETE FROM currentproducts;
DELETE FROM categories;
DELETE FROM users;

DBCC CHECKIDENT('dbo.Users', RESEED, 0);
DBCC CHECKIDENT('dbo.CurrentProducts', RESEED, 0);
DBCC CHECKIDENT('dbo.HistoricProducts', RESEED, 0);
