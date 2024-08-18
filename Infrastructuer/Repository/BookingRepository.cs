using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructuer.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext context;

        public BookingRepository(HotelDbContext context)
        {
            this.context = context;
        }

        public void Delete(Booking item)
        {
            context.Remove(item);
        }

        public List<Booking> Get(Func<Booking, bool> where, string? include = null)
        {
            if (include == null)
            {
                return context.Bookings.Where(where).ToList();
            }
            return context.Bookings.Include(include).Where(where).ToList();
        }

        public List<Booking> GetAll(string userId, string[] includes = null)
        {
            IQueryable<Booking> query = context.Bookings.Where(b => b.UserId == userId);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public Booking GetById(int? id, string[] includes = null)
        {
            IQueryable<Booking> query = context.Bookings;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(e => e.Id == id);
        }

        //public User GetUserById(string userId)
        //{
        //    return context.Users.SingleOrDefault(u => u.Id == userId);
        //}

        //public List<Booking> GetRange(Func<Booking, bool> where, int take, string? include = null)
        //{
        //    if (include == null)
        //    {
        //        return context.Bookings.Where(where).Take(take).ToList();
        //    }
        //    return context.Bookings.Include(include).Where(where).Take(take).ToList();
        //}

        public void Insert(Booking item)
        {
            context.Add(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Booking item)
        {
            context.Update(item);
        }

        public User GetUserById(string userId)
        {
            return context.Users.SingleOrDefault(u => u.Id == userId);
        }

        public Booking? Find(Expression<Func<Booking, bool>> criteria, string[]? includes = null)
        {
            IQueryable<Booking> query = context.Bookings;

            if (criteria is not null)
            {
                query = query.Where(criteria);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault();
        }
    }
}
