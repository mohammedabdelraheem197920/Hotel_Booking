using Core.Models;

namespace Core.RepositoryInterfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        public User GetUserById(string userId);

    }
}
