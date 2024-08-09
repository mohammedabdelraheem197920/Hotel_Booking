using Core.RepositoryInterfaces;
using Core.ServiceInterfaces;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

    }
}
