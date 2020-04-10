using System.Collections.Generic;
using System.Threading.Tasks;
using NeighbourhoodServices.Services.Mapping;

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

        public T GetUser<T>(string id)
        {
            var user =
                this.userRepository
                    .All()
                    .Where(x => x.Id == id)
                    .To<T>()
                    .FirstOrDefault();

            return user;
        }
    }
}