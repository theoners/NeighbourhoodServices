﻿namespace NeighbourhoodServices.Services.Data.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.ViewModels.Announcements;

    public class AnnouncementService : IAnnouncementService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Announcement> announcementRepository;

        public AnnouncementService(IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<Announcement> announcementRepository)
        {
            this.categoriesRepository = categoriesRepository;
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

        public IEnumerable<T> GetByCategory<T>(string categoryName, int? count = null)
        {
            IQueryable<Announcement> query =
                this.announcementRepository.All().Where(x => x.Category.Name == categoryName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByCreatedOn<T>(int? count = null)
        {
            IQueryable<Announcement> query =
                this.announcementRepository.All().OrderByDescending(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}