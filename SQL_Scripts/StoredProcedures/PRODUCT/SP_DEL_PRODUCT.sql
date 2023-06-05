USE DBSHOPPING_CART
GO

CREATE PROC SP_DEL_PRODUCT(
	@ProductId INT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM SALE_DETAILS D
				   INNER JOIN PRODUCT P ON P.ProductId = D.ProductId
				   WHERE D.ProductId = @ProductId)
		BEGIN
			DELETE TOP(1) FROM PRODUCT
			WHERE ProductId = @ProductId

			SET @Result = 1
		END
	ELSE 
		BEGIN
			SET @Message = 'El producto se encuentra relacionado a una venta'
		END
END
GO