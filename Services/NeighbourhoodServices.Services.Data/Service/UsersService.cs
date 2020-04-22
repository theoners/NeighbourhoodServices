namespace NeighbourhoodServices.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

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

        public T SearchUser<T>(string userName, string city)
        {
            var userId = this.userRepository
                .All()
                .FirstOrDefault(x => x.UserName.Contains(userName) || x.City.Contains(city))?.Id;
            return this.GetUser<T>(userId);
        }

        public IEnumerable<T> GetTopUsers<T>()
        {
            var query =
                this.userRepository
                    .All().OrderByDescending(x => x.Announcements.Count)
                    .Take(5)
                    .To<T>();

            return query;
        }
    }
}
