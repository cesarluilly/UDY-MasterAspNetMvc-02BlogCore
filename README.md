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

* Agregamos controlador de Articulos y funcionalidad de listado(Es una copia del de categoria con sus modificaciones correspondientes)
* Agregamos Vista de Articulos y html para lista (Es una copia del de categoria con sus modificaciones correspondientes)
* Agregamos archivo Javascript de Articulo para consumir el servicio y mostrarlo en el datatable.
* Insertamos un Articulo insert into articulo values('Nom1', 'Desc', GETDATE(), 'TestRuta', 5);

![1714342796409](image/README/1714342796409.png)

![1714342540585](image/README/1714342540585.png)

### Video 60 Formulario para Crear Articulo

* Creamos el Action de Create(Nos basamos en el action de create de Categoria)
* Creamos su vista para el Create
* En el siguiente video se terminara de implementar un dropdown para elegir la categoria en el dropdown.

  ![1714344276780](image/README/1714344276780.png)

### Video 61 Lista de categorias al crear Articulo

Para trabajar con 2 tablas en una misma vista, tengo que crear un ViewModel, y lo que nos va a permitir es traer

datos de varias tablas.

Nombre se define por

* Nombre de la entidad principal con la que quiero trabajar
* Nombre identificador para ViewModel y puede ser
  * "ViewModel"
  * o Abreviado "VM"

![1714345826405](image/README/1714345826405.png)

![1714346024624](image/README/1714346024624.png)

Despues de esto, tenemos que hacer un cambio en `ArticulosController` en el metodo `Create`.

Para eso agregamos un metodo al Repositorio de Categoria

![1714346756320](image/README/1714346756320.png)

Agregamos el dropdown a traves del TagHelper @Html.DropDownListFor

![1714348903638](image/README/1714348903638.png)

Vamos a correr la aplicacion

* En caso de que mande error, hay que instalar la siguiente extension
  * ![1714348580236](image/README/1714348580236.png)

![1714348847385](image/README/1714348847385.png)

### Video 62 Implementar Subida de Archivo - Parte 1

(NOTA. Se iso un acomodo bien de las etiquetas que habia anteriormente.)

Agregamos al Form el atributo -> enctype="multipart/form-data"

![1714350346654](image/README/1714350346654.png)

Agregamos la etiqueta.

![1714350419031](image/README/1714350419031.png)

Corremos la aplicacion

![1714350521625](image/README/1714350521625.png)

### Video 63 Implementar Subida de Archivo - Parte 2

![1714364242992](image/README/1714364242992.png)

Agrego la propiedad  `private readonly IWebHostEnvironment _hostingEnviroment;`![1714364334301](image/README/1714364334301.png)

Creamos el Action Create con implementacion para subir archivos

![1714364601292](image/README/1714364601292.png)

* **Hemos echo una correcion en el commit del video 65 aqui en esta parte, y para no cambiar todas las imagenes, decidi solo poner la correcion**
* 
* ![1714609491095](image/README/1714609491095.png)

Corremos la app

![1714364683394](image/README/1714364683394.png)

Evidencia de guardado de registro

![1714364756218](image/README/1714364756218.png)

![1714364804708](image/README/1714364804708.png)

![1714364854796](image/README/1714364854796.png)

### Video 64 Integrar Editor Tinymce

* Cambiamos el input de nuestro input de Descripcion por un textarea
* NOTA.
* ![1714606424345](image/README/1714606424345.png)

![1714601833092](image/README/1714601833092.png)

* Procedemos a agregar Tinymce

https://www.tiny.cloud/

![1714595735150](image/README/1714595735150.png)

* Vamos a la liga de cdnjs

Que es un cdnjs?

![1714596170614](image/README/1714596170614.png)![1714596193309](image/README/1714596193309.png)

* y buscamos la libreria de tinymce

![1714595970148](image/README/1714595970148.png)

![1714596284452](image/README/1714596284452.png)

* Vamos a nuestro proyecto y vamos a la vista Create de Articulos y en la seccion de script agregamos lo siguiente

![1714602201858](image/README/1714602201858.png)

Textarea con Tinymce asociado

![1714602056141](image/README/1714602056141.png)

* Ahora le damos en "Crear" y lo guarda

  ![1714607035590](image/README/1714607035590.png)

### Video 65 Vista Editar articulo e Imagen

* Creamos el action Edit para Articulos

  ![1714602806366](image/README/1714602806366.png)
* Creamos la vista de Edit para Articulos(tomamos como base la vista de Create, hacemos un copy-past) y hacemos las modificaciones correspondientes.
* ![1714606946414](image/README/1714606946414.png)
* Lo que nos faltaria seria traer la imagen en la URl
* ![1714609240711](image/README/1714609240711.png)
* Ejemplo de porque a veses si se muestra la imagen y otras veses no.
* ![1714608907092](image/README/1714608907092.png)
* Tambien se modifico el HTML para posicionar los elementos
* ![1714608994161](image/README/1714608994161.png)
* ![1714609064646](image/README/1714609064646.png)

### Video 66 Funcionalidad Editar Articulo e Imagen

