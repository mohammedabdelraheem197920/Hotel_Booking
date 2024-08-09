using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;

namespace Infrastructuer.Repository
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(HotelDbContext context) : base(context)
        {

        }
    }
}
