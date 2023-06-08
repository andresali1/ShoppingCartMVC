USE DBSHOPPING_CART
GO

CREATE PROC SP_CON_SalesReport(
	@BeginDate VARCHAR(10)
	,@EndDate VARCHAR(10)
	,@TransactionId VARCHAR(50)
)
AS
BEGIN
	SET DATEFORMAT dmy;

	SELECT CONVERT(CHAR(10), S.SaleDate, 103)[SaleDate], CONCAT(C.C_name, ' ', C.C_surname)[R_client],
		   P.P_name[R_product], P.Price, SD.Amount, SD.Total, S.TransactionId
	FROM SALE_DETAILS SD
	INNER JOIN PRODUCT P ON P.ProductId = SD.ProductId
	INNER JOIN SALE S ON S.SaleId = SD.SaleId
	INNER JOIN CLIENT C ON C.ClientId = S.ClientId
	WHERE CONVERT(DATE, S.SaleDate) BETWEEN @BeginDate AND @EndDate
	AND S.TransactionId = IIF(@TransactionId = '', S.TransactionId, @TransactionId)
END
GO