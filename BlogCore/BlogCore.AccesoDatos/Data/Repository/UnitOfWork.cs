﻿using BlogCore.AccesoDatos.Data.Repository.IRepository;
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

        //--------------------------------------------------------------------------------------------------------------
        public UnitOfWork(
            
            ApplicationDbContext dbContext
            )
        {
            this._dbContext = dbContext;
            CategoriaRepo = new CategoriaRepository(_dbContext);
            ArticuloRepo = new ArticuloRepository(_dbContext);
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
