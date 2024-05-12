using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    //==================================================================================================================
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //--------------------------------------------------------------------------------------------------------------
        public UsuariosController(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hostingEnviroment
            )
        {
            this._unitOfWork = unitOfWork;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Index()
        {
            //                                              //Opcion 1: Obtener todos los usuarios.
            return View(_unitOfWork.UsuarioRepo.GetAll());
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.UsuarioRepo.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.UsuarioRepo.DesbloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
