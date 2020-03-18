namespace NeighbourhoodServices.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.ViewModels.Announcement;

    public class AnnouncementService : IAnnouncementService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Service> announcementRepository;

        public AnnouncementService(IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<Service> announcementRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.announcementRepository = announcementRepository;
        }

        public async Task<string> CreateAsync(AnnouncementInputModel announcementInputModel,string userId)
        {
            var announcement = new Service()
            {
                Description = announcementInputModel.Description,
                Place = announcementInputModel.Address,
                ServiceType = announcementInputModel.ServiceType,
                CategoryId = int.Parse(announcementInputModel.Category),
                UserId = userId,
            };

            await this.announcementRepository.AddAsync(announcement);
            await this.announcementRepository.SaveChangesAsync();
            return announcement.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}