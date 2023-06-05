USE DBSHOPPING_CART
GO

CREATE PROC SP_INS_BRAND(
	@B_description VARCHAR(100)
	,@Active BIT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM BRAND WHERE B_description = @B_description)
		BEGIN
			INSERT INTO BRAND (B_description, Active) VALUES
			(@B_description, @Active)

			SET @Result = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			SET @Message = 'La marca ya existe'
		END
END
GO