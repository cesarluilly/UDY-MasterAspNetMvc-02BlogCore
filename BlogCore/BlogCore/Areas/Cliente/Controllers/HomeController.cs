using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;

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
        //                                                  //Primera Version de Inicio sin paginacion.
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    HomeVM homeVM = new HomeVM()
        //    {
        //        Sliders = _unitOfWork.SliderRepo.GetAll(),
        //        ListArticulos = _unitOfWork.ArticuloRepo.GetAll()
        //    };

        //    //                                              //Esta línea es para poder saber si estamos
        //    //                                              //    en el home o no
        //    ViewBag.IsHome = true;

        //    return View(homeVM);
        //}

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //Segunda Version pagina de Inicio con Paginacion
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 4)
        {
            var articulos = _unitOfWork.ArticuloRepo.AsQueryable();

            //                                              //Paginar los resultados.
            //                                              //Skip es para omitir elementos.
            //                                              //Take tomo n cantidade de elementos despues de haber omitido.
            var paginatedEntries = articulos.Skip((page - 1) * pageSize).Take(pageSize);

            HomeVM homeVM = new HomeVM()
            {
                Sliders = _unitOfWork.SliderRepo.GetAll(),
                ListArticulos = paginatedEntries.ToList(),
                PageIndex = page, 
                TotalPages = (int)(Math.Ceiling(articulos.Count() / (double)pageSize))
            };

            //                                              //Esta línea es para poder saber si estamos
            //                                              //    en el home o no
            ViewBag.IsHome = true;

            return View(homeVM);
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult ResultadoBusqueda(string searchString, int page = 1, int pageSize = 2)
        {
            var articulos = _unitOfWork.ArticuloRepo.AsQueryable();

            //                                              //Filtra por titulo si hay un termino de busqueda
            if (
                !string.IsNullOrEmpty(searchString)
                )
            {
                articulos = articulos.Where(e => e.Nombre.Contains(searchString));
            }

            //                                              //Paginar los resultados.
            var paginatedEntries = articulos.Skip((page - 1) * pageSize).Take(pageSize);

            //                                              //Crear modelo de la vista.
            var model = new ListaPaginada<Articulo>(paginatedEntries.ToList(), articulos.Count(), page, pageSize, searchString);

            return View(model);
        }

        [HttpGet]
        //==================================================================================================================
        public IActionResult Detalle(int id)
        {
            var articuloDesdeBd = _unitOfWork.ArticuloRepo.Get(id);
            return View(articuloDesdeBd);
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
