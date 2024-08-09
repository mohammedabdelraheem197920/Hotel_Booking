using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;

namespace Infrastructuer.Repository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(HotelDbContext context) : base(context)
        {

        }
    }
}
