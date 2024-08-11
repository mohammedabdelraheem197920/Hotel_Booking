using Core.RepositoryInterfaces;
using Infrastructuer.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructuer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly HotelDbContext context;

        public GenericRepository(HotelDbContext context)
        {
            this.context = context;
        }

        public void Delete(T item)
        {
            context.Remove(item);
        }

        public List<T> Get(Func<T, bool> where, string? include = null)
        {
            if (include == null)
            {
                return context.Set<T>().Where(where).ToList();
            }
            return context.Set<T>().Include(include).Where(where).ToList();
        }

        public List<T> GetAll(string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public T GetById(int? Id, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.First(e => e.Id == Id);
        }

        public List<T> GetRange(Func<T, bool> where, int take, string? include = null)
        {
            if (include == null)
            {
                return context.Set<T>().Where(where).Take(take).ToList();
            }
            return context.Set<T>().Include(include).Where(where).Take(take).ToList();
        }

        public void Insert(T item)
        {
            context.Add(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T item)
        {
            context.Update(item);
        }
    }
}
