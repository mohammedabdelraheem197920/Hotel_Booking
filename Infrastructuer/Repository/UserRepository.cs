using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;

namespace Infrastructuer.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(HotelDbContext context) : base(context)
        {

        }
    }
}
