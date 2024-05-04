using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    //==================================================================================================================
    [Area("Admin")]
    public class SlidersController : Controller
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //INSTANCE VARIABLES.
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //CONSTRUCTORS.
        public SlidersController(IUnitOfWork contenedorTrabajo,
            IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }

        //--------------------------------------------------------------------------------------------------------------
        public IActionResult Index()
        {
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (archivos.Count() > 0)
                {
                    //Nuevo slider
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                    _unitOfWork.SliderRepo.Add(slider);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }

            }

            return View(slider);
        }

        #region Llamadas a la API (SERVICIOS DE BACKEND DONDE SOLO SE RETORNA INFO A TRAVES DE JSON)
        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.SliderRepo.GetAll() });
        }

        #endregion
    }
}
