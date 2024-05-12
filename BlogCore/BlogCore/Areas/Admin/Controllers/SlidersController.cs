using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    //==================================================================================================================
    [Authorize(Roles = "Administrador")]
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

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id != null)
            {
                var slider = _unitOfWork.SliderRepo.Get(id.GetValueOrDefault());
                return View(slider);
            }

            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDdesdeBd = _unitOfWork.SliderRepo.Get(slider.Id);

                if (archivos.Count() > 0)
                {
                    //Nuevo imagen slider
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    //var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDdesdeBd.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }


                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                    _unitOfWork.SliderRepo.Update(slider);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aquí sería cuando la imagen ya existe y se conserva
                    slider.UrlImagen = sliderDdesdeBd.UrlImagen;
                }

                _unitOfWork.SliderRepo.Update(slider);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderDesdeBd = _unitOfWork.SliderRepo.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, sliderDesdeBd.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }


            if (sliderDesdeBd == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }

            _unitOfWork.SliderRepo.Remove(sliderDesdeBd);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Slider Borrado Correctamente" });
        }

        #endregion
    }
}
