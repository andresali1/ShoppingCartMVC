USE DBSHOPPING_CART
GO

CREATE PROC SP_INS_APP_USER(
	@U_name VARCHAR(50)
	,@U_surname VARCHAR(50)
	,@Email VARCHAR(100)
	,@Pass VARCHAR(150)
	,@Active BIT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM APP_USER WHERE Email = @Email)
		BEGIN
			INSERT INTO APP_USER (U_name, U_surname, Email, Pass, Active) VALUES
			(@U_name, @U_surname, @Email, @Pass, @Active)

			SET @Result = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			SET @Message = 'El correo del usuario ya existe'
		END
END
GO