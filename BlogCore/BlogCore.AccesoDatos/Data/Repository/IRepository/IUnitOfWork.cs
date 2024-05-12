using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    //==================================================================================================================
    public interface IUnitOfWork : IDisposable
    {
        //                                                  //IDisposable es generalmente utilizado para liberar
        //                                                  //    recursos no administrados como conexiones
        //                                                  //    a base de datos, archivos y permite
        //                                                  //    que se liberen estos recursos cuando ya
        //                                                  //    no se necesite.

        public ICategoriaRepository CategoriaRepo { get; }
        public IArticuloRepository ArticuloRepo { get; }
        public ISliderRepository SliderRepo { get; }
        public IUsuarioRepository UsuarioRepo { get; }

        void Save();
    }
}
