using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{

    //==================================================================================================================
    //                                                      //Namas protegemos solo con estar autenticado
    //[Authorize]
    //                                                      //Protegemos por Autenticacion de Rol "Administrador", 
    //                                                      //    solo el administrador puede acceder a este controlador.
    [Authorize(Roles = "Administrador")]
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
        //                                                  //Por fines practicos, este va a ser publico, cualquiera
        //                                                  //    puede acceder dado que tiene [AllowAnonymous], 
        //                                                  //Sobreescribe a lo que tiene a nivel de controlador.
        [AllowAnonymous]
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

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //Metodo sirve para mostrar el formulario para editar
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = _unitOfWork.CategoriaRepo.Get(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //Metodo sirve para recibir el id para poderlo editar.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            //                                              //Validamos los datos del modelo
            if (ModelState.IsValid)
            {
                //                                          //Logica para actualizar en la DB.
                _unitOfWork.CategoriaRepo.Update(categoria);
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

        //--------------------------------------------------------------------------------------------------------------
        [HttpDelete]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            Categoria categoria = _unitOfWork.CategoriaRepo.Get(id);

            if (
                categoria == null
                )
            {
                return Json(new {success = false, message = "Error borrando categoria"});
            }

            _unitOfWork.CategoriaRepo.Remove(categoria);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Categoria Borrada Correctamente" });
        }

        #endregion  
    }
}
