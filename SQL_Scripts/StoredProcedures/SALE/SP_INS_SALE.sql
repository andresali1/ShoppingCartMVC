USE DBSHOPPING_CART
GO

CREATE PROC SP_INS_SALE (
	@ClientId INT
	,@ProductAmount INT
	,@Total DECIMAL(10,2)
	,@Contact VARCHAR(50)
	,@LocationId VARCHAR(20)
	,@PhoneNumber VARCHAR(50)
	,@S_address VARCHAR(500)
	,@TransactionId VARCHAR(50)
	,@Sale_Details [oSale_Detail] READONLY
	,@Message VARCHAR(500) OUTPUT
	,@Result BIT OUTPUT
)
AS
BEGIN
	BEGIN TRY
		DECLARE @SaleId INT = 0
		SET @Result = 1
		SET @Message = ''

		BEGIN TRANSACTION REGISTER
			INSERT INTO SALE (ClientId, ProductAmount, Total, Contact, LocationId, PhoneNumber, S_address, TransactionId)
			VALUES (@ClientId, @ProductAmount, @Total, @Contact, @LocationId, @PhoneNumber, @S_address, @TransactionId)

			SET @SaleId = SCOPE_IDENTITY()

			INSERT INTO SALE_DETAILS (SaleId, ProductId, Amount, Total)
			(SELECT @SaleId, ProductId, Amount, Total FROM @Sale_Details)

			DELETE FROM SHOPPING_CART WHERE ClientId = @ClientId
		COMMIT TRANSACTION REGISTER
	END TRY
	BEGIN CATCH
		SET @Result = 0
		SET @Message = ERROR_MESSAGE()
		ROLLBACK TRANSACTION REGISTER
	END CATCH
END
GO