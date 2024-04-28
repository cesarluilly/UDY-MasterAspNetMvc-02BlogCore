# UDY-MasterAspNetMvc-02BlogCore

## Seccion 2: Proyecto 1: Crud con Entity Framework Core

### Video 31 Creacion del proyecto

![1713750066509](image/README/1713750066509.png)

Ahora aparece una nuevas carpetas llamadas Areas y Data

![1713750569444](image/README/1713750569444.png)

Esto practicamente ya me configura todo, el appSettings

![1713750820910](image/README/1713750820910.png)

El Program tambien me lo configura

![1713750853880](image/README/1713750853880.png)

Tambien ya me configura las Dependencias

![1713750902373](image/README/1713750902373.png)

### Video 32 Creacion de las libreria de clases

Vamos a utilizar la arquitectura por capas.

![1713751386227](image/README/1713751386227.png)

### Video 33 Instalacion de Extensiones necesarias

![1714010517887](image/README/1714010517887.png)

![1714010639816](image/README/1714010639816.png)

![1714010812764](image/README/1714010812764.png)

![1714011003331](image/README/1714011003331.png)

### Video 34 Organizacion del Proyecto en Areas

* **Creo dos areas de MVC llamados Admin y Cliente**

![1714011156846](image/README/1714011156846.png)

![1714011411684](image/README/1714011411684.png)

**Hacemos cambios escructurales**

* Muevo mi HomeController y mi vista al area cliente
* Complemento en los ViewImports
* Modifico el Programa para que apunte al controlador correcto
* En el ControladorHome indico a que area pertenece

![1714013017531](image/README/1714013017531.png)

![1714013112434](image/README/1714013112434.png)

![1714013059602](image/README/1714013059602.png)

![1714013139757](image/README/1714013139757.png)

### Video 35 Organizacion del Proyecto por niveles

* **Movemos el modelo a su correspondiente capa**
* **La carpeta Data la movemos a su correspondiente capa**
* **La carpeta Migration lo dejo al mismo nivel jerarquico que Data**
* Agrego una referencia al Proyecto de BlogCore para que tenga acceso a la capa "AccesoDatos"
* Agrego una referencia al Proyecto de Acceso a datos para que tenga dependencia con

  * Models
  * Utilidades
* Creamos la carpeta ViewModes en el proyecto de Models

  ![1714015592873](image/README/1714015592873.png)

### Video 36 Configuracion de Bootstrap y Bootswatch

Bootswatch es como la siguiente Fase de Bootstrap, ya que te ofrece todo lo de bootstrap y ademas te

ofrece templates con sus respectivos colores y efectos(Atomos, Moleculas, Organismos)

* Descargo la version minimizada
* Creo un archivo de CSS dentro del directorio "wwwroot/lib/Bootstrap/dis/css/bootstrap5.min.css"
  * Dentro pego el contenido de lo que descargamos
* Vamos a la pagina maestra "_layout.cshtml"
  * **Remuevo** la etiqueta link de "~/lib/bootstrap/dist/css/**bootstrap.min.css**"
  * **Agrego** la etiqueta para el nuevo link de "~/lib/bootstrap/dist/css/**bootstrap5.min.css**"

![1714016301817](image/README/1714016301817.png)

![1714016331947](image/README/1714016331947.png)

![1714017359167](image/README/1714017359167.png)

**Recomendacion de nombre de controladores, EN PLURAL**

* Elimino el nav que estaba en la pagina maestra
* Agrego el nuevo Nav

![1714018571660](image/README/1714018571660.png)

### Video 37 Instalacion de Plugins Frontend

* En la seccion anterior podemos crear tablas basicas, pero si queremos crear tablas mas avanzadas podemos utiliza el Plugin llamado **"DataTables"**
* **sDataTables [CSS, JS]**
* **JQuery [CSS, JS]**
* **Toastr [CSS, JS]** muestra ventanitas
* **sweetalert2[CSS, JS]** para mandar alertas
* **fontawesome.com/icons[CSS]**
* **cdnjs para encontrar las librerias**

  Los CSS se importan al inicio de la pantalla

  Los JS se importan al final de la pagina.

![1714019114531](image/README/1714019114531.png)

### Video 38 Conexion SQL, Contexto, Migraciones y Base de Datos

Renderizo una vista parcial con la etiqueta en el _Layout.

``<partial name="_LoginPartial" />``

Configuramos nuestra cadena de conexion

![1714048570349](image/README/1714048570349.png)

Ejecutamos el comando en el Command Nugget

``Update-Database``

### Video 39 Creacion Modelo Categoria, Migracion y Base de Datos

Se crear el Modelo Categoria

