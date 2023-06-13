CREATE FUNCTION FN_GETSHOPPINGCART_CLIENT(
	@clientId INT
) RETURNS TABLE
AS
RETURN(
	SELECT P.ProductId, B.B_description[Brand], P.P_name, P.Price, C.Amount,
		   P.ImageRoute, P.ImageName
	FROM SHOPPING_CART C
	INNER JOIN PRODUCT P ON P.ProductId = C.ProductId
	INNER JOIN BRAND B ON B.BrandId = P.BrandId
	WHERE ClientId = @clientId
)