using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructuer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelDbContext context;

        public UserRepository(HotelDbContext context)
        {
            this.context = context;
        }

        public void Delete(User item)
        {
            context.Remove(item);
        }

        public List<User> Get(Func<User, bool> where, string? include = null)
        {
            if (include == null)
            {
                return context.Users.Where(where).ToList();
            }
            return context.Users.Include(include).Where(where).ToList();
        }

        public List<User> GetAll(string[] includes = null)
        {
            IQueryable<User> query = context.Users;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public User GetById(int? id, string[] includes = null)
        {
            throw new NotImplementedException();

        }

        public User GetUserById(string userId)
        {
            return context.Users.SingleOrDefault(u => u.Id == userId);
        }

        public List<User> GetRange(Func<User, bool> where, int take, string? include = null)
        {
            if (include == null)
            {
                return context.Users.Where(where).Take(take).ToList();
            }
            return context.Users.Include(include).Where(where).Take(take).ToList();
        }

        public void Insert(User item)
        {
            context.Add(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(User item)
        {
            context.Update(item);
        }
    }
}
