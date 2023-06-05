USE DBSHOPPING_CART
GO

CREATE PROC SP_INS_PRODUCT(
	@P_name VARCHAR(500)
	,@P_description VARCHAR(500)
	,@BrandId VARCHAR(100)
	,@CategoryId VARCHAR(100)
	,@Price DECIMAL(10,2)
	,@Stock INT
	,@Active BIT
	,@Message VARCHAR(500) OUTPUT
	,@Result INT OUTPUT
)
AS
BEGIN
	SET @Result = 0
	IF NOT EXISTS (SELECT * FROM PRODUCT WHERE P_name = @P_name)
		BEGIN
			INSERT INTO PRODUCT (P_name, P_description, BrandId, CategoryId, Price, Stock, Active) VALUES
			(@P_name, @P_description, @BrandId, @CategoryId, @Price, @Stock, @Active)

			SET @Result = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			SET @Message = 'El producto ya existe'
		END
END
GO