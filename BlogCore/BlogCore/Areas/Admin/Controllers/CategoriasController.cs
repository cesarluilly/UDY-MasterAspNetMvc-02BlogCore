using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
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

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //Metodo sirve para mostrar el formulario
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        //                                                  //Metodo sirve para recibir los datos y crear el registro.
        public IActionResult Create(Categoria categoria)
        {
            //                                              //Validamos los datos del modelo
            if (ModelState.IsValid)
            {
                //                                          //Logica para guardar en la DB.
                _unitOfWork.CategoriaRepo.Add(categoria);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                //                                          //En caso de un error, mandara la misma vista de Create
            }

            return View(categoria);
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
