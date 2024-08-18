using Core.Models;

namespace Core.RepositoryInterfaces
{
    public interface IBookingRepository
    {
        public User GetUserById(string userId);
        public List<Booking> GetAll(string userId, string[] include = null);

        public Booking GetById(int? id, string[] includes = null);

        List<Booking> Get(Func<Booking, bool> where, string? include = null);
        //public List<T> GetRange(Func<T, bool> where, int take, string? include = null);

        void Insert(Booking item);

        void Update(Booking item);

        void Delete(Booking item);

        void Save();



    }
}
