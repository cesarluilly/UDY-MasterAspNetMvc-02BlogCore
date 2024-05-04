using BlogCore.AccesoDatos.Data.Repository.IRepository;
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
