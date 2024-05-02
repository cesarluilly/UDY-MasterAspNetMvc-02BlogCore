using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    //==================================================================================================================
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //                                                  //Lo que nos va a permiti es acceder a las carpetas que
        //                                                  //    tengo en la raiz, ejemplo el wwwroot y guardar 
        //                                                  //    la imagen, incluso obtenerla.
        private readonly IWebHostEnvironment _hostingEnviroment;

        //--------------------------------------------------------------------------------------------------------------
        public ArticulosController(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hostingEnviroment
            )
        {
            this._unitOfWork = unitOfWork;
            this._hostingEnviroment = hostingEnviroment;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo = new BlogCore.Models.Articulo(),
                ListaCategorias = _unitOfWork.CategoriaRepo.GetListaCategorias()
            };

            return View(artiVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo = new BlogCore.Models.Articulo(),
                ListaCategorias = _unitOfWork.CategoriaRepo.GetListaCategorias()
            };

            if (
                id != null
                )
            {
                artiVM.Articulo = _unitOfWork.ArticuloRepo.Get(id.GetValueOrDefault());
            }

            return View(artiVM);
        }

        #region LLamadas a la API
        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult GetAll()
        {
            //                                              //Con el IncludeProperties se pueden pasar
            //                                              //    varias tablas que esten relacionadas
            //                                              //    separado por comas.
            return Json(new { data = _unitOfWork.ArticuloRepo.GetAll(includeProperties: "Categoria") });
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM articuloVM) {
            if (ModelState.IsValid)
            {
                //                                          //Obtenemos la ruta de wwwroot.
                String rutaPrincipal = _hostingEnviroment.WebRootPath;
                //                                          //Gestionamos la subida de archivos a traves de Form HTTP
                IFormFileCollection archivos = HttpContext.Request.Form.Files;
                //                                          //Validamos que sea un articulo nuevo
                //                                          //    y que se haya seleccionado un archivo.
                if (articuloVM.Articulo.Id == 0 && archivos.Count() > 0)
                {
                    String nombreArchivo = Guid.NewGuid().ToString();
                    String extensionArchivo = Path.GetExtension(archivos[0].FileName);

                    //                                      //la carpeta imagenes\articulos ya debe estar creada.
                    var FolderSubidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    using (var fileStreams = new FileStream(Path.Combine(FolderSubidas, nombreArchivo + extensionArchivo), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    articuloVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extensionArchivo;
                    articuloVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _unitOfWork.ArticuloRepo.Add(articuloVM.Articulo);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //                                      //Manera de crear un error personalizado.
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }

            //                                              //Devuelvo de nuevo la lista de categorias para no perder
            //                                              //    la seleccion del usuario,
            //                                              //    de lo contrario estaria tronando en el dropdown.
            articuloVM.ListaCategorias = _unitOfWork.CategoriaRepo.GetListaCategorias();
            return View(articuloVM);
        }



        ////--------------------------------------------------------------------------------------------------------------
        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{

        //    Categoria categoria = _unitOfWork.CategoriaRepo.Get(id);

        //    if (
        //        categoria == null
        //        )
        //    {
        //        return Json(new { success = false, message = "Error borrando categoria" });
        //    }

        //    _unitOfWork.CategoriaRepo.Remove(categoria);
        //    _unitOfWork.Save();
        //    return Json(new { success = true, message = "Categoria Borrada Correctamente" });
        //}

        #endregion
    }
}
