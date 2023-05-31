USE DBSHOPPING_CART
GO

--User
SELECT * FROM APP_USER
--
INSERT INTO APP_USER(U_name, U_surname, Email, Pass) VALUES
('test nombre', 'test apellido', 'test@example.com', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')
--('test 2', 'test 2', 'user2@example.com', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')
GO
--
UPDATE APP_USER SET Active = 0 WHERE UserId = 2
--


--Category
SELECT * FROM CATEGORY
--
INSERT INTO CATEGORY(C_description) VALUES
('Tecnología')
,('Muebles')
,('Dormitorio')
,('Deportes')
GO


--Brand
SELECT * FROM BRAND
--
INSERT INTO BRAND(B_description) VALUES
('SONNY')
,('HHP')
,('LGG')
,('HYUNNDAI')
,('CANOON')
,('NIKKE')
GO


--Department
SELECT * FROM DEPARTMENT
--
INSERT INTO DEPARTMENT(DepartmentId, D_description) VALUES
(11 ,'Bogotá D.C.')
,(25 ,'Cundinamarca')
,(05 ,'Antioquia')
GO


--Town
SELECT * FROM TOWN
--
INSERT INTO TOWN(TownId, T_description, DepartmentId) VALUES
(11 ,'Bogotá D.C.', 11)
,(175 ,'Chía', 25)
,(126 ,'Cajicá', 25)
,(1 ,'Medellín', 05)
,(266 ,'Envigado', 05)
GO


--Town location
SELECT * FROM TOWN_LOCATION
--
INSERT INTO TOWN_LOCATION(LocationId, L_description, TownId) VALUES
('11_1' ,'Usaquén', 11)
,('11_2' ,'Chapinero', 11)
,('11_11' ,'Suba', 11)
,('175_175' ,'Chía', 175)
,('126_126' ,'Cajicá', 126)
,('1_14' ,'El Poblado', 1)
,('1_9' ,'Buenos Aires', 1)
,('266_266' ,'Envigado', 266)
GO