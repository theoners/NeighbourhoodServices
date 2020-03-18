using System.Collections.Generic;
using System.Linq;
using NeighbourhoodServices.Data.Common.Repositories;
using NeighbourhoodServices.Data.Models;
using NeighbourhoodServices.Services.Mapping;

namespace NeighbourhoodServices.Services.Data
{
    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Announcement> servicesRepository;

        public ServicesService(IDeletableEntityRepository<Announcement> servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        public IEnumerable<T> GetAll<T>(string name , int? count = null)
        {
            IQueryable<Announcement> query =
                this.servicesRepository.All().OrderBy(x => x.CreatedOn).Where(x => x.Category.Name == name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}