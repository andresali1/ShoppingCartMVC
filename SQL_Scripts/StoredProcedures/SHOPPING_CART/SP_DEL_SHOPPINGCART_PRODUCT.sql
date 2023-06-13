USE DBSHOPPING_CART
GO

CREATE PROC SP_DEL_SHOPPINGCART_PRODUCT (
	@ClientId INT
	,@ProductId INT
	,@Result BIT OUTPUT
)
AS
BEGIN
	SET @Result = 1
	DECLARE @ProductAmount INT = (SELECT Amount FROM SHOPPING_CART WHERE ClientId = @ClientId AND ProductId = @ProductId)

	BEGIN TRY
		BEGIN TRANSACTION OPERATION

		UPDATE PRODUCT SET Stock = Stock + @ProductAmount WHERE ProductId = @ProductId
		DELETE TOP(1) FROM SHOPPING_CART WHERE ClientId = @ClientId AND ProductId = @ProductId

		COMMIT TRANSACTION OPERATION
	END TRY
	BEGIN CATCH
		SET @Result = 0
		ROLLBACK TRANSACTION OPERATION
	END CATCH
END
GO