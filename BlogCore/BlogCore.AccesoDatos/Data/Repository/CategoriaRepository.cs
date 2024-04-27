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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        //--------------------------------------------------------------------------------------------------------------
        public CategoriaRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
                this._dbContext = dbContext;
        }

        //--------------------------------------------------------------------------------------------------------------
        public void Update(Categoria categoria)
        {
            Categoria entity = this._dbContext.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            entity.Nombre = categoria.Nombre;
            entity.Orden = categoria.Orden;

            this._dbContext.SaveChanges();
        }
    }
}
