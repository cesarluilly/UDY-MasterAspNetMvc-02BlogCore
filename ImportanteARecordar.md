#### Borramos cache con Ctrl + F5

#### Snippets C#

prop -> Agregar una propiedad de instancia

ctor -> Agregar el constructor

#### Temas Generales

* Nombre de Controladores va en Plural
* Nombre de Tablas o Modelo va en Singular
* Nombre de Repositorio va en Singular
* Nombre controlador de javascript va en Singular
* Nombre de carpetas de las imagenes, va separado por controlador y en plural

  * ![1714834622882](image/ImportanteARecordar/1714834622882.png)
* Nombre de vistas parciales "_NombreVistaParcial" en singular empezando con Mayuscula

### TagHelpers(Conocido en todo el curso)

* `@Html.DropDownListFor(m => m.Articulo.CategoriaId, Model.ListaCategorias, "-Por favor seleccion una categoria-", new { @class = "form-control" })`
* ``<th>@Html.DisplayNameFor(m => m.Id)</th>``
* `<td>@Html.DisplayFor(m => item.Id)</td> `
* `<p class="text-secondary">@Html.Raw(Model.Descripcion)</p>`
* `@Url.Content(slider.UrlImagen)`
* `<h1>@Html.Raw(slider.Nombre)</h1>`
* `@model BlogCore.Models.ViewModels.ArticuloVM`
* `@{ ViewData["Title"] = "Crear Articulo"; }`
* `@* Coments *@`
* `@section Scripts { }`
* `@using BlogCore`
* `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`
* `<h1>@ViewData["Title"]</h1>`
* `@page`
* `@RenderBody()`
* `@media (min-width: 768px) { html { font-size: 16px; } }`

### Asp- (Conocidos en todo el curso)

* `<form method="POST" asp-action="Create" enctype="multipart/form-data">`
* `<div asp-validation-summary="All" class="text-danger"></div>`
* `<label asp-for="Articulo.Nombre"></label>`
* `<input asp-for="Input.Password" class="form-control" autocomplete="current-password" aria-required="true" placeholder="password" />`
* `<span asp-validation-for="Articulo.Nombre" class="text-danger"></span>`
* `<a asp-page="./Disable2fa" class="btn btn-primary">Disable 2FA</a>`
* `<a asp-area="Identity" asp-page="/Account/Register" class="btn btn-info"> <i class="fas fa-plus"></i> &nbsp; Registra un nuevo usuario </a>s`
  * Para acceder a paginas Razor sin MVC
* `<a class="dropdown-item" asp-area="Admin" asp-controller="Categorias" asp-action="Index">Categorias</a>`
  * Para acceder a paginas de MVC
* `<a asp-action="Bloquear" asp-route-id="@item.Id"> <i class="fas fa-lock-open"></i> </a>`
* `<form asp-page-handler="Confirmation" asp-route-returnUrl="@Model.ReturnUrl" method="post">`
* `<a asp-page="./Register" asp-route-returnUrl="@Model.ReturnUrl">No tienes cuenta? Registrate Aquí</a>`
* `<input class="form-control me-sm-2 d-flex text-dark" asp-controller="Home" asp-action="resultadoBusqueda" method="get" placeholder="Ingresa lo que quieres buscar">`
* `<a class="dropdown-item" asp-area="Admin" asp-controller="Categorias" asp-action="Index">Categorias</a>`
* `<a class="page-link" asp-controller="Home" asp-action="Index" asp-route-page="@(Model.PageIndex - 1)">Anterior</a>`


#### **Uso de Include**

![1714342084289](image/ImportanteARecordar/1714342084289.png)

![1714342796409](image/README/1714342796409.png)

#### Poner bien las etiquetas de HTML, de lo contrario me podria romper lo que intento poner

![1714606424345](image/README/1714606424345.png)

#### Para actualizar una entity, no es necesario consultarlo de la DB para traer el objeto, tambien se puede hacer si front nos envia el objeto ya armado, lo importante es que tenga valor en el Id o llave primaria y los demas campos se supone que son los modificados, y ya despues se pasa al metodo update y lo puedo actualizar

![1714611904762](image/ImportanteARecordar/1714611904762.png)

#### En los metodos de tipo [Delete], si yo le pongo el [ValidateAntiForgeryToken], al momento de consumirlo, no me lo va a reconocer

