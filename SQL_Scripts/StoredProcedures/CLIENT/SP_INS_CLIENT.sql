USE DBSHOPPING_CART
GO

CREATE PROC SP_INS_CLIENT(
	@C_name VARCHAR(50)
	,@C_surname VARCHAR(50)
	,@Email VARCHAR(100)
	,@Pass VARCHAR(150)
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM CLIENT WHERE Email = @Email)
		BEGIN
			INSERT INTO CLIENT(C_name, C_surname, Email, Pass, IsReset) VALUES
			(@C_name, @C_surname, @Email, @Pass, 0)

			SET @Result = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			SET @Message = 'El correo del usuario ya existe'
		END
END
GO