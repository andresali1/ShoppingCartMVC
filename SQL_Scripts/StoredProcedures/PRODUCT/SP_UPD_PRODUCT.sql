USE DBSHOPPING_CART
GO

CREATE PROC SP_UPD_PRODUCT(
	@ProductId INT
	,@P_name VARCHAR(500)
	,@P_description VARCHAR(500)
	,@BrandId VARCHAR(100)
	,@CategoryId VARCHAR(100)
	,@Price DECIMAL(10,2)
	,@Stock INT
	,@Active BIT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM PRODUCT WHERE P_name = @P_name AND ProductId != @ProductId)
		BEGIN
			UPDATE PRODUCT SET
				P_name = @P_name
				,P_description = @P_description
				,BrandId = @BrandId
				,CategoryId = @CategoryId
				,Price = @Price
				,Stock = @Stock
				,Active = @Active
			WHERE ProductId = @ProductId

			SET @Result = 1
		END
	ELSE
		BEGIN
			SET @Message = 'El producto ya existe'
		END
END
GO