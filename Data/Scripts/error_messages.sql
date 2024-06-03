USE [zbd]
GO

EXEC sys.sp_addmessage 50001,1,'category_not_found',@replace='replace'
GO