## **Seccion 4: BlogCore - RepositoryPattern**

### Video 40 Fundamentos de RepositoryPattern

![1714050028142](image/README/1714050028142.png)

### Video 41 Introduccion RepositoryPattern

![1714050135737](image/README/1714050135737.png)

### Video 42 Unidad contenedora o unidad de trabajo

https://learn.microsoft.com/es-es/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

![1714050203337](image/README/1714050203337.png)

![1714050564405](image/README/1714050564405.png)

### Video 43 Como es el Flujo de Trabajo con el RepositoryPattern

![1714050685937](image/README/1714050685937.png)

### Video 44 Implementando Repository Parte 1

* Agregamos la carpeta Repository dentro de la capa AccesoDatos/Data
  * Creamos la carpeta IRepository
    * Creamos el interfaz IRepository Generico con su contenido
  * Creamos la clase Repository Generico con su contenido

![1714052115454](image/README/1714052115454.png)

### Video 45 Implementando Repository Parte 2

```}}
public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
{
    //                                              //Se crea una consulta IQueryable a partir del DBSet del 
    //                                              //    contexto.
    IQueryable<T> query = dbSet;

    if (
        //                                          //Existe el filtro.
        filter != null
        )
    {
        query = query.Where(filter);
    }

    //                                              //Se incluyen propiedades de navegacion si se proporciona.
    if (
        includeProperties != null
        )
    {
        //                                          //Take each includeProperty
        foreach (var includeProperty in 
            includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
    }

    //                                              //Se aplica el ordenamiento si se proporciona.
    if (
        orderBy != null
        )
    {
        //                                          //Se ejecuta la funcion y se convierte la consulta
        //                                          //    en una lista.
        return orderBy(query).ToList();
    }

    return query.ToList();
}

public T GetFirstOfDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
{
    //                                              //Se crea una consulta IQueryable a partir del DBSet del 
    //                                              //    contexto.
    IQueryable<T> query = dbSet;

    if (
        //                                          //Existe el filtro.
        filter != null
        )
    {
        query = query.Where(filter);
    }

    //                                              //Se incluyen propiedades de navegacion si se proporciona.
    if (
        includeProperties != null
        )
    {
        //                                          //Take each includeProperty
        foreach (var includeProperty in
            includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
    }

    return query.FirstOrDefault();
}

public void Remove(int id)
{
    T entityToRemove = dbSet.Find(id);
}

public void Remove(T entity)
{
    dbSet.Remove(entity);
}

```

### Video 46 Repositorio Categoria

* Se agrego la Interfaz ICategoria
* Se agrego la clase Categoria

![1714225086240](image/README/1714225086240.png)

![1714194034378](image/README/1714194034378.png)

### Video 47.0 Ejercicio de codificacion 2: Implementacion Patron de Repositorio (RepositoryPattern)

### Video 47.1 Implementando Unidad de Trabajo (Unit Of Work)

Es un patron que se utiliza para gestionar transaciones en un conjunto de operaciones, asegura

que todas las unidades de trabajo, se ejecuten en una transacion.

![1714225237921](image/README/1714225237921.png)

![1714225279348](image/README/1714225279348.png)

![1714225440537](image/README/1714225440537.png)

### Video 48 Crear Controlador Categorias

Nota. El nombre de los controladores lleva el nombre

* Agregamos controlador MVC en blanco en el area de Admin
  * Lo primero que debemos hacer despues de crear un controlador, es ponerle el area al cual pertenece [Area("Admin")]
  * Agregamos su contenido

![1714226660542](image/README/1714226660542.png)

![1714226733967](image/README/1714226733967.png)

* Agregamos la vista razor vacia del controlador del metodo Index.

  ![1714226813224](image/README/1714226813224.png)

### Video 49 Crear Vista Lista de Categorias

Liga para la implementacion de DataTable -> https://datatables.net/examples/data_sources/js_array

![1714228477866](image/README/1714228477866.png)

![1714228494985](image/README/1714228494985.png)

![1714228511235](image/README/1714228511235.png)

![1714228534604](image/README/1714228534604.png)

![1714252630007](image/README/1714252630007.png)

Documento Categoria.js

