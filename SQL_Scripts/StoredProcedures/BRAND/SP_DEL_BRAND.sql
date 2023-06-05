USE DBSHOPPING_CART
GO

CREATE PROC SP_DEL_BRAND(
	@BrandId INT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM PRODUCT P
				   INNER JOIN BRAND B ON B.BrandId = P.BrandId
				   WHERE P.BrandId = @BrandId)
		BEGIN
			DELETE TOP(1) FROM BRAND
			WHERE BrandId = @BrandId

			SET @Result = 1
		END
	ELSE 
		BEGIN
			SET @Message = 'La Marca se encuentra relacionada a un producto'
		END
END
GO