USE DBSHOPPING_CART
GO

CREATE PROC SP_UPD_APP_USER(
	@UserId INT
	,@U_name VARCHAR(50)
	,@U_surname VARCHAR(50)
	,@Email VARCHAR(100)
	,@Active BIT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM APP_USER WHERE Email = @Email AND UserId != @UserId)
		BEGIN
			UPDATE TOP(1) APP_USER SET
				U_name = @U_name
				,U_surname = @U_surname
				,Email = @Email
				,Active = @Active
			WHERE UserId = @UserId

			SET @Result = 1
		END
	ELSE
		BEGIN
			SET @Message = 'El correo del usuario ya existe'
		END
END
GO