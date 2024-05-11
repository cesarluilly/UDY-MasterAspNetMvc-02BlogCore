using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Cliente.Controllers
{
    //==================================================================================================================
    [Area("Cliente")]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        //--------------------------------------------------------------------------------------------------------------
        public HomeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        //--------------------------------------------------------------------------------------------------------------
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Sliders = _unitOfWork.SliderRepo.GetAll(),
                ListArticulos = _unitOfWork.ArticuloRepo.GetAll()
            };

            //                                              //Esta línea es para poder saber si estamos
            //                                              //    en el home o no
            ViewBag.IsHome = true;

            return View(homeVM);
        }

        //--------------------------------------------------------------------------------------------------------------
        public IActionResult Privacy()
        {
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
