namespace NeighbourhoodServices.Services.Data.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.ViewModels.Announcements;

    using static NeighbourhoodServices.Common.AnnouncementsConstants;

    public class AnnouncementService : IAnnouncementService
    {
        private readonly IDeletableEntityRepository<Announcement> announcementRepository;


        public AnnouncementService(IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<Announcement> announcementRepository)
        {
            this.announcementRepository = announcementRepository;
        }

        public async Task<string> CreateAsync(AnnouncementInputModel announcementInputModel, string userId)
        {
            var announcement = new Announcement()
            {
                Title = announcementInputModel.Title,
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

        public IEnumerable<T> GetByCategory<T>(string categoryName, int skip = 0)
        {
            var query =
                 this.announcementRepository
                     .All()
                     .Where(x => x.Category.Name == categoryName)
                     .Skip(skip)
                     .Take(AnnouncementsPerPage);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByCreatedOn<T>(int skip = 0)
        {
            var query =
                 this.announcementRepository
                     .All()
                     .OrderByDescending(x => x.CreatedOn)
                     .Skip(skip)
                     .Take(AnnouncementsPerPage);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByUser<T>(string userId, int skip = 0)
        {
            var query =
                 this.announcementRepository
                     .All()
                     .Where(x => x.UserId == userId)
                     .OrderByDescending(x => x.CreatedOn)
                     .Skip(skip)
                     .Take(AnnouncementsPerPage);

            return query.To<T>().ToList();
        }

        public T GetDetails<T>(string id)
        {
            var announcement =
                 this.announcementRepository
                     .All()
                     .Where(x => x.Id == id)
                     .To<T>()
                     .FirstOrDefault();

            return announcement;
        }

        public int AllAnnouncementByCategoryCount(string categoryName)
        {
            return this.announcementRepository.All().Count(x => x.Category.Name == categoryName);
        }

        public int AllAnnouncementCount()
        {
            return this.announcementRepository.All().Count();
        }

        public async Task<int> DeleteAsync(string id)
        {
            var announcement = await this.announcementRepository.GetByIdWithDeletedAsync(id);
            this.announcementRepository.Delete(announcement);
            return await this.announcementRepository.SaveChangesAsync();
        }
    }
}
