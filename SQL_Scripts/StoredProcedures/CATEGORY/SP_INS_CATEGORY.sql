USE DBSHOPPING_CART
GO

CREATE PROC SP_INS_CATEGORY(
	@C_description VARCHAR(100)
	,@Active BIT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM CATEGORY WHERE C_description = @C_description)
		BEGIN
			INSERT INTO CATEGORY (C_description, Active) VALUES
			(@C_description, @Active)

			SET @Result = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			SET @Message = 'La categoría ya existe'
		END
END
GO 