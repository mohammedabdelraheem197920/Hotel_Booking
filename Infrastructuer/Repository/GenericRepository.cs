﻿using Core.RepositoryInterfaces;
using Infrastructuer.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructuer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
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

        public List<T> GetAll(string? include = null)
        {
            if (include == null)
            {
                return context.Set<T>().ToList();
            }
            return context.Set<T>().Include(include).ToList();
        }

        public T GetById(int? id)
        {
            return context.Set<T>().Find(id);
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
