using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    //==================================================================================================================
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //--------------------------------------------------------------------------------------------------------------
        public ArticulosController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

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
            //                                              //Con el IncludeProperties se pueden pasar
            //                                              //    varias tablas que esten relacionadas
            //                                              //    separado por comas.
            return Json(new { data = _unitOfWork.ArticuloRepo.GetAll(includeProperties: "Categoria") });
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
