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
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _dbContext;

        //--------------------------------------------------------------------------------------------------------------
        public ArticuloRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
                this._dbContext = dbContext;
        }

        //--------------------------------------------------------------------------------------------------------------
        public void Update(Articulo articulo)
        {
            Articulo articuloEntity = this._dbContext.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            articuloEntity.Nombre = articulo.Nombre;
            articuloEntity.Descripcion = articulo.Descripcion;
            articuloEntity.UrlImagen = articulo.UrlImagen;
            articuloEntity.CategoriaId = articulo.CategoriaId;

            //this._dbContext.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public IQueryable<Articulo> AsQueryable()
        {
            return _dbContext.Set<Articulo>().AsQueryable();
        }
    }
}
