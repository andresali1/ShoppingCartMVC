## ShoppingCartMVC
Projecto Web que emula un e-commerce de venta de productos por categorías, tiene dos proyectos a ejecutar, uno como administrador de negocio y otro para clientes

### Tecnologías Utilizadas
:keyboard: C# 8  
:keyboard: .Net Framework 4.7.2  
:minidisc: SqlServer 2019  
:computer: Visual Studio 2019  

### :open_book: Configuración  
:one: En una carpeta del sistema ejecutar el comando :arrow_forward: git clone https://github.com/andresali1/ShoppingCartMVC.git  
:two: Dentro de la carpeta "ShoppingCartMVC" abrir la carpeta "SQL_Scripts" y usando SQLServer:
* Ejecutar primero el archivo "DDL_DB_TablesCreation.sql"
* Luego Ejecutar el archivo "DML_FirstInsertion.sql"
* Finalmente Ejecutar todos los scripts dentro de las subcarpetas dentro de la carpeta "StoredProcedures", no importa el orden  
:three: Proyectos para ejecutar:  
:four: Revizar Web.Config:  
:five:  Ejecución:  

* "AdminPresentationLayer": Proyecto con interfaz para administrador de aplicación :arrow_forward: usuario default: test@example.com; contraseña default: test123.
* "StorePresentationLayer": Proyecto con interfaz para usuarios de la aplicación  

* Revisar la cadena de conexión y cambiarla en ambos proyectos por la cadena de la base de datos local
* Revisar las credenciales de envío de correo, se debe usar un correo gmail para la funcionalidad de envío de correo y añadirlas en ambos proyectos  

* Se puede ejecutar un solo proyecto o ambos al mismo tiempo, para esto se debe configurar en visual studio el proyecto de inicio y seleccionar uno de los dos proyectos  
anteriormente mencionados, o elegir los dos proyectos de inicio múltiples
