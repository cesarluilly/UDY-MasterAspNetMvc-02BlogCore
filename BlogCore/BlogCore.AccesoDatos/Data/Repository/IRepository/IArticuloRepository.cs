﻿using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IArticuloRepository : IRepository<Articulo>
    {
        //                                                  //Entity Framework, realmente realiza el seguimeinto
        //                                                  //    del cambios de las entidades, cuando estan vinculados
        //                                                  //    a un contexto.
        //                                                  //Entonces en lugar de tener un metodo explicito de 
        //                                                  //    actualizacion en el repositorio, se espera que las
        //                                                  //    entidades se actualizen a traves del contexto y 
        //                                                  //    se guarden mediante el metodo saveChanges().
        void Update(Articulo articulo);

        //                                                  //Metodo para el buscador.
        //                                                  //Esto es un metodo comun.
        IQueryable<Articulo> AsQueryable();
    }
}