```javascript
let dataTable;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    //Para mas ejemplos. Consultar la siguiente liga.
    //https://datatables.net/examples/ajax/objects.html
    dataTable = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/admin/categorias/GetAll",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "id", "width": "5%"},
            { "data": "nombre", "width": "50%"},
            { "data": "orden", "width": "20%" },
            {
                "data": "id",   
                //Render ya es de datatable.
                "render": function (data) {
                    //Utilizamos comillas invertidas para no tener problemas con las comillas dobles
                    //    y tambien para acceder rapido a variables a traves del signo de pesos

                    //&nbsp ----> es un espacio
                    return `<div class="text-center">
                                <a href="/Admin/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                    <i class="far fa-edit"></i> Editar
                                </a>
                                 
                                <a onclick=Delete("/Admin/Categorias/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                    <i class="far fa-trash-alt"></i> Borrar
                                </a>
                            </div>
                            `;
                }, 
                "width": "40%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

```

### Video 50 Agregar Contenedor de Trabajo como Inyeccion de Dependencias

![1714253647156](image/README/1714253647156.png)

Ahora si ya podemos correr nuestra aplicacion

![1714255917911](image/README/1714255917911.png)

### Video 51 Metodo y Vista Crear Categoria

* Agregamos el Action Create en el controlador Categoria
* Creamos la vista parcial "_VistaCrearVolver"

  * Esto se hace posicionandonos en carpeta "Shared" damos click derecho y escojemos agregar Vista vacio
  * Para renderizar la vista hacemos uso de la etiqueta partial `<partial name="_VistaCrearVolver" />`

  ![1714258651506](image/README/1714258651506.png)

  ![1714258546255](image/README/1714258546255.png)

![1714258624122](image/README/1714258624122.png)

### Video 52 Funcionalidad Crear Nueva Categoria

* Crear el Action de Edit de tipo [GET] y su Vista(La vista es una copia del del Create)
* Creamos el Action de Edit de tipo [POST] para poder editar la categoria

![1714270421847](image/README/1714270421847.png)

### Video 53 Editar Categoria

### Video 54 Borrar Categoria

Agregamos Librerias Sweetalert y Toast js y css

```html
Al inicio
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.css" />

Al final
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>
```

Creamos el Action de Delete

![1714329436702](image/README/1714329436702.png)

Agregamos la funcion de Javascript de Delete, que va a ser invocado en el metodo

```javascript
function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}
```

### Video 55 Validacion del Lado del Servidor

Esta validacion se logra 

* ```c#
  `if (ModelState.IsValid)
  ```
* ```c#
  <div asp-validation-summary="All" class="text-danger"></div>` O <div asp-validation-summary="ModelOnly" class="text-danger"></div> 
  ```

![1714330486835](image/README/1714330486835.png)

![1714330532212](image/README/1714330532212.png)

![1714330461024](image/README/1714330461024.png)


### Video 56 Validacion del Lado del Cliente

Prestamos atencion al archivo que se creo cuando creamos el proyecto `_ValidationScriptsPartial`

![1714331610879](image/README/1714331610879.png)

Importamos el archivo en los lugares donde deseemos realizar una validacion, por lo general es

en los servicios de tipo POST.

![1714332124303](image/README/1714332124303.png)

Ahora corremos la aplicacion y tecleamos un valor en el Orden, y si nos fijamos sin haberle dado click en el

boton "Crear Categoria", la validacion se hace en automatico

![1714332166236](image/README/1714332166236.png)

## Seccion 6: BlogCore: CRUD Articulos

### Video 57 Crear Modelo Articulo, Migracion y Base de Datos

* Creamos Modelo Articulo
* Agregamos Articulo en el contexto

  ![1714334498561](image/README/1714334498561.png)

### Video 58 Repositorio Articulo y Unidad de trabajo

* Creamos el Repo de Articulo
* Agregamos el Repo de Articulo al UnitOfWork

### Video 59 Controlador y Lista de Articulos

* Agregamos controlador de Articulos
* Agregamos Vista de Articulos
* Agregamos archivo Javascript de Articulo
* Insertamos un Articulo insert into articulo values('Nom1', 'Desc', GETDATE(), 'TestRuta', 5);

![1714342796409](image/README/1714342796409.png)

![1714342540585](image/README/1714342540585.png)

### Video 60 Formulario para Crear Articulo

### Video 61 Lista de categorias al crear Articulo

### Video 62 Implementar Subida de Archivo - Parte 1

### Video 63 Implementar Subida de Archivo - Parte 2

### Video 64 Integrar Editor Tinymce

### Video 65 Vista Editar articulo e Imagen

### Video 66 Funcionalidad Editar Articulo e Imagen

### Video 67 Funcionalidad Borrar articulo

### Video 68 Mostrar Imagen en Datatable

## Seccion 7: Depuracion o Debug

### Video 69

### Video 70 

### Video 71

### Video 72 

### Video 73

### Video 74

### Video 75

### Video 76 

### Video 77

### Video 78

### Video 79

### Video 80

### Video 81

### Video 82

### Video 83

### Video 84 

### Video 85

### Video 86
