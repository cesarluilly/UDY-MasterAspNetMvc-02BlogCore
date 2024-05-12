using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
    //==================================================================================================================
    [Authorize(Roles = "Administrador")]
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
            //return View(_unitOfWork.UsuarioRepo.GetAll());

            //                                              //Opcion 2: Obtener todos los usuarios menos el que este
            //                                              //    logueado, para que no se bloquee a si mismo

            //                                              //Los Claims son reclamaciones, son piezas de informacion
            //                                              //    que representan caracteristicas, propiedades
            //                                              //    sobre un usuario que ha sido autenticado en la app.
            //                                              //Son utilizadas en sistemas de autenticacion y 
            //                                              //    autorizacion para proporcionar informacion adicional
            //                                              //    sobre la identidad del usuario.
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;

            //                                              //Con esto obtenemos el usuario autenticado.
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //                                              //Obtenemos todos menos el logueado.
            return View(_unitOfWork.UsuarioRepo.GetAll(u => u.Id != usuarioActual.Value)); ;
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
