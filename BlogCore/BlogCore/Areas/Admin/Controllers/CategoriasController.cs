using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{

    //==================================================================================================================
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //--------------------------------------------------------------------------------------------------------------
        public CategoriasController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region LLamadas a la API
        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json( new { data = _unitOfWork.CategoriaRepo.GetAll()});
        }
        #endregion  
    }
}
