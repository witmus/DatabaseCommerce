EXEC sys.sp_addmessage 50001,1,'category_not_found',@replace='replace'
GO

EXEC sys.sp_addmessage 50002,1,'user_not_found',@replace='replace'
GO

EXEC sys.sp_addmessage 50003,1,'product_not_found',@replace='replace'
GO

EXEC sys.sp_addmessage 50004,1,'invalid_product_amount',@replace='replace'
GO

EXEC sys.sp_addmessage 50005,1,'category_name_taken',@replace='replace'
GO

EXEC sys.sp_addmessage 50006,1,'cart_empty',@replace='replace'
GO

EXEC sys.sp_addmessage 50007,1,'email_taken',@replace='replace'
GO

EXEC sys.sp_addmessage 50008,1,'user_without_address',@replace='replace'
GO