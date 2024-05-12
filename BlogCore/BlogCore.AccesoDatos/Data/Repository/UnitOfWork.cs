using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    //==================================================================================================================
    public class UnitOfWork : IUnitOfWork    
    {
        private readonly ApplicationDbContext _dbContext;

        //--------------------------------------------------------------------------------------------------------------
        public ICategoriaRepository CategoriaRepo { get; private set; }
        public IArticuloRepository ArticuloRepo { get; private set; }
        public ISliderRepository SliderRepo { get; private set; }
        public IUsuarioRepository UsuarioRepo { get; private set; }

        //--------------------------------------------------------------------------------------------------------------
        public UnitOfWork(
            
            ApplicationDbContext dbContext
            )
        {
            this._dbContext = dbContext;

            //                                              //La ventaja de Instanciar aqui los repositorios, 
            //                                              //    es que no tenemos que hacer la instaciacion
            //                                              //    en el program a traves de inyeccion de dependencias, 
            //                                              //    es mas rapido instanciarlo aqui que inyectarlo.
            //                                              //De esta forma en el Program solo inyectamos el
            //                                              //    IUnitOfWork y evitamos agregar cada uno de los repos.
            CategoriaRepo = new CategoriaRepository(_dbContext);
            ArticuloRepo = new ArticuloRepository(_dbContext);
            SliderRepo = new SliderRepository(_dbContext);
            UsuarioRepo = new UsuarioRepository(_dbContext);
        }

        //--------------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            this._dbContext.Dispose();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void Save()
        {
            this._dbContext.SaveChanges();
        }
    }
}
