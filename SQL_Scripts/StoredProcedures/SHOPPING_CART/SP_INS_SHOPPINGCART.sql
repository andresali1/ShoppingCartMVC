USE DBSHOPPING_CART
GO

CREATE PROC SP_INS_SHOPPINGCART (
	@ClientId INT
	,@ProductId INT
	,@Add BIT
	,@Message VARCHAR(50) OUTPUT
	,@Result BIT OUTPUT
)
AS
BEGIN
	SET @Result = 1
	SET @Message = ''

	DECLARE @CartExists BIT = IIF(EXISTS(SELECT * FROM SHOPPING_CART WHERE ClientId = @ClientId AND ProductId = @ProductId),1,0)
			,@ProductStock INT = (SELECT Stock FROM PRODUCT WHERE ProductId = @ProductId)

	BEGIN TRY
		BEGIN TRANSACTION OPERATION

			IF(@Add = 1)
				BEGIN
					IF(@ProductStock > 0)
						BEGIN
							IF(@CartExists = 1)
								BEGIN
									UPDATE SHOPPING_CART SET AMOUNT = AMOUNT + 1 WHERE ClientId = @ClientId AND ProductId = @ProductId
								END
							ELSE
								BEGIN
									INSERT INTO SHOPPING_CART(ClientId, ProductId, Amount) VALUES (@ClientId, @ProductId, 1)
								END

							UPDATE PRODUCT SET Stock = Stock - 1 WHERE ProductId = @ProductId
						END
					ELSE
						BEGIN
							SET @Result = 0
							SET @Message = 'El producto no cuenta con stock disponible'
						END
				END
			ELSE
				BEGIN
					UPDATE SHOPPING_CART SET Amount = Amount - 1 WHERE ClientId = @ClientId AND ProductId = @ProductId
					UPDATE PRODUCT SET Stock = Stock + 1 WHERE ProductId = @ProductId
				END

		COMMIT TRANSACTION OPERATION
	END TRY
	BEGIN CATCH
		SET @Result = 0
		SET @Message = ERROR_MESSAGE()
		ROLLBACK TRANSACTION OPERATION
	END CATCH
END
GO