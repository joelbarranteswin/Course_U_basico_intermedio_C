---AGREGA MAS DATOS A LA TABLA VENTAS Y DETALLE VENTAS 

----PROCEDIMIENTOS ALMACENADOS 
----------------------ESTADISTICAS GENERALES
create proc DashboardDatos
@totVentas float output,
@nprod int output,
@nmarc int output,
@ncateg int output,
@nclient int output,
@nprove int output,
@nemple int output
as
Set @totVentas =(select sum(total)as TotalVentas from VENTAS )
Set @nclient=(select count (ID)  as Clientes from CLIENTES)
Set @nprove =(select count (ID) as Proveedores from PROVEEDORES)
Set @nemple  =(select count (ID)as Empleados from EMPLEADOS )
Set @nprod=(select count (ID) as Productos  from PRODUCTOS)
set @nmarc  = (select count (ID) AS marcas from MARCAS)
set @ncateg  = (select count (ID) AS categorias from CATEGORIAS)
go

----------------------TOP 5 PRODUCTOS PREFERIDOS / MAS VENDIDOS 

CREATE PROC ProdPreferidos
AS
SELECT TOP(5) C.CATEGORIA+''+M.MARCA+''+P.DESCRIPCION AS PRODUCTO, COUNT(ID_PRODUCTO) AS CANTIDAD
FROM DETALLE_VENTA AS DV
INNER JOIN PRODUCTOS AS P ON P.ID=DV.ID_PRODUCTO
INNER JOIN CATEGORIAS AS C ON C.ID=P.ID_CATEGORIA
INNER JOIN MARCAS AS M ON M.ID=P.ID_MARCA
GROUP BY DV.ID_PRODUCTO, C.CATEGORIA, P.DESCRIPCION, M.MARCA
ORDER BY COUNT(ID_PRODUCTO) DESC
GO

----------------------CANTIDAD DE PRODUCTOS POR CATEGORIA
CREATE PROC ProdPorCategoria
AS 
SELECT C.CATEGORIA , COUNT(ID_CATEGORIA) AS Cantidad
FROM PRODUCTOS AS P
INNER JOIN CATEGORIAS AS C ON C.ID=P.ID_CATEGORIA
GROUP BY P.ID_CATEGORIA, C.CATEGORIA 
ORDER BY Cantidad
GO 


-- see all procedures
select top 10 * from
sys.procedures

-- MODIFY A VALUE IN A TABLE
UPDATE PRODUCTOS SET DESCRIPCION='Nuevo nombre' WHERE ID=1


-- EDIT VALUES FROM CLIENTES 
UPDATE CLIENTES 
SET NOMBRES='Mary Leyva', 
APELLIDOS='Leyva', 
RUC='123456789',
DIRECCION='--', 
TELEFONO='123456789'
WHERE ID=4;