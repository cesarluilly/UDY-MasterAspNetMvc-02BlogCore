using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return this.dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            string? includeProperties = null)
        {
            //                                              //Se crea una consulta IQueryable a partir del DBSet del 
            //                                              //    contexto.
            IQueryable<T> query = dbSet;

            if (
                //                                          //Existe el filtro.
                filter != null
                )
            {
                query = query.Where(filter);
            }

            //                                              //Se incluyen propiedades de navegacion si se proporciona.
            if (
                includeProperties != null
                )
            {
                //                                          //Take each includeProperty
                foreach (var includeProperty in 
                    includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            //                                              //Se aplica el ordenamiento si se proporciona.
            if (
                orderBy != null
                )
            {
                //                                          //Se ejecuta la funcion y se convierte la consulta
                //                                          //    en una lista.
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOfDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            //                                              //Se crea una consulta IQueryable a partir del DBSet del 
            //                                              //    contexto.
            IQueryable<T> query = dbSet;

            if (
                //                                          //Existe el filtro.
                filter != null
                )
            {
                query = query.Where(filter);
            }

            //                                              //Se incluyen propiedades de navegacion si se proporciona.
            if (
                includeProperties != null
                )
            {
                //                                          //Take each includeProperty
                foreach (var includeProperty in
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
