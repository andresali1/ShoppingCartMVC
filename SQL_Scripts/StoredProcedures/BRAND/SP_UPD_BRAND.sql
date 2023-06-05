USE DBSHOPPING_CART
GO

CREATE PROC SP_UPD_BRAND(
	@BrandId INT
	,@B_description VARCHAR(100)
	,@Active BIT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM BRAND WHERE B_description = @B_description AND BrandId != @BrandId)
		BEGIN
			UPDATE TOP(1) BRAND SET
				B_description = @B_description
				,Active = @Active
			WHERE BrandId = @BrandId

			SET @Result = 1
		END
	ELSE
		BEGIN
			SET @Message = 'La Marca ya existe'
		END
END
GO