using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    //==================================================================================================================
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //INSTANCE VARIABLES.
        private readonly ApplicationDbContext _db;

        //--------------------------------------------------------------------------------------------------------------
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //--------------------------------------------------------------------------------------------------------------
        public void Update(Slider slider)
        {
            var objDesdeDb = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;
        }
    }
}
