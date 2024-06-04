CREATE DATABASE zbd_prod;
USE zbd_prod;

CREATE TABLE ProductsInCart (UserId int NOT NULL, CurrentProductId int NOT NULL, Amount int NOT NULL, PRIMARY KEY (UserId, CurrentProductId));
CREATE TABLE CurrentProducts (CategoryName varchar(100) NULL, Id int IDENTITY NOT NULL, Name varchar(100) NOT NULL, NetPrice decimal(19, 2) NOT NULL, IsDiscount bit DEFAULT 0 NOT NULL, PRIMARY KEY (Id));
CREATE TABLE [Users] (Id int IDENTITY NOT NULL, FirstName varchar(100) NOT NULL, LastName varchar(100) NOT NULL, PhoneNumber varchar(20) NOT NULL, Email varchar(100) NOT NULL, PasswordHash varchar(255) NOT NULL, PRIMARY KEY (Id));
CREATE TABLE [Orders] (InvoiceNumber varchar(20) NOT NULL, IncomeDate datetime NOT NULL, UserId int NOT NULL, PRIMARY KEY (InvoiceNumber));
CREATE TABLE Categories (Name varchar(100) NOT NULL, VatRate decimal(6, 2) NOT NULL, PRIMARY KEY (Name));
CREATE TABLE Addresses (UserId int NOT NULL, Town varchar(100) NOT NULL, Street varchar(100) NOT NULL, BuildingNumber int NOT NULL, ApartmentNumber int NULL, ZipCode varchar(10) NOT NULL, Country varchar(100) NOT NULL, PRIMARY KEY (UserId));
CREATE TABLE ProductOrders (OrderInvoiceNumber varchar(20) NOT NULL, HistoricProductId int NOT NULL, Amount int NOT NULL, PRIMARY KEY (OrderInvoiceNumber, HistoricProductId));
CREATE TABLE HistoricProducts (Id int IDENTITY NOT NULL, Name varchar(100) NOT NULL, NetPrice decimal(19, 2) NOT NULL, VatRate decimal(6, 2) NOT NULL, CreatedTimestamp datetime NOT NULL, CurrentProductId int, PRIMARY KEY (Id));
ALTER TABLE ProductsInCart ADD CONSTRAINT FKProductInC434088 FOREIGN KEY (UserId) REFERENCES [Users] (Id);
ALTER TABLE ProductsInCart ADD CONSTRAINT FKProductInC15878 FOREIGN KEY (CurrentProductId) REFERENCES CurrentProducts (Id);
ALTER TABLE CurrentProducts ADD CONSTRAINT FKCurrentPro572125 FOREIGN KEY (CategoryName) REFERENCES Categories (Name);
ALTER TABLE ProductOrders ADD CONSTRAINT FKProductOrde936248 FOREIGN KEY (OrderInvoiceNumber) REFERENCES [Orders] (InvoiceNumber);
ALTER TABLE Addresses ADD CONSTRAINT FKAddress555376 FOREIGN KEY (UserId) REFERENCES [Users] (Id);
ALTER TABLE ProductOrders ADD CONSTRAINT FKProductOrde272508 FOREIGN KEY (HistoricProductId) REFERENCES HistoricProducts (Id);
ALTER TABLE [Orders] ADD CONSTRAINT FKOrder63375 FOREIGN KEY (UserId) REFERENCES [Users] (Id);
