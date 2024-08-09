using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;

namespace Infrastructuer.Repository
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(HotelDbContext context) : base(context)
        {

        }
    }
}