<pre class="vditor-reset" placeholder="" contenteditable="true" spellcheck="false"><p data-block="0"><img src="https://file+.vscode-resource.vscode-cdn.net/c%3A/Users/Cesar%20Garcia/source/UdemyAspNetMVC/Proyecto2/UDY-MasterAspNetMvc-02BlogCore/image/README/1714616155568.png" alt="1714616155568"/></p></pre>

#### Si en el HTML agrego una columna en la tabla, tambien tengo que agregarlo en el Javascript donde se consume el servicio para renderizarlo, de lo contrario, nos mandara un error

![1714616838211](image/ImportanteARecordar/1714616838211.png)

#### ViewBag. Normalmente se utiliza para comportamientos del HTML, asi como para si estoy o no en una pagina de inicio, o para mandar variables y que interacturen con otras vistas

![1714966440143](image/ImportanteARecordar/1714966440143.png)

![1714966403853](image/ImportanteARecordar/1714966403853.png)

#### Html.Raw

Es para asegurar que se renderize el codigo HTML o para que se ejecute correctamente el Script.

El método `@Html.Raw()` en ASP.NET MVC es útil cuando necesitas renderizar HTML o scripts en tu vista de forma que no sean codificados HTML por defecto. Por default, Razor codifica los contenidos para evitar problemas de seguridad como inyecciones XSS (Cross-Site Scripting). Sin embargo, hay situaciones donde puedes necesitar insertar HTML o scripts que ya están formateados y quieres que se rendericen tal cual en el navegador.

![1715438977933](image/README/1715438977933.png)

![1715439375571](image/README/1715439375571.png)

![1715439317277](image/ImportanteARecordar/1715439317277.png)

### Diferencia entre Razor y MVC

* En MVC se tienen los controladores, metodos(Action) y Vistas
  * ![1715476682877](image/ImportanteARecordar/1715476682877.png)
* Razor es un archivo cshtml con su archivo ligado directamente
  * ![1715476832213](image/README/1715476832213.png)

### Autocomplete

```html
<input type="text" id="direccion" name="direccion" autocomplete="direccion" />`
```

* ![1715479517272](image/ImportanteARecordar/1715479517272.png)

### Request.Form["radUsuarioRole"]

* ![1715495343423](image/ImportanteARecordar/1715495343423.png)

### Solo inyectar el IUnitOfWork en el IoT(contenedor de depencias)

Los repos instanciarlos dentro del constructor del UnitOfWork, y asi nos evitamos inyectarlos en el IoT, ya que ese proceso es mas tedioso

![1715530929953](image/ImportanteARecordar/1715530929953.png)

![1715531778423](image/ImportanteARecordar/1715531778423.png)

### Liga para ir a una pagina de MVC (asp-area, asp-controller y asp-action)

![1715531901300](image/ImportanteARecordar/1715531901300.png)

### Liga para ir a una pagina Razor que no tiene MVC (asp-area y asp-page)

![1715531669030](image/ImportanteARecordar/1715531669030.png)

### Claims

![1715536676291](image/ImportanteARecordar/1715536676291.png)

### Autorizacion a parte de HTML

![1715540574442](image/ImportanteARecordar/1715540574442.png)

### Proteger controladores

Si se pone a nivel Controlador, aplica para todos los Action del controlador

Si se pone a nivel de Action, sobreescribe lo que tenga a nivel de Controlador

* Nivel de Controlador o de Action
  * [Authorize]
    * Cualquiera que este autenticado
  * [Authorize("Administrador")]
    * Solo el Rol Administrador
  * [AllowAnonymous]
    * Define un action como publico, cualquiera puede tener acceso

![1715542994227](image/README/1715542994227.png)

![1715541418167](image/ImportanteARecordar/1715541418167.png)

### Recibir valores en parametros del Action desde un Input

![1715551465136](image/ImportanteARecordar/1715551465136.png)

![1715551521917](image/ImportanteARecordar/1715551521917.png)

### Recibir valores en parametros del Action desde cualquier elemento(Boton, enlace, etc)

![1715554112516](image/ImportanteARecordar/1715554112516.png)

![1715554141671](image/ImportanteARecordar/1715554141671.png)

![1715554202565](image/ImportanteARecordar/1715554202565.png)

### Clase interesante para Paginacion

Es interesante porque es como si se comportara como una lista de elementos, pero a su ves como si se comportara como una clase.

![1715554822421](image/ImportanteARecordar/1715554822421.png)

![1715559722823](image/ImportanteARecordar/1715559722823.png)

![1715559798756](image/ImportanteARecordar/1715559798756.png)

### Segunda Forma de Realizar Paginacion

![1715559967295](image/ImportanteARecordar/1715559967295.png)
