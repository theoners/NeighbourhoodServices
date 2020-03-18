namespace NeighbourhoodServices.Services.Data
{
    using System.Linq;

    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public int GetUserCount()
        {
            var userCount =
                this.userRepository.All().Count();


            return userCount;
        }
    }
}