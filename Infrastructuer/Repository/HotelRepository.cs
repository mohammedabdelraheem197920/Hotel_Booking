using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;

namespace Infrastructuer.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelDbContext context) : base(context)
        {

        }
    }
}
