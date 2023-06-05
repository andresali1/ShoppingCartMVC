USE DBSHOPPING_CART
GO

CREATE PROC SP_UPD_CATEGORY(
	@CategoryId INT
	,@C_description VARCHAR(100)
	,@Active BIT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM CATEGORY WHERE C_description = @C_description AND CategoryId != @CategoryId)
		BEGIN
			UPDATE TOP(1) CATEGORY SET
				C_description = @C_description
				,Active = @Active
			WHERE CategoryId = @CategoryId

			SET @Result = 1
		END
	ELSE
		BEGIN
			SET @Message = 'La Categoría ya existe'
		END
END
GO