USE DBSHOPPING_CART
GO

CREATE PROC SP_DEL_CATEGORY(
	@CategoryId INT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM PRODUCT P
				   INNER JOIN CATEGORY C ON C.CategoryId = P.CategoryId
				   WHERE P.CategoryId = @CategoryId)
		BEGIN
			DELETE TOP(1) FROM CATEGORY
			WHERE CategoryId = @CategoryId

			SET @Result = 1
		END
	ELSE 
		BEGIN
			SET @Message = 'La Categoría se encuentra relacionada a un producto'
		END
END
GO