* Agregamos un campo oculto para mandar el Id al controlador
* ![1714610150948](image/README/1714610150948.png)
* Hacemos un Copy-Paste del metodo Create de Articulo, para el Edit y hacemos las correspondientes modificaciones
* ![1714612749167](image/README/1714612749167.png)
* ![1714612843792](image/README/1714612843792.png)
* ![1714612881469](image/README/1714612881469.png)

  Corremos y tomamos como base inicial esto

  ![1714611366233](image/README/1714611366233.png)
* Solo cambio el campo nombre
* ![1714612135699](image/README/1714612135699.png)
* ![1714612153853](image/README/1714612153853.png)
* Editamos la imagen
* ![1714612210509](image/README/1714612210509.png)

### Video 67 Funcionalidad Borrar articulo

Nos basamos en el delete del controlador de Categoria(hacemos un copy-paste)

![1714616155568](image/README/1714616155568.png)

* Corremos la aplicacion y borramos

![1714615322074](image/README/1714615322074.png)

![1714615910907](image/README/1714615910907.png)

### Video 68 Mostrar Imagen en Datatable

IMPORTANTE.- Si en el HTML agrego una columna en la tabla, tambien tengo que agregarlo en el Javascript donde se consume el servicio para renderizarlo, de lo contrario, nos mandara un error

![1714616838211](image/ImportanteARecordar/1714616838211.png)

Agregamos su parte de javascript

![1714619015254](image/README/1714619015254.png)

Corremos la aplicacion

![1714619044266](image/README/1714619044266.png)

## Seccion 7: Depuracion o Debug

### Video 69 Depuracion

### Video 70 Tarea - Realizar CRUD para slider

![1714831019046](image/README/1714831019046.png)

## Seccion 9: BlogCore: Pagina de Inicio - Cliente

### Video 73 Controlador y Vista Pagina de Inicio

* Modificamos el Home(index) del controlador Cliente
  * ![1714964273848](image/README/1714964273848.png)![1714964309250](image/README/1714964309250.png)

Resultado

* ![1714964334607](image/README/1714964334607.png)

### Video 74 Pagina de Inicio Slider

* El Slider se va a realiar en el Layout ya que queremos que se vea a pantalla completa
* Vamos al metodo index
* ![1715437766536](image/README/1715437766536.png)
* ![1715437841841](image/README/1715437841841.png)
* ViewBag ahi se almacenan informacion o variables para el control de la pagina
* ![1715437917498](image/README/1715437917498.png)

### Video 75 Slider Activo

### Video 76 Pagina Detalle

Agregamos el Action Detalle

![1715438977933](image/README/1715438977933.png)

![1715439375571](image/README/1715439375571.png)

![1715439317277](image/ImportanteARecordar/1715439317277.png)

## Seccion 10: BlogCore: Identity(Autenticacion)

### Video 77 Introduccion

![1715455636551](image/README/1715455636551.png)

Identity se refiere al sistema de administracion de identidades que proporciona el marco para manejar la autenticacion y autorizacion y autorizacion de usuarios en una aplicacion web.

Este sistema facilita a gestion de usuarios, la autenticacion, la autorizacion y otras caracteristicas relacionadas con la seguridad.

Entonces podemos ver que Identity es un sistema completo, muy robusto y muy seguro para implementar todo lo que tiene que ver con la autenticacion y tambien con la autorizacion.

La autorizacion trata de una ves de loguearme, ver a que puedo y a que no puedo acceder.

Ver siguiente liga

[https://learn.microsoft.com/es-es/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&amp;tabs=visual-studio](https://learn.microsoft.com/es-es/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio)

Cuando manejamos Identity, se van a crear las siguientes tablas

![1715456296430](image/README/1715456296430.png)

Podemos configurar muchas opciones

![1715456407447](image/README/1715456407447.png)

![1715457456796](image/README/1715457456796.png)

### Video 78 Scaffold Identity

![1715457410845](image/README/1715457410845.png)

Un Scaffold, se refiere a la generacion automatica de codigo para implementar ciertas caracteristicas o funcionalidades en una aplicacion web.

![1715457685788](image/README/1715457685788.png)

![1715457789835](image/README/1715457789835.png)

![1715457988886](image/README/1715457988886.png)

Despues de que termine su proceso, vamos a ver varias paginas creadas y estas paginas yo ya los puedo personalizar, etc.

![1715458250577](image/README/1715458250577.png)

Hay que aclarar que **Identity utiliza el sistema Razor** es por eso que aqui no vamos a encontrar controladores, a diferencia de MVC que Existen los controladores y esos tiene metodos y a su ves vistas.

Entonces en Razor tenemos el archivo C# asociado a la vista directamente, .

* MVC
  * ![1715476682877](image/ImportanteARecordar/1715476682877.png)
* Razor
  * ![1715476832213](image/README/1715476832213.png)

### Video 79 Agregar Campos al Identity

* Agregamos un nuevo modelo![1715478652407](image/README/1715478652407.png)
* Agregamos al Context ![1715478734689](image/README/1715478734689.png)
* Generamos la Migracion y el Update de la DB
  * Y los campos del modelo se veran reflejados en la tabla `AspNewUsers`
    * ![1715478852629](image/README/1715478852629.png)

### Video 80 Registro de Usuarios

### Video 81 Ajustes en Layout Acceso

### Video 82 Acceso Login de Usuarios

### Video 83 Implementacion de Roles

### Video 84 Crud Usuarios, Bloquear y Desbloquear Usuarios

### Video 85 Esconder Usuario Autenticado

### Video 86
