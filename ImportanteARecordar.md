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
