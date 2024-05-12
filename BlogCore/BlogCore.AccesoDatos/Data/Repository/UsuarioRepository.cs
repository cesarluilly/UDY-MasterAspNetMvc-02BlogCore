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
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        //--------------------------------------------------------------------------------------------------------------
        public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
                this._dbContext = dbContext;
        }

        //--------------------------------------------------------------------------------------------------------------
        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDB = _dbContext.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            //                                              //Agregamos que estara bloqueado 1000 a partir de este
            //                                              //    momento.
            usuarioDesdeDB.LockoutEnd = DateTime.Now.AddYears(1000);
            _dbContext.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDB = _dbContext.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            //                                              //A partir de ahora, va a tomar la fecha y hora y va a 
            //                                              //    desbloquear el usuario.
            usuarioDesdeDB.LockoutEnd = DateTime.Now;
            _dbContext.SaveChanges();
        }
    }
